using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class PartsDialog : Form
    {
        private readonly PartsViewModel _viewModel;
        private readonly int? _partId;
        private readonly bool _isDeleteMode;
        private Part _part;
        private Label lblMessage;
        private System.Windows.Forms.Timer _errorTimer;

        public PartsDialog(PartsViewModel viewModel, int? partId, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _partId = partId;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false; // Panel har doim yashirin
            btnSave.Enabled = true; // Tugma har doim faol
            if (!_isDeleteMode)
            {
                LoadPart();
            }
            else
            {
                SetupDeleteModeUI();
            }
        }

        private void LoadPart()
        {
            try
            {
                _part = _partId == null ? new Part() : _viewModel.LoadParts().Find(p => p.PartID == _partId);
                if (_part == null && _partId != null)
                {
                    ShowError("Деталь не найдена.");
                    return;
                }

                var brands = _viewModel.LoadBrands();
                cmbBrand.DataSource = brands;
                cmbBrand.DisplayMember = "CarBrandName";
                cmbBrand.ValueMember = "Id";

                var manufacturers = _viewModel.LoadManufacturers();
                cmbManufacturer.DataSource = manufacturers;
                cmbManufacturer.DisplayMember = "Name";
                cmbManufacturer.ValueMember = "ManufacturerID";

                var qualities = _viewModel.LoadQualities();
                cmbQuality.DataSource = qualities;
                cmbQuality.DisplayMember = "Name";
                cmbQuality.ValueMember = "QualityID";

                if (_partId != null)
                {
                    cmbBrand.SelectedValue = _part.CarBrandId;
                    txtCatalogNumber.Text = _part.CatalogNumber;
                    cmbManufacturer.SelectedValue = _part.ManufacturerID;
                    cmbQuality.SelectedValue = _part.QualityID;
                    txtPartName.Text = _part.PartName;
                    txtCharacteristics.Text = _part.Characteristics;
                    txtPhotoPath.Text = _part.PhotoPath;
                    UpdatePhotoPreview();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке детали: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadPart Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void SetupDeleteModeUI()
        {
            this.ClientSize = new Size(434, 142);
            this.Text = "Подтверждение удаления";

            lblMessage = new Label
            {
                AutoSize = true,
                Text = "Вы хотите удалить эту деталь?",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Name = "lblMessage",
                Anchor = AnchorStyles.Left,
                AccessibleName = "Сообщение об удалении",
                AccessibleDescription = "Подтверждение удаления детали"
            };

            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            tableLayoutPanel.Controls.Add(lblMessage, 0, 0);
            tableLayoutPanel.SetColumnSpan(lblMessage, 2);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 1);
            tableLayoutPanel.Controls.Add(btnSave, 1, 1);

            btnCancel.Text = "Нет";
            btnSave.Text = "Да";
            btnCancel.BackColor = Color.FromArgb(25, 118, 210);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnSave.BackColor = Color.FromArgb(220, 53, 69);
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 100, 100);
        }

        private void ValidateInputs()
        {
            if (_isDeleteMode) return;

            lblError.Visible = false; // Har safar tekshiruvda panelni yashirish

            if (cmbBrand.SelectedValue == null)
            {
                ShowError("Пожалуйста, выберите марку автомобиля.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCatalogNumber.Text))
            {
                ShowError("Пожалуйста, введите каталожный номер.");
                return;
            }

            if (txtCatalogNumber.Text.Length > 50)
            {
                ShowError("Каталожный номер не должен превышать 50 символов.");
                return;
            }

            if (!Regex.IsMatch(txtCatalogNumber.Text, @"^[a-zA-Z0-9]+$"))
            {
                ShowError("Каталожный номер должен содержать только латинские буквы и цифры.");
                return;
            }

            if (cmbManufacturer.SelectedValue == null)
            {
                ShowError("Пожалуйста, выберите производителя.");
                return;
            }

            if (cmbQuality.SelectedValue == null)
            {
                ShowError("Пожалуйста, выберите качество детали.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPartName.Text))
            {
                ShowError("Пожалуйста, введите название детали.");
                return;
            }

            if (txtPartName.Text.Length > 100)
            {
                ShowError("Название детали не должно превышать 100 символов.");
                return;
            }

            if (!string.IsNullOrEmpty(txtPhotoPath.Text))
            {
                string fullPath = Path.Combine(Application.StartupPath, txtPhotoPath.Text);
                if (!File.Exists(fullPath))
                {
                    ShowError("Указанный путь к фотографии недействителен.");
                    return;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Yakuniy tekshiruvni amalga oshirish
                ValidateInputs();
                if (lblError.Visible) return; // Agar xatolik bo‘lsa, saqlashni to‘xtatish

                if (_isDeleteMode)
                {
                    _viewModel.DeletePart(_partId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                _part.CarBrandId = (int)cmbBrand.SelectedValue;
                _part.CatalogNumber = txtCatalogNumber.Text.Trim();
                _part.ManufacturerID = (int)cmbManufacturer.SelectedValue;
                _part.QualityID = (int)cmbQuality.SelectedValue;
                _part.PartName = txtPartName.Text.Trim();
                _part.Characteristics = txtCharacteristics.Text.Trim();
                _part.PhotoPath = txtPhotoPath.Text.Trim();

                if (_partId == null)
                {
                    _viewModel.AddPart(_part);
                }
                else
                {
                    _part.PartID = _partId.Value;
                    _viewModel.UpdatePart(_part);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при сохранении: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"BtnSave_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnBrowsePhoto_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Изображения (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Все файлы (*.*)|*.*";
                    openFileDialog.Title = "Выберите фотографию детали";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string photoDir = Path.Combine(Application.StartupPath, "Resources", "PartsPhoto");
                        if (!Directory.Exists(photoDir))
                        {
                            Directory.CreateDirectory(photoDir);
                        }

                        string fileName = Path.GetFileName(openFileDialog.FileName);
                        string destPath = Path.Combine(photoDir, fileName);
                        File.Copy(openFileDialog.FileName, destPath, true);

                        txtPhotoPath.Text = Path.Combine("Resources", "PartsPhoto", fileName);
                        UpdatePhotoPreview();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при выборе фотографии: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"BtnBrowsePhoto_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnManageQualities_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new PartsQualitiesDialog(_viewModel))
                {
                    dialog.ShowDialog();
                    var qualities = _viewModel.LoadQualities();
                    cmbQuality.DataSource = qualities;
                    cmbQuality.DisplayMember = "Name";
                    cmbQuality.ValueMember = "QualityID";
                    if (_partId != null)
                    {
                        cmbQuality.SelectedValue = _part.QualityID;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при управлении качествами: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"BtnManageQualities_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void UpdatePhotoPreview()
        {
            try
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = null;
                if (!string.IsNullOrEmpty(txtPhotoPath.Text))
                {
                    string fullPath = Path.Combine(Application.StartupPath, txtPhotoPath.Text);
                    if (File.Exists(fullPath))
                    {
                        using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        {
                            pictureBox.Image = Image.FromStream(stream);
                        }
                        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке изображения: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UpdatePhotoPreview Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            panelError.Visible = true;
            _errorTimer.Start();
        }

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            panelError.Visible = false;
            _errorTimer.Stop();
        }

        private void CmbBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Faqat tugma holatini yangilash
        }

        private void TxtCatalogNumber_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCatalogNumber.Text) && !Regex.IsMatch(txtCatalogNumber.Text, @"^[a-zA-Z0-9]+$"))
            {
                ShowError("Каталожный номер должен содержать только латинские буквы и цифры.");
            }
        }

        private void CmbManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Faqat tugma holatini yangilash
        }

        private void CmbQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Faqat tugma holatini yangilash
        }

        private void TxtPartName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPartName.Text) && txtPartName.Text.Length > 100)
            {
                ShowError("Название детали не должно превышать 100 символов.");
            }
        }

        private void TxtPhotoPath_TextChanged(object sender, EventArgs e)
        {
            UpdatePhotoPreview();
            if (!string.IsNullOrEmpty(txtPhotoPath.Text))
            {
                string fullPath = Path.Combine(Application.StartupPath, txtPhotoPath.Text);
                if (!File.Exists(fullPath))
                {
                    ShowError("Указанный путь к фотографии недействителен.");
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pictureBox.Image?.Dispose();
                _errorTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PartsDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
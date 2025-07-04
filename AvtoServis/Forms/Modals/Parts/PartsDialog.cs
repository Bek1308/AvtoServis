using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;

namespace AvtoServis.Forms.Controls
{
    public partial class PartsDialog : Form
    {
        private readonly PartsViewModel _viewModel;
        private readonly int? _partId;
        private readonly bool _isDeleteMode;
        private Part _part;
        private System.Windows.Forms.Timer _errorTimer;

        public PartsDialog(PartsViewModel viewModel, int? partId, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _partId = partId;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            if (!_isDeleteMode)
            {
                LoadPart();
            }
            else
            {
                SetupDeleteModeUI();
            }
            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Форма для добавления или редактирования детали");
            toolTip.SetToolTip(lblBrand, "Выберите марку автомобиля");
            toolTip.SetToolTip(cmbBrand, "Список доступных марок автомобилей");
            toolTip.SetToolTip(lblCatalogNumber, "Введите каталожный номер детали");
            toolTip.SetToolTip(txtCatalogNumber, "Каталожный номер детали (только латинские буквы и цифры)");
            toolTip.SetToolTip(lblManufacturer, "Выберите производителя детали");
            toolTip.SetToolTip(cmbManufacturer, "Список доступных производителей");
            toolTip.SetToolTip(lblQuality, "Выберите качество детали");
            toolTip.SetToolTip(cmbQuality, "Список доступных уровней качества");
            toolTip.SetToolTip(lblPartName, "Введите название детали");
            toolTip.SetToolTip(txtPartName, "Название детали");
            toolTip.SetToolTip(lblCharacteristics, "Введите характеристики детали");
            toolTip.SetToolTip(txtCharacteristics, "Характеристики детали (опционально)");
            toolTip.SetToolTip(lblPhotoPath, "Выберите фотографию детали");
            toolTip.SetToolTip(txtPhotoPath, "Путь к файлу фотографии");
            toolTip.SetToolTip(btnBrowsePhoto, "Выбрать файл фотографии");
            toolTip.SetToolTip(pictureBox, "Предпросмотр фотографии детали");
            toolTip.SetToolTip(btnCancel, "Отменить изменения");
            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление детали" : "Сохранить изменения");
            toolTip.SetToolTip(lblError, "Сообщение об ошибке");
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

            var lblMessage = new Label
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

        private void BtnBrowsePhoto_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        txtPhotoPath.Text = openFileDialog.FileName;
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

        private void UpdatePhotoPreview()
        {
            try
            {
                pictureBox.Image = null;
                if (!string.IsNullOrEmpty(txtPhotoPath.Text) && File.Exists(txtPhotoPath.Text))
                {
                    using (var img = Image.FromFile(txtPhotoPath.Text))
                    {
                        pictureBox.Image = new Bitmap(img, pictureBox.Size);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке изображения: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"UpdatePhotoPreview Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
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
                _part.PhotoPath = txtPhotoPath.Text;

                if (_partId == null)
                {
                    _viewModel.AddPart(_part);
                }
                else
                {
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            _errorTimer.Stop();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            _errorTimer.Stop();
            _errorTimer.Start();
        }
    }
}
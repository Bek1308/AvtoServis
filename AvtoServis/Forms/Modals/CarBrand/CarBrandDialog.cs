using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System.Text.RegularExpressions;

namespace AvtoServis.Forms.Controls
{
    public partial class CarBrandDialog : Form
    {
        private readonly CarBrandViewModel _viewModel;
        private readonly int? _brandId;
        private readonly bool _isDeleteMode;
        private readonly bool _isViewOnly;
        private CarBrand _brand;
        private System.Windows.Forms.Timer _errorTimer;

        public CarBrandDialog(CarBrandViewModel viewModel, int? brandId, bool isViewOnly = false, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _brandId = brandId;
            _isDeleteMode = isDeleteMode;
            _isViewOnly = isViewOnly;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            SetToolTips();
            if (!_isDeleteMode)
            {
                LoadBrand();
            }
            else
            {
                SetupDeleteModeUI();
            }
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Форма для добавления или редактирования марки автомобиля");
            toolTip.SetToolTip(lblBrand, "Метка для ввода названия марки");
            toolTip.SetToolTip(txtBrand, _isDeleteMode ? "Название марки автомобиля" : "Введите название марки (латинские буквы, цифры, пробел, максимум 100 символов)");
            toolTip.SetToolTip(lblError, "Отображает ошибки или сообщения об операции");
            toolTip.SetToolTip(btnCancel, _isDeleteMode ? "Отменить удаление" : "Закрыть без сохранения");
            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление" : "Сохранить изменения");
            if (lblMessage != null)
                toolTip.SetToolTip(lblMessage, "Подтверждение удаления марки");
        }

        private void LoadBrand()
        {
            try
            {
                _brand = _brandId == null ? new CarBrand() : _viewModel.LoadBrands().Find(b => b.Id == _brandId);
                if (_brand == null && _brandId != null)
                {
                    ShowError("Марка не найдена.");
                    btnSave.Enabled = false;
                    return;
                }

                txtBrand.Text = _brand?.CarBrandName;

                if (_isViewOnly)
                {
                    txtBrand.Enabled = false;
                    btnSave.Visible = false;
                    btnCancel.Text = "Закрыть";
                    Text = "Просмотр марки";
                }

                ValidateInputs();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке марки: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadBrand Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void SetupDeleteModeUI()
        {
            this.ClientSize = new Size(434, 142);
            this.Text = "Подтверждение удаления";

            lblMessage = new Label
            {
                AutoSize = true,
                Text = "Вы хотите удалить эту марку?",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Name = "lblMessage",
                Anchor = AnchorStyles.Left,
                AccessibleName = "Сообщение об удалении",
                AccessibleDescription = "Подтверждение удаления марки"
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
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 77, 77);
        }

        private void ValidateInputs()
        {
            if (_isDeleteMode || _isViewOnly) return;

            lblError.Visible = false;
            btnSave.Enabled = true;

            if (string.IsNullOrWhiteSpace(txtBrand.Text))
            {
                ShowError("Пожалуйста, введите название марки.");
                btnSave.Enabled = false;
                return;
            }

            if (txtBrand.Text.Length > 100)
            {
                ShowError("Название марки не должно превышать 100 символов.");
                btnSave.Enabled = false;
                return;
            }

            if (!Regex.IsMatch(txtBrand.Text, @"^[a-zA-Z0-9\s]+$"))
            {
                ShowError("Название марки должно содержать только латинские буквы, цифры и пробелы.");
                btnSave.Enabled = false;
                return;
            }
        }

        private void txtBrand_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isDeleteMode)
                {
                    _viewModel.DeleteBrand(_brandId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                if (_isViewOnly)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                _brand.CarBrandName = txtBrand.Text.Trim();

                if (_brandId == null)
                {
                    _viewModel.AddBrand(_brand);
                }
                else
                {
                    _viewModel.UpdateBrand(_brand);
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
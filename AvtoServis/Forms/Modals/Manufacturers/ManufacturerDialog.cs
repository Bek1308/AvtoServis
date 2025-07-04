using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System.Text.RegularExpressions;

namespace AvtoServis.Forms.Modals.Manufacturers
{
    public partial class ManufacturerDialog : Form
    {
        private readonly ManufacturersViewModel _viewModel;
        private readonly int? _manufacturerId;
        private readonly bool _isDeleteMode;
        private Manufacturer _manufacturer;
        private System.Windows.Forms.Timer _errorTimer;

        public ManufacturerDialog(ManufacturersViewModel viewModel, int? manufacturerId, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _manufacturerId = manufacturerId;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            panelError.Visible = false;
            if (!_isDeleteMode)
            {
                LoadManufacturer();
            }
            else
            {
                SetupDeleteModeUI();
            }
            SetToolTips();
        }

        private void SetToolTips()
        {
            if (!_isDeleteMode)
            {
                toolTip.SetToolTip(txtName, "Введите название производителя");
            }
            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление производителя" : "Сохранить данные производителя");
            toolTip.SetToolTip(btnCancel, "Закрыть без сохранения изменений");
        }

        private void LoadManufacturer()
        {
            try
            {
                _manufacturer = _manufacturerId == null ? new Manufacturer() : _viewModel.LoadManufacturer(_manufacturerId.Value);
                if (_manufacturer == null && _manufacturerId != null)
                {
                    ShowError("Производитель не найден.");
                    return;
                }

                if (_manufacturerId != null)
                {
                    txtName.Text = _manufacturer.Name;
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке производителя: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadManufacturer Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void SetupDeleteModeUI()
        {
            this.ClientSize = new Size(400, 142);
            this.Text = "Подтверждение удаления";

            lblName.Text = "Вы хотите удалить этого производителя?";
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            tableLayoutPanel.Controls.Add(lblName, 0, 0);
            tableLayoutPanel.SetColumnSpan(lblName, 2);
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

            lblError.Visible = false;
            panelError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Пожалуйста, введите название производителя.");
                return;
            }

            if (txtName.Text.Length > 100)
            {
                ShowError("Название производителя не должно превышать 100 символов.");
                return;
            }

            if (!Regex.IsMatch(txtName.Text, @"^[а-яА-Яa-zA-Z0-9\s]+$"))
            {
                ShowError("Название производителя должно содержать только буквы (русские или латинские), цифры и пробелы.");
                return;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInputs();
                if (lblError.Visible) return;

                if (_isDeleteMode)
                {
                    _viewModel.DeleteManufacturer(_manufacturerId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                _manufacturer.Name = txtName.Text.Trim();

                if (_manufacturerId == null)
                {
                    _viewModel.AddManufacturer(_manufacturer);
                }
                else
                {
                    _manufacturer.ManufacturerID = _manufacturerId.Value;
                    _viewModel.UpdateManufacturer(_manufacturer);
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

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && txtName.Text.Length > 100)
            {
                ShowError("Название производителя не должно превышать 100 символов.");
            }
            else if (!string.IsNullOrWhiteSpace(txtName.Text) && !Regex.IsMatch(txtName.Text, @"^[а-яА-Яa-zA-Z0-9\s]+$"))
            {
                ShowError("Название производителя должно содержать только буквы (русские или латинские), цифры и пробелы.");
            }
        }
    }
}
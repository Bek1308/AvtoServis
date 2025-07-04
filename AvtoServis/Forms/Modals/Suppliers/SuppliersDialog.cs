using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;

namespace AvtoServis.Forms.Controls
{
    public partial class SuppliersDialog : Form
    {
        private readonly SuppliersViewModel _viewModel;
        private readonly Supplier _supplier;
        private readonly bool _isEditMode;
        private readonly bool _isDeleteMode;
        private System.Windows.Forms.Timer _errorTimer;

        public SuppliersDialog(SuppliersViewModel viewModel, int? id = null, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _isDeleteMode = isDeleteMode;
            _supplier = id.HasValue ? _viewModel.GetSupplierById(id.Value) : new Supplier();
            _isEditMode = id.HasValue && !isDeleteMode;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            SetToolTips();
            LoadData();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Форма для добавления/редактирования поставщика");
            toolTip.SetToolTip(titleLabel, "Заголовок формы");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(lblName, "Введите название поставщика");
            toolTip.SetToolTip(txtName, "Название поставщика");
            toolTip.SetToolTip(lblContactPhone, "Введите контактный телефон");
            toolTip.SetToolTip(txtContactPhone, "Контактный телефон");
            toolTip.SetToolTip(lblEmail, "Введите электронную почту");
            toolTip.SetToolTip(txtEmail, "Электронная почта");
            toolTip.SetToolTip(lblAddress, "Введите адрес");
            toolTip.SetToolTip(txtAddress, "Адрес поставщика");
            toolTip.SetToolTip(btnCancel, _isDeleteMode ? "Отменить удаление" : "Отменить изменения");
            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление" : "Сохранить изменения");
            toolTip.SetToolTip(lblError, "Сообщение об ошибке");
        }

        private void LoadData()
        {
            txtName.Text = _supplier.Name;
            txtContactPhone.Text = _supplier.ContactPhone;
            txtEmail.Text = _supplier.Email;
            txtAddress.Text = _supplier.Address;
            Text = _isDeleteMode ? "Удалить поставщика" : (_isEditMode ? "Редактировать поставщика" : "Добавить поставщика");
            if (_isDeleteMode)
            {
                txtName.Enabled = false;
                txtContactPhone.Enabled = false;
                txtEmail.Enabled = false;
                txtAddress.Enabled = false;
                btnSave.Text = "Удалить";
                btnSave.BackColor = Color.FromArgb(220, 53, 69);
                btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 77, 77);
                btnCancel.Text = "Отменить удаление";
                btnCancel.BackColor = Color.FromArgb(40, 167, 69);
                btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 139, 34);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isDeleteMode)
                {
                    _viewModel.DeleteSupplier(_supplier.SupplierID);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    ShowError("Название обязательно для заполнения.");
                    return;
                }

                _supplier.Name = txtName.Text.Trim();
                _supplier.ContactPhone = txtContactPhone.Text.Trim();
                _supplier.Email = txtEmail.Text.Trim();
                _supplier.Address = txtAddress.Text.Trim();

                if (_isEditMode)
                {
                    _viewModel.UpdateSupplier(_supplier);
                }
                else
                {
                    _viewModel.AddSupplier(_supplier);
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
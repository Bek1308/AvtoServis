using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using Timer = System.Windows.Forms.Timer;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class SuppliersDialog : Form
    {
        private readonly SuppliersViewModel _viewModel;
        private readonly int? _supplierId;
        private readonly bool _isDeleteMode;
        private Supplier _supplier;
        private Label lblMessage;
        private System.Windows.Forms.Timer _errorTimer;

        public SuppliersDialog(SuppliersViewModel viewModel, int? supplierId, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _supplierId = supplierId;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            _errorTimer = new Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            btnSave.Enabled = true;
            if (!isDeleteMode)
            {
                LoadSupplier();
            }
            else
            {
                SetupDeleteModeUI();
            }
        }

        private void LoadSupplier()
        {
            try
            {
                _supplier = _supplierId == null ? new Supplier() : _viewModel.LoadSuppliers().Find(s => s.SupplierID == _supplierId);
                if (_supplier == null && _supplierId != null)
                {
                    ShowError("Поставщик не найден.");
                    return;
                }

                if (_supplierId != null)
                {
                    txtName.Text = _supplier.Name;
                    txtContactPhone.Text = _supplier.ContactPhone;
                    txtEmail.Text = _supplier.Email;
                    txtAddress.Text = _supplier.Address;
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке поставщика: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadSupplier Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void SetupDeleteModeUI()
        {
            this.ClientSize = new Size(434, 142);
            this.Text = "Подтверждение удаления";

            lblMessage = new Label
            {
                AutoSize = true,
                Text = "Вы хотите удалить этого поставщика?",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Name = "lblMessage",
                Anchor = AnchorStyles.Left,
                AccessibleName = "Сообщение об удалении",
                AccessibleDescription = "Подтверждение удаления поставщика"
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

            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Пожалуйста, введите название поставщика.");
                return;
            }

            if (txtName.Text.Length > 100)
            {
                ShowError("Название поставщика не должно превышать 100 символов.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtContactPhone.Text) && !Regex.IsMatch(txtContactPhone.Text, @"^\+?\d{10,15}$"))
            {
                ShowError("Контактный телефон должен содержать от 10 до 15 цифр и может начинаться с '+'.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ShowError("Пожалуйста, введите корректный адрес электронной почты.");
                return;
            }

            if (txtEmail.Text.Length > 255)
            {
                ShowError("Электронная почта не должна превышать 255 символов.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                ShowError("Пожалуйста, введите адрес поставщика.");
                return;
            }

            if (txtAddress.Text.Length > 200)
            {
                ShowError("Адрес не должен превышать 200 символов.");
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
                    _viewModel.DeleteSupplier(_supplierId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                _supplier.Name = txtName.Text.Trim();
                _supplier.ContactPhone = txtContactPhone.Text.Trim();
                _supplier.Email = txtEmail.Text.Trim();
                _supplier.Address = txtAddress.Text.Trim();

                if (_supplierId == null)
                {
                    _viewModel.AddSupplier(_supplier);
                }
                else
                {
                    _supplier.SupplierID = _supplierId.Value;
                    _viewModel.UpdateSupplier(_supplier);
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
                ShowError("Название поставщика не должно превышать 100 символов.");
            }
        }

        private void TxtContactPhone_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtContactPhone.Text) && !Regex.IsMatch(txtContactPhone.Text, @"^\+?\d{10,15}$"))
            {
                ShowError("Контактный телефон должен содержать от 10 до 15 цифр и может начинаться с '+'.");
            }
        }

        private void TxtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ShowError("Пожалуйста, введите корректный адрес электронной почты.");
            }
        }

        private void TxtAddress_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAddress.Text) && txtAddress.Text.Length > 200)
            {
                ShowError("Адрес не должен превышать 200 символов.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _errorTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
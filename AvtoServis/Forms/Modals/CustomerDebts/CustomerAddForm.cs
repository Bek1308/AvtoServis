using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class CustomerAddForm : Form
    {
        private readonly CustomerDebtsViewModel _viewModel;
        private readonly Customer _customer;

        public CustomerAddForm(CustomerDebtsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _customer = new Customer
            {
                RegistrationDate = DateTime.Now,
                IsActive = true
            };
            dtpRegistrationDate.Value = DateTime.Now;
            chkIsActive.Checked = true;
            lblError.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _customer.FullName = txtFullName.Text;
                _customer.Phone = txtPhone.Text;
                _customer.Email = txtEmail.Text;
                _customer.Address = txtAddress.Text;
                _customer.RegistrationDate = dtpRegistrationDate.Value;
                _customer.IsActive = chkIsActive.Checked;

                var errors = _viewModel.ValidateCustomer(_customer);
                if (errors.Any())
                {
                    lblError.Text = string.Join("\n", errors);
                    lblError.Visible = true;
                    return;
                }

                _viewModel.AddCustomer(_customer);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblError.Text = $"Ошибка при сохранении клиента: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
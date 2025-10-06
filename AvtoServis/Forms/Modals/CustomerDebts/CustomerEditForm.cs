using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class CustomerEditForm : Form
    {
        private readonly CustomerDebtsViewModel _viewModel;
        private readonly Customer _customer;

        public CustomerEditForm(CustomerDebtsViewModel viewModel, Customer customer)
        {
            InitializeComponent();
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));

            txtFullName.Text = _customer.FullName;
            txtPhone.Text = _customer.Phone;
            txtEmail.Text = _customer.Email;
            txtAddress.Text = _customer.Address;
            dtpRegistrationDate.Value = _customer.RegistrationDate;
            chkIsActive.Checked = _customer.IsActive;
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

                var errors = _viewModel.UpdateCustomer(_customer);
                if (errors.Any())
                {
                    lblError.Text = string.Join("\n", errors);
                    lblError.Visible = true;
                    return;
                }

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
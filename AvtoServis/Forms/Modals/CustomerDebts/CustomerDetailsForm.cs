using AvtoServis.Model.DTOs;
using System;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class CustomerDetailsForm : Form
    {
        private readonly CustomerDebtInfoDto _customer;

        public CustomerDetailsForm(CustomerDebtInfoDto customer)
        {
            InitializeComponent();
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));

            txtFullName.Text = _customer.FullName ?? "Не указано";
            txtPhone.Text = _customer.Phone ?? "Не указано";
            txtEmail.Text = _customer.Email ?? "Не указано";
            txtAddress.Text = _customer.Address ?? "Не указано";
            txtRegistrationDate.Text = _customer.RegistrationDate == DateTime.MinValue ? "Не указано" : _customer.RegistrationDate.ToString("yyyy-MM-dd");
            txtIsActive.Text = _customer.IsActive ? "Да" : "Нет";
            txtIsActive.ForeColor = _customer.IsActive ? System.Drawing.Color.FromArgb(40, 167, 69) : System.Drawing.Color.FromArgb(220, 53, 69);
            txtUmumiyQarz.Text = _customer.UmumiyQarz.ToString("C");
            txtUmumiyQarz.ForeColor = _customer.UmumiyQarz > 0 ? System.Drawing.Color.FromArgb(220, 53, 69) : System.Drawing.Color.FromArgb(40, 167, 69);

            foreach (var car in _customer.CarModels)
            {
                dataGridView.Rows.Add("Модель машины", car);
            }
            foreach (var debt in _customer.DebtDetails)
            {
                dataGridView.Rows.Add("Деталь долга", $"{debt.ItemName}: {debt.Amount:C}");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
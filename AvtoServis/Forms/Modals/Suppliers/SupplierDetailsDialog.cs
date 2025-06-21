using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class SupplierDetailsDialog : Form
    {
        private readonly SuppliersViewModel _viewModel;
        private readonly int _supplierId;
        private Supplier _supplier;

        public SupplierDetailsDialog(SuppliersViewModel viewModel, int supplierId)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _supplierId = supplierId;
            InitializeComponent();
            LoadSupplierDetails();
        }

        private void LoadSupplierDetails()
        {
            try
            {
                _supplier = _viewModel.LoadSuppliers().Find(s => s.SupplierID == _supplierId);
                if (_supplier == null)
                {
                    MessageBox.Show("Поставщик не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                lblSupplierIDValue.Text = _supplier.SupplierID.ToString();
                lblNameValue.Text = _supplier.Name;
                lblContactPhoneValue.Text = _supplier.ContactPhone ?? "Нет данных";
                lblEmailValue.Text = _supplier.Email ?? "Нет данных";
                lblAddressValue.Text = _supplier.Address ?? "Нет данных";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных поставщика: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadSupplierDetails Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                Close();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
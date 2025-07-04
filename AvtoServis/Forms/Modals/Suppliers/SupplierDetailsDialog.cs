using AvtoServis.Model.Entities;

namespace AvtoServis.Forms.Controls
{
    public partial class SupplierDetailsDialog : Form
    {
        private readonly Supplier _supplier;

        public SupplierDetailsDialog(Supplier supplier)
        {
            _supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));
            InitializeComponent();
            SetToolTips();
            LoadData();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Форма для просмотра деталей поставщика");
            toolTip.SetToolTip(titleLabel, "Заголовок формы");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(lblName, "Название поставщика");
            toolTip.SetToolTip(txtName, "Название поставщика");
            toolTip.SetToolTip(lblContactPhone, "Контактный телефон");
            toolTip.SetToolTip(txtContactPhone, "Контактный телефон");
            toolTip.SetToolTip(lblEmail, "Электронная почта");
            toolTip.SetToolTip(txtEmail, "Электронная почта");
            toolTip.SetToolTip(lblAddress, "Адрес поставщика");
            toolTip.SetToolTip(txtAddress, "Адрес поставщика");
            toolTip.SetToolTip(btnOk, "Закрыть форму");
        }

        private void LoadData()
        {
            txtName.Text = _supplier.Name;
            txtContactPhone.Text = _supplier.ContactPhone;
            txtEmail.Text = _supplier.Email;
            txtAddress.Text = _supplier.Address;
            Text = "Подробности о поставщике";
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
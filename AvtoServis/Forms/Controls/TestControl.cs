using System.Data;
using System.Data.SqlClient;

namespace AvtoServis.Forms.Controls
{
    public partial class TestControl : UserControl
    {
        private DataTable servicesTable;
        private readonly string connectionString = "Server=YourServer;Database=AutoServiceDB;Trusted_Connection=True;"; // O'zgartiring

        public TestControl()
        {
            InitializeComponent();
            LoadServices();
        }

        // Ma'lumotlarni yuklash
        private void LoadServices()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ServiceID, Name, Price FROM AutoServiceDB.dbo.Services";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        servicesTable = new DataTable();
                        adapter.Fill(servicesTable);
                        dataGridViewMain.DataSource = servicesTable;

                        // Ustun nomlarini rus tiliga o'zgartirish
                        dataGridViewMain.Columns["ServiceID"].HeaderText = "ID";
                        dataGridViewMain.Columns["Name"].HeaderText = "Название";
                        dataGridViewMain.Columns["Price"].HeaderText = "Цена";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Filtrlarni qo‘llash
        private void ApplyFilters()
        {
            if (servicesTable == null) return;

            string filter = "1=1"; // Default shart
            if (!string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                filter += $" AND Name LIKE '%{textBoxSearch.Text}%'";
            }

            if (comboBoxFilter.SelectedIndex > 0)
            {
                string status = comboBoxFilter.SelectedItem.ToString();
                if (status == "Активные")
                    filter += " AND Price > 0"; // Misol uchun faol xizmatlar
                else if (status == "Архив")
                    filter += " AND Price = 0"; // Misol uchun arxiv xizmatlar
            }

            if (numericUpDownMinPrice.Value > 0)
                filter += $" AND Price >= {numericUpDownMinPrice.Value}";
            if (numericUpDownMaxPrice.Value > 0)
                filter += $" AND Price <= {numericUpDownMaxPrice.Value}";

            try
            {
                servicesTable.DefaultView.RowFilter = filter;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ComboBox tanlanganda filtr panelini ko‘rsatish/yashirish
        private void ComboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelFilter.Visible = comboBoxFilter.SelectedIndex > 0; // "Все"dan boshqa hollarda ko‘rsatiladi
            ApplyFilters();
        }

        // Filtr qo‘llash tugmasi
        private void BtnApplyFilter_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // Qidiruv maydonida Enter tugmasi
        private void TextBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ApplyFilters();
                e.Handled = true;
            }
        }

        // Resize hodisasi
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (panelSearch != null && textBoxSearch != null)
            {
                textBoxSearch.Width = panelSearch.Width - 20;
            }
        }

        // Новый tugmasi
        private void BtnIncome_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Добавление новой услуги", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // TODO: Yangi xizmat qo'shish logikasini qo'shing
        }
    }
}
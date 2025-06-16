using System;
using System.Drawing;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class FilterDialog : Form
    {
        public decimal? MinPrice { get; private set; }
        public decimal? MaxPrice { get; private set; }
        public bool HighPrice { get; private set; }

        public FilterDialog()
        {
            InitializeComponent();
            AddToolTips();
        }

        private void AddToolTips()
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(txtPriceMin, "Введите минимальную цену для фильтрации");
            toolTip.SetToolTip(txtPriceMax, "Введите максимальную цену для фильтрации");
            toolTip.SetToolTip(chkHighPrice, "Показать только услуги с ценой выше 1000");
            toolTip.SetToolTip(btnApply, "Применить выбранные фильтры");
            toolTip.SetToolTip(btnCancel, "Закрыть без применения фильтров");
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtPriceMin.Text))
                {
                    if (decimal.TryParse(txtPriceMin.Text, out decimal priceMin))
                        MinPrice = priceMin;
                    else
                    {
                        MessageBox.Show("Введите корректное значение для минимальной цены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtPriceMax.Text))
                {
                    if (decimal.TryParse(txtPriceMax.Text, out decimal priceMax))
                        MaxPrice = priceMax;
                    else
                    {
                        MessageBox.Show("Введите корректное значение для максимальной цены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (MinPrice.HasValue && MaxPrice.HasValue && MaxPrice < MinPrice)
                {
                    MessageBox.Show("Максимальная цена должна быть больше или равна минимальной.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                HighPrice = chkHighPrice.Checked;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка применения фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPriceMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
        }

        private void TxtPriceMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
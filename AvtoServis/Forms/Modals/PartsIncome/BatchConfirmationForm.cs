using System;
using System.Drawing;
using System.Windows.Forms;

namespace AvtoServis.Forms.Modals.PartsIncome
{
    public partial class BatchConfirmationForm : Form
    {
        private readonly decimal _totalCost;
        private readonly decimal _totalPaidSum;
        public string BatchName { get; private set; }
        public decimal TotalPaidAmount { get; private set; }

        public BatchConfirmationForm(decimal totalCost, decimal totalPaidSum)
        {
            _totalCost = totalCost;
            _totalPaidSum = totalPaidSum;
            InitializeComponent();
            InitializeTimer();
            UpdateLabels();
            ShowInitialMessage();
        }

        private void InitializeTimer()
        {
            errorTimer.Tick += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtBatchName.Text) &&
                    decimal.TryParse(txtTotalPaidAmount.Text, out decimal totalPaidAmount) &&
                    totalPaidAmount >= 0 && totalPaidAmount <= _totalCost &&
                    totalPaidAmount >= _totalPaidSum)
                {
                    ShowSuccess("Все данные введены корректно! Нажмите 'Продолжить'.");
                }
                else
                {
                    ShowInitialMessage();
                }
                errorTimer.Stop();
            };
        }

        private void UpdateLabels()
        {
            lblTotalCost.Text = $"Общая стоимость: {_totalCost}";
            lblTotalPaidSum.Text = $"Общая оплаченная сумма деталей: {_totalPaidSum}";
        }

        private void ShowInitialMessage()
        {
            lblError.Text = "Пожалуйста, введите название партии";
            lblError.ForeColor = Color.FromArgb(40, 167, 69);
            lblError.BackColor = Color.Transparent;
            panelError.BackColor = Color.FromArgb(245, 255, 245);
            panelError.Visible = true;
            lblError.Visible = true;
            panelError.BringToFront();
            AdjustFormHeight();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.BackColor = Color.Transparent;
            panelError.BackColor = Color.FromArgb(255, 245, 245);
            panelError.Visible = true;
            lblError.Visible = true;
            panelError.BringToFront();
            errorTimer.Stop();
            AdjustFormHeight();
        }

        private void ShowSuccess(string message)
        {
            lblError.Text = message;
            lblError.ForeColor = Color.FromArgb(40, 167, 69);
            lblError.BackColor = Color.Transparent;
            panelError.BackColor = Color.FromArgb(245, 255, 245);
            panelError.Visible = true;
            lblError.Visible = true;
            panelError.BringToFront();
            errorTimer.Start();
            AdjustFormHeight();
        }

        private void AdjustFormHeight()
        {
            lblError.AutoSize = true;
            lblError.MaximumSize = new Size(376, 0);
            int textHeight = TextRenderer.MeasureText(lblError.Text, lblError.Font, new Size(376, 0), TextFormatFlags.WordBreak).Height;
            panelError.Height = textHeight + panelError.Padding.Top + panelError.Padding.Bottom;
            tableLayoutPanel.RowStyles[4].Height = panelError.Height;
            int newFormHeight = (int)(tableLayoutPanel.RowStyles[0].Height +
                                     tableLayoutPanel.RowStyles[1].Height +
                                     tableLayoutPanel.RowStyles[2].Height +
                                     tableLayoutPanel.RowStyles[3].Height +
                                     panelError.Height +
                                     tableLayoutPanel.RowStyles[5].Height +
                                     tableLayoutPanel.Padding.Top +
                                     tableLayoutPanel.Padding.Bottom);
            ClientSize = new Size(ClientSize.Width, newFormHeight);
        }

        private void ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtBatchName.Text))
            {
                ShowError("Введите название партии!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTotalPaidAmount.Text))
            {
                ShowError("Введите общую оплаченную сумму!");
                return;
            }

            if (!decimal.TryParse(txtTotalPaidAmount.Text, out decimal totalPaidAmount) || totalPaidAmount < 0)
            {
                ShowError("Введите корректную общую оплаченную сумму (положительное число)!");
                return;
            }

            if (totalPaidAmount > _totalCost)
            {
                ShowError("Общая оплаченная сумма не должна превышать общей стоимости деталей!");
                return;
            }

            if (totalPaidAmount < _totalPaidSum)
            {
                ShowError("Общая оплаченная сумма не должна быть меньше оплаченной суммы деталей!");
                return;
            }

            ShowSuccess("Все данные введены корректно! Нажмите 'Продолжить'.");
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBatchName.Text))
            {
                ShowError("Введите название партии!");
                return;
            }

            if (!decimal.TryParse(txtTotalPaidAmount.Text, out decimal totalPaidAmount) || totalPaidAmount < 0)
            {
                ShowError("Введите корректную общую оплаченную сумму (положительное число)!");
                return;
            }

            if (totalPaidAmount > _totalCost)
            {
                ShowError("Общая оплаченная сумма не должна превышать общей стоимости деталей!");
                return;
            }

            if (totalPaidAmount < _totalPaidSum)
            {
                ShowError("Общая оплаченная сумма не должна быть меньше оплаченной суммы деталей!");
                return;
            }

            BatchName = txtBatchName.Text;
            TotalPaidAmount = totalPaidAmount;
            DialogResult = DialogResult.OK;
        }
    }
}
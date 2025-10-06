using AvtoServis.ViewModels.Screens;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AvtoServis.Forms.Modals.PartsIncome
{
    public partial class PartsIncomeDeleteDialog : Form
    {
        private readonly PartsIncomeViewModel _viewModel;
        private readonly int _incomeId;
        private System.Windows.Forms.Timer _errorTimer;

        public string SelectedReason { get; private set; }

        public PartsIncomeDeleteDialog(PartsIncomeViewModel viewModel, int incomeId)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _incomeId = incomeId <= 0 ? throw new ArgumentException("IncomeID должен быть действительным положительным числом.") : incomeId;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            panelError.Visible = false;
            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(lblTitle, "Заголовок подтверждения удаления");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(cmbReason, "Выберите причину удаления");
            toolTip.SetToolTip(btnConfirm, "Подтвердить удаление поступления");
            toolTip.SetToolTip(btnCancel, "Закрыть без сохранения изменений");
        }

        private void ValidateInputs()
        {
            lblError.Visible = false;
            panelError.Visible = false;

            if (cmbReason.SelectedIndex == -1)
            {
                ShowError("Пожалуйста, выберите причину удаления.");
            }
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInputs();
                if (lblError.Visible) return;

                SelectedReason = cmbReason.SelectedItem.ToString();
                _viewModel.DeletePartsIncome(_incomeId, SelectedReason);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при удалении: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"BtnConfirm_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
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

            // Add error panel to TableLayoutPanel dynamically
            if (!tableLayoutPanel.Controls.Contains(panelError))
            {
                tableLayoutPanel.RowCount = 6;
                tableLayoutPanel.RowStyles.Insert(4, new RowStyle(SizeType.Absolute, 64F));
                tableLayoutPanel.Controls.Add(panelError, 0, 4);
                tableLayoutPanel.SetColumnSpan(panelError, 2);
                ClientSize = new Size(ClientSize.Width, ClientSize.Height + 64); // Increase form height
            }

            _errorTimer.Start();
        }

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            panelError.Visible = false;

            // Remove error panel and adjust form size
            if (tableLayoutPanel.Controls.Contains(panelError))
            {
                tableLayoutPanel.Controls.Remove(panelError);
                tableLayoutPanel.RowStyles.RemoveAt(4);
                tableLayoutPanel.RowCount = 5;
                ClientSize = new Size(ClientSize.Width, ClientSize.Height - 64); // Decrease form height
            }

            _errorTimer.Stop();
        }
    }
}
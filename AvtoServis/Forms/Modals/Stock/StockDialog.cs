using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using Timer = System.Windows.Forms.Timer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class StockDialog : Form
    {
        private readonly StockViewModel _viewModel;
        private readonly int? _stockId;
        private readonly bool _isDeleteMode;
        private Stock _stock;
        private Label lblMessage;
        private System.Windows.Forms.Timer _errorTimer;

        public StockDialog(StockViewModel viewModel, int? stockId, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _stockId = stockId;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            _errorTimer = new Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            btnSave.Enabled = true;
            if (!isDeleteMode)
            {
                LoadStock();
            }
            else
            {
                SetupDeleteModeUI();
            }
        }

        private void LoadStock()
        {
            try
            {
                _stock = _stockId == null ? new Stock() : _viewModel.LoadStock().Find(s => s.StockID == _stockId);
                if (_stock == null && _stockId != null)
                {
                    ShowError("Склад не найден.");
                    return;
                }

                if (_stockId != null)
                {
                    txtName.Text = _stock.Name;
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке склада: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadStock Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void SetupDeleteModeUI()
        {
            this.ClientSize = new Size(434, 142);
            this.Text = "Подтверждение удаления";

            lblMessage = new Label
            {
                AutoSize = true,
                Text = "Вы хотите удалить этот склад?",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Name = "lblMessage",
                Anchor = AnchorStyles.Left,
                AccessibleName = "Сообщение об удалении",
                AccessibleDescription = "Подтверждение удаления склада"
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
                ShowError("Пожалуйста, введите название склада.");
                return;
            }

            if (txtName.Text.Length > 100)
            {
                ShowError("Название склада не должно превышать 100 символов.");
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
                    _viewModel.DeleteStock(_stockId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                _stock.Name = txtName.Text.Trim();

                if (_stockId == null)
                {
                    _viewModel.AddStock(_stock);
                }
                else
                {
                    _stock.StockID = _stockId.Value;
                    _viewModel.UpdateStock(_stock);
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
                ShowError("Название склада не должно превышать 100 символов.");
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
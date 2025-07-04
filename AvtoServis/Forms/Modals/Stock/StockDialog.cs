using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class StockDialog : Form
    {
        private readonly StockViewModel _viewModel;
        private readonly int? _stockId;
        private readonly bool _isDeleteMode;
        private readonly bool _isViewOnly;
        private Stock _stock;
        private System.Windows.Forms.Timer _errorTimer;

        public StockDialog(StockViewModel viewModel, int? stockId, bool isViewOnly = false, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _stockId = stockId;
            _isDeleteMode = isDeleteMode;
            _isViewOnly = isViewOnly;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            SetToolTips();
            if (!_isDeleteMode)
            {
                LoadStock();
            }
            else
            {
                SetupDeleteModeUI();
            }
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Форма для добавления или редактирования склада");
            toolTip.SetToolTip(lblStockName, "Метка для ввода названия склада");
            toolTip.SetToolTip(txtStockName, _isDeleteMode ? "Название склада" : "Введите название склада (латинские буквы, цифры, пробел, максимум 100 символов)");
            toolTip.SetToolTip(lblError, "Отображает ошибки или сообщения об операции");
            toolTip.SetToolTip(btnCancel, _isDeleteMode ? "Отменить удаление" : "Закрыть без сохранения");
            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление" : "Сохранить изменения");
            if (lblMessage != null)
                toolTip.SetToolTip(lblMessage, "Подтверждение удаления склада");
        }

        private void LoadStock()
        {
            try
            {
                _stock = _stockId == null ? new Stock() : _viewModel.LoadStocks().Find(s => s.StockID == _stockId);
                if (_stock == null && _stockId != null)
                {
                    ShowError("Склад не найден.");
                    btnSave.Enabled = false;
                    return;
                }

                txtStockName.Text = _stock?.Name;

                if (_isViewOnly)
                {
                    txtStockName.Enabled = false;
                    btnSave.Visible = false;
                    btnCancel.Text = "Закрыть";
                    Text = "Просмотр склада";
                }

                ValidateInputs();
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
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 77, 77);
        }

        private void ValidateInputs()
        {
            if (_isDeleteMode || _isViewOnly) return;

            lblError.Visible = false;
            btnSave.Enabled = true;

            if (string.IsNullOrWhiteSpace(txtStockName.Text))
            {
                ShowError("Пожалуйста, введите название склада.");
                btnSave.Enabled = false;
                return;
            }

            if (txtStockName.Text.Length > 100)
            {
                ShowError("Название склада не должно превышать 100 символов.");
                btnSave.Enabled = false;
                return;
            }

            if (!Regex.IsMatch(txtStockName.Text, @"^[a-zA-Z0-9\s]+$"))
            {
                ShowError("Название склада должно содержать только латинские буквы, цифры и пробелы.");
                btnSave.Enabled = false;
                return;
            }
        }

        private void txtStockName_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isDeleteMode)
                {
                    _viewModel.DeleteStock(_stockId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                if (_isViewOnly)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                _stock.Name = txtStockName.Text.Trim();

                if (_stockId == null)
                {
                    _viewModel.AddStock(_stock);
                }
                else
                {
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

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            _errorTimer.Stop();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            _errorTimer.Stop();
            _errorTimer.Start();
        }
    }
}
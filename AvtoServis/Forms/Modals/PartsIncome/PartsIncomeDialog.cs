using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AvtoServis.Data.Configuration;
using AvtoServis.Data.Repositories;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;

namespace AvtoServis.Forms.Controls
{
    public partial class PartsIncomeDialog : Form
    {
        private readonly PartsIncomeViewModel _viewModel;
        private readonly int? _incomeId;
        private PartsIncome _partsIncome;
        private readonly string _connectionString = DatabaseConfig.ConnectionString;

        public PartsIncomeDialog(PartsIncomeViewModel viewModel, int? incomeId = null)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _incomeId = incomeId;
            InitializeComponent();
            InitializeTimer();
            InitializeComboBoxes();
            ShowInitialMessage();
            LoadData();
        }

        private void InitializeTimer()
        {
            errorTimer.Tick += (s, e) =>
            {
                _messagePanel.Visible = true; // Xabar paneli doimiy ko'rinib turadi
                _messageLabel.Visible = true;
                errorTimer.Stop();
                AdjustFormHeight();
            };
        }

        private void InitializeComboBoxes()
        {
            try
            {
                var partsRepository = new PartsRepository(_connectionString);
                var parts = partsRepository.GetAll();
                cmbPartID.DataSource = parts;
                cmbPartID.DisplayMember = "PartName";
                cmbPartID.ValueMember = "PartID";
                cmbPartID.SelectedIndex = -1;

                var suppliersRepository = new SuppliersRepository(_connectionString);
                var suppliers = suppliersRepository.GetAll();
                cmbSupplierID.DataSource = suppliers;
                cmbSupplierID.DisplayMember = "Name";
                cmbSupplierID.ValueMember = "SupplierID";
                cmbSupplierID.SelectedIndex = -1;

                var statusesRepository = new StatusRepository(_connectionString);
                var statuses = statusesRepository.GetAll("IncomeStatuses");
                cmbStatusID.DataSource = statuses;
                cmbStatusID.DisplayMember = "Name";
                cmbStatusID.ValueMember = "StatusID";
                cmbStatusID.SelectedIndex = -1;

                var stockRepository = new StockRepository(_connectionString);
                var stocks = stockRepository.GetAll();
                cmbStockID.DataSource = stocks;
                cmbStockID.DisplayMember = "Name";
                cmbStockID.ValueMember = "StockID";
                cmbStockID.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке данных для выпадающих списков: {ex.Message}");
            }
        }

        private void ShowInitialMessage()
        {
            _messageLabel.Text = "Редактирование поступления: измените необходимые поля и нажмите 'Сохранить'";
            _messageLabel.ForeColor = Color.FromArgb(40, 167, 69);
            _messageLabel.BackColor = Color.Transparent;
            _messagePanel.BackColor = Color.FromArgb(245, 255, 245);
            _messagePanel.Visible = true;
            _messageLabel.Visible = true;
            _messagePanel.BringToFront();
            AdjustFormHeight();
        }

        private void ShowError(string message)
        {
            _messageLabel.Text = message;
            _messageLabel.ForeColor = Color.FromArgb(220, 53, 69);
            _messageLabel.BackColor = Color.Transparent;
            _messagePanel.BackColor = Color.FromArgb(255, 245, 245);
            _messagePanel.Visible = true;
            _messageLabel.Visible = true;
            _messagePanel.BringToFront();
            AdjustFormHeight();
        }

        private void ShowSuccess(string message)
        {
            _messageLabel.Text = message;
            _messageLabel.ForeColor = Color.FromArgb(40, 167, 69);
            _messageLabel.BackColor = Color.Transparent;
            _messagePanel.BackColor = Color.FromArgb(245, 255, 245);
            _messagePanel.Visible = true;
            _messageLabel.Visible = true;
            _messagePanel.BringToFront();
            AdjustFormHeight();
        }

        private void AdjustFormHeight()
        {
            _messageLabel.AutoSize = true;
            _messageLabel.MaximumSize = new Size(460, 0);
            int textHeight = TextRenderer.MeasureText(_messageLabel.Text, _messageLabel.Font, new Size(460, 0), TextFormatFlags.WordBreak).Height;
            _messagePanel.Height = textHeight + _messagePanel.Padding.Top + _messagePanel.Padding.Bottom;
            tableLayoutPanel.RowStyles[11].Height = _messagePanel.Height;

            // Forma balandligini qatorlar va paddinglarga qarab hisoblash
            int newFormHeight = 0;
            foreach (RowStyle row in tableLayoutPanel.RowStyles)
            {
                newFormHeight += (int)row.Height;
            }
            newFormHeight += tableLayoutPanel.Padding.Top + tableLayoutPanel.Padding.Bottom + 20; // Qo'shimcha bo'shliq

            // Forma va tableLayoutPanel balandligini o'zgartirish
            ClientSize = new Size(ClientSize.Width, newFormHeight);
            tableLayoutPanel.Size = new Size(tableLayoutPanel.Width, newFormHeight);
        }

        private void LoadData()
        {
            if (_incomeId.HasValue)
            {
                try
                {
                    var partsIncomes = _viewModel.LoadPartsIncomes();
                    _partsIncome = partsIncomes.FirstOrDefault(p => p.IncomeID == _incomeId);
                    if (_partsIncome == null)
                    {
                        ShowError("Поступление не найдено!");
                        return;
                    }

                    txtBatchName.Text = _partsIncome.BatchName;
                    cmbPartID.SelectedValue = _partsIncome.PartID;
                    cmbSupplierID.SelectedValue = _partsIncome.SupplierID;
                    dtpDate.Value = _partsIncome.Date;
                    txtQuantity.Text = _partsIncome.Quantity.ToString();
                    txtUnitPrice.Text = _partsIncome.UnitPrice.ToString();
                    txtMarkup.Text = _partsIncome.Markup.ToString();
                    cmbStatusID.SelectedValue = _partsIncome.StatusID;
                    cmbStockID.SelectedValue = _partsIncome.StockID;
                    txtInvoiceNumber.Text = _partsIncome.InvoiceNumber;
                    txtPaidAmount.Text = _partsIncome.PaidAmount.ToString();
                }
                catch (Exception ex)
                {
                    ShowError($"Ошибка при загрузке данных: {ex.Message}");
                }
            }
            else
            {
                _partsIncome = new PartsIncome();
            }
            ValidateInputs();
        }

        private void ValidateInputs()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(txtBatchName.Text))
                errors.Add("Введите название партии!");
            if (cmbPartID.SelectedValue == null)
                errors.Add("Выберите деталь!");
            if (cmbSupplierID.SelectedValue == null)
                errors.Add("Выберите поставщика!");
            if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                errors.Add("Введите корректное количество (положительное число)!");
            if (string.IsNullOrWhiteSpace(txtUnitPrice.Text) || !decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) || unitPrice <= 0)
                errors.Add("Введите корректную цену за единицу (положительное число)!");
            if (string.IsNullOrWhiteSpace(txtMarkup.Text) || !decimal.TryParse(txtMarkup.Text, out decimal markup) || markup < 0)
                errors.Add("Введите корректную наценку (неотрицательное число)!");
            if (cmbStatusID.SelectedValue == null)
                errors.Add("Выберите статус!");
            if (cmbStockID.SelectedValue == null)
                errors.Add("Выберите склад!");
            if (string.IsNullOrWhiteSpace(txtPaidAmount.Text) || !decimal.TryParse(txtPaidAmount.Text, out decimal paidAmount) || paidAmount < 0)
                errors.Add("Введите корректную оплаченную сумму (неотрицательное число)!");

            // Replace the selected code block with the following:
            if (int.TryParse(txtQuantity.Text, out quantity) && decimal.TryParse(txtUnitPrice.Text, out unitPrice) && decimal.TryParse(txtPaidAmount.Text, out paidAmount))
            {
                decimal expectedPaidAmount = quantity * unitPrice;
                if (paidAmount > expectedPaidAmount + 0.01m)
                    errors.Add("Оплаченная сумма не должна превышать количество * цену за единицу!");
            }

            if (errors.Count > 0)
            {
                ShowError(string.Join("\n", errors));
                return;
            }

            ShowSuccess("Все данные введены корректно! Нажмите 'Сохранить'.");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var errors = new List<string>();
            int quantity = 0;
            decimal unitPrice = 0;
            decimal markup = 0;
            decimal paidAmount = 0;

            if (string.IsNullOrWhiteSpace(txtBatchName.Text))
                errors.Add("Введите название партии!");
            if (cmbPartID.SelectedValue == null)
                errors.Add("Выберите деталь!");
            if (cmbSupplierID.SelectedValue == null)
                errors.Add("Выберите поставщика!");
            if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
                errors.Add("Введите корректное количество (положительное число)!");
            if (string.IsNullOrWhiteSpace(txtUnitPrice.Text) || !decimal.TryParse(txtUnitPrice.Text, out unitPrice) || unitPrice <= 0)
                errors.Add("Введите корректную цену за единицу (положительное число)!");
            if (string.IsNullOrWhiteSpace(txtMarkup.Text) || !decimal.TryParse(txtMarkup.Text, out markup) || markup < 0)
                errors.Add("Введите корректную наценку (неотрицательное число)!");
            if (cmbStatusID.SelectedValue == null)
                errors.Add("Выберите статус!");
            if (cmbStockID.SelectedValue == null)
                errors.Add("Выберите склад!");
            if (string.IsNullOrWhiteSpace(txtPaidAmount.Text) || !decimal.TryParse(txtPaidAmount.Text, out paidAmount) || paidAmount < 0)
                errors.Add("Введите корректную оплаченную сумму (неотрицательное число)!");

            if (int.TryParse(txtQuantity.Text, out quantity) && decimal.TryParse(txtUnitPrice.Text, out unitPrice) && decimal.TryParse(txtPaidAmount.Text, out paidAmount))
            {
                decimal expectedPaidAmount = quantity * unitPrice;
                if (paidAmount > expectedPaidAmount + 0.01m)
                    errors.Add("Оплаченная сумма не должна превышать количество * цену за единицу!");
            }

            if (errors.Count > 0)
            {
                ShowError(string.Join("\n", errors));
                return;
            }

            try
            {
                _partsIncome.BatchName = txtBatchName.Text;
                _partsIncome.PartID = (int)cmbPartID.SelectedValue;
                _partsIncome.SupplierID = (int)cmbSupplierID.SelectedValue;
                _partsIncome.Date = dtpDate.Value;
                _partsIncome.Quantity = quantity;
                _partsIncome.UnitPrice = unitPrice;
                _partsIncome.Markup = markup;
                _partsIncome.StatusID = (int)cmbStatusID.SelectedValue;
                _partsIncome.StockID = (int)cmbStockID.SelectedValue;
                _partsIncome.InvoiceNumber = string.IsNullOrWhiteSpace(txtInvoiceNumber.Text) ? null : txtInvoiceNumber.Text;
                _partsIncome.PaidAmount = paidAmount;

                if (_incomeId.HasValue)
                {
                    _viewModel.UpdatePartsIncome(_partsIncome, txtBatchName.Text);
                    ShowSuccess("Поступление успешно обновлено!");
                }
                else
                {
                    _viewModel.SavePartsIncomes(new List<PartsIncome> { _partsIncome }, txtBatchName.Text, _partsIncome.PaidAmount);
                    ShowSuccess("Поступление успешно сохранено!");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtBatchName_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void cmbPartID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void cmbSupplierID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void txtMarkup_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void cmbStatusID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void cmbStockID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }
    }
}
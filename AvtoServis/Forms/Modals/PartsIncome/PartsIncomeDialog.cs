//using AvtoServis.Model.Entities;
//using AvtoServis.ViewModels.Screens;
//using System;
//using System.Drawing;
//using System.Windows.Forms;

//namespace AvtoServis.Forms.Controls
//{
//    public partial class PartsIncomeDialog : Form
//    {
//        private readonly PartsIncomeViewModel _viewModel;
//        private readonly int? _incomeId;
//        private readonly bool _isDeleteMode;
//        private PartsIncome _income;
//        private System.Windows.Forms.Timer _errorTimer;

//        public PartsIncomeDialog(PartsIncomeViewModel viewModel, int? incomeId, bool isDeleteMode = false)
//        {
//            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
//            _incomeId = incomeId;
//            _isDeleteMode = isDeleteMode;
//            InitializeComponent();
//            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
//            _errorTimer.Tick += ErrorTimer_Tick;
//            lblError.Visible = false;
//            if (!_isDeleteMode)
//            {
//                LoadIncome();
//            }
//            else
//            {
//                SetupDeleteModeUI();
//            }
//            SetToolTips();
//        }

//        private void SetToolTips()
//        {
//            toolTip.SetToolTip(tableLayoutPanel, "Форма для добавления или редактирования поступления деталей");
//            toolTip.SetToolTip(lblPart, "Выберите деталь");
//            toolTip.SetToolTip(cmbPart, "Список доступных деталей");
//            toolTip.SetToolTip(lblSupplier, "Выберите поставщика");
//            toolTip.SetToolTip(cmbSupplier, "Список доступных поставщиков");
//            toolTip.SetToolTip(lblDate, "Введите дату поступления");
//            toolTip.SetToolTip(dtpDate, "Дата поступления");
//            toolTip.SetToolTip(lblQuantity, "Введите количество");
//            toolTip.SetToolTip(txtQuantity, "Количество поступивших деталей");
//            toolTip.SetToolTip(lblUnitPrice, "Введите цену за единицу");
//            toolTip.SetToolTip(txtUnitPrice, "Цена за единицу детали");
//            toolTip.SetToolTip(lblMarkup, "Введите наценку");
//            toolTip.SetToolTip(txtMarkup, "Наценка на деталь");
//            toolTip.SetToolTip(lblStatus, "Выберите статус");
//            toolTip.SetToolTip(cmbStatus, "Список доступных статусов");
//            toolTip.SetToolTip(lblOperation, "Выберите операцию");
//            toolTip.SetToolTip(cmbOperation, "Список доступных операций");
//            toolTip.SetToolTip(lblStock, "Выберите склад");
//            toolTip.SetToolTip(cmbStock, "Список доступных складов");
//            toolTip.SetToolTip(lblInvoiceNumber, "Введите номер счета");
//            toolTip.SetToolTip(txtInvoiceNumber, "Номер счета для поступления");
//            toolTip.SetToolTip(lblUser, "Выберите пользователя");
//            toolTip.SetToolTip(cmbUser, "Список доступных пользователей");
//            toolTip.SetToolTip(lblPaidAmount, "Введите оплаченную сумму");
//            toolTip.SetToolTip(txtPaidAmount, "Оплаченная сумма за поступление");
//            toolTip.SetToolTip(lblBatch, "Введите ID партии");
//            toolTip.SetToolTip(txtBatch, "ID партии поступления");
//            toolTip.SetToolTip(btnCancel, "Отменить изменения");
//            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление поступления" : "Сохранить изменения");
//            toolTip.SetToolTip(lblError, "Сообщение об ошибке");
//        }

//        private void LoadIncome()
//        {
//            try
//            {
//                _income = _incomeId == null ? new PartsIncome() : _viewModel.LoadPartsIncome().Find(p => p.IncomeID == _incomeId);
//                if (_income == null && _incomeId != null)
//                {
//                    ShowError("Поступление не найдено.");
//                    return;
//                }

//                var parts = _viewModel.LoadParts();
//                cmbPart.DataSource = parts;
//                cmbPart.DisplayMember = "PartName";
//                cmbPart.ValueMember = "PartID";

//                var suppliers = _viewModel.LoadSuppliers();
//                cmbSupplier.DataSource = suppliers;
//                cmbSupplier.DisplayMember = "Name";
//                cmbSupplier.ValueMember = "SupplierID";

//                var statuses = _viewModel.LoadStatuses();
//                cmbStatus.DataSource = statuses;
//                cmbStatus.DisplayMember = "Name";
//                cmbStatus.ValueMember = "StatusID";

//                var operations = _viewModel.LoadOperations();
//                cmbOperation.DataSource = operations;
//                cmbOperation.DisplayMember = "Name";
//                cmbOperation.ValueMember = "OperationID";

//                var stocks = _viewModel.LoadStocks();
//                cmbStock.DataSource = stocks;
//                cmbStock.DisplayMember = "Name";
//                cmbStock.ValueMember = "StockID";

//                var users = _viewModel.LoadUsers();
//                cmbUser.DataSource = users;
//                cmbUser.DisplayMember = "Name";
//                cmbUser.ValueMember = "UserID";

//                if (_incomeId != null)
//                {
//                    cmbPart.SelectedValue = _income.PartID;
//                    cmbSupplier.SelectedValue = _income.SupplierID;
//                    dtpDate.Value = _income.Date;
//                    txtQuantity.Text = _income.Quantity.ToString();
//                    txtUnitPrice.Text = _income.UnitPrice.ToString();
//                    txtMarkup.Text = _income.Markup.ToString();
//                    cmbStatus.SelectedValue = _income.StatusID;
//                    cmbOperation.SelectedValue = _income.OperationID;
//                    cmbStock.SelectedValue = _income.StockID;
//                    txtInvoiceNumber.Text = _income.InvoiceNumber;
//                    cmbUser.SelectedValue = _income.UserID;
//                    txtPaidAmount.Text = _income.PaidAmount.ToString();
//                    txtBatch.Text = _income.BatchID.ToString();
//                }
//            }
//            catch (Exception ex)
//            {
//                ShowError($"Ошибка при загрузке поступления: {ex.Message}");
//                System.Diagnostics.Debug.WriteLine($"LoadIncome Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
//            }
//        }

//        private void SetupDeleteModeUI()
//        {
//            this.ClientSize = new Size(434, 142);
//            this.Text = "Подтверждение удаления";

//            var lblMessage = new Label
//            {
//                AutoSize = true,
//                Text = "Вы хотите удалить это поступление?",
//                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
//                ForeColor = Color.FromArgb(33, 37, 41),
//                Name = "lblMessage",
//                Anchor = AnchorStyles.Left,
//                AccessibleName = "Сообщение об удалении",
//                AccessibleDescription = "Подтверждение удаления поступления"
//            };

//            tableLayoutPanel.Controls.Clear();
//            tableLayoutPanel.RowCount = 2;
//            tableLayoutPanel.RowStyles.Clear();
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

//            tableLayoutPanel.Controls.Add(lblMessage, 0, 0);
//            tableLayoutPanel.SetColumnSpan(lblMessage, 2);
//            tableLayoutPanel.Controls.Add(btnCancel, 0, 1);
//            tableLayoutPanel.Controls.Add(btnSave, 1, 1);

//            btnCancel.Text = "Нет";
//            btnSave.Text = "Да";
//            btnCancel.BackColor = Color.FromArgb(25, 118, 210);
//            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
//            btnSave.BackColor = Color.FromArgb(220, 53, 69);
//            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 100, 100);
//        }

//        private void BtnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (_isDeleteMode)
//                {
//                    _viewModel.DeletePartsIncome(_incomeId.Value);
//                    DialogResult = DialogResult.OK;
//                    Close();
//                    return;
//                }

//                if (!ValidateInput()) return;

//                _income.PartID = (int)cmbPart.SelectedValue;
//                _income.SupplierID = (int)cmbSupplier.SelectedValue;
//                _income.Date = dtpDate.Value;
//                _income.Quantity = int.Parse(txtQuantity.Text.Trim());
//                _income.UnitPrice = decimal.Parse(txtUnitPrice.Text.Trim());
//                _income.Markup = decimal.Parse(txtMarkup.Text.Trim());
//                _income.StatusID = (int)cmbStatus.SelectedValue;
//                _income.OperationID = (int)cmbOperation.SelectedValue;
//                _income.StockID = (int)cmbStock.SelectedValue;
//                _income.InvoiceNumber = txtInvoiceNumber.Text.Trim();
//                _income.UserID = (int)cmbUser.SelectedValue;
//                _income.PaidAmount = decimal.Parse(txtPaidAmount.Text.Trim());
//                _income.BatchID = int.Parse(txtBatch.Text.Trim());

//                if (_incomeId == null)
//                {
//                    _viewModel.AddPartsIncome(_income);
//                }
//                else
//                {
//                    _viewModel.UpdatePartsIncome(_income);
//                }

//                DialogResult = DialogResult.OK;
//                Close();
//            }
//            catch (Exception ex)
//            {
//                ShowError($"Ошибка при сохранении: {ex.Message}");
//                System.Diagnostics.Debug.WriteLine($"BtnSave_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
//            }
//        }

//        private bool ValidateInput()
//        {
//            if (cmbPart.SelectedValue == null)
//            {
//                ShowError("Выберите деталь.");
//                return false;
//            }
//            if (cmbSupplier.SelectedValue == null)
//            {
//                ShowError("Выберите поставщика.");
//                return false;
//            }
//            if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text.Trim(), out int quantity) || quantity <= 0)
//            {
//                ShowError("Введите корректное количество (положительное число).");
//                return false;
//            }
//            if (string.IsNullOrWhiteSpace(txtUnitPrice.Text) || !decimal.TryParse(txtUnitPrice.Text.Trim(), out decimal unitPrice) || unitPrice < 0)
//            {
//                ShowError("Введите корректную цену за единицу (неотрицательное число).");
//                return false;
//            }
//            if (string.IsNullOrWhiteSpace(txtMarkup.Text) || !decimal.TryParse(txtMarkup.Text.Trim(), out decimal markup) || markup < 0)
//            {
//                ShowError("Введите корректную наценку (неотрицательное число).");
//                return false;
//            }
//            if (cmbStatus.SelectedValue == null)
//            {
//                ShowError("Выберите статус.");
//                return false;
//            }
//            if (cmbOperation.SelectedValue == null)
//            {
//                ShowError("Выберите операцию.");
//                return false;
//            }
//            if (cmbStock.SelectedValue == null)
//            {
//                ShowError("Выберите склад.");
//                return false;
//            }
//            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
//            {
//                ShowError("Введите номер счета.");
//                return false;
//            }
//            if (cmbUser.SelectedValue == null)
//            {
//                ShowError("Выберите пользователя.");
//                return false;
//            }
//            if (string.IsNullOrWhiteSpace(txtPaidAmount.Text) || !decimal.TryParse(txtPaidAmount.Text.Trim(), out decimal paidAmount) || paidAmount < 0)
//            {
//                ShowError("Введите корректную оплаченную сумму (неотрицательное число).");
//                return false;
//            }
//            if (string.IsNullOrWhiteSpace(txtBatch.Text) || !int.TryParse(txtBatch.Text.Trim(), out int batchId) || batchId <= 0)
//            {
//                ShowError("Введите корректный ID партии (положительное число).");
//                return false;
//            }
//            return true;
//        }

//        private void BtnCancel_Click(object sender, EventArgs e)
//        {
//            DialogResult = DialogResult.Cancel;
//            Close();
//        }

//        private void ErrorTimer_Tick(object sender, EventArgs e)
//        {
//            lblError.Visible = false;
//            _errorTimer.Stop();
//        }

//        private void ShowError(string message)
//        {
//            lblError.Text = message;
//            lblError.Visible = true;
//            _errorTimer.Stop();
//            _errorTimer.Start();
//        }
//    }
//}
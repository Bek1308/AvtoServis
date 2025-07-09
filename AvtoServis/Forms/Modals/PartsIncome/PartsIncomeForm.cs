using AvtoServis.Data.Configuration;
using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.Forms.Controls;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Text.Json;
using System.IO;
using AvtoServis.Services.Core;

namespace AvtoServis.Forms
{
    public partial class PartsIncomeForm : Form
    {
        private readonly PartsIncomeViewModel _viewModel;
        private List<PartsIncome> _dataSource;
        private System.Windows.Forms.Timer _errorTimer;
        private System.Windows.Forms.Timer _searchTimer;
        private readonly ImageList _actionImageList;
        private readonly Dictionary<string, bool> _columnVisibility;
        private string _sortColumn = "IncomeID";
        private SortOrder _sortOrder = SortOrder.Ascending;
        private readonly string _tempFilePath = Path.Combine("parts_income_temp.json");
        private bool _isProcessing = false;
        string connectionString = DatabaseConfig.ConnectionString;

        public PartsIncomeForm(PartsIncomeViewModel viewModel = null, ImageList actionImageList = null)
        {
            _viewModel = new PartsIncomeViewModel(
                    new PartsIncomeRepository(connectionString),
                    new PartsRepository(connectionString),
                    new SuppliersRepository(connectionString),
                    new StatusRepository(connectionString),
                    new Finance_StatusRepository(connectionString),
                    new StockRepository(connectionString),
                    new BatchRepository(connectionString));

            _actionImageList = actionImageList ?? new ImageList { ImageSize = new Size(24, 24) };
            _dataSource = LoadTempData();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            _searchTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _searchTimer.Tick += SearchTimer_Tick;
            _columnVisibility = new Dictionary<string, bool>
            {
                { "IncomeID", true },
                { "PartName", true },
                { "SupplierName", true },
                { "Date", true },
                { "Quantity", true },
                { "UnitPrice", true },
                { "Markup", true },
                { "StatusName", true },
                { "OperationID", true },
                { "StockName", true },
                { "InvoiceNumber", true },
                { "UserFullName", true },
                { "PaidAmount", true },
                { "Actions", true }
            };
            InitializeComponent();
            ConfigureDataGridView();
            InitializeComboBoxes();
            EnhanceVisualStyles();
            InitializeSearch();
            OptimizeDataGridView();
            UpdateVisibleColumns();
            SetToolTips();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateSaveButtonState();
        }

        private List<PartsIncome> LoadTempData()
        {
            try
            {
                if (File.Exists(_tempFilePath))
                {
                    var json = File.ReadAllText(_tempFilePath);
                    var data = JsonSerializer.Deserialize<List<PartsIncome>>(json);
                    return data?.Where(item => ValidateIncome(item)).ToList() ?? new List<PartsIncome>();
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "загрузке временных данных");
            }
            return new List<PartsIncome>();
        }

        private void SaveTempData()
        {
            try
            {
                var json = JsonSerializer.Serialize(_dataSource, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_tempFilePath, json);
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "сохранении временных данных");
            }
        }

        private void UpdateSaveButtonState()
        {
            btnSave.Enabled = _dataSource.All(item => ValidateIncome(item));
        }

        private bool ValidateIncome(PartsIncome income)
        {
            // Majburiy maydonlarni tekshirish
            return income != null &&
                   income.PartID > 0 &&
                   income.SupplierID > 0 &&
                   income.Date != default &&
                   income.Quantity > 0 &&
                   income.UnitPrice >= 0 &&
                   income.StatusID > 0 &&
                   income.StockID > 0 &&
                   !string.IsNullOrWhiteSpace(income.InvoiceNumber) &&
                   income.PaidAmount >= 0 &&
                   income.Markup >= 0 &&
                   income.PaidAmount <= income.Quantity * income.UnitPrice;
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(mainTableLayoutPanel, "Основная панель управления поступлениями деталей");
            toolTip.SetToolTip(titleLabel, "Заголовок раздела поступлений деталей");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(verticalSeparator, "Разделительная линия");
            toolTip.SetToolTip(searchBox, "Введите текст для поиска по видимым столбцам");
            toolTip.SetToolTip(buttonPanel, "Панель с кнопками управления");
            toolTip.SetToolTip(btnExport, "Экспортировать данные в Excel");
            toolTip.SetToolTip(btnOpenFilterDialog, "Открыть окно фильтров");
            toolTip.SetToolTip(btnImport, "Импортировать новые поступления или скачать пример");
            toolTip.SetToolTip(btnColumns, "Выбрать видимые столбцы таблицы");
            toolTip.SetToolTip(dataGridView, "Таблица с данными о поступлениях деталей");
            toolTip.SetToolTip(countLabel, "Количество отображаемых поступлений");
            toolTip.SetToolTip(btnAdd, "Добавить новое поступление");
            toolTip.SetToolTip(rightPanel, "Панель для ввода данных о новом поступлении");
            toolTip.SetToolTip(lblPartID, "Выберите деталь");
            toolTip.SetToolTip(cmbPartID, "Список доступных деталей");
            toolTip.SetToolTip(lblSupplierID, "Выберите поставщика");
            toolTip.SetToolTip(cmbSupplierID, "Список доступных поставщиков");
            toolTip.SetToolTip(lblDate, "Дата поступления");
            toolTip.SetToolTip(dtpDate, "Выберите дату поступления");
            toolTip.SetToolTip(lblQuantity, "Количество деталей");
            toolTip.SetToolTip(txtQuantity, "Введите количество деталей");
            toolTip.SetToolTip(lblUnitPrice, "Цена за единицу");
            toolTip.SetToolTip(txtUnitPrice, "Введите цену за единицу");
            toolTip.SetToolTip(lblMarkup, "Наценка на деталь (необязательно)");
            toolTip.SetToolTip(txtMarkup, "Введите наценку (необязательно)");
            toolTip.SetToolTip(lblStatusID, "Статус поступления");
            toolTip.SetToolTip(cmbStatusID, "Выберите статус поступления");
            toolTip.SetToolTip(lblStockID, "Склад поступления");
            toolTip.SetToolTip(cmbStockID, "Выберите склад");
            toolTip.SetToolTip(lblInvoiceNumber, "Номер счета");
            toolTip.SetToolTip(txtInvoiceNumber, "Введите номер счета");
            toolTip.SetToolTip(lblPaidAmount, "Оплаченная сумма (необязательно)");
            toolTip.SetToolTip(txtPaidAmount, "Введите оплаченную сумму (необязательно)");
            toolTip.SetToolTip(btnSave, "Сохранить изменения");
            toolTip.SetToolTip(btnCancel, "Отменить изменения");
            toolTip.SetToolTip(panelError, "Панель для отображения ошибок");
            toolTip.SetToolTip(lblError, "Сообщение об ошибке");
        }

        private void UpdateVisibleColumns()
        {
            _viewModel.VisibleColumns = _columnVisibility
                .Where(kvp => kvp.Value && kvp.Key != "Actions")
                .Select(kvp => kvp.Key)
                .ToList();
        }

        private void ConfigureDataGridView()
        {
            dataGridView.Columns.Clear();
            var columnDefinitions = new List<(string Name, string HeaderText, string DataPropertyName)>
            {
                ("IncomeID", "Номер", "IncomeID"),
                ("PartName", "Название детали", "PartName"),
                ("SupplierName", "Поставщик", "SupplierName"),
                ("Date", "Дата", "Date"),
                ("Quantity", "Количество", "Quantity"),
                ("UnitPrice", "Цена за единицу", "UnitPrice"),
                ("Markup", "Наценка", "Markup"),
                ("StatusName", "Статус", "StatusName"),
                ("OperationID", "ID операции", "OperationID"),
                ("StockName", "Склад", "StockName"),
                ("InvoiceNumber", "Номер счета", "InvoiceNumber"),
                ("UserFullName", "Пользователь", "UserFullName"),
                ("PaidAmount", "Оплаченная сумма", "PaidAmount")
            };

            foreach (var col in columnDefinitions)
            {
                if (_columnVisibility.ContainsKey(col.Name) && _columnVisibility[col.Name])
                {
                    dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = col.Name,
                        HeaderText = col.HeaderText,
                        DataPropertyName = col.DataPropertyName,
                        ReadOnly = true,
                        Width = col.Name.Contains("Name") ? 150 : 100,
                        DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                    });
                }
            }

            dataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Actions",
                HeaderText = "Действия",
                Text = "...",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Padding = new Padding(0),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            });

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.CellFormatting += DataGridView_CellFormatting;
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _dataSource.Count) return;

            var income = _dataSource[e.RowIndex];
            var columnName = dataGridView.Columns[e.ColumnIndex].Name;

            bool isInvalid = false;
            switch (columnName)
            {
                case "PartName":
                    isInvalid = income.PartID <= 0;
                    break;
                case "SupplierName":
                    isInvalid = income.SupplierID <= 0;
                    break;
                case "Date":
                    isInvalid = income.Date == default;
                    break;
                case "Quantity":
                    isInvalid = income.Quantity <= 0;
                    break;
                case "UnitPrice":
                    isInvalid = income.UnitPrice < 0;
                    break;
                case "Markup":
                    isInvalid = income.Markup < 0;
                    break;
                case "StatusName":
                    isInvalid = income.StatusID <= 0;
                    break;
                case "StockName":
                    isInvalid = income.StockID <= 0;
                    break;
                case "InvoiceNumber":
                    isInvalid = string.IsNullOrWhiteSpace(income.InvoiceNumber);
                    break;
                case "PaidAmount":
                    isInvalid = income.PaidAmount < 0 || income.PaidAmount > income.Quantity * income.UnitPrice;
                    break;
            }

            if (isInvalid)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 200, 200);
            }
        }

        private void InitializeComboBoxes()
        {
            try
            {
                var partsrepository = new PartsRepository(connectionString);
                var parts = partsrepository.GetAll();
                cmbPartID.DataSource = parts;
                cmbPartID.DisplayMember = "PartName";
                cmbPartID.ValueMember = "PartID";
                cmbPartID.SelectedIndex = -1;

                var suppliersrepository = new SuppliersRepository(connectionString);
                var suppliers = suppliersrepository.GetAll();
                cmbSupplierID.DataSource = suppliers;
                cmbSupplierID.DisplayMember = "Name";
                cmbSupplierID.ValueMember = "SupplierID";
                cmbSupplierID.SelectedIndex = -1;

                var statusesrepository = new StatusRepository(connectionString);
                var statuses = statusesrepository.GetAll("IncomeStatuses");
                cmbStatusID.DataSource = statuses;
                cmbStatusID.DisplayMember = "Name";
                cmbStatusID.ValueMember = "StatusID";
                cmbStatusID.SelectedIndex = -1;

                var stockrepository = new StockRepository(connectionString);
                var stocks = stockrepository.GetAll();
                cmbStockID.DataSource = stocks;
                cmbStockID.DisplayMember = "Name";
                cmbStockID.ValueMember = "StockID";
                cmbStockID.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "загрузке данных для выпадающих списков");
            }
        }

        private void EnhanceVisualStyles()
        {
            btnExport.BackColor = Color.FromArgb(25, 118, 210);
            btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnOpenFilterDialog.BackColor = Color.FromArgb(25, 118, 210);
            btnOpenFilterDialog.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnImport.BackColor = Color.FromArgb(25, 118, 210);
            btnImport.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnColumns.BackColor = Color.FromArgb(25, 118, 210);
            btnColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnAdd.BackColor = Color.FromArgb(25, 118, 210);
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);

            dataGridView.BackgroundColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 243, 245);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void InitializeSearch()
        {
            _searchTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _searchTimer.Tick += SearchTimer_Tick;
        }

        private void OptimizeDataGridView()
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dataGridView,
                new object[] { true }
            );
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var income = new PartsIncome
            {
                IncomeID = _dataSource.Any() ? _dataSource.Max(x => x.IncomeID) + 1 : 1,
                PartID = (int)cmbPartID.SelectedValue,
                SupplierID = (int)cmbSupplierID.SelectedValue,
                Date = dtpDate.Value,
                Quantity = int.Parse(txtQuantity.Text),
                UnitPrice = decimal.Parse(txtUnitPrice.Text),
                Markup = string.IsNullOrWhiteSpace(txtMarkup.Text) ? 0 : decimal.Parse(txtMarkup.Text),
                StatusID = (int)cmbStatusID.SelectedValue,
                StockID = (int)cmbStockID.SelectedValue,
                InvoiceNumber = txtInvoiceNumber.Text,
                PaidAmount = string.IsNullOrWhiteSpace(txtPaidAmount.Text) ? 0 : decimal.Parse(txtPaidAmount.Text),
                PartName = cmbPartID.Text,
                SupplierName = cmbSupplierID.Text,
                StatusName = cmbStatusID.Text,
                StockName = cmbStockID.Text,
                UserID = CurrentUser.Instance.IsAuthenticated ? CurrentUser.Instance.User.UserID : 0,
                UserFullName = CurrentUser.Instance.IsAuthenticated ? CurrentUser.Instance.User.FullName : "Текущий пользователь"
            };

            _dataSource.Add(income);
            SaveTempData();
            RefreshDataGridView();
            ShowSuccess("Запись успешно добавлена!");
            ClearInputs();
            UpdateSaveButtonState();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!_dataSource.All(item => ValidateIncome(item)))
            {
                ShowError("Исправьте все ошибки в данных перед сохранением!");
                return;
            }
            DialogResult = DialogResult.OK;
            File.Delete(_tempFilePath);
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = $"PartsIncome_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _viewModel.ExportToExcel(_dataSource, saveFileDialog.FileName, _columnVisibility);
                        MessageBox.Show("Данные успешно экспортированы в Excel!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "экспорте данных в Excel");
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            using (var dialog = new Form
            {
                Text = "Импорт данных",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(245, 245, 245)
            })
            {
                var btnImport = new Button
                {
                    Text = "Импорт",
                    Location = new Point(50, 50),
                    Size = new Size(100, 30),
                    BackColor = Color.FromArgb(25, 118, 210),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(50, 140, 230) },
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };
                btnImport.Click += (s, ev) => { ImportFromExcel(); dialog.Close(); };

                var btnExample = new Button
                {
                    Text = "Пример",
                    Location = new Point(160, 50),
                    Size = new Size(100, 30),
                    BackColor = Color.FromArgb(25, 118, 210),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(50, 140, 230) },
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };
                btnExample.Click += (s, ev) => { DownloadExampleExcel(); dialog.Close(); };

                var btnCancel = new Button
                {
                    Text = "Отменить",
                    Location = new Point(105, 100),
                    Size = new Size(100, 30),
                    BackColor = Color.FromArgb(108, 117, 125),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(130, 140, 150) },
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };
                btnCancel.Click += (s, ev) => dialog.Close();

                dialog.Controls.Add(btnImport);
                dialog.Controls.Add(btnExample);
                dialog.Controls.Add(btnCancel);
                dialog.ShowDialog();
            }
        }

        private void DownloadExampleExcel()
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PartsIncomeExample");
                    worksheet.Cell(1, 1).Value = "PartName";
                    worksheet.Cell(1, 2).Value = "SupplierName";
                    worksheet.Cell(1, 3).Value = "Date";
                    worksheet.Cell(1, 4).Value = "Quantity";
                    worksheet.Cell(1, 5).Value = "UnitPrice";
                    worksheet.Cell(1, 6).Value = "Markup";
                    worksheet.Cell(1, 7).Value = "StatusName";
                    worksheet.Cell(1, 8).Value = "StockName";
                    worksheet.Cell(1, 9).Value = "InvoiceNumber";
                    worksheet.Cell(1, 10).Value = "PaidAmount";

                    worksheet.Cell(2, 1).Value = "Shina";
                    worksheet.Cell(2, 2).Value = "AutoSupplier";
                    worksheet.Cell(2, 3).Value = DateTime.Now.ToString("yyyy-MM-dd");
                    worksheet.Cell(2, 4).Value = 10;
                    worksheet.Cell(2, 5).Value = 100.50;
                    worksheet.Cell(2, 6).Value = 20.00; // Ixtiyoriy maydon
                    worksheet.Cell(2, 7).Value = "Received";
                    worksheet.Cell(2, 8).Value = "MainStock";
                    worksheet.Cell(2, 9).Value = "INV001";
                    worksheet.Cell(2, 10).Value = 1000.00; // Ixtiyoriy maydon

                    using (var saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                        saveFileDialog.FileName = "PartsIncome_Example.xlsx";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Пример Excel файла успешно сохранен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "создании примера Excel файла");
            }
        }

        private void ImportFromExcel()
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook(openFileDialog.FileName))
                        {
                            var worksheet = workbook.Worksheet(1);
                            var rowCount = worksheet.LastRowUsed()?.RowNumber() ?? 0;
                            if (rowCount < 2) throw new Exception("Файл не содержит данных для импорта.");

                            var headers = new Dictionary<string, int>();
                            var firstRow = worksheet.Row(1);
                            for (int col = 1; col <= worksheet.LastColumnUsed().ColumnNumber(); col++)
                            {
                                var header = firstRow.Cell(col).GetString()?.Trim();
                                if (!string.IsNullOrEmpty(header))
                                {
                                    headers[header] = col;
                                }
                            }

                            var partsRepo = new PartsRepository(connectionString);
                            var suppliersRepo = new SuppliersRepository(connectionString);
                            var statusesRepo = new StatusRepository(connectionString);
                            var stocksRepo = new StockRepository(connectionString);

                            var parts = partsRepo.GetAll().ToList();
                            var suppliers = suppliersRepo.GetAll().ToList();
                            var statuses = statusesRepo.GetAll("IncomeStatuses").ToList();
                            var stocks = stocksRepo.GetAll().ToList();

                            for (int row = 2; row <= rowCount; row++)
                            {
                                var income = new PartsIncome
                                {
                                    IncomeID = _dataSource.Any() ? _dataSource.Max(x => x.IncomeID) + 1 : 1,
                                    UserID = CurrentUser.Instance.IsAuthenticated ? CurrentUser.Instance.User.UserID : 0,
                                    UserFullName = CurrentUser.Instance.IsAuthenticated ? CurrentUser.Instance.User.FullName : "Текущий пользователь",
                                    Date = DateTime.Now,
                                    PaidAmount = 0,
                                    Markup = 0
                                };

                                bool hasRequiredFields = true;

                                if (headers.ContainsKey("PartID") && int.TryParse(worksheet.Cell(row, headers["PartID"]).GetString(), out int partID) && parts.Any(p => p.PartID == partID))
                                {
                                    income.PartID = partID;
                                    income.PartName = parts.First(p => p.PartID == partID).PartName;
                                }
                                else if (headers.ContainsKey("PartName"))
                                {
                                    var partName = worksheet.Cell(row, headers["PartName"]).GetString()?.Trim();
                                    var part = parts.FirstOrDefault(p => p.PartName.Equals(partName, StringComparison.OrdinalIgnoreCase));
                                    if (part != null)
                                    {
                                        income.PartID = part.PartID;
                                        income.PartName = part.PartName;
                                    }
                                    else
                                    {
                                        hasRequiredFields = false;
                                    }
                                }
                                else
                                {
                                    hasRequiredFields = false;
                                }

                                if (headers.ContainsKey("Quantity") && int.TryParse(worksheet.Cell(row, headers["Quantity"]).GetString(), out int quantity) && quantity > 0)
                                {
                                    income.Quantity = quantity;
                                }
                                else
                                {
                                    hasRequiredFields = false;
                                }

                                if (headers.ContainsKey("UnitPrice") && decimal.TryParse(worksheet.Cell(row, headers["UnitPrice"]).GetString(), out decimal unitPrice) && unitPrice >= 0)
                                {
                                    income.UnitPrice = unitPrice;
                                }
                                else
                                {
                                    hasRequiredFields = false;
                                }

                                if (headers.ContainsKey("Markup") && decimal.TryParse(worksheet.Cell(row, headers["Markup"]).GetString(), out decimal markup) && markup >= 0)
                                {
                                    income.Markup = markup;
                                }
                                // Markup ixtiyoriy, shuning uchun bo'sh bo'lsa 0 qabul qilinadi

                                if (headers.ContainsKey("SupplierID") && int.TryParse(worksheet.Cell(row, headers["SupplierID"]).GetString(), out int supplierID) && suppliers.Any(s => s.SupplierID == supplierID))
                                {
                                    income.SupplierID = supplierID;
                                    income.SupplierName = suppliers.First(s => s.SupplierID == supplierID).Name;
                                }
                                else if (headers.ContainsKey("SupplierName"))
                                {
                                    var supplierName = worksheet.Cell(row, headers["SupplierName"]).GetString()?.Trim();
                                    var supplier = suppliers.FirstOrDefault(s => s.Name.Equals(supplierName, StringComparison.OrdinalIgnoreCase));
                                    if (supplier != null)
                                    {
                                        income.SupplierID = supplier.SupplierID;
                                        income.SupplierName = supplier.Name;
                                    }
                                    else
                                    {
                                        hasRequiredFields = false;
                                    }
                                }
                                else
                                {
                                    hasRequiredFields = false;
                                }

                                if (headers.ContainsKey("Date") && DateTime.TryParse(worksheet.Cell(row, headers["Date"]).GetString(), out DateTime date))
                                {
                                    income.Date = date;
                                }
                                else
                                {
                                    hasRequiredFields = false;
                                }

                                if (headers.ContainsKey("StatusID") && int.TryParse(worksheet.Cell(row, headers["StatusID"]).GetString(), out int statusID) && statuses.Any(s => s.StatusID == statusID))
                                {
                                    income.StatusID = statusID;
                                    income.StatusName = statuses.First(s => s.StatusID == statusID).Name;
                                }
                                else if (headers.ContainsKey("StatusName"))
                                {
                                    var statusName = worksheet.Cell(row, headers["StatusName"]).GetString()?.Trim();
                                    var status = statuses.FirstOrDefault(s => s.Name.Equals(statusName, StringComparison.OrdinalIgnoreCase));
                                    if (status != null)
                                    {
                                        income.StatusID = status.StatusID;
                                        income.StatusName = status.Name;
                                    }
                                    else
                                    {
                                        hasRequiredFields = false;
                                    }
                                }
                                else
                                {
                                    hasRequiredFields = false;
                                }

                                if (headers.ContainsKey("StockID") && int.TryParse(worksheet.Cell(row, headers["StockID"]).GetString(), out int stockID) && stocks.Any(s => s.StockID == stockID))
                                {
                                    income.StockID = stockID;
                                    income.StockName = stocks.First(s => s.StockID == stockID).Name;
                                }
                                else if (headers.ContainsKey("StockName"))
                                {
                                    var stockName = worksheet.Cell(row, headers["StockName"]).GetString()?.Trim();
                                    var stock = stocks.FirstOrDefault(s => s.Name.Equals(stockName, StringComparison.OrdinalIgnoreCase));
                                    if (stock != null)
                                    {
                                        income.StockID = stock.StockID;
                                        income.StockName = stock.Name;
                                    }
                                    else
                                    {
                                        hasRequiredFields = false;
                                    }
                                }
                                else
                                {
                                    hasRequiredFields = false;
                                }

                                if (headers.ContainsKey("InvoiceNumber"))
                                {
                                    var invoiceNumber = worksheet.Cell(row, headers["InvoiceNumber"]).GetString()?.Trim();
                                    if (!string.IsNullOrWhiteSpace(invoiceNumber))
                                    {
                                        income.InvoiceNumber = invoiceNumber;
                                    }
                                    else
                                    {
                                        hasRequiredFields = false;
                                    }
                                }
                                else
                                {
                                    hasRequiredFields = false;
                                }

                                if (headers.ContainsKey("PaidAmount") && decimal.TryParse(worksheet.Cell(row, headers["PaidAmount"]).GetString(), out decimal paidAmount) && paidAmount >= 0)
                                {
                                    income.PaidAmount = paidAmount;
                                }
                                // PaidAmount ixtiyoriy, shuning uchun bo'sh bo'lsa 0 qabul qilinadi

                                if (hasRequiredFields && ValidateIncome(income))
                                {
                                    _dataSource.Add(income);
                                }
                                else
                                {
                                    ShowError($"Qator {row} import qilinmadi: to'liq bo'lmagan yoki noto'g'ri ma'lumotlar.");
                                }
                            }

                            SaveTempData();
                            RefreshDataGridView();
                            UpdateSaveButtonState();
                            if (_dataSource.Any())
                            {
                                ShowSuccess("Данные успешно импортированы! Пропущенные или неверные поля отмечены красным в таблице.");
                            }
                            else
                            {
                                ShowError("Ни одна строка не была импортирована, так как не найдено корректных данных.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "импорте данных из Excel");
            }
        }

        private void BtnColumns_Click(object sender, EventArgs e)
        {
            using (var dialog = new Form
            {
                Text = "Выбор столбцов",
                Size = new Size(300, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(245, 245, 245)
            })
            {
                var checkedListBox = new CheckedListBox
                {
                    Location = new Point(10, 10),
                    Size = new Size(260, 300),
                    CheckOnClick = true,
                    Font = new Font("Segoe UI", 10F),
                    BorderStyle = BorderStyle.FixedSingle
                };
                var columnDisplayNames = new Dictionary<string, string>
                {
                    { "IncomeID", "Номер" },
                    { "PartName", "Название детали" },
                    { "SupplierName", "Поставщик" },
                    { "Date", "Дата" },
                    { "Quantity", "Количество" },
                    { "UnitPrice", "Цена за единицу" },
                    { "Markup", "Наценка" },
                    { "StatusName", "Статус" },
                    { "OperationID", "ID операции" },
                    { "StockName", "Склад" },
                    { "InvoiceNumber", "Номер счета" },
                    { "UserFullName", "Пользователь" },
                    { "PaidAmount", "Оплаченная сумма" }
                };

                foreach (var column in _columnVisibility)
                {
                    if (column.Key != "Actions")
                    {
                        checkedListBox.Items.Add(new { Name = column.Key, DisplayName = columnDisplayNames[column.Key] }, column.Value);
                    }
                }
                checkedListBox.DisplayMember = "DisplayName";
                checkedListBox.ValueMember = "Name";

                var btnOk = new Button
                {
                    Text = "ОК",
                    Location = new Point(100, 320),
                    Size = new Size(80, 30),
                    BackColor = Color.FromArgb(40, 167, 69),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(60, 187, 89) },
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };
                btnOk.Click += (s, ev) =>
                {
                    int visibleCount = checkedListBox.CheckedItems.Count;
                    if (visibleCount == 0)
                    {
                        MessageBox.Show("Необходимо выбрать хотя бы один столбец!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    foreach (var column in _columnVisibility.Keys.ToList())
                    {
                        if (column != "Actions")
                        {
                            _columnVisibility[column] = false;
                        }
                    }
                    foreach (var item in checkedListBox.CheckedItems)
                    {
                        var col = (dynamic)item;
                        _columnVisibility[col.Name] = true;
                    }
                    ConfigureDataGridView();
                    UpdateVisibleColumns();
                    RefreshDataGridView();
                    dialog.Close();
                };

                dialog.Controls.Add(checkedListBox);
                dialog.Controls.Add(btnOk);
                dialog.ShowDialog();
            }
        }

        private void BtnOpenFilterDialog_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateVisibleColumns();
                using (var dialog = new PartsIncomeFilterDialog(_viewModel))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        ApplyFilters(dialog.Filters);
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "открытии фильтров");
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_isProcessing || e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _dataSource.Count) return;

            if (dataGridView.Columns[e.ColumnIndex].Name == "Actions")
            {
                var selectedRow = dataGridView.Rows[e.RowIndex];
                if (selectedRow.Cells["IncomeID"].Value == null || !int.TryParse(selectedRow.Cells["IncomeID"].Value.ToString(), out int incomeID))
                {
                    ShowError("Запись не найдена!");
                    return;
                }

                var income = _dataSource.FirstOrDefault(x => x.IncomeID == incomeID);
                if (income == null)
                {
                    ShowError("Запись не найдена!");
                    return;
                }

                var menu = new ContextMenuStrip
                {
                    ImageScalingSize = new Size(24, 24),
                    Renderer = new ToolStripProfessionalRenderer()
                };

                var editItem = new ToolStripMenuItem
                {
                    Text = "Редактировать",
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Size = new Size(0, 32),
                    Tag = "Edit"
                };
                editItem.Click += (s, ev) => EditIncome(income);
                menu.Items.Add(editItem);

                var deleteItem = new ToolStripMenuItem
                {
                    Text = "Удалить",
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Size = new Size(0, 32),
                    Tag = "Delete"
                };
                deleteItem.Click += (s, ev) => DeleteIncome(income);
                menu.Items.Add(deleteItem);

                menu.Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
            }
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var column = dataGridView.Columns[e.ColumnIndex].Name;
            if (column == "Actions") return;

            _sortOrder = _sortColumn == column && _sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            _sortColumn = column;
            SortDataSource();
            RefreshDataGridView();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            PerformSearch();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBox.Text) || searchBox.Text != "Поиск...")
            {
                _searchTimer.Stop();
                _searchTimer.Start();
            }
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _searchTimer.Stop();
                PerformSearch();
            }
        }

        private void SearchBox_Enter(object sender, EventArgs e)
        {
            if (searchBox.Text == "Поиск...")
            {
                searchBox.Text = "";
                searchBox.ForeColor = Color.Black;
            }
        }

        private void SearchBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = "Поиск...";
                searchBox.ForeColor = Color.Gray;
            }
        }

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            panelError.Visible = false;
            lblError.Visible = false;
            _errorTimer.Stop();
        }

        private void EditIncome(PartsIncome income)
        {
            try
            {
                if (_isProcessing) return;
                _isProcessing = true;

                // Kombinatsion ro'yxatlarni tekshirish va to'ldirish
                if (cmbPartID.Items.Cast<object>().Any(item => ((dynamic)item).PartID == income.PartID))
                {
                    cmbPartID.SelectedValue = income.PartID;
                }
                else
                {
                    ShowError("Выбранная деталь не найдена в списке!");
                    return;
                }

                if (cmbSupplierID.Items.Cast<object>().Any(item => ((dynamic)item).SupplierID == income.SupplierID))
                {
                    cmbSupplierID.SelectedValue = income.SupplierID;
                }
                else
                {
                    ShowError("Выбранный поставщик не найден в списке!");
                    return;
                }

                if (cmbStatusID.Items.Cast<object>().Any(item => ((dynamic)item).StatusID == income.StatusID))
                {
                    cmbStatusID.SelectedValue = income.StatusID;
                }
                else
                {
                    ShowError("Выбранный статус не найден в списке!");
                    return;
                }

                if (cmbStockID.Items.Cast<object>().Any(item => ((dynamic)item).StockID == income.StockID))
                {
                    cmbStockID.SelectedValue = income.StockID;
                }
                else
                {
                    ShowError("Выбранный склад не найден в списке!");
                    return;
                }

                dtpDate.Value = income.Date;
                txtQuantity.Text = income.Quantity.ToString();
                txtUnitPrice.Text = income.UnitPrice.ToString();
                txtMarkup.Text = income.Markup != 0 ? income.Markup.ToString() : "";
                txtInvoiceNumber.Text = income.InvoiceNumber;
                txtPaidAmount.Text = income.PaidAmount != 0 ? income.PaidAmount.ToString() : "";

                btnAdd.Text = "Обновить";

                btnAdd.Click -= BtnAdd_Click;
                btnAdd.Click -= UpdateIncome_Click;

                btnAdd.Click += UpdateIncome_Click;

                void UpdateIncome_Click(object sender, EventArgs e)
                {
                    try
                    {
                        if (!ValidateInputs()) return;

                        income.PartID = (int)cmbPartID.SelectedValue;
                        income.SupplierID = (int)cmbSupplierID.SelectedValue;
                        income.Date = dtpDate.Value;
                        income.Quantity = int.Parse(txtQuantity.Text);
                        income.UnitPrice = decimal.Parse(txtUnitPrice.Text);
                        income.Markup = string.IsNullOrWhiteSpace(txtMarkup.Text) ? 0 : decimal.Parse(txtMarkup.Text);
                        income.StatusID = (int)cmbStatusID.SelectedValue;
                        income.StockID = (int)cmbStockID.SelectedValue;
                        income.InvoiceNumber = txtInvoiceNumber.Text;
                        income.PaidAmount = string.IsNullOrWhiteSpace(txtPaidAmount.Text) ? 0 : decimal.Parse(txtPaidAmount.Text);
                        income.UserID = CurrentUser.Instance.IsAuthenticated ? CurrentUser.Instance.User.UserID : 0;
                        income.UserFullName = CurrentUser.Instance.IsAuthenticated ? CurrentUser.Instance.User.FullName : "Текущий пользователь";
                        income.PartName = cmbPartID.Text;
                        income.SupplierName = cmbSupplierID.Text;
                        income.StatusName = cmbStatusID.Text;
                        income.StockName = cmbStockID.Text;

                        SaveTempData();
                        RefreshDataGridView();
                        ShowSuccess("Запись успешно обновлена!");

                        ClearInputs();
                        btnAdd.Text = "Добавить";
                        btnAdd.Click -= UpdateIncome_Click;
                        btnAdd.Click += BtnAdd_Click;
                        UpdateSaveButtonState();
                    }
                    catch (Exception ex)
                    {
                        LogAndShowError(ex, "редактировании записи");
                    }
                    finally
                    {
                        _isProcessing = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "подготовке редактирования записи");
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private void DeleteIncome(PartsIncome income)
        {
            if (_isProcessing) return;
            try
            {
                _isProcessing = true;
                var result = MessageBox.Show($"Вы уверены, что хотите удалить поступление #{income.IncomeID}?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var itemToRemove = _dataSource.FirstOrDefault(x => x.IncomeID == income.IncomeID);
                    if (itemToRemove != null)
                    {
                        _dataSource.Remove(itemToRemove);
                        SaveTempData();
                        RefreshDataGridView();
                        ShowSuccess("Запись успешно удалена!");
                        UpdateSaveButtonState();
                    }
                    else
                    {
                        ShowError("Запись не найдена для удаления!");
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "удалении записи");
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private bool ValidateInputs()
        {
            // Kombinatsion ro'yxatlar uchun tekshiruv
            if (cmbPartID.SelectedValue == null)
            {
                ShowError("Деталь не выбрана!");
                return false;
            }

            if (cmbSupplierID.SelectedValue == null)
            {
                ShowError("Поставщик не выбран!");
                return false;
            }

            if (cmbStatusID.SelectedValue == null)
            {
                ShowError("Статус не выбран!");
                return false;
            }

            if (cmbStockID.SelectedValue == null)
            {
                ShowError("Склад не выбран!");
                return false;
            }

            // Majburiy matn maydonlari
            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                ShowError("Введите корректное количество (больше нуля)!");
                return false;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) || unitPrice < 0)
            {
                ShowError("Введите корректную цену за единицу (неотрицательное число)!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
            {
                ShowError("Введите номер счета!");
                return false;
            }

            // Ixtiyoriy maydonlar
            decimal markup = 0;
            if (!string.IsNullOrWhiteSpace(txtMarkup.Text) && (!decimal.TryParse(txtMarkup.Text, out markup) || markup < 0))
            {
                ShowError("Введите корректную наценку (неотрицательное число или оставьте пустым)!");
                return false;
            }

            decimal paidAmount = 0;
            if (!string.IsNullOrWhiteSpace(txtPaidAmount.Text) && (!decimal.TryParse(txtPaidAmount.Text, out paidAmount) || paidAmount < 0))
            {
                ShowError("Введите корректную оплаченную сумму (неотрицательное число или оставьте пустым)!");
                return false;
            }

            if (paidAmount > quantity * unitPrice)
            {
                ShowError("Оплаченная сумма не может превышать стоимость (количество * цена за единицу)!");
                return false;
            }

            return true;
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            panelError.BackColor = Color.FromArgb(255, 245, 245);
            panelError.Visible = true;
            lblError.Visible = true;
            _errorTimer.Start();
        }

        private void ShowSuccess(string message)
        {
            lblError.Text = message;
            lblError.ForeColor = Color.FromArgb(40, 167, 69);
            panelError.BackColor = Color.FromArgb(245, 255, 245);
            panelError.Visible = true;
            lblError.Visible = true;
            _errorTimer.Start();
        }

        private void ClearInputs()
        {
            cmbPartID.SelectedIndex = -1;
            cmbSupplierID.SelectedIndex = -1;
            dtpDate.Value = DateTime.Now;
            txtQuantity.Clear();
            txtUnitPrice.Clear();
            txtMarkup.Clear();
            cmbStatusID.SelectedIndex = -1;
            cmbStockID.SelectedIndex = -1;
            txtInvoiceNumber.Clear();
            txtPaidAmount.Clear();
        }

        public void LoadData()
        {
            SortDataSource();
            RefreshDataGridView();
        }

        private void PerformSearch()
        {
            try
            {
                var originalData = LoadTempData();
                if (string.IsNullOrWhiteSpace(searchBox.Text) || searchBox.Text == "Поиск...")
                {
                    _dataSource = originalData;
                }
                else
                {
                    _dataSource = _viewModel.SearchPartsIncomes(searchBox.Text.Trim(), _viewModel.VisibleColumns);
                }
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "поиске");
            }
        }

        public void ApplyFilters(List<(string Column, string SearchText)> filters)
        {
            try
            {
                var originalData = LoadTempData();
                _viewModel.Filters = filters;
                _dataSource = originalData.Where(item =>
                {
                    foreach (var filter in filters)
                    {
                        var property = typeof(PartsIncome).GetProperty(filter.Column);
                        if (property != null)
                        {
                            var value = property.GetValue(item)?.ToString();
                            if (!string.IsNullOrEmpty(value) && !value.Contains(filter.SearchText, StringComparison.OrdinalIgnoreCase))
                                return false;
                        }
                    }
                    return true;
                }).ToList();
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "применении фильтров");
            }
        }

        private void SortDataSource()
        {
            if (_dataSource == null) return;

            switch (_sortColumn)
            {
                case "IncomeID":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.IncomeID.CompareTo(y.IncomeID) : y.IncomeID.CompareTo(x.IncomeID));
                    break;
                case "PartName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.PartName, y.PartName) : string.Compare(y.PartName, x.PartName));
                    break;
                case "SupplierName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.SupplierName, y.SupplierName) : string.Compare(y.SupplierName, x.SupplierName));
                    break;
                case "Date":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.Date.CompareTo(y.Date) : y.Date.CompareTo(x.Date));
                    break;
                case "Quantity":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.Quantity.CompareTo(y.Quantity) : y.Quantity.CompareTo(x.Quantity));
                    break;
                case "UnitPrice":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.UnitPrice.CompareTo(y.UnitPrice) : y.UnitPrice.CompareTo(x.UnitPrice));
                    break;
                case "Markup":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.Markup.CompareTo(y.Markup) : y.Markup.CompareTo(x.Markup));
                    break;
                case "StatusName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.StatusName, y.StatusName) : string.Compare(y.StatusName, x.StatusName));
                    break;
                case "OperationID":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.OperationID.CompareTo(y.OperationID) : y.OperationID.CompareTo(x.OperationID));
                    break;
                case "StockName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.StockName, y.StockName) : string.Compare(y.StockName, x.StockName));
                    break;
                case "InvoiceNumber":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.InvoiceNumber, y.InvoiceNumber) : string.Compare(y.InvoiceNumber, x.InvoiceNumber));
                    break;
                case "UserFullName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.UserFullName, y.UserFullName) : string.Compare(y.UserFullName, x.UserFullName));
                    break;
                case "PaidAmount":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.PaidAmount.CompareTo(y.PaidAmount) : y.PaidAmount.CompareTo(x.PaidAmount));
                    break;
            }
        }

        private void RefreshDataGridView()
        {
            try
            {
                dataGridView.SuspendLayout();
                int selectedRowIndex = dataGridView.CurrentCell?.RowIndex ?? -1;

                dataGridView.DataSource = null;

                if (_dataSource != null && _dataSource.Any())
                {
                    dataGridView.DataSource = _dataSource.ToList();
                    countLabel.Text = $"Поступления: {_dataSource.Count}";

                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        if (_columnVisibility.ContainsKey(column.Name))
                        {
                            column.Visible = _columnVisibility[column.Name];
                        }
                    }

                    if (selectedRowIndex >= 0 && selectedRowIndex < _dataSource.Count)
                    {
                        dataGridView.CurrentCell = dataGridView.Rows[selectedRowIndex].Cells[0];
                    }
                }
                else
                {
                    _dataSource = new List<PartsIncome>();
                    countLabel.Text = "Поступления: 0";
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "обновлении таблицы");
            }
            finally
            {
                dataGridView.ResumeLayout();
                dataGridView.Refresh();
            }
        }

        private void LogAndShowError(Exception ex, string operation)
        {
            Console.WriteLine($"Ошибка при {operation}: {ex.Message}\nStackTrace: {ex.StackTrace}");
            ShowError($"Ошибка при {operation}: {ex.Message}");
        }
    }
}
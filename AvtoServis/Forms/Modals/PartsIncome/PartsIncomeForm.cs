using AvtoServis.Data.Configuration;
using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.Forms.Controls;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System.Reflection;
using AvtoServis.Forms.Modals.PartsIncome;
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
                //{ "OperationID", true },
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
                //("OperationID", "ID операции", "OperationID"),
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

            var current = CurrentUser.Instance;
            var user = current.IsAuthenticated ? current.User : null;

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
                UserID = user?.UserID ?? 0,
                UserFullName = user?.FullName ?? "Текущий пользователь"
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

            // Umumiy narxni hisoblash (Quantity * UnitPrice)
            decimal totalCost = _dataSource.Sum(item => item.Quantity * item.UnitPrice);
            decimal totalPaidSum = _dataSource.Sum(item => item.PaidAmount);
            // Yangi tasdiqlash formasini ochish
            using (var dialog = new BatchConfirmationForm(totalCost, totalPaidSum))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Partiya nomini va umumiy to'langan summasini olish
                        string batchName = dialog.BatchName;
                        decimal totalPaidAmount = dialog.TotalPaidAmount;

                        // Validatsiya: Umumiy to'langan summa totalCost dan oshmasligi kerak
                        if (totalPaidAmount > totalCost)
                        {
                            ShowError("Указанная общая оплаченная сумма превышает стоимость всех деталей!");
                            return;
                        }
                        if (totalPaidAmount < totalPaidSum)
                        {
                            ShowError("Указанная общая оплаченная сумма не может быть меньше суммы оплаченных сумм деталей!");
                            return;
                        }

                        // Ma'lumotlarni bazaga yozish
                        _viewModel.SavePartsIncomes(_dataSource, batchName, totalPaidAmount);

                        // Muvaffaqiyatli saqlangandan so'ng
                        File.Delete(_tempFilePath);
                        DialogResult = DialogResult.OK;
                        ShowSuccess("Данные успешно сохранены в базу!");
                        Close();
                    }
                    catch (Exception ex)
                    {
                        LogAndShowError(ex, "сохранении данных в базу");
                    }
                }
            }
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
                Size = new Size(300, 150),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(245, 245, 245)
            })
            {
                var lblMessage = new Label
                {
                    Text = "Выберите действия",
                    Location = new Point(15, 10),
                    AutoSize = true,
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)

                };
                var btnImport = new Button
                {
                    Text = "Импорт",
                    Location = new Point(15, 50),
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
                    Location = new Point(165, 50),
                    Size = new Size(100, 30),
                    BackColor = Color.FromArgb(25, 118, 210),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(50, 140, 230) },
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };
                btnExample.Click += (s, ev) => { DownloadExampleExcel(); dialog.Close(); };


                dialog.Controls.Add(btnImport);
                dialog.Controls.Add(btnExample);
                dialog.Controls.Add(lblMessage);
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

                    // Заголовки (только ID и обязательные поля)
                    worksheet.Cell(1, 1).Value = "PartID";
                    worksheet.Cell(1, 2).Value = "SupplierID";
                    worksheet.Cell(1, 3).Value = "Date";
                    worksheet.Cell(1, 4).Value = "Quantity";
                    worksheet.Cell(1, 5).Value = "UnitPrice";
                    worksheet.Cell(1, 6).Value = "Markup";
                    worksheet.Cell(1, 7).Value = "StatusID";
                    worksheet.Cell(1, 8).Value = "StockID";
                    worksheet.Cell(1, 9).Value = "InvoiceNumber";
                    worksheet.Cell(1, 10).Value = "PaidAmount";
                    worksheet.Cell(1, 11).Value = "UserID";
                    worksheet.Cell(1, 12).Value = "BatchID";

                    // Пример данных из базы
                    var partsRepo = new PartsRepository(connectionString);
                    var suppliersRepo = new SuppliersRepository(connectionString);
                    var statusesRepo = new StatusRepository(connectionString);
                    var stocksRepo = new StockRepository(connectionString);

                    var parts = partsRepo.GetAll().Take(2).ToList();
                    var suppliers = suppliersRepo.GetAll().Take(2).ToList();
                    var statuses = statusesRepo.GetAll("IncomeStatuses").Take(2).ToList();
                    var stocks = stocksRepo.GetAll().Take(2).ToList();

                    int row = 2;
                    worksheet.Cell(row, 1).Value = parts.Any() ? parts[0].PartID : 1;
                    worksheet.Cell(row, 2).Value = suppliers.Any() ? suppliers[0].SupplierID : 1;
                    worksheet.Cell(row, 3).Value = DateTime.Now.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 4).Value = 10;
                    worksheet.Cell(row, 5).Value = 100.50;
                    worksheet.Cell(row, 6).Value = 20.00;
                    worksheet.Cell(row, 7).Value = statuses.Any() ? statuses[0].StatusID : 1;
                    worksheet.Cell(row, 8).Value = stocks.Any() ? stocks[0].StockID : 1;
                    worksheet.Cell(row, 9).Value = "INV001";
                    worksheet.Cell(row, 10).Value = 1000.00;
                    worksheet.Cell(row, 11).Value = CurrentUser.Instance.IsAuthenticated ? CurrentUser.Instance.User.UserID : 1;
                    worksheet.Cell(row, 12).Value = 1;

                    row++;
                    if (parts.Count > 1 && suppliers.Count > 1 && statuses.Count > 1 && stocks.Count > 1)
                    {
                        worksheet.Cell(row, 1).Value = parts[1].PartID;
                        worksheet.Cell(row, 2).Value = suppliers[1].SupplierID;
                        worksheet.Cell(row, 3).Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                        worksheet.Cell(row, 4).Value = 5;
                        worksheet.Cell(row, 5).Value = 200.75;
                        worksheet.Cell(row, 6).Value = 15.00;
                        worksheet.Cell(row, 7).Value = statuses[1].StatusID;
                        worksheet.Cell(row, 8).Value = stocks[1].StockID;
                        worksheet.Cell(row, 9).Value = "INV002";
                        worksheet.Cell(row, 10).Value = 1500.00;
                        worksheet.Cell(row, 11).Value = CurrentUser.Instance.IsAuthenticated ? CurrentUser.Instance.User.UserID : 1;
                        worksheet.Cell(row, 12).Value = 2;
                    }

                    worksheet.Columns().AdjustToContents();

                    using (var saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                        saveFileDialog.FileName = "PartsIncome_Example.xlsx";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Пример файла Excel успешно сохранен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "при создании примера файла Excel");
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

                            var errors = new List<string>();
                            bool hasValidData = false;

                            for (int row = 2; row <= rowCount; row++)
                            {
                                var cerrent = CurrentUser.Instance;
                                var  user = cerrent.IsAuthenticated ? cerrent.User : null;
                                var income = new PartsIncome
                                {

                                    IncomeID = _dataSource.Any() ? _dataSource.Max(x => x.IncomeID) + 1 : 1,
                                    UserID = user?.UserID ?? 1,
                                    UserFullName = user?.FullName ?? "Текущий пользователь",
                                    //Date = DateTime.Now, // Default sifatida null
                                    PaidAmount = 0,
                                    Markup = 0,
                                    Quantity = 0,
                                    UnitPrice = 0,
                                    //PartID = 0,
                                    //SupplierID = 0,
                                    //StatusID = 0,
                                    //StockID = 0,
                                    ////OperationID = 0,
                                    //Finance_Status_Id = 0,
                                    //BatchID = 0
                                };

                                bool hasAnyValidField = false;
                                var rowErrors = new List<string>();

                                // PartID
                                if (headers.ContainsKey("PartID") && int.TryParse(worksheet.Cell(row, headers["PartID"]).GetString(), out int partID))
                                {
                                    if (parts.Any(p => p.PartID == partID))
                                    {
                                        income.PartID = partID;
                                        income.PartName = parts.First(p => p.PartID == partID).PartName;
                                        hasAnyValidField = true;
                                    }
                                    else
                                    {
                                        rowErrors.Add($"PartID '{partID}' не найден, установлен как 0.");
                                    }
                                }

                                // SupplierID
                                if (headers.ContainsKey("SupplierID") && int.TryParse(worksheet.Cell(row, headers["SupplierID"]).GetString(), out int supplierID))
                                {
                                    if (suppliers.Any(s => s.SupplierID == supplierID))
                                    {
                                        income.SupplierID = supplierID;
                                        income.SupplierName = suppliers.First(s => s.SupplierID == supplierID).Name;
                                        hasAnyValidField = true;
                                    }
                                    else
                                    {
                                        rowErrors.Add($"SupplierID '{supplierID}' не найден, установлен как 0.");
                                    }
                                }

                                // Date
                                if (headers.ContainsKey("Date") && DateTime.TryParse(worksheet.Cell(row, headers["Date"]).GetString(), out DateTime date))
                                {
                                    income.Date = date;
                                    hasAnyValidField = true;
                                }
                                else if (headers.ContainsKey("Date"))
                                {
                                    rowErrors.Add("Дата некорректна, установлена как null.");
                                }

                                // Quantity
                                if (headers.ContainsKey("Quantity") && int.TryParse(worksheet.Cell(row, headers["Quantity"]).GetString(), out int quantity))
                                {
                                    if (quantity >= 0)
                                    {
                                        income.Quantity = quantity;
                                        hasAnyValidField = true;
                                    }
                                    else
                                    {
                                        rowErrors.Add("Количество отрицательное, установлено как 0.");
                                    }
                                }
                                else if (headers.ContainsKey("Quantity"))
                                {
                                    rowErrors.Add("Количество некорректно, установлено как 0.");
                                }

                                // UnitPrice
                                if (headers.ContainsKey("UnitPrice") && decimal.TryParse(worksheet.Cell(row, headers["UnitPrice"]).GetString(), out decimal unitPrice))
                                {
                                    if (unitPrice >= 0)
                                    {
                                        income.UnitPrice = unitPrice;
                                        hasAnyValidField = true;
                                    }
                                    else
                                    {
                                        rowErrors.Add("Цена за единицу отрицательная, установлена как 0.");
                                    }
                                }
                                else if (headers.ContainsKey("UnitPrice"))
                                {
                                    rowErrors.Add("Цена за единицу некорректна, установлена как 0.");
                                }

                                // Markup
                                if (headers.ContainsKey("Markup") && decimal.TryParse(worksheet.Cell(row, headers["Markup"]).GetString(), out decimal markup))
                                {
                                    if (markup >= 0)
                                    {
                                        income.Markup = markup;
                                        hasAnyValidField = true;
                                    }
                                    else
                                    {
                                        rowErrors.Add("Наценка отрицательная, установлена как 0.");
                                    }
                                }

                                // StatusID
                                if (headers.ContainsKey("StatusID") && int.TryParse(worksheet.Cell(row, headers["StatusID"]).GetString(), out int statusID))
                                {
                                    if (statuses.Any(s => s.StatusID == statusID))
                                    {
                                        income.StatusID = statusID;
                                        income.StatusName = statuses.First(s => s.StatusID == statusID).Name;
                                        hasAnyValidField = true;
                                    }
                                    else
                                    {
                                        rowErrors.Add($"StatusID '{statusID}' не найден, установлен как 0.");
                                    }
                                }

                                // StockID
                                if (headers.ContainsKey("StockID") && int.TryParse(worksheet.Cell(row, headers["StockID"]).GetString(), out int stockID))
                                {
                                    if (stocks.Any(s => s.StockID == stockID))
                                    {
                                        income.StockID = stockID;
                                        income.StockName = stocks.First(s => s.StockID == stockID).Name;
                                        hasAnyValidField = true;
                                    }
                                    else
                                    {
                                        rowErrors.Add($"StockID '{stockID}' не найден, установлен как 0.");
                                    }
                                }

                                // InvoiceNumber
                                if (headers.ContainsKey("InvoiceNumber"))
                                {
                                    var invoiceNumber = worksheet.Cell(row, headers["InvoiceNumber"]).GetString()?.Trim();
                                    if (!string.IsNullOrWhiteSpace(invoiceNumber))
                                    {
                                        income.InvoiceNumber = invoiceNumber;
                                        hasAnyValidField = true;
                                    }
                                    else
                                    {
                                        rowErrors.Add("Номер счета-фактуры пуст, установлен как null.");
                                    }
                                }

                                // PaidAmount
                                if (headers.ContainsKey("PaidAmount") && decimal.TryParse(worksheet.Cell(row, headers["PaidAmount"]).GetString(), out decimal paidAmount))
                                {
                                    if (paidAmount >= 0)
                                    {
                                        income.PaidAmount = paidAmount;
                                        hasAnyValidField = true;
                                    }
                                    else
                                    {
                                        rowErrors.Add("Оплаченная сумма отрицательная, установлена как 0.");
                                    }
                                }

                                // UserID
                                if (headers.ContainsKey("UserID") && int.TryParse(worksheet.Cell(row, headers["UserID"]).GetString(), out int userID))
                                {
                                    income.UserID = userID;
                                    hasAnyValidField = true;
                                }

                                // BatchID
                                if (headers.ContainsKey("BatchID") && int.TryParse(worksheet.Cell(row, headers["BatchID"]).GetString(), out int batchID))
                                {
                                    income.BatchID = batchID;
                                    hasAnyValidField = true;
                                }

                                // Простая валидация
                                bool isValid = hasAnyValidField; // Хотя бы одно корректное поле
                                if (isValid)
                                {
                                    // Проверка минимальных требований
                                    if (income.Quantity < 0)
                                    {
                                        income.Quantity = 0;
                                        rowErrors.Add("Количество было отрицательным, установлено как 0.");
                                    }
                                    if (income.UnitPrice < 0)
                                    {
                                        income.UnitPrice = 0;
                                        rowErrors.Add("Цена за единицу была отрицательной, установлена как 0.");
                                    }
                                    if (income.PaidAmount < 0)
                                    {
                                        income.PaidAmount = 0;
                                        rowErrors.Add("Оплаченная сумма была отрицательной, установлена как 0.");
                                    }
                                    if (income.Markup < 0)
                                    {
                                        income.Markup = 0;
                                        rowErrors.Add("Наценка была отрицательной, установлена как 0.");
                                    }

                                    _dataSource.Add(income);
                                    hasValidData = true;
                                    if (rowErrors.Any())
                                    {
                                        errors.Add($"Строка {row}: {string.Join("; ", rowErrors)}");
                                    }
                                }
                                else
                                {
                                    errors.Add($"Строка {row}: Не найдено корректных данных.");
                                }
                            }

                            SaveTempData();
                            RefreshDataGridView();
                            UpdateSaveButtonState();

                            if (hasValidData)
                            {
                                if (errors.Any())
                                {
                                    ShowError($"Некоторые строки не были импортированы:\n{string.Join("\n", errors)}");
                                }
                                else
                                {
                                    ShowSuccess("Данные успешно импортированы!");
                                }
                            }
                            else
                            {
                                ShowError("Ни одна строка не была импортирована:\n" + string.Join("\n", errors));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "при импорте данных из Excel");
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

                try
                {
                    Size ActionIconSize = new(24, 24);
                    var menu = new ContextMenuStrip
                    {
                        ImageScalingSize = ActionIconSize,
                        Renderer = new CustomToolStripRenderer()
                    };

                    var editItem = new ToolStripMenuItem
                    {
                        Text = "Редактировать",
                        Image = _actionImageList.Images[9],
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
                        Image = _actionImageList.Images[8],
                        ImageAlign = ContentAlignment.MiddleLeft,
                        TextImageRelation = TextImageRelation.ImageBeforeText,
                        Size = new Size(0, 32),
                        Tag = "Delete"
                    };
                    deleteItem.Click += (s, ev) => DeleteIncome(income);
                    menu.Items.Add(deleteItem);

                    menu.Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
                }
                catch (Exception ex)
                {
                    LogAndShowError(ex, "выполнении действия");
                }
            }
        }

        private class CustomToolStripRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                var item = e.Item as ToolStripMenuItem;
                if (item != null)
                {
                    Color backgroundColor = item.Selected ? Color.FromArgb(200, 230, 255) : Color.White;
                    if (item.Tag?.ToString() == "Edit")
                        backgroundColor = item.Selected ? Color.FromArgb(50, 140, 230) : Color.FromArgb(25, 118, 210);
                    else if (item.Tag?.ToString() == "Delete")
                        backgroundColor = item.Selected ? Color.FromArgb(255, 77, 77) : Color.FromArgb(220, 53, 69);

                    using (var brush = new SolidBrush(backgroundColor))
                    {
                        e.Graphics.FillRectangle(brush, new Rectangle(0, 0, e.Item.Width, e.Item.Height));
                    }
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = e.Item.Selected ? Color.Black : Color.White;
                base.OnRenderItemText(e);
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

                        var current = CurrentUser.Instance;
                        var user = current.IsAuthenticated ? current.User : null;
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
                        income.UserID = user?.UserID ?? 0;
                        income.UserFullName = user?.FullName ?? "Текущий пользователь";
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
            try
            {
                using (var dialog = new Form
                {
                    Text = "Подтверждение удаления",
                    ClientSize = new Size(400, 142),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    BackColor = Color.FromArgb(245, 245, 245)
                })
                {
                    var tableLayoutPanel = new TableLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        RowCount = 2,
                        ColumnCount = 2,
                        Padding = new Padding(16),
                        BackColor = Color.FromArgb(245, 245, 245)
                    };
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

                    var lblMessage = new Label
                    {
                        Text = $"Вы хотите удалить поступление #{income.IncomeID} ({income.PartName})?",
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.FromArgb(33, 37, 41),
                        AutoSize = true,
                        Location = new Point(19, 16),
                        TextAlign = ContentAlignment.MiddleLeft
                    };

                    var btnConfirm = new Button
                    {
                        Text = "Да",
                        Size = new Size(100, 36),
                        BackColor = Color.FromArgb(220, 53, 69),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(255, 100, 100) },
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        Location = new Point(dialog.ClientSize.Width - 110, dialog.ClientSize.Height - 46), // O'ng chetdan 10px
                        Anchor = AnchorStyles.Top | AnchorStyles.Right
                    };
                    btnConfirm.Click += (s, ev) =>
                    {
                        _dataSource.Remove(income);
                        SaveTempData();
                        RefreshDataGridView();
                        ShowSuccess("Запись успешно удалена!");
                        dialog.Close();
                    };

                    var btnCancel = new Button
                    {
                        Text = "Нет",
                        Size = new Size(100, 36),
                        BackColor = Color.FromArgb(25, 118, 210),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(50, 140, 230) },
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        Location = new Point(10, dialog.ClientSize.Height - 46), // Chap chetdan 10px
                        Anchor = AnchorStyles.Top | AnchorStyles.Left
                    };
                    btnCancel.Click += (s, ev) => dialog.Close();

                    tableLayoutPanel.Controls.Add(lblMessage, 0, 0);
                    tableLayoutPanel.SetColumnSpan(lblMessage, 2);
                    dialog.Controls.Add(btnConfirm);
                    dialog.Controls.Add(btnCancel);
                    dialog.Controls.Add(tableLayoutPanel);
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "удалении записи");
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
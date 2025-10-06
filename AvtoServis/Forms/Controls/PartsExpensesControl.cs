using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.Forms.Controls;
using AvtoServis.Model.DTOs;
using AvtoServis.ViewModels.Screens;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Size = System.Drawing.Size;

namespace AvtoServis.Forms.Controls
{
    public partial class PartsExpensesControl : UserControl
    {
        private readonly PartExpensesViewModel _viewModel;
        private readonly IFullPartsRepository _partsRepository;
        private readonly IPartsExpensesRepository _partsExpensesRepository;
        private readonly IPartsIncomeRepository _partsIncomeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IStatusRepository _statusRepository;
        private string _connectionString;
        private List<PartExpenseDto> _dataSource;
        private readonly System.Windows.Forms.Timer _searchTimer;
        private readonly Dictionary<string, bool> _columnVisibility;
        private string _sortColumn = "SaleId";
        private SortOrder _sortOrder = SortOrder.Ascending;

        public PartsExpensesControl(PartExpensesViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _dataSource = new List<PartExpenseDto>();
            this._connectionString = _viewModel.ConnectionString;
            _partsRepository = new FullPartsRepository(_connectionString);
            _partsExpensesRepository = new PartsExpensesRepository(_connectionString);
            _customerRepository = new CustomerRepository(_connectionString);
            _partsIncomeRepository = new PartsIncomeRepository(_connectionString);
            _statusRepository = new StatusRepository(_connectionString);
            _columnVisibility = new Dictionary<string, bool>
            {
                { "SaleId", true }, // Muhim
                { "PartName", true }, // Muhim
                { "Quantity", true }, // Muhim
                { "UnitPrice", true }, // Muhim
                { "TotalAmount", true }, // Muhim
                { "PaymentStatusId", true }, // Muhim
                { "SaleDate", true }, // Muhim
                {"PaidAmount", true }, // Doimiy ko‘rinadi" }
                { "Manufacturer", false },
                { "CustomerName", false },
                { "CustomerPhone", false },
                { "CatalogNumber", false },
                { "CarBrand", false },
                { "Status", false },
                { "Seller", false },
                { "InvoiceNumber", false },
                { "Action", true } // Doimiy ko‘rinadi
            };
            _searchTimer = new System.Windows.Forms.Timer { Interval = 300 };
            InitializeComponent();
            ConfigureColumns();
            EnhanceVisualStyles();
            InitializeSearch();
            OptimizeDataGridView();
            LoadData();
            UpdateVisibleColumns();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Основная панель управления продажами деталей");
            toolTip.SetToolTip(titleLabel, "Заголовок раздела продаж деталей");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(searchBox, "Введите текст для поиска по видимым столбцам");
            toolTip.SetToolTip(buttonPanel, "Панель с кнопками управления");
            toolTip.SetToolTip(btnColumns, "Выбрать видимые столбцы таблицы");
            toolTip.SetToolTip(btnOpenFilterDialog, "Открыть окно фильтров");
            toolTip.SetToolTip(btnExport, "Экспортировать данные в Excel");
            toolTip.SetToolTip(btnProdat, "Инициировать продажу детали");
            toolTip.SetToolTip(countLabel, "Количество отображаемых записей");
            toolTip.SetToolTip(dataGridView, "Таблица с данными о продажах деталей");
        }

        private void UpdateVisibleColumns()
        {
            _viewModel.VisibleColumns = _columnVisibility
                .Where(kvp => kvp.Value && kvp.Key != "Action")
                .Select(kvp => kvp.Key)
                .ToList();
        }

        private void ConfigureColumns()
        {
            dataGridView.Columns.Clear();
            var columnMapping = new Dictionary<string, string>
            {
                { "SaleId", "ID" },
                { "PartName", "Название детали" },
                { "Quantity", "Количество" },
                { "UnitPrice", "Цена за единицу" },
                { "TotalAmount", "Общая сумма" },
                { "PaymentStatusId", "Статус оплаты" },
                { "SaleDate", "Дата продажи" },
                { "PaidAmount", "Оплаченная сумма" },
                { "Manufacturer", "Производитель" },
                { "CustomerName", "Имя клиента" },
                { "CustomerPhone", "Телефон клиента" },
                { "CatalogNumber", "Каталожный номер" },
                { "CarBrand", "Марка автомобиля" },
                { "Status", "Статус" },
                { "Seller", "Продавец" },
                { "InvoiceNumber", "Номер счета" },
                { "Action", "Действия" }
            };

            foreach (var column in _columnVisibility)
            {
                if (column.Key == "Action")
                {
                    var col = new DataGridViewButtonColumn
                    {
                        Name = column.Key,
                        HeaderText = columnMapping[column.Key],
                        Text = "...",
                        UseColumnTextForButtonValue = true,
                        ReadOnly = true,
                        DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 10F, FontStyle.Bold) },
                        Width = 80
                    };
                    dataGridView.Columns.Add(col);
                }
                else if (column.Value)
                {
                    var col = new DataGridViewTextBoxColumn
                    {
                        Name = column.Key,
                        HeaderText = columnMapping[column.Key],
                        DataPropertyName = column.Key,
                        ReadOnly = true,
                        DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                    };
                    if (column.Key == "SaleId" || column.Key == "Quantity")
                        col.Width = 80;
                    else if (column.Key == "CarBrand" || column.Key == "Manufacturer" || column.Key == "Seller")
                        col.Width = 120;
                    else if (column.Key == "PaymentStatusId" || column.Key == "Status")
                        col.Width = 150;
                    else
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns.Add(col);
                }
            }

            dataGridView.AutoGenerateColumns = false;
        }

        private void EnhanceVisualStyles()
        {
            btnColumns.BackColor = Color.FromArgb(25, 118, 210);
            btnColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnOpenFilterDialog.BackColor = Color.FromArgb(25, 118, 210);
            btnOpenFilterDialog.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnExport.BackColor = Color.FromArgb(25, 118, 210);
            btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnProdat.BackColor = Color.FromArgb(40, 167, 69); // Yashil rang
            btnProdat.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);

            dataGridView.BackgroundColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 243, 245);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void InitializeSearch()
        {
            _searchTimer.Tick += SearchTimer_Tick;
            searchBox.TextChanged += SearchBox_TextChanged;
            searchBox.KeyDown += SearchBox_KeyDown;
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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //using (var dialog = new PartExpenseDetailsDialog(_viewModel)) // ID yo‘q, yangi sotuv
                //{
                //    dialog.ShowDialog();
                //}
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel файлы (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = $"Продажи_деталей_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
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

        private void BtnProdat_Click(object sender, EventArgs e)
        {
            try
            {
                var viewModel = new SaleViewModel(_partsRepository, _partsExpensesRepository, _partsIncomeRepository, _statusRepository);

                // SaleForm ochish
                using (var saleForm = new SaleForm(viewModel, _customerRepository))
                {
                    saleForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "выполнении продажи");
            }
        }

        private void BtnColumns_Click(object sender, EventArgs e)
        {
            try
            {
                //using (var dialog = new PartExpenseDetailsDialog(_viewModel)) // ID yo‘q
                //{
                //    dialog.ShowDialog();
                //}
                using (var form = new Form
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
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "SaleId", "ID" },
                        { "PartName", "Название детали" },
                        { "Quantity", "Количество" },
                        { "UnitPrice", "Цена за единицу" },
                        { "TotalAmount", "Общая сумма" },
                        { "PaymentStatusId", "Статус оплаты" },
                        { "SaleDate", "Дата продажи" },
                        { "PaidAmount", "Оплаченная сумма" },
                        { "Manufacturer", "Производитель" },
                        { "CustomerName", "Имя клиента" },
                        { "CustomerPhone", "Телефон клиента" },
                        { "CatalogNumber", "Каталожный номер" },
                        { "CarBrand", "Марка автомобиля" },
                        { "Status", "Статус" },
                        { "Seller", "Продавец" },
                        { "InvoiceNumber", "Номер счета" }
                    };
                    foreach (var column in _columnVisibility.Where(c => c.Key != "Action"))
                    {
                        checkedListBox.Items.Add(new { Name = column.Key, DisplayName = columnMapping[column.Key] }, column.Value);
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
                        if (visibleCount == 0 && !_columnVisibility["Action"])
                        {
                            MessageBox.Show("Необходимо выбрать хотя бы один столбец!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        foreach (var column in _columnVisibility.Keys.ToList())
                        {
                            if (column != "Action")
                                _columnVisibility[column] = false;
                        }
                        foreach (var item in checkedListBox.CheckedItems)
                        {
                            var col = (dynamic)item;
                            _columnVisibility[col.Name] = true;
                        }
                        ConfigureColumns();
                        UpdateVisibleColumns();
                        RefreshDataGridView();
                        form.Close();
                    };

                    form.Controls.Add(checkedListBox);
                    form.Controls.Add(btnOk);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "открытии выбора столбцов");
            }
        }

        private void BtnOpenFilterDialog_Click(object sender, EventArgs e)
        {
            try
            {
                //using (var dialog = new PartExpenseDetailsDialog(_viewModel)) // ID yo‘q
                //{
                //    dialog.ShowDialog();
                //}
                //UpdateVisibleColumns();
                using (var dialog = new PartExpensesFilterDialog(_viewModel))
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
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView.Columns[e.ColumnIndex].Name == "Action")
            {
                int id = (int)dataGridView.Rows[e.RowIndex].Tag;
                try
                {
                    var menu = new ContextMenuStrip
                    {
                        Renderer = new CustomToolStripRenderer()
                    };

                    var detailsItem = new ToolStripMenuItem
                    {
                        Text = "Подробнее",
                        Tag = "Details"
                    };
                    detailsItem.Click += (s, ev) =>
                    {
                        using (var dialog = new PartExpenseDetailsDialog(_viewModel, id))
                        {
                            dialog.ShowDialog();
                        }
                    };
                    menu.Items.Add(detailsItem);

                    var editItem = new ToolStripMenuItem
                    {
                        Text = "Редактировать",
                        Tag = "Edit"
                    };
                    editItem.Click += (s, ev) =>
                    {
                        try
                        {
                            _viewModel.OpenPartExpenseEditForm(id, ParentForm);
                            LoadData(); // Yangilashdan so'ng ro'yxatni qayta yuklash
                        }
                        catch (Exception ex)
                        {
                            LogAndShowError(ex, "открытии формы редактирования");
                        }
                    };
                    menu.Items.Add(editItem);

                    var returnItem = new ToolStripMenuItem
                    {
                        Text = "Возврат",
                        Tag = "Return"
                    };
                    returnItem.Click += (s, ev) =>
                    {
                        if (MessageBox.Show("Вы уверены, что хотите отменить эту продажу запчасти? Это действие нельзя отменить.", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            try
                            {
                            
                                _viewModel.ReturnPartExpense(id);
                                LoadData(); // Ma'lumotlarni yangilash
                                MessageBox.Show("Продажа запчасти успешно отменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                LogAndShowError(ex, "отмене продажи запчасти");
                            }
                        }
                    };
                    menu.Items.Add(returnItem);

                    menu.Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
                }
                catch (Exception ex)
                {
                    LogAndShowError(ex, "выполнении действия");
                }
            }
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var column = dataGridView.Columns[e.ColumnIndex].Name;
            if (column == "Action") return;
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

        public void LoadData()
        {
            try
            {
                _dataSource = _viewModel.LoadPartExpenses();
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "загрузке данных");
            }
        }

        private void PerformSearch()
        {
            try
            {
                if (searchBox.Text == "Поиск..." || string.IsNullOrWhiteSpace(searchBox.Text))
                {
                    _dataSource = _viewModel.LoadPartExpenses();
                }
                else
                {
                    _dataSource = _viewModel.SearchPartExpenses(searchBox.Text.Trim(), _viewModel.VisibleColumns);
                }
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "поиске");
            }
        }

        public void ApplyFilters(List<(string Column, string Operator, string SearchText)> filters)
        {
            try
            {
                _viewModel.Filters = filters;
                _dataSource = _viewModel.LoadPartExpenses();
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
            switch (_sortColumn)
            {
                case "SaleId":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.SaleId.CompareTo(y.SaleId) : y.SaleId.CompareTo(x.SaleId));
                    break;
                case "PartName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.PartName, y.PartName) : string.Compare(y.PartName, x.PartName));
                    break;
                case "Quantity":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.Quantity.CompareTo(y.Quantity) : y.Quantity.CompareTo(x.Quantity));
                    break;
                case "UnitPrice":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.UnitPrice.CompareTo(y.UnitPrice) : y.UnitPrice.CompareTo(x.UnitPrice));
                    break;
                case "TotalAmount":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.TotalAmount.CompareTo(y.TotalAmount) : y.TotalAmount.CompareTo(x.TotalAmount));
                    break;
                case "PaymentStatusId":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? (x.PaymentStatusId ?? 0).CompareTo(y.PaymentStatusId ?? 0) : (y.PaymentStatusId ?? 0).CompareTo(x.PaymentStatusId ?? 0));
                    break;
                case "SaleDate":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.SaleDate.CompareTo(y.SaleDate) : y.SaleDate.CompareTo(x.SaleDate));
                    break;
                case "PaidAmount":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.PaidAmount.CompareTo(y.PaidAmount) : y.PaidAmount.CompareTo(x.PaidAmount));
                    break;
                case "Manufacturer":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Manufacturer, y.Manufacturer) : string.Compare(y.Manufacturer, x.Manufacturer));
                    break;
                case "CustomerName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.CustomerName, y.CustomerName) : string.Compare(y.CustomerName, x.CustomerName));
                    break;
                case "CustomerPhone":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.CustomerPhone, y.CustomerPhone) : string.Compare(y.CustomerPhone, x.CustomerPhone));
                    break;
                case "CatalogNumber":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.CatalogNumber, y.CatalogNumber) : string.Compare(y.CatalogNumber, x.CatalogNumber));
                    break;
                case "CarBrand":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.CarBrand, y.CarBrand) : string.Compare(y.CarBrand, x.CarBrand));
                    break;
                case "Status":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Status, y.Status) : string.Compare(y.Status, x.Status));
                    break;
                case "Seller":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Seller, y.Seller) : string.Compare(y.Seller, x.Seller));
                    break;
                case "InvoiceNumber":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.InvoiceNumber, y.InvoiceNumber) : string.Compare(y.InvoiceNumber, x.InvoiceNumber));
                    break;
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            foreach (var expense in _dataSource)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                if (_columnVisibility["SaleId"])
                    row.Cells[dataGridView.Columns["SaleId"].Index].Value = expense.SaleId;
                if (_columnVisibility["PartName"])
                    row.Cells[dataGridView.Columns["PartName"].Index].Value = expense.PartName ?? "Не указано";
                if (_columnVisibility["Quantity"])
                    row.Cells[dataGridView.Columns["Quantity"].Index].Value = expense.Quantity;
                if (_columnVisibility["UnitPrice"])
                    row.Cells[dataGridView.Columns["UnitPrice"].Index].Value = expense.UnitPrice;
                if (_columnVisibility["TotalAmount"])
                    row.Cells[dataGridView.Columns["TotalAmount"].Index].Value = expense.TotalAmount;
                if (_columnVisibility["PaymentStatusId"])
                {
                    var cell = row.Cells[dataGridView.Columns["PaymentStatusId"].Index];
                    cell.Value = expense.PaymentStatusId switch
                    {
                        1 => "Оплачено",
                        2 => "Не оплачено",
                        3 => "Частично оплачено",
                        _ => "Неизвестно"
                    };
                    cell.Style.ForeColor = expense.PaymentStatusId switch
                    {
                        1 => Color.FromArgb(40, 167, 69),
                        2 => Color.FromArgb(220, 53, 69),
                        3 => Color.FromArgb(255, 193, 7),
                        _ => Color.Black
                    };
                }
                if (_columnVisibility["SaleDate"])
                    row.Cells[dataGridView.Columns["SaleDate"].Index].Value = expense.SaleDate == DateTime.MinValue ? "Не указано" : expense.SaleDate.ToString("yyyy-MM-dd");
                if (_columnVisibility["PaidAmount"])
                    row.Cells[dataGridView.Columns["PaidAmount"].Index].Value = expense.PaidAmount;
                if (_columnVisibility["Manufacturer"])
                    row.Cells[dataGridView.Columns["Manufacturer"].Index].Value = expense.Manufacturer ?? "Не указано";
                if (_columnVisibility["CustomerName"])
                    row.Cells[dataGridView.Columns["CustomerName"].Index].Value = expense.CustomerName ?? "Не указано";
                if (_columnVisibility["CustomerPhone"])
                    row.Cells[dataGridView.Columns["CustomerPhone"].Index].Value = expense.CustomerPhone ?? "Не указано";
                if (_columnVisibility["CatalogNumber"])
                    row.Cells[dataGridView.Columns["CatalogNumber"].Index].Value = expense.CatalogNumber ?? "Не указано";
                if (_columnVisibility["CarBrand"])
                    row.Cells[dataGridView.Columns["CarBrand"].Index].Value = expense.CarBrand ?? "Не указано";
                if (_columnVisibility["Status"])
                {
                    var cell = row.Cells[dataGridView.Columns["Status"].Index];
                    cell.Value = expense.Status ?? "Не указано";
                    if (!string.IsNullOrEmpty(expense.StatusColor))
                    {
                        try
                        {
                            cell.Style.ForeColor = ColorTranslator.FromHtml(expense.StatusColor);
                        }
                        catch
                        {
                            cell.Style.ForeColor = Color.Black;
                        }
                    }
                }
                if (_columnVisibility["Seller"])
                    row.Cells[dataGridView.Columns["Seller"].Index].Value = expense.Seller ?? "Не указано";
                if (_columnVisibility["InvoiceNumber"])
                    row.Cells[dataGridView.Columns["InvoiceNumber"].Index].Value = expense.InvoiceNumber ?? "Не указано";
                if (_columnVisibility["Action"])
                    row.Cells[dataGridView.Columns["Action"].Index].Value = "...";
                row.Tag = expense.SaleId;
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Записи: {_dataSource.Count}";
            dataGridView.Refresh();
        }

        private void LogAndShowError(Exception ex, string operation)
        {
            System.Diagnostics.Debug.WriteLine($"{operation} Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            MessageBox.Show($"Произошла ошибка при {operation.ToLower()}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    else if (item.Tag?.ToString() == "Return")
                        backgroundColor = item.Selected ? Color.FromArgb(255, 77, 77) : Color.FromArgb(220, 53, 69);
                    else if (item.Tag?.ToString() == "Details")
                        backgroundColor = item.Selected ? Color.FromArgb(60, 187, 89) : Color.FromArgb(40, 167, 69);

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
    }
}
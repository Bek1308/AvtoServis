using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using AvtoServis.Model.DTOs;

namespace AvtoServis.Forms.Controls
{
    public partial class FullPartsControl : UserControl
    {
        private readonly FullPartsViewModel _viewModel;
        private readonly ImageList _actionImageList;
        private List<FullParts> _dataSource;
        private System.Windows.Forms.Timer _searchTimer;
        private readonly Dictionary<string, bool> _columnVisibility;
        private string _sortColumn = "PartID";
        private SortOrder _sortOrder = SortOrder.Ascending;
        private bool _isDialogMode; // Dialog rejimi uchun flag

        public FullPartsControl(FullPartsViewModel viewModel, ImageList actionImageList, bool isDialogMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _actionImageList = actionImageList ?? throw new ArgumentNullException(nameof(actionImageList));
            _dataSource = new List<FullParts>();
            _isDialogMode = isDialogMode;
            _columnVisibility = new Dictionary<string, bool>
            {
                { "PartID", true },
                { "PartName", true },
                { "CatalogNumber", true },
                { "RemainingQuantity", true },
                { "IsAvailable", true },
                { "StockName", true },
                { "IsPlacedInStock", true },
                { "ShelfNumber", true },
                { "CarBrandName", true },
                { "ManufacturerName", true },
                { "QualityName", true },
                { "Characteristics", true },
                { "PhotoPath", false },
                { "IncomeQuantity", false },
                { "IncomeUnitPrice", false },
                { "Markup", false },
                { "IncomeDate", false },
                { "IncomeInvoiceNumber", false },
                { "IncomePaidAmount", false },
                { "SupplierName", false },
                { "BatchName", false },
                { "FinanceStatusName", false },
                { "IncomeStatusName", false },
                { "AttributeName", false },
                { "AttributeValue", false }
            };
            InitializeComponent();
            ConfigureColumns();
            EnhanceVisualStyles();
            InitializeSearch();
            OptimizeDataGridView();
            LoadData();
            UpdateVisibleColumns();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetToolTips();

            if (_isDialogMode)
            {
                btnExport.Visible = false; // Dialog rejimida Export tugmasini yashirish
            }
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Основная панель управления деталями");
            toolTip.SetToolTip(titleLabel, "Заголовок раздела деталей");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(searchBox, "Введите текст для поиска по видимым столбцам");
            toolTip.SetToolTip(buttonPanel, "Панель с кнопками управления");
            toolTip.SetToolTip(btnColumns, "Выбрать видимые столбцы таблицы");
            toolTip.SetToolTip(btnOpenFilterDialog, "Открыть окно фильтров");
            if (!_isDialogMode)
            {
                toolTip.SetToolTip(btnExport, "Экспортировать данные в Excel");
            }
            toolTip.SetToolTip(countLabel, "Количество отображаемых деталей");
            toolTip.SetToolTip(dataGridView, _isDialogMode ? "Дважды щелкните, чтобы выбрать товар" : "Таблица с данными о деталях");
        }
        public void SetVisibleColumns(string[] visibleColumns)
        {
            try
            {
                foreach (var column in _columnVisibility.Keys.ToList())
                {
                    _columnVisibility[column] = visibleColumns.Contains(column);
                }
                ConfigureColumns();
                UpdateVisibleColumns();
                RefreshDataGridView();
                System.Diagnostics.Debug.WriteLine($"SetVisibleColumns: Columns set to {string.Join(", ", visibleColumns)}");
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "настройке видимых столбцов");
            }
        }

        private void UpdateVisibleColumns()
        {
            _viewModel.VisibleColumns = _columnVisibility
                .Where(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .ToList();
        }

        private void ConfigureColumns()
        {
            dataGridView.Columns.Clear();
            var columnMapping = new Dictionary<string, string>
            {
                { "PartID", "ID" },
                { "PartName", "Название детали" },
                { "CatalogNumber", "Каталожный номер" },
                { "RemainingQuantity", "Остаток" },
                { "IsAvailable", "В наличии" },
                { "StockName", "Склад" },
                { "IsPlacedInStock", "Размещено" },
                { "ShelfNumber", "Номер полки" },
                { "CarBrandName", "Марка" },
                { "ManufacturerName", "Производитель" },
                { "QualityName", "Качество" },
                { "Characteristics", "Характеристики" },
                { "PhotoPath", "Путь к фото" },
                { "IncomeQuantity", "Количество в приходе" },
                { "IncomeUnitPrice", "Цена за единицу" },
                { "Markup", "Наценка" },
                { "IncomeDate", "Дата прихода" },
                { "IncomeInvoiceNumber", "Номер счета" },
                { "IncomePaidAmount", "Оплаченная сумма" },
                { "SupplierName", "Поставщик" },
                { "BatchName", "Партия" },
                { "FinanceStatusName", "Финансовый статус" },
                { "IncomeStatusName", "Статус прихода" },
                { "AttributeName", "Имя атрибута" },
                { "AttributeValue", "Значение атрибута" }
            };

            foreach (var column in _columnVisibility)
            {
                if (column.Value)
                {
                    var col = new DataGridViewTextBoxColumn
                    {
                        Name = column.Key,
                        HeaderText = columnMapping[column.Key],
                        DataPropertyName = column.Key,
                        ReadOnly = true,
                        DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                    };
                    if (column.Key == "PartID" || column.Key == "RemainingQuantity" || column.Key == "IncomeQuantity")
                        col.Width = 80;
                    else if (column.Key == "CarBrandName" || column.Key == "ManufacturerName" || column.Key == "QualityName")
                        col.Width = 120;
                    else if (column.Key == "IsAvailable" || column.Key == "IsPlacedInStock")
                        col.Width = 100;
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
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = $"FullParts_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
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
                var columnMapping = new Dictionary<string, string>
                {
                    { "PartID", "ID" },
                    { "PartName", "Название детали" },
                    { "CatalogNumber", "Каталожный номер" },
                    { "RemainingQuantity", "Остаток" },
                    { "IsAvailable", "В наличии" },
                    { "StockName", "Склад" },
                    { "IsPlacedInStock", "Размещено" },
                    { "ShelfNumber", "Номер полки" },
                    { "CarBrandName", "Марка" },
                    { "ManufacturerName", "Производитель" },
                    { "QualityName", "Качество" },
                    { "Characteristics", "Характеристики" },
                    { "PhotoPath", "Путь к фото" },
                    { "IncomeQuantity", "Количество в приходе" },
                    { "IncomeUnitPrice", "Цена за единицу" },
                    { "Markup", "Наценка" },
                    { "IncomeDate", "Дата прихода" },
                    { "IncomeInvoiceNumber", "Номер счета" },
                    { "IncomePaidAmount", "Оплаченная сумма" },
                    { "SupplierName", "Поставщик" },
                    { "BatchName", "Партия" },
                    { "FinanceStatusName", "Финансовый статус" },
                    { "IncomeStatusName", "Статус прихода" },
                    { "AttributeName", "Имя атрибута" },
                    { "AttributeValue", "Значение атрибута" }
                };
                foreach (var column in _columnVisibility)
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
                    if (visibleCount == 0)
                    {
                        MessageBox.Show("Необходимо выбрать хотя бы один столбец!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    foreach (var column in _columnVisibility.Keys.ToList())
                    {
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
                using (var dialog = new FullPartsFilterDialog(_viewModel))
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
            if (_isDialogMode || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int id = (int)dataGridView.Rows[e.RowIndex].Tag;

            try
            {
                using (var dialog = new PartFullDetailsDialog(_viewModel, id))
                {
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "открытии подробной информации");
            }
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var column = dataGridView.Columns[e.ColumnIndex].Name;
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
                _dataSource = _viewModel.LoadParts();
                SortDataSource();
                RefreshDataGridView();
                System.Diagnostics.Debug.WriteLine($"LoadData: DataGridView refreshed with {_dataSource.Count} rows.");
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
                    _dataSource = _viewModel.LoadParts();
                }
                else
                {
                    _dataSource = _viewModel.SearchParts(searchBox.Text.Trim(), _viewModel.VisibleColumns);
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
                _dataSource = _viewModel.LoadParts();
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
                case "PartID":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.PartID.CompareTo(y.PartID) : y.PartID.CompareTo(x.PartID));
                    break;
                case "PartName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.PartName, y.PartName) : string.Compare(y.PartName, x.PartName));
                    break;
                case "CatalogNumber":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.CatalogNumber, y.CatalogNumber) : string.Compare(y.CatalogNumber, x.CatalogNumber));
                    break;
                case "RemainingQuantity":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.RemainingQuantity.CompareTo(y.RemainingQuantity) : y.RemainingQuantity.CompareTo(x.RemainingQuantity));
                    break;
                case "IsAvailable":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.IsAvailable.CompareTo(y.IsAvailable) : y.IsAvailable.CompareTo(x.IsAvailable));
                    break;
                case "StockName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.StockName, y.StockName) : string.Compare(y.StockName, x.StockName));
                    break;
                case "IsPlacedInStock":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.IsPlacedInStock.CompareTo(y.IsPlacedInStock) : y.IsPlacedInStock.CompareTo(x.IsPlacedInStock));
                    break;
                case "ShelfNumber":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.ShelfNumber, y.ShelfNumber) : string.Compare(y.ShelfNumber, x.ShelfNumber));
                    break;
                case "CarBrandName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.CarBrandName, y.CarBrandName) : string.Compare(y.CarBrandName, x.CarBrandName));
                    break;
                case "ManufacturerName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.ManufacturerName, y.ManufacturerName) : string.Compare(y.ManufacturerName, x.ManufacturerName));
                    break;
                case "QualityName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.QualityName, y.QualityName) : string.Compare(y.QualityName, x.QualityName));
                    break;
                case "Characteristics":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Characteristics, y.Characteristics) : string.Compare(y.Characteristics, x.Characteristics));
                    break;
                case "PhotoPath":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.PhotoPath, y.PhotoPath) : string.Compare(y.PhotoPath, x.PhotoPath));
                    break;
                case "IncomeQuantity":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? (x.IncomeQuantity ?? 0).CompareTo(y.IncomeQuantity ?? 0) : (y.IncomeQuantity ?? 0).CompareTo(x.IncomeQuantity ?? 0));
                    break;
                case "IncomeUnitPrice":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? (x.IncomeUnitPrice ?? 0).CompareTo(y.IncomeUnitPrice ?? 0) : (y.IncomeUnitPrice ?? 0).CompareTo(x.IncomeUnitPrice ?? 0));
                    break;
                case "Markup":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? (x.Markup ?? 0).CompareTo(y.Markup ?? 0) : (y.Markup ?? 0).CompareTo(x.Markup ?? 0));
                    break;
                case "IncomeDate":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? (x.IncomeDate ?? DateTime.MinValue).CompareTo(y.IncomeDate ?? DateTime.MinValue) : (y.IncomeDate ?? DateTime.MinValue).CompareTo(x.IncomeDate ?? DateTime.MinValue));
                    break;
                case "IncomeInvoiceNumber":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.IncomeInvoiceNumber, y.IncomeInvoiceNumber) : string.Compare(y.IncomeInvoiceNumber, x.IncomeInvoiceNumber));
                    break;
                case "IncomePaidAmount":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? (x.IncomePaidAmount ?? 0).CompareTo(y.IncomePaidAmount ?? 0) : (y.IncomePaidAmount ?? 0).CompareTo(x.IncomePaidAmount ?? 0));
                    break;
                case "SupplierName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.SupplierName, y.SupplierName) : string.Compare(y.SupplierName, x.SupplierName));
                    break;
                case "BatchName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.BatchName, y.BatchName) : string.Compare(y.BatchName, x.BatchName));
                    break;
                case "FinanceStatusName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.FinanceStatusName, y.FinanceStatusName) : string.Compare(y.FinanceStatusName, x.FinanceStatusName));
                    break;
                case "IncomeStatusName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.IncomeStatusName, y.IncomeStatusName) : string.Compare(y.IncomeStatusName, x.IncomeStatusName));
                    break;
                case "AttributeName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.AttributeName, y.AttributeName) : string.Compare(y.AttributeName, x.AttributeName));
                    break;
                case "AttributeValue":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.AttributeValue, y.AttributeValue) : string.Compare(y.AttributeValue, x.AttributeValue));
                    break;
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            foreach (var part in _dataSource)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                if (_columnVisibility["PartID"])
                    row.Cells[dataGridView.Columns["PartID"]?.Index ?? 0].Value = part.PartID;
                if (_columnVisibility["PartName"])
                    row.Cells[dataGridView.Columns["PartName"]?.Index ?? 0].Value = part.PartName;
                if (_columnVisibility["CatalogNumber"])
                    row.Cells[dataGridView.Columns["CatalogNumber"]?.Index ?? 0].Value = part.CatalogNumber;
                if (_columnVisibility["RemainingQuantity"])
                    row.Cells[dataGridView.Columns["RemainingQuantity"]?.Index ?? 0].Value = part.RemainingQuantity;
                if (_columnVisibility["IsAvailable"])
                {
                    var cell = row.Cells[dataGridView.Columns["IsAvailable"].Index];
                    cell.Value = part.IsAvailable ? "В наличии" : "Нет в наличии";
                    cell.Style.ForeColor = part.IsAvailable ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                }
                if (_columnVisibility["StockName"])
                    row.Cells[dataGridView.Columns["StockName"]?.Index ?? 0].Value = part.StockName;
                if (_columnVisibility["IsPlacedInStock"])
                {
                    var cell = row.Cells[dataGridView.Columns["IsPlacedInStock"].Index];
                    cell.Value = part.IsPlacedInStock ? "Размещено" : "Не размещено";
                    cell.Style.ForeColor = part.IsPlacedInStock ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                }
                if (_columnVisibility["ShelfNumber"])
                    row.Cells[dataGridView.Columns["ShelfNumber"]?.Index ?? 0].Value = part.ShelfNumber;
                if (_columnVisibility["CarBrandName"])
                    row.Cells[dataGridView.Columns["CarBrandName"]?.Index ?? 0].Value = part.CarBrandName;
                if (_columnVisibility["ManufacturerName"])
                    row.Cells[dataGridView.Columns["ManufacturerName"]?.Index ?? 0].Value = part.ManufacturerName;
                if (_columnVisibility["QualityName"])
                    row.Cells[dataGridView.Columns["QualityName"]?.Index ?? 0].Value = part.QualityName;
                if (_columnVisibility["Characteristics"])
                    row.Cells[dataGridView.Columns["Characteristics"]?.Index ?? 0].Value = part.Characteristics;
                if (_columnVisibility["PhotoPath"])
                    row.Cells[dataGridView.Columns["PhotoPath"]?.Index ?? 0].Value = part.PhotoPath;
                if (_columnVisibility["IncomeQuantity"])
                    row.Cells[dataGridView.Columns["IncomeQuantity"]?.Index ?? 0].Value = part.IncomeQuantity;
                if (_columnVisibility["IncomeUnitPrice"])
                    row.Cells[dataGridView.Columns["IncomeUnitPrice"]?.Index ?? 0].Value = part.IncomeUnitPrice;
                if (_columnVisibility["Markup"])
                    row.Cells[dataGridView.Columns["Markup"]?.Index ?? 0].Value = part.Markup;
                if (_columnVisibility["IncomeDate"])
                    row.Cells[dataGridView.Columns["IncomeDate"]?.Index ?? 0].Value = part.IncomeDate;
                if (_columnVisibility["IncomeInvoiceNumber"])
                    row.Cells[dataGridView.Columns["IncomeInvoiceNumber"]?.Index ?? 0].Value = part.IncomeInvoiceNumber;
                if (_columnVisibility["IncomePaidAmount"])
                    row.Cells[dataGridView.Columns["IncomePaidAmount"]?.Index ?? 0].Value = part.IncomePaidAmount;
                if (_columnVisibility["SupplierName"])
                    row.Cells[dataGridView.Columns["SupplierName"]?.Index ?? 0].Value = part.SupplierName;
                if (_columnVisibility["BatchName"])
                    row.Cells[dataGridView.Columns["BatchName"]?.Index ?? 0].Value = part.BatchName;
                if (_columnVisibility["FinanceStatusName"])
                    row.Cells[dataGridView.Columns["FinanceStatusName"]?.Index ?? 0].Value = part.FinanceStatusName;
                if (_columnVisibility["IncomeStatusName"])
                    row.Cells[dataGridView.Columns["IncomeStatusName"]?.Index ?? 0].Value = part.IncomeStatusName;
                if (_columnVisibility["AttributeName"])
                    row.Cells[dataGridView.Columns["AttributeName"]?.Index ?? 0].Value = part.AttributeName;
                if (_columnVisibility["AttributeValue"])
                    row.Cells[dataGridView.Columns["AttributeValue"]?.Index ?? 0].Value = part.AttributeValue;
                row.Tag = part.PartID;
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Детали: {_dataSource.Count}";
            dataGridView.Refresh();
        }

        private void LogAndShowError(Exception ex, string operation)
        {
            System.Diagnostics.Debug.WriteLine($"{operation} Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            MessageBox.Show($"Произошла ошибка при {operation.ToLower()}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
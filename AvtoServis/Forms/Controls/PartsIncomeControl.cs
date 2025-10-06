using AvtoServis.Forms.Modals.PartsIncome;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class PartsIncomeControl : UserControl
    {
        private readonly PartsIncomeViewModel _viewModel;
        private readonly ImageList _actionImageList;
        private List<PartsIncome> _dataSource;
        private System.Windows.Forms.Timer _searchTimer;
        private readonly Dictionary<string, bool> _columnVisibility;
        private string _sortColumn = "IncomeID";
        private SortOrder _sortOrder = SortOrder.Ascending;

        public PartsIncomeControl(PartsIncomeViewModel viewModel, ImageList actionImageList)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _actionImageList = actionImageList ?? throw new ArgumentNullException(nameof(actionImageList));
            _dataSource = new List<PartsIncome>();
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
                //{ "FinanceStatusName", true },
                { "OperationID", true },
                { "StockName", true },
                { "InvoiceNumber", true },
                { "UserFullName", true },
                { "PaidAmount", true },
                { "BatchName", true },
                { "Actions", true }
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
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Основная панель управления поступлениями деталей");
            toolTip.SetToolTip(titleLabel, "Заголовок раздела поступлений деталей");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(searchBox, "Введите текст для поиска по видимым столбцам");
            toolTip.SetToolTip(buttonPanel, "Панель с кнопками управления");
            toolTip.SetToolTip(addButton, "Добавить новое поступление");
            toolTip.SetToolTip(btnColumns, "Выбрать видимые столбцы таблицы");
            toolTip.SetToolTip(btnOpenFilterDialog, "Открыть окно фильтров");
            toolTip.SetToolTip(btnExport, "Экспортировать данные в Excel");
            toolTip.SetToolTip(countLabel, "Количество отображаемых поступлений");
            toolTip.SetToolTip(dataGridView, "Таблица с данными о поступлениях деталей");
        }

        private void UpdateVisibleColumns()
        {
            _viewModel.VisibleColumns = _columnVisibility
                .Where(kvp => kvp.Value && kvp.Key != "Actions")
                .Select(kvp => kvp.Key)
                .ToList();
        }

        private void ConfigureColumns()
        {
            dataGridView.Columns.Clear();
            var columnDefinitions = new List<(string Name, string HeaderText, string DataPropertyName)>
            {
                ("IncomeID", "ID поступления", "IncomeID"),
                ("PartName", "Название детали", "PartName"),
                ("SupplierName", "Поставщик", "SupplierName"),
                ("Date", "Дата", "Date"),
                ("Quantity", "Количество", "Quantity"),
                ("UnitPrice", "Цена за единицу", "UnitPrice"),
                ("Markup", "Наценка", "Markup"),
                ("StatusName", "Статус", "StatusName"),
                //("FinanceStatusName", "Фин. статус", "Finance_Status_Name"),
                ("OperationID", "ID операции", "OperationID"),
                ("StockName", "Склад", "StockName"),
                ("InvoiceNumber", "Номер счета", "InvoiceNumber"),
                ("UserFullName", "Пользователь", "UserFullName"),
                ("PaidAmount", "Оплаченная сумма", "PaidAmount"),
                ("BatchName", "Партия", "BatchName")
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
        }

        private void EnhanceVisualStyles()
        {
            addButton.BackColor = Color.FromArgb(40, 167, 69);
            addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new PartsIncomeForm(_viewModel, _actionImageList))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                       
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "открытии диалога");
            }
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
                    { "IncomeID", "ID поступления" },
                    { "PartName", "Название детали" },
                    { "SupplierName", "Поставщик" },
                    { "Date", "Дата" },
                    { "Quantity", "Количество" },
                    { "UnitPrice", "Цена за единицу" },
                    { "Markup", "Наценка" },
                    { "StatusName", "Статус" },
                    //{ "FinanceStatusName", "Фин. статус" },
                    { "OperationID", "ID операции" },
                    { "StockName", "Склад" },
                    { "InvoiceNumber", "Номер счета" },
                    { "UserFullName", "Пользователь" },
                    { "PaidAmount", "Оплаченная сумма" },
                    { "BatchName", "Партия" }
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
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int id = (int)dataGridView.Rows[e.RowIndex].Tag;

            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == "Actions")
                {
                    Size ActionIconSize = new(24, 24);
                    var menu = new ContextMenuStrip
                    {
                        ImageScalingSize = ActionIconSize,
                        Renderer = new CustomToolStripRenderer()
                    };

                    var detailsItem = new ToolStripMenuItem
                    {
                        Text = "Подробнее",
                        Image = _actionImageList.Images[10],
                        ImageAlign = ContentAlignment.MiddleLeft,
                        TextImageRelation = TextImageRelation.ImageBeforeText,
                        Size = new Size(0, 32),
                        Tag = "Details"
                    };
                    detailsItem.Click += (s, ev) =>
                    {
                        //using (var dialog = new PartsIncomeDetailsDialog(_viewModel, id))
                        //{
                        //    dialog.ShowDialog();
                        //}
                    };
                    menu.Items.Add(detailsItem);

                    var editItem = new ToolStripMenuItem
                    {
                        Text = "Редактировать",
                        Image = _actionImageList.Images[9],
                        ImageAlign = ContentAlignment.MiddleLeft,
                        TextImageRelation = TextImageRelation.ImageBeforeText,
                        Size = new Size(0, 32),
                        Tag = "Edit"
                    };
                    editItem.Click += (s, ev) => ShowDialog(id);
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
                    deleteItem.Click += (s, ev) =>
                    {
                        using (var dialog = new PartsIncomeDeleteDialog(_viewModel, id))
                        {
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                LoadData();
                            }
                        }
                    };
                    menu.Items.Add(deleteItem);

                    menu.Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "выполнении действия");
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

        public void LoadData()
        {
            try
            {
                _dataSource = _viewModel.LoadPartsIncomes();
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
                    _dataSource = _viewModel.LoadPartsIncomes();
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
                _viewModel.Filters = filters;
                _dataSource = _viewModel.LoadPartsIncomes();
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
                //case "FinanceStatusName":
                //    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Finance_Status_Name, y.Finance_Status_Name) : string.Compare(y.Finance_Status_Name, x.Finance_Status_Name));
                //    break;
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
                case "BatchName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.BatchName, y.BatchName) : string.Compare(y.BatchName, x.BatchName));
                    break;
            }
        }

        public void ShowDialog(int? id)
        {
            try
            {
                using (var dialog = new PartsIncomeDialog(_viewModel, id))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "открытии диалога");
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            foreach (var income in _dataSource)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                if (_columnVisibility.ContainsKey("IncomeID") && _columnVisibility["IncomeID"])
                    row.Cells[dataGridView.Columns["IncomeID"]?.Index ?? 0].Value = income.IncomeID;
                if (_columnVisibility.ContainsKey("PartName") && _columnVisibility["PartName"])
                    row.Cells[dataGridView.Columns["PartName"]?.Index ?? 0].Value = income.PartName;
                if (_columnVisibility.ContainsKey("SupplierName") && _columnVisibility["SupplierName"])
                    row.Cells[dataGridView.Columns["SupplierName"]?.Index ?? 0].Value = income.SupplierName;
                if (_columnVisibility.ContainsKey("Date") && _columnVisibility["Date"])
                    row.Cells[dataGridView.Columns["Date"]?.Index ?? 0].Value = income.Date.ToString("yyyy-MM-dd");
                if (_columnVisibility.ContainsKey("Quantity") && _columnVisibility["Quantity"])
                    row.Cells[dataGridView.Columns["Quantity"]?.Index ?? 0].Value = income.Quantity;
                if (_columnVisibility.ContainsKey("UnitPrice") && _columnVisibility["UnitPrice"])
                    row.Cells[dataGridView.Columns["UnitPrice"]?.Index ?? 0].Value = income.UnitPrice;
                if (_columnVisibility.ContainsKey("Markup") && _columnVisibility["Markup"])
                    row.Cells[dataGridView.Columns["Markup"]?.Index ?? 0].Value = income.Markup;
                if (_columnVisibility.ContainsKey("StatusName") && _columnVisibility["StatusName"])
                    row.Cells[dataGridView.Columns["StatusName"]?.Index ?? 0].Value = income.StatusName;
                //if (_columnVisibility.ContainsKey("FinanceStatusName") && _columnVisibility["FinanceStatusName"])
                //    row.Cells[dataGridView.Columns["FinanceStatusName"]?.Index ?? 0].Value = income.Finance_Status_Name;
                if (_columnVisibility.ContainsKey("OperationID") && _columnVisibility["OperationID"])
                    row.Cells[dataGridView.Columns["OperationID"]?.Index ?? 0].Value = income.OperationID;
                if (_columnVisibility.ContainsKey("StockName") && _columnVisibility["StockName"])
                    row.Cells[dataGridView.Columns["StockName"]?.Index ?? 0].Value = income.StockName;
                if (_columnVisibility.ContainsKey("InvoiceNumber") && _columnVisibility["InvoiceNumber"])
                    row.Cells[dataGridView.Columns["InvoiceNumber"]?.Index ?? 0].Value = income.InvoiceNumber;
                if (_columnVisibility.ContainsKey("UserFullName") && _columnVisibility["UserFullName"])
                    row.Cells[dataGridView.Columns["UserFullName"]?.Index ?? 0].Value = income.UserFullName;
                if (_columnVisibility.ContainsKey("PaidAmount") && _columnVisibility["PaidAmount"])
                    row.Cells[dataGridView.Columns["PaidAmount"]?.Index ?? 0].Value = income.PaidAmount;
                if (_columnVisibility.ContainsKey("BatchName") && _columnVisibility["BatchName"])
                    row.Cells[dataGridView.Columns["BatchName"]?.Index ?? 0].Value = income.BatchName;

                var actionCell = row.Cells[dataGridView.Columns["Actions"].Index];
                actionCell.Value = "...";
                actionCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.Tag = income.IncomeID;
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Поступления: {_dataSource.Count}";
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
                    else if (item.Tag?.ToString() == "Delete")
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
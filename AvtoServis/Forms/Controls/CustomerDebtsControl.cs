using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.Model.DTOs;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace AvtoServis.Forms.Controls
{
    public partial class CustomerDebtsControl : UserControl
    {
        private readonly CustomerDebtsViewModel _viewModel;
        private readonly ICustomerRepository _customerRepository;
        private string _connectionString;
        private List<CustomerDebtInfoDto> _dataSource;
        private readonly System.Windows.Forms.Timer _searchTimer;
        private readonly Dictionary<string, bool> _columnVisibility;
        private string _sortColumn = "CustomerID";
        private SortOrder _sortOrder = SortOrder.Ascending;

        public CustomerDebtsControl(CustomerDebtsViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _dataSource = new List<CustomerDebtInfoDto>();
            //this._connectionString = _viewModel.ConnectionString;
            _customerRepository = new CustomerRepository(_connectionString);
            _columnVisibility = new Dictionary<string, bool>
            {
                { "CustomerID", true },
                { "FullName", true },
                { "Phone", true },
                { "Email", false },
                { "Address", false },
                { "RegistrationDate", true },
                { "IsActive", true },
                { "UmumiyQarz", true },
                { "CarModels", false },
                { "DebtDetails", false },
                { "Action", true }
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
            toolTip.SetToolTip(tableLayoutPanel, "Основная панель управления списком должников");
            toolTip.SetToolTip(titleLabel, "Заголовок раздела должников");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(searchBox, "Введите текст для поиска по видимым столбцам");
            toolTip.SetToolTip(buttonPanel, "Панель с кнопками управления");
            toolTip.SetToolTip(btnColumns, "Выбрать видимые столбцы таблицы");
            toolTip.SetToolTip(btnOpenFilterDialog, "Открыть окно фильтров");
            toolTip.SetToolTip(btnExport, "Экспортировать данные в Excel");
            toolTip.SetToolTip(btnAddCustomer, "Добавить нового клиента");
            toolTip.SetToolTip(countLabel, "Количество отображаемых записей");
            toolTip.SetToolTip(dataGridView, "Таблица с данными о должниках");
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
                { "CustomerID", "ID" },
                { "FullName", "ФИО" },
                { "Phone", "Телефон" },
                { "Email", "Эл. почта" },
                { "Address", "Адрес" },
                { "RegistrationDate", "Дата регистрации" },
                { "IsActive", "Активен" },
                { "UmumiyQarz", "Общий долг" },
                { "CarModels", "Модели машин" },
                { "DebtDetails", "Детали долга" },
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
                    if (column.Key == "CustomerID")
                        col.Width = 80;
                    dataGridView.Columns.Add(col);
                }
            }
        }

        private void EnhanceVisualStyles()
        {
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.BackgroundColor = Color.White;
        }

        private void InitializeSearch()
        {
            searchBox.TextChanged += (s, e) =>
            {
                _searchTimer.Stop();
                _searchTimer.Tick -= SearchTimer_Tick;
                _searchTimer.Tick += SearchTimer_Tick;
                _searchTimer.Start();
            };
        }

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            try
            {
                _dataSource = await _viewModel.SearchCustomerDebtsAsync(searchBox.Text, _viewModel.VisibleColumns);
                SortData();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "поиск");
            }
        }

        private void OptimizeDataGridView()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private async void LoadData()
        {
            try
            {
                _dataSource = await _viewModel.LoadCustomerDebtsAsync();
                SortData();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "загрузка данных");
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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new SaveFileDialog())
                {
                    dialog.Filter = "Excel Files|*.xlsx";
                    dialog.FileName = "CustomerDebts.xlsx";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        _viewModel.ExportToExcel(dialog.FileName, _dataSource);
                        MessageBox.Show("Данные успешно экспортированы!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "экспорт");
            }
        }

        private void BtnOpenFilterDialog_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new CustomerDebtsFilterDialog(_viewModel))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "открытие фильтров");
            }
        }

        private void BtnColumns_Click(object sender, EventArgs e)
        {
            try
            {
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
                        { "CustomerID", "ID" },
                        { "FullName", "ФИО" },
                        { "Phone", "Телефон" },
                        { "Email", "Эл. почта" },
                        { "Address", "Адрес" },
                        { "RegistrationDate", "Дата регистрации" },
                        { "IsActive", "Активен" },
                        { "UmumiyQarz", "Общий долг" },
                        { "CarModels", "Модели машин" },
                        { "DebtDetails", "Детали долга" }
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
                LogAndShowError(ex, "выбор столбцов");
            }
        }

        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new CustomerAddForm(_viewModel))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "добавление клиента");
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridView.Columns[e.ColumnIndex].Name == "Action")
            {
                var customerId = (int)dataGridView.Rows[e.RowIndex].Tag;
                var contextMenu = new ContextMenuStrip { Renderer = new CustomToolStripRenderer() };
                var editItem = new ToolStripMenuItem("Редактировать", null, (s, ev) => _viewModel.OpenCustomerEditForm(customerId, this.FindForm())) { Tag = "Edit" };
                var detailsItem = new ToolStripMenuItem("Подробности", null, (s, ev) => ShowCustomerDetails(customerId)) { Tag = "Details" };
                contextMenu.Items.AddRange(new[] { editItem, detailsItem });
                contextMenu.Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
            }
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var columnName = dataGridView.Columns[e.ColumnIndex].Name;
            if (_sortColumn == columnName)
            {
                _sortOrder = _sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                _sortColumn = columnName;
                _sortOrder = SortOrder.Ascending;
            }
            SortData();
            RefreshDataGridView();
        }

        private void SortData()
        {
            switch (_sortColumn)
            {
                case "CustomerID":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.CustomerID.CompareTo(y.CustomerID) : y.CustomerID.CompareTo(x.CustomerID));
                    break;
                case "FullName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.FullName, y.FullName) : string.Compare(y.FullName, x.FullName));
                    break;
                case "Phone":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Phone, y.Phone) : string.Compare(y.Phone, x.Phone));
                    break;
                case "Email":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Email, y.Email) : string.Compare(y.Email, x.Email));
                    break;
                case "Address":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Address, y.Address) : string.Compare(y.Address, x.Address));
                    break;
                case "RegistrationDate":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.RegistrationDate.CompareTo(y.RegistrationDate) : y.RegistrationDate.CompareTo(x.RegistrationDate));
                    break;
                case "IsActive":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.IsActive.CompareTo(y.IsActive) : y.IsActive.CompareTo(x.IsActive));
                    break;
                case "UmumiyQarz":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.UmumiyQarz.CompareTo(y.UmumiyQarz) : y.UmumiyQarz.CompareTo(x.UmumiyQarz));
                    break;
                case "CarModels":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(string.Join(",", x.CarModels), string.Join(",", y.CarModels)) : string.Compare(string.Join(",", y.CarModels), string.Join(",", x.CarModels)));
                    break;
                case "DebtDetails":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(string.Join(",", x.DebtDetails.Select(d => d.ItemName)), string.Join(",", y.DebtDetails.Select(d => d.ItemName))) : string.Compare(string.Join(",", y.DebtDetails.Select(d => d.ItemName)), string.Join(",", x.DebtDetails.Select(d => d.ItemName))));
                    break;
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            foreach (var customer in _dataSource)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                if (_columnVisibility["CustomerID"])
                    row.Cells[dataGridView.Columns["CustomerID"].Index].Value = customer.CustomerID;
                if (_columnVisibility["FullName"])
                    row.Cells[dataGridView.Columns["FullName"].Index].Value = customer.FullName ?? "Не указано";
                if (_columnVisibility["Phone"])
                    row.Cells[dataGridView.Columns["Phone"].Index].Value = customer.Phone ?? "Не указано";
                if (_columnVisibility["Email"])
                    row.Cells[dataGridView.Columns["Email"].Index].Value = customer.Email ?? "Не указано";
                if (_columnVisibility["Address"])
                    row.Cells[dataGridView.Columns["Address"].Index].Value = customer.Address ?? "Не указано";
                if (_columnVisibility["RegistrationDate"])
                    row.Cells[dataGridView.Columns["RegistrationDate"].Index].Value = customer.RegistrationDate == DateTime.MinValue ? "Не указано" : customer.RegistrationDate.ToString("yyyy-MM-dd");
                if (_columnVisibility["IsActive"])
                {
                    var cell = row.Cells[dataGridView.Columns["IsActive"].Index];
                    cell.Value = customer.IsActive ? "Да" : "Нет";
                    cell.Style.ForeColor = customer.IsActive ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                }
                if (_columnVisibility["UmumiyQarz"])
                    row.Cells[dataGridView.Columns["UmumiyQarz"].Index].Value = customer.UmumiyQarz;
                if (_columnVisibility["CarModels"])
                    row.Cells[dataGridView.Columns["CarModels"].Index].Value = string.Join(", ", customer.CarModels) ?? "Не указано";
                if (_columnVisibility["DebtDetails"])
                    row.Cells[dataGridView.Columns["DebtDetails"].Index].Value = string.Join(", ", customer.DebtDetails.Select(d => $"{d.ItemName}: {d.Amount}")) ?? "Не указано";
                if (_columnVisibility["Action"])
                    row.Cells[dataGridView.Columns["Action"].Index].Value = "...";
                row.Tag = customer.CustomerID;
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

        private async void ShowCustomerDetails(int customerId)
        {
            try
            {
                var customerFullInfo = await _viewModel.GetCustomerWithDebtDetailsAsync(customerId);

                if (customerFullInfo == null)
                {
                    MessageBox.Show("Информация о клиенте не найдена!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cars = (customerFullInfo.CarModels != null && customerFullInfo.CarModels.Any())
                    ? string.Join(Environment.NewLine, customerFullInfo.CarModels
                        .Select((c, index) => $"   {index + 1}. {c}"))
                    : "Данные отсутствуют";

                string debtDetails = (customerFullInfo.DebtDetails != null && customerFullInfo.DebtDetails.Any())
                    ? string.Join(Environment.NewLine, customerFullInfo.DebtDetails
                        .Select((d, index) => $"   {index + 1}. {d.ItemName}: {d.Amount:#,0}".Replace(",", ".")))
                    : "Долгов нет";

                string umumiyQarz = customerFullInfo.UmumiyQarz.ToString("#,0").Replace(",", ".") + " С";

                string message =
                    $"👤 ФИО: {customerFullInfo.FullName}\n" +
                    $"📞 Телефон: {customerFullInfo.Phone}\n" +
                    $"🆔 ID: {customerFullInfo.CustomerID}\n" +
                    $"📧 Email: {customerFullInfo.Email}\n" +
                    $"🏠 Адрес: {customerFullInfo.Address}\n" +
                    $"📅 Дата регистрации: {customerFullInfo.RegistrationDate:dd.MM.yyyy}\n" +
                    $"🔄 Статус: {(customerFullInfo.IsActive ? "Активен" : "Неактивен")}\n" +
                    $"🚗 Автомобили:\n{cars}\n" +
                    $"💰 Общий долг: {umumiyQarz}\n" +
                    $"📌 Детализация долгов:\n{debtDetails}";

                MessageBox.Show(message, "Информация о клиенте",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "показ деталей клиента");
            }
        }
    }
}
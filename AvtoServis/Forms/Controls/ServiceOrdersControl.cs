using AvtoServis.Data;
using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.Model.DTOs;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class ServiceOrdersControl : UserControl
    {
        private readonly ServiceOrdersViewModel _viewModel;
        private readonly IServiceOrdersRepository _serviceOrdersRepository;
        private readonly IServicesRepository _serviceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ICarModelsRepository _carModelRepository;
        private readonly UsersRepository _userRepository;
        private string _connectionString;
        private List<ServiceOrderDto> _dataSource;
        private readonly System.Windows.Forms.Timer _searchTimer;
        private readonly Dictionary<string, bool> _columnVisibility;
        private string _sortColumn = "OrderID";
        private SortOrder _sortOrder = SortOrder.Ascending;

        private readonly Control _uiControl = new Control();

        public ServiceOrdersControl(ServiceOrdersViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _dataSource = new List<ServiceOrderDto>();
            _connectionString = _viewModel.ConnectionString;
            _serviceOrdersRepository = new ServiceOrdersRepository(_connectionString);
            _serviceRepository = new ServicesRepository(_connectionString);
            _customerRepository = new CustomerRepository(_connectionString);
            _statusRepository = new StatusRepository(_connectionString);
            _carModelRepository = new CarModelsRepository(_connectionString);
            _userRepository = new UsersRepository();
            _columnVisibility = new Dictionary<string, bool>
            {
                { "OrderID", true },
                { "CustomerName", true },
                { "ServiceName", true },
                { "Quantity", true },
                { "TotalAmount", true },
                { "OrderDate", true },
                { "FinanceStatusID", true },
                { "PaidAmount", true },
                { "VehicleModel", false },
                { "StatusName", false },
                { "UserName", false },
                { "Action", true }
            };
            _searchTimer = new System.Windows.Forms.Timer { Interval = 300 };
            InitializeComponent();
            ConfigureColumns();
            EnhanceVisualStyles();
            InitializeSearch();  // O'zgartirildi: Timer event bir marta o'rnatiladi
            OptimizeDataGridView();
            LoadData();
            UpdateVisibleColumns();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Основная панель управления сервисными заказами");
            toolTip.SetToolTip(titleLabel, "Заголовок раздела сервисных заказов");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(searchBox, "Введите текст для поиска по видимым столбцам");
            toolTip.SetToolTip(buttonPanel, "Панель с кнопками управления");
            toolTip.SetToolTip(btnColumns, "Выбрать видимые столбцы таблицы");
            toolTip.SetToolTip(btnOpenFilterDialog, "Открыть окно фильтров");
            toolTip.SetToolTip(btnExport, "Экспортировать данные в Excel");
            toolTip.SetToolTip(btnAddOrder, "Инициировать добавление сервисного заказа");
            toolTip.SetToolTip(countLabel, "Количество отображаемых записей");
            toolTip.SetToolTip(dataGridView, "Таблица с данными о сервисных заказах");
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
            if (string.IsNullOrEmpty(searchBox.Text))
            {
                searchBox.Text = "Поиск...";
                searchBox.ForeColor = Color.Gray;
            }
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
                { "OrderID", "ID" },
                { "CustomerName", "Имя клиента" },
                { "ServiceName", "Услуга" },
                { "Quantity", "Количество" },
                { "TotalAmount", "Общая сумма" },
                { "OrderDate", "Дата заказа" },
                { "FinanceStatusID", "Статус оплаты" },
                { "PaidAmount", "Оплаченная сумма" },
                { "VehicleModel", "Модель автомобиля" },
                { "StatusName", "Статус" },
                { "UserName", "Продавец" },
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
                    if (column.Key == "OrderID" || column.Key == "Quantity")
                        col.Width = 80;
                    else if (column.Key == "VehicleModel" || column.Key == "UserName")
                        col.Width = 120;
                    else if (column.Key == "FinanceStatusID" || column.Key == "StatusName")
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
            btnAddOrder.BackColor = Color.FromArgb(40, 167, 69);
            btnAddOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);

            dataGridView.BackgroundColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 243, 245);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        // O'ZGARTIRILDI: Timer event bir marta o'rnatiladi, har safar TextChanged da qayta o'rnatilmaydi
        private void InitializeSearch()
        {
            _searchTimer.Tick += SearchTimer_Tick;  // Bir marta o'rnatish
            searchBox.TextChanged += (s, e) =>
            {
                _searchTimer.Stop();
                _searchTimer.Start();
            };
        }

        // O'ZGARTIRILDI: Trim() qo'shildi, LoadServiceOrders() orqali filtrlarni hisobga olish
        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            string searchText = searchBox.Text == "Поиск..." ? "" : searchBox.Text.Trim();
            try
            {
                // Filtrlarni hisobga olgan holda qidiruv
                var fullData = _viewModel.LoadServiceOrders();
                _dataSource = _viewModel.SearchServiceOrders(searchText, _viewModel.VisibleColumns);
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "поиск сервисных заказов");
            }
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
            dataGridView.AutoGenerateColumns = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly = true;
        }

        private void LoadData()
        {
            try
            {
                _dataSource = _viewModel.LoadServiceOrders();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "загрузка данных");
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.Rows.Clear();
            if (_dataSource == null || !_dataSource.Any())
            {
                countLabel.Text = "Записи: 0";
                dataGridView.Refresh();
                return;
            }

            var sortedData = _sortOrder == SortOrder.Ascending
                ? _dataSource.OrderBy(x => typeof(ServiceOrderDto).GetProperty(_sortColumn)?.GetValue(x))
                : _dataSource.OrderByDescending(x => typeof(ServiceOrderDto).GetProperty(_sortColumn)?.GetValue(x));

            foreach (var order in sortedData)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);

                if (_columnVisibility["OrderID"] && dataGridView.Columns["OrderID"] != null)
                    row.Cells[dataGridView.Columns["OrderID"].Index].Value = order.OrderID;
                if (_columnVisibility["CustomerName"] && dataGridView.Columns["CustomerName"] != null)
                    row.Cells[dataGridView.Columns["CustomerName"].Index].Value = order.CustomerName ?? "Не указано";
                if (_columnVisibility["ServiceName"] && dataGridView.Columns["ServiceName"] != null)
                    row.Cells[dataGridView.Columns["ServiceName"].Index].Value = order.ServiceName ?? "Не указано";
                if (_columnVisibility["Quantity"] && dataGridView.Columns["Quantity"] != null)
                    row.Cells[dataGridView.Columns["Quantity"].Index].Value = order.Quantity;
                if (_columnVisibility["TotalAmount"] && dataGridView.Columns["TotalAmount"] != null)
                    row.Cells[dataGridView.Columns["TotalAmount"].Index].Value = order.TotalAmount;
                if (_columnVisibility["OrderDate"] && dataGridView.Columns["OrderDate"] != null)
                    row.Cells[dataGridView.Columns["OrderDate"].Index].Value = order.OrderDate == DateTime.MinValue ? "Не указано" : order.OrderDate.ToString("yyyy-MM-dd");
                if (_columnVisibility["FinanceStatusID"] && dataGridView.Columns["FinanceStatusID"] != null)
                {
                    var cell = row.Cells[dataGridView.Columns["FinanceStatusID"].Index];
                    cell.Value = order.FinanceStatusID switch
                    {
                        1 => "Оплачен",
                        2 => "Не оплачен",
                        3 => "Частично оплачен",
                        _ => "Неизвестно"
                    };
                    cell.Style.ForeColor = order.FinanceStatusID switch
                    {
                        1 => Color.FromArgb(40, 167, 69),
                        2 => Color.FromArgb(220, 53, 69),
                        3 => Color.FromArgb(255, 193, 7),
                        _ => Color.Black
                    };
                }
                if (_columnVisibility["PaidAmount"] && dataGridView.Columns["PaidAmount"] != null)
                    row.Cells[dataGridView.Columns["PaidAmount"].Index].Value = order.PaidAmount.ToString("C");
                if (_columnVisibility["VehicleModel"] && dataGridView.Columns["VehicleModel"] != null)
                    row.Cells[dataGridView.Columns["VehicleModel"].Index].Value = order.VehicleModel ?? "Не указано";
                if (_columnVisibility["StatusName"] && dataGridView.Columns["StatusName"] != null)
                    row.Cells[dataGridView.Columns["StatusName"].Index].Value = order.StatusName ?? "Не указано";
                if (_columnVisibility["UserName"] && dataGridView.Columns["UserName"] != null)
                    row.Cells[dataGridView.Columns["UserName"].Index].Value = order.UserName ?? "Не указано";
                if (_columnVisibility["Action"] && dataGridView.Columns["Action"] != null)
                    row.Cells[dataGridView.Columns["Action"].Index].Value = "...";
                row.Tag = order.OrderID;
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
                        { "OrderID", "ID" },
                        { "CustomerName", "Имя клиента" },
                        { "ServiceName", "Услуга" },
                        { "Quantity", "Количество" },
                        { "TotalAmount", "Общая сумма" },
                        { "OrderDate", "Дата заказа" },
                        { "FinanceStatusID", "Статус оплаты" },
                        { "PaidAmount", "Оплаченная сумма" },
                        { "VehicleModel", "Модель автомобиля" },
                        { "StatusName", "Статус" },
                        { "UserName", "Продавец" }
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
                        LoadData(); // Refresh data after changing columns
                        form.Close();
                    };

                    form.Controls.Add(checkedListBox);
                    form.Controls.Add(btnOk);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "выборе столбцов");
            }
        }

        private void BtnOpenFilterDialog_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new ServiceOrdersFilterDialog(_viewModel))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        _dataSource = _viewModel.LoadServiceOrders();
                        RefreshDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "открытии диалога фильтров");
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Сохранить как Excel",
                FileName = "ServiceOrders.xlsx"
            })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _viewModel.ExportToExcel(_dataSource, saveFileDialog.FileName, _columnVisibility);
                        MessageBox.Show("Экспорт успешно завершен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        LogAndShowError(ex, "экспорт в Excel");
                    }
                }
            }
        }

        private void BtnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                var viewModel = new ServiceOrderViewModel(
                    _serviceRepository,
                    _serviceOrdersRepository,
                    _statusRepository,
                    _carModelRepository,
                    _customerRepository,
                    _userRepository,
                    _connectionString
                    );
                using (var saleForm = new ServiceOrderForm(viewModel, _customerRepository))
                {
                    if (saleForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); // Refresh data after adding a new order
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "выполнении продажи");
            }
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var columnName = dataGridView.Columns[e.ColumnIndex].Name;
            if (columnName == "Action") return;

            _sortOrder = _sortColumn == columnName && _sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            _sortColumn = columnName;
            RefreshDataGridView();
        }

        // QO'SHIMCHA: Action menu ni kengaytirish (faqat bu metod o'zgartirildi)
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridView.Columns[e.ColumnIndex].Name != "Action") return;

            var row = dataGridView.Rows[e.RowIndex];
            if (row.Tag == null || !(row.Tag is int orderId)) return;

            var menu = new ContextMenuStrip { Renderer = new CustomToolStripRenderer() };

            // 1. Подробности (mavjud, yashil rang)
            menu.Items.Add(new ToolStripMenuItem("Подробности", null, (s, ev) =>
            {
                // Uncomment and implement if needed
                // using (var dialog = new ServiceOrderDetailsDialog(_viewModel, orderId))
                // {
                //     dialog.ShowDialog();
                // }
            })
            { Tag = "Details" });  // Yashil rang uchun Tag

            // 2. Редактировать (qo‘shimcha, ko'k rang)
            menu.Items.Add(new ToolStripMenuItem("Редактировать", null, (s, ev) =>
            {
                try
                {
                    _viewModel.OpenServiceOrderEditForm(orderId, ParentForm);  // Edit form ochish (ViewModel da bor)
                    LoadData();  // Ma'lumotlarni yangilash
                }
                catch (Exception ex)
                {
                    LogAndShowError(ex, "редактировании заказа");
                }
            })
            { Tag = "Edit" });  // Ko'k rang uchun Tag

            // 3. Отменить (qo‘shimcha, qizil rang)
            menu.Items.Add(new ToolStripMenuItem("Отменить", null, (s, ev) =>
            {
                if (MessageBox.Show("Вы уверены, что хотите отменить этот заказ? Это действие нельзя отменить.", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        _viewModel.ReturnService(orderId);
                        LoadData();  // Ma'lumotlarni yangilash
                        MessageBox.Show("Заказ успешно отменен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        LogAndShowError(ex, "отмене заказа");
                    }
                }
            })
            { Tag = "Cancel" });  // Qizil rang uchun Tag

            // Menu ni ko'rsatish (original pozitsiya)
            menu.Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
        }

        // QO'SHIMCHA: Renderer ni kengaytirish (faqat yangi Tag lar uchun rang qo'shildi)
        private class CustomToolStripRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                var item = e.Item as ToolStripMenuItem;
                if (item != null)
                {
                    Color backgroundColor = Color.White;
                    Color selectedColor = Color.FromArgb(200, 230, 255);

                    // Ranglarni Tag bo'yicha belgilash
                    switch (item.Tag?.ToString())
                    {
                        case "Details":  // Yashil
                            backgroundColor = Color.FromArgb(40, 167, 69);
                            selectedColor = Color.FromArgb(60, 187, 89);
                            break;
                        case "Edit":  // Ko'k
                            backgroundColor = Color.FromArgb(25, 118, 210);
                            selectedColor = Color.FromArgb(45, 138, 230);
                            break;
                        case "Cancel":  // Qizil
                            backgroundColor = Color.FromArgb(220, 53, 69);
                            selectedColor = Color.FromArgb(240, 73, 89);
                            break;
                    }

                    // Selected holatda rang o'zgartirish
                    Color finalColor = item.Selected ? selectedColor : backgroundColor;
                    using (var brush = new SolidBrush(finalColor))
                    {
                        e.Graphics.FillRectangle(brush, new Rectangle(0, 0, e.Item.Width, e.Item.Height));
                    }
                }
                base.OnRenderMenuItemBackground(e);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                // Text rangini Selected holatda oq qilish (rangli background uchun)
                if (e.Item.Selected)
                {
                    e.TextColor = Color.White;
                }
                else
                {
                    e.TextColor = Color.Black;
                }
                base.OnRenderItemText(e);
            }
        }
    }
}
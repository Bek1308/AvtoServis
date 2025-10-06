using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AvtoServis.Model.DTOs;
using AvtoServis.ViewModels.Screens;

namespace AvtoServis.Forms.Controls
{
    public partial class FullServiceControl : UserControl
    {
        private readonly ServiceOrdersViewModel _viewModel;
        private readonly ImageList _actionImageList;
        private List<FullService> _dataSource;
        private System.Windows.Forms.Timer _searchTimer;
        private readonly Dictionary<string, bool> _columnVisibility;
        private string _sortColumn = "ServiceID";
        private SortOrder _sortOrder = SortOrder.Ascending;
        private bool _isDialogMode;

        public FullServiceControl(ServiceOrdersViewModel viewModel, ImageList actionImageList, bool isDialogMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _actionImageList = actionImageList ?? throw new ArgumentNullException(nameof(actionImageList));
            _dataSource = new List<FullService>();
            _isDialogMode = isDialogMode;
            _columnVisibility = new Dictionary<string, bool>
            {
                { "ServiceID", true },
                { "Name", true },
                { "Price", true },
                { "SoldCount", true },
                { "TotalRevenue", true }
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
            //btnExport.Visible = true; // Tugma ko‘rinishini majburiy o‘rnatish
            //System.Diagnostics.Debug.WriteLine($"btnExport.Visible after InitializeComponent: {btnExport.Visible}");
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Основная панель управления услугами");
            toolTip.SetToolTip(titleLabel, "Заголовок раздела услуг");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(searchBox, "Введите текст для поиска по видимым столбцам");
            toolTip.SetToolTip(buttonPanel, "Панель с кнопками управления");
            toolTip.SetToolTip(btnColumns, "Выбрать видимые столбцы таблицы");
            //toolTip.SetToolTip(btnExport, "Экспортировать данные в Excel"); // _isDialogMode shartisiz
            toolTip.SetToolTip(countLabel, "Количество отображаемых услуг");
            toolTip.SetToolTip(dataGridView, _isDialogMode ? "Дважды щелкните, чтобы выбрать услугу" : "Таблица с данными об услугах");
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
                { "ServiceID", "ID" },
                { "Name", "Название услуги" },
                { "Price", "Цена" },
                { "SoldCount", "Количество продаж" },
                { "TotalRevenue", "Общая выручка" }
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
                    if (column.Key == "ServiceID" || column.Key == "SoldCount")
                        col.Width = 80;
                    else if (column.Key == "Price" || column.Key == "TotalRevenue")
                        col.Width = 120;
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
            //btnExport.BackColor = Color.FromArgb(25, 118, 210);
            //btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);

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
                    saveFileDialog.FileName = $"FullServices_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _viewModel.ExportToExcelServices(_dataSource, saveFileDialog.FileName, _columnVisibility);
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
                Size = new Size(300, 250),
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
                    Size = new Size(260, 150),
                    CheckOnClick = true,
                    Font = new Font("Segoe UI", 10F),
                    BorderStyle = BorderStyle.FixedSingle
                };
                var columnMapping = new Dictionary<string, string>
                {
                    { "ServiceID", "ID" },
                    { "Name", "Название услуги" },
                    { "Price", "Цена" },
                    { "SoldCount", "Количество продаж" },
                    { "TotalRevenue", "Общая выручка" }
                };
                foreach (var column in _columnVisibility)
                {
                    checkedListBox.Items.Add(new { Name = column.Key, DisplayName = columnMapping[column.Key] }, column.Value);
                }
                checkedListBox.DisplayMember = "DisplayName";
                checkedListBox.ValueMember = "Name";

                var btnOk = new Button
                {
                    Text = "OK",
                    Location = new Point(100, 160),
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
                using (var dialog = new Form
                {
                    Text = "Фильтры",
                    Size = new Size(400, 300),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    BackColor = Color.FromArgb(245, 245, 245)
                })
                {
                    var panel = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        FlowDirection = FlowDirection.TopDown,
                        AutoSize = true
                    };

                    var columnComboBox = new ComboBox
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Width = 150,
                        Font = new Font("Segoe UI", 10F)
                    };
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "ServiceID", "ID" },
                        { "Name", "Название услуги" },
                        { "Price", "Цена" },
                        { "SoldCount", "Количество продаж" },
                        { "TotalRevenue", "Общая выручка" }
                    };
                    columnComboBox.Items.AddRange(columnMapping.Values.ToArray());
                    columnComboBox.SelectedIndex = 0;

                    var operatorComboBox = new ComboBox
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Width = 100,
                        Font = new Font("Segoe UI", 10F)
                    };
                    operatorComboBox.Items.AddRange(new[] { "=", ">", "<", "Содержит" });
                    operatorComboBox.SelectedIndex = 0;

                    var valueTextBox = new TextBox
                    {
                        Width = 150,
                        Font = new Font("Segoe UI", 10F)
                    };

                    var btnApply = new Button
                    {
                        Text = "Применить",
                        Size = new Size(100, 30),
                        BackColor = Color.FromArgb(40, 167, 69),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(60, 187, 89) },
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                    };

                    btnApply.Click += (s, ev) =>
                    {
                        try
                        {
                            var column = columnMapping.FirstOrDefault(kvp => kvp.Value == columnComboBox.SelectedItem.ToString()).Key;
                            var op = operatorComboBox.SelectedItem.ToString();
                            var value = valueTextBox.Text;
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                MessageBox.Show("Введите значение для фильтра!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            _viewModel.Filters = new List<(string Column, string Operator, string SearchText)>
                            {
                                (column, op, value)
                            };
                            LoadData();
                            dialog.Close();
                        }
                        catch (Exception ex)
                        {
                            LogAndShowError(ex, "применении фильтра");
                        }
                    };

                    panel.Controls.Add(new Label { Text = "Столбец:", Font = new Font("Segoe UI", 10F) });
                    panel.Controls.Add(columnComboBox);
                    panel.Controls.Add(new Label { Text = "Оператор:", Font = new Font("Segoe UI", 10F) });
                    panel.Controls.Add(operatorComboBox);
                    panel.Controls.Add(new Label { Text = "Значение:", Font = new Font("Segoe UI", 10F) });
                    panel.Controls.Add(valueTextBox);
                    panel.Controls.Add(btnApply);
                    dialog.Controls.Add(panel);
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "открытии диалога фильтров");
            }
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
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
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "сортировке столбца");
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            try
            {
                var row = dataGridView.Rows[e.RowIndex];
                if (row.Tag != null && row.Tag is int id)
                {
                    System.Diagnostics.Debug.WriteLine($"Cell clicked: ServiceID = {id}");
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "обработке клика по ячейке");
            }
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
                _dataSource = _viewModel.LoadServices();
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
                    _dataSource = _viewModel.LoadServices();
                }
                else
                {
                    _dataSource = _viewModel.LoadServices()
                        .Where(s => s.Name.Contains(searchBox.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "поиске");
            }
        }

        private void SortDataSource()
        {
            switch (_sortColumn)
            {
                case "ServiceID":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.ServiceID.CompareTo(y.ServiceID) : y.ServiceID.CompareTo(x.ServiceID));
                    break;
                case "Name":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Name, y.Name) : string.Compare(y.Name, x.Name));
                    break;
                case "Price":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.Price.CompareTo(y.Price) : y.Price.CompareTo(x.Price));
                    break;
                case "SoldCount":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.SoldCount.CompareTo(y.SoldCount) : y.SoldCount.CompareTo(x.SoldCount));
                    break;
                case "TotalRevenue":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.TotalRevenue.CompareTo(y.TotalRevenue) : y.TotalRevenue.CompareTo(x.TotalRevenue));
                    break;
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            foreach (var service in _dataSource)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                if (_columnVisibility["ServiceID"])
                    row.Cells[dataGridView.Columns["ServiceID"]?.Index ?? 0].Value = service.ServiceID;
                if (_columnVisibility["Name"])
                    row.Cells[dataGridView.Columns["Name"]?.Index ?? 0].Value = service.Name;
                if (_columnVisibility["Price"])
                    row.Cells[dataGridView.Columns["Price"]?.Index ?? 0].Value = service.Price;
                if (_columnVisibility["SoldCount"])
                    row.Cells[dataGridView.Columns["SoldCount"]?.Index ?? 0].Value = service.SoldCount;
                if (_columnVisibility["TotalRevenue"])
                    row.Cells[dataGridView.Columns["TotalRevenue"]?.Index ?? 0].Value = service.TotalRevenue;
                row.Tag = service.ServiceID;
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Услуги: {_dataSource.Count}";
            dataGridView.Refresh();
        }

        private void LogAndShowError(Exception ex, string operation)
        {
            System.Diagnostics.Debug.WriteLine($"{operation} Ошибка: {ex.Message}\nStackTrace: {ex.StackTrace}");
            MessageBox.Show($"Произошла ошибка при {operation.ToLower()}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
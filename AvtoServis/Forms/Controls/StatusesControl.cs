using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System.Reflection;
using System.Drawing;

namespace AvtoServis.Forms.Controls
{
    public partial class StatusesControl : UserControl
    {
        private readonly StatusesViewModel _viewModel;
        private readonly ImageList _actionImageList;
        private List<Status> _dataSource;
        private System.Windows.Forms.Timer _searchTimer;
        private readonly Dictionary<string, bool> _columnVisibility;
        private string _sortColumn = "StatusID";
        private SortOrder _sortOrder = SortOrder.Ascending;

        // Jadval nomlari va ularning ko'rsatiladigan nomlari uchun lug'at
        private readonly Dictionary<string, string> _tableDisplayNames = new Dictionary<string, string>
        {
            { "ExpenseStatuses", "Статусы расходов" },
            { "IncomeStatuses", "Статусы доходов" },
            { "OrderStatuses", "Статусы заказов" }
        };

        public StatusesControl(StatusesViewModel viewModel, ImageList actionImageList)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _actionImageList = actionImageList ?? throw new ArgumentNullException(nameof(actionImageList));
            _dataSource = new List<Status>();
            _columnVisibility = new Dictionary<string, bool>
            {
                { "StatusID", true },
                { "Name", true },
                { "Description", true },
                { "Color", true },
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
            toolTip.SetToolTip(tableLayoutPanel, "Основная панель управления статусами");
            toolTip.SetToolTip(titleLabel, "Заголовок раздела статусов");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(searchBox, "Введите текст для поиска по видимым столбцам");
            toolTip.SetToolTip(buttonPanel, "Панель с кнопками управления");
            toolTip.SetToolTip(addButton, "Добавить новый статус");
            toolTip.SetToolTip(btnColumns, "Выбрать видимые столбцы и таблицу");
            toolTip.SetToolTip(btnOpenFilterDialog, "Открыть окно фильтров");
            toolTip.SetToolTip(btnExport, "Экспортировать данные в Excel");
            toolTip.SetToolTip(countLabel, "Количество отображаемых статусов");
            toolTip.SetToolTip(dataGridView, "Таблица с данными о статусах");
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
            if (_columnVisibility["StatusID"])
            {
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "StatusID",
                    HeaderText = "ID",
                    DataPropertyName = "StatusID",
                    ReadOnly = true,
                    Width = 80,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });
            }
            if (_columnVisibility["Name"])
            {
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Name",
                    HeaderText = "Название",
                    DataPropertyName = "Name",
                    ReadOnly = true,
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });
            }
            if (_columnVisibility["Description"])
            {
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Description",
                    HeaderText = "Описание",
                    DataPropertyName = "Description",
                    ReadOnly = true,
                    Width = 200,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });
            }
            if (_columnVisibility["Color"])
            {
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Color",
                    HeaderText = "Цвет",
                    DataPropertyName = "Color",
                    ReadOnly = true,
                    Width = 100
                });
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
            UpdateVisibleColumns();
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
            ShowDialog(null);
        }

        private void BtnColumns_Click(object sender, EventArgs e)
        {
            using (var dialog = new Form
            {
                Text = "Выбор столбцов и таблицы",
                Size = new Size(300, 350),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(245, 245, 245)
            })
            {
                var tableLayout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    ColumnCount = 1,
                    RowCount = 4,
                    RowStyles = { new RowStyle(SizeType.Absolute, 40F), new RowStyle(SizeType.Absolute, 2F), new RowStyle(SizeType.Percent, 100F), new RowStyle(SizeType.Absolute, 40F) }
                };

                var cmbTable = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(260, 30),
                    Location = new Point(10, 10)
                };
                // ComboBox'ga zagolovka nomlarini qo'shish
                foreach (var table in _tableDisplayNames)
                {
                    cmbTable.Items.Add(table.Value);
                }
                // Tanlangan jadvalni zagolovka nomi sifatida ko'rsatish
                cmbTable.SelectedItem = _tableDisplayNames.ContainsKey(_viewModel.SelectedTable)
                    ? _tableDisplayNames[_viewModel.SelectedTable]
                    : "Статусы";
                tableLayout.Controls.Add(cmbTable, 0, 0);

                var separator = new Panel
                {
                    BackColor = Color.FromArgb(180, 180, 180),
                    Size = new Size(260, 2),
                    Dock = DockStyle.Fill
                };
                tableLayout.Controls.Add(separator, 0, 1);

                var checkedListBox = new CheckedListBox
                {
                    Size = new Size(260, 200),
                    CheckOnClick = true,
                    Font = new Font("Segoe UI", 10F),
                    BorderStyle = BorderStyle.FixedSingle
                };
                foreach (var column in _columnVisibility)
                {
                    if (column.Key != "Actions")
                    {
                        checkedListBox.Items.Add(new { Name = column.Key, DisplayName = dataGridView.Columns[column.Key]?.HeaderText ?? column.Key }, column.Value);
                    }
                }
                checkedListBox.DisplayMember = "DisplayName";
                checkedListBox.ValueMember = "Name";
                tableLayout.Controls.Add(checkedListBox, 0, 2);

                var btnOk = new Button
                {
                    Text = "ОК",
                    Size = new Size(80, 30),
                    BackColor = Color.FromArgb(40, 167, 69),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(60, 187, 89) },
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Anchor = AnchorStyles.None
                };
                tableLayout.Controls.Add(btnOk, 0, 3);
                tableLayout.SetColumnSpan(btnOk, 1);
                tableLayout.RowStyles[3].SizeType = SizeType.Absolute;
                tableLayout.RowStyles[3].Height = 40F;

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
                    // Tanlangan zagolovka nomidan asl jadval nomini aniqlash
                    var selectedDisplayName = cmbTable.SelectedItem?.ToString();
                    _viewModel.SelectedTable = _tableDisplayNames.FirstOrDefault(x => x.Value == selectedDisplayName).Key ?? _viewModel.SelectedTable;
                    ConfigureColumns();
                    LoadData();
                    UpdateTitle();
                    dialog.Close();
                };

                dialog.Controls.Add(tableLayout);
                dialog.ShowDialog();
            }
        }

        private void BtnOpenFilterDialog_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateVisibleColumns();
                using (var dialog = new StatusesFilterDialog(_viewModel))
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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = $"Statuses_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
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
                        ShowDialog(id, true);
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
                        using (var dialog = new StatusesDialog(_viewModel, id, isDeleteMode: true))
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
                _dataSource = _viewModel.LoadStatuses();
                SortDataSource();
                RefreshDataGridView();
                UpdateTitle();
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
                    _dataSource = _viewModel.LoadStatuses();
                }
                else
                {
                    _dataSource = _viewModel.SearchStatuses(searchBox.Text.Trim());
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
                _dataSource = _viewModel.LoadStatuses();
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "применении фильтров");
            }
        }

        public void ShowDialog(int? id, bool isViewOnly = false)
        {
            try
            {
                using (var dialog = new StatusesDialog(_viewModel, id, isViewOnly))
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

        private void SortDataSource()
        {
            switch (_sortColumn)
            {
                case "StatusID":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.StatusID.CompareTo(y.StatusID) : y.StatusID.CompareTo(x.StatusID));
                    break;
                case "Name":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Name, y.Name) : string.Compare(y.Name, x.Name));
                    break;
                case "Description":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Description, y.Description) : string.Compare(y.Description, x.Description));
                    break;
                case "Color":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Color, y.Color) : string.Compare(y.Color, x.Color));
                    break;
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            foreach (var status in _dataSource)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                if (_columnVisibility["StatusID"])
                    row.Cells[dataGridView.Columns["StatusID"]?.Index ?? 0].Value = status.StatusID;
                if (_columnVisibility["Name"])
                    row.Cells[dataGridView.Columns["Name"]?.Index ?? 0].Value = status.Name;
                if (_columnVisibility["Description"])
                    row.Cells[dataGridView.Columns["Description"]?.Index ?? 0].Value = status.Description;
                if (_columnVisibility["Color"])
                {
                    var colorCell = row.Cells[dataGridView.Columns["Color"]?.Index ?? 0];
                    colorCell.Value = status.Color;
                    try
                    {
                        colorCell.Style.BackColor = Color.FromName(status.Color);
                        colorCell.Style.ForeColor = Color.White; // Matn o'qilishi uchun oq rang
                    }
                    catch
                    {
                        colorCell.Style.BackColor = Color.White; // Agar rang nomi noto'g'ri bo'lsa, oq fon
                        colorCell.Style.ForeColor = Color.Black;
                    }
                }
                var actionCell = row.Cells[dataGridView.Columns["Actions"].Index];
                actionCell.Value = "...";
                actionCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.Tag = status.StatusID;
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Статусы: {_dataSource.Count}";
            dataGridView.Refresh();
        }

        private void UpdateTitle()
        {
            switch (_viewModel.SelectedTable)
            {
                case "ExpenseStatuses":
                    titleLabel.Text = "Статусы расходов";
                    break;
                case "IncomeStatuses":
                    titleLabel.Text = "Статусы доходов";
                    break;
                //case "OperationStatuses":
                //    titleLabel.Text = "Статусы операций";
                //    break;
                case "OrderStatuses":
                    titleLabel.Text = "Статусы заказов";
                    break;
                default:
                    titleLabel.Text = "Статусы";
                    break;
            }
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
using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System.Reflection;

namespace AvtoServis.Forms.Controls
{
    public partial class StatusesControl : UserControl, IStatusesUserInterface
    {
        private readonly StatusesViewModel _viewModel;
        private readonly ImageList _actionImageList;
        private List<Status> _dataSource;
        private System.Windows.Forms.Timer _searchTimer;
        private string _sortColumn = "StatusID";
        private SortOrder _sortOrder = SortOrder.Ascending;
        private string _selectedTable;

        public StatusesControl(StatusesViewModel viewModel, ImageList actionImageList)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _actionImageList = actionImageList ?? throw new ArgumentNullException(nameof(actionImageList));
            _dataSource = new List<Status>();
            InitializeComponent();
            ConfigureColumns();
            EnhanceVisualStyles();
            InitializeSearch();
            InitializeComboBox();
            OptimizeDataGridView();
            LoadData();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void InitializeComboBox()
        {
            comboBoxTables.Items.Clear();
            var tableDisplayNames = _viewModel.GetTableDisplayNames();
            foreach (var table in tableDisplayNames)
            {
                comboBoxTables.Items.Add(new KeyValuePair<string, string>(table.Key, table.Value));
            }
            comboBoxTables.DisplayMember = "Value";
            comboBoxTables.ValueMember = "Key";
            if (comboBoxTables.Items.Count > 0)
            {
                comboBoxTables.SelectedIndex = 0;
                _selectedTable = ((KeyValuePair<string, string>)comboBoxTables.SelectedItem).Key;
            }
            comboBoxTables.SelectedIndexChanged += ComboBoxTables_SelectedIndexChanged;
        }

        private void ConfigureColumns()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StatusID",
                HeaderText = "ID",
                DataPropertyName = "StatusID",
                ReadOnly = true,
                Width = 80
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "Название",
                DataPropertyName = "Name",
                ReadOnly = true,
                Width = 200
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Описание",
                DataPropertyName = "Description",
                ReadOnly = true,
                Width = 300
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Color",
                HeaderText = "Цвет",
                DataPropertyName = "Color",
                ReadOnly = true,
                Width = 100
            });
            dataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Actions",
                HeaderText = "Действия",
                Text = "...",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 80
            });

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoGenerateColumns = false;
        }

        private void EnhanceVisualStyles()
        {
            addButton.BackColor = Color.FromArgb(25, 118, 210);
            addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnExport.BackColor = Color.FromArgb(40, 167, 69);
            btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);

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

        private void ComboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedItem != null)
            {
                _selectedTable = ((KeyValuePair<string, string>)comboBoxTables.SelectedItem).Key;
                titleLabel.Text = $"Список: {((KeyValuePair<string, string>)comboBoxTables.SelectedItem).Value}";
                LoadData();
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            ShowDialog(null);
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
                        using (var dialog = new StatusesDialog(_viewModel, _selectedTable, id, isDeleteMode: true))
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

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "Color" && e.Value != null)
            {
                try
                {
                    e.CellStyle.BackColor = ColorTranslator.FromHtml(e.Value.ToString());
                    e.CellStyle.ForeColor = GetContrastColor(e.CellStyle.BackColor);
                    e.Value = e.Value.ToString().ToUpper();
                }
                catch
                {
                    e.CellStyle.BackColor = Color.White;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        private Color GetContrastColor(Color backgroundColor)
        {
            int brightness = (int)(backgroundColor.R * 0.299 + backgroundColor.G * 0.587 + backgroundColor.B * 0.114);
            return brightness > 128 ? Color.Black : Color.White;
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

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel файлы (*.xlsx)|*.xlsx";
                    saveFileDialog.DefaultExt = "xlsx";
                    saveFileDialog.FileName = $"{_selectedTable}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportData(saveFileDialog.FileName);
                        MessageBox.Show("Данные успешно экспортированы!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "экспорте");
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            PerformSearch(searchBox.Text.Trim());
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text != "Поиск...")
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
                PerformSearch(searchBox.Text.Trim());
            }
        }

        public void LoadData()
        {
            try
            {
                if (string.IsNullOrEmpty(_selectedTable)) return;
                _dataSource = _viewModel.LoadStatuses(_selectedTable);
                SortDataSource();
                RefreshDataGridView();
                System.Diagnostics.Debug.WriteLine($"LoadData: DataGridView refreshed with {_dataSource.Count} rows for table {_selectedTable}.");
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "загрузке данных");
            }
        }

        public void PerformSearch(string searchText)
        {
            try
            {
                if (string.IsNullOrEmpty(_selectedTable)) return;
                _dataSource = _viewModel.SearchStatuses(_selectedTable, searchText);
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "поиске");
            }
        }

        public void ExportData(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(_selectedTable)) return;
                _viewModel.ExportToExcel(_dataSource, filePath, _selectedTable);
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "экспорте данных");
            }
        }

        public void ShowDialog(int? id)
        {
            try
            {
                if (string.IsNullOrEmpty(_selectedTable)) return;
                using (var dialog = new StatusesDialog(_viewModel, _selectedTable, id))
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
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Description ?? "", y.Description ?? "") : string.Compare(y.Description ?? "", x.Description ?? ""));
                    break;
                case "Color":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Color ?? "", y.Color ?? "") : string.Compare(y.Color ?? "", x.Color ?? ""));
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
                row.Cells[dataGridView.Columns["StatusID"].Index].Value = status.StatusID;
                row.Cells[dataGridView.Columns["Name"].Index].Value = status.Name;
                row.Cells[dataGridView.Columns["Description"].Index].Value = status.Description;
                row.Cells[dataGridView.Columns["Color"].Index].Value = status.Color;
                row.Tag = status.StatusID;
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Статусы: {_dataSource.Count}";
            dataGridView.Refresh();
        }

        private void LogAndShowError(Exception ex, string operation)
        {
            System.Diagnostics.Debug.WriteLine($"{operation} Ошибка: {ex.Message}\nStackTrace: {ex.StackTrace}");
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
                        backgroundColor = item.Selected ? Color.FromArgb(255, 100, 100) : Color.FromArgb(220, 53, 69);

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
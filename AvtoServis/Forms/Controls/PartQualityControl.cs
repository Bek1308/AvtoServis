using AvtoServis.Forms.Modals.PartQualities;
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
    public partial class PartQualityControl : UserControl
    {
        private readonly PartQualitiesViewModel _viewModel;
        private readonly ImageList _actionImageList;
        private List<PartQuality> dataSource;
        private System.Windows.Forms.Timer _searchTimer;
        private readonly Dictionary<string, bool> _columnVisibility;
        private string _sortColumn = "QualityID";
        private SortOrder _sortOrder = SortOrder.Ascending;

        public PartQualityControl(PartQualitiesViewModel viewModel, ImageList actionImageList)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _actionImageList = actionImageList ?? throw new ArgumentNullException(nameof(actionImageList));
            dataSource = new List<PartQuality>();
            _columnVisibility = new Dictionary<string, bool>
            {
                { "QualityID", true },
                { "Name", true },
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
            this.MinimumSize = new Size(600, 400);
            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Основная панель управления качествами запчастей");
            toolTip.SetToolTip(titleLabel, "Заголовок раздела качеств запчастей");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(searchBox, "Введите текст для поиска по всем видимым столбцам");
            toolTip.SetToolTip(buttonPanel, "Панель с кнопками управления");
            toolTip.SetToolTip(addButton, "Добавить новое качество запчасти");
            toolTip.SetToolTip(btnColumns, "Выбрать видимые столбцы таблицы");
            toolTip.SetToolTip(btnOpenFilterDialog, "Открыть окно фильтров");
            toolTip.SetToolTip(btnExport, "Экспортировать данные в Excel");
            toolTip.SetToolTip(countLabel, "Количество отображаемых качеств");
            toolTip.SetToolTip(dataGridView, "Таблица с данными о качествах запчастей");
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
            if (_columnVisibility["QualityID"])
            {
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "QualityID",
                    HeaderText = "Номер",
                    DataPropertyName = "QualityID",
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
                    HeaderText = "Название качества",
                    DataPropertyName = "Name",
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
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
            ShowPartQualityDialog(null);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = $"PartQualities_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _viewModel.ExportToExcel(dataSource, saveFileDialog.FileName, _columnVisibility);
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
                Size = new Size(300, 200),
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
                    Size = new Size(260, 100),
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

                var btnOk = new Button
                {
                    Text = "ОК",
                    Location = new Point(100, 120),
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
                using (var dialog = new PartQualityFilterDialog(_viewModel))
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
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dataGridView.Columns[e.ColumnIndex].Name != "Actions") return;

            int id = (int)dataGridView.Rows[e.RowIndex].Tag;

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
                editItem.Click += (s, ev) => ShowPartQualityDialog(id);
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
                    using (var dialog = new PartQualityDialog(_viewModel, id, isDeleteMode: true))
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
                dataSource = _viewModel.LoadPartQualities();
                SortDataSource();
                RefreshDataGridView();
                System.Diagnostics.Debug.WriteLine($"LoadData: DataGridView refreshed with {dataSource.Count} rows.");
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
                    dataSource = _viewModel.LoadPartQualities();
                }
                else
                {
                    dataSource = _viewModel.SearchPartQualities(searchBox.Text.Trim());
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
                dataSource = _viewModel.LoadPartQualities();
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
                case "QualityID":
                    dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.QualityID.CompareTo(y.QualityID) : y.QualityID.CompareTo(x.QualityID));
                    break;
                case "Name":
                    dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Name, y.Name) : string.Compare(y.Name, x.Name));
                    break;
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            foreach (var quality in dataSource)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                if (_columnVisibility["QualityID"])
                    row.Cells[dataGridView.Columns["QualityID"]?.Index ?? 0].Value = quality.QualityID;
                if (_columnVisibility["Name"])
                    row.Cells[dataGridView.Columns["Name"]?.Index ?? 0].Value = quality.Name;
                var actionCell = row.Cells[dataGridView.Columns["Actions"].Index];
                actionCell.Value = "...";
                actionCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.Tag = quality.QualityID;
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Качеств: {dataSource.Count}";
            dataGridView.Refresh();
        }

        private void ShowPartQualityDialog(int? id)
        {
            try
            {
                using (var dialog = new PartQualityDialog(_viewModel, id))
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
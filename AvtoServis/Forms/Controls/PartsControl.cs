using AvtoServis.Data.Interfaces.UserInterface;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class PartsControl : UserControl, IPartsUserInterface
    {
        private readonly PartsViewModel _viewModel;
        private readonly ImageList _actionImageList;
        private List<Part> _dataSource;
        private System.Windows.Forms.Timer _searchTimer;
        private string _sortColumn = "PartID";
        private SortOrder _sortOrder = SortOrder.Ascending;

        public PartsControl(PartsViewModel viewModel, ImageList actionImageList)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _actionImageList = actionImageList ?? throw new ArgumentNullException(nameof(actionImageList));
            _dataSource = new List<Part>();
            InitializeComponent();
            ConfigureColumns();
            EnhanceVisualStyles();
            InitializeSearch();
            OptimizeDataGridView();
            LoadData();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void ConfigureColumns()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PartID",
                HeaderText = "ID",
                DataPropertyName = "PartID",
                ReadOnly = true,
                Width = 80
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CarBrandName",
                HeaderText = "Марка",
                DataPropertyName = "CarBrandName",
                ReadOnly = true,
                Width = 150
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CatalogNumber",
                HeaderText = "Каталожный номер",
                DataPropertyName = "CatalogNumber",
                ReadOnly = true
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PartName",
                HeaderText = "Название детали",
                DataPropertyName = "PartName",
                ReadOnly = true
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "QualityName",
                HeaderText = "Качество",
                DataPropertyName = "QualityName",
                ReadOnly = true,
                Width = 120
            });
            dataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Details",
                HeaderText = "Детали",
                Text = "Подробнее",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(40, 167, 69) }
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
            btnOpenFilterDialog.BackColor = Color.FromArgb(25, 118, 210);
            btnOpenFilterDialog.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnExport.BackColor = Color.FromArgb(40, 167, 69);
            btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnManageQualities.BackColor = Color.FromArgb(25, 118, 210);
            btnManageQualities.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);

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

        private void BtnManageQualities_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new PartsQualitiesDialog(_viewModel))
                {
                    dialog.ShowDialog();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "управлении качествами");
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int id = (int)dataGridView.Rows[e.RowIndex].Tag;

            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == "Details")
                {
                    using (var dialog = new PartDetailsDialog(_viewModel, id))
                    {
                        dialog.ShowDialog();
                    }
                }
                else if (dataGridView.Columns[e.ColumnIndex].Name == "Actions")
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
                        using (var dialog = new PartsDialog(_viewModel, id, isDeleteMode: true))
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
            if (column == "Actions" || column == "Details") return;

            _sortOrder = _sortColumn == column && _sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            _sortColumn = column;
            SortDataSource();
            RefreshDataGridView();
        }

        private void BtnOpenFilterDialog_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new PartsFilterDialog(_viewModel))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        ApplyFilters(dialog.BrandId, dialog.QualityId);
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "открытии фильтров");
            }
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
                    saveFileDialog.FileName = $"Parts_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
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

        public void PerformSearch(string searchText)
        {
            try
            {
                _dataSource = _viewModel.SearchParts(searchText);
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "поиске");
            }
        }

        public void ApplyFilters(int? brandId, int? qualityId)
        {
            try
            {
                _dataSource = _viewModel.FilterParts(null, null, brandId, qualityId);
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "применении фильтров");
            }
        }

        public void ApplyFilters(int? minYear, int? maxYear, int? brandId, int? qualityId)
        {
            try
            {
                _dataSource = _viewModel.FilterParts(minYear, maxYear, brandId, qualityId);
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "применении фильтров");
            }
        }

        public void ExportData(string filePath)
        {
            try
            {
                _viewModel.ExportToExcel(_dataSource, filePath);
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
                using (var dialog = new PartsDialog(_viewModel, id))
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
                case "PartID":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.PartID.CompareTo(y.PartID) : y.PartID.CompareTo(x.PartID));
                    break;
                case "CarBrandName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.CarBrandName, y.CarBrandName) : string.Compare(y.CarBrandName, x.CarBrandName));
                    break;
                case "CatalogNumber":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.CatalogNumber, y.CatalogNumber) : string.Compare(y.CatalogNumber, x.CatalogNumber));
                    break;
                case "PartName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.PartName, y.PartName) : string.Compare(y.PartName, x.PartName));
                    break;
                case "QualityName":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.QualityName, y.QualityName) : string.Compare(y.QualityName, x.QualityName));
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
                row.Cells[dataGridView.Columns["PartID"].Index].Value = part.PartID;
                row.Cells[dataGridView.Columns["CarBrandName"].Index].Value = part.CarBrandName;
                row.Cells[dataGridView.Columns["CatalogNumber"].Index].Value = part.CatalogNumber;
                row.Cells[dataGridView.Columns["PartName"].Index].Value = part.PartName;
                row.Cells[dataGridView.Columns["QualityName"].Index].Value = part.QualityName;
                row.Cells[dataGridView.Columns["Details"].Index].Value = "Подробнее";
                row.Tag = part.PartID;
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Детали: {_dataSource.Count}";
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
using AvtoServis.Data.Interfaces.SuppliersInterface;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class SuppliersControl : UserControl, ISuppliersUserInterface
    {
        private readonly SuppliersViewModel _viewModel;
        private readonly ImageList _actionImageList;
        private List<Supplier> _dataSource;
        private System.Windows.Forms.Timer _searchTimer;
        private string _sortColumn = "SupplierID";
        private SortOrder _sortOrder = SortOrder.Ascending;

        public SuppliersControl(SuppliersViewModel viewModel, ImageList actionImageList)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _actionImageList = actionImageList ?? throw new ArgumentNullException(nameof(actionImageList));
            _dataSource = new List<Supplier>();
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
                Name = "SupplierID",
                HeaderText = "ID",
                DataPropertyName = "SupplierID",
                ReadOnly = true,
                Width = 80
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "Название",
                DataPropertyName = "Name",
                ReadOnly = true,
                Width = 150
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ContactPhone",
                HeaderText = "Контактный телефон",
                DataPropertyName = "ContactPhone",
                ReadOnly = true
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Электронная почта",
                DataPropertyName = "Email",
                ReadOnly = true
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Адрес",
                DataPropertyName = "Address",
                ReadOnly = true,
                Width = 200
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

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int id = (int)dataGridView.Rows[e.RowIndex].Tag;

            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == "Details")
                {
                    using (var dialog = new SupplierDetailsDialog(_viewModel, id))
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
                        using (var dialog = new SuppliersDialog(_viewModel, id, isDeleteMode: true))
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
                using (var dialog = new SuppliersFilterDialog(_viewModel))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        ApplyFilters(dialog.Name);
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
                    saveFileDialog.FileName = $"Suppliers_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
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
                _dataSource = _viewModel.LoadSuppliers();
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
                _dataSource = _viewModel.SearchSuppliers(searchText);
                SortDataSource();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "поиске");
            }
        }

        public void ApplyFilters(string name)
        {
            try
            {
                _dataSource = _viewModel.FilterSuppliers(name);
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
                using (var dialog = new SuppliersDialog(_viewModel, id))
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
                case "SupplierID":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? x.SupplierID.CompareTo(y.SupplierID) : y.SupplierID.CompareTo(x.SupplierID));
                    break;
                case "Name":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Name, y.Name) : string.Compare(y.Name, x.Name));
                    break;
                case "ContactPhone":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.ContactPhone, y.ContactPhone) : string.Compare(y.ContactPhone, x.ContactPhone));
                    break;
                case "Email":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Email, y.Email) : string.Compare(y.Email, x.Email));
                    break;
                case "Address":
                    _dataSource.Sort((x, y) => _sortOrder == SortOrder.Ascending ? string.Compare(x.Address, y.Address) : string.Compare(y.Address, x.Address));
                    break;
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            foreach (var supplier in _dataSource)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                row.Cells[dataGridView.Columns["SupplierID"].Index].Value = supplier.SupplierID;
                row.Cells[dataGridView.Columns["Name"].Index].Value = supplier.Name;
                row.Cells[dataGridView.Columns["ContactPhone"].Index].Value = supplier.ContactPhone;
                row.Cells[dataGridView.Columns["Email"].Index].Value = supplier.Email;
                row.Cells[dataGridView.Columns["Address"].Index].Value = supplier.Address;
                row.Cells[dataGridView.Columns["Details"].Index].Value = "Подробнее";
                row.Tag = supplier.SupplierID;
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Поставщики: {_dataSource.Count}";
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
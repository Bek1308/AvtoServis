using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using AvtoServis.Forms.Modals.Manufacturers;

namespace AvtoServis.Forms.Controls
{
    public partial class ManufacturerControl : UserControl
    {
        private readonly ManufacturersViewModel _viewModel;
        private readonly ImageList _actionImageList;
        private List<Manufacturer> dataSource;
        private System.Windows.Forms.Timer _searchTimer;

        public ManufacturerControl(ManufacturersViewModel viewModel, ImageList actionImageList)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _actionImageList = actionImageList ?? throw new ArgumentNullException(nameof(actionImageList));
            dataSource = new List<Manufacturer>();
            InitializeComponent();
            ConfigureColumns();
            EnhanceVisualStyles();
            InitializeSearch();
            OptimizeDataGridView();
            LoadData();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            addButton.Click += (s, e) => ShowManufacturerDialog(null);
            dataGridView.CellClick += DataGridView_CellClick;
            btnOpenFilterDialog.Click += BtnOpenFilterDialog_Click;
            btnRefresh.Click += (s, e) => LoadData();
        }

        private void ConfigureColumns()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nomer",
                HeaderText = "Номер",
                ReadOnly = true,
                Width = 80
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "Название производителя",
                DataPropertyName = "Name",
                ReadOnly = true
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
            _searchTimer.Tick += (s, e) =>
            {
                _searchTimer.Stop();
                PerformSearch();
            };
            searchBox.TextChanged += (s, e) =>
            {
                if (searchBox.Text != "Поиск...")
                {
                    _searchTimer.Stop();
                    _searchTimer.Start();
                }
            };
            searchBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    _searchTimer.Stop();
                    PerformSearch();
                }
            };
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

        private void BtnOpenFilterDialog_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new FilterDialog())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        dataSource = _viewModel.FilterManufacturers(dialog.SortAlphabetically);
                        RefreshDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия фильтров: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadData()
        {
            try
            {
                dataSource = _viewModel.LoadManufacturers();
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
                dataSource = _viewModel.SearchManufacturers(searchBox.Text.Trim());
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "поиске");
            }
        }

        private void RefreshDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            foreach (var manufacturer in dataSource)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                row.Cells[dataGridView.Columns["Nomer"].Index].Value = manufacturer.ManufacturerID;
                row.Cells[dataGridView.Columns["Name"].Index].Value = manufacturer.Name;
                row.Tag = manufacturer.ManufacturerID;
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Производителей: {dataSource.Count}";
            dataGridView.Refresh();
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
                editItem.Click += (s, ev) => ShowManufacturerDialog(id);
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
                    using (var dialog = new ManufacturerDialog(_viewModel, id, isDeleteMode: true))
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

        private void ShowManufacturerDialog(int? id)
        {
            try
            {
                using (var dialog = new ManufacturerDialog(_viewModel, id))
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

        private class FilterDialog : Form
        {
            public bool SortAlphabetically { get; private set; }

            private TableLayoutPanel tableLayoutPanel;
            private CheckBox chkSortAlphabetically;
            private Button btnApply;
            private Button btnCancel;

            public FilterDialog()
            {
                InitializeComponent();
                AddToolTips();
            }

            private void InitializeComponent()
            {
                Text = "Фильтры";
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                StartPosition = FormStartPosition.CenterParent;
                Size = new Size(400, 180);
                BackColor = Color.FromArgb(245, 245, 245);
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

                tableLayoutPanel = new TableLayoutPanel
                {
                    ColumnCount = 4,
                    RowCount = 3,
                    Dock = DockStyle.Fill,
                    Padding = new Padding(16),
                    AutoSize = true
                };
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

                chkSortAlphabetically = new CheckBox
                {
                    Text = "Сортировать по алфавиту",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    AccessibleName = "Сортировка по алфавиту",
                    AccessibleDescription = "Сортировать производителей по названию в алфавитном порядке"
                };
                tableLayoutPanel.Controls.Add(chkSortAlphabetically, 0, 0);
                tableLayoutPanel.SetColumnSpan(chkSortAlphabetically, 2);

                btnApply = new Button
                {
                    Text = "Применить",
                    Size = new Size(100, 28),
                    BackColor = Color.FromArgb(25, 118, 210),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(50, 140, 230) },
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    AccessibleName = "Применить фильтр",
                    AccessibleDescription = "Применяет фильтр сортировки"
                };
                btnApply.Click += BtnApply_Click;
                tableLayoutPanel.Controls.Add(btnApply, 2, 2);

                btnCancel = new Button
                {
                    Text = "Отмена",
                    Size = new Size(100, 28),
                    BackColor = Color.FromArgb(108, 117, 125),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(130, 140, 150) },
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    AccessibleName = "Отменить",
                    AccessibleDescription = "Закрывает окно без применения фильтров"
                };
                btnCancel.Click += (s, e) => Close();
                tableLayoutPanel.Controls.Add(btnCancel, 1, 2);

                Controls.Add(tableLayoutPanel);
            }

            private void AddToolTips()
            {
                var toolTip = new ToolTip();
                toolTip.SetToolTip(chkSortAlphabetically, "Сортировать производителей по названию в алфавитном порядке");
                toolTip.SetToolTip(btnApply, "Применить выбранные фильтры");
                toolTip.SetToolTip(btnCancel, "Закрыть без применения фильтров");
            }

            private void BtnApply_Click(object sender, EventArgs e)
            {
                try
                {
                    SortAlphabetically = chkSortAlphabetically.Checked;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка применения фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
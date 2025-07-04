using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class StockFilterDialog : Form
    {
        private readonly StockViewModel _viewModel;
        private readonly List<(ComboBox Column, TextBox SearchText, Button RemoveButton)> _filters;
        private System.Windows.Forms.Timer _errorTimer;
        public List<(string Column, string SearchText)> Filters { get; private set; }

        public StockFilterDialog(StockViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _filters = new List<(ComboBox, TextBox, Button)>();
            Filters = _viewModel.Filters?.ToList() ?? new List<(string, string)>();
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            SetToolTips();
            LoadExistingFilters();
            if (!_filters.Any())
            {
                AddFilterRow();
            }
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Форма для фильтрации складов");
            toolTip.SetToolTip(titleLabel, "Заголовок фильтров складов");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(flowLayoutFilters, "Добавьте фильтры для поиска");
            toolTip.SetToolTip(btnAddFilter, "Добавить новый фильтр");
            toolTip.SetToolTip(btnReset, "Сбросить все фильтры");
            toolTip.SetToolTip(btnApply, "Применить выбранные фильтры");
            toolTip.SetToolTip(lblError, "Сообщение об ошибке");
        }

        private void LoadExistingFilters()
        {
            var columnDisplayNames = new Dictionary<string, string>
            {
                { "StockID", "ID" },
                { "Name", "Название" }
            };
            var reverseColumnNames = columnDisplayNames.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

            foreach (var filter in Filters)
            {
                if (!_viewModel.VisibleColumns.Contains(filter.Column))
                {
                    continue;
                }

                var panel = new FlowLayoutPanel
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    Margin = new Padding(0, 5, 0, 5),
                    Size = new Size(396, 40)
                };

                var cmbColumn = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(150, 30),
                    Margin = new Padding(0, 0, 10, 0)
                };
                foreach (var col in _viewModel.VisibleColumns)
                {
                    if (columnDisplayNames.ContainsKey(col))
                    {
                        cmbColumn.Items.Add(columnDisplayNames[col]);
                    }
                }
                cmbColumn.SelectedItem = columnDisplayNames[filter.Column];

                var txtSearch = new TextBox
                {
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(190, 30),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0, 0, 5, 0),
                    Text = filter.SearchText
                };

                var btnRemove = new Button
                {
                    Text = "X",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Size = new Size(30, 30),
                    BackColor = Color.FromArgb(220, 53, 69),
                    FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(255, 77, 77) },
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    Margin = new Padding(0)
                };
                btnRemove.Click += (s, e) =>
                {
                    flowLayoutFilters.Controls.Remove(panel);
                    _filters.Remove((cmbColumn, txtSearch, btnRemove));
                    AdjustFormSize();
                };
                toolTip.SetToolTip(btnRemove, "Удалить этот фильтр");

                panel.Controls.Add(cmbColumn);
                panel.Controls.Add(txtSearch);
                panel.Controls.Add(btnRemove);
                flowLayoutFilters.Controls.Add(panel);
                _filters.Add((cmbColumn, txtSearch, btnRemove));
            }

            AdjustFormSize();
        }

        private void AddFilterRow()
        {
            try
            {
                var columnDisplayNames = new Dictionary<string, string>
                {
                    { "StockID", "ID" },
                    { "Name", "Название" }
                };
                var reverseColumnNames = columnDisplayNames.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

                if (_viewModel.VisibleColumns == null || !_viewModel.VisibleColumns.Any())
                {
                    ShowError("Нет доступных столбцов для фильтрации. Выберите столбцы в таблице.");
                    return;
                }

                var availableColumns = _viewModel.VisibleColumns
                    .Where(c => !_filters.Any(f => reverseColumnNames.ContainsKey(f.Column.SelectedItem?.ToString()) && reverseColumnNames[f.Column.SelectedItem.ToString()] == c))
                    .ToList();

                if (availableColumns.Count == 0)
                {
                    ShowError("Все доступные столбцы уже добавлены.");
                    return;
                }

                var panel = new FlowLayoutPanel
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    Margin = new Padding(0, 5, 0, 5),
                    Size = new Size(396, 40)
                };

                var cmbColumn = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(150, 30),
                    Margin = new Padding(0, 0, 10, 0)
                };
                foreach (var col in availableColumns)
                {
                    if (columnDisplayNames.ContainsKey(col))
                    {
                        cmbColumn.Items.Add(columnDisplayNames[col]);
                    }
                }
                cmbColumn.SelectedIndex = 0;

                var txtSearch = new TextBox
                {
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(190, 30),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0, 0, 5, 0)
                };

                var btnRemove = new Button
                {
                    Text = "X",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Size = new Size(30, 30),
                    BackColor = Color.FromArgb(220, 53, 69),
                    FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(255, 77, 77) },
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    Margin = new Padding(0)
                };
                btnRemove.Click += (s, e) =>
                {
                    flowLayoutFilters.Controls.Remove(panel);
                    _filters.Remove((cmbColumn, txtSearch, btnRemove));
                    AdjustFormSize();
                };
                toolTip.SetToolTip(btnRemove, "Удалить этот фильтр");

                panel.Controls.Add(cmbColumn);
                panel.Controls.Add(txtSearch);
                panel.Controls.Add(btnRemove);
                flowLayoutFilters.Controls.Add(panel);
                _filters.Add((cmbColumn, txtSearch, btnRemove));

                AdjustFormSize();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при добавлении фильтра: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"AddFilterRow Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void AdjustFormSize()
        {
            int filterHeight = _filters.Count * (40 + 10);
            int baseHeight = 35 + 2 + 65 + 17 + 48;
            int totalHeight = Math.Max(228, Math.Min(524, filterHeight + baseHeight));
            ClientSize = new Size(455, totalHeight);
            tableLayoutPanel.Size = new Size(455, totalHeight);
        }

        private void BtnAddFilter_Click(object sender, EventArgs e)
        {
            AddFilterRow();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                var columnDisplayNames = new Dictionary<string, string>
                {
                    { "StockID", "ID" },
                    { "Name", "Название" }
                };
                var reverseColumnNames = columnDisplayNames.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

                Filters = _filters
                    .Where(f => !string.IsNullOrWhiteSpace(f.SearchText.Text))
                    .Select(f => (reverseColumnNames[f.Column.SelectedItem?.ToString()], f.SearchText.Text))
                    .ToList();

                _viewModel.Filters = Filters;
                _viewModel.LoadStocks(); // Joriy filtrlar asosida ma'lumotlarni qayta yuklash
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при применении фильтров: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"BtnApply_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                flowLayoutFilters.Controls.Clear();
                _filters.Clear();
                Filters.Clear();
                _viewModel.Filters = null;
                AddFilterRow();
                _viewModel.LoadStocks(); // Filtrlarni tozalab, ma'lumotlarni qayta yuklash
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при сбросе фильтров: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"BtnReset_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            _errorTimer.Stop();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            _errorTimer.Stop();
            _errorTimer.Start();
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
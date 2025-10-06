using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace AvtoServis.Forms.Controls
{
    public partial class PartExpensesFilterDialog : Form
    {
        private readonly PartExpensesViewModel _viewModel;
        private readonly List<(ComboBox Column, ComboBox Operator, Control InputControl, Button RemoveButton)> _filters;
        private readonly Dictionary<string, string> _columnMapping;
        private System.Windows.Forms.Timer _errorTimer;

        public List<(string Column, string Operator, string SearchText)> Filters { get; private set; }

        public PartExpensesFilterDialog(PartExpensesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _filters = new List<(ComboBox, ComboBox, Control, Button)>();
            Filters = _viewModel.Filters?.ToList() ?? new List<(string, string, string)>();
            _columnMapping = new Dictionary<string, string>
            {
                { "SaleId", "ID" },
                { "PartName", "Название детали" },
                { "Quantity", "Количество" },
                { "UnitPrice", "Цена за единицу" },
                { "TotalAmount", "Общая сумма" },
                { "PaymentStatusId", "Толлов холати" },
                { "SaleDate", "Дата продажи" },
                { "Manufacturer", "Производитель" },
                { "CustomerName", "Имя клиента" },
                { "CustomerPhone", "Телефон клиента" },
                { "CatalogNumber", "Каталожный номер" },
                { "CarBrand", "Марка автомобиля" },
                { "Status", "Статус" },
                { "Seller", "Продавец" },
                { "InvoiceNumber", "Номер счета" }
            };
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
            toolTip.SetToolTip(tableLayoutPanel, "Форма для фильтрации расходов на детали");
            toolTip.SetToolTip(titleLabel, "Заголовок фильтров расходов");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(flowLayoutFilters, "Добавьте фильтры для поиска");
            toolTip.SetToolTip(btnAddFilter, "Добавить новый фильтр");
            toolTip.SetToolTip(btnReset, "Сбросить все фильтры");
            toolTip.SetToolTip(btnApply, "Применить выбранные фильтры");
            toolTip.SetToolTip(lblError, "Сообщение об ошибке");
        }

        private void LoadExistingFilters()
        {
            var reverseColumnNames = _columnMapping.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

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
                    Size = new Size(120, 30),
                    Margin = new Padding(0, 0, 5, 0)
                };
                foreach (var col in _viewModel.VisibleColumns)
                {
                    if (_columnMapping.ContainsKey(col))
                    {
                        cmbColumn.Items.Add(_columnMapping[col]);
                    }
                }
                cmbColumn.SelectedItem = _columnMapping[filter.Column];

                var cmbOperator = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(80, 30),
                    Margin = new Padding(0, 0, 5, 0)
                };

                Control inputControl;
                if (filter.Column == "SaleDate")
                {
                    var datePicker = new DateTimePicker
                    {
                        Font = new Font("Segoe UI", 10F),
                        Size = new Size(150, 30),
                        Margin = new Padding(0, 0, 5, 0),
                        Format = DateTimePickerFormat.Short
                    };
                    if (DateTime.TryParse(filter.SearchText, out var date))
                    {
                        datePicker.Value = date;
                    }
                    inputControl = datePicker;
                }
                else if (filter.Column == "PaymentStatusId")
                {
                    var comboBoxStatus = new ComboBox
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Font = new Font("Segoe UI", 10F),
                        Size = new Size(150, 30),
                        Margin = new Padding(0, 0, 5, 0)
                    };
                    comboBoxStatus.Items.AddRange(new[] { "Оплачен", "Не оплачен", "Частично Оплачен" });
                    comboBoxStatus.SelectedItem = filter.SearchText;
                    inputControl = comboBoxStatus;
                }
                else
                {
                    inputControl = new TextBox
                    {
                        Font = new Font("Segoe UI", 10F),
                        Size = new Size(150, 30),
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(0, 0, 5, 0),
                        Text = filter.SearchText
                    };
                }

                UpdateOperatorComboBox(cmbColumn, cmbOperator, filter.Operator);

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
                    _filters.Remove((cmbColumn, cmbOperator, inputControl, btnRemove));
                    UpdateAvailableColumns();
                    AdjustFormSize();
                };
                toolTip.SetToolTip(btnRemove, "Удалить этот фильтр");

                panel.Controls.Add(cmbColumn);
                panel.Controls.Add(cmbOperator);
                panel.Controls.Add(inputControl);
                panel.Controls.Add(btnRemove);
                flowLayoutFilters.Controls.Add(panel);
                _filters.Add((cmbColumn, cmbOperator, inputControl, btnRemove));

                cmbColumn.SelectedIndexChanged += (s, e) => UpdateInputControlAndOperator(cmbColumn, panel, cmbOperator, btnRemove);
            }

            UpdateAvailableColumns();
            AdjustFormSize();
        }

        private void UpdateOperatorComboBox(ComboBox cmbColumn, ComboBox cmbOperator, string savedOperator = null)
        {
            cmbOperator.Items.Clear();
            if (cmbColumn.SelectedItem?.ToString() == "ID" ||
                cmbColumn.SelectedItem?.ToString() == "Количество" ||
                cmbColumn.SelectedItem?.ToString() == "Цена за единицу" ||
                cmbColumn.SelectedItem?.ToString() == "Общая сумма")
            {
                cmbOperator.Items.AddRange(new[] { "=", ">", "<", ">=", "<=", "!=" });
            }
            else if (cmbColumn.SelectedItem?.ToString() == "Толлов холати")
            {
                cmbOperator.Items.AddRange(new[] { "=", "!=" });
            }
            else if (cmbColumn.SelectedItem?.ToString() == "Дата продажи")
            {
                cmbOperator.Items.AddRange(new[] { "=", ">", "<", ">=", "<=" });
            }
            else
            {
                cmbOperator.Items.Add("LIKE");
            }

            if (savedOperator != null && cmbOperator.Items.Contains(savedOperator))
            {
                cmbOperator.SelectedItem = savedOperator;
            }
            else if (cmbOperator.Items.Count > 0)
            {
                cmbOperator.SelectedIndex = 0;
            }
        }

        private void UpdateInputControlAndOperator(ComboBox cmbColumn, FlowLayoutPanel panel, ComboBox cmbOperator, Button btnRemove)
        {
            var oldInputControl = panel.Controls.OfType<Control>().Skip(2).First();
            Control newInputControl;
            string currentValue = oldInputControl is TextBox textBox ? textBox.Text :
                                 oldInputControl is DateTimePicker datePicker ? datePicker.Value.ToString("yyyy-MM-dd") :
                                 oldInputControl is ComboBox comboBox ? comboBox.SelectedItem?.ToString() : string.Empty;

            if (cmbColumn.SelectedItem?.ToString() == "Дата продажи")
            {
                newInputControl = new DateTimePicker
                {
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(150, 30),
                    Margin = new Padding(0, 0, 5, 0),
                    Format = DateTimePickerFormat.Short
                };
                if (DateTime.TryParse(currentValue, out var date))
                {
                    ((DateTimePicker)newInputControl).Value = date;
                }
            }
            else if (cmbColumn.SelectedItem?.ToString() == "Толлов холати")
            {
                newInputControl = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(150, 30),
                    Margin = new Padding(0, 0, 5, 0)
                };
                ((ComboBox)newInputControl).Items.AddRange(new[] { "Оплачен", "Не оплачен", "Частично Оплачен" });
                if (((ComboBox)newInputControl).Items.Contains(currentValue))
                {
                    ((ComboBox)newInputControl).SelectedItem = currentValue;
                }
            }
            else
            {
                newInputControl = new TextBox
                {
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(150, 30),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0, 0, 5, 0),
                    Text = currentValue
                };
            }

            panel.Controls.Remove(oldInputControl);
            panel.Controls.Add(newInputControl);
            panel.Controls.SetChildIndex(newInputControl, 2);

            var index = _filters.FindIndex(f => f.Column == cmbColumn);
            if (index >= 0)
            {
                _filters[index] = (cmbColumn, cmbOperator, newInputControl, btnRemove);
            }

            UpdateOperatorComboBox(cmbColumn, cmbOperator, null);
        }

        private void UpdateAvailableColumns()
        {
            var updatedFilters = new List<(ComboBox Column, ComboBox Operator, Control InputControl, Button RemoveButton)>();

            foreach (var filter in _filters.ToList())
            {
                var cmbColumn = filter.Column;
                var currentSelection = cmbColumn.SelectedItem?.ToString();
                var currentOperator = filter.Operator.SelectedItem?.ToString();
                cmbColumn.Items.Clear();
                foreach (var col in _viewModel.VisibleColumns)
                {
                    if (_columnMapping.ContainsKey(col))
                    {
                        cmbColumn.Items.Add(_columnMapping[col]);
                    }
                }
                if (currentSelection != null && cmbColumn.Items.Contains(currentSelection))
                {
                    cmbColumn.SelectedItem = currentSelection;
                }
                else if (cmbColumn.Items.Count > 0)
                {
                    cmbColumn.SelectedIndex = 0;
                }

                updatedFilters.Add((filter.Column, filter.Operator, filter.InputControl, filter.RemoveButton));
            }

            _filters.Clear();
            _filters.AddRange(updatedFilters);
        }

        private void AddFilterRow()
        {
            try
            {
                if (_viewModel.VisibleColumns == null || !_viewModel.VisibleColumns.Any())
                {
                    ShowError("Нет доступных столбцов для фильтрации. Выберите столбцы в таблице.");
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
                    Size = new Size(120, 30),
                    Margin = new Padding(0, 0, 5, 0)
                };
                foreach (var col in _viewModel.VisibleColumns)
                {
                    if (_columnMapping.ContainsKey(col))
                    {
                        cmbColumn.Items.Add(_columnMapping[col]);
                    }
                }
                if (cmbColumn.Items.Count > 0)
                    cmbColumn.SelectedIndex = 0;

                var cmbOperator = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(80, 30),
                    Margin = new Padding(0, 0, 5, 0)
                };

                Control inputControl = new TextBox
                {
                    Font = new Font("Segoe UI", 10F),
                    Size = new Size(150, 30),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0, 0, 5, 0)
                };
                UpdateOperatorComboBox(cmbColumn, cmbOperator);

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
                    _filters.Remove((cmbColumn, cmbOperator, inputControl, btnRemove));
                    UpdateAvailableColumns();
                    AdjustFormSize();
                };
                toolTip.SetToolTip(btnRemove, "Удалить этот фильтр");

                panel.Controls.Add(cmbColumn);
                panel.Controls.Add(cmbOperator);
                panel.Controls.Add(inputControl);
                panel.Controls.Add(btnRemove);
                flowLayoutFilters.Controls.Add(panel);
                _filters.Add((cmbColumn, cmbOperator, inputControl, btnRemove));

                cmbColumn.SelectedIndexChanged += (s, e) => UpdateInputControlAndOperator(cmbColumn, panel, cmbOperator, btnRemove);

                UpdateAvailableColumns();
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
            int errorHeight = lblError.Visible ? 30 : 0;
            int baseHeight = 35 + 2 + 65 + 80 + 20;
            int totalHeight = Math.Max(228, Math.Min(600, filterHeight + baseHeight + errorHeight));
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
                var reverseColumnNames = _columnMapping.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

                Filters = _filters
                    .Where(f => f.InputControl is TextBox textBox && !string.IsNullOrWhiteSpace(textBox.Text) ||
                                f.InputControl is DateTimePicker ||
                                f.InputControl is ComboBox comboBox && comboBox.SelectedItem != null)
                    .Select(f =>
                    {
                        string searchText;
                        if (f.InputControl is DateTimePicker datePicker)
                        {
                            searchText = datePicker.Value.ToString("yyyy-MM-dd");
                        }
                        else if (f.InputControl is ComboBox comboBox)
                        {
                            searchText = comboBox.SelectedItem?.ToString();
                        }
                        else
                        {
                            searchText = ((TextBox)f.InputControl).Text;
                        }
                        return (reverseColumnNames[f.Column.SelectedItem?.ToString()], f.Operator.SelectedItem?.ToString(), searchText);
                    })
                    .ToList();

                if (!Filters.Any())
                {
                    ShowError("Добавьте хотя бы один фильтр с текстом поиска, датой или статусом.");
                    return;
                }

                _viewModel.Filters = Filters;
                _viewModel.LoadPartExpenses();
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
                _viewModel.LoadPartExpenses();
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
            AdjustFormSize();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            _errorTimer.Stop();
            _errorTimer.Start();
            AdjustFormSize();
        }
    }
}
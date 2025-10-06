using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using Timer = System.Windows.Forms.Timer;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Collections.ObjectModel;
using AvtoServis.Data.Interfaces;
using AvtoServis.Model.DTOs;
using AvtoServis.ViewModels.Screens;

namespace AvtoServis.Forms
{
    public partial class SaleForm : Form
    {
        private readonly SaleViewModel _viewModel;
        private readonly ICustomerRepository customerRepository;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private bool suppressTextChanged = false;
        private bool isCustomerSelected = false;
        private CustomerInfo selectedCustomer = null;
        private readonly Timer _notificationTimer;
        private string _currentField = "grid";
        private string _editingBuffer = string.Empty;
        private string _lastCustomerLabelText = string.Empty; // Oldingi mijoz labelini saqlash uchun
        private decimal DefaultDiscountPercentage = 0.1m;

        public SaleForm(SaleViewModel viewModel, ICustomerRepository customerRepository)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            _viewModel = viewModel;
            Console.WriteLine("SaleForm: Starting initialization...");

            InitializeComponent();
            if (selectedProductsGrid == null || popularProductsGrid == null || numericKeypadLayout == null || totalAmountLabel == null || notificationLabel == null || incompleteSalesFlow == null)
            {
                throw new InvalidOperationException("UI komponentlari noto‘g‘ri inicializatsiya qilingan. Designer faylini tekshiring.");
            }

            _notificationTimer = new System.Windows.Forms.Timer { Interval = 5000 };
            _notificationTimer.Tick += (s, e) => ClearNotification();

            BindViewModel();
            SetToolTips();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            foreach (Control control in numericKeypadLayout.Controls)
            {
                if (control is Button button)
                {
                    button.TabStop = false;
                    button.CausesValidation = false;
                }
            }

            _currentField = "grid";
            UpdateTotalAmountLabel();
            LoadIncompleteSalesButtons();
            _viewModel.LoadSelectedProductsFromJson();
            this.customerRepository = customerRepository;
            InitializeComboBoxSearch();
            this.Load += Form1_Load;

            Console.WriteLine("SaleForm: Initialization completed.");
        }

        private void InitializeComboBoxSearch()
        {
            comboBox1.AutoCompleteMode = AutoCompleteMode.None;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;

            comboBox1.TextChanged += ComboBox1_TextChanged;
            comboBox1.SelectionChangeCommitted += ComboBox1_SelectionChangeCommitted;
            comboBox1.KeyDown += ComboBox1_KeyDown;

            comboBox1.MouseEnter += (s, e) => Cursor.Current = Cursors.IBeam;
            comboBox1.MouseMove += (s, e) => Cursor.Current = Cursors.IBeam;
            comboBox1.Enter += (s, e) => Cursor.Current = Cursors.IBeam;
            comboBox1.DropDown += (s, e) => Cursor.Current = Cursors.IBeam;
        }

        private void ComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && selectedCustomer != null && isCustomerSelected)
            {
                ShowCustomerDetails();
                incompleteSalesTitleLabel.Text = $"Незавершенные продажи ({selectedCustomer?.FullName})";
                SetCustomerLabel(); // Labelni yangilash
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer cursorTimer = new Timer();
            cursorTimer.Interval = 300;
            cursorTimer.Tick += (s, ev) =>
            {
                if (comboBox1.Focused || comboBox1.DroppedDown)
                {
                    if (Cursor.Current != Cursors.IBeam)
                        Cursor.Current = Cursors.IBeam;
                }
            };
            cursorTimer.Start();
        }

        private async void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (suppressTextChanged)
            {
                suppressTextChanged = false;
                return;
            }

            cts.Cancel();
            cts = new CancellationTokenSource();
            var token = cts.Token;

            string searchText = comboBox1.Text?.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                comboBox1.DroppedDown = false;
                comboBox1.Items.Clear();
                if (isCustomerSelected)
                {
                    // Faqat foydalanuvchi aniq tozalashni xohlasa holatni o'zgartiramiz
                    // Bu yerda hech narsa qilmaymiz, chunki mijoz tanlangan bo'lsa, label saqlanadi
                }
                else
                {
                    selectedCustomer = null;
                    isCustomerSelected = false;
                    SetCustomerLabel(); // Labelni yashirish
                }
                return;
            }

            try
            {
                await Task.Delay(300, token);

                var results = await customerRepository.SearchCustomersAsync(searchText);

                if (token.IsCancellationRequested) return;

                string currentText = comboBox1.Text;
                int selStart = comboBox1.SelectionStart;

                comboBox1.BeginInvoke(() =>
                {
                    comboBox1.Items.Clear();

                    if (results.Count > 0)
                    {
                        comboBox1.Items.AddRange(results.ToArray());
                    }
                    else
                    {
                        comboBox1.Items.Add(new CustomerInfo { FullName = "Данные не найдены", Phone = "" });
                    }

                    comboBox1.DroppedDown = true;

                    comboBox1.Text = currentText;
                    comboBox1.SelectionStart = selStart;
                    comboBox1.SelectionLength = 0;

                    Application.DoEvents();
                    Cursor.Current = Cursors.IBeam;
                });
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            suppressTextChanged = true;

            if (comboBox1.SelectedItem is CustomerInfo customer && customer.Phone != "")
            {
                selectedCustomer = customer;
                isCustomerSelected = true;
                _viewModel.SetCustomerInfo(selectedCustomer);
                ShowCustomerDetails();
                comboBox1.Text = string.Empty;
                SetCustomerLabel(); // Labelni darhol yangilash
            }
            else
            {
                // Tanlanmagan bo'lsa, holatni o'zgartirmaymiz, chunki label saqlanishi kerak
                if (!isCustomerSelected)
                {
                    selectedCustomer = null;
                    SetCustomerLabel(); // Labelni yashirish
                }
            }
        }

        private void ShowCustomerDetails()
        {
            if (selectedCustomer != null && isCustomerSelected)
            {
                MessageBox.Show($"Выбранный клиент:\n\n" +
                                $"👤 Имя: {selectedCustomer.FullName}\n" +
                                $"📞 Телефон: {selectedCustomer.Phone}\n" +
                                $"🆔 ID: {selectedCustomer.CustomerID}\n" +
                                $"📧 Email: {selectedCustomer.Email}\n" +
                                $"🏠 Адрес: {selectedCustomer.Address}\n" +
                                $"📅 Дата регистрации: {selectedCustomer.RegistrationDate:dd.MM.yyyy}\n" +
                                $"🔄 Активен: {(selectedCustomer.IsActive ? "Да" : "Нет")}",
                                "Информация о клиенте", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetCustomerLabel()
        {
            if (isCustomerSelected && selectedCustomer != null)
            {
                string customerLabelText = $"Клиент: {selectedCustomer.FullName} ({selectedCustomer.Phone})";
                if (customerLabelText != _lastCustomerLabelText) // Faqat o'zgarganda bildirishnoma ko'rsatamiz
                {
                    customerInfoLbl.Text = customerLabelText;
                    customerInfoLbl.Visible = true;
                    _lastCustomerLabelText = customerLabelText;
                    ShowSuccessMessage($"Выбран {customerLabelText}");
                }
            }
            else
            {
                customerInfoLbl.Visible = false;
                customerInfoLbl.Text = string.Empty;
                _lastCustomerLabelText = string.Empty;
            }
            customerInfoLbl.Refresh(); // UI ni majburlab yangilash
        }
        private void SetNewCustomerLabel()
        {
            string customerLabelText = $"Клиент: Новый клиент";
            if (customerLabelText != _lastCustomerLabelText) // Faqat o'zgarganda bildirishnoma ko'rsatamiz
            {
                customerInfoLbl.Text = customerLabelText;
                customerInfoLbl.Visible = true;
                _lastCustomerLabelText = customerLabelText;
                ShowSuccessMessage($"Выбран {customerLabelText}");
            }
            customerInfoLbl.Refresh(); // UI ni majburlab yangilash
        }

        private void NumericButton_Click(object sender, EventArgs e)
        {
            if (!(sender is Button button)) return;

            try
            {
                string buttonText = button.Text;

                if (button == btnNumClear)
                {
                    if (_currentField == "paidAmount")
                    {
                        _editingBuffer = string.IsNullOrEmpty(_editingBuffer) ? "0" : _editingBuffer.Substring(0, _editingBuffer.Length - 1);
                        if (string.IsNullOrEmpty(_editingBuffer)) _editingBuffer = "0";
                        paidAmountTextBox.Text = _editingBuffer;
                        _viewModel.PaidAmount = _editingBuffer;
                    }
                    else if (_currentField == "customerSearch")
                    {
                        _editingBuffer = string.Empty;
                        comboBox1.Text = string.Empty;
                        _viewModel.CustomerSearchText = string.Empty;
                        selectedCustomer = null;
                        isCustomerSelected = false;
                        SetCustomerLabel();
                    }
                    else if (_currentField == "grid" && selectedProductsGrid.CurrentCell != null)
                    {
                        string columnName = selectedProductsGrid.Columns[selectedProductsGrid.CurrentCell.ColumnIndex].Name;
                        int rowIndex = selectedProductsGrid.CurrentCell.RowIndex;
                        if (columnName == "Quantity")
                        {
                            _editingBuffer = string.IsNullOrEmpty(_editingBuffer) || _editingBuffer == "0" ? "0" : _editingBuffer.Substring(0, _editingBuffer.Length - 1);
                            if (string.IsNullOrEmpty(_editingBuffer)) _editingBuffer = "0";
                            selectedProductsGrid.CurrentCell.Value = _editingBuffer;
                            if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                            if (_editingBuffer == "0")
                            {
                                _viewModel.RemoveSelectedProduct(rowIndex);
                                ShowSuccessMessage("Товар удален, так как количество равно 0");
                            }
                        }
                        else if (columnName == "Price")
                        {
                            _editingBuffer = string.IsNullOrEmpty(_editingBuffer) ? "0" : _editingBuffer.Substring(0, _editingBuffer.Length - 1);
                            if (string.IsNullOrEmpty(_editingBuffer)) _editingBuffer = "0";
                            selectedProductsGrid.CurrentCell.Value = _editingBuffer;
                            if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        }
                        UpdateTotalAmountLabel();
                    }
                    return;
                }

                if (_currentField == "paidAmount")
                {
                    if (buttonText == "Ввод")
                    {
                        if (decimal.TryParse(_editingBuffer, out decimal paidAmount) && paidAmount >= 0)
                        {
                            paidAmount = Math.Round(paidAmount, 2);
                            _viewModel.ConfirmPaidAmount();
                            ShowSuccessMessage("Сумма оплаты подтверждена");
                        }
                        else
                        {
                            ShowErrorMessage("Некорректная сумма оплаты. Установлено: 0");
                            _editingBuffer = "0";
                            paidAmountTextBox.Text = _editingBuffer;
                            _viewModel.PaidAmount = _editingBuffer;
                        }
                        return;
                    }
                    if (buttonText == "." && _editingBuffer.Contains(".")) return;
                    _editingBuffer = _editingBuffer == "0" ? buttonText : _editingBuffer + buttonText;
                    if (_editingBuffer.Contains("."))
                    {
                        var parts = _editingBuffer.Split('.');
                        if (parts.Length > 1 && parts[1].Length > 2)
                        {
                            _editingBuffer = parts[0] + "." + parts[1].Substring(0, 2);
                        }
                    }
                    paidAmountTextBox.Text = _editingBuffer;
                    _viewModel.PaidAmount = _editingBuffer;
                }
                else if (_currentField == "customerSearch")
                {
                    if (buttonText == "Ввод")
                    {
                        if (selectedCustomer != null && isCustomerSelected)
                        {
                            ShowSuccessMessage("Поиск покупателя подтвержден");
                            SetCustomerLabel();
                        }
                        else
                        {
                            ShowErrorMessage("Покупатель не выбран");
                        }
                        return;
                    }
                    if (comboBox1.Text == "Найти покупателя...")
                    {
                        _viewModel.ClearCustomerSearchPlaceholder();
                        comboBox1.Text = "";
                        _editingBuffer = "";
                    }
                    _editingBuffer += buttonText;
                    comboBox1.Text = _editingBuffer;
                    _viewModel.CustomerSearchText = _editingBuffer;
                }
                else if (_currentField == "grid" && selectedProductsGrid.CurrentCell != null)
                {
                    string columnName = selectedProductsGrid.Columns[selectedProductsGrid.CurrentCell.ColumnIndex].Name;
                    int rowIndex = selectedProductsGrid.CurrentCell.RowIndex;
                    if (columnName == "Quantity" || columnName == "Price")
                    {
                        if (buttonText == "Ввод")
                        {
                            var product = _viewModel.SelectedProducts[rowIndex];
                            var fullProduct = _viewModel.GetPartById(product.ProductId);
                            if (fullProduct == null)
                            {
                                ShowErrorMessage("Товар не найден в базе данных");
                                _editingBuffer = columnName == "Quantity" ? "1" : product.Price.ToString();
                                selectedProductsGrid.CurrentCell.Value = _editingBuffer;
                                if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                                return;
                            }

                            if (columnName == "Quantity")
                            {
                                if (!int.TryParse(_editingBuffer, out int quantity) || quantity < 0)
                                {
                                    ShowErrorMessage("Количество должно быть неотрицательным целым числом. Установлено: 0");
                                    _editingBuffer = "0";
                                    selectedProductsGrid.CurrentCell.Value = _editingBuffer;
                                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                                    _viewModel.UpdateProductQuantity(rowIndex, 0);
                                    _viewModel.RemoveSelectedProduct(rowIndex);
                                    ShowSuccessMessage("Товар удален, так как количество равно 0");
                                    UpdateTotalAmountLabel();
                                }
                                else if (quantity > fullProduct.RemainingQuantity)
                                {
                                    ShowErrorMessage($"Количество не может превышать остаток на складе ({fullProduct.RemainingQuantity})");
                                    _editingBuffer = fullProduct.RemainingQuantity.ToString();
                                    selectedProductsGrid.CurrentCell.Value = _editingBuffer;
                                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                                    _viewModel.UpdateProductQuantity(rowIndex, fullProduct.RemainingQuantity);
                                    UpdateTotalAmountLabel();
                                }
                                else
                                {
                                    _viewModel.UpdateProductQuantity(rowIndex, quantity);
                                    selectedProductsGrid.CurrentCell.Value = quantity;
                                    _editingBuffer = quantity.ToString();
                                    if (quantity == 0)
                                    {
                                        _viewModel.RemoveSelectedProduct(rowIndex);
                                        ShowSuccessMessage("Товар удален, так как количество равно 0");
                                    }
                                    else
                                    {
                                        ShowSuccessMessage($"Количество товара '{product.ProductName}' обновлено: {quantity}");
                                    }
                                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                                    UpdateTotalAmountLabel();
                                }
                            }
                            else if (columnName == "Price")
                            {
                                decimal minPrice = (fullProduct.IncomeUnitPrice ?? 0) * (1 - DefaultDiscountPercentage);
                                if (!decimal.TryParse(_editingBuffer, out decimal price) || price < 0)
                                {
                                    ShowErrorMessage($"Цена должна быть положительным числом. Установлена минимальная цена: {minPrice:N2}");
                                    _viewModel.UpdateProductPrice(rowIndex, minPrice);
                                    selectedProductsGrid.CurrentCell.Value = minPrice;
                                    _editingBuffer = minPrice.ToString();
                                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                                    UpdateTotalAmountLabel();
                                }
                                else if (price < minPrice)
                                {
                                    ShowErrorMessage($"Цена не может быть меньше минимальной ({minPrice:N2})");
                                    _viewModel.UpdateProductPrice(rowIndex, minPrice);
                                    selectedProductsGrid.CurrentCell.Value = minPrice;
                                    _editingBuffer = minPrice.ToString();
                                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                                    UpdateTotalAmountLabel();
                                }
                                else
                                {
                                    price = Math.Round(price, 2);
                                    _viewModel.UpdateProductPrice(rowIndex, price);
                                    selectedProductsGrid.CurrentCell.Value = price;
                                    _editingBuffer = price.ToString();
                                    ShowSuccessMessage($"Цена товара '{product.ProductName}' обновлена: {price:N2}");
                                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                                    UpdateTotalAmountLabel();
                                }
                            }
                            return;
                        }
                        if (buttonText == "." && _editingBuffer.Contains(".")) return;
                        _editingBuffer = _editingBuffer == "0" ? buttonText : _editingBuffer + buttonText;
                        if (columnName == "Price" && _editingBuffer.Contains("."))
                        {
                            var parts = _editingBuffer.Split('.');
                            if (parts.Length > 1 && parts[1].Length > 2)
                            {
                                _editingBuffer = parts[0] + "." + parts[1].Substring(0, 2);
                            }
                        }
                        selectedProductsGrid.CurrentCell.Value = _editingBuffer;
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        if (columnName == "Quantity" && int.TryParse(_editingBuffer, out int tempQuantity) && tempQuantity >= 0)
                        {
                            if (tempQuantity == 0)
                            {
                                _viewModel.RemoveSelectedProduct(rowIndex);
                                ShowSuccessMessage("Товар удален, так как количество равно 0");
                            }
                            UpdateTotalAmountLabel();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Произошла ошибка: {ex.Message}");
                _editingBuffer = string.Empty;
            }
        }

        private void ShowSuccessMessage(string message)
        {
            notificationLabel.Text = message;
            notificationPanel.BackColor = Color.FromArgb(204, 255, 204);
            notificationLabel.ForeColor = Color.FromArgb(0, 100, 0);
            notificationPanel.Visible = true;
            _notificationTimer.Stop();
            _notificationTimer.Start();
        }

        private void ShowErrorMessage(string message)
        {
            notificationLabel.Text = message;
            notificationPanel.BackColor = Color.FromArgb(255, 204, 204);
            notificationLabel.ForeColor = Color.FromArgb(139, 0, 0);
            notificationPanel.Visible = true;
            _notificationTimer.Stop();
            _notificationTimer.Start();
        }

        private void ClearNotification()
        {
            notificationLabel.Text = "";
            notificationPanel.BackColor = Color.FromArgb(245, 245, 245);
            notificationLabel.ForeColor = Color.FromArgb(33, 37, 41);
            notificationPanel.Visible = false;
            _notificationTimer.Stop();
        }

        private void UpdateTotalAmountLabel()
        {
            try
            {
                decimal total = _viewModel.SelectedProducts?.Sum(product => product.Quantity * product.Price) ?? 0;
                totalAmountLabel.Text = $"Общая сумма: {total:N2}";
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при расчете общей суммы: {ex.Message}");
                totalAmountLabel.Text = "Общая сумма: 0.00";
            }
        }

        private void BindViewModel()
        {
            if (popularProductsGrid == null || selectedProductsGrid == null)
            {
                ShowErrorMessage("UI grid komponentlari noto‘g‘ri inicializatsiya qilingan");
                return;
            }

            popularProductsGrid.DataSource = _viewModel.PopularProducts ?? new ObservableCollection<FullParts>();
            selectedProductsGrid.DataSource = _viewModel.SelectedProducts ?? new List<SaleItemDto>();
            selectedProductInfoLabel.DataBindings.Add("Text", _viewModel, nameof(_viewModel.SelectedProductInfo));
            paidAmountTextBox.DataBindings.Add("Text", _viewModel, nameof(_viewModel.PaidAmount));

            ConfigurePopularProductsGrid();
            ConfigureSelectedProductsGrid();
            OptimizeDataGridView();

            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_viewModel.ColumnVisibility))
                    ConfigurePopularProductsGrid();
                else if (e.PropertyName == nameof(_viewModel.SelectedColumnVisibility))
                    ConfigureSelectedProductsGrid();
                else if (e.PropertyName == nameof(_viewModel.NotificationMessage))
                {
                    if (!string.IsNullOrEmpty(_viewModel.NotificationMessage))
                    {
                        if (_viewModel.NotificationMessage.Contains("Ошибка") || _viewModel.NotificationMessage.Contains("не"))
                            ShowErrorMessage(_viewModel.NotificationMessage);
                        else
                            ShowSuccessMessage(_viewModel.NotificationMessage);
                    }
                }
                else if (e.PropertyName == nameof(_viewModel.SelectedProducts))
                {
                    ConfigureSelectedProductsGrid();
                    UpdateTotalAmountLabel();
                }
            };

            selectedProductsGrid.CellBeginEdit += SelectedProductsGrid_CellBeginEdit;
            selectedProductsGrid.CellValidating += SelectedProductsGrid_CellValidating;
            selectedProductsGrid.CellEndEdit += SelectedProductsGrid_CellEndEdit;
            selectedProductsGrid.CellEnter += SelectedProductsGrid_CellEnter;
            selectedProductsGrid.CellLeave += SelectedProductsGrid_CellLeave;
            paidAmountTextBox.Enter += PaidAmountTextBox_Enter;
        }

        private void SelectedProductsGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedProducts?.Count) return;
            _editingBuffer = selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value?.ToString() ?? string.Empty;
        }

        private void SelectedProductsGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedProducts?.Count) return;

            try
            {
                string columnName = selectedProductsGrid.Columns[e.ColumnIndex].Name;
                string newValue = e.FormattedValue?.ToString()?.Trim();
                var product = _viewModel.SelectedProducts[e.RowIndex];
                var fullProduct = _viewModel.GetPartById(product.ProductId);

                if (fullProduct == null)
                {
                    ShowErrorMessage("Товар не найден в базе данных");
                    selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = columnName == "Quantity" ? 1 : product.Price;
                    if (columnName == "Quantity")
                        _viewModel.UpdateProductQuantity(e.RowIndex, 1);
                    else
                        _viewModel.UpdateProductPrice(e.RowIndex, product.Price);
                    _editingBuffer = columnName == "Quantity" ? "1" : product.Price.ToString();
                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                    e.Cancel = false;
                    return;
                }

                if (columnName == "Quantity")
                {
                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        ShowSuccessMessage("Количество не указано. Установлено значение: 1");
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = 1;
                        _viewModel.UpdateProductQuantity(e.RowIndex, 1);
                        _editingBuffer = "1";
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        e.Cancel = false;
                        return;
                    }

                    if (!int.TryParse(newValue, out int quantity) || quantity < 0)
                    {
                        ShowSuccessMessage("Количество должно быть неотрицательным целым числом. Установлено значение: 1");
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = 1;
                        _viewModel.UpdateProductQuantity(e.RowIndex, 1);
                        _editingBuffer = "1";
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        e.Cancel = false;
                        return;
                    }

                    if (quantity == 0)
                    {
                        ShowSuccessMessage("Количество не может быть равно 0. Установлено значение: 1");
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = 1;
                        _viewModel.UpdateProductQuantity(e.RowIndex, 1);
                        _editingBuffer = "1";
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        e.Cancel = false;
                        return;
                    }

                    if (quantity > fullProduct.RemainingQuantity)
                    {
                        ShowErrorMessage($"Количество не может превышать остаток на складе ({fullProduct.RemainingQuantity})");
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = fullProduct.RemainingQuantity;
                        _viewModel.UpdateProductQuantity(e.RowIndex, fullProduct.RemainingQuantity);
                        _editingBuffer = fullProduct.RemainingQuantity.ToString();
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        e.Cancel = false;
                        return;
                    }

                    _editingBuffer = newValue;
                    _viewModel.UpdateProductQuantity(e.RowIndex, quantity);
                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                    e.Cancel = false;
                }
                else if (columnName == "Price")
                {
                    decimal minPrice = (fullProduct.IncomeUnitPrice ?? 0) * (1 - DefaultDiscountPercentage);
                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        ShowSuccessMessage($"Цена не указана. Установлена минимальная цена: {minPrice:N2}");
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _viewModel.UpdateProductPrice(e.RowIndex, minPrice);
                        _editingBuffer = minPrice.ToString();
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }

                    if (!decimal.TryParse(newValue, out decimal price) || price < 0)
                    {
                        ShowSuccessMessage($"Цена должна быть положительным числом. Установлена минимальная цена: {minPrice:N2}");
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _viewModel.UpdateProductPrice(e.RowIndex, minPrice);
                        _editingBuffer = minPrice.ToString();
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }

                    if (price < minPrice)
                    {
                        ShowSuccessMessage($"Цена не может быть меньше минимальной ({minPrice:N2})");
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _viewModel.UpdateProductPrice(e.RowIndex, minPrice);
                        _editingBuffer = minPrice.ToString();
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }

                    price = Math.Round(price, 2);
                    _viewModel.UpdateProductPrice(e.RowIndex, price);
                    _editingBuffer = price.ToString();
                    selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = price;
                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                    UpdateTotalAmountLabel();
                    e.Cancel = false;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при валидации ячейки: {ex.Message}");
                if (selectedProductsGrid != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = _viewModel.SelectedProducts[e.RowIndex].Price;
                    _viewModel.UpdateProductPrice(e.RowIndex, _viewModel.SelectedProducts[e.RowIndex].Price);
                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                }
                UpdateTotalAmountLabel();
                e.Cancel = false;
            }
        }

        private void SelectedProductsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedProducts?.Count) return;

            try
            {
                string columnName = selectedProductsGrid.Columns[e.ColumnIndex].Name;
                int rowIndex = e.RowIndex;
                var product = _viewModel.SelectedProducts[rowIndex];
                var fullProduct = _viewModel.GetPartById(product.ProductId);

                if (fullProduct == null)
                {
                    ShowErrorMessage("Товар не найден в базе данных");
                    selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = columnName == "Quantity" ? 1 : product.Price;
                    if (columnName == "Quantity")
                        _viewModel.UpdateProductQuantity(e.RowIndex, 1);
                    else
                        _viewModel.UpdateProductPrice(e.RowIndex, product.Price);
                    _editingBuffer = columnName == "Quantity" ? "1" : product.Price.ToString();
                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                    UpdateTotalAmountLabel();
                    return;
                }

                if (columnName == "Quantity")
                {
                    object cellValueObj = selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value;
                    string cellValue = cellValueObj?.ToString()?.Trim();

                    if (string.IsNullOrWhiteSpace(cellValue) || !int.TryParse(cellValue, out int quantity) || quantity <= 0)
                    {
                        ShowSuccessMessage($"Количество товара '{product.ProductName}' обновлено: 1");
                        _viewModel.UpdateProductQuantity(rowIndex, 1);
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = 1;
                        _editingBuffer = "1";
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        return;
                    }

                    if (quantity > fullProduct.RemainingQuantity)
                    {
                        ShowErrorMessage($"Количество не может превышать остаток на складе ({fullProduct.RemainingQuantity})");
                        _viewModel.UpdateProductQuantity(rowIndex, fullProduct.RemainingQuantity);
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = fullProduct.RemainingQuantity;
                        _editingBuffer = fullProduct.RemainingQuantity.ToString();
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        return;
                    }

                    _viewModel.UpdateProductQuantity(rowIndex, quantity);
                    selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = quantity;
                    _editingBuffer = quantity.ToString();
                    ShowSuccessMessage($"Количество товара '{product.ProductName}' обновлено: {quantity}");
                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                    UpdateTotalAmountLabel();
                }
                else if (columnName == "Price")
                {
                    decimal minPrice = (fullProduct.IncomeUnitPrice ?? 0) * (1 - DefaultDiscountPercentage);
                    object cellValueObj = selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value;
                    string cellValue = cellValueObj?.ToString()?.Trim();

                    if (string.IsNullOrWhiteSpace(cellValue) || !decimal.TryParse(cellValue, out decimal price) || price < 0)
                    {
                        ShowSuccessMessage($"Цена товара '{product.ProductName}' обновлена: {minPrice:N2}");
                        _viewModel.UpdateProductPrice(e.RowIndex, minPrice);
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _editingBuffer = minPrice.ToString();
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        return;
                    }

                    if (price < minPrice)
                    {
                        ShowSuccessMessage($"Цена не может быть меньше минимальной. Установлена: {minPrice:N2}");
                        _viewModel.UpdateProductPrice(e.RowIndex, minPrice);
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _editingBuffer = minPrice.ToString();
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        return;
                    }

                    price = Math.Round(price, 2);
                    _viewModel.UpdateProductPrice(e.RowIndex, price);
                    selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = price;
                    _editingBuffer = price.ToString();
                    ShowSuccessMessage($"Цена товара '{product.ProductName}' обновлена: {price:N2}");
                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                    UpdateTotalAmountLabel();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при завершении редактирования ячейки: {ex.Message}");
                if (selectedProductsGrid != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = _viewModel.SelectedProducts[e.RowIndex].Price;
                    _viewModel.UpdateProductPrice(e.RowIndex, _viewModel.SelectedProducts[e.RowIndex].Price);
                    if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                }
                UpdateTotalAmountLabel();
            }
        }

        private void SelectedProductsGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedProducts?.Count) return;

            try
            {
                if (selectedProductsGrid.IsCurrentCellInEditMode)
                {
                    selectedProductsGrid.EndEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при выходе из ячейки: {ex.Message}");
                selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = _editingBuffer;
                _viewModel.UpdateProductPrice(e.RowIndex, _viewModel.SelectedProducts[e.RowIndex].Price);
                selectedProductsGrid.Refresh();
            }
        }

        private void PaidAmountTextBox_Enter(object sender, EventArgs e)
        {
            _currentField = "paidAmount";
            _editingBuffer = paidAmountTextBox.Text;
        }

        private void OptimizeDataGridView()
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                selectedProductsGrid,
                new object[] { true }
            );
            selectedProductsGrid.AutoGenerateColumns = false;
            selectedProductsGrid.EnableHeadersVisualStyles = false;
        }

        private void ConfigurePopularProductsGrid()
        {
            if (popularProductsGrid == null)
            {
                ShowErrorMessage("PopularProductsGrid null bo‘lib qoldi");
                return;
            }

            popularProductsGrid.SuspendLayout();
            popularProductsGrid.Columns.Clear();

            if (_viewModel.PopularProducts == null || _viewModel.PopularProducts.Count == 0)
            {
                popularProductsGrid.ResumeLayout();
                return;
            }

            var columnMapping = new Dictionary<string, string>
            {
                { "PartName", "Название товара" },
                { "IncomeUnitPrice", "Цена" },
                { "RemainingQuantity", "Остаток на складе" },
                { "ManufacturerName", "Производитель" },
                { "CatalogNumber", "Каталожный номер" },
                { "StockName", "Название склада" },
                { "ShelfNumber", "Номер полки" }
            };

            foreach (var column in _viewModel.ColumnVisibility ?? new Dictionary<string, bool>())
            {
                if (column.Value)
                {
                    var col = new DataGridViewTextBoxColumn
                    {
                        Name = column.Key,
                        HeaderText = columnMapping[column.Key],
                        DataPropertyName = column.Key,
                        ReadOnly = true,
                        MinimumWidth = 100,
                        DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                    };
                    if (column.Key == "IncomeUnitPrice" || column.Key == "RemainingQuantity" || column.Key == "ShelfNumber")
                        col.Width = 100;
                    else
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    popularProductsGrid.Columns.Add(col);
                }
            }

            popularProductsGrid.AutoGenerateColumns = false;
            popularProductsGrid.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < _viewModel.PopularProducts?.Count)
                {
                    var product = _viewModel.PopularProducts[e.RowIndex];
                    if (product?.RemainingQuantity == 0)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 204, 204);
                    }
                    if (e.ColumnIndex >= 0 && popularProductsGrid.Columns[e.ColumnIndex].Name == "IncomeUnitPrice")
                    {
                        e.Value = ((product?.IncomeUnitPrice ?? 0m) + (product?.Markup ?? 0m)).ToString();
                    }
                }
            };
            popularProductsGrid.ResumeLayout();
        }

        private void ConfigureSelectedProductsGrid()
        {
            if (selectedProductsGrid == null)
            {
                ShowErrorMessage("SelectedProductsGrid null bo‘lib qoldi");
                return;
            }

            selectedProductsGrid.SuspendLayout();
            selectedProductsGrid.Columns.Clear();

            if (_viewModel.SelectedProducts == null || _viewModel.SelectedProducts.Count == 0)
            {
                selectedProductsGrid.AllowUserToAddRows = false;
                selectedProductsGrid.DataSource = new List<SaleItemDto>();
                UpdateTotalAmountLabel();
                selectedProductsGrid.ResumeLayout();
                return;
            }

            var columns = new Dictionary<string, string>
            {
                { "RowNumber", "№" },
                { "ProductName", "Название товара" },
                { "Quantity", "Количество" },
                { "Price", "Цена" },
                { "Total", "Итого" },
                { "ManufacturerName", "Производитель" },
                { "CatalogNumber", "Каталожный номер" },
                { "StockName", "Название склада" },
                { "Status", "Статус" }, // Status ustuni
                { "PaidAmount", "Оплаченная сумма" }, // PaidAmount ustuni
                { "Delete", "Удалить" }

            };
    

            foreach (var column in columns)
            {
                // Yangi shart: Status va PaidAmount ham SelectedColumnVisibility orqali tekshiriladi
                if (column.Key != "RowNumber" && column.Key != "Delete" && _viewModel.SelectedColumnVisibility?.ContainsKey(column.Key) == true && !_viewModel.SelectedColumnVisibility[column.Key])
                    continue;

                if (column.Key == "Delete")
                {
                    var col = new DataGridViewButtonColumn
                    {
                        Name = column.Key,
                        HeaderText = column.Value,
                        Text = "X",
                        UseColumnTextForButtonValue = true,
                        Width = 50,
                        MinimumWidth = 50,
                        DefaultCellStyle = new DataGridViewCellStyle
                        {
                            Alignment = DataGridViewContentAlignment.MiddleCenter,
                            BackColor = Color.FromArgb(220, 53, 69),
                            ForeColor = Color.White,
                            Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                        }
                    };
                    selectedProductsGrid.Columns.Add(col);
                }
                else if (column.Key == "RowNumber")
                {
                    var col = new DataGridViewTextBoxColumn
                    {
                        Name = column.Key,
                        HeaderText = column.Value,
                        ReadOnly = true,
                        MinimumWidth = 50,
                        Width = 50,
                        DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                    };
                    selectedProductsGrid.Columns.Add(col);
                }
                else if (column.Key == "Status")
                {
                    var col = new DataGridViewComboBoxColumn
                    {
                        Name = column.Key,
                        HeaderText = column.Value,
                        DataPropertyName = column.Key,
                        DataSource = _viewModel.GetStatuses(), // Statuslar ro'yxatini ViewModel dan olish
                        DisplayMember = "Name", // Status nomini ko'rsatish
                        ValueMember = "StatusID", // Status ID ni saqlash
                        Width = 100,
                        MinimumWidth = 80,
                        DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                    };
                    selectedProductsGrid.Columns.Add(col);
                }
                else if (column.Key == "PaidAmount")
                {
                    var col = new DataGridViewTextBoxColumn
                    {
                        Name = column.Key,
                        HeaderText = column.Value,
                        DataPropertyName = column.Key,
                        ReadOnly = false, // Foydalanuvchi tomonidan tahrirlanishi mumkin
                        Width = 100,
                        MinimumWidth = 100,
                        DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                    };
                    selectedProductsGrid.Columns.Add(col);
                }
                else
                {
                    var col = new DataGridViewTextBoxColumn
                    {
                        Name = column.Key,
                        HeaderText = column.Value,
                        DataPropertyName = column.Key,
                        ReadOnly = !(column.Key == "Quantity" || column.Key == "Price"),
                        MinimumWidth = 80,
                        DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                    };
                    if (column.Key == "Quantity" || column.Key == "Price" || column.Key == "Total")
                        col.Width = 80;
                    else
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    selectedProductsGrid.Columns.Add(col);
                }
            }

            selectedProductsGrid.AutoGenerateColumns = false;
            selectedProductsGrid.AllowUserToAddRows = false;
            selectedProductsGrid.DataSource = _viewModel.SelectedProducts;

            selectedProductsGrid.RowPostPaint += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < _viewModel.SelectedProducts?.Count && selectedProductsGrid.Columns.Contains("RowNumber"))
                {
                    selectedProductsGrid.Rows[e.RowIndex].Cells["RowNumber"].Value = (e.RowIndex + 1).ToString();
                }
            };

            selectedProductsGrid.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < _viewModel.SelectedProducts?.Count && e.ColumnIndex >= 0)
                {
                    var product = _viewModel.SelectedProducts[e.RowIndex];
                    if (selectedProductsGrid.Columns[e.ColumnIndex].Name == "Total")
                    {
                        e.Value = (product.Quantity * product.Price).ToString("N2");
                        e.FormattingApplied = true;
                    }
                    else if (selectedProductsGrid.Columns[e.ColumnIndex].Name == "PaidAmount")
                    {
                        e.Value = (0m).ToString("N2"); // PaidAmount ni formatlash
                        e.FormattingApplied = true;
                    }
                }
            };

            UpdateTotalAmountLabel();
            selectedProductsGrid.ResumeLayout();
            selectedProductsGrid.Refresh();
        }

        private void SelectedProductsGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedProducts?.Count) return;

            try
            {
                _currentField = "grid";
                var columnName = selectedProductsGrid.Columns[e.ColumnIndex].Name;
                if (columnName == "Quantity" || columnName == "Price")
                {
                    _editingBuffer = selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value?.ToString() ?? "0";
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при выборе ячейки: {ex.Message}");
            }
        }

        private void BtnColumns_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.ShowColumnSelectionDialog(this);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при открытии диалога выбора столбцов: {ex.Message}");
            }
        }

        private void BtnColumnsSl_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.ShowSelectedColumnsDialog(this);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при открытии диалога выбора столбцов для выбранных товаров: {ex.Message}");
            }
        }

        private void PopularProductsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.PopularProducts?.Count) return;

            try
            {
                _viewModel.SelectProduct(e.RowIndex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при выборе товара: {ex.Message}");
            }
        }

        private void PopularProductsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.PopularProducts?.Count) return;
            try
            {
                var product = _viewModel.PopularProducts[e.RowIndex];
                _viewModel.AddProductToSale(product);
                ConfigureSelectedProductsGrid();
                UpdateTotalAmountLabel();
                _viewModel.SelectProduct(e.RowIndex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при добавлении товара: {ex.Message}");
            }
        }

        private void SelectedProductsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedProducts?.Count) return;

            try
            {
                if (selectedProductsGrid.Columns[e.ColumnIndex].Name == "Delete")
                {
                    _viewModel.RemoveSelectedProduct(e.RowIndex);
                    ConfigureSelectedProductsGrid();
                    UpdateTotalAmountLabel();
                }
                else
                {
                    _viewModel.SelectSelectedProduct(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при обработке клика: {ex.Message}");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.OpenSearchDialog();
                ConfigureSelectedProductsGrid();
                UpdateTotalAmountLabel();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при открытии диалога поиска: {ex.Message}");
            }
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.AddNewProduct();
                ConfigureSelectedProductsGrid();
                UpdateTotalAmountLabel();
                LoadIncompleteSalesButtons();
                SetNewCustomerLabel();
                ShowSuccessMessage("Новая продажа начата");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при добавлении новой продажи: {ex.Message}");
            }
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.ContinueSale();
                ConfigureSelectedProductsGrid();
                UpdateTotalAmountLabel();
                LoadIncompleteSalesButtons();
                ShowSuccessMessage("Список выбранных товаров сохранен");
                _viewModel.RefreshPopularProducts();
                popularProductsGrid.DataSource = new BindingSource(_viewModel.PopularProducts, null);
                // Qo'shildi: Mijoz ma'lumotlarini yangilash
                var currentSale = _viewModel.GetIncompleteSales()?.FirstOrDefault(s => s.SaleId == _viewModel.CurrentSaleId);
                if (currentSale != null && currentSale.CustomerId.HasValue)
                {
                    selectedCustomer = new CustomerInfo
                    {
                        CustomerID = currentSale.CustomerId.Value,
                        FullName = currentSale.CustomerName ?? "Неизвестный клиент",
                        Phone = currentSale.CustomerPhone
                    };
                    isCustomerSelected = true;
                }
                else
                {
                    selectedCustomer = null;
                    isCustomerSelected = false;
                }
                SetCustomerLabel(); // customerInfoLbl ni yangilash
                ShowSuccessMessage("Список выбранных товаров сохранен");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при продолжении продажи: {ex.Message}");
            }
        }

        private void BtnCancelClose_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.CancelAndClose();
                Close();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при закрытии формы: {ex.Message}");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.DeleteCurrentIncompleteSale();
                ConfigureSelectedProductsGrid();
                UpdateTotalAmountLabel();
                LoadIncompleteSalesButtons();
                ShowSuccessMessage("Текущая незавершенная продажа удалена и начата новая продажа");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при удалении текущей продажи: {ex.Message}");
            }
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(titleLabel, "Панель продаж");
            toolTip.SetToolTip(popularProductsTitleLabel, "Список популярных товаров");
            toolTip.SetToolTip(popularProductsGrid, "Список популярных товаров");
            toolTip.SetToolTip(btnColumns, "Выбор столбцов для популярных товаров");
            toolTip.SetToolTip(btnColumnsSl, "Выбор столбцов для выбранных товаров");
            toolTip.SetToolTip(selectedProductsTitleLabel, "Список выбранных товаров");
            toolTip.SetToolTip(btnSearch, "Открыть форму поиска товаров");
            toolTip.SetToolTip(btnAddProduct, "Добавить новую продажу");
            toolTip.SetToolTip(selectedProductsGrid, "Список выбранных товаров");
            toolTip.SetToolTip(incompleteSalesTitleLabel, "Список незавершенных продаж");
            toolTip.SetToolTip(incompleteSalesFlow, "Список незавершенных продаж");
            toolTip.SetToolTip(selectedProductInfoLabel, "Информация о выбранном товаре");
            toolTip.SetToolTip(totalAmountLabel, "Общая сумма продажи");
            toolTip.SetToolTip(paidAmountTextBox, "Ввод оплаченной суммы");
            toolTip.SetToolTip(comboBox1, "Поиск покупателя");
            toolTip.SetToolTip(btnContinue, "Продолжить продажу");
            toolTip.SetToolTip(btnCancelClose, "Отменить продажу и закрыть форму");
            toolTip.SetToolTip(button1, "Удалить текущую незавершенную продажу и начать новую");
            toolTip.SetToolTip(notificationPanel, "Уведомления и ошибки");
            toolTip.SetToolTip(btnNumClear, "Очистить текущую строку или поле");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _viewModel.SaveSelectedProductsToJson();
            _notificationTimer.Stop();
            _notificationTimer.Dispose();
        }

        private void selectedProductsGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Exception is ArgumentException && e.Exception.Message.Contains("System.DBNull"))
                {
                    string columnName = selectedProductsGrid.Columns[e.ColumnIndex].Name;
                    int rowIndex = e.RowIndex;
                    var product = _viewModel.SelectedProducts[rowIndex];
                    var fullProduct = _viewModel.GetPartById(product.ProductId);

                    if (fullProduct == null)
                    {
                        ShowErrorMessage("Товар не найден в базе данных");
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = columnName == "Quantity" ? 1 : product.Price;
                        if (columnName == "Quantity")
                            _viewModel.UpdateProductQuantity(rowIndex, 1);
                        else
                            _viewModel.UpdateProductPrice(rowIndex, product.Price);
                        _editingBuffer = columnName == "Quantity" ? "1" : product.Price.ToString();
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }

                    if (columnName == "Quantity")
                    {
                        ShowSuccessMessage("Количество не указано или некорректно. Установлено значение: 1");
                        _viewModel.UpdateProductQuantity(rowIndex, 1);
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = 1;
                        _editingBuffer = "1";
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }
                    else if (columnName == "Price")
                    {
                        decimal minPrice = (fullProduct.IncomeUnitPrice ?? 0) * (1 - DefaultDiscountPercentage);
                        ShowSuccessMessage($"Цена не указана или некорректна. Установлена минимальная цена: {minPrice:N2}");
                        _viewModel.UpdateProductPrice(rowIndex, minPrice);
                        selectedProductsGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _editingBuffer = minPrice.ToString();
                        if (selectedProductsGrid.CurrentCell != null) selectedProductsGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }
                }
                else
                {
                    ShowErrorMessage($"Ошибка в DataGridView: {e.Exception.Message}");
                    e.Cancel = false;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при обработке DataGridView ошибки: {ex.Message}");
                e.Cancel = false;
                UpdateTotalAmountLabel();
            }
        }

        private void LoadIncompleteSalesButtons()
        {
            try
            {
                incompleteSalesFlow.Controls.Clear();
                var incompleteSales = _viewModel.GetIncompleteSales() ?? new List<IncompleteSaleDto>();
                foreach (var sale in incompleteSales)
                {
                    if (sale == null) continue;
                    var button = new Button
                    {
                        Text = $"Продажа {sale.SaleId} ({sale.Items?.Count ?? 0}т) - {sale.CustomerName ?? "Без клиента"}",
                        Size = new Size(200, 30),
                        BackColor = Color.FromArgb(25, 118, 210),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        Tag = sale.SaleId
                    };

                    var contextMenu = new ContextMenuStrip();
                    var deleteMenuItem = new ToolStripMenuItem
                    {
                        Text = "Удалить",
                        Tag = sale.SaleId
                    };
                    deleteMenuItem.Click += (s, e) =>
                    {
                        try
                        {
                            int saleId = (int)deleteMenuItem.Tag;
                            _viewModel.DeleteIncompleteSale(saleId);
                            if (_viewModel.CurrentSaleId == saleId)
                            {
                                _viewModel.AddNewProduct();
                            }
                            ConfigureSelectedProductsGrid();
                            UpdateTotalAmountLabel();
                            LoadIncompleteSalesButtons();
                            ShowSuccessMessage($"Продажа {saleId} удалена");
                        }
                        catch (Exception ex)
                        {
                            ShowErrorMessage($"Ошибка при удалении продажи {sale.SaleId}: {ex.Message}");
                        }
                    };
                    contextMenu.Items.Add(deleteMenuItem);
                    button.ContextMenuStrip = contextMenu;

                    button.Click += (s, e) =>
                    {
                        try
                        {
                            _viewModel.SaveIncompleteSale();
                            _viewModel.LoadIncompleteSale((int)button.Tag);
                            ConfigureSelectedProductsGrid();
                            UpdateTotalAmountLabel();
                            var sale = _viewModel.GetIncompleteSales()?.FirstOrDefault(s => s.SaleId == (int)button.Tag);
                            if (sale != null && sale.CustomerId.HasValue) // O'zgartirildi: Faqat CustomerId tekshiriladi
                            {
                                selectedCustomer = new CustomerInfo
                                {
                                    CustomerID = sale.CustomerId.Value,
                                    FullName = sale.CustomerName ?? "Неизвестный клиент", // O'zgartirildi: Null bo'lsa default qiymat
                                    Phone = sale.CustomerPhone
                                };
                                isCustomerSelected = true;
                            }
                            else
                            {
                                selectedCustomer = null;
                                isCustomerSelected = false;
                            }
                            SetCustomerLabel(); // customerInfoLbl ni yangilash
                            ShowSuccessMessage($"Загружена продажа {(int)button.Tag}");
                        }
                        catch (Exception ex)
                        {
                            ShowErrorMessage($"Ошибка при загрузке продажи {(int)button.Tag}: {ex.Message}");
                        }
                    };
                    incompleteSalesFlow.Controls.Add(button);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при загрузке незавершенных продаж: {ex.Message}");
            }
        }
    }
}

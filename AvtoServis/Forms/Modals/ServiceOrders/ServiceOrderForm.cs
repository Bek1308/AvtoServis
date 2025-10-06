using AvtoServis.Data.Interfaces;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace AvtoServis.Forms
{
    public partial class ServiceOrderForm : Form
    {
        private readonly ServiceOrderViewModel _viewModel;
        private readonly ICustomerRepository customerRepository;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private bool suppressTextChanged = false;
        private bool isCustomerSelected = false;
        private CustomerInfo selectedCustomer = null;
        private readonly Timer _notificationTimer;
        private string _currentField = "grid";
        private string _editingBuffer = string.Empty;
        private string _lastCustomerLabelText = string.Empty;

        public ServiceOrderForm(ServiceOrderViewModel viewModel, ICustomerRepository customerRepository)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            _viewModel = viewModel;
            Console.WriteLine("ServiceOrderForm: Starting initialization...");

            InitializeComponent();
            if (selectedServicesGrid == null || popularServicesGrid == null || numericKeypadLayout == null || totalAmountLabel == null || notificationLabel == null || incompleteOrdersFlow == null)
            {
                throw new InvalidOperationException("UI komponentlari noto‘g‘ri inicializatsiya qilingan. Designer faylini tekshiring.");
            }

            _notificationTimer = new System.Windows.Forms.Timer { Interval = 5000 };
            _notificationTimer.Tick += (s, e) => ClearNotification();
            _viewModel.LoadSelectedServicesFromJson();

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
            LoadIncompleteOrdersButtons();
            //_viewModel.LoadSelectedServicesFromJson();
            this.customerRepository = customerRepository;
            InitializeComboBoxSearch();
            this.Load += Form1_Load;

            // Add event handler for restricting input to numbers only
            selectedServicesGrid.EditingControlShowing += SelectedServicesGrid_EditingControlShowing;

            Console.WriteLine("ServiceOrderForm: Initialization completed.");
        }

        private void InitializeComboBoxSearch()
        {
            clientComboBox.AutoCompleteMode = AutoCompleteMode.None;
            clientComboBox.DropDownStyle = ComboBoxStyle.DropDown;

            clientComboBox.TextChanged += ClientComboBox_TextChanged;
            clientComboBox.SelectionChangeCommitted += ClientComboBox_SelectionChangeCommitted;
            clientComboBox.KeyDown += ClientComboBox_KeyDown;

            clientComboBox.MouseEnter += (s, e) => Cursor.Current = Cursors.IBeam;
            clientComboBox.MouseMove += (s, e) => Cursor.Current = Cursors.IBeam;
            clientComboBox.Enter += (s, e) => Cursor.Current = Cursors.IBeam;
            clientComboBox.DropDown += (s, e) => Cursor.Current = Cursors.IBeam;
        }

        private void ClientComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && selectedCustomer != null && isCustomerSelected)
            {
                ShowCustomerDetails();
                ConfigureSelectedServicesGrid();
                SetCustomerLabel();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer cursorTimer = new Timer();
            cursorTimer.Interval = 300;
            cursorTimer.Tick += (s, ev) =>
            {
                if (clientComboBox.Focused || clientComboBox.DroppedDown)
                {
                    if (Cursor.Current != Cursors.IBeam)
                        Cursor.Current = Cursors.IBeam;
                }
            };
            cursorTimer.Start();

        }

        private async void ClientComboBox_TextChanged(object sender, EventArgs e)
        {
            if (suppressTextChanged)
            {
                suppressTextChanged = false;
                return;
            }

            cts.Cancel();
            cts = new CancellationTokenSource();
            var token = cts.Token;

            string searchText = clientComboBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                clientComboBox.DroppedDown = false;
                clientComboBox.Items.Clear();
                if (!isCustomerSelected)
                {
                    selectedCustomer = null;
                    isCustomerSelected = false;
                    SetCustomerLabel();
                }
                return;
            }

            try
            {
                await Task.Delay(300, token);

                var results = await customerRepository.SearchCustomersAsync(searchText);

                if (token.IsCancellationRequested) return;

                string currentText = clientComboBox.Text;
                int selStart = clientComboBox.SelectionStart;

                clientComboBox.BeginInvoke(() =>
                {
                    clientComboBox.Items.Clear();

                    if (results.Count > 0)
                    {
                        clientComboBox.Items.AddRange(results.ToArray());
                    }
                    else
                    {
                        clientComboBox.Items.Add(new CustomerInfo { FullName = "Данные не найдены", Phone = "" });
                    }

                    clientComboBox.DroppedDown = true;

                    clientComboBox.Text = currentText;
                    clientComboBox.SelectionStart = selStart;
                    clientComboBox.SelectionLength = 0;

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

        private void ClientComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            suppressTextChanged = true;

            if (clientComboBox.SelectedItem is CustomerInfo customer && customer.Phone != "")
            {
                selectedCustomer = customer;
                isCustomerSelected = true;
                _viewModel.SetCustomerInfo(selectedCustomer);
                ShowCustomerDetails();
                clientComboBox.Text = string.Empty;
                SetCustomerLabel();
            }
            else
            {
                if (!isCustomerSelected)
                {
                    selectedCustomer = null;
                    SetCustomerLabel();
                }
            }
        }

        private void ShowCustomerDetails()
        {
            ConfigureSelectedServicesGrid();
            if (selectedCustomer == null || !isCustomerSelected)
                return;

            var customerFullInfo = customerRepository.GetCustomerWithDebtDetailsById(selectedCustomer.CustomerID);

            if (customerFullInfo == null)
            {
                MessageBox.Show("Информация о клиенте не найдена!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Машины (номерованная нумерация)
            string cars = (customerFullInfo.CarModels != null && customerFullInfo.CarModels.Any())
                ? string.Join(Environment.NewLine, customerFullInfo.CarModels
                    .Select((c, index) => $"   {index + 1}. {c}"))
                : "Данные отсутствуют";

            // Детализация долгов (каждый долг в новой строке, тоже с номером)
            string debtDetails = (customerFullInfo.DebtDetails != null && customerFullInfo.DebtDetails.Any())
                ? string.Join(Environment.NewLine, customerFullInfo.DebtDetails
                    .Select((d, index) => $"   {index + 1}. {d.ItemName}: {d.Amount:#,0}".Replace(",", ".") + " С"))
                : "Долгов нет";

            // Общий долг с точкой как разделитель тысяч
            string umumiyQarz = customerFullInfo.UmumiyQarz.ToString("#,0").Replace(",", ".") + " С";

            // Формируем сообщение
            string message =
                $"👤 ФИО: {customerFullInfo.FullName}\n" +
                $"📞 Телефон: {customerFullInfo.Phone}\n" +
                $"🆔 ID: {customerFullInfo.CustomerID}\n" +
                $"📧 Email: {customerFullInfo.Email}\n" +
                $"🏠 Адрес: {customerFullInfo.Address}\n" +
                $"📅 Дата регистрации: {customerFullInfo.RegistrationDate:dd.MM.yyyy}\n" +
                $"🔄 Статус: {(customerFullInfo.IsActive ? "Активен" : "Неактивен")}\n" +
                $"🚗 Автомобили:\n{cars}\n" +
                $"💰 Общий долг: {umumiyQarz}\n" +
                $"📌 Детализация долгов:\n{debtDetails}";

            // Вывод через MessageBox
            MessageBox.Show(message, "Информация о клиенте",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }





        private void SetCustomerLabel()
        {
            if (isCustomerSelected && selectedCustomer != null)
            {
                string customerLabelText = $"Клиент: {selectedCustomer.FullName} ({selectedCustomer.Phone})";
                if (customerLabelText != _lastCustomerLabelText)
                {
                    clientInfoLbl.Text = customerLabelText;
                    clientInfoLbl.Visible = true;
                    _lastCustomerLabelText = customerLabelText;
                    ShowSuccessMessage($"Выбран {customerLabelText}");
                }
            }
            else
            {
                clientInfoLbl.Visible = false;
                clientInfoLbl.Text = string.Empty;
                _lastCustomerLabelText = string.Empty;
            }
            clientInfoLbl.Refresh();
        }

        private void SetNewCustomerLabel()
        {
            string customerLabelText = $"Клиент: Новый клиент";
            if (customerLabelText != _lastCustomerLabelText)
            {
                clientInfoLbl.Text = customerLabelText;
                clientInfoLbl.Visible = true;
                _lastCustomerLabelText = customerLabelText;
                ShowSuccessMessage($"Выбран {customerLabelText}");
            }
            clientInfoLbl.Refresh();
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
                        clientComboBox.Text = string.Empty;
                        _viewModel.CustomerSearchText = string.Empty;
                        selectedCustomer = null;
                        isCustomerSelected = false;
                        SetCustomerLabel();
                    }
                    else if (_currentField == "grid" && selectedServicesGrid.CurrentCell != null)
                    {
                        string columnName = selectedServicesGrid.Columns[selectedServicesGrid.CurrentCell.ColumnIndex].Name;
                        int rowIndex = selectedServicesGrid.CurrentCell.RowIndex;
                        if (columnName == "Quantity")
                        {
                            _editingBuffer = string.IsNullOrEmpty(_editingBuffer) || _editingBuffer == "0" ? "0" : _editingBuffer.Substring(0, _editingBuffer.Length - 1);
                            if (string.IsNullOrEmpty(_editingBuffer)) _editingBuffer = "0";
                            selectedServicesGrid.CurrentCell.Value = _editingBuffer;
                            if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                            if (_editingBuffer == "0")
                            {
                                _viewModel.RemoveSelectedService(rowIndex);
                                ShowSuccessMessage("Услуга удалена, так как количество равно 0");
                            }
                        }
                        else if (columnName == "Price")
                        {
                            _editingBuffer = string.IsNullOrEmpty(_editingBuffer) ? "0" : _editingBuffer.Substring(0, _editingBuffer.Length - 1);
                            if (string.IsNullOrEmpty(_editingBuffer)) _editingBuffer = "0";
                            selectedServicesGrid.CurrentCell.Value = _editingBuffer;
                            if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
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
                            ShowSuccessMessage("Поиск клиента подтвержден");
                            SetCustomerLabel();
                        }
                        else
                        {
                            ShowErrorMessage("Клиент не выбран");
                        }
                        return;
                    }
                    if (clientComboBox.Text == "Найти клиента...")
                    {
                        _viewModel.ClearCustomerSearchPlaceholder();
                        clientComboBox.Text = "";
                        _editingBuffer = "";
                    }
                    _editingBuffer += buttonText;
                    clientComboBox.Text = _editingBuffer;
                    _viewModel.CustomerSearchText = _editingBuffer;
                }
                else if (_currentField == "grid" && selectedServicesGrid.CurrentCell != null)
                {
                    string columnName = selectedServicesGrid.Columns[selectedServicesGrid.CurrentCell.ColumnIndex].Name;
                    int rowIndex = selectedServicesGrid.CurrentCell.RowIndex;
                    if (columnName == "Quantity" || columnName == "Price")
                    {
                        if (buttonText == "Ввод")
                        {
                            var service = _viewModel.SelectedServices[rowIndex];
                            var fullService = _viewModel.GetServiceById(service.ServiceId);
                            if (fullService == null)
                            {
                                ShowErrorMessage("Услуга не найдена в базе данных");
                                _editingBuffer = columnName == "Quantity" ? "1" : service.Price.ToString();
                                selectedServicesGrid.CurrentCell.Value = _editingBuffer;
                                if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                                return;
                            }

                            if (columnName == "Quantity")
                            {
                                if (!int.TryParse(_editingBuffer, out int quantity) || quantity < 0)
                                {
                                    ShowErrorMessage("Количество должно быть неотрицательным целым числом. Установлено: 0");
                                    _editingBuffer = "0";
                                    selectedServicesGrid.CurrentCell.Value = _editingBuffer;
                                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                                    _viewModel.UpdateServiceQuantity(rowIndex, 0);
                                    _viewModel.RemoveSelectedService(rowIndex);
                                    ShowSuccessMessage("Услуга удалена, так как количество равно 0");
                                    UpdateTotalAmountLabel();
                                }
                                else
                                {
                                    _viewModel.UpdateServiceQuantity(rowIndex, quantity);
                                    selectedServicesGrid.CurrentCell.Value = quantity;
                                    _editingBuffer = quantity.ToString();
                                    if (quantity == 0)
                                    {
                                        _viewModel.RemoveSelectedService(rowIndex);
                                        ShowSuccessMessage("Услуга удалена, так как количество равно 0");
                                    }
                                    else
                                    {
                                        ShowSuccessMessage($"Количество услуги '{service.ServiceName}' обновлено: {quantity}");
                                    }
                                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                                    UpdateTotalAmountLabel();
                                }
                            }
                            else if (columnName == "Price")
                            {
                                decimal minPrice = fullService.Price * (1 - _viewModel.DiscountPercentage); // Chegirma asosida minimal narx
                                if (!decimal.TryParse(_editingBuffer, out decimal price) || price < 0)
                                {
                                    ShowErrorMessage($"Цена должна быть положительным числом. Установлена минимальная цена: {minPrice:N2}");
                                    _viewModel.UpdateServicePrice(rowIndex, minPrice);
                                    selectedServicesGrid.CurrentCell.Value = minPrice;
                                    _editingBuffer = minPrice.ToString();
                                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                                    UpdateTotalAmountLabel();
                                }
                                else if (price < minPrice)
                                {
                                    ShowErrorMessage($"Цена не может быть меньше минимальной ({minPrice:N2})");
                                    _viewModel.UpdateServicePrice(rowIndex, minPrice);
                                    selectedServicesGrid.CurrentCell.Value = minPrice;
                                    _editingBuffer = minPrice.ToString();
                                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                                    UpdateTotalAmountLabel();
                                }
                                else
                                {
                                    price = Math.Round(price, 2);
                                    _viewModel.UpdateServicePrice(rowIndex, price);
                                    selectedServicesGrid.CurrentCell.Value = price;
                                    _editingBuffer = price.ToString();
                                    ShowSuccessMessage($"Цена услуги '{service.ServiceName}' обновлена: {price:N2}");
                                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
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
                        selectedServicesGrid.CurrentCell.Value = _editingBuffer;
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        if (columnName == "Quantity" && int.TryParse(_editingBuffer, out int tempQuantity) && tempQuantity >= 0)
                        {
                            if (tempQuantity == 0)
                            {
                                _viewModel.RemoveSelectedService(rowIndex);
                                ShowSuccessMessage("Услуга удалена, так как количество равно 0");
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
                decimal total = _viewModel.SelectedServices?.Sum(service => service.Quantity * service.Price) ?? 0;
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
            if (popularServicesGrid == null || selectedServicesGrid == null)
            {
                ShowErrorMessage("UI grid komponentlari noto‘g‘ri inicializatsiya qilingan");
                return;
            }

            popularServicesGrid.DataSource = _viewModel.PopularServices ?? new ObservableCollection<FullService>();
            selectedServicesGrid.DataSource = _viewModel.SelectedServices ?? new List<ServiceItemDto>();
            selectedServiceInfoLabel.DataBindings.Add("Text", _viewModel, nameof(_viewModel.SelectedServiceInfo));
            paidAmountTextBox.DataBindings.Add("Text", _viewModel, nameof(_viewModel.PaidAmount));

            ConfigurePopularServicesGrid();
            //ConfigureSelectedServicesGrid();
            OptimizeDataGridView();

            _viewModel.PropertyChanged += (s, e) =>
            {
                try
                {
                    if (e.PropertyName == nameof(_viewModel.ColumnVisibility))
                        ConfigurePopularServicesGrid();
                    else if (e.PropertyName == nameof(_viewModel.SelectedColumnVisibility))
                        ConfigureSelectedServicesGrid();
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
                    else if (e.PropertyName == nameof(_viewModel.SelectedServices))
                    {
                        this.Invoke((Action)(() =>
                        {
                            ConfigureSelectedServicesGrid();
                            UpdateTotalAmountLabel();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Ошибка в обработчике PropertyChanged для свойства {e.PropertyName}: {ex.Message}");
                }
            };

            selectedServicesGrid.CellBeginEdit += SelectedServicesGrid_CellBeginEdit;
            selectedServicesGrid.CellValidating += SelectedServicesGrid_CellValidating;
            selectedServicesGrid.CellEndEdit += SelectedServicesGrid_CellEndEdit;
            selectedServicesGrid.CellEnter += SelectedServicesGrid_CellEnter;
            selectedServicesGrid.CellLeave += SelectedServicesGrid_CellLeave;
            paidAmountTextBox.Enter += PaidAmountTextBox_Enter;
        }

        private void SelectedServicesGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                string columnName = selectedServicesGrid.Columns[selectedServicesGrid.CurrentCell.ColumnIndex].Name;
                if (columnName == "Quantity" || columnName == "Price")
                {
                    textBox.KeyPress -= TextBox_KeyPress;
                    textBox.KeyPress += TextBox_KeyPress;
                }
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string columnName = selectedServicesGrid.Columns[selectedServicesGrid.CurrentCell.ColumnIndex].Name;
            if (columnName == "Quantity")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
            }
            else if (columnName == "Price")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
        }

        private void SelectedServicesGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedServices?.Count) return;
            _editingBuffer = selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value?.ToString() ?? string.Empty;
        }

        private void SelectedServicesGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedServices?.Count) return;

            try
            {
                string columnName = selectedServicesGrid.Columns[e.ColumnIndex].Name;
                string newValue = e.FormattedValue?.ToString()?.Trim();
                var service = _viewModel.SelectedServices[e.RowIndex];
                var fullService = _viewModel.GetServiceById(service.ServiceId);

                if (fullService == null)
                {
                    ShowErrorMessage("Услуга не найдена в базе данных");
                    selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = columnName == "Quantity" ? 1 : service.Price;
                    if (columnName == "Quantity")
                        _viewModel.UpdateServiceQuantity(e.RowIndex, 1);
                    else
                        _viewModel.UpdateServicePrice(e.RowIndex, service.Price);
                    _editingBuffer = columnName == "Quantity" ? "1" : service.Price.ToString();
                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                    e.Cancel = false;
                    return;
                }

                if (columnName == "Quantity")
                {
                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        ShowSuccessMessage("Количество не указано. Установлено значение: 1");
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = 1;
                        _viewModel.UpdateServiceQuantity(e.RowIndex, 1);
                        _editingBuffer = "1";
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        e.Cancel = false;
                        return;
                    }

                    if (!int.TryParse(newValue, out int quantity) || quantity < 0)
                    {
                        ShowSuccessMessage("Количество должно быть неотрицательным целым числом. Установлено значение: 1");
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = 1;
                        _viewModel.UpdateServiceQuantity(e.RowIndex, 1);
                        _editingBuffer = "1";
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        e.Cancel = false;
                        return;
                    }

                    if (quantity == 0)
                    {
                        ShowSuccessMessage("Количество не может быть равно 0. Установлено значение: 1");
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = 1;
                        _viewModel.UpdateServiceQuantity(e.RowIndex, 1);
                        _editingBuffer = "1";
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        e.Cancel = false;
                        return;
                    }

                    _editingBuffer = newValue;
                    _viewModel.UpdateServiceQuantity(e.RowIndex, quantity);
                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                    e.Cancel = false;
                }
                else if (columnName == "Price")
                {
                    decimal minPrice = fullService.Price * (1 - _viewModel.DiscountPercentage);
                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        ShowSuccessMessage($"Цена не указана. Установлена минимальная цена: {minPrice:N2}");
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _viewModel.UpdateServicePrice(e.RowIndex, minPrice);
                        _editingBuffer = minPrice.ToString();
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }

                    if (!decimal.TryParse(newValue, out decimal price) || price < 0)
                    {
                        ShowSuccessMessage($"Цена должна быть положительным числом. Установлена минимальная цена: {minPrice:N2}");
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _viewModel.UpdateServicePrice(e.RowIndex, minPrice);
                        _editingBuffer = minPrice.ToString();
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }

                    if (price < minPrice)
                    {
                        ShowSuccessMessage($"Цена не может быть меньше минимальной ({minPrice:N2})");
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _viewModel.UpdateServicePrice(e.RowIndex, minPrice);
                        _editingBuffer = minPrice.ToString();
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }

                    price = Math.Round(price, 2);
                    _viewModel.UpdateServicePrice(e.RowIndex, price);
                    _editingBuffer = price.ToString();
                    selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = price;
                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                    UpdateTotalAmountLabel();
                    e.Cancel = false;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при валидации ячейки: {ex.Message}");
                if (selectedServicesGrid != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = _viewModel.SelectedServices[e.RowIndex].Price;
                    _viewModel.UpdateServicePrice(e.RowIndex, _viewModel.SelectedServices[e.RowIndex].Price);
                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                }
                UpdateTotalAmountLabel();
                e.Cancel = false;
            }
        }

        private void SelectedServicesGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedServices?.Count) return;

            try
            {
                string columnName = selectedServicesGrid.Columns[e.ColumnIndex].Name;
                int rowIndex = e.RowIndex;
                var service = _viewModel.SelectedServices[rowIndex];
                var fullService = _viewModel.GetServiceById(service.ServiceId);

                if (fullService == null)
                {
                    ShowErrorMessage("Услуга не найдена в базе данных");
                    selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = columnName == "Quantity" ? 1 : service.Price;
                    if (columnName == "Quantity")
                        _viewModel.UpdateServiceQuantity(e.RowIndex, 1);
                    else
                        _viewModel.UpdateServicePrice(e.RowIndex, service.Price);
                    _editingBuffer = columnName == "Quantity" ? "1" : service.Price.ToString();
                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                    UpdateTotalAmountLabel();
                    return;
                }

                if (columnName == "Quantity")
                {
                    object cellValueObj = selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value;
                    string cellValue = cellValueObj?.ToString()?.Trim();

                    if (string.IsNullOrWhiteSpace(cellValue) || !int.TryParse(cellValue, out int quantity) || quantity <= 0)
                    {
                        ShowSuccessMessage($"Количество услуги '{service.ServiceName}' обновлено: 1");
                        _viewModel.UpdateServiceQuantity(rowIndex, 1);
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = 1;
                        _editingBuffer = "1";
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        return;
                    }

                    _viewModel.UpdateServiceQuantity(rowIndex, quantity);
                    selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = quantity;
                    _editingBuffer = quantity.ToString();
                    ShowSuccessMessage($"Количество услуги '{service.ServiceName}' обновлено: {quantity}");
                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                    UpdateTotalAmountLabel();
                }
                else if (columnName == "Price")
                {
                    decimal minPrice = fullService.Price * (1 - _viewModel.DiscountPercentage);
                    object cellValueObj = selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value;
                    string cellValue = cellValueObj?.ToString()?.Trim();

                    if (string.IsNullOrWhiteSpace(cellValue) || !decimal.TryParse(cellValue, out decimal price) || price < 0)
                    {
                        ShowSuccessMessage($"Цена услуги '{service.ServiceName}' обновлена: {minPrice:N2}");
                        _viewModel.UpdateServicePrice(e.RowIndex, minPrice);
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _editingBuffer = minPrice.ToString();
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        return;
                    }

                    if (price < minPrice)
                    {
                        ShowSuccessMessage($"Цена не может быть меньше минимальной. Установлена: {minPrice:N2}");
                        _viewModel.UpdateServicePrice(e.RowIndex, minPrice);
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _editingBuffer = minPrice.ToString();
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        return;
                    }

                    price = Math.Round(price, 2);
                    _viewModel.UpdateServicePrice(e.RowIndex, price);
                    selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = price;
                    _editingBuffer = price.ToString();
                    ShowSuccessMessage($"Цена услуги '{service.ServiceName}' обновлена: {price:N2}");
                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                    UpdateTotalAmountLabel();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при завершении редактирования ячейки: {ex.Message}");
                if (selectedServicesGrid != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = _viewModel.SelectedServices[e.RowIndex].Price;
                    _viewModel.UpdateServicePrice(e.RowIndex, _viewModel.SelectedServices[e.RowIndex].Price);
                    if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                }
                UpdateTotalAmountLabel();
            }
        }

        private void SelectedServicesGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedServices?.Count) return;

            try
            {
                if (selectedServicesGrid.IsCurrentCellInEditMode)
                {
                    selectedServicesGrid.EndEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при выходе из ячейки: {ex.Message}");
                selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = _editingBuffer;
                _viewModel.UpdateServicePrice(e.RowIndex, _viewModel.SelectedServices[e.RowIndex].Price);
                selectedServicesGrid.Refresh();
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
                selectedServicesGrid,
                new object[] { true }
            );
            selectedServicesGrid.AutoGenerateColumns = false;
            selectedServicesGrid.EnableHeadersVisualStyles = false;
        }

        private void ConfigurePopularServicesGrid()
        {
            if (popularServicesGrid == null)
            {
                ShowErrorMessage("PopularServicesGrid null bo‘lib qoldi");
                return;
            }

            popularServicesGrid.SuspendLayout();
            popularServicesGrid.Columns.Clear();

            if (_viewModel.PopularServices == null || _viewModel.PopularServices.Count == 0)
            {
                popularServicesGrid.ResumeLayout();
                return;
            }

            var columnMapping = new Dictionary<string, string>
            {
                { "Name", "Название услуги" },
                { "Price", "Цена" },
                { "SoldCount", "Продано" },
                { "TotalRevenue", "Общая выручка" }
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
                    if (column.Key == "Price" || column.Key == "SoldCount")
                        col.Width = 100;
                    else
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    popularServicesGrid.Columns.Add(col);
                }
            }

            popularServicesGrid.AutoGenerateColumns = false;
            popularServicesGrid.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < _viewModel.PopularServices?.Count)
                {
                    var service = _viewModel.PopularServices[e.RowIndex];
                    if (e.ColumnIndex >= 0 && popularServicesGrid.Columns[e.ColumnIndex].Name == "Price")
                    {
                        e.Value = service?.Price.ToString("N2");
                    }
                }
            };
            popularServicesGrid.ResumeLayout();
        }

        private void ConfigureSelectedServicesGrid()
        {
            if (selectedServicesGrid == null)
            {
                ShowErrorMessage("SelectedServicesGrid null bo‘lib qoldi");
                return;
            }

            selectedServicesGrid.SuspendLayout();
            selectedServicesGrid.Columns.Clear();

            if (_viewModel.SelectedServices == null || _viewModel.SelectedServices.Count == 0)
            {
                selectedServicesGrid.AllowUserToAddRows = false;
                selectedServicesGrid.DataSource = new List<ServiceItemDto>();
                UpdateTotalAmountLabel();
                selectedServicesGrid.ResumeLayout();
                return;
            }

            var columns = new Dictionary<string, string>
    {
        { "RowNumber", "№" },
        { "ServiceName", "Название услуги" },
        { "Quantity", "Количество" },
        { "Price", "Цена" },
        { "Total", "Итого" },
        { "VehicleId", "Модель" },
        { "StatusId", "Статус ID" },
        { "PaidAmount", "Оплаченная сумма" },
        { "Delete", "Удалить" }
    };

            foreach (var column in columns)
            {
                if (column.Key != "RowNumber" && column.Key != "Delete" && _viewModel.SelectedColumnVisibility?.ContainsKey(column.Key) == true && !_viewModel.SelectedColumnVisibility[column.Key])
                    continue;

                try
                {
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
                        selectedServicesGrid.Columns.Add(col);
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
                        selectedServicesGrid.Columns.Add(col);
                    }
                    else if (column.Key == "StatusId")
                    {
                        var col = new DataGridViewComboBoxColumn
                        {
                            Name = column.Key,
                            HeaderText = column.Value,
                            DataPropertyName = column.Key,
                            DataSource = _viewModel.GetStatuses(),
                            DisplayMember = "Name",
                            ValueMember = "StatusID",
                            Width = 100,
                            MinimumWidth = 80,
                            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                        };
                        selectedServicesGrid.Columns.Add(col);
                    }
                    else if (column.Key == "VehicleId")
                    {
                        var col = new DataGridViewComboBoxColumn
                        {
                            Name = column.Key,
                            HeaderText = column.Value,
                            DataPropertyName = column.Key,
                            Width = 200,
                            MinimumWidth = 100,
                            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                        };

                        // Populate ComboBox with car models based on selected customer
                        List<CarModel> carModels;
                        try
                        {
                            carModels = isCustomerSelected && selectedCustomer != null
                                ? _viewModel.GetCarModelByCustomerId(selectedCustomer.CustomerID)
                                : _viewModel.GetCarModelByCustomerId();
                        }
                        catch (Exception ex)
                        {
                            ShowErrorMessage($"Ошибка при загрузке моделей автомобилей: {ex.Message}");
                            carModels = new List<CarModel>();
                        }

                        // Add a default "Not selected" option
                        carModels.Insert(0, new CarModel { Id = 0, Model = "Не выбрано" });

                        col.DataSource = carModels;
                        col.DisplayMember = "DisplayName"; // Updated to match CarModel property
                        col.ValueMember = "Id"; // Matches CarModel.Id
                        selectedServicesGrid.Columns.Add(col);
                    
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
                        selectedServicesGrid.Columns.Add(col);
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Ошибка при добавлении столбца {column.Value}: {ex.Message}");
                    continue; // Continue with the next column to prevent grid failure
                }
            }

            selectedServicesGrid.AutoGenerateColumns = false;
            selectedServicesGrid.AllowUserToAddRows = false;
            selectedServicesGrid.DataSource = _viewModel.SelectedServices;

            selectedServicesGrid.RowPostPaint += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < _viewModel.SelectedServices?.Count && selectedServicesGrid.Columns.Contains("RowNumber"))
                {
                    selectedServicesGrid.Rows[e.RowIndex].Cells["RowNumber"].Value = (e.RowIndex + 1).ToString();
                }
            };

            selectedServicesGrid.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < _viewModel.SelectedServices?.Count && e.ColumnIndex >= 0)
                {
                    var service = _viewModel.SelectedServices[e.RowIndex];
                    if (selectedServicesGrid.Columns[e.ColumnIndex].Name == "Total")
                    {
                        e.Value = (service.Quantity * service.Price).ToString("N2");
                        e.FormattingApplied = true;
                    }
                    else if (selectedServicesGrid.Columns[e.ColumnIndex].Name == "PaidAmount")
                    {
                        e.Value = service.PaidAmount.ToString("N2");
                        e.FormattingApplied = true;
                    }
                    else if (selectedServicesGrid.Columns[e.ColumnIndex].Name == "VehicleId")
                    {
                        var carModels = (selectedServicesGrid.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn)?.DataSource as List<CarModel>;
                        if (carModels == null || service.VehicleId == 0)
                        {
                            e.Value = "Не выбрано";
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            var selectedModel = carModels.FirstOrDefault(c => c.Id == service.VehicleId);
                            e.Value = selectedModel?.Model ?? "Неизвестная модель";
                            e.FormattingApplied = true;
                        }
                    }
                }
            };

            UpdateTotalAmountLabel();
            selectedServicesGrid.ResumeLayout();
            selectedServicesGrid.Refresh();
        }

        private void SelectedServicesGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedServices?.Count) return;

            try
            {
                _currentField = "grid";
                var columnName = selectedServicesGrid.Columns[e.ColumnIndex].Name;
                if (columnName == "Quantity" || columnName == "Price")
                {
                    _editingBuffer = selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value?.ToString() ?? "0";
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
                ConfigureSelectedServicesGrid();
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
                ShowErrorMessage($"Ошибка при открытии диалога выбора столбцов для выбранных услуг: {ex.Message}");
            }
        }

        private void PopularServicesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.PopularServices?.Count) return;

            try
            {
                _viewModel.SelectService(e.RowIndex);
                ConfigureSelectedServicesGrid();

            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при выборе услуги: {ex.Message}");
            }
        }

        private void PopularServicesGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.PopularServices?.Count) return;
            try
            {
                var service = _viewModel.PopularServices[e.RowIndex];
                _viewModel.AddServiceToOrder(service);
                ConfigureSelectedServicesGrid();
                UpdateTotalAmountLabel();
                _viewModel.SelectService(e.RowIndex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при добавлении услуги: {ex.Message}");
            }
        }

        private void SelectedServicesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _viewModel.SelectedServices?.Count) return;

            try
            {
                if (selectedServicesGrid.Columns[e.ColumnIndex].Name == "Delete")
                {
                    _viewModel.RemoveSelectedService(e.RowIndex);
                    ConfigureSelectedServicesGrid();
                    UpdateTotalAmountLabel();
                }
                else
                {
                    _viewModel.SelectSelectedService(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при обработке клика: {ex.Message}");
            }
        }

        private void BtnSearchService_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.OpenSearchDialog();
                ConfigureSelectedServicesGrid();
                UpdateTotalAmountLabel();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при открытии диалога поиска: {ex.Message}");
            }
        }

        private void BtnAddService_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.AddNewService();
                ConfigureSelectedServicesGrid();
                UpdateTotalAmountLabel();
                LoadIncompleteOrdersButtons();
                SetNewCustomerLabel();
                isCustomerSelected = false;
                selectedCustomer = null;
                ShowSuccessMessage("Новый заказ начат");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при добавлении нового заказа: {ex.Message}");
            }
        }

        private void BtnConfirmOrder_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.ContinueOrder();
                ConfigureSelectedServicesGrid();
                UpdateTotalAmountLabel();
                //LoadIncompleteOrdersButtons();
                ShowSuccessMessage("Список выбранных услуг сохранен");
                _viewModel.RefreshPopularServices();
                popularServicesGrid.DataSource = new BindingSource(_viewModel.PopularServices, null);
                var currentOrder = _viewModel.GetIncompleteOrders()?.FirstOrDefault(s => s.OrderId == _viewModel.CurrentOrderId);
                if (currentOrder != null && currentOrder.CustomerId.HasValue)
                {
                    selectedCustomer = new CustomerInfo
                    {
                        CustomerID = currentOrder.CustomerId.Value,
                        FullName = currentOrder.CustomerName ?? "Неизвестный клиент",
                        Phone = currentOrder.CustomerPhone
                    };
                    isCustomerSelected = true;
                }
                else
                {
                    selectedCustomer = null;
                    isCustomerSelected = false;
                }
                SetCustomerLabel();
                ShowSuccessMessage("Список выбранных услуг сохранен");
                RefreshIncompleteOrdersButtons();
             
                MessageBox.Show("Список выбранных услуг сохранен",
                                "Успех",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                _viewModel.DeleteCurrentIncompleteOrder();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при продолжении заказа: {ex.Message}");
                MessageBox.Show($"Ошибка при продолжении заказа: {ex.Message}",
                       "Ошибка",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
            }
        }

        private void BtnCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.CancelAndClose();
                ConfigureSelectedServicesGrid();

                Close();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при закрытии формы: {ex.Message}");
            }
        }

        private void BtnRemoveService_Click(object sender, EventArgs e)
        {
            try
            {
                _viewModel.DeleteCurrentIncompleteOrder();
                ConfigureSelectedServicesGrid();
                UpdateTotalAmountLabel();
                LoadIncompleteOrdersButtons();
                ShowSuccessMessage("Текущий незавершенный заказ удален и начат новый заказ");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при удалении текущего заказа: {ex.Message}");
            }
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(titleLabel, "Панель заказов");
            toolTip.SetToolTip(popularServicesTitleLabel, "Список популярных услуг");
            toolTip.SetToolTip(popularServicesGrid, "Список популярных услуг");
            toolTip.SetToolTip(btnColumns, "Выбор столбцов для популярных услуг");
            toolTip.SetToolTip(btnColumnsSl, "Выбор столбцов для выбранных услуг");
            toolTip.SetToolTip(selectedServicesTitleLabel, "Список выбранных услуг");
            toolTip.SetToolTip(btnSearchService, "Открыть форму поиска услуг");
            toolTip.SetToolTip(btnAddService, "Добавить новый заказ");
            toolTip.SetToolTip(selectedServicesGrid, "Список выбранных услуг");
            toolTip.SetToolTip(incompleteOrdersTitleLabel, "Список незавершенных заказов");
            toolTip.SetToolTip(incompleteOrdersFlow, "Список незавершенных заказов");
            toolTip.SetToolTip(selectedServiceInfoLabel, "Информация о выбранной услуге");
            toolTip.SetToolTip(totalAmountLabel, "Общая сумма заказа");
            toolTip.SetToolTip(paidAmountTextBox, "Ввод оплаченной суммы");
            toolTip.SetToolTip(clientComboBox, "Поиск клиента");
            toolTip.SetToolTip(btnConfirmOrder, "Продолжить заказ");
            toolTip.SetToolTip(btnCancelOrder, "Отменить заказ и закрыть форму");
            toolTip.SetToolTip(btnRemoveService, "Удалить текущий незавершенный заказ и начать новый");
            toolTip.SetToolTip(notificationPanel, "Уведомления и ошибки");
            toolTip.SetToolTip(btnNumClear, "Очистить текущую строку или поле");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _viewModel.SaveSelectedServicesToJson();
            _notificationTimer.Stop();
            _notificationTimer.Dispose();
        }

        private void selectedServicesGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Exception is ArgumentException && e.Exception.Message.Contains("System.DBNull"))
                {
                    string columnName = selectedServicesGrid.Columns[e.ColumnIndex].Name;
                    int rowIndex = e.RowIndex;
                    var service = _viewModel.SelectedServices[rowIndex];
                    var fullService = _viewModel.GetServiceById(service.ServiceId);

                    if (fullService == null)
                    {
                        ShowErrorMessage("Услуга не найдена в базе данных");
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = columnName == "Quantity" ? 1 : service.Price;
                        if (columnName == "Quantity")
                            _viewModel.UpdateServiceQuantity(rowIndex, 1);
                        else
                            _viewModel.UpdateServicePrice(rowIndex, service.Price);
                        _editingBuffer = columnName == "Quantity" ? "1" : service.Price.ToString();
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }

                    if (columnName == "Quantity")
                    {
                        ShowSuccessMessage("Количество не указано или некорректно. Установлено значение: 1");
                        _viewModel.UpdateServiceQuantity(rowIndex, 1);
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = 1;
                        _editingBuffer = "1";
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
                        UpdateTotalAmountLabel();
                        e.Cancel = false;
                        return;
                    }
                    else if (columnName == "Price")
                    {
                        decimal minPrice = fullService.Price * (1 - _viewModel.DiscountPercentage);
                        ShowSuccessMessage($"Цена не указана или некорректна. Установлена минимальная цена: {minPrice:N2}");
                        _viewModel.UpdateServicePrice(rowIndex, minPrice);
                        selectedServicesGrid[e.ColumnIndex, e.RowIndex].Value = minPrice;
                        _editingBuffer = minPrice.ToString();
                        if (selectedServicesGrid.CurrentCell != null) selectedServicesGrid.NotifyCurrentCellDirty(true);
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

        private void LoadIncompleteOrdersButtons()
        {
            try
            {
                ConfigureSelectedServicesGrid();
                incompleteOrdersFlow.Controls.Clear();
                var incompleteOrders = _viewModel.GetIncompleteOrders() ?? new List<IncompleteOrderDto>();
                foreach (var order in incompleteOrders)
                {
                    if (order == null) continue;
                    var button = new Button
                    {
                        Text = $"Заказ {order.OrderId} ({order.Items?.Count ?? 0} у) - {order.CustomerName ?? "Без клиента"}",
                        Size = new Size(200, 30),
                        BackColor = Color.FromArgb(25, 118, 210),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        Tag = order.OrderId
                    };

                    var contextMenu = new ContextMenuStrip();
                    var deleteMenuItem = new ToolStripMenuItem
                    {
                        Text = "Удалить",
                        Tag = order.OrderId
                    };
                    deleteMenuItem.Click += (s, e) =>
                    {
                        try
                        {
                            int orderId = (int)deleteMenuItem.Tag;
                            _viewModel.DeleteIncompleteOrder(orderId);
                            if (_viewModel.CurrentOrderId == orderId)
                            {
                                _viewModel.AddNewService();
                            }
                            ConfigureSelectedServicesGrid();
                            UpdateTotalAmountLabel();
                            LoadIncompleteOrdersButtons();
                            ShowSuccessMessage($"Заказ {orderId} удален");
                        }
                        catch (Exception ex)
                        {
                            ShowErrorMessage($"Ошибка при удалении заказа {order.OrderId}: {ex.Message}");
                        }
                    };
                    contextMenu.Items.Add(deleteMenuItem);
                    button.ContextMenuStrip = contextMenu;

                    button.Click += (s, e) =>
                    {
                        try
                        {
                            _viewModel.SaveIncompleteOrder();
                            _viewModel.LoadIncompleteOrder((int)button.Tag);
                            ConfigureSelectedServicesGrid();
                            UpdateTotalAmountLabel();
                            var order = _viewModel.GetIncompleteOrders()?.FirstOrDefault(s => s.OrderId == (int)button.Tag);
                            if (order != null && order.CustomerId.HasValue)
                            {
                                selectedCustomer = new CustomerInfo
                                {
                                    CustomerID = order.CustomerId.Value,
                                    FullName = order.CustomerName ?? "Неизвестный клиент",
                                    Phone = order.CustomerPhone
                                };
                                isCustomerSelected = true;
                            }
                            else
                            {
                                selectedCustomer = null;
                                isCustomerSelected = false;
                            }
                            SetCustomerLabel();
                            ConfigureSelectedServicesGrid();
                            ShowSuccessMessage($"Загружен заказ {(int)button.Tag}");
                        }
                        catch (Exception ex)
                        {
                            ShowErrorMessage($"Ошибка при загрузке заказа {(int)button.Tag}: {ex.Message}");
                        }
                    };
                    incompleteOrdersFlow.Controls.Add(button);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при загрузке незавершенных заказов: {ex.Message}");
            }
        }
        private void RefreshIncompleteOrdersButtons()
        {
            try
            {
                incompleteOrdersFlow.Controls.Clear();
                //LoadIncompleteOrdersButtons();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при обновлении списка незавершенных заказов: {ex.Message}");
            }
        }

    }
}
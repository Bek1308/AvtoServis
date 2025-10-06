using AvtoServis.Data.Configuration;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvtoServis.Forms
{
    public partial class ServiceOrderEditForm : Form
    {
        private readonly ServiceOrdersViewModel _viewModel;
        private readonly ServiceOrder _serviceOrder;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _suppressTextChanged = false;
        private bool _isCustomerSelected = false;
        private CustomerInfo _selectedCustomer = null;

        public ServiceOrderEditForm(
            ServiceOrdersViewModel viewModel,
            ServiceOrder serviceOrder)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _serviceOrder = serviceOrder ?? throw new ArgumentNullException(nameof(serviceOrder));
            InitializeComponent();
            InitializeTimer();
            InitializeComboBoxes();
            ShowInitialMessage();
            LoadData();
        }

        private void InitializeTimer()
        {
            errorTimer.Tick += (s, e) =>
            {
                _messagePanel.Visible = true;
                _messageLabel.Visible = true;
                errorTimer.Stop();
            };
        }

        private void InitializeComboBoxes()
        {
            try
            {
                var services = _viewModel.GetAllServices();
                cmbServiceID.DataSource = services;
                cmbServiceID.DisplayMember = "Name";
                cmbServiceID.ValueMember = "ServiceID";
                cmbServiceID.SelectedIndex = -1;
                cmbServiceID.Enabled = false; // Faqat ma'lumot uchun

                cmbCustomerID.AutoCompleteMode = AutoCompleteMode.None;
                cmbCustomerID.DropDownStyle = ComboBoxStyle.DropDown;
                cmbCustomerID.TextChanged += CmbCustomerID_TextChanged;
                cmbCustomerID.SelectionChangeCommitted += CmbCustomerID_SelectionChangeCommitted;
                cmbCustomerID.KeyDown += CmbCustomerID_KeyDown;
                cmbCustomerID.MouseEnter += (s, e) => Cursor.Current = Cursors.IBeam;
                cmbCustomerID.MouseMove += (s, e) => Cursor.Current = Cursors.IBeam;
                cmbCustomerID.Enter += (s, e) => Cursor.Current = Cursors.IBeam;
                cmbCustomerID.DropDown += (s, e) => Cursor.Current = Cursors.IBeam;

                LoadVehiclesForCustomer(null);  // Dastlab barcha mashinalar

                var statuses = _viewModel.GetAllOrderStatuses();
                cmbStatusID.DataSource = statuses;
                cmbStatusID.DisplayMember = "Name";
                cmbStatusID.ValueMember = "StatusID";
                cmbStatusID.SelectedIndex = -1;

                // Events qo'shish
                txtQuantity.TextChanged += txtQuantity_TextChanged;
                txtUnitPrice.TextChanged += txtUnitPrice_TextChanged;
                txtUnitPrice.Leave += txtUnitPrice_Leave;
                txtPaidAmount.TextChanged += txtPaidAmount_TextChanged;
                cmbStatusID.SelectedIndexChanged += cmbStatusID_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке данных для выпадающих списков: {ex.Message}");
                Console.WriteLine($"InitializeComboBoxes Error: {ex}");
            }
        }

        private void LoadVehiclesForCustomer(int? customerId)
        {
            try
            {
                var vehicles = _viewModel.GetCarModelByCustomerId(customerId);
                cmbVehicleID.DataSource = null;
                cmbVehicleID.Items.Clear();
                cmbVehicleID.Items.Add(new CarModel { Id = 0, Model = "Не выбрано" }); // Ixtiyoriy tanlash uchun
                if (vehicles != null && vehicles.Any())
                {
                    vehicles.ForEach(v => cmbVehicleID.Items.Add(v));
                }
                cmbVehicleID.DisplayMember = "Model";
                cmbVehicleID.ValueMember = "Id";
                cmbVehicleID.SelectedIndex = 0; // "Не выбрано" default holatda tanlanadi
                Console.WriteLine($"Loaded {vehicles.Count} vehicles for customer {customerId ?? 0}");
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке автомобилей: {ex.Message}");
                Console.WriteLine($"LoadVehiclesForCustomer Error: {ex}");
            }
        }

        private async void CmbCustomerID_TextChanged(object sender, EventArgs e)
        {
            if (_suppressTextChanged)
            {
                _suppressTextChanged = false;
                return;
            }

            _cts.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            string searchText = cmbCustomerID.Text?.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                cmbCustomerID.DroppedDown = false;
                cmbCustomerID.Items.Clear();
                if (!_isCustomerSelected)
                {
                    _selectedCustomer = null;
                    _isCustomerSelected = false;
                    UpdateCustomerInfoLabel(null);
                    LoadVehiclesForCustomer(null);
                }
                ValidateInputs();
                return;
            }

            try
            {
                await Task.Delay(300, token);

                var results = await _viewModel.SearchCustomersAsync(searchText);

                if (token.IsCancellationRequested) return;

                string currentText = cmbCustomerID.Text;
                int selStart = cmbCustomerID.SelectionStart;

                cmbCustomerID.BeginInvoke((Action)(() =>
                {
                    cmbCustomerID.Items.Clear();

                    if (results.Count > 0)
                    {
                        cmbCustomerID.Items.AddRange(results.ToArray());
                    }
                    else
                    {
                        cmbCustomerID.Items.Add(new CustomerInfo { FullName = "Данные не найдены", Phone = "" });
                    }

                    cmbCustomerID.DroppedDown = true;

                    cmbCustomerID.Text = currentText;
                    cmbCustomerID.SelectionStart = selStart;
                    cmbCustomerID.SelectionLength = 0;

                    Application.DoEvents();
                    Cursor.Current = Cursors.IBeam;
                }));
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                ShowError($"Ошибка при поиске клиента: {ex.Message}");
                Console.WriteLine($"CmbCustomerID_TextChanged Error: {ex}");
            }
        }

        private void CmbCustomerID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _suppressTextChanged = true;

            if (cmbCustomerID.SelectedItem is CustomerInfo customer && !string.IsNullOrEmpty(customer.Phone))
            {
                _selectedCustomer = customer;
                _isCustomerSelected = true;
                UpdateCustomerInfoLabel(customer);
                ShowCustomerDetails();
                LoadVehiclesForCustomer(customer.CustomerID);
                cmbCustomerID.Text = string.Empty;
                Console.WriteLine($"Customer Selected: ID={customer.CustomerID}, Name={customer.FullName}, Phone={customer.Phone}");
            }
            else
            {
                if (!_isCustomerSelected)
                {
                    _selectedCustomer = null;
                    _isCustomerSelected = false;
                    UpdateCustomerInfoLabel(null);
                    LoadVehiclesForCustomer(null);
                    Console.WriteLine("No valid customer selected.");
                }
            }
            ValidateInputs();
        }

        private void CmbCustomerID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Console.WriteLine($"KeyDown Enter Pressed: _isCustomerSelected={_isCustomerSelected}, _selectedCustomer={(_selectedCustomer != null ? _selectedCustomer.CustomerID : "null")}");
                if (_selectedCustomer != null && _isCustomerSelected)
                {
                    ShowCustomerDetails();
                }
                else
                {
                    ShowError("Клиент не выбран для отображения деталей!");
                    Console.WriteLine("ShowCustomerDetails not called: No customer selected.");
                }
            }
        }

        private void ShowCustomerDetails()
        {
            if (_selectedCustomer == null || !_isCustomerSelected)
            {
                ShowError("Клиент не выбран для отображения деталей!");
                Console.WriteLine("ShowCustomerDetails: No customer selected.");
                return;
            }

            try
            {
                var customerFullInfo = _viewModel.GetCustomerWithDebtDetailsById(_selectedCustomer.CustomerID);
                if (customerFullInfo == null)
                {
                    ShowError("Информация о клиенте не найдена!");
                    Console.WriteLine($"ShowCustomerDetails: Customer ID {_selectedCustomer.CustomerID} not found.");
                    return;
                }

                string cars = (customerFullInfo.CarModels != null && customerFullInfo.CarModels.Any())
                    ? string.Join(Environment.NewLine, customerFullInfo.CarModels
                        .Select((c, index) => $"   {index + 1}. {c}"))
                    : "Данные отсутствуют";

                string debtDetails = (customerFullInfo.DebtDetails != null && customerFullInfo.DebtDetails.Any())
                    ? string.Join(Environment.NewLine, customerFullInfo.DebtDetails
                        .Select((d, index) => $"   {index + 1}. {d.ItemName}: {d.Amount:#,0}".Replace(",", ".") + " С"))
                    : "Долгов нет";

                string umumiyQarz = customerFullInfo.UmumiyQarz.ToString("#,0").Replace(",", ".") + " С";

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

                MessageBox.Show(message, "Информация о клиенте",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("ShowCustomerDetails: Customer details displayed successfully.");
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке данных клиента: {ex.Message}");
                Console.WriteLine($"ShowCustomerDetails Error: {ex}");
            }
        }

        private void ShowInitialMessage()
        {
            _messageLabel.Text = "Редактирование сервисного заказа: измените необходимые поля и нажмите 'Сохранить'";
            _messageLabel.ForeColor = Color.FromArgb(40, 167, 69);
            _messageLabel.BackColor = Color.Transparent;
            _messagePanel.BackColor = Color.FromArgb(245, 255, 245);
            _messagePanel.Visible = true;
            _messageLabel.Visible = true;
            _messagePanel.BringToFront();
        }

        private void ShowError(string message)
        {
            _messageLabel.Text = message;
            _messageLabel.ForeColor = Color.FromArgb(220, 53, 69);
            _messageLabel.BackColor = Color.Transparent;
            _messagePanel.BackColor = Color.FromArgb(255, 245, 245);
            _messagePanel.Visible = true;
            _messageLabel.Visible = true;
            _messagePanel.BringToFront();
            errorTimer.Stop();
            errorTimer.Start();
        }

        private void ShowSuccess(string message)
        {
            _messageLabel.Text = message;
            _messageLabel.ForeColor = Color.FromArgb(40, 167, 69);
            _messageLabel.BackColor = Color.Transparent;
            _messagePanel.BackColor = Color.FromArgb(245, 255, 245);
            _messagePanel.Visible = true;
            _messageLabel.Visible = true;
            _messagePanel.BringToFront();
            errorTimer.Stop();
            errorTimer.Start();
        }

        private void UpdateCustomerInfoLabel(CustomerInfo customer)
        {
            if (customer == null)
            {
                lblCustomerInfo.Text = "Клиент не выбран";
                lblCustomerInfo.ForeColor = Color.FromArgb(108, 117, 125);
            }
            else
            {
                string displayName = $"{customer.FullName} ({customer.Phone ?? "Телефон не указан"})";
                lblCustomerInfo.Text = displayName;
                lblCustomerInfo.ForeColor = Color.FromArgb(33, 37, 41);
            }
        }

        private void LoadData()
        {
            try
            {
                cmbServiceID.SelectedValue = _serviceOrder.ServiceID;

                decimal totalAmount = (decimal)(_serviceOrder.TotalAmount ?? 0m);
                decimal unitPrice = _serviceOrder.Quantity > 0 ? totalAmount / _serviceOrder.Quantity : 0m;
                txtQuantity.Text = _serviceOrder.Quantity.ToString();
                txtUnitPrice.Text = unitPrice.ToString();
                txtPaidAmount.Text = _serviceOrder.PaidAmount.ToString();
                cmbStatusID.SelectedValue = _serviceOrder.StatusID;

                if (_serviceOrder.CustomerID.HasValue)
                {
                    var customer = _viewModel.GetCustomerById(_serviceOrder.CustomerID.Value);
                    if (customer != null)
                    {
                        _selectedCustomer = new CustomerInfo
                        {
                            CustomerID = customer.CustomerID,
                            FullName = customer.FullName,
                            Phone = customer.Phone
                        };
                        UpdateCustomerInfoLabel(_selectedCustomer);
                        LoadVehiclesForCustomer(_serviceOrder.CustomerID.Value);
                        cmbVehicleID.SelectedValue = _serviceOrder.VehicleID ?? 0; // Ixtiyoriy bo'lsa 0 tanlanadi
                        Console.WriteLine($"LoadData: Customer loaded - ID={customer.CustomerID}, Name={customer.FullName}");
                    }
                    else
                    {
                        _selectedCustomer = null;
                        _isCustomerSelected = false;
                        UpdateCustomerInfoLabel(null);
                        LoadVehiclesForCustomer(null);
                        lblCustomerInfo.Text = "Клиент не найден";
                        lblCustomerInfo.ForeColor = Color.FromArgb(220, 53, 69);
                        Console.WriteLine("LoadData: Customer not found.");
                    }
                }
                else
                {
                    _selectedCustomer = null;
                    _isCustomerSelected = false;
                    UpdateCustomerInfoLabel(null);
                    LoadVehiclesForCustomer(null);
                    Console.WriteLine("LoadData: No customer ID provided.");
                }

                cmbCustomerID.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке данных: {ex.Message}");
                Console.WriteLine($"LoadData Error: {ex}");
            }
            ValidateInputs();
        }

        private void ValidateInputs()
        {
            var errors = _viewModel.ValidateServiceOrder(
                cmbServiceID.SelectedValue,
                txtQuantity.Text,
                txtUnitPrice.Text,
                txtPaidAmount.Text,
                cmbStatusID.SelectedValue);

            if (errors.Any())
            {
                ShowError(string.Join("\n", errors));
                btnSave.Enabled = false;
                return;
            }

            ShowSuccess("Все данные введены корректно! Нажмите 'Сохранить'.");
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isCustomerSelected)
                {
                    _serviceOrder.CustomerID = _selectedCustomer?.CustomerID;
                }
       

                // Avtomobil ixtiyoriy
                _serviceOrder.VehicleID = (int?)cmbVehicleID.SelectedValue == 0 ? null : (int?)cmbVehicleID.SelectedValue;
                _serviceOrder.Quantity = int.Parse(txtQuantity.Text);
                decimal unitPrice = decimal.Parse(txtUnitPrice.Text);
                _serviceOrder.TotalAmount = _serviceOrder.Quantity * unitPrice;
                _serviceOrder.PaidAmount = decimal.Parse(txtPaidAmount.Text);
                _serviceOrder.StatusID = (int)cmbStatusID.SelectedValue;
                _serviceOrder.ServiceID = (int)cmbServiceID.SelectedValue;

                var errors = _viewModel.UpdateServiceOrder(_serviceOrder, unitPrice);
                if (errors.Any())
                {
                    ShowError(string.Join("\n", errors));
                    return;
                }

                ShowSuccess("Сервисный заказ успешно обновлен!");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при сохранении: {ex.Message}");
                Console.WriteLine($"btnSave_Click Error: {ex}");
            }
        }

        private void cmbStatusID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) && cmbServiceID.SelectedValue != null)
                {
                    var minPrice = _viewModel.GetMinimumUnitPrice((int)cmbServiceID.SelectedValue);
                    if (unitPrice < minPrice)
                    {
                        ShowError($"Внимание: Цена ниже минимальной ({minPrice}). Установится при потере фокуса или сохранении.");
                    }
                    else
                    {
                        ValidateInputs();
                    }
                }
                ValidateInputs();
            }
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            if (!DesignMode && decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) && cmbServiceID.SelectedValue != null)
            {
                var minPrice = _viewModel.GetMinimumUnitPrice((int)cmbServiceID.SelectedValue);
                if (unitPrice < minPrice)
                {
                    txtUnitPrice.Text = minPrice.ToString();
                    ShowError($"Цена за единицу установлена на минимальное значение: {minPrice} (90% от цены услуги).");
                    ValidateInputs();
                }
                else
                {
                    ValidateInputs();
                }
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
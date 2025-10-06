using AvtoServis.Data;
using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.Forms.Modals.ServiceExpenses;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using EleCho.MvvmToolkit.ComponentModel.__Internals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms; // Control uchun qo'shildi

namespace AvtoServis.ViewModels.Screens
{
    public class ServiceOrderViewModel : INotifyPropertyChanged
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly IServiceOrdersRepository _serviceOrderRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ICarModelsRepository _carModelsRepository;
        private readonly ICustomerRepository _customersRepository;
        private readonly UsersRepository _userRepository;
        private readonly string _connectionString;
        private FullService _selectedService;
        private ServiceItemDto _selectedServiceItem;
        private CustomerInfo selectedCustomer { get; set; }
        private string _paidAmount;
        private string _customerSearchText;
        private string _notificationMessage;
        private readonly string _jsonFilePath = "selected_services.json";
        private readonly string _incompleteOrdersFilePath = "incomplete_orders.json";
        private bool _isProcessing;
        private int? _currentOrderId;
        private List<ServiceItemDto> _selectedServices;
        private static readonly object _fileLock = new object();
        public decimal DiscountPercentage { get; } = 0.1m; // Chegirma foizi (10%)

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<FullService> PopularServices { get; }
        public List<ServiceItemDto> SelectedServices
        {
            get => _selectedServices;
            set
            {
                if (_selectedServices != value)
                {
                    _selectedServices = value ?? new List<ServiceItemDto>();
                    OnPropertyChanged(nameof(SelectedServices));
                }
            }
        }
        public Dictionary<string, bool> ColumnVisibility { get; }
        public Dictionary<string, bool> SelectedColumnVisibility { get; }
        public int? CurrentOrderId => _currentOrderId;

        public string SelectedServiceInfo => _selectedServiceItem != null
            ? $"Название: **{_selectedServiceItem.ServiceName ?? "Не указан"}**\n" +
              $"Цена: **{_selectedServiceItem.Price:N2}**\n" +
              $"Количество: **{_selectedServiceItem.Quantity}**\n" +
              $"Итого: **{_selectedServiceItem.Total:N2}**\n" +
              $"ID автомобиля: **{_selectedServiceItem.VehicleId}**\n" +
              $"Статус ID: **{_selectedServiceItem.StatusId}**\n" +
              $"Оплачено: **{_selectedServiceItem.PaidAmount:N2}**"
            : _selectedService != null
                ? $"Название: **{_selectedService.Name ?? "Не указан"}**\n" +
                  $"Цена: **{_selectedService.Price:N2}**\n" +
                  $"Продано: **{_selectedService.SoldCount}**\n" +
                  $"Общая выручка: **{_selectedService.TotalRevenue:N2}**"
                : "Информация об услуге отсутствует";

        public string PaidAmount
        {
            get => _paidAmount;
            set
            {
                if (_paidAmount != value)
                {
                    _paidAmount = value;
                    OnPropertyChanged(nameof(PaidAmount));
                }
            }
        }

        public string CustomerSearchText
        {
            get => _customerSearchText;
            set
            {
                if (_customerSearchText != value)
                {
                    _customerSearchText = value;
                    OnPropertyChanged(nameof(CustomerSearchText));
                }
            }
        }

        public string NotificationMessage
        {
            get => _notificationMessage;
            set
            {
                if (_notificationMessage != value)
                {
                    _notificationMessage = value;
                    OnPropertyChanged(nameof(NotificationMessage));
                }
            }
        }

        public ServiceOrderViewModel(IServicesRepository servicesRepository, IServiceOrdersRepository serviceOrderRepository, IStatusRepository statusRepository, ICarModelsRepository carModelsRepository, ICustomerRepository customerRepository, UsersRepository userRepository, string connectionString)
        {
            if (servicesRepository == null)
            {
                throw new ArgumentNullException(nameof(servicesRepository));
            }
            if (serviceOrderRepository == null)
            {
                throw new ArgumentNullException(nameof(serviceOrderRepository));
            }
            if (statusRepository == null)
            {
                throw new ArgumentNullException(nameof(statusRepository));
            }
            if (carModelsRepository == null)
            {
                throw new ArgumentNullException(nameof(carModelsRepository));
            }
            if (customerRepository == null)
            {
                throw new ArgumentNullException(nameof(customerRepository));
            }

            _servicesRepository = servicesRepository;
            _serviceOrderRepository = serviceOrderRepository;
            _statusRepository = statusRepository;
            _carModelsRepository = carModelsRepository;
            _customersRepository = customerRepository;
            _userRepository = userRepository;



            _connectionString = connectionString;

            Console.WriteLine("ServiceOrderViewModel: Starting initialization...");

            PopularServices = new ObservableCollection<FullService>(_serviceOrderRepository.GetTopSellingServices() ?? new List<FullService>());
            _selectedServices = new List<ServiceItemDto>();
            ColumnVisibility = new Dictionary<string, bool>
            {
                { "Name", true },
                { "Price", true },
                { "SoldCount", false },
                { "TotalRevenue", false }
            };
            SelectedColumnVisibility = new Dictionary<string, bool>
            {
                { "ServiceName", true },
                { "Quantity", true },
                { "Price", true },
                { "Total", true },
                { "VehicleId", false },
                { "StatusId", false },
                { "PaidAmount", false }
            };
            _paidAmount = "0";
            _customerSearchText = "Найти клиента...";
            _notificationMessage = "";
            _currentOrderId = null;
            CustomerInfo customerInfo = new CustomerInfo();

            Console.WriteLine($"ServiceOrderViewModel: Initialization completed. PopularServices count = {PopularServices.Count}");
        }

        public FullService GetServiceById(int serviceId)
        {
            try
            {
                return _serviceOrderRepository.GetServiceById(serviceId);
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при получении услуги по ID: {ex.Message}";
                return null;
            }
        }

        public List<Status> GetStatuses()
        {
            try
            {
                return _statusRepository.GetAll("OrderStatuses");
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при получении статусов: {ex.Message}";
                return new List<Status>();
            }
        }

        public void SetCustomerInfo(CustomerInfo customer)
        {
            selectedCustomer = customer;
        }

        public void RefreshPopularServices()
        {
            try
            {
                PopularServices.Clear();
                var topSellingServices = _serviceOrderRepository.GetTopSellingServices() ?? new List<FullService>();
                foreach (var service in topSellingServices)
                {
                    PopularServices.Add(service);
                }
                OnPropertyChanged(nameof(PopularServices));
                NotificationMessage = "Список популярных услуг обновлен";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при обновлении популярных услуг: {ex.Message}";
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            // Проверка на null или пустую строку
            if (string.IsNullOrEmpty(propertyName))
            {
                NotificationMessage = "Имя свойства не может быть null или пустым.";
                return;
            }

            // Получение копии обработчика событий для thread-безопасности
            var handler = PropertyChanged;
            if (handler != null)
            {
                try
                {
                    // Получение списка делегатов для индивидуального вызова
                    var delegates = handler.GetInvocationList();
                    foreach (var del in delegates)
                    {
                        try
                        {
                            // Вызов каждого делегата отдельно
                            del.DynamicInvoke(this, new PropertyChangedEventArgs(propertyName));
                        }
                        catch (Exception ex)
                        {
                            // Логирование ошибки для конкретного подписчика
                            NotificationMessage = $"Ошибка при вызове PropertyChanged для свойства '{propertyName}': {ex.Message}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Общая ошибка при обработке события
                    NotificationMessage = $"Непредвиденная ошибка в OnPropertyChanged для свойства '{propertyName}': {ex.Message}";
                }
            }
        }


        public List<ServiceItemDto> LoadSelectedServicesFromJson()
        {
            lock (_fileLock)
            {
                try
                {
                    if (File.Exists(_jsonFilePath))
                    {
                        var json = File.ReadAllText(_jsonFilePath);
                        var loadedServices = JsonSerializer.Deserialize<List<ServiceItemDto>>(json);
                        var validServices = new List<ServiceItemDto>();
                        if (loadedServices != null)
                        {
                            foreach (var service in loadedServices)
                            {
                                if (service == null) continue;
                                var fullService = _serviceOrderRepository.GetServiceById(service.ServiceId);
                                if (fullService != null)
                                {
                                    decimal minPrice = fullService.Price * (1 - DiscountPercentage); // Minimal narx chegirma asosida
                                    service.Quantity = service.Quantity <= 0 ? 1 : service.Quantity;
                                    service.Price = Math.Round(Math.Max(service.Price, minPrice), 2);
                                    service.Total = service.Quantity * service.Price;
                                    service.ServiceName = service.ServiceName ?? fullService.Name ?? string.Empty;
                                    validServices.Add(service);
                                }
                            }
                        }
                        SelectedServices = validServices;
                        NotificationMessage = "Список выбранных услуг загружен";
                        return validServices;
                    }
                }
                catch (IOException ex)
                {
                    NotificationMessage = $"Ошибка ввода-вывода при чтении JSON файла: {ex.Message}";
                }
                catch (JsonException ex)
                {
                    NotificationMessage = $"Ошибка формата JSON при чтении файла: {ex.Message}";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Ошибка при чтении из JSON файла: {ex.Message}";
                }
                return new List<ServiceItemDto>();
            }
        }

        public void SaveSelectedServicesToJson()
        {
            lock (_fileLock)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_jsonFilePath))
                    {
                        NotificationMessage = "Фатальная ошибка: Путь к JSON файлу не указан. Установлен путь по умолчанию.";
                    }

                    string directoryPath = Path.GetDirectoryName(_jsonFilePath);
                    if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                        NotificationMessage = $"Директория {directoryPath} создана.";
                    }

                    if (SelectedServices == null)
                    {
                        SelectedServices = new List<ServiceItemDto>();
                        NotificationMessage = "Список выбранных услуг пуст. Сохранен пустой список.";
                    }

                    var validServices = SelectedServices
                        .Where(s => s != null && s.ServiceId > 0 && !string.IsNullOrEmpty(s.ServiceName))
                        .ToList();

                    if (validServices.Count != SelectedServices.Count)
                    {
                        NotificationMessage = "Обнаружены некорректные элементы в списке услуг. Сохранены только валидные элементы.";
                        SelectedServices = validServices;
                    }

                    var jsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                    };
                    string json = JsonSerializer.Serialize(SelectedServices, jsonOptions);
                    File.WriteAllText(_jsonFilePath, json);
                    NotificationMessage = $"Список выбранных услуг успешно сохранен в {_jsonFilePath}";
                }
                catch (ArgumentException ex)
                {
                    NotificationMessage = $"Ошибка в пути к файлу: {ex.Message}. Убедитесь, что путь к файлу корректен.";
                }
                catch (UnauthorizedAccessException ex)
                {
                    NotificationMessage = $"Ошибка доступа: Нет прав для записи в файл {_jsonFilePath}. {ex.Message}";
                }
                catch (IOException ex)
                {
                    NotificationMessage = $"Ошибка ввода-вывода при записи в JSON файл: {ex.Message}";
                }
                catch (JsonException ex)
                {
                    NotificationMessage = $"Ошибка формата JSON при записи в файл: {ex.Message}";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Общая ошибка при записи в JSON файл: {ex.Message}";
                }
            }
        }

        public void SaveIncompleteOrder()
        {
            lock (_fileLock)
            {
                try
                {
                    if (SelectedServices == null || !SelectedServices.Any())
                    {
                        NotificationMessage = "Сохранять нечего: список услуг пуст";
                        return;
                    }

                    var incompleteOrders = GetIncompleteOrders() ?? new List<IncompleteOrderDto>();
                    if (_currentOrderId == null)
                    {
                        _currentOrderId = incompleteOrders.Any() ? incompleteOrders.Max(s => s.OrderId) + 1 : 1;
                    }

                    var order = new IncompleteOrderDto
                    {
                        OrderId = _currentOrderId ?? 0,
                        CustomerName = selectedCustomer?.FullName ?? (_customerSearchText != "Найти клиента..." ? _customerSearchText : null),
                        CustomerId = selectedCustomer?.CustomerID,
                        CustomerPhone = selectedCustomer?.Phone,
                        TotalAmount = SelectedServices.Sum(x => x.Quantity * x.Price),
                        PaidAmount = decimal.TryParse(_paidAmount, out var paid) ? Math.Round(paid, 2) : 0,
                        Date = DateTime.Now,
                        Items = SelectedServices.ToList()
                    };

                    var existingOrder = incompleteOrders.FirstOrDefault(s => s.OrderId == _currentOrderId);
                    if (existingOrder != null)
                    {
                        incompleteOrders.Remove(existingOrder);
                    }
                    incompleteOrders.Add(order);

                    var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                    var json = JsonSerializer.Serialize(incompleteOrders, jsonOptions);
                    File.WriteAllText(_incompleteOrdersFilePath, json);
                    NotificationMessage = $"Незавершенный заказ {_currentOrderId} успешно сохранен";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Ошибка при сохранении незавершенного заказа: {ex.Message}";
                }
            }
        }

        public List<IncompleteOrderDto> GetIncompleteOrders()
        {
            lock (_fileLock)
            {
                try
                {
                    if (File.Exists(_incompleteOrdersFilePath))
                    {
                        var json = File.ReadAllText(_incompleteOrdersFilePath);
                        var orders = JsonSerializer.Deserialize<List<IncompleteOrderDto>>(json);
                        return orders?.Where(s => s != null).ToList() ?? new List<IncompleteOrderDto>();
                    }
                }
                catch (IOException ex)
                {
                    NotificationMessage = $"Ошибка ввода-вывода при загрузке незавершенных заказов: {ex.Message}";
                }
                catch (JsonException ex)
                {
                    NotificationMessage = $"Ошибка формата JSON при загрузке незавершенных заказов: {ex.Message}";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Ошибка при загрузке незавершенных заказов: {ex.Message}";
                }
                return new List<IncompleteOrderDto>();
            }
        }

        public void LoadIncompleteOrder(int orderId)
        {
            lock (_fileLock)
            {
                try
                {
                    var incompleteOrders = GetIncompleteOrders() ?? new List<IncompleteOrderDto>();
                    var order = incompleteOrders.FirstOrDefault(s => s.OrderId == orderId);
                    if (order == null)
                    {
                        NotificationMessage = $"Заказ с ID {orderId} не найден";
                        return;
                    }

                    _currentOrderId = order.OrderId;
                    SelectedServices = order.Items?.Select(item =>
                    {
                        var fullService = _serviceOrderRepository.GetServiceById(item.ServiceId);
                        if (fullService != null)
                        {
                            decimal minPrice = fullService.Price * (1 - DiscountPercentage); // Minimal narx chegirma asosida
                            item.Price = Math.Round(Math.Max(item.Price, minPrice), 2);
                            item.Total = item.Quantity * item.Price;
                        }
                        return item;
                    }).ToList() ?? new List<ServiceItemDto>();
                    PaidAmount = order.PaidAmount.ToString("N2");
                    CustomerSearchText = order.CustomerName ?? "Найти клиента...";
                    selectedCustomer = new CustomerInfo
                    {
                        CustomerID = order.CustomerId ?? 0,
                        FullName = order.CustomerName,
                        Phone = order.CustomerPhone
                    };
                    OnPropertyChanged(nameof(SelectedServices));
                    OnPropertyChanged(nameof(SelectedServiceInfo));
                    OnPropertyChanged(nameof(CustomerSearchText));
                    NotificationMessage = $"Заказ {orderId} успешно загружен";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Ошибка при загрузке незавершенного заказа: {ex.Message}";
                }
            }
        }

        public void DeleteIncompleteOrder(int orderId)
        {
            lock (_fileLock)
            {
                try
                {
                    var incompleteOrders = GetIncompleteOrders() ?? new List<IncompleteOrderDto>();
                    var order = incompleteOrders.FirstOrDefault(s => s.OrderId == orderId);
                    if (order != null)
                    {
                        incompleteOrders.Remove(order);
                        File.WriteAllText(_incompleteOrdersFilePath, JsonSerializer.Serialize(incompleteOrders, new JsonSerializerOptions { WriteIndented = true }));
                        if (_currentOrderId == orderId)
                        {
                            _currentOrderId = null;
                            SelectedServices = new List<ServiceItemDto>();
                        }
                        NotificationMessage = $"Незавершенный заказ {orderId} удален";
                    }
                    else
                    {
                        NotificationMessage = $"Заказ с ID {orderId} не найден";
                    }
                }
                catch (IOException ex)
                {
                    NotificationMessage = $"Ошибка ввода-вывода при удалении незавершенного заказа: {ex.Message}";
                }
                catch (JsonException ex)
                {
                    NotificationMessage = $"Ошибка формата JSON при удалении незавершенного заказа: {ex.Message}";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Ошибка при удалении незавершенного заказа: {ex.Message}";
                }
            }
        }

        public void DeleteCurrentIncompleteOrder()
        {
            if (_isProcessing || !_currentOrderId.HasValue) return;
            _isProcessing = true;
            try
            {
                DeleteIncompleteOrder(_currentOrderId.Value);
                _currentOrderId = null;
                AddNewService();
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при удалении текущего незавершенного заказа: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }
        // YANGI METOD: selected_services.json ni to'liq tozalash (bo'shatish)
        public void ClearSelectedServicesJson()
        {
            lock (_fileLock)
            {
                try
                {
                    // In-memory ni bo'shatish
                    SelectedServices = new List<ServiceItemDto>();
                    OnPropertyChanged(nameof(SelectedServices));

                    // Faylga bo'sh ro'yxat yozish (tozalash)
                    var jsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                    };
                    string emptyJson = JsonSerializer.Serialize(SelectedServices, jsonOptions);  // [] bo'ladi
                    File.WriteAllText(_jsonFilePath, emptyJson);

                    // Direktoriya mavjudligini tekshirish
                    string directoryPath = Path.GetDirectoryName(_jsonFilePath);
                    if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    NotificationMessage = $"Файл selected_services.json полностью очищен (пустой список сохранен)";
                }
                catch (ArgumentException ex)
                {
                    NotificationMessage = $"Ошибка в пути к файлу: {ex.Message}";
                }
                catch (UnauthorizedAccessException ex)
                {
                    NotificationMessage = $"Ошибка доступа: Нет прав для записи в файл. {ex.Message}";
                }
                catch (IOException ex)
                {
                    NotificationMessage = $"Ошибка ввода-вывода при очистке JSON файла: {ex.Message}";
                }
                catch (JsonException ex)
                {
                    NotificationMessage = $"Ошибка формата JSON при очистке файла: {ex.Message}";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Общая ошибка при очистке selected_services.json: {ex.Message}";
                }
            }
        }
        public void ShowColumnSelectionDialog(Form parent)
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                using (var form = new Form
                {
                    Text = "Выбор столбцов",
                    Size = new Size(300, 350),
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
                        Size = new Size(260, 250),
                        CheckOnClick = true,
                        Font = new Font("Segoe UI", 10F),
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "Name", "Название услуги" },
                        { "Price", "Цена" },
                        { "SoldCount", "Продано" },
                        { "TotalRevenue", "Общая выручка" }
                    };
                    foreach (var column in ColumnVisibility ?? new Dictionary<string, bool>())
                    {
                        checkedListBox.Items.Add(new { Name = column.Key, DisplayName = columnMapping[column.Key] }, column.Value);
                    }
                    checkedListBox.DisplayMember = "DisplayName";
                    checkedListBox.ValueMember = "Name";

                    var btnOk = new Button
                    {
                        Text = "OK",
                        Location = new Point(100, 270),
                        Size = new Size(80, 30),
                        BackColor = Color.FromArgb(40, 167, 69),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(60, 187, 89) },
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                    };
                    btnOk.Click += (s, ev) =>
                    {
                        if (checkedListBox.CheckedItems.Count == 0)
                        {
                            NotificationMessage = "Необходимо выбрать хотя бы один столбец";
                            return;
                        }
                        if (checkedListBox.CheckedItems.Count > 5)
                        {
                            NotificationMessage = "Можно выбрать максимум 5 столбцов";
                            return;
                        }
                        foreach (var column in ColumnVisibility.Keys.ToList())
                        {
                            ColumnVisibility[column] = false;
                        }
                        foreach (var item in checkedListBox.CheckedItems)
                        {
                            var col = (dynamic)item;
                            ColumnVisibility[col.Name] = true;
                        }
                        OnPropertyChanged(nameof(ColumnVisibility));
                        NotificationMessage = "Столбцы для популярных услуг обновлены";
                        form.Close();
                    };

                    form.Controls.Add(checkedListBox);
                    form.Controls.Add(btnOk);
                    form.ShowDialog(parent);
                }
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при открытии диалога выбора столбцов: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void ShowSelectedColumnsDialog(Form parent)
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                using (var form = new Form
                {
                    Text = "Выбор столбцов для выбранных услуг",
                    Size = new Size(300, 320),
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
                        Size = new Size(260, 220),
                        CheckOnClick = true,
                        Font = new Font("Segoe UI", 10F),
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "ServiceName", "Название услуги" },
                        { "Quantity", "Количество" },
                        { "Price", "Цена" },
                        { "Total", "Итого" },
                        { "VehicleId", "ID автомобиля" },
                        { "StatusId", "Статус ID" },
                        { "PaidAmount", "Оплаченная сумма" }
                    };
                    foreach (var column in SelectedColumnVisibility ?? new Dictionary<string, bool>())
                    {
                        checkedListBox.Items.Add(new { Name = column.Key, DisplayName = columnMapping[column.Key] }, column.Value);
                    }
                    checkedListBox.DisplayMember = "DisplayName";
                    checkedListBox.ValueMember = "Name";

                    var btnOk = new Button
                    {
                        Text = "OK",
                        Location = new Point(100, 240),
                        Size = new Size(80, 30),
                        BackColor = Color.FromArgb(40, 167, 69),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(60, 187, 89) },
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                    };
                    btnOk.Click += (s, ev) =>
                    {
                        if (checkedListBox.CheckedItems.Count == 0)
                        {
                            NotificationMessage = "Необходимо выбрать хотя бы один столбец";
                            return;
                        }
                        if (checkedListBox.CheckedItems.Count > 7)
                        {
                            NotificationMessage = "Можно выбрать максимум 7 столбцов";
                            return;
                        }
                        foreach (var column in SelectedColumnVisibility.Keys.ToList())
                        {
                            SelectedColumnVisibility[column] = false;
                        }
                        foreach (var item in checkedListBox.CheckedItems)
                        {
                            var col = (dynamic)item;
                            SelectedColumnVisibility[col.Name] = true;
                        }
                        OnPropertyChanged(nameof(SelectedColumnVisibility));
                        NotificationMessage = "Столбцы для выбранных услуг обновлены";
                        form.Close();
                    };

                    form.Controls.Add(checkedListBox);
                    form.Controls.Add(btnOk);
                    form.ShowDialog(parent);
                }
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при открытии диалога выбора столбцов для выбранных услуг: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void SelectService(int index)
        {
            if (_isProcessing || index < 0 || index >= PopularServices?.Count)
            {
                _selectedService = null;
                OnPropertyChanged(nameof(SelectedServiceInfo));
                return;
            }
            _isProcessing = true;
            try
            {
                _selectedService = PopularServices[index];
                _selectedServiceItem = null;
                OnPropertyChanged(nameof(SelectedServiceInfo));
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при выборе услуги: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void SelectSelectedService(int index)
        {
            if (_isProcessing || index < 0 || index >= SelectedServices?.Count)
            {
                _selectedServiceItem = null;
                OnPropertyChanged(nameof(SelectedServiceInfo));
                return;
            }
            _isProcessing = true;
            try
            {
                _selectedServiceItem = SelectedServices[index];
                _selectedService = null;
                OnPropertyChanged(nameof(SelectedServiceInfo));
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при выборе услуги из списка: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void AddServiceToOrder(FullService service)
        {
            if (_isProcessing || service == null) return;
            _isProcessing = true;
            try
            {
                var existingItem = SelectedServices.FirstOrDefault(x => x.ServiceId == service.ServiceID);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                    existingItem.Total = existingItem.Quantity * existingItem.Price;
                }
                else
                {
                    var price = Math.Round(service.Price, 2); // Asl narx ishlatiladi
                    SelectedServices.Add(new ServiceItemDto
                    {
                        ServiceId = service.ServiceID,
                        ServiceName = service.Name ?? string.Empty,
                        Quantity = 1,
                        Price = price,
                        Total = price,
                        VehicleId = 0,
                        StatusId = 1,
                        PaidAmount = 0
                    });
                }
                NotificationMessage = $"Услуга '{service.Name ?? "Не указан"}' добавлена в заказ";
                OnPropertyChanged(nameof(SelectedServices));
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при добавлении услуги в заказ: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void AddServiceToSelection(int index)
        {
            if (_isProcessing || index < 0 || index >= PopularServices?.Count) return;
            _isProcessing = true;
            try
            {
                var service = PopularServices[index];
                AddServiceToOrder(service);
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при добавлении услуги в выбор: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void RemoveSelectedService(int index)
        {
            if (_isProcessing || index < 0 || index >= SelectedServices?.Count) return;
            _isProcessing = true;
            try
            {
                var service = SelectedServices[index];
                SelectedServices.RemoveAt(index);
                _selectedServiceItem = null;
                _selectedService = null;
                OnPropertyChanged(nameof(SelectedServices));
                OnPropertyChanged(nameof(SelectedServiceInfo));
                NotificationMessage = $"Услуга '{service?.ServiceName ?? "Не указан"}' удалена из заказа";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при удалении услуги из заказа: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void UpdateServiceQuantity(int index, int quantity)
        {
            if (_isProcessing || index < 0 || index >= SelectedServices?.Count) return;
            _isProcessing = true;
            try
            {
                var service = SelectedServices[index];
                var fullService = _serviceOrderRepository.GetServiceById(service.ServiceId);
                if (fullService == null)
                {
                    NotificationMessage = $"Услуга '{service.ServiceName ?? "Не указан"}' не найдена в базе данных";
                    service.Quantity = 1;
                    service.Total = service.Quantity * service.Price;
                }
                else
                {
                    service.Quantity = quantity <= 0 ? 1 : quantity; // Miqdor kamida 1 bo'ladi
                    service.Total = service.Quantity * service.Price;
                    NotificationMessage = $"Количество для услуги '{service.ServiceName ?? "Не указан"}' обновлено до {service.Quantity}";
                }
                OnPropertyChanged(nameof(SelectedServices));
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при обновлении количества услуги: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void UpdateServicePrice(int index, decimal price)
        {
            if (_isProcessing || index < 0 || index >= SelectedServices?.Count) return;
            _isProcessing = true;
            try
            {
                var service = SelectedServices[index];
                var fullService = _serviceOrderRepository.GetServiceById(service.ServiceId);
                if (fullService == null)
                {
                    NotificationMessage = $"Услуга '{service.ServiceName ?? "Не указан"}' не найдена в базе данных";
                    service.Price = service.Price;
                    service.Total = service.Quantity * service.Price;
                }
                else
                {
                    decimal minPrice = fullService.Price * (1 - DiscountPercentage); // Minimal narx chegirma asosida
                    service.Price = Math.Round(Math.Max(price, minPrice), 2); // Narx minimal narxdan past bo'lmaydi
                    service.Total = service.Quantity * service.Price;
                    NotificationMessage = $"Цена для услуги '{service.ServiceName ?? "Не указан"}' обновлена до {service.Price:N2}";
                }
                OnPropertyChanged(nameof(SelectedServices));
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при обновлении цены услуги: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void ConfirmPaidAmount()
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                if (!decimal.TryParse(_paidAmount, out decimal paidAmount) || paidAmount < 0)
                {
                    NotificationMessage = "Некорректная сумма оплаты";
                    PaidAmount = "0";
                    return;
                }
                PaidAmount = Math.Round(paidAmount, 2).ToString("N2");
                NotificationMessage = "Сумма оплаты подтверждена";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при подтверждении суммы оплаты: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void OpenSearchDialog()
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                var serviceOrdersViewModel = new ServiceOrdersViewModel(
                    _serviceOrderRepository,
                    _customersRepository,
                    _servicesRepository,
                    _carModelsRepository,
                    _statusRepository,
                    _userRepository,
                    _connectionString

                )
                {

                };
                using (var dialog = new SearchServiceDialog(serviceOrdersViewModel, this, new ImageList()))
                {
                    dialog.ShowDialog();
                }
                NotificationMessage = "Диалог поиска услуги закрыт";
                System.Diagnostics.Debug.WriteLine("OpenSearchDialog: SearchServiceDialog closed");
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при открытии диалога поиска услуги: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"OpenSearchDialog Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void AddServiceFromSearch(FullService service)
        {
            try
            {
                _isProcessing = false;
                AddServiceToOrder(service);
                NotificationMessage = $"Услуга '{service.Name ?? "Не указан"}' добавлена из поиска";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при добавлении услуги из поиска: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void AddNewService()
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                if (SelectedServices != null && SelectedServices.Any())
                {
                    SaveIncompleteOrder();
                    NotificationMessage = $"Текущий заказ {_currentOrderId ?? 0} сохранен перед началом нового";
                }

                SelectedServices = new List<ServiceItemDto>();
                PaidAmount = "0";
                CustomerSearchText = "Найти клиента...";
                _selectedServiceItem = null;
                _selectedService = null;
                _currentOrderId = null;
                OnPropertyChanged(nameof(SelectedServices));
                OnPropertyChanged(nameof(SelectedServiceInfo));
                NotificationMessage = "Новый заказ начат";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при создании нового заказа: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void ContinueOrder()
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                if (SelectedServices != null && SelectedServices.Any())
                {
                    var order = new ServiceOrderDto
                    {
                        OrderId = _currentOrderId ?? 0,
                        Items = SelectedServices.ToList(),
                        PaidAmount = decimal.TryParse(_paidAmount, out var paid) ? Math.Round(paid, 2) : 0,
                        CustomerName = _customerSearchText != "Найти клиента..." ? _customerSearchText : null,
                        CustomerId = selectedCustomer?.CustomerID,
                        CustomerPhone = selectedCustomer?.Phone,
                        TotalAmount = SelectedServices.Sum(x => x.Quantity * x.Price),
                        Date = DateTime.Now
                    };

                    decimal totalAmount = order.TotalAmount;
                    decimal remainingPaid = order.PaidAmount;

                    // 1️⃣ Avval xizmatlar uchun minimal narxni hisoblash
                    foreach (var item in order.Items)
                    {
                        var serviceIncome = _serviceOrderRepository.GetServiceById(item.ServiceId);
                        if (serviceIncome == null)
                        {
                            NotificationMessage = $"Услуга с ID {item.ServiceId} не найдена в приходах";
                            return;
                        }

                        decimal minPrice = serviceIncome.Price * (1 - DiscountPercentage);
                        item.Price = Math.Max(item.Price, minPrice);
                        item.Total = item.Quantity * item.Price;
                    }

                    // 2️⃣ Avvaldan belgilangan PaidAmount'larni hisobga olish
                    foreach (var item in order.Items)
                    {
                        if (item.PaidAmount > 0)
                        {
                            decimal prePaid = Math.Min(item.PaidAmount, item.Total);
                            item.PaidAmount = prePaid;
                            remainingPaid -= prePaid;
                        }
                    }

                    // 3️⃣ Qolgan summani taqsimlash
                    var unpaidItems = order.Items.Where(x => x.PaidAmount < x.Total).ToList();

                    while (remainingPaid > 0 && unpaidItems.Any())
                    {
                        int count = unpaidItems.Count;
                        decimal share = remainingPaid / count;

                        var stillUnpaid = new List<ServiceItemDto>();

                        foreach (var item in unpaidItems)
                        {
                            decimal need = item.Total - item.PaidAmount;
                            decimal add = Math.Min(share, need);
                            item.PaidAmount += add;
                            remainingPaid -= add;

                            if (item.PaidAmount < item.Total)
                                stillUnpaid.Add(item);
                        }

                        unpaidItems = stillUnpaid;
                        if (share < 0.01m) break; // juda kichik summada aylanishni to‘xtatish
                    }

                    // 4️⃣ Har bir item uchun FinanceStatusID aniqlash
                    var serviceOrders = new List<ServiceOrder>();

                    foreach (var item in order.Items)
                    {
                        int financeStatusId;
                        if (item.PaidAmount <= 0)
                            financeStatusId = 2; // Не оплачен
                        else if (item.PaidAmount < item.Total)
                            financeStatusId = 3; // Частично Оплачен
                        else
                            financeStatusId = 1; // Оплачен

                        var serviceOrder = new ServiceOrder
                        {
                            OrderID = order.OrderId,
                            CustomerID = order.CustomerId,
                            VehicleID = item.VehicleId,
                            ServiceID = item.ServiceId,
                            OperationID = 0,
                            UserID = GetCurrentUserId() ?? 0,
                            OrderDate = order.Date,
                            Quantity = item.Quantity,
                            PaidAmount = item.PaidAmount,
                            StatusID = item.StatusId,
                            FinanceStatusID = financeStatusId,
                            TotalAmount = item.Total
                        };

                        serviceOrders.Add(serviceOrder);
                    }

                    // 5️⃣ Saqlash
                    foreach (var so in serviceOrders)
                    {
                        _serviceOrderRepository.Add(so);
                    }

                    NotificationMessage = $"Заказ {order.OrderId} успешно сохранен в базе данных";
                    ClearSelectedServicesJson();
                }
                else
                {
                    NotificationMessage = "Нет услуг для сохранения заказа";
                }
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при продолжении заказа: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }


        private int? GetCurrentUserId()
        {
            return null;
        }

        public void CancelAndClose()
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                //SaveIncompleteOrder();
                SaveSelectedServicesToJson();
                SelectedServices = new List<ServiceItemDto>();
                PaidAmount = "0";
                CustomerSearchText = "Найти клиента...";
                _selectedService = null;
                _selectedServiceItem = null;
                _currentOrderId = null;
                OnPropertyChanged(nameof(SelectedServices));
                OnPropertyChanged(nameof(SelectedServiceInfo));
                NotificationMessage = "Заказ отменен и форма закрыта";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при отмене и закрытии заказа: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void ClearCustomerSearchPlaceholder()
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                if (CustomerSearchText == "Найти клиента...")
                {
                    CustomerSearchText = "";
                    NotificationMessage = "Поле поиска клиента очищено";
                }
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при очистке поля поиска клиента: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void SetCustomerSearchPlaceholder()
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                if (string.IsNullOrWhiteSpace(CustomerSearchText))
                {
                    CustomerSearchText = "Найти клиента...";
                    NotificationMessage = "Установлен текст-заполнитель для поиска клиента";
                }
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при установке текста-заполнителя: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public List<CarModel> GetCarModelByCustomerId(int? customerId=null)
        {
            try
            {
                if (customerId == 0)
                {
                    return _carModelsRepository.GetCarModels();
                }
                return _carModelsRepository.GetCarModels(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении моделей автомобилей: " + ex.Message, ex);
            }
        }
    }
}
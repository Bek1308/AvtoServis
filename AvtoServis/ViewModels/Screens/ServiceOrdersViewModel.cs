using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.Forms;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AvtoServis.ViewModels.Screens
{
    public class ServiceOrdersViewModel
    {
        private readonly IServiceOrdersRepository _serviceOrdersRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IServicesRepository _serviceRepository;
        private readonly ICarModelsRepository _vehicleRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly UsersRepository _userRepository; 
        private List<ServiceOrderDto> _serviceOrders;
        private readonly Dictionary<string, string> _columnMapping;
        internal string ConnectionString;

        public ServiceOrdersViewModel(
            IServiceOrdersRepository serviceOrdersRepository,
            ICustomerRepository customerRepository,
            IServicesRepository serviceRepository,
            ICarModelsRepository vehicleRepository,
            IStatusRepository statusRepository,
            UsersRepository userRepository,
            string connectionString)
        {
            _serviceOrdersRepository = serviceOrdersRepository ?? throw new ArgumentNullException(nameof(serviceOrdersRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _statusRepository = statusRepository ?? throw new ArgumentNullException(nameof(statusRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            ConnectionString = connectionString;
            _serviceOrders = new List<ServiceOrderDto>();
            VisibleColumns = new List<string>();
            Filters = new List<(string Column, string Operator, string SearchText)>();
            // O'ZGARTIRILDI: TotalAmount qo'shildi
            _columnMapping = new Dictionary<string, string>
            {
                { "OrderID", "ID" },
                { "CustomerName", "Имя клиента" },
                { "ServiceName", "Услуга" },
                { "Quantity", "Количество" },
                { "TotalAmount", "Общая сумма" },  // Qo'shildi: TotalAmount uchun
                { "OrderDate", "Дата заказа" },
                { "FinanceStatusID", "Статус оплаты" },
                { "PaidAmount", "Оплаченная сумма" },
                { "VehicleModel", "Модель автомобиля" },
                { "StatusName", "Статус" },
                { "UserName", "Продавец" }
            };
        }

        public List<string> VisibleColumns { get; set; }
        public List<(string Column, string Operator, string SearchText)> Filters { get; set; }
        public Dictionary<string, string> ColumnMapping => _columnMapping;

        public List<ServiceOrderDto> LoadServiceOrders()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading service orders...");
                var rawOrders = _serviceOrdersRepository.GetAll();
                System.Diagnostics.Debug.WriteLine($"Retrieved {rawOrders.Count} raw orders");
                // O'ZGARTIRILDI: OrderId -> OrderID (kodga mos)
                _serviceOrders = rawOrders.Select(o => new ServiceOrderDto
                {
                    OrderID = o.OrderID,  // Tuzatildi: OrderId -> OrderID
                    CustomerName = GetCustomerName(o.CustomerID ?? 0),
                    ServiceName = GetServiceName(o.ServiceID),
                    Quantity = o.Quantity,
                    OrderDate = o.OrderDate,
                    FinanceStatusID = o.FinanceStatusID,
                    PaidAmount = o.PaidAmount,
                    VehicleModel = GetVehicleModel(o.VehicleID),
                    StatusName = GetStatusName(o.StatusID),
                    UserName = GetUserName(o.UserID),
                    TotalAmount = o.TotalAmount ?? 0
                }).ToList();
                System.Diagnostics.Debug.WriteLine($"Mapped {_serviceOrders.Count} service orders");

                if (Filters != null && Filters.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"Applying filters: {string.Join(", ", Filters.Select(f => $"{f.Column} {f.Operator} {f.SearchText}"))}");
                    _serviceOrders = ApplyFilters(_serviceOrders);
                    System.Diagnostics.Debug.WriteLine($"Filtered to {_serviceOrders.Count} orders");
                }
                return _serviceOrders;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadServiceOrders Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Error loading service orders: " + ex.Message, ex);
            }
        }

        public List<FullService> LoadServices()
        {
            try
            {
                return _serviceOrdersRepository.GetFullServices();
            }
            catch (Exception ex)
            {
                throw new Exception("Xizmatlarni yuklashda xatolik yuz berdi: " + ex.Message, ex);
            }
        }

        private List<ServiceOrderDto> ApplyFilters(List<ServiceOrderDto> data)
        {
            try
            {
                var result = data.AsEnumerable();
                var reverseColumnNames = _columnMapping.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

                foreach (var filter in Filters)
                {
                    if (!reverseColumnNames.ContainsKey(filter.Column))
                    {
                        System.Diagnostics.Debug.WriteLine($"Skipping invalid column: {filter.Column}");
                        continue;
                    }
                    string propertyName = reverseColumnNames[filter.Column];
                    var property = typeof(ServiceOrderDto).GetProperty(propertyName);
                    if (property == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Property not found: {propertyName}");
                        continue;
                    }

                    result = result.Where(item =>
                    {
                        var value = property.GetValue(item);
                        if (value == null)
                        {
                            return false;
                        }

                        string stringValue = value.ToString();
                        if (string.IsNullOrEmpty(stringValue))
                        {
                            return false;
                        }

                        switch (propertyName)
                        {
                            case "FinanceStatusID":
                                var financeStatusText = filter.SearchText switch
                                {
                                    "Оплачен" => "1",
                                    "Не оплачен" => "2",
                                    "Частично оплачен" => "3",
                                    _ => null
                                };
                                if (financeStatusText == null)
                                {
                                    return false;
                                }
                                var itemFinanceStatus = item.FinanceStatusID.ToString();
                                return filter.Operator switch
                                {
                                    "=" => itemFinanceStatus == financeStatusText,
                                    "!=" => itemFinanceStatus != financeStatusText,
                                    _ => false
                                };
                            case "OrderDate":
                                if (!DateTime.TryParse(filter.SearchText, out var filterDate) || !DateTime.TryParse(stringValue, out var itemDate))
                                {
                                    return false;
                                }
                                return filter.Operator switch
                                {
                                    "=" => itemDate.Date == filterDate.Date,
                                    ">" => itemDate.Date > filterDate.Date,
                                    "<" => itemDate.Date < filterDate.Date,
                                    ">=" => itemDate.Date >= filterDate.Date,
                                    "<=" => itemDate.Date <= filterDate.Date,
                                    _ => false
                                };
                            // O'ZGARTIRILDI: TotalAmount qo'shildi
                            case "OrderID":
                            case "Quantity":
                            case "PaidAmount":
                            case "TotalAmount":  // Qo'shildi: TotalAmount uchun
                                if (!decimal.TryParse(filter.SearchText, out var filterValue) || !decimal.TryParse(stringValue, out var itemValue))
                                {
                                    return false;
                                }
                                return filter.Operator switch
                                {
                                    "=" => itemValue == filterValue,
                                    ">" => itemValue > filterValue,
                                    "<" => itemValue < filterValue,
                                    ">=" => itemValue >= filterValue,
                                    "<=" => itemValue <= filterValue,
                                    "!=" => itemValue != filterValue,
                                    _ => false
                                };
                            case "VehicleModel":
                            case "CustomerName":
                            case "ServiceName":
                            case "StatusName":
                            case "UserName":
                                return filter.Operator == "LIKE" && stringValue.ToLower().Contains(filter.SearchText.ToLower());
                            default:
                                return false;
                        }
                    });
                }
                return result.ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ApplyFilters Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Error applying filters: " + ex.Message, ex);
            }
        }

        // O'ZGARTIRILDI: Asosiy tuzatish bu yerda - reverseColumnNames olib tashlandi, visibleColumns to'g'ridan ishlatiladi
        public List<ServiceOrderDto> SearchServiceOrders(string searchText, List<string> visibleColumns)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Searching with text: '{searchText}', visible columns: {string.Join(", ", visibleColumns ?? new List<string>())}");
                var data = LoadServiceOrders();  // Filtrlarni hisobga olgan holda yuklash
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    System.Diagnostics.Debug.WriteLine("Search text is empty, returning full data.");
                    return data;
                }

                // reverseColumnNames kerak emas: visibleColumns allaqachon property nomlari
                return data.Where(item =>
                {
                    foreach (var propertyName in visibleColumns ?? new List<string>())
                    {
                        var property = typeof(ServiceOrderDto).GetProperty(propertyName);
                        if (property == null)
                        {
                            System.Diagnostics.Debug.WriteLine($"Property not found: {propertyName}");
                            continue;
                        }

                        var value = property.GetValue(item)?.ToString();
                        if (string.IsNullOrEmpty(value))
                        {
                            continue;
                        }

                        if (propertyName == "FinanceStatusID")
                        {
                            var financeStatusText = item.FinanceStatusID switch
                            {
                                1 => "Оплачен",
                                2 => "Не оплачен",
                                3 => "Частично оплачен",
                                _ => "Неизвестно"
                            };
                            if (financeStatusText.ToLower().Contains(searchText.ToLower()))
                            {
                                System.Diagnostics.Debug.WriteLine($"Match found in FinanceStatusID: {financeStatusText}");
                                return true;
                            }
                        }
                        else if (value.ToLower().Contains(searchText.ToLower()))
                        {
                            System.Diagnostics.Debug.WriteLine($"Match found in {propertyName}: {value}");
                            return true;
                        }
                    }
                    return false;
                }).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchServiceOrders Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Error searching service orders: " + ex.Message, ex);
            }
        }

        // O'ZGARTIRILDI: TryGetValue qo'shildi (xavfsizlik uchun), TotalAmount uchun handling
        public void ExportToExcel(List<ServiceOrderDto> data, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Exporting {data.Count} records to {filePath}");
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("ServiceOrders");
                    int colIndex = 1;
                    var visibleColumns = columnVisibility
                        .Where(c => c.Value && c.Key != "Action")
                        .Select(c => c.Key)
                        .ToList();

                    foreach (var column in visibleColumns)
                    {
                        worksheet.Cell(1, colIndex).Value = _columnMapping.TryGetValue(column, out var header) ? header : column;
                        colIndex++;
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        colIndex = 1;
                        foreach (var column in visibleColumns)
                        {
                            var property = typeof(ServiceOrderDto).GetProperty(column);
                            var value = property?.GetValue(data[i]);
                            if (column == "FinanceStatusID")
                            {
                                worksheet.Cell(i + 2, colIndex).Value = value switch
                                {
                                    1 => "Оплачен",
                                    2 => "Не оплачен",
                                    3 => "Частично оплачен",
                                    _ => "Неизвестно"
                                };
                            }
                            else if (column == "OrderDate" && value is DateTime date)
                            {
                                worksheet.Cell(i + 2, colIndex).Value = date.ToString("yyyy-MM-dd");
                            }
                            else if ((column == "PaidAmount" || column == "TotalAmount") && value is decimal amount)
                            {
                                worksheet.Cell(i + 2, colIndex).Value = amount.ToString("C");
                            }
                            else
                            {
                                worksheet.Cell(i + 2, colIndex).Value = value?.ToString() ?? "Не указано";
                            }
                            colIndex++;
                        }
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(filePath);
                    System.Diagnostics.Debug.WriteLine("Export completed successfully");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExportToExcel Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Error exporting to Excel: " + ex.Message, ex);
            }
        }

        public void ExportToExcelServices(List<FullService> data, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Экспорт {data.Count} записей в {filePath}");
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Services");
                    int colIndex = 1;
                    var visibleColumns = columnVisibility
                        .Where(c => c.Value)
                        .Select(c => c.Key)
                        .ToList();

                    foreach (var column in visibleColumns)
                    {
                        worksheet.Cell(1, colIndex).Value = _columnMapping.TryGetValue(column, out var header) ? header : column;
                        colIndex++;
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        colIndex = 1;
                        foreach (var column in visibleColumns)
                        {
                            var property = typeof(FullService).GetProperty(column);
                            var value = property?.GetValue(data[i]);
                            if (column == "Price" && value is decimal price)
                            {
                                worksheet.Cell(i + 2, colIndex).Value = price.ToString("C");
                            }
                            else if (column == "TotalRevenue" && value is decimal revenue)
                            {
                                worksheet.Cell(i + 2, colIndex).Value = revenue.ToString("C");
                            }
                            else
                            {
                                worksheet.Cell(i + 2, colIndex).Value = value?.ToString() ?? "Не указано";
                            }
                            colIndex++;
                        }
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(filePath);
                    System.Diagnostics.Debug.WriteLine("Экспорт успешно завершен");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка ExportToExcel: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при экспорте в Excel: " + ex.Message, ex);
            }
        }

        private string GetCustomerName(int customerId)
        {
            try
            {
                var customer = _customerRepository.GetById(customerId);
                return customer?.FullName ?? "Не указано";
            }
            catch (Exception)
            {
                return "Не указано";
            }
        }

        private string GetServiceName(int serviceId)
        {
            try
            {
                var service = _serviceRepository.GetById(serviceId);
                return service?.Name ?? "Не указано";
            }
            catch (Exception)
            {
                return "Не указано";
            }
        }

        private string GetVehicleModel(int? vehicleId)
        {
            try
            {
                var vehicle = _vehicleRepository.GetAll().ToList().FirstOrDefault(v => v.Id == vehicleId);
                return vehicle != null ? $"{vehicle.CarBrandName} {vehicle.Model}" : "Не указано";
            }
            catch (Exception)
            {
                return "Не указано";
            }
        }

        private string GetStatusName(int statusId)
        {
            try
            {
                var status = _statusRepository.GetById("OrderStatuses", statusId);
                return status?.Name ?? "Не указано";
            }
            catch (Exception)
            {
                return "Не указано";
            }
        }

        private string GetUserName(int userId)
        {
            try
            {
                var user =  _userRepository.GetUserById(userId); 
                return user?.FullName ?? "Не указано";
            }
            catch (Exception)
            {
                return "Не указано";
            }
        }
        // ServiceOrdersViewModel.cs ga qo'shing (mavjud metodlardan keyin):

        public List<Service> GetAllServices()
        {
            return _serviceRepository.GetAll();
        }

        public List<Status> GetAllOrderStatuses()
        {
            return _statusRepository.GetAll("OrderStatuses");
        }

        public Status GetOrderStatusById(int statusId)
        {
            return _statusRepository.GetById("OrderStatuses", statusId);
        }

        public List<CarModel> GetAllVehicles()
        {
            return _vehicleRepository.GetAll().ToList();
        }

        public List<CarModel> GetCarModelByCustomerId(int? customerId = null)
        {
            try
            {
                if (customerId == 0 || !customerId.HasValue)
                {
                    return _vehicleRepository.GetAll().ToList();  // Barcha mashinalar
                }
                return _vehicleRepository.GetCarModels(customerId.Value);  // Faqat mijozning mashinalari
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении моделей автомобилей: " + ex.Message, ex);
            }
        }

        // Customer metodlari (xatolik uchun qo'shildi)
        public Customer GetCustomerById(int customerId)
        {
            return _customerRepository.GetById(customerId);
        }

        public async Task<List<CustomerInfo>> SearchCustomersAsync(string searchText)
        {
            return await _customerRepository.SearchCustomersAsync(searchText);
        }

        public CustomerDebtInfoDto GetCustomerWithDebtDetailsById(int customerId)
        {
            return _customerRepository.GetCustomerWithDebtDetailsById(customerId);
        }

        public decimal GetMinimumUnitPrice(int serviceId)
        {
            var service = _serviceRepository.GetById(serviceId);
            if (service == null || service.Price == 0)
                return 0m;
            return service.Price * 0.9m;  // 90% of the service Price
        }

        private int CalculateFinanceStatusId(decimal paidAmount, int quantity, decimal unitPrice)
        {
            decimal totalAmount = quantity * unitPrice;
            if (paidAmount >= totalAmount - 0.01m && paidAmount <= totalAmount + 0.01m)
                return 1; // Оплачен
            else if (paidAmount == 0)
                return 2; // Не оплачен
            else if (paidAmount > 0 && paidAmount < totalAmount)
                return 3; // Частично Оплачен
            return 2; // Default: Не оплачен
        }

        // ValidateServiceOrder: Vehicle tekshiruvi olib tashlandi
        public List<string> ValidateServiceOrder(object serviceId, string quantityText, string unitPriceText, string paidAmountText, object statusId /*, object vehicleId olib tashlandi*/)
        {
            var errors = new List<string>();

            if (serviceId == null)
                errors.Add("Выберите услугу!");
            if (string.IsNullOrWhiteSpace(quantityText) || !int.TryParse(quantityText, out int quantity) || quantity <= 0)
                errors.Add("Введите корректное количество (положительное число)!");
            if (string.IsNullOrWhiteSpace(unitPriceText) || !decimal.TryParse(unitPriceText, out decimal unitPrice) || unitPrice <= 0)
                errors.Add("Введите корректную цену за единицу (положительное число)!");
            if (string.IsNullOrWhiteSpace(paidAmountText) || !decimal.TryParse(paidAmountText, out decimal paidAmount) || paidAmount < 0)
                errors.Add("Введите корректную оплаченную сумму (неотрицательное число)!");
            if (statusId == null)
                errors.Add("Выберите статус заказа!");
            // Vehicle tekshiruvi olib tashlandi — ixtiyoriy

            if (int.TryParse(quantityText, out quantity) && decimal.TryParse(unitPriceText, out unitPrice) && decimal.TryParse(paidAmountText, out paidAmount))
            {
                decimal expectedTotal = quantity * unitPrice;
                if (paidAmount > expectedTotal + 0.01m)
                    errors.Add("Оплаченная сумма не должна превышать количество * цену за единицу!");
            }
            if (serviceId != null && decimal.TryParse(unitPriceText, out unitPrice))
            {
                var minPrice = GetMinimumUnitPrice((int)serviceId);
                if (unitPrice < minPrice)
                    errors.Add($"Цена за единицу не может быть меньше {minPrice} (90% от цены услуги).");
            }

            return errors;
        }

        // UpdateServiceOrder: Vehicle tekshiruvi olib tashlandi
        public List<string> UpdateServiceOrder(ServiceOrder serviceOrder, decimal unitPrice)  // unitPrice ni parametrlardan oldik
        {
            var errors = new List<string>();

            if (serviceOrder.ServiceID == 0)
                errors.Add("Услуга не выбрана!");
            if (serviceOrder.Quantity <= 0)
                errors.Add("Количество должно быть положительным числом!");
            if (unitPrice <= 0)
                errors.Add("Цена за единицу должна быть положительным числом!");
            if (serviceOrder.PaidAmount < 0)
                errors.Add("Оплаченная сумма не может быть отрицательной!");
            if (serviceOrder.StatusID == 0)
                errors.Add("Статус заказа не выбран!");
            // Vehicle tekshiruvi olib tashlandi — ixtiyoriy
            if (serviceOrder.Quantity * unitPrice < serviceOrder.PaidAmount - 0.01m)
                errors.Add("Оплаченная сумма не должна превышать количество * цену за единицу!");

            var minPrice = GetMinimumUnitPrice(serviceOrder.ServiceID);
            if (unitPrice < minPrice)
            {
                unitPrice = minPrice;  // Local o'zgaruvchi
                errors.Add($"Цена за единицу не может быть меньше {minPrice}. Установлено минимальное значение.");
            }

            if (errors.Any())
                return errors;

            try
            {
                // Date avto: Hozirgi vaqt
                serviceOrder.OrderDate = DateTime.Now;

                // FinanceStatus avto-hisoblash
                serviceOrder.FinanceStatusID = CalculateFinanceStatusId(serviceOrder.PaidAmount, serviceOrder.Quantity, unitPrice);

                // TotalAmount hisoblash (entity da saqlash)
                serviceOrder.TotalAmount = serviceOrder.Quantity * unitPrice;

                // VehicleID ni null qilish, agar tanlanmagan bo'lsa (lekin form da boshqariladi)
                // Repository orqali update
                _serviceOrdersRepository.Update(serviceOrder);
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string> { $"Ошибка при сохранении: {ex.Message}" };
            }
        }

        public void ReturnService(int orderId)
        {
            try
            {
                var serviceOrder = _serviceOrdersRepository.GetById(orderId);
                if (serviceOrder == null)
                    throw new ArgumentNullException(nameof(serviceOrder));

                // Reset fields

                serviceOrder.Quantity = 0;
                serviceOrder.PaidAmount = 0m;
                serviceOrder.FinanceStatusID = 1; // Оплачен
                serviceOrder.TotalAmount = null;
                if (serviceOrder.VehicleID == 0)
                {
                    serviceOrder.VehicleID = null;

                }
                serviceOrder.OrderDate = DateTime.Now;

                // Call repository method
                _serviceOrdersRepository.ReturnService(serviceOrder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ReturnService Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при возврате сервисного заказа: " + ex.Message, ex);
            }
        }

        public void OpenServiceOrderEditForm(int orderId, Form owner)
        {
            try
            {
                var serviceOrder = _serviceOrdersRepository.GetById(orderId);
                if (serviceOrder == null)
                {
                    MessageBox.Show("Запись не найдена!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var editForm = new ServiceOrderEditForm(this, serviceOrder))
                {
                    editForm.ShowDialog(owner);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы редактирования: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
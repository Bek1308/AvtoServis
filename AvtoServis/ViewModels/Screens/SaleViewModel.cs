using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvtoServis.Forms.Modals.PartExpenses;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using AvtoServis.Data.Interfaces;
using AvtoServis.Forms.Controls;
using AvtoServis.Forms.Modals.PartExpenses;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;

namespace AvtoServis.ViewModels.Screens
{
    public class SaleViewModel : INotifyPropertyChanged
    {
        private readonly IFullPartsRepository _partsRepository;
        private readonly IPartsExpensesRepository _partsExpensesRepository;
        private readonly IPartsIncomeRepository _partsIncomeRepository;
        private readonly IStatusRepository _statusRepository;
        private FullParts _selectedProduct;
        private SaleItemDto _selectedSaleItem;
        private CustomerInfo selectedCustomer { get; set; }
        private string _paidAmount;
        private string _customerSearchText;
        private string _notificationMessage;
        private readonly string _jsonFilePath = "selected_products.json";
        private readonly string _incompleteSalesFilePath = "incomplete_sales.json";
        private bool _isProcessing;
        private int? _currentSaleId;
        private List<SaleItemDto> _selectedProducts;
        private static readonly object _fileLock = new object();

        private SaleViewModel _fullPartsControl;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<FullParts> PopularProducts { get; }
        public List<SaleItemDto> SelectedProducts
        {
            get => _selectedProducts;
            set
            {
                _selectedProducts = value ?? new List<SaleItemDto>();
                OnPropertyChanged(nameof(SelectedProducts));
            }
        }
        public Dictionary<string, bool> ColumnVisibility { get; }
        public Dictionary<string, bool> SelectedColumnVisibility { get; }
        public int? CurrentSaleId => _currentSaleId;

        public string SelectedProductInfo => _selectedSaleItem != null
            ? $"Название: **{_selectedSaleItem.ProductName ?? "Не указан"}**\n" +
              $"Цена: **{_selectedSaleItem.Price:N2}**\n" +
              $"Количество: **{_selectedSaleItem.Quantity}**\n" +
              $"Итого: **{_selectedSaleItem.Total:N2}**\n" +
              $"Производитель: **{_selectedSaleItem.ManufacturerName ?? "Не указан"}**\n" +
              $"Каталожный номер: **{_selectedSaleItem.CatalogNumber ?? "Не указан"}**\n" +
              $"Склад: **{_selectedSaleItem.StockName ?? "Не указан"}**\n" +
              $"Номер полки: **{(_partsRepository.GetPartById(_selectedSaleItem.ProductId)?.ShelfNumber ?? "Не указан")}**"
            : _selectedProduct != null
                ? $"Название: **{_selectedProduct.PartName ?? "Не указан"}**\n" +
                  $"Цена: **{((_selectedProduct.IncomeUnitPrice ?? 0) + (_selectedProduct.Markup ?? 0)):N2}**\n" +
                  $"Остаток: **{_selectedProduct.RemainingQuantity}**\n" +
                  $"Производитель: **{_selectedProduct.ManufacturerName ?? "Не указан"}**\n" +
                  $"Каталожный номер: **{_selectedProduct.CatalogNumber ?? "Не указан"}**\n" +
                  $"Склад: **{_selectedProduct.StockName ?? "Не указан"}**\n" +
                  $"Номер полки: **{_selectedProduct.ShelfNumber ?? "Не указан"}**\n" +
                  $"Марка автомобиля: **{_selectedProduct.CarBrandName ?? "Не указан"}**\n" +
                  $"Качество: **{_selectedProduct.QualityName ?? "Не указан"}**"
                : "Информация о товаре отсутствует";

        public string PaidAmount
        {
            get => _paidAmount;
            set
            {
                _paidAmount = value;
                OnPropertyChanged(nameof(PaidAmount));
            }
        }

        public string CustomerSearchText
        {
            get => _customerSearchText;
            set
            {
                _customerSearchText = value;
                OnPropertyChanged(nameof(CustomerSearchText));
            }
        }

        public string NotificationMessage
        {
            get => _notificationMessage;
            set
            {
                _notificationMessage = value;
                OnPropertyChanged(nameof(NotificationMessage));
            }
        }

        public SaleViewModel(IFullPartsRepository partsRepository, IPartsExpensesRepository partsExpensesRepository, IPartsIncomeRepository partsIncomeRepository, IStatusRepository statusRepository)
        {
            if (partsRepository == null)
            {
                throw new ArgumentNullException(nameof(partsRepository));
            }
            if (partsExpensesRepository == null)
            { 
                throw new ArgumentNullException(nameof(partsExpensesRepository));

            }
            if (partsIncomeRepository == null)
            {
                throw new ArgumentNullException(nameof(partsIncomeRepository));
            }
            if (statusRepository == null)
            {
                throw new ArgumentNullException(nameof(statusRepository));
            }

            _partsRepository = partsRepository;
            _partsExpensesRepository = partsExpensesRepository;
            _partsIncomeRepository = partsIncomeRepository;
            _statusRepository = statusRepository;

            Console.WriteLine("SaleViewModel: Starting initialization...");

            PopularProducts = new ObservableCollection<FullParts>(_partsRepository.GetTopSellingParts() ?? new List<FullParts>());
            _selectedProducts = new List<SaleItemDto>();
            ColumnVisibility = new Dictionary<string, bool>
            {
                { "PartName", true },
                { "IncomeUnitPrice", true },
                { "RemainingQuantity", true },
                { "ManufacturerName", false },
                { "CatalogNumber", false },
                { "StockName", false },
                { "ShelfNumber", true }
            };
            SelectedColumnVisibility = new Dictionary<string, bool>
            {
                { "ProductName", true },
                { "Quantity", true },
                { "Price", true },
                { "Total", true },
                { "Status", false }, // Status uchun yangi ustun
                { "PaidAmount", false },
                { "ManufacturerName", false },
                { "CatalogNumber", false },
                { "StockName", false }
            };
            _paidAmount = "0";
            _customerSearchText = "Найти покупателя...";
            _notificationMessage = "";
            _currentSaleId = null;
            CustomerInfo customerInfo = new CustomerInfo();
          
            Console.WriteLine($"SaleViewModel: Initialization completed. PopularProducts count = {PopularProducts.Count}");
        }

        public FullParts GetPartById(int productId)
        {
            try
            {
                return _partsRepository.GetPartById(productId);
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при получении товара по ID: {ex.Message}";
                return null;
            }
        }

        public List<Status> GetStatuses()
        {
            try
            {
                return _statusRepository.GetAll("ExpenseStatuses"); // "PartExpenses" jadvali uchun statuslar
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
        public void RefreshPopularProducts()
        {
            try
            {
                PopularProducts.Clear();
                var topSellingParts = _partsRepository.GetTopSellingParts() ?? new List<FullParts>();
                foreach (var part in topSellingParts)
                {
                    PopularProducts.Add(part);
                }
                OnPropertyChanged(nameof(PopularProducts));
                NotificationMessage = "Список популярных товаров обновлен";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при обновлении популярных товаров: {ex.Message}";
            }
        }

        /// <summary>
        /// Свойство изменено, уведомляет подписчиков об изменении.
        /// </summary>
        /// <param name="propertyName">Имя измененного свойства.</param>
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

        public List<SaleItemDto> LoadSelectedProductsFromJson()
        {
            lock (_fileLock)
            {
                try
                {
                    if (File.Exists(_jsonFilePath))
                    {
                        var json = File.ReadAllText(_jsonFilePath);
                        var loadedProducts = JsonSerializer.Deserialize<List<SaleItemDto>>(json);
                        var validProducts = new List<SaleItemDto>();
                        if (loadedProducts != null)
                        {
                            foreach (var product in loadedProducts)
                            {
                                if (product == null) continue;
                                var fullProduct = _partsRepository.GetPartById(product.ProductId);
                                if (fullProduct != null)
                                {
                                    decimal minPrice = (fullProduct.IncomeUnitPrice ?? 0) * (1 + (fullProduct.Markup ?? 0.1m) / 100);
                                    product.Quantity = product.Quantity <= 0 ? 1 : Math.Min(product.Quantity, fullProduct.RemainingQuantity);
                                    product.Price = Math.Round(Math.Max(product.Price, minPrice), 2);
                                    product.Total = product.Quantity * product.Price;
                                    product.ProductName = product.ProductName ?? string.Empty;
                                    product.ManufacturerName = product.ManufacturerName ?? string.Empty;
                                    product.CatalogNumber = product.CatalogNumber ?? string.Empty;
                                    product.StockName = product.StockName ?? string.Empty;
                                    validProducts.Add(product);
                                }
                            }
                        }
                        SelectedProducts = validProducts;
                        NotificationMessage = "Список выбранных товаров загружен";
                        return validProducts;
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
                return new List<SaleItemDto>();
            }
        }

        public void SaveSelectedProductsToJson()
        {
            lock (_fileLock)
            {
                try
                {
                    // Fayl yo'lini tekshirish
                    if (string.IsNullOrWhiteSpace(_jsonFilePath))
                    {
                        
                        NotificationMessage = "Фатальная ошибка: Путь к JSON файлу не указан. Установлен путь по умолчанию.";
                    }

                    // Direktoriyani yaratish
                    string directoryPath = Path.GetDirectoryName(_jsonFilePath);
                    if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                        NotificationMessage = $"Директория {directoryPath} создана.";
                    }

                    // SelectedProducts ni validatsiya qilish
                    if (SelectedProducts == null)
                    {
                        SelectedProducts = new List<SaleItemDto>();
                        NotificationMessage = "Список выбранных товаров пуст. Сохранен пустой список.";
                    }

                    // Har bir elementni tekshirish
                    var validProducts = SelectedProducts
                        .Where(p => p != null && p.ProductId > 0 && !string.IsNullOrEmpty(p.ProductName))
                        .ToList();

                    if (validProducts.Count != SelectedProducts.Count)
                    {
                        NotificationMessage = "Обнаружены некорректные элементы в списке товаров. Сохранены только валидные элементы.";
                        SelectedProducts = validProducts;
                    }

                    // JSON seriyalizatsiya
                    var jsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                    };
                    string json = JsonSerializer.Serialize(SelectedProducts, jsonOptions);

                    // Faylga yozish
                    File.WriteAllText(_jsonFilePath, json);
                    NotificationMessage = $"Список выбранных товаров успешно сохранен в {_jsonFilePath}";
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

        public void SaveIncompleteSale()
        {
            lock (_fileLock)
            {
                try
                {
                    if (SelectedProducts == null || !SelectedProducts.Any())
                    {
                        NotificationMessage = "Сохранять нечего: список товаров пуст";
                        return;
                    }

                    var incompleteSales = GetIncompleteSales() ?? new List<IncompleteSaleDto>();
                    if (_currentSaleId == null)
                    {
                        _currentSaleId = incompleteSales.Any() ? incompleteSales.Max(s => s.SaleId) + 1 : 1;
                    }

                    var sale = new IncompleteSaleDto
                    {
                        SaleId = _currentSaleId ?? 0,
                        CustomerName = selectedCustomer?.FullName ?? (_customerSearchText != "Найти покупателя..." ? _customerSearchText : null), // O'zgartirildi: FullName ishlatiladi
                        CustomerId = selectedCustomer?.CustomerID,
                        CustomerPhone = selectedCustomer?.Phone,
                        TotalAmount = SelectedProducts.Sum(x => x.Quantity * x.Price),
                        PaidAmount = decimal.TryParse(_paidAmount, out var paid) ? Math.Round(paid, 2) : 0,
                        Date = DateTime.Now,
                        Items = SelectedProducts.ToList()
                    };

                    var existingSale = incompleteSales.FirstOrDefault(s => s.SaleId == _currentSaleId);
                    if (existingSale != null)
                    {
                        incompleteSales.Remove(existingSale);
                    }
                    incompleteSales.Add(sale);

                    var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                    var json = JsonSerializer.Serialize(incompleteSales, jsonOptions);
                    File.WriteAllText(_incompleteSalesFilePath, json);
                    NotificationMessage = $"Незавершенная продажа {_currentSaleId} успешно сохранена";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Ошибка при сохранении незавершенной продажи: {ex.Message}";
                }
            }
        }

        public List<IncompleteSaleDto> GetIncompleteSales()
        {
            lock (_fileLock)
            {
                try
                {
                    if (File.Exists(_incompleteSalesFilePath))
                    {
                        var json = File.ReadAllText(_incompleteSalesFilePath);
                        var sales = JsonSerializer.Deserialize<List<IncompleteSaleDto>>(json);
                        return sales?.Where(s => s != null).ToList() ?? new List<IncompleteSaleDto>();
                    }
                }
                catch (IOException ex)
                {
                    NotificationMessage = $"Ошибка ввода-вывода при загрузке незавершенных продаж: {ex.Message}";
                }
                catch (JsonException ex)
                {
                    NotificationMessage = $"Ошибка формата JSON при загрузке незавершенных продаж: {ex.Message}";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Ошибка при загрузке незавершенных продаж: {ex.Message}";
                }
                return new List<IncompleteSaleDto>();
            }
        }

        public void LoadIncompleteSale(int saleId)
        {
            lock (_fileLock)
            {
                try
                {
                    var incompleteSales = GetIncompleteSales() ?? new List<IncompleteSaleDto>();
                    var sale = incompleteSales.FirstOrDefault(s => s.SaleId == saleId);
                    if (sale == null)
                    {
                        NotificationMessage = $"Продажа с ID {saleId} не найдена";
                        return;
                    }

                    _currentSaleId = sale.SaleId;
                    SelectedProducts = sale.Items ?? new List<SaleItemDto>();
                    PaidAmount = sale.PaidAmount.ToString("N2");
                    CustomerSearchText = sale.CustomerName ?? "Найти покупателя...";
                    selectedCustomer = new CustomerInfo
                    {
                        CustomerID = sale.CustomerId ?? 0,
                        FullName = sale.CustomerName, // O'zgartirildi: FullName to'g'ri o'rnatiladi
                        Phone = sale.CustomerPhone
                    };
                    OnPropertyChanged(nameof(SelectedProducts));
                    OnPropertyChanged(nameof(SelectedProductInfo));
                    OnPropertyChanged(nameof(CustomerSearchText)); // Qo'shildi: UI yangilanishi uchun
                    NotificationMessage = $"Продажа {saleId} успешно загружена";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Ошибка при загрузке незавершенной продажи: {ex.Message}";
                }
            }
        }

        public void DeleteIncompleteSale(int saleId)
        {
            lock (_fileLock)
            {
                try
                {
                    var incompleteSales = GetIncompleteSales() ?? new List<IncompleteSaleDto>();
                    var sale = incompleteSales.FirstOrDefault(s => s.SaleId == saleId);
                    if (sale != null)
                    {
                        incompleteSales.Remove(sale);
                        File.WriteAllText(_incompleteSalesFilePath, JsonSerializer.Serialize(incompleteSales, new JsonSerializerOptions { WriteIndented = true }));
                        if (_currentSaleId == saleId)
                        {
                            _currentSaleId = null;
                            SelectedProducts = new List<SaleItemDto>();
                        }
                        NotificationMessage = $"Незавершенная продажа {saleId} удалена";
                    }
                    else
                    {
                        NotificationMessage = $"Продажа с ID {saleId} не найдена";
                    }
                }
                catch (IOException ex)
                {
                    NotificationMessage = $"Ошибка ввода-вывода при удалении незавершенной продажи: {ex.Message}";
                }
                catch (JsonException ex)
                {
                    NotificationMessage = $"Ошибка формата JSON при удалении незавершенной продажи: {ex.Message}";
                }
                catch (Exception ex)
                {
                    NotificationMessage = $"Ошибка при удалении незавершенной продажи: {ex.Message}";
                }
            }
        }

        public void DeleteCurrentIncompleteSale()
        {
            if (_isProcessing || !_currentSaleId.HasValue) return;
            _isProcessing = true;
            try
            {
                DeleteIncompleteSale(_currentSaleId.Value);
                AddNewProduct();
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при удалении текущей незавершенной продажи: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
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
                        { "PartName", "Название товара" },
                        { "IncomeUnitPrice", "Цена" },
                        { "RemainingQuantity", "Остаток на складе" },
                        { "ManufacturerName", "Производитель" },
                        { "CatalogNumber", "Каталожный номер" },
                        { "StockName", "Название склада" },
                        { "ShelfNumber", "Номер полки" }
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
                        if (checkedListBox.CheckedItems.Count > 6)
                        {
                            NotificationMessage = "Можно выбрать максимум 6 столбцов";
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
                        NotificationMessage = "Столбцы для популярных товаров обновлены";
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
                    Text = "Выбор столбцов для выбранных товаров",
                    Size = new Size(300, 320), // O'lcham biroz kattalashtirildi, yangi ustunlar uchun
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
                        Size = new Size(260, 220), // O'lcham yangi elementlar uchun moslashtirildi
                        CheckOnClick = true,
                        Font = new Font("Segoe UI", 10F),
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    var columnMapping = new Dictionary<string, string>
            {
                { "ProductName", "Название товара" },
                { "Quantity", "Количество" },
                { "Price", "Цена" },
                { "Total", "Итого" },
                { "ManufacturerName", "Производитель" },
                { "CatalogNumber", "Каталожный номер" },
                { "StockName", "Название склада" },
                { "Status", "Статус" }, // Yangi Status ustuni qo'shildi
                { "PaidAmount", "Оплаченная сумма" } // Yangi PaidAmount ustuni qo'shildi
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
                        Location = new Point(100, 240), // Tugma joylashuvi yangi o'lchamga moslashtirildi
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
                        if (checkedListBox.CheckedItems.Count > 8) // Maksimal ustunlar soni 8 ga oshirildi
                        {
                            NotificationMessage = "Можно выбрать максимум 8 столбцов";
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
                        NotificationMessage = "Столбцы для выбранных товаров обновлены";
                        form.Close();
                    };

                    form.Controls.Add(checkedListBox);
                    form.Controls.Add(btnOk);
                    form.ShowDialog(parent);
                }
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при открытии диалога выбора столбцов для выбранных товаров: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void SelectProduct(int index)
        {
            if (_isProcessing || index < 0 || index >= PopularProducts?.Count)
            {
                _selectedProduct = null;
                OnPropertyChanged(nameof(SelectedProductInfo));
                return;
            }
            _isProcessing = true;
            try
            {
                _selectedProduct = PopularProducts[index];
                _selectedSaleItem = null;
                OnPropertyChanged(nameof(SelectedProductInfo));
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при выборе товара: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void SelectSelectedProduct(int index)
        {
            if (_isProcessing || index < 0 || index >= SelectedProducts?.Count)
            {
                _selectedSaleItem = null;
                OnPropertyChanged(nameof(SelectedProductInfo));
                return;
            }
            _isProcessing = true;
            try
            {
                _selectedSaleItem = SelectedProducts[index];
                _selectedProduct = null;
                OnPropertyChanged(nameof(SelectedProductInfo));
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при выборе товара из списка: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void AddProductToSale(FullParts product)
        {
            if (_isProcessing || product == null) return;
            _isProcessing = true;
            try
            {
                if (product.RemainingQuantity == 0)
                {
                    NotificationMessage = "Товар отсутствует на складе";
                    return;
                }

                var existingItem = SelectedProducts.FirstOrDefault(x => x.ProductId == product.PartID);
                if (existingItem != null)
                {
                    if (existingItem.Quantity + 1 <= product.RemainingQuantity)
                    {
                        existingItem.Quantity++;
                        existingItem.Total = existingItem.Quantity * existingItem.Price;
                    }
                    else
                    {
                        NotificationMessage = $"Количество не может превышать остаток на складе ({product.RemainingQuantity})";
                        existingItem.Quantity = product.RemainingQuantity;
                        existingItem.Total = existingItem.Quantity * existingItem.Price;
                        return;
                    }
                }
                else
                {
                    var price = Math.Round((product.IncomeUnitPrice ?? 0) * (1 + (product.Markup ?? 0.1m) / 100), 2);
                    SelectedProducts.Add(new SaleItemDto
                    {
                        ProductId = product.PartID,
                        IncomeId = product.IncomeID,
                        ProductName = product.PartName ?? string.Empty,
                        Quantity = 1,
                        Price = price,
                        Total = price,
                        ManufacturerName = product.ManufacturerName ?? string.Empty,
                        CatalogNumber = product.CatalogNumber ?? string.Empty,
                        StockName = product.StockName ?? string.Empty
                    });
                }
                NotificationMessage = $"Товар '{product.PartName ?? "Не указан"}' добавлен в продажу";
                OnPropertyChanged(nameof(SelectedProducts));
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при добавлении товара в продажу: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void AddProductToSelection(int index)
        {
            if (_isProcessing || index < 0 || index >= PopularProducts?.Count) return;
            _isProcessing = true;
            try
            {
                var product = PopularProducts[index];
                AddProductToSale(product);
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при добавлении товара в выбор: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void RemoveSelectedProduct(int index)
        {
            if (_isProcessing || index < 0 || index >= SelectedProducts?.Count) return;
            _isProcessing = true;
            try
            {
                var product = SelectedProducts[index];
                SelectedProducts.RemoveAt(index);
                _selectedSaleItem = null;
                _selectedProduct = null;
                OnPropertyChanged(nameof(SelectedProducts));
                OnPropertyChanged(nameof(SelectedProductInfo));
                NotificationMessage = $"Товар '{product?.ProductName ?? "Не указан"}' удален из продажи";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при удалении товара из продажи: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void UpdateProductQuantity(int index, int quantity)
        {
            if (_isProcessing || index < 0 || index >= SelectedProducts?.Count) return;
            _isProcessing = true;
            try
            {
                //int lastproductquantity=1;
                var product = SelectedProducts[index];
                var fullProduct = _partsRepository.GetPartById(product.ProductId);
                if (fullProduct == null)
                {
                    NotificationMessage = "Товар не найден в базе данных";
                    product.Quantity = 1;
                    product.Total = product.Quantity * product.Price;
                }
                else
                {
                    product.Quantity = Math.Min(quantity, fullProduct.RemainingQuantity);
                    product.Total = product.Quantity * product.Price;
                }
                //if (lastproductquantity != product.Quantity) 
                //{
                //OnPropertyChanged(nameof(SelectedProducts));
                //}
                //lastproductquantity = product.Quantity;
                
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при обновлении количества товара: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void UpdateProductPrice(int index, decimal price)
        {
            if (_isProcessing || index < 0 || index >= SelectedProducts?.Count) return;
            _isProcessing = true;
            try
            {
                //decimal lastproductprice=1;
                var product = SelectedProducts[index];
                var fullProduct = _partsRepository.GetPartById(product.ProductId);
                if (fullProduct == null)
                {
                    NotificationMessage = "Товар не найден в базе данных";
                    product.Price = product.Price;
                    product.Total = product.Quantity * product.Price;
                }
                else
                {
                    decimal minPrice = (fullProduct.IncomeUnitPrice ?? 0) * (1 + (fullProduct.Markup ?? 0.1m) / 100);
                    product.Price = Math.Round(Math.Max(price, minPrice), 2);
                    product.Total = product.Quantity * product.Price;
                }
                //if (lastproductprice != product.Price)
                //{
                //    OnPropertyChanged(nameof(SelectedProducts));
                //}
                //lastproductprice = product.Price;
                
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при обновлении цены товара: {ex.Message}";
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


        public IFullPartsRepository PartsRepository => _partsRepository; // IFullPartsRepository ga kirish uchun xususiyat

        public void OpenSearchDialog()
        {
           
            try
            {
                var fullPartsViewModel = new FullPartsViewModel(_partsRepository);
                var imageList = new ImageList();
                var dialog = new AvtoServis.Forms.Modals.PartExpenses.SearchProductDialog(fullPartsViewModel, this, imageList);
                dialog.ShowDialog();
                NotificationMessage = "Диалог поиска товаров успешно открыт";
            }
            catch (ArgumentNullException ex)
            {
                NotificationMessage = $"Ошибка инициализации диалога поиска: {ex.Message}";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при открытии диалога поиска: {ex.Message}";
            }
         
        }

        public void AddProductFromSearch(FullParts product)
        {
           
            try
            {
                AddProductToSale(product); 
                NotificationMessage = $"Товар '{product.PartName ?? "Не указан"}' добавлен из поиска";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при добавлении товара из поиска: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void AddNewProduct()
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                // Joriy savdoni saqlash
                if (SelectedProducts != null && SelectedProducts.Any())
                {
                    SaveIncompleteSale();
                    NotificationMessage = $"Текущая продажа {_currentSaleId ?? 0} сохранена перед началом новой";
                }

                // Yangi savdoni boshlash
                SelectedProducts = new List<SaleItemDto>();
                PaidAmount = "0";
                CustomerSearchText = "Найти покупателя...";
                _selectedSaleItem = null;
                _selectedProduct = null;
                _currentSaleId = null;
                OnPropertyChanged(nameof(SelectedProducts));
                OnPropertyChanged(nameof(SelectedProductInfo));
                NotificationMessage = "Новая продажа начата";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при создании новой продажи: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public void ContinueSale()
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                // Joriy savdoni ma'lumotlar bazasiga saqlash
                if (SelectedProducts != null && SelectedProducts.Any())
                {
                    var sale = new SaleDto
                    {
                        SaleId = _currentSaleId ?? 0,
                        Items = SelectedProducts.ToList(),
                        PaidAmount = decimal.TryParse(_paidAmount, out var paid) ? Math.Round(paid, 2) : 0,
                        CustomerName = _customerSearchText != "Найти покупателя..." ? _customerSearchText : null,
                        TotalAmount = SelectedProducts.Sum(x => x.Quantity * x.Price),
                        Date = DateTime.Now
                    };

                    // PartExpense obyektlarini yaratish va saqlash
                    var partExpenses = new List<PartExpense>();
                    // Calculate equal PaidAmount per item
                    decimal distributedPaidAmount = sale.Items.Any() ? sale.PaidAmount / sale.Items.Count : 0;

                    foreach (var item in sale.Items)
                    {
                        if (item == null) continue;
                        var partsIncome = _partsIncomeRepository.GetByIncomeId(item.IncomeId);
                        if (partsIncome == null)
                        {
                            NotificationMessage = $"Товар с ID {item.ProductId} не найден в приходах";
                            return;
                        }

                        // No need to adjust item.Price or item.Quantity
                        // item.Total is assumed to be already calculated correctly in SelectedProducts
                        var partExpense = new PartExpense
                        {
                            PartID = item.ProductId,
                            IncomeID = partsIncome.IncomeID,
                            ExpenseTypeID = 1, // Savdo turi
                            CustomerID = selectedCustomer.CustomerID,
                            Date = sale.Date,
                            Quantity = item.Quantity,
                            UnitPrice = item.Price,
                            StatusID = 2, // Faol status
                            SuplierID = partsIncome.SupplierID,
                            OperationID = null, // OperationID dastlab null sifatida o'rnatiladi
                            InvoiceNumber = $"INV-{sale.SaleId}-{DateTime.Now.Ticks}",
                            UserID = GetCurrentUserId(),
                            PaidAmount = Math.Round(distributedPaidAmount, 2), // Distribute PaidAmount equally
                            Finance_statusId = item.PaidAmount >= item.Price ? 2 : 1
                        };
                        partExpenses.Add(partExpense);
                    }

                    // Ma'lumotlar bazasiga saqlash va OperationID ni olish
                    _partsExpensesRepository.SavePartsExpenses(partExpenses);

                    NotificationMessage = $"Продажа успешно сохранена в базе данных";

                    // Tugallanmagan savdolarni tekshirish
                    var incompleteSales = GetIncompleteSales() ?? new List<IncompleteSaleDto>();
                    var nextSale = incompleteSales.FirstOrDefault(s => s.SaleId != _currentSaleId);
                    if (nextSale != null)
                    {
                        LoadIncompleteSale(nextSale.SaleId);
                        NotificationMessage = $"Загружена незавершенная продажа {nextSale.SaleId}";
                    }
                    else
                    {
                        SelectedProducts = new List<SaleItemDto>();
                        PaidAmount = "0";
                        CustomerSearchText = "Найти покупателя...";
                        _currentSaleId = null;
                        OnPropertyChanged(nameof(SelectedProducts));
                        OnPropertyChanged(nameof(SelectedProductInfo));
                        NotificationMessage = "Нет незавершенных продаж. Список очищен.";
                    }
                }
                else
                {
                    NotificationMessage = "Нет товаров для сохранения продажи";
                }
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при продолжении продажи: {ex.Message}";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        // Yordamchi metodlar


        private int? GetCurrentUserId()
        {
            // TODO: Joriy foydalanuvchi ID sini qaytarish logikasi
            return null; // Vaqtinchalik konstanta
        }

        public void CancelAndClose()
        {
            if (_isProcessing) return;
            _isProcessing = true;
            try
            {
                SaveIncompleteSale();
                SaveSelectedProductsToJson();
                SelectedProducts = new List<SaleItemDto>();
                PaidAmount = "0";
                CustomerSearchText = "Найти покупателя...";
                _selectedProduct = null;
                _selectedSaleItem = null;
                _currentSaleId = null;
                OnPropertyChanged(nameof(SelectedProducts));
                OnPropertyChanged(nameof(SelectedProductInfo));
                NotificationMessage = "Продажа отменена и форма закрыта";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при отмене и закрытии продажи: {ex.Message}";
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
                if (CustomerSearchText == "Найти покупателя...")
                {
                    CustomerSearchText = "";
                    NotificationMessage = "Поле поиска покупателя очищено";
                }
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Ошибка при очистке поля поиска покупателя: {ex.Message}";
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
                    CustomerSearchText = "Найти покупателя...";
                    NotificationMessage = "Установлен текст-заполнитель для поиска покупателя";
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
    }
}
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvtoServis.ViewModels.Screens
{
    public class PartExpensesViewModel
    {
        private readonly IPartsExpensesRepository _repository;
        private readonly IPartsRepository _partsRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IPartsIncomeRepository _partsIncomeRepository;
        private List<PartExpenseDto> _partExpenses;
        internal string? ConnectionString;

        public PartExpensesViewModel(
            IPartsExpensesRepository repository,
            IPartsRepository partsRepository,
            ICustomerRepository customerRepository,
            IStatusRepository statusRepository,
            IPartsIncomeRepository partsIncomeRepository,
            string connectionString)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _partsRepository = partsRepository ?? throw new ArgumentNullException(nameof(partsRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _statusRepository = statusRepository ?? throw new ArgumentNullException(nameof(statusRepository));
            _partsIncomeRepository = partsIncomeRepository ?? throw new ArgumentNullException(nameof(partsIncomeRepository));
            this.ConnectionString = connectionString;
            _partExpenses = new List<PartExpenseDto>();
            VisibleColumns = new List<string>();
            Filters = new List<(string Column, string Operator, string SearchText)>();
        }

        public List<string> VisibleColumns { get; set; }
        public List<(string Column, string Operator, string SearchText)> Filters { get; set; }

        public List<PartExpenseDto> LoadPartExpenses()
        {
            try
            {
                _partExpenses = _repository.GetAllPartExpenses();
                foreach (var expense in _partExpenses)
                {
                    expense.PaymentStatusId = CalculatePaymentStatusId(expense.PaidAmount, expense.Quantity, expense.UnitPrice);
                }
                if (Filters != null && Filters.Any())
                {
                    _partExpenses = ApplyFilters(_partExpenses);
                }
                return _partExpenses;
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading part expenses: " + ex.Message, ex);
            }
        }

        private int CalculatePaymentStatusId(decimal paidAmount, int quantity, decimal unitPrice)
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

        private List<PartExpenseDto> ApplyFilters(List<PartExpenseDto> data)
        {
            var result = data.AsEnumerable();
            foreach (var filter in Filters)
            {
                var property = typeof(PartExpenseDto).GetProperty(filter.Column);
                if (property == null)
                    continue;

                result = result.Where(item =>
                {
                    var value = property.GetValue(item)?.ToString();
                    if (string.IsNullOrEmpty(value))
                        return false;

                    switch (filter.Column)
                    {
                        case "PaymentStatusId":
                            var statusText = filter.SearchText switch
                            {
                                "Оплачен" => "1",
                                "Не оплачен" => "2",
                                "Частично Оплачен" => "3",
                                _ => null
                            };
                            if (statusText == null)
                                return false;
                            var itemStatus = item.PaymentStatusId?.ToString();
                            return filter.Operator switch
                            {
                                "=" => itemStatus == statusText,
                                "!=" => itemStatus != statusText,
                                _ => false
                            };
                        case "SaleDate":
                            if (!DateTime.TryParse(filter.SearchText, out var filterDate) || !DateTime.TryParse(value, out var itemDate))
                                return false;
                            return filter.Operator switch
                            {
                                "=" => itemDate.Date == filterDate.Date,
                                ">" => itemDate.Date > filterDate.Date,
                                "<" => itemDate.Date < filterDate.Date,
                                ">=" => itemDate.Date >= filterDate.Date,
                                "<=" => itemDate.Date <= filterDate.Date,
                                _ => false
                            };
                        case "Quantity":
                        case "UnitPrice":
                        case "TotalAmount":
                        case "SaleId":
                            if (!decimal.TryParse(filter.SearchText, out var filterValue) || !decimal.TryParse(value, out var itemValue))
                                return false;
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
                        default:
                            return filter.Operator == "LIKE" && value.ToLower().Contains(filter.SearchText.ToLower());
                    }
                });
            }
            return result.ToList();
        }

        public List<PartExpenseDto> SearchPartExpenses(string searchText, List<string> visibleColumns)
        {
            try
            {
                var data = LoadPartExpenses();
                if (string.IsNullOrWhiteSpace(searchText))
                    return data;

                return data.Where(item =>
                {
                    foreach (var column in visibleColumns)
                    {
                        var property = typeof(PartExpenseDto).GetProperty(column);
                        if (property == null)
                            continue;

                        var value = property.GetValue(item)?.ToString();
                        if (string.IsNullOrEmpty(value))
                            continue;

                        if (column == "PaymentStatusId")
                        {
                            var statusText = item.PaymentStatusId switch
                            {
                                1 => "Оплачен",
                                2 => "Не оплачен",
                                3 => "Частично Оплачен",
                                _ => "Неизвестно"
                            };
                            if (statusText.ToLower().Contains(searchText.ToLower()))
                                return true;
                        }
                        else if (value.ToLower().Contains(searchText.ToLower()))
                        {
                            return true;
                        }
                    }
                    return false;
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching part expenses: " + ex.Message, ex);
            }
        }

        public void ExportToExcel(List<PartExpenseDto> data, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PartExpenses");
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "SaleId", "ID" },
                        { "PartName", "Название детали" },
                        { "Quantity", "Количество" },
                        { "UnitPrice", "Цена за единицу" },
                        { "TotalAmount", "Общая сумма" },
                        { "PaymentStatusId", "Финансовый статус" },
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

                    int colIndex = 1;
                    var visibleColumns = columnVisibility.Where(c => c.Value && c.Key != "Action").Select(c => c.Key).ToList();
                    foreach (var column in visibleColumns)
                    {
                        worksheet.Cell(1, colIndex).Value = columnMapping[column];
                        colIndex++;
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        colIndex = 1;
                        foreach (var column in visibleColumns)
                        {
                            var property = typeof(PartExpenseDto).GetProperty(column);
                            var value = property?.GetValue(data[i]);
                            if (column == "PaymentStatusId")
                            {
                                worksheet.Cell(i + 2, colIndex).Value = value switch
                                {
                                    1 => "Оплачен",
                                    2 => "Не оплачен",
                                    3 => "Частично Оплачен",
                                    _ => "Неизвестно"
                                };
                            }
                            else if (column == "SaleDate" && value is DateTime date)
                            {
                                worksheet.Cell(i + 2, colIndex).Value = date.ToString("yyyy-MM-dd");
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
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error exporting to Excel: " + ex.Message, ex);
            }
        }

        public Part GetPartById(int partId)
        {
            return _partsRepository.GetById(partId);
        }

        public Customer GetCustomerById(int customerId)
        {
            return _customerRepository.GetById(customerId);
        }

        public CustomerDebtInfoDto GetCustomerWithDebtDetailsById(int customerId)
        {
            return _customerRepository.GetCustomerWithDebtDetailsById(customerId);
        }

        public async Task<List<CustomerInfo>> SearchCustomersAsync(string searchText)
        {
            return await _customerRepository.SearchCustomersAsync(searchText);
        }

        public List<Part> GetAllParts()
        {
            return _partsRepository.GetAll();
        }

        public List<Status> GetAllStatuses()
        {
            return _statusRepository.GetAll("ExpenseStatuses");
        }

        public Status GetStatusById(int statusId)
        {
            return _statusRepository.GetById("ExpenseStatuses", statusId);
        }

        public decimal GetMinimumUnitPrice(int partId, int? incomeId)
        {
            if (!incomeId.HasValue)
                return 0m;

            var income = _partsIncomeRepository.GetById(incomeId.Value);
            if (income == null || income.UnitPrice == 0)
                return 0m;

            return income.UnitPrice * 0.9m; // 90% of the income UnitPrice
        }

        public List<string> ValidatePartExpense(object partId, string quantityText, string unitPriceText, string paidAmountText, object statusId, int? incomeId)
        {
            var errors = new List<string>();

            if (partId == null)
                errors.Add("Выберите деталь!");
            if (string.IsNullOrWhiteSpace(quantityText) || !int.TryParse(quantityText, out int quantity) || quantity <= 0)
                errors.Add("Введите корректное количество (положительное число)!");
            if (string.IsNullOrWhiteSpace(unitPriceText) || !decimal.TryParse(unitPriceText, out decimal unitPrice) || unitPrice <= 0)
                errors.Add("Введите корректную цену за единицу (положительное число)!");
            if (string.IsNullOrWhiteSpace(paidAmountText) || !decimal.TryParse(paidAmountText, out decimal paidAmount) || paidAmount < 0)
                errors.Add("Введите корректную оплаченную сумму (неотрицательное число)!");
            if (statusId == null)
                errors.Add("Выберите статус!");
            if (int.TryParse(quantityText, out quantity) && decimal.TryParse(unitPriceText, out unitPrice) && decimal.TryParse(paidAmountText, out paidAmount))
            {
                decimal expectedTotal = quantity * unitPrice;
                if (paidAmount > expectedTotal + 0.01m)
                    errors.Add("Оплаченная сумма не должна превышать количество * цену за единицу!");
            }
            if (partId != null && decimal.TryParse(unitPriceText, out unitPrice) && incomeId.HasValue)
            {
                var minPrice = GetMinimumUnitPrice((int)partId, incomeId);
                if (unitPrice < minPrice)
                    errors.Add($"Цена за единицу не может быть меньше {minPrice} (90% от цены поступления).");
            }

            return errors;
        }

        public List<string> UpdatePartExpense(PartExpense partExpense)
        {
            var errors = new List<string>();

            if (partExpense.PartID == 0)
                errors.Add("Деталь не выбрана!");
            if (partExpense.Quantity <= 0)
                errors.Add("Количество должно быть положительным числом!");
            if (partExpense.UnitPrice <= 0)
                errors.Add("Цена за единицу должна быть положительным числом!");
            if (partExpense.PaidAmount < 0)
                errors.Add("Оплаченная сумма не может быть отрицательной!");
            if (partExpense.Finance_statusId == 0)
                errors.Add("Статус не выбран!");
            if (partExpense.Quantity * partExpense.UnitPrice < partExpense.PaidAmount - 0.01m)
                errors.Add("Оплаченная сумма не должна превышать количество * цену за единицу!");
            if (partExpense.IncomeID.HasValue)
            {
                var minPrice = GetMinimumUnitPrice(partExpense.PartID, partExpense.IncomeID);
                if (partExpense.UnitPrice < minPrice)
                {
                    partExpense.UnitPrice = minPrice;
                    errors.Add($"Цена за единицу не может быть меньше {minPrice}. Установлено минимальное значение.");
                }
            }

            if (errors.Any())
                return errors;

            try
            {
                string batchName = $"Batch_{DateTime.Now:yyyyMMddHHmmss}";
                _repository.Update(partExpense, batchName);
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string> { $"Ошибка при сохранении: {ex.Message}" };
            }
        }
        public void ReturnPartExpense(int expenseId)
        {
            try
            {
                var expense = _repository.GetById(expenseId);
                if (expense == null)
                    throw new ArgumentNullException(nameof(expense));

                // Reset fields
                expense.IncomeID = null;
                expense.ExpenseTypeID = 2;
                expense.Quantity = 0;
                expense.UnitPrice = 0m;
                expense.PaidAmount = 0m;
                expense.Finance_statusId = 1; // Оплачен
                expense.InvoiceNumber = null;
                expense.Date = DateTime.Now;

                // Call repository method
                _repository.ReturnPartExpense(expense);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ReturnPartExpense Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при возврате расхода запчасти: " + ex.Message, ex);
            }
        }
        public void OpenPartExpenseEditForm(int saleId, Form owner)
        {
            try
            {
                var partExpense = _repository.GetById(saleId);
                if (partExpense == null)
                {
                    MessageBox.Show("Запись не найдена!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var editForm = new PartExpenseEditForm(this, partExpense))
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
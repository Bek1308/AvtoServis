using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using AvtoServis.Forms.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvtoServis.ViewModels.Screens
{
    public class CustomerDebtsViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        private List<CustomerDebtInfoDto> _customerDebts;

        public CustomerDebtsViewModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _customerDebts = new List<CustomerDebtInfoDto>();
            VisibleColumns = new List<string>();
            Filters = new List<(string Column, string Operator, string SearchText)>();
        }

        public List<string> VisibleColumns { get; set; }
        public List<(string Column, string Operator, string SearchText)> Filters { get; set; }

        public async Task<List<CustomerDebtInfoDto>> LoadCustomerDebtsAsync()
        {
            try
            {
                _customerDebts = await _customerRepository.GetAllCustomersWithDebtDetailsAsync();
                if (Filters != null && Filters.Any())
                {
                    _customerDebts = ApplyFilters(_customerDebts);
                }
                return _customerDebts;
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading customer debts: " + ex.Message, ex);
            }
        }

        private List<CustomerDebtInfoDto> ApplyFilters(List<CustomerDebtInfoDto> data)
        {
            var result = data.AsEnumerable();
            foreach (var filter in Filters)
            {
                var property = typeof(CustomerDebtInfoDto).GetProperty(filter.Column);
                if (property == null)
                    continue;

                result = result.Where(item =>
                {
                    var value = property.GetValue(item)?.ToString();
                    if (string.IsNullOrEmpty(value))
                        return false;

                    switch (filter.Column)
                    {
                        case "IsActive":
                            var statusText = filter.SearchText switch
                            {
                                "Да" => "True",
                                "Нет" => "False",
                                _ => null
                            };
                            if (statusText == null)
                                return false;
                            var itemStatus = item.IsActive.ToString();
                            return filter.Operator switch
                            {
                                "=" => itemStatus == statusText,
                                "!=" => itemStatus != statusText,
                                _ => false
                            };
                        case "RegistrationDate":
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
                        case "CustomerID":
                        case "UmumiyQarz":
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

        public async Task<List<CustomerDebtInfoDto>> SearchCustomerDebtsAsync(string searchText, List<string> visibleColumns)
        {
            try
            {
                var data = await LoadCustomerDebtsAsync();
                if (string.IsNullOrWhiteSpace(searchText))
                    return data;

                return data.Where(item =>
                {
                    foreach (var column in visibleColumns)
                    {
                        var property = typeof(CustomerDebtInfoDto).GetProperty(column);
                        if (property == null) continue;
                        var value = property.GetValue(item)?.ToString();
                        if (column == "CarModels")
                            value = string.Join(",", item.CarModels);
                        else if (column == "DebtDetails")
                            value = string.Join(",", item.DebtDetails.Select(d => $"{d.ItemName}: {d.Amount}"));
                        if (!string.IsNullOrEmpty(value) && value.ToLower().Contains(searchText.ToLower()))
                            return true;
                    }
                    return false;
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching customer debts: " + ex.Message, ex);
            }
        }

        public void ExportToExcel(string filePath, List<CustomerDebtInfoDto> data)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("CustomerDebts");
                    var headers = new Dictionary<string, string>
                    {
                        { "CustomerID", "ID" },
                        { "FullName", "ФИО" },
                        { "Phone", "Телефон" },
                        { "Email", "Эл. почта" },
                        { "Address", "Адрес" },
                        { "RegistrationDate", "Дата регистрации" },
                        { "IsActive", "Активен" },
                        { "UmumiyQarz", "Общий долг" },
                        { "CarModels", "Модели машин" },
                        { "DebtDetails", "Детали долга" }
                    };

                    int colIndex = 1;
                    foreach (var column in headers)
                    {
                        if (VisibleColumns.Contains(column.Key))
                        {
                            worksheet.Cell(1, colIndex).Value = column.Value;
                            colIndex++;
                        }
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        colIndex = 1;
                        var customer = data[i];
                        foreach (var column in VisibleColumns)
                        {
                            var value = typeof(CustomerDebtInfoDto).GetProperty(column)?.GetValue(customer)?.ToString();
                            if (column == "RegistrationDate" && DateTime.TryParse(value, out var date))
                                value = date.ToString("yyyy-MM-dd");
                            else if (column == "IsActive")
                                value = customer.IsActive ? "Да" : "Нет";
                            else if (column == "CarModels")
                                value = string.Join(",", customer.CarModels);
                            else if (column == "DebtDetails")
                                value = string.Join(",", customer.DebtDetails.Select(d => $"{d.ItemName}: {d.Amount}"));
                            worksheet.Cell(i + 2, colIndex).Value = value ?? "Не указано";
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

        public List<string> ValidateCustomer(Customer customer)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(customer.FullName))
                errors.Add("ФИО обязательно для заполнения!");
            if (string.IsNullOrWhiteSpace(customer.Phone))
                errors.Add("Телефон обязателен для заполнения!");
            if (customer.RegistrationDate == DateTime.MinValue)
                errors.Add("Дата регистрации некорректна!");

            return errors;
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                _customerRepository.Add(customer);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при добавлении клиента: " + ex.Message, ex);
            }
        }

        public List<string> UpdateCustomer(Customer customer)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(customer.FullName))
                errors.Add("ФИО обязательно для заполнения!");
            if (string.IsNullOrWhiteSpace(customer.Phone))
                errors.Add("Телефон обязателен для заполнения!");
            if (customer.RegistrationDate == DateTime.MinValue)
                errors.Add("Дата регистрации некорректна!");

            if (errors.Any())
                return errors;

            try
            {
                _customerRepository.Update(customer);
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string> { $"Ошибка при сохранении: {ex.Message}" };
            }
        }

        public void OpenCustomerEditForm(int customerId, Form owner)
        {
            try
            {
                var customer = _customerRepository.GetById(customerId);
                if (customer == null)
                {
                    MessageBox.Show("Клиент не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var editForm = new CustomerEditForm(this, customer))
                {
                    editForm.ShowDialog(owner);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы редактирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowCustomerDetails(int customerId, Form owner)
        {
            try
            {
                var customer = _customerRepository.GetCustomerWithDebtDetailsById(customerId);
                if (customer == null)
                {
                    MessageBox.Show("Клиент не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var detailsForm = new CustomerDetailsForm(customer))
                {
                    detailsForm.ShowDialog(owner);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии деталей клиента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task<CustomerDebtInfoDto> GetCustomerWithDebtDetailsAsync(int customerId)
        {
            try
            {
                var customer = _customerRepository.GetCustomerWithDebtDetailsById(customerId);
                return customer;

            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customer debt details: " + ex.Message, ex);
            }
        }
    }
}
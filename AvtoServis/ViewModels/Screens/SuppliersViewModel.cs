using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AvtoServis.ViewModels.Screens
{
    public class SuppliersViewModel
    {
        private readonly ISuppliersRepository _suppliersRepository;

        public SuppliersViewModel(ISuppliersRepository suppliersRepository)
        {
            _suppliersRepository = suppliersRepository ?? throw new ArgumentNullException(nameof(suppliersRepository));
        }

        public List<Supplier> LoadSuppliers()
        {
            try
            {
                var suppliers = _suppliersRepository.GetAll();
                System.Diagnostics.Debug.WriteLine($"LoadSuppliers: Загружено {suppliers.Count} поставщиков.");
                return suppliers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadSuppliers Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при загрузке поставщиков: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Supplier>();
            }
        }

        public void AddSupplier(Supplier supplier)
        {
            try
            {
                ValidateSupplier(supplier);
                _suppliersRepository.Add(supplier);
                System.Diagnostics.Debug.WriteLine($"AddSupplier: Добавлен поставщик '{supplier.Name}' с ID {supplier.SupplierID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddSupplier Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при добавлении поставщика: {ex.Message}", ex);
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            try
            {
                ValidateSupplier(supplier);
                _suppliersRepository.Update(supplier);
                System.Diagnostics.Debug.WriteLine($"UpdateSupplier: Обновлен поставщик '{supplier.Name}' с ID {supplier.SupplierID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateSupplier Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при обновлении поставщика: {ex.Message}", ex);
            }
        }

        public void DeleteSupplier(int id)
        {
            try
            {
                var supplier = _suppliersRepository.GetById(id);
                if (supplier == null)
                    throw new Exception($"Поставщик с ID {id} не найден.");

                _suppliersRepository.Delete(id);
                System.Diagnostics.Debug.WriteLine($"DeleteSupplier: Удален поставщик с ID {id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteSupplier Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при удалении поставщика: {ex.Message}", ex);
            }
        }

        public List<Supplier> SearchSuppliers(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return LoadSuppliers();

                var suppliers = _suppliersRepository.Search(searchText);
                System.Diagnostics.Debug.WriteLine($"SearchSuppliers: Найдено {suppliers.Count} поставщиков для запроса '{searchText}'.");
                return suppliers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchSuppliers Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Supplier>();
            }
        }

        public List<Supplier> FilterSuppliers(string name)
        {
            try
            {
                var suppliers = LoadSuppliers();
                var filtered = suppliers.Where(s =>
                    string.IsNullOrEmpty(name) || s.Name.ToLower().Contains(name.ToLower())
                ).ToList();
                System.Diagnostics.Debug.WriteLine($"FilterSuppliers: Найдено {filtered.Count} поставщиков с именем '{name}'.");
                return filtered;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FilterSuppliers Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при фильтрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Supplier>();
            }
        }

        public void ExportToExcel(List<Supplier> suppliers, string filePath)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Suppliers");
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Название";
                    worksheet.Cell(1, 3).Value = "Контактный телефон";
                    worksheet.Cell(1, 4).Value = "Электронная почта";
                    worksheet.Cell(1, 5).Value = "Адрес";

                    for (int i = 0; i < suppliers.Count; i++)
                    {
                        var supplier = suppliers[i];
                        worksheet.Cell(i + 2, 1).Value = supplier.SupplierID;
                        worksheet.Cell(i + 2, 2).Value = supplier.Name;
                        worksheet.Cell(i + 2, 3).Value = supplier.ContactPhone;
                        worksheet.Cell(i + 2, 4).Value = supplier.Email;
                        worksheet.Cell(i + 2, 5).Value = supplier.Address;
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(filePath);
                }
                System.Diagnostics.Debug.WriteLine($"ExportToExcel: Экспортировано {suppliers.Count} поставщиков в {filePath}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExportToExcel Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при экспорте в Excel файл: {ex.Message}", ex);
            }
        }

        private void ValidateSupplier(Supplier supplier)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));

            if (string.IsNullOrWhiteSpace(supplier.Name))
                throw new ArgumentException("Название поставщика не должно быть пустым.");

            if (supplier.Name.Length > 100)
                throw new ArgumentException("Название поставщика не должно превышать 100 символов.");

            if (!string.IsNullOrEmpty(supplier.ContactPhone) && !Regex.IsMatch(supplier.ContactPhone, @"^\+?\d{10,15}$"))
                throw new ArgumentException("Контактный телефон должен содержать от 10 до 15 цифр и может начинаться с '+'.");

            if (!string.IsNullOrEmpty(supplier.Email) && !Regex.IsMatch(supplier.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Электронная почта должна быть корректной.");

            if (!string.IsNullOrEmpty(supplier.Email) && supplier.Email.Length > 100)
                throw new ArgumentException("Электронная почта не должна превышать 100 символов.");

            if (string.IsNullOrWhiteSpace(supplier.Address))
                throw new ArgumentException("Адрес поставщика не должен быть пустым.");

            if (supplier.Address.Length > 200)
                throw new ArgumentException("Адрес не должен превышать 200 символов.");
        }
    }
}
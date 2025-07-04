using AvtoServis.Data.Repositories;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AvtoServis.ViewModels.Screens
{
    public class ManufacturersViewModel
    {
        private readonly ManufacturersRepository _repository;
        public List<(string Column, string SearchText)> Filters { get; set; }
        public List<string> VisibleColumns { get; set; }

        public ManufacturersViewModel(ManufacturersRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Filters = new List<(string Column, string SearchText)>();
            VisibleColumns = new List<string> { "Nomer", "Name" };
        }

        public List<Manufacturer> LoadManufacturers()
        {
            try
            {
                var manufacturers = _repository.GetAll();
                if (Filters != null && Filters.Any())
                {
                    manufacturers = ApplyFilters(manufacturers);
                }
                System.Diagnostics.Debug.WriteLine($"LoadManufacturers: Loaded {manufacturers.Count} manufacturers.");
                return manufacturers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadManufacturers Error: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки производителей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Manufacturer>();
            }
        }

        public Manufacturer LoadManufacturer(int id)
        {
            try
            {
                var manufacturer = _repository.GetById(id);
                if (manufacturer == null)
                    throw new Exception($"Производитель с ID {id} не найден.");
                return manufacturer;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadManufacturer Error: {ex.Message}");
                throw;
            }
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            try
            {
                ValidateManufacturer(manufacturer);
                _repository.Add(manufacturer);
                System.Diagnostics.Debug.WriteLine($"AddManufacturer: Added manufacturer '{manufacturer.Name}' with ID {manufacturer.ManufacturerID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddManufacturer Error: {ex.Message}");
                throw new Exception($"Ошибка при добавлении производителя: {ex.Message}", ex);
            }
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            try
            {
                ValidateManufacturer(manufacturer);
                _repository.Update(manufacturer);
                System.Diagnostics.Debug.WriteLine($"UpdateManufacturer: Updated manufacturer '{manufacturer.Name}' with ID {manufacturer.ManufacturerID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateManufacturer Error: {ex.Message}");
                throw new Exception($"Ошибка при обновлении производителя: {ex.Message}", ex);
            }
        }

        public void DeleteManufacturer(int id)
        {
            try
            {
                _repository.Delete(id);
                System.Diagnostics.Debug.WriteLine($"DeleteManufacturer: Deleted manufacturer with ID {id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteManufacturer Error: {ex.Message}");
                throw new Exception($"Ошибка при удалении производителя: {ex.Message}", ex);
            }
        }

        public List<Manufacturer> SearchManufacturers(string searchText)
        {
            try
            {
                var manufacturers = LoadManufacturers();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return manufacturers;
                }

                searchText = searchText.ToLower().Trim();
                var filteredManufacturers = manufacturers.Where(manufacturer =>
                    (VisibleColumns.Contains("Nomer") && manufacturer.ManufacturerID.ToString().Contains(searchText)) ||
                    (VisibleColumns.Contains("Name") && manufacturer.Name?.ToLower().Contains(searchText) == true)
                ).ToList();

                System.Diagnostics.Debug.WriteLine($"SearchManufacturers: Found {filteredManufacturers.Count} manufacturers for query '{searchText}'.");
                return filteredManufacturers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchManufacturers Error: {ex.Message}");
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Manufacturer>();
            }
        }

        private List<Manufacturer> ApplyFilters(IEnumerable<Manufacturer> manufacturers)
        {
            var filteredManufacturers = manufacturers.ToList();
            foreach (var filter in Filters)
            {
                var searchText = filter.SearchText.ToLower().Trim();
                switch (filter.Column)
                {
                    case "Nomer":
                        if (int.TryParse(searchText, out int id))
                        {
                            filteredManufacturers = filteredManufacturers.Where(m => m.ManufacturerID == id).ToList();
                        }
                        else
                        {
                            filteredManufacturers = filteredManufacturers.Where(m => m.ManufacturerID.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "Name":
                        filteredManufacturers = filteredManufacturers.Where(m => m.Name?.ToLower().Contains(searchText) == true).ToList();
                        break;
                }
            }
            return filteredManufacturers;
        }

        private void ValidateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            if (string.IsNullOrWhiteSpace(manufacturer.Name))
                throw new ArgumentException("Название производителя не может быть пустым.");

            if (manufacturer.Name.Length > 100)
                throw new ArgumentException("Название производителя не может превышать 100 символов.");

            if (!Regex.IsMatch(manufacturer.Name, @"^[а-яА-Яa-zA-Z0-9\s]+$"))
                throw new ArgumentException("Название производителя может содержать только буквы (русские или латинские), цифры и пробелы.");
        }

        public void ExportToExcel(List<Manufacturer> manufacturers, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Manufacturers");
                    var headers = new List<string>();
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "Nomer", "Номер" },
                        { "Name", "Название производителя" }
                    };

                    int colIndex = 1;
                    foreach (var key in columnVisibility.Keys)
                    {
                        if (key != "Actions" && columnVisibility[key])
                        {
                            headers.Add(columnMapping[key]);
                            worksheet.Cell(1, colIndex).Value = columnMapping[key];
                            colIndex++;
                        }
                    }

                    var headerRange = worksheet.Range(1, 1, 1, colIndex - 1);
                    headerRange.Style
                        .Fill.SetBackgroundColor(XLColor.FromArgb(200, 204, 208))
                        .Font.SetFontName("Segoe UI")
                        .Font.SetBold(true)
                        .Font.SetFontSize(12)
                        .Border.SetOutsideBorder(XLBorderStyleValues.Medium)
                        .Border.SetInsideBorder(XLBorderStyleValues.Thin)
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    for (int i = 0; i < manufacturers.Count; i++)
                    {
                        colIndex = 1;
                        if (columnVisibility["Nomer"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = manufacturers[i].ManufacturerID;
                            colIndex++;
                        }
                        if (columnVisibility["Name"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = manufacturers[i].Name;
                            colIndex++;
                        }
                    }

                    var dataRange = worksheet.Range(2, 1, manufacturers.Count + 1, colIndex - 1);
                    dataRange.Style
                        .Font.SetFontName("Segoe UI")
                        .Font.SetFontSize(10)
                        .Border.SetOutsideBorder(XLBorderStyleValues.Medium)
                        .Border.SetInsideBorder(XLBorderStyleValues.Thin)
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExportToExcel Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System.Text.RegularExpressions;

namespace AvtoServis.ViewModels.Screens
{
    public class CarBrandViewModel
    {
        private readonly ICarBrandRepository _carBrandRepository;
        public List<(string Column, string SearchText)> Filters { get; set; }
        public List<string> VisibleColumns { get; set; }

        public CarBrandViewModel(ICarBrandRepository carBrandRepository)
        {
            _carBrandRepository = carBrandRepository ?? throw new ArgumentNullException(nameof(carBrandRepository));
            Filters = new List<(string Column, string SearchText)>();
            VisibleColumns = new List<string> { "Id", "CarBrandName" };
        }

        public List<CarBrand> LoadBrands()
        {
            try
            {
                var brands = _carBrandRepository.GetAll();
                if (Filters != null && Filters.Any())
                {
                    brands = ApplyFilters(brands);
                }
                return brands.ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadBrands Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при загрузке марок автомобилей.", ex);
            }
        }

        public List<CarBrand> SearchBrands(string searchText)
        {
            try
            {
                var brands = LoadBrands();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return brands;
                }

                searchText = searchText.ToLower().Trim();
                var filteredBrands = brands.Where(brand =>
                    (VisibleColumns.Contains("Id") && brand.Id.ToString().Contains(searchText)) ||
                    (VisibleColumns.Contains("CarBrandName") && brand.CarBrandName?.ToLower().Contains(searchText) == true)
                ).ToList();

                return filteredBrands;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchBrands Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при поиске марок автомобилей.", ex);
            }
        }

        private List<CarBrand> ApplyFilters(IEnumerable<CarBrand> brands)
        {
            var filteredBrands = brands.ToList();
            foreach (var filter in Filters)
            {
                var searchText = filter.SearchText.ToLower().Trim();
                switch (filter.Column)
                {
                    case "Id":
                        if (int.TryParse(searchText, out int id))
                        {
                            filteredBrands = filteredBrands.Where(b => b.Id == id).ToList();
                        }
                        else
                        {
                            filteredBrands = filteredBrands.Where(b => b.Id.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "CarBrandName":
                        filteredBrands = filteredBrands.Where(b => b.CarBrandName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                }
            }
            return filteredBrands;
        }

        public void AddBrand(CarBrand brand)
        {
            try
            {
                ValidateBrand(brand);
                _carBrandRepository.Add(brand);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddBrand Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при добавлении марки автомобиля.", ex);
            }
        }

        public void UpdateBrand(CarBrand brand)
        {
            try
            {
                ValidateBrand(brand);
                _carBrandRepository.Update(brand);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateBrand Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при обновлении марки автомобиля.", ex);
            }
        }

        public void DeleteBrand(int id)
        {
            try
            {
                _carBrandRepository.Delete(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteBrand Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при удалении марки автомобиля.", ex);
            }
        }

        private void ValidateBrand(CarBrand brand)
        {
            if (brand == null)
            {
                throw new ArgumentNullException(nameof(brand), "Марка автомобиля не может быть null.");
            }

            if (string.IsNullOrWhiteSpace(brand.CarBrandName))
            {
                throw new ArgumentException("Название марки не может быть пустым.", nameof(brand.CarBrandName));
            }

            if (brand.CarBrandName.Length > 100)
            {
                throw new ArgumentException("Название марки не должно превышать 100 символов.", nameof(brand.CarBrandName));
            }

            if (!Regex.IsMatch(brand.CarBrandName, @"^[a-zA-Z0-9\s]+$"))
            {
                throw new ArgumentException("Название марки должно содержать только латинские буквы, цифры и пробелы.", nameof(brand.CarBrandName));
            }
        }

        public void ExportToExcel(List<CarBrand> brands, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("CarBrands");
                    int colIndex = 1;

                    if (columnVisibility["Id"])
                    {
                        worksheet.Cell(1, colIndex).Value = "ID";
                        colIndex++;
                    }
                    if (columnVisibility["CarBrandName"])
                    {
                        worksheet.Cell(1, colIndex).Value = "Марка";
                        colIndex++;
                    }

                    var headerRange = worksheet.Range(1, 1, 1, colIndex - 1);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(240, 243, 245);
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                    int rowIndex = 2;
                    foreach (var brand in brands)
                    {
                        colIndex = 1;
                        if (columnVisibility["Id"])
                        {
                            worksheet.Cell(rowIndex, colIndex).Value = brand.Id;
                            colIndex++;
                        }
                        if (columnVisibility["CarBrandName"])
                        {
                            worksheet.Cell(rowIndex, colIndex).Value = brand.CarBrandName;
                            colIndex++;
                        }
                        rowIndex++;
                    }

                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExportToExcel Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при экспорте данных в Excel.", ex);
            }
        }
    }
}
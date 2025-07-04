using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System.Text.RegularExpressions;

namespace AvtoServis.ViewModels.Screens
{
    public class CarModelViewModel
    {
        private readonly ICarModelsRepository _carModelRepository;
        private readonly ICarBrandRepository _carBrandRepository;
        public List<(string Column, string SearchText)> Filters { get; set; }
        public List<string> VisibleColumns { get; set; }

        public CarModelViewModel(ICarModelsRepository carModelRepository, ICarBrandRepository carBrandRepository)
        {
            _carModelRepository = carModelRepository ?? throw new ArgumentNullException(nameof(carModelRepository));
            _carBrandRepository = carBrandRepository ?? throw new ArgumentNullException(nameof(carBrandRepository));
            Filters = new List<(string, string)>();
            VisibleColumns = new List<string> { "Id", "CarBrandName", "Model", "Year" };
        }

        public List<CarModel> LoadModels()
        {
            try
            {
                var models = _carModelRepository.GetAll();
                if (Filters != null && Filters.Any())
                {
                    models = ApplyFilters(models);
                }
                return models;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при загрузке моделей автомобилей.", ex);
            }
        }

        public List<CarBrand> LoadBrands()
        {
            try
            {
                return _carBrandRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при загрузке марок автомобилей.", ex);
            }
        }

        public void AddModel(CarModel model)
        {
            try
            {
                ValidateModel(model);
                _carModelRepository.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при добавлении модели.", ex);
            }
        }

        public void UpdateModel(CarModel model)
        {
            try
            {
                ValidateModel(model);
                _carModelRepository.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при обновлении модели.", ex);
            }
        }

        public void DeleteModel(int id)
        {
            try
            {
                _carModelRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при удалении модели.", ex);
            }
        }

        public List<CarModel> SearchModels(string searchText)
        {
            try
            {
                var models = LoadModels();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return models;
                }

                searchText = searchText.ToLower();
                return models.Where(m =>
                    (VisibleColumns.Contains("Id") && m.Id.ToString().Contains(searchText)) ||
                    (VisibleColumns.Contains("CarBrandName") && m.CarBrandName?.ToLower().Contains(searchText) == true) ||
                    (VisibleColumns.Contains("Model") && m.Model?.ToLower().Contains(searchText) == true) ||
                    (VisibleColumns.Contains("Year") && m.Year.ToString().Contains(searchText))
                ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при поиске моделей.", ex);
            }
        }

        private List<CarModel> ApplyFilters(List<CarModel> models)
        {
            foreach (var filter in Filters)
            {
                var searchText = filter.SearchText.ToLower();
                models = filter.Column switch
                {
                    "Id" => models.Where(m => m.Id.ToString().Contains(searchText)).ToList(),
                    "CarBrandName" => models.Where(m => m.CarBrandName?.ToLower().Contains(searchText) == true).ToList(),
                    "Model" => models.Where(m => m.Model?.ToLower().Contains(searchText) == true).ToList(),
                    "Year" => models.Where(m => m.Year.ToString().Contains(searchText)).ToList(),
                    _ => models
                };
            }
            return models;
        }

        private void ValidateModel(CarModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.CarBrandId <= 0)
                throw new ArgumentException("Марка автомобиля должна быть выбрана.");

            if (string.IsNullOrWhiteSpace(model.Model))
                throw new ArgumentException("Название модели не может быть пустым.");

            if (model.Model.Length > 100)
                throw new ArgumentException("Название модели не должно превышать 100 символов.");

            if (!Regex.IsMatch(model.Model, @"^[a-zA-Z0-9\s]+$"))
                throw new ArgumentException("Название модели должно содержать только латинские буквы, цифры и пробелы.");

            if (model.Year < 1900 || model.Year > DateTime.Now.Year + 1)
                throw new ArgumentException($"Год должен быть между 1900 и {DateTime.Now.Year + 1}.");
        }

        public void ExportToExcel(List<CarModel> models, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("CarModels");

                int colIndex = 1;
                List<string> headers = new List<string>();
                if (columnVisibility["Id"]) headers.Add("ID");
                if (columnVisibility["CarBrandName"]) headers.Add("Марка");
                if (columnVisibility["Model"]) headers.Add("Модель");
                if (columnVisibility["Year"]) headers.Add("Год");

                for (int i = 0; i < headers.Count; i++)
                {
                    var cell = worksheet.Cell(1, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.FromArgb(240, 243, 245);
                }

                int rowIndex = 2;
                foreach (var model in models)
                {
                    colIndex = 1;
                    if (columnVisibility["Id"])
                        worksheet.Cell(rowIndex, colIndex++).Value = model.Id;
                    if (columnVisibility["CarBrandName"])
                        worksheet.Cell(rowIndex, colIndex++).Value = model.CarBrandName;
                    if (columnVisibility["Model"])
                        worksheet.Cell(rowIndex, colIndex++).Value = model.Model;
                    if (columnVisibility["Year"])
                        worksheet.Cell(rowIndex, colIndex++).Value = model.Year;
                    rowIndex++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при экспорте данных в Excel.", ex);
            }
        }
    }
}
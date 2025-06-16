using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AvtoServis.ViewModels.Screens
{
    public class CarModelsViewModel
    {
        private readonly ICarModelsRepository _modelRepository;
        private readonly ICarBrandRepository _brandRepository;

        public CarModelsViewModel(ICarModelsRepository modelRepository, ICarBrandRepository brandRepository)
        {
            _modelRepository = modelRepository ?? throw new ArgumentNullException(nameof(modelRepository));
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
        }

        public List<CarModel> LoadModels()
        {
            try
            {
                var models = _modelRepository.GetAll();
                System.Diagnostics.Debug.WriteLine($"LoadModels: Загружено {models.Count} моделей автомобилей.");
                return models;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadModels Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при загрузке моделей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<CarModel>();
            }
        }

        public List<CarBrand> LoadBrands()
        {
            try
            {
                var brands = _brandRepository.GetAll();
                System.Diagnostics.Debug.WriteLine($"LoadBrands: Загружено {brands.Count} марок автомобилей.");
                return brands;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadBrands Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при загрузке марок: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<CarBrand>();
            }
        }

        public void AddModel(CarModel model)
        {
            try
            {
                ValidateModel(model);
                _modelRepository.Add(model);
                System.Diagnostics.Debug.WriteLine($"AddModel: Добавлена модель '{model.Model}' с ID {model.Id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddModel Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при добавлении модели: {ex.Message}", ex);
            }
        }

        public void UpdateModel(CarModel model)
        {
            try
            {
                ValidateModel(model);
                _modelRepository.Update(model);
                System.Diagnostics.Debug.WriteLine($"UpdateModel: Обновлена модель '{model.Model}' с ID {model.Id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateModel Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при обновлении модели: {ex.Message}", ex);
            }
        }

        public void DeleteModel(int id)
        {
            try
            {
                _modelRepository.Delete(id);
                System.Diagnostics.Debug.WriteLine($"DeleteModel: Удалена модель с ID {id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteModel Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при удалении модели: {ex.Message}", ex);
            }
        }

        public void AddBrand(CarBrand brand)
        {
            try
            {
                ValidateBrand(brand);
                _brandRepository.Add(brand);
                System.Diagnostics.Debug.WriteLine($"AddBrand: Добавлена марка '{brand.CarBrandName}' с ID {brand.Id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddBrand Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при добавлении марки: {ex.Message}", ex);
            }
        }

        public List<CarModel> SearchModels(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return LoadModels();

                var models = _modelRepository.SearchByModel(searchText);
                System.Diagnostics.Debug.WriteLine($"SearchModels: Найдено {models.Count} моделей для запроса '{searchText}'.");
                return models;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchModels Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<CarModel>();
            }
        }

        public List<CarModel> SearchByYear(int year)
        {
            try
            {
                var models = _modelRepository.SearchByYear(year);
                System.Diagnostics.Debug.WriteLine($"SearchByYear: Найдено {models.Count} моделей для года {year}.");
                return models;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchByYear Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при поиске по году: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<CarModel>();
            }
        }

        public List<CarModel> FilterModels(int? minYear, int? maxYear, int? brandId)
        {
            try
            {
                var models = LoadModels();
                var filtered = models.Where(m =>
                    (!minYear.HasValue || m.Year >= minYear) &&
                    (!maxYear.HasValue || m.Year <= maxYear) &&
                    (!brandId.HasValue || m.CarBrandId == brandId)
                ).ToList();
                System.Diagnostics.Debug.WriteLine($"FilterModels: Найдено {filtered.Count} моделей с minYear={minYear ?? 0}, maxYear={maxYear ?? int.MaxValue}, brandId={brandId ?? 0}.");
                return filtered;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FilterModels Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при фильтрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<CarModel>();
            }
        }

        public List<CarModel> FilterByBrand(int brandId)
        {
            try
            {
                var models = _modelRepository.FilterByBrand(brandId);
                System.Diagnostics.Debug.WriteLine($"FilterByBrand: Найдено {models.Count} моделей для марки ID {brandId}.");
                return models;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FilterByBrand Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при фильтрации по марке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<CarModel>();
            }
        }

        public void ExportToCsv(List<CarModel> models, string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("ID,Марка,Модель,Год");
                    foreach (var model in models)
                    {
                        writer.WriteLine($"{model.Id},\"{model.CarBrandName}\",\"{model.Model}\", {model.Year}");
                    }
                }
                System.Diagnostics.Debug.WriteLine($"ExportToCsv: Экспортировано {models.Count} моделей в {filePath}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExportToCsv Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при экспорте в CSV файл: {ex.Message}", ex);
            }
        }

        private void ValidateModel(CarModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Model))
                throw new ArgumentException("Название модели не должно быть пустым.");

            if (model.Model.Length > 100)
                throw new ArgumentException("Название модели не должно превышать 100 символов.");

            if (!Regex.IsMatch(model.Model, @"^[a-zA-Z0-9\s]+$"))
                throw new ArgumentException("Название модели должно содержать только латинские буквы, цифры и пробелы.");

            if (model.Year < 1900 || model.Year > DateTime.Now.Year + 1)
                throw new ArgumentException($"Год не должен быть больше {DateTime.Now.Year + 1} или меньше 1900.");

            if (model.CarBrandId <= 0)
                throw new ArgumentException("Марка автомобиля не выбрана.");
        }

        private void ValidateBrand(CarBrand brand)
        {
            if (brand == null)
                throw new ArgumentNullException(nameof(brand));

            if (string.IsNullOrWhiteSpace(brand.CarBrandName))
                throw new ArgumentException("Название марки не должно быть пустым.");

            if (brand.CarBrandName.Length > 100)
                throw new ArgumentException("Название марки не должно превышать 100 символов.");

            if (!Regex.IsMatch(brand.CarBrandName, @"^[a-zA-Z0-9\s]+$"))
                throw new ArgumentException("Название марки должно содержать только латинские буквы, цифры и пробелы.");
        }
    }
}
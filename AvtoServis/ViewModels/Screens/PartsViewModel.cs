using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AvtoServis.ViewModels.Screens
{
    public class PartsViewModel
    {
        private readonly IPartsRepository _partsRepository;
        private readonly IPartQualitiesRepository _qualitiesRepository;
        private readonly ICarBrandRepository _brandRepository;
        private readonly IManufacturersRepository _manufacturersRepository;

        public PartsViewModel(IPartsRepository partsRepository, IPartQualitiesRepository qualitiesRepository, ICarBrandRepository brandRepository, IManufacturersRepository manufacturersRepository)
        {
            _partsRepository = partsRepository ?? throw new ArgumentNullException(nameof(partsRepository));
            _qualitiesRepository = qualitiesRepository ?? throw new ArgumentNullException(nameof(qualitiesRepository));
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _manufacturersRepository = manufacturersRepository ?? throw new ArgumentNullException(nameof(manufacturersRepository));
        }

        public List<Part> LoadParts()
        {
            try
            {
                var parts = _partsRepository.GetAll();
                System.Diagnostics.Debug.WriteLine($"LoadParts: Загружено {parts.Count} деталей.");
                return parts;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadParts Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при загрузке деталей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Part>();
            }
        }

        public List<PartQuality> LoadQualities()
        {
            try
            {
                var qualities = _qualitiesRepository.GetAll();
                System.Diagnostics.Debug.WriteLine($"LoadQualities: Загружено {qualities.Count} качеств деталей.");
                return qualities;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadQualities Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при загрузке качеств: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<PartQuality>();
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

        public List<Manufacturer> LoadManufacturers()
        {
            try
            {
                var manufacturers = _manufacturersRepository.GetAll();
               
                return manufacturers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadManufacturers Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при загрузке производителей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Manufacturer>();
            }
        }

        public void AddPart(Part part)
        {
            try
            {
                ValidatePart(part);
                _partsRepository.Add(part);
                System.Diagnostics.Debug.WriteLine($"AddPart: Добавлена деталь '{part.PartName}' с ID {part.PartID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddPart Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при добавлении детали: {ex.Message}", ex);
            }
        }

        public void UpdatePart(Part part)
        {
            try
            {
                ValidatePart(part);
                _partsRepository.Update(part);
                System.Diagnostics.Debug.WriteLine($"UpdatePart: Обновлена деталь '{part.PartName}' с ID {part.PartID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdatePart Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при обновлении детали: {ex.Message}", ex);
            }
        }

        public void DeletePart(int id)
        {
            try
            {
                var part = _partsRepository.GetById(id);
                if (part == null)
                    throw new Exception($"Деталь с ID {id} не найдена.");

                if (!string.IsNullOrEmpty(part.PhotoPath))
                {
                    string fullPath = Path.Combine(Application.StartupPath, part.PhotoPath);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                        System.Diagnostics.Debug.WriteLine($"DeletePart: Удалена фотография {fullPath}.");
                    }
                }

                _partsRepository.Delete(id);
                System.Diagnostics.Debug.WriteLine($"DeletePart: Удалена деталь с ID {id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeletePart Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при удалении детали: {ex.Message}", ex);
            }
        }

        public void AddQuality(PartQuality quality)
        {
            try
            {
                ValidateQuality(quality);
                _qualitiesRepository.Add(quality);
                System.Diagnostics.Debug.WriteLine($"AddQuality: Добавлено качество '{quality.Name}' с ID {quality.QualityID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddQuality Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при добавлении качества: {ex.Message}", ex);
            }
        }

        public void UpdateQuality(PartQuality quality)
        {
            try
            {
                ValidateQuality(quality);
                _qualitiesRepository.Update(quality);
                System.Diagnostics.Debug.WriteLine($"UpdateQuality: Обновлено качество '{quality.Name}' с ID {quality.QualityID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateQuality Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при обновлении качества: {ex.Message}", ex);
            }
        }

        public void DeleteQuality(int id)
        {
            try
            {
                _qualitiesRepository.Delete(id);
                System.Diagnostics.Debug.WriteLine($"DeleteQuality: Удалено качество с ID {id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteQuality Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при удалении качества: {ex.Message}", ex);
            }
        }

        public PartQuality GetQualityById(int qualityId)
        {
            try
            {
                var quality = _qualitiesRepository.GetById(qualityId);
                if (quality == null)
                    throw new Exception($"Качество с ID {qualityId} не найдено.");
                System.Diagnostics.Debug.WriteLine($"GetQualityById: Загружено качество '{quality.Name}' с ID {qualityId}.");
                return quality;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetQualityById Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при загрузке качества: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public List<Part> SearchParts(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return LoadParts();

                var parts = _partsRepository.Search(searchText);
                System.Diagnostics.Debug.WriteLine($"SearchParts: Найдено {parts.Count} деталей для запроса '{searchText}'.");
                return parts;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchParts Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Part>();
            }
        }

        public List<Part> FilterParts(int? minYear, int? maxYear, int? brandId, int? qualityId)
        {
            try
            {
                var parts = LoadParts();
                var filtered = parts.Where(p =>
                    (!brandId.HasValue || p.CarBrandId == brandId) &&
                    (!qualityId.HasValue || p.QualityID == qualityId)
                ).ToList();
                System.Diagnostics.Debug.WriteLine($"FilterParts: Найдено {filtered.Count} деталей с brandId={brandId ?? 0}, qualityId={qualityId ?? 0}.");
                return filtered;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FilterParts Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка при фильтрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Part>();
            }
        }

        public void ExportToExcel(List<Part> parts, string filePath)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Parts");
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Марка";
                    worksheet.Cell(1, 3).Value = "Каталожный номер";
                    worksheet.Cell(1, 4).Value = "Название детали";
                    worksheet.Cell(1, 5).Value = "Качество";
                    worksheet.Cell(1, 6).Value = "Характеристики";
                    worksheet.Cell(1, 7).Value = "Фотография";

                    for (int i = 0; i < parts.Count; i++)
                    {
                        var part = parts[i];
                        worksheet.Cell(i + 2, 1).Value = part.PartID;
                        worksheet.Cell(i + 2, 2).Value = part.CarBrandName;
                        worksheet.Cell(i + 2, 3).Value = part.CatalogNumber;
                        worksheet.Cell(i + 2, 4).Value = part.PartName;
                        worksheet.Cell(i + 2, 5).Value = part.QualityName;
                        worksheet.Cell(i + 2, 6).Value = part.Characteristics;
                        worksheet.Cell(i + 2, 7).Value = part.PhotoPath;
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(filePath);
                }
                System.Diagnostics.Debug.WriteLine($"ExportToExcel: Экспортировано {parts.Count} деталей в {filePath}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExportToExcel Ошибка: {ex.Message}");
                throw new Exception($"Ошибка при экспорте в Excel файл: {ex.Message}", ex);
            }
        }

        private void ValidatePart(Part part)
        {
            if (part == null)
                throw new ArgumentNullException(nameof(part));

            if (string.IsNullOrWhiteSpace(part.CatalogNumber))
                throw new ArgumentException("Каталожный номер не должен быть пустым.");

            if (part.CatalogNumber.Length > 50)
                throw new ArgumentException("Каталожный номер не должен превышать 50 символов.");

            if (!Regex.IsMatch(part.CatalogNumber, @"^[a-zA-Z0-9]+$"))
                throw new ArgumentException("Каталожный номер должен содержать только латинские буквы и цифры.");

            if (string.IsNullOrWhiteSpace(part.PartName))
                throw new ArgumentException("Название детали не должно быть пустым.");

            if (part.PartName.Length > 100)
                throw new ArgumentException("Название детали не должен превышать 100 символов.");

            if (part.CarBrandId <= 0)
                throw new ArgumentException("Марка автомобиля не выбрана.");

            if (part.QualityID <= 0)
                throw new ArgumentException("Качество детали не выбрано.");

            if (part.ManufacturerID <= 0)
                throw new ArgumentException("Производитель не выбран.");

            if (!string.IsNullOrEmpty(part.PhotoPath))
            {
                string fullPath = Path.Combine(Application.StartupPath, part.PhotoPath);
                if (!File.Exists(fullPath))
                    throw new ArgumentException("Указанный путь к фотографии недействителен.");
            }
        }

        private void ValidateQuality(PartQuality quality)
        {
            if (quality == null)
                throw new ArgumentNullException(nameof(quality));

            if (string.IsNullOrWhiteSpace(quality.Name))
                throw new ArgumentException("Название качества не должно быть пустым.");

            if (quality.Name.Length > 50)
                throw new ArgumentException("Название качества не должно превышать 50 символов.");

            if (!Regex.IsMatch(quality.Name, @"^[a-zA-Z0-9\s]+$"))
                throw new ArgumentException("Название качества должно содержать только латинские буквы, цифры и пробелы.");
        }
    }
}
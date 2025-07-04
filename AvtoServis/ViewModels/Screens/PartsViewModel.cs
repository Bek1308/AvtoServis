using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvtoServis.ViewModels.Screens
{
    public class PartsViewModel
    {
        private readonly IPartsRepository _partsRepository;
        private readonly ICarBrandRepository _carBrandsRepository;
        private readonly IManufacturersRepository _manufacturersRepository;
        private readonly IPartQualitiesRepository _partQualitiesRepository;
        public List<(string Column, string SearchText)> Filters { get; set; }
        public List<string> VisibleColumns { get; set; }

        public PartsViewModel(
            IPartsRepository partsRepository,
            ICarBrandRepository carBrandsRepository,
            IManufacturersRepository manufacturersRepository,
            IPartQualitiesRepository partQualitiesRepository)
        {
            _partsRepository = partsRepository ?? throw new ArgumentNullException(nameof(partsRepository));
            _carBrandsRepository = carBrandsRepository ?? throw new ArgumentNullException(nameof(carBrandsRepository));
            _manufacturersRepository = manufacturersRepository ?? throw new ArgumentNullException(nameof(manufacturersRepository));
            _partQualitiesRepository = partQualitiesRepository ?? throw new ArgumentNullException(nameof(partQualitiesRepository));
            Filters = new List<(string Column, string SearchText)>();
            VisibleColumns = new List<string>
            {
                "PartID",
                "CarBrandName",
                "CatalogNumber",
                "PartName",
                "ManufacturerName",
                "QualityName",
                "Characteristics"
            };
        }

        public List<Part> LoadParts()
        {
            try
            {
                var parts = _partsRepository.GetAll();
                var brands = _carBrandsRepository.GetAll();
                var manufacturers = _manufacturersRepository.GetAll();
                var qualities = _partQualitiesRepository.GetAll();

                var result = parts.Select(p => new Part
                {
                    PartID = p.PartID,
                    CarBrandId = p.CarBrandId,
                    CarBrandName = brands.FirstOrDefault(b => b.Id == p.CarBrandId)?.CarBrandName ?? "Неизвестно",
                    CatalogNumber = p.CatalogNumber,
                    ManufacturerID = p.ManufacturerID,
                    ManufacturerName = manufacturers.FirstOrDefault(m => m.ManufacturerID == p.ManufacturerID)?.Name ?? "Неизвестно",
                    QualityID = p.QualityID,
                    QualityName = qualities.FirstOrDefault(q => q.QualityID == p.QualityID)?.Name ?? "Неизвестно",
                    PartName = p.PartName,
                    Characteristics = p.Characteristics,
                    PhotoPath = p.PhotoPath
                }).ToList();

                if (Filters != null && Filters.Any())
                {
                    result = ApplyFilters(result);
                }

                System.Diagnostics.Debug.WriteLine($"LoadParts: Loaded {result.Count} parts.");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadParts Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public List<PartQuality> LoadQualities()
        {
            try
            {
                return _partQualitiesRepository.GetAll();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadQualities Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public List<object> LoadBrands()
        {
            try
            {
                return _carBrandsRepository.GetAll().Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadBrands Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public List<object> LoadManufacturers()
        {
            try
            {
                return _manufacturersRepository.GetAll().Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadManufacturers Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public List<Part> SearchParts(string searchText, List<string> visibleColumns)
        {
            try
            {
                var parts = LoadParts();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return parts;
                }

                searchText = searchText.ToLower().Trim();
                var filteredParts = parts.Where(p =>
                {
                    bool matches = false;
                    if (visibleColumns.Contains("PartID"))
                        matches |= p.PartID.ToString().Contains(searchText);
                    if (visibleColumns.Contains("CarBrandName"))
                        matches |= p.CarBrandName?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("CatalogNumber"))
                        matches |= p.CatalogNumber?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("PartName"))
                        matches |= p.PartName?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("ManufacturerName"))
                        matches |= p.ManufacturerName?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("QualityName"))
                        matches |= p.QualityName?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("Characteristics"))
                        matches |= p.Characteristics?.ToLower().Contains(searchText) == true;
                    return matches;
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"SearchParts: Found {filteredParts.Count} parts for query '{searchText}'.");
                return filteredParts;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchParts Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        private List<Part> ApplyFilters(IEnumerable<Part> parts)
        {
            var filteredParts = parts.ToList();
            foreach (var filter in Filters)
            {
                var searchText = filter.SearchText.ToLower().Trim();
                switch (filter.Column)
                {
                    case "PartID":
                        if (int.TryParse(searchText, out int id))
                        {
                            filteredParts = filteredParts.Where(p => p.PartID == id).ToList();
                        }
                        else
                        {
                            filteredParts = filteredParts.Where(p => p.PartID.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "CarBrandName":
                        filteredParts = filteredParts.Where(p => p.CarBrandName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "CatalogNumber":
                        filteredParts = filteredParts.Where(p => p.CatalogNumber?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "PartName":
                        filteredParts = filteredParts.Where(p => p.PartName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "ManufacturerName":
                        filteredParts = filteredParts.Where(p => p.ManufacturerName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "QualityName":
                        filteredParts = filteredParts.Where(p => p.QualityName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "Characteristics":
                        filteredParts = filteredParts.Where(p => p.Characteristics?.ToLower().Contains(searchText) == true).ToList();
                        break;
                }
            }
            return filteredParts;
        }

        public void AddPart(Part part)
        {
            try
            {
                _partsRepository.Add(part);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddPart Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void UpdatePart(Part part)
        {
            try
            {
                _partsRepository.Update(part);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdatePart Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void DeletePart(int id)
        {
            try
            {
                _partsRepository.Delete(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeletePart Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void ExportToExcel(List<Part> parts, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Parts");
                    var headers = new List<string>();
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "PartID", "ID" },
                        { "CarBrandName", "Марка" },
                        { "CatalogNumber", "Каталожный номер" },
                        { "PartName", "Название детали" },
                        { "ManufacturerName", "Производитель" },
                        { "QualityName", "Качество" },
                        { "Characteristics", "Характеристики" }
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

                    for (int i = 0; i < parts.Count; i++)
                    {
                        colIndex = 1;
                        if (columnVisibility["PartID"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = parts[i].PartID;
                            colIndex++;
                        }
                        if (columnVisibility["CarBrandName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = parts[i].CarBrandName;
                            colIndex++;
                        }
                        if (columnVisibility["CatalogNumber"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = parts[i].CatalogNumber;
                            colIndex++;
                        }
                        if (columnVisibility["PartName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = parts[i].PartName;
                            colIndex++;
                        }
                        if (columnVisibility["ManufacturerName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = parts[i].ManufacturerName;
                            colIndex++;
                        }
                        if (columnVisibility["QualityName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = parts[i].QualityName;
                            colIndex++;
                        }
                        if (columnVisibility["Characteristics"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = parts[i].Characteristics;
                            colIndex++;
                        }
                    }

                    var dataRange = worksheet.Range(2, 1, parts.Count + 1, colIndex - 1);
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
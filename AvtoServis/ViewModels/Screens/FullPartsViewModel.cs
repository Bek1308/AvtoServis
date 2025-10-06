using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using AvtoServis.Data.Repositories;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AvtoServis.Model.DTOs;

namespace AvtoServis.ViewModels.Screens
{
    public class FullPartsViewModel
    {
        private readonly IFullPartsRepository _partsRepository;
        public List<string> VisibleColumns { get; set; }
        public List<(string Column, string Operator, string SearchText)> Filters { get; set; }

        public FullPartsViewModel(IFullPartsRepository partsRepository)
        {
            _partsRepository = partsRepository ?? throw new ArgumentNullException(nameof(partsRepository));
            VisibleColumns = new List<string>
            {
                "PartID",
                "PartName",
                "CatalogNumber",
                "RemainingQuantity",
                "IsAvailable",
                "StockName",
                "IsPlacedInStock",
                "ShelfNumber",
                "CarBrandName",
                "ManufacturerName",
                "QualityName",
                "Characteristics"
            };
            System.Diagnostics.Debug.WriteLine($"FullPartsViewModel Constructor: Initial Filters = {Filters?.Count ?? 0}");
        }

        public List<FullParts> LoadParts()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"LoadParts: Filters count = {Filters?.Count ?? 0}, Sample: {string.Join(", ", Filters?.Select(f => $"{f.Column}:{f.Operator}:{f.SearchText}") ?? new string[] { "null" })}");
                var parts = _partsRepository.GetFullParts();
                if (Filters != null && Filters.Any())
                {
                    parts = ApplyFilters(parts, Filters);
                }
                return parts;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadParts Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при загрузке данных деталей.", ex);
            }
        }

        private List<FullParts> ApplyFilters(List<FullParts> parts, List<(string Column, string Operator, string SearchText)> filters)
        {
            var filteredParts = parts;
            foreach (var filter in filters)
            {
                try
                {
                    filteredParts = FilterParts(filteredParts, filter.Column, filter.Operator, filter.SearchText);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ApplyFilters Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    throw new Exception($"Ошибка при применении фильтра на столбец {filter.Column}: {ex.Message}", ex);
                }
            }
            return filteredParts;
        }

        private List<FullParts> FilterParts(List<FullParts> parts, string column, string operation, string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return parts;

                switch (column)
                {
                    case "PartID":
                        if (int.TryParse(searchText, out int partId))
                        {
                            parts = ApplyNumericFilter(parts, p => p.PartID, operation, partId);
                        }
                        break;
                    case "PartName":
                        parts = ApplyStringFilter(parts, p => p.PartName, operation, searchText);
                        break;
                    case "CatalogNumber":
                        parts = ApplyStringFilter(parts, p => p.CatalogNumber, operation, searchText);
                        break;
                    case "RemainingQuantity":
                        if (int.TryParse(searchText, out int quantity))
                        {
                            parts = ApplyNumericFilter(parts, p => p.RemainingQuantity, operation, quantity);
                        }
                        break;
                    case "IsAvailable":
                        if (bool.TryParse(searchText, out bool isAvailable) || searchText.ToLower() == "в наличии" || searchText.ToLower() == "нет в наличии")
                        {
                            bool target = searchText.ToLower() == "в наличии" ? true : searchText.ToLower() == "нет в наличии" ? false : isAvailable;
                            parts = ApplyBooleanFilter(parts, p => p.IsAvailable, operation, target);
                        }
                        break;
                    case "StockName":
                        parts = ApplyStringFilter(parts, p => p.StockName, operation, searchText);
                        break;
                    case "IsPlacedInStock":
                        if (bool.TryParse(searchText, out bool isPlaced) || searchText.ToLower() == "размещено" || searchText.ToLower() == "не размещено")
                        {
                            bool target = searchText.ToLower() == "размещено" ? true : searchText.ToLower() == "не размещено" ? false : isPlaced;
                            parts = ApplyBooleanFilter(parts, p => p.IsPlacedInStock, operation, target);
                        }
                        break;
                    case "ShelfNumber":
                        parts = ApplyStringFilter(parts, p => p.ShelfNumber, operation, searchText);
                        break;
                    case "CarBrandName":
                        parts = ApplyStringFilter(parts, p => p.CarBrandName, operation, searchText);
                        break;
                    case "ManufacturerName":
                        parts = ApplyStringFilter(parts, p => p.ManufacturerName, operation, searchText);
                        break;
                    case "QualityName":
                        parts = ApplyStringFilter(parts, p => p.QualityName, operation, searchText);
                        break;
                    case "Characteristics":
                        parts = ApplyStringFilter(parts, p => p.Characteristics, operation, searchText);
                        break;
                    case "PhotoPath":
                        parts = ApplyStringFilter(parts, p => p.PhotoPath, operation, searchText);
                        break;
                    case "IncomeQuantity":
                        if (int.TryParse(searchText, out int incomeQuantity))
                        {
                            parts = ApplyNumericFilter(parts, p => p.IncomeQuantity ?? 0, operation, incomeQuantity);
                        }
                        break;
                    case "IncomeUnitPrice":
                        if (decimal.TryParse(searchText, out decimal unitPrice))
                        {
                            parts = ApplyNumericFilter(parts, p => p.IncomeUnitPrice ?? 0, operation, unitPrice);
                        }
                        break;
                    case "Markup":
                        if (decimal.TryParse(searchText, out decimal markup))
                        {
                            parts = ApplyNumericFilter(parts, p => p.Markup ?? 0, operation, markup);
                        }
                        break;
                    case "IncomeDate":
                        if (DateTime.TryParse(searchText, out DateTime incomeDate))
                        {
                            parts = ApplyDateFilter(parts, p => p.IncomeDate ?? DateTime.MinValue, operation, incomeDate);
                        }
                        break;
                    case "IncomeInvoiceNumber":
                        parts = ApplyStringFilter(parts, p => p.IncomeInvoiceNumber, operation, searchText);
                        break;
                    case "IncomePaidAmount":
                        if (decimal.TryParse(searchText, out decimal paidAmount))
                        {
                            parts = ApplyNumericFilter(parts, p => p.IncomePaidAmount ?? 0, operation, paidAmount);
                        }
                        break;
                    case "SupplierName":
                        parts = ApplyStringFilter(parts, p => p.SupplierName, operation, searchText);
                        break;
                    case "BatchName":
                        parts = ApplyStringFilter(parts, p => p.BatchName, operation, searchText);
                        break;
                    case "FinanceStatusName":
                        parts = ApplyStringFilter(parts, p => p.FinanceStatusName, operation, searchText);
                        break;
                    case "IncomeStatusName":
                        parts = ApplyStringFilter(parts, p => p.IncomeStatusName, operation, searchText);
                        break;
                    case "AttributeName":
                        parts = ApplyStringFilter(parts, p => p.AttributeName, operation, searchText);
                        break;
                    case "AttributeValue":
                        parts = ApplyStringFilter(parts, p => p.AttributeValue, operation, searchText);
                        break;
                }
                return parts;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FilterParts Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception($"Ошибка фильтрации по столбцу {column}.", ex);
            }
        }

        private List<FullParts> ApplyStringFilter(List<FullParts> parts, Func<FullParts, string> selector, string operation, string searchText)
        {
            if (operation == "LIKE")
            {
                return parts.Where(p => selector(p)?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            return parts;
        }

        private List<FullParts> ApplyNumericFilter<T>(List<FullParts> parts, Func<FullParts, T> selector, string operation, T value) where T : IComparable<T>
        {
            return operation switch
            {
                "=" => parts.Where(p => selector(p).CompareTo(value) == 0).ToList(),
                ">" => parts.Where(p => selector(p).CompareTo(value) > 0).ToList(),
                "<" => parts.Where(p => selector(p).CompareTo(value) < 0).ToList(),
                ">=" => parts.Where(p => selector(p).CompareTo(value) >= 0).ToList(),
                "<=" => parts.Where(p => selector(p).CompareTo(value) <= 0).ToList(),
                "!=" => parts.Where(p => selector(p).CompareTo(value) != 0).ToList(),
                _ => parts
            };
        }

        private List<FullParts> ApplyBooleanFilter(List<FullParts> parts, Func<FullParts, bool> selector, string operation, bool value)
        {
            return operation switch
            {
                "=" => parts.Where(p => selector(p) == value).ToList(),
                "!=" => parts.Where(p => selector(p) != value).ToList(),
                _ => parts
            };
        }

        private List<FullParts> ApplyDateFilter(List<FullParts> parts, Func<FullParts, DateTime> selector, string operation, DateTime value)
        {
            return operation switch
            {
                "=" => parts.Where(p => selector(p).Date == value.Date).ToList(),
                ">" => parts.Where(p => selector(p).Date > value.Date).ToList(),
                "<" => parts.Where(p => selector(p).Date < value.Date).ToList(),
                ">=" => parts.Where(p => selector(p).Date >= value.Date).ToList(),
                "<=" => parts.Where(p => selector(p).Date <= value.Date).ToList(),
                "!=" => parts.Where(p => selector(p).Date != value.Date).ToList(),
                _ => parts
            };
        }

        public List<FullParts> SearchParts(string searchText, List<string> visibleColumns)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return LoadParts();

                var parts = LoadParts();
                return parts.Where(p =>
                    visibleColumns.Any(col =>
                    {
                        var value = GetPropertyValue(p, col)?.ToString();
                        return value != null && value.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    })).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchParts Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при поиске деталей.", ex);
            }
        }

        private object GetPropertyValue(FullParts part, string propertyName)
        {
            try
            {
                return typeof(FullParts).GetProperty(propertyName)?.GetValue(part);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetPropertyValue Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return null;
            }
        }

        public void ExportToExcel(List<FullParts> parts, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Детали");
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "PartID", "ID" },
                        { "PartName", "Название детали" },
                        { "CatalogNumber", "Каталожный номер" },
                        { "RemainingQuantity", "Остаток" },
                        { "IsAvailable", "В наличии" },
                        { "StockName", "Склад" },
                        { "IsPlacedInStock", "Размещено" },
                        { "ShelfNumber", "Номер полки" },
                        { "CarBrandName", "Марка" },
                        { "ManufacturerName", "Производитель" },
                        { "QualityName", "Качество" },
                        { "Characteristics", "Характеристики" },
                        { "PhotoPath", "Путь к фото" },
                        { "IncomeQuantity", "Количество в приходе" },
                        { "IncomeUnitPrice", "Цена за единицу" },
                        { "Markup", "Наценка" },
                        { "IncomeDate", "Дата прихода" },
                        { "IncomeInvoiceNumber", "Номер счета" },
                        { "IncomePaidAmount", "Оплаченная сумма" },
                        { "SupplierName", "Поставщик" },
                        { "BatchName", "Партия" },
                        { "FinanceStatusName", "Финансовый статус" },
                        { "IncomeStatusName", "Статус прихода" },
                        { "AttributeName", "Имя атрибута" },
                        { "AttributeValue", "Значение атрибута" }
                    };

                    int colIndex = 1;
                    var visibleColumnNames = columnVisibility.Where(c => c.Value).Select(c => c.Key).ToList();
                    foreach (var column in visibleColumnNames)
                    {
                        worksheet.Cell(1, colIndex).Value = columnMapping[column];
                        colIndex++;
                    }

                    for (int i = 0; i < parts.Count; i++)
                    {
                        colIndex = 1;
                        foreach (var column in visibleColumnNames)
                        {
                            var value = GetPropertyValue(parts[i], column);
                            if (column == "IsAvailable")
                                worksheet.Cell(i + 2, colIndex).Value = (bool)value ? "В наличии" : "Нет в наличии";
                            else if (column == "IsPlacedInStock")
                                worksheet.Cell(i + 2, colIndex).Value = (bool)value ? "Размещено" : "Не размещено";
                            else
                                worksheet.Cell(i + 2, colIndex).Value = value?.ToString();
                            colIndex++;
                        }
                    }

                    worksheet.Columns().AdjustToContents();
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
using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Interfaces.UserInterface;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvtoServis.ViewModels.Screens
{
    public class PartsIncomeViewModel
    {
        private readonly IPartsIncomeRepository _partsIncomeRepository;
        private readonly IPartsRepository _partsRepository;
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly IStatusRepository _statusesRepository;
        private readonly IFinance_StatusRepository _finance_StatusRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IBatchRepository _batchRepository;

        public List<(string Column, string SearchText)> Filters { get; set; }
        public List<string> VisibleColumns { get; set; }

        public PartsIncomeViewModel(
            IPartsIncomeRepository partsIncomeRepository,
            IPartsRepository partsRepository,
            ISuppliersRepository suppliersRepository,
            IStatusRepository statusesRepository,
            IFinance_StatusRepository finance_StatusRepository,
            IStockRepository stockRepository,
            IBatchRepository batchRepository)
        {
            _partsIncomeRepository = partsIncomeRepository ?? throw new ArgumentNullException(nameof(partsIncomeRepository));
            _partsRepository = partsRepository ?? throw new ArgumentNullException(nameof(partsRepository));
            _suppliersRepository = suppliersRepository ?? throw new ArgumentNullException(nameof(suppliersRepository));
            _statusesRepository = statusesRepository ?? throw new ArgumentNullException(nameof(statusesRepository));
            _finance_StatusRepository = finance_StatusRepository ?? throw new ArgumentNullException(nameof(finance_StatusRepository));
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            _batchRepository = batchRepository ?? throw new ArgumentNullException(nameof(batchRepository));

            Filters = new List<(string Column, string SearchText)>();
            VisibleColumns = new List<string>
            {
                "IncomeID",
                "PartName",
                "SupplierName",
                "Date",
                "Quantity",
                "UnitPrice",
                "Markup",
                "StatusName",
                "FinanceStatusName",
                "OperationID",
                "StockName",
                "InvoiceNumber",
                "UserFullName",
                "PaidAmount",
                "BatchName"
            };
        }

        public List<PartsIncome> LoadPartsIncomes()
        {
            try
            {
                var partsIncomes = _partsIncomeRepository.GetAll();
                var partsDict = _partsRepository.GetAll().ToDictionary(p => p.PartID, p => p.PartName);
                var suppliersDict = _suppliersRepository.GetAll().ToDictionary(s => s.SupplierID, s => s.Name);
                var statusesDict = _statusesRepository.GetAll("IncomeStatuses").ToDictionary(s => s.StatusID, s => s.Name);
                var financeStatusesDict = _finance_StatusRepository.GetAll().ToDictionary(f => f.Id, f => f.Name);
                var stocksDict = _stockRepository.GetAll().ToDictionary(s => s.StockID, s => s.Name);
                var usersDict = _batchRepository.GetAllUsers().ToDictionary(u => u.UserID, u => u.FullName);
                var batchesDict = _batchRepository.GetAll().ToDictionary(b => b.BatchID, b => b.Name);

                var result = partsIncomes.Select(p => new PartsIncome
                {
                    IncomeID = p.IncomeID,
                    PartID = p.PartID,
                    PartName = partsDict.ContainsKey(p.PartID) ? partsDict[p.PartID] : "Неизвестно",
                    SupplierID = p.SupplierID,
                    SupplierName = suppliersDict.ContainsKey(p.SupplierID) ? suppliersDict[p.SupplierID] : "Неизвестно",
                    Date = p.Date,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice,
                    Markup = p.Markup,
                    StatusID = p.StatusID,
                    StatusName = statusesDict.ContainsKey(p.StatusID) ? statusesDict[p.StatusID] : "Неизвестно",
                    Finance_Status_Id = p.Finance_Status_Id,
                    Finance_Status_Name = financeStatusesDict.ContainsKey(p.Finance_Status_Id) ? financeStatusesDict[p.Finance_Status_Id] : "Неизвестно",
                    OperationID = p.OperationID,
                    StockID = p.StockID,
                    StockName = stocksDict.ContainsKey(p.StockID) ? stocksDict[p.StockID] : "Неизвестно",
                    InvoiceNumber = p.InvoiceNumber,
                    UserID = p.UserID,
                    UserFullName = usersDict.ContainsKey(p.UserID) ? usersDict[p.UserID] : "Неизвестно",
                    PaidAmount = p.PaidAmount,
                    BatchID = p.BatchID,
                    BatchName = batchesDict.ContainsKey(p.BatchID) ? batchesDict[p.BatchID] : "Неизвестно"
                }).ToList();

                if (Filters != null && Filters.Any())
                {
                    result = ApplyFilters(result);
                }

                System.Diagnostics.Debug.WriteLine($"LoadPartsIncomes: Loaded {result.Count} PartsIncomes.");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadPartsIncomes Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public List<PartsIncome> SearchPartsIncomes(string searchText, List<string> visibleColumns)
        {
            try
            {
                var partsIncomes = LoadPartsIncomes();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return partsIncomes;
                }

                searchText = searchText.ToLower().Trim();
                var filteredPartsIncomes = partsIncomes.Where(p =>
                {
                    bool matches = false;
                    if (visibleColumns.Contains("IncomeID"))
                        matches |= p.IncomeID.ToString().Contains(searchText);
                    if (visibleColumns.Contains("PartName"))
                        matches |= p.PartName?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("SupplierName"))
                        matches |= p.SupplierName?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("Date"))
                        matches |= p.Date.ToString("yyyy-MM-dd").Contains(searchText);
                    if (visibleColumns.Contains("Quantity"))
                        matches |= p.Quantity.ToString().Contains(searchText);
                    if (visibleColumns.Contains("UnitPrice"))
                        matches |= p.UnitPrice.ToString().Contains(searchText);
                    if (visibleColumns.Contains("Markup"))
                        matches |= p.Markup.ToString().Contains(searchText);
                    if (visibleColumns.Contains("StatusName"))
                        matches |= p.StatusName?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("FinanceStatusName"))
                        matches |= p.Finance_Status_Name?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("OperationID"))
                        matches |= p.OperationID.ToString().Contains(searchText);
                    if (visibleColumns.Contains("StockName"))
                        matches |= p.StockName?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("InvoiceNumber"))
                        matches |= p.InvoiceNumber?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("UserFullName"))
                        matches |= p.UserFullName?.ToLower().Contains(searchText) == true;
                    if (visibleColumns.Contains("PaidAmount"))
                        matches |= p.PaidAmount.ToString().Contains(searchText);
                    if (visibleColumns.Contains("BatchName"))
                        matches |= p.BatchName?.ToLower().Contains(searchText) == true;
                    return matches;
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"SearchPartsIncomes: '{searchText}' so'rovi uchun {filteredPartsIncomes.Count} ta qism topildi.");
                return filteredPartsIncomes;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchPartsIncomes Xatosi: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        private List<PartsIncome> ApplyFilters(IEnumerable<PartsIncome> partsIncomes)
        {
            var filteredPartsIncomes = partsIncomes.ToList();
            foreach (var filter in Filters)
            {
                var searchText = filter.SearchText.ToLower().Trim();
                switch (filter.Column)
                {
                    case "IncomeID":
                        if (int.TryParse(searchText, out int incomeId))
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.IncomeID == incomeId).ToList();
                        }
                        else
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.IncomeID.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "PartName":
                        filteredPartsIncomes = filteredPartsIncomes.Where(p => p.PartName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "SupplierName":
                        filteredPartsIncomes = filteredPartsIncomes.Where(p => p.SupplierName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "Date":
                        if (DateTime.TryParse(searchText, out DateTime date))
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.Date.Date == date.Date).ToList();
                        }
                        else
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.Date.ToString("yyyy-MM-dd").Contains(searchText)).ToList();
                        }
                        break;
                    case "Quantity":
                        if (int.TryParse(searchText, out int quantity))
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.Quantity == quantity).ToList();
                        }
                        else
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.Quantity.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "UnitPrice":
                        if (decimal.TryParse(searchText, out decimal unitPrice))
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.UnitPrice == unitPrice).ToList();
                        }
                        else
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.UnitPrice.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "Markup":
                        if (decimal.TryParse(searchText, out decimal markup))
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.Markup == markup).ToList();
                        }
                        else
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.Markup.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "StatusName":
                        filteredPartsIncomes = filteredPartsIncomes.Where(p => p.StatusName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "FinanceStatusName":
                        filteredPartsIncomes = filteredPartsIncomes.Where(p => p.Finance_Status_Name?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "OperationID":
                        if (int.TryParse(searchText, out int operationId))
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.OperationID == operationId).ToList();
                        }
                        else
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.OperationID.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "StockName":
                        filteredPartsIncomes = filteredPartsIncomes.Where(p => p.StockName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "InvoiceNumber":
                        filteredPartsIncomes = filteredPartsIncomes.Where(p => p.InvoiceNumber?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "UserFullName":
                        filteredPartsIncomes = filteredPartsIncomes.Where(p => p.UserFullName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "PaidAmount":
                        if (decimal.TryParse(searchText, out decimal paidAmount))
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.PaidAmount == paidAmount).ToList();
                        }
                        else
                        {
                            filteredPartsIncomes = filteredPartsIncomes.Where(p => p.PaidAmount.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "BatchName":
                        filteredPartsIncomes = filteredPartsIncomes.Where(p => p.BatchName?.ToLower().Contains(searchText) == true).ToList();
                        break;
                }
            }
            return filteredPartsIncomes;
        }

        public void AddPartsIncome(PartsIncome partsIncome)
        {
            try
            {
                _partsIncomeRepository.Add(partsIncome);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddPartsIncome Xatosi: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void UpdatePartsIncome(PartsIncome partsIncome)
        {
            try
            {
                _partsIncomeRepository.Update(partsIncome);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdatePartsIncome Xatosi: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void DeletePartsIncome(int id)
        {
            try
            {
                _partsIncomeRepository.Delete(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeletePartsIncome Xatosi: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void ExportToExcel(List<PartsIncome> partsIncomes, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PartsIncome");
                    var headers = new List<string>();
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "IncomeID", "ID поступления" },
                        { "PartName", "Название детали" },
                        { "SupplierName", "Поставщик" },
                        { "Date", "Дата" },
                        { "Quantity", "Количество" },
                        { "UnitPrice", "Цена за единицу" },
                        { "Markup", "Наценка" },
                        { "StatusName", "Статус" },
                        { "FinanceStatusName", "Фин. статус" },
                        { "OperationID", "ID операции" },
                        { "StockName", "Склад" },
                        { "InvoiceNumber", "Номер счета" },
                        { "UserFullName", "Пользователь" },
                        { "PaidAmount", "Оплаченная сумма" },
                        { "BatchName", "Партия" }
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

                    for (int i = 0; i < partsIncomes.Count; i++)
                    {
                        colIndex = 1;
                        if (columnVisibility["IncomeID"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].IncomeID;
                            colIndex++;
                        }
                        if (columnVisibility["PartName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].PartName;
                            colIndex++;
                        }
                        if (columnVisibility["SupplierName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].SupplierName;
                            colIndex++;
                        }
                        if (columnVisibility["Date"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].Date.ToString("yyyy-MM-dd");
                            colIndex++;
                        }
                        if (columnVisibility["Quantity"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].Quantity;
                            colIndex++;
                        }
                        if (columnVisibility["UnitPrice"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].UnitPrice;
                            colIndex++;
                        }
                        if (columnVisibility["Markup"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].Markup;
                            colIndex++;
                        }
                        if (columnVisibility["StatusName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].StatusName;
                            colIndex++;
                        }
                        if (columnVisibility["FinanceStatusName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].Finance_Status_Name;
                            colIndex++;
                        }
                        if (columnVisibility["OperationID"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].OperationID;
                            colIndex++;
                        }
                        if (columnVisibility["StockName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].StockName;
                            colIndex++;
                        }
                        if (columnVisibility["InvoiceNumber"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].InvoiceNumber;
                            colIndex++;
                        }
                        if (columnVisibility["UserFullName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].UserFullName;
                            colIndex++;
                        }
                        if (columnVisibility["PaidAmount"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].PaidAmount;
                            colIndex++;
                        }
                        if (columnVisibility["BatchName"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = partsIncomes[i].BatchName;
                            colIndex++;
                        }
                    }

                    var dataRange = worksheet.Range(2, 1, partsIncomes.Count + 1, colIndex - 1);
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
                System.Diagnostics.Debug.WriteLine($"ExportToExcel Xatosi: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
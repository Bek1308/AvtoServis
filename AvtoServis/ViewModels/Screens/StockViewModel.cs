using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AvtoServis.ViewModels.Screens
{
    public class StockViewModel
    {
        private readonly IStockRepository _stockRepository;
        public List<(string Column, string SearchText)> Filters { get; set; }
        public List<string> VisibleColumns { get; set; }
        public event EventHandler StocksChanged; // Yangi hodisa qo'shildi

        public StockViewModel(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            Filters = new List<(string Column, string SearchText)>();
            VisibleColumns = new List<string> { "StockID", "Name" };
        }

        public List<Stock> LoadStocks()
        {
            try
            {
                var stocks = _stockRepository.GetAll();
                if (Filters != null && Filters.Any())
                {
                    stocks = ApplyFilters(stocks);
                }
                StocksChanged?.Invoke(this, EventArgs.Empty); // Ma'lumotlar yangilanganda hodisani chaqirish
                return stocks.ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadStocks Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при загрузке складов.", ex);
            }
        }

        public List<Stock> SearchStocks(string searchText)
        {
            try
            {
                var stocks = LoadStocks();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return stocks;
                }

                searchText = searchText.ToLower().Trim();
                var filteredStocks = stocks.Where(stock =>
                    (VisibleColumns.Contains("StockID") && stock.StockID.ToString().Contains(searchText)) ||
                    (VisibleColumns.Contains("Name") && stock.Name?.ToLower().Contains(searchText) == true)
                ).ToList();

                StocksChanged?.Invoke(this, EventArgs.Empty); // Qidiruv natijasida hodisani chaqirish
                return filteredStocks;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchStocks Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при поиске складов.", ex);
            }
        }

        private List<Stock> ApplyFilters(IEnumerable<Stock> stocks)
        {
            var filteredStocks = stocks.ToList();
            foreach (var filter in Filters)
            {
                var searchText = filter.SearchText.ToLower().Trim();
                switch (filter.Column)
                {
                    case "StockID":
                        if (int.TryParse(searchText, out int id))
                        {
                            filteredStocks = filteredStocks.Where(s => s.StockID == id).ToList();
                        }
                        else
                        {
                            filteredStocks = filteredStocks.Where(s => s.StockID.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "Name":
                        filteredStocks = filteredStocks.Where(s => s.Name?.ToLower().Contains(searchText) == true).ToList();
                        break;
                }
            }
            return filteredStocks;
        }

        public void AddStock(Stock stock)
        {
            try
            {
                ValidateStock(stock);
                _stockRepository.Add(stock);
                StocksChanged?.Invoke(this, EventArgs.Empty); // Ma'lumotlar o'zgarganda hodisani chaqirish
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddStock Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при добавлении склада.", ex);
            }
        }

        public void UpdateStock(Stock stock)
        {
            try
            {
                ValidateStock(stock);
                _stockRepository.Update(stock);
                StocksChanged?.Invoke(this, EventArgs.Empty); // Ma'lumotlar o'zgarganda hodisani chaqirish
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateStock Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при обновлении склада.", ex);
            }
        }

        public void DeleteStock(int id)
        {
            try
            {
                _stockRepository.Delete(id);
                StocksChanged?.Invoke(this, EventArgs.Empty); // Ma'lumotlar o'zgarganda hodisani chaqirish
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteStock Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при удалении склада.", ex);
            }
        }

        private void ValidateStock(Stock stock)
        {
            if (stock == null)
            {
                throw new ArgumentNullException(nameof(stock), "Склад не может быть null.");
            }

            if (string.IsNullOrWhiteSpace(stock.Name))
            {
                throw new ArgumentException("Название склада не может быть пустым.", nameof(stock.Name));
            }

            if (stock.Name.Length > 100)
            {
                throw new ArgumentException("Название склада не должно превышать 100 символов.", nameof(stock.Name));
            }

            if (!Regex.IsMatch(stock.Name, @"^[a-zA-Z0-9\s]+$"))
            {
                throw new ArgumentException("Название склада должно содержать только латинские буквы, цифры и пробелы.", nameof(stock.Name));
            }
        }

        public void ExportToExcel(List<Stock> stocks, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Stocks");
                    int colIndex = 1;

                    if (columnVisibility["StockID"])
                    {
                        worksheet.Cell(1, colIndex).Value = "ID";
                        colIndex++;
                    }
                    if (columnVisibility["Name"])
                    {
                        worksheet.Cell(1, colIndex).Value = "Название";
                        colIndex++;
                    }

                    var headerRange = worksheet.Range(1, 1, 1, colIndex - 1);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(240, 243, 245);
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                    int rowIndex = 2;
                    foreach (var stock in stocks)
                    {
                        colIndex = 1;
                        if (columnVisibility["StockID"])
                        {
                            worksheet.Cell(rowIndex, colIndex).Value = stock.StockID;
                            colIndex++;
                        }
                        if (columnVisibility["Name"])
                        {
                            worksheet.Cell(rowIndex, colIndex).Value = stock.Name;
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
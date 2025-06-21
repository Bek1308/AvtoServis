using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AvtoServis.ViewModels.Screens
{
    public class StockViewModel
    {
        private readonly IStockRepository _stockRepository;

        public StockViewModel(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
 
        }

        public List<Stock> LoadStock()
        {
            try
            {
                return _stockRepository.GetAll();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadStock Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при загрузке данных складов.", ex);
            }
        }

        public List<Stock> SearchStock(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return LoadStock();

                return _stockRepository.Search(searchText);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchStock Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при поиске складов.", ex);
            }
        }

        public void AddStock(Stock stock)
        {
            try
            {
                _stockRepository.Add(stock);
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
                _stockRepository.Update(stock);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateStock Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при обновлении склада.", ex);
            }
        }

        public void DeleteStock(int stockId)
        {
            try
            {
                _stockRepository.Delete(stockId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteStock Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при удалении склада.", ex);
            }
        }

        public void ExportToExcel(List<Stock> stocks, string filePath)
        {
            try
            {
                using (var package = new XLWorkbook())
                {
                    var worksheet = package.Worksheets.Add("Stocks");
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Название склада";
                    for (int i = 0; i < stocks.Count; i++)
                    {
                        var stock = stocks[i];
                        worksheet.Cell(i + 2, 1).Value = stock.StockID;
                        worksheet.Cell(i + 2, 2).Value = stock.Name;
                    }
                    worksheet.Columns().AdjustToContents();
                    package.SaveAs(filePath);

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
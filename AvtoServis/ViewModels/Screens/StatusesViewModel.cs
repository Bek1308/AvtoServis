using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AvtoServis.ViewModels.Screens
{
    public class StatusesViewModel
    {
        private readonly IStatusRepository _statusRepository;
        private readonly Dictionary<string, string> _tableDisplayNames;

        public StatusesViewModel(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository ?? throw new ArgumentNullException(nameof(statusRepository));
            _tableDisplayNames = new Dictionary<string, string>
            {
                { "ExpenseStatuses", "Статусы расходов" },
                { "IncomeStatuses", "Статусы доходов" },
                { "OperationStatuses", "Статусы операций" },
                { "OrderStatuses", "Статусы заказов" }
            };
        }

        public Dictionary<string, string> GetTableDisplayNames()
        {
            return _tableDisplayNames;
        }

        public List<Status> LoadStatuses(string tableName)
        {
            try
            {
                return _statusRepository.GetAll(tableName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadStatuses Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception($"Ошибка при загрузке данных из таблицы {tableName}.", ex);
            }
        }

        public List<Status> SearchStatuses(string tableName, string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return LoadStatuses(tableName);

                return _statusRepository.Search(tableName, searchText);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchStatuses Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception($"Ошибка при поиске в таблице {tableName}.", ex);
            }
        }

        public void AddStatus(string tableName, Status status)
        {
            try
            {
                _statusRepository.Add(tableName, status);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddStatus Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception($"Ошибка при добавлении статуса в таблицу {tableName}.", ex);
            }
        }

        public void UpdateStatus(string tableName, Status status)
        {
            try
            {
                _statusRepository.Update(tableName, status);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateStatus Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception($"Ошибка при обновлении статуса в таблице {tableName}.", ex);
            }
        }

        public void DeleteStatus(string tableName, int statusId)
        {
            try
            {
                _statusRepository.Delete(tableName, statusId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteStatus Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception($"Ошибка при удалении статуса из таблицы {tableName}.", ex);
            }
        }

        public void ExportToExcel(List<Status> statuses, string filePath, string tableName)
        {
            try
            {
                using (var package = new XLWorkbook())
                {
                    var worksheet = package.Worksheets.Add(_tableDisplayNames[tableName]);
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Название";
                    worksheet.Cell(1, 3).Value = "Описание";
                    worksheet.Cell(1, 4).Value = "Цвет";
                    for (int i = 0; i < statuses.Count; i++)
                    {
                        var status = statuses[i];
                        worksheet.Cell(i + 2, 1).Value = status.StatusID;
                        worksheet.Cell(i + 2, 2).Value = status.Name;
                        worksheet.Cell(i + 2, 3).Value = status.Description;
                        worksheet.Cell(i + 2, 4).Value = status.Color;
                    }
                    worksheet.Columns().AdjustToContents();
                    package.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExportToExcel Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception($"Ошибка при экспорте данных из таблицы {tableName}.", ex);
            }
        }
    }
}
using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System.Text.RegularExpressions;

namespace AvtoServis.ViewModels.Screens
{
    public class StatusesViewModel
    {
        private readonly IStatusRepository _statusRepository;
        public List<(string Column, string SearchText)> Filters { get; set; }
        public List<string> VisibleColumns { get; set; }
        public string SelectedTable { get; set; }

        public StatusesViewModel(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository ?? throw new ArgumentNullException(nameof(statusRepository));
            Filters = new List<(string Column, string SearchText)>();
            VisibleColumns = new List<string> { "StatusID", "Name", "Description", "Color" };
            SelectedTable = "OrderStatuses"; // Default table
        }

        public List<Status> LoadStatuses()
        {
            try
            {
                var statuses = _statusRepository.GetAll(SelectedTable);
                if (Filters != null && Filters.Any())
                {
                    statuses = ApplyFilters(statuses);
                }
                return statuses.ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadStatuses Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при загрузке статусов.", ex);
            }
        }

        public List<Status> SearchStatuses(string searchText)
        {
            try
            {
                var statuses = LoadStatuses();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return statuses;
                }

                searchText = searchText.ToLower().Trim();
                var filteredStatuses = statuses.Where(status =>
                    (VisibleColumns.Contains("StatusID") && status.StatusID.ToString().Contains(searchText)) ||
                    (VisibleColumns.Contains("Name") && status.Name?.ToLower().Contains(searchText) == true) ||
                    (VisibleColumns.Contains("Description") && status.Description?.ToLower().Contains(searchText) == true) ||
                    (VisibleColumns.Contains("Color") && status.Color?.ToLower().Contains(searchText) == true)
                ).ToList();

                return filteredStatuses;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchStatuses Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при поиске статусов.", ex);
            }
        }

        private List<Status> ApplyFilters(IEnumerable<Status> statuses)
        {
            var filteredStatuses = statuses.ToList();
            foreach (var filter in Filters)
            {
                var searchText = filter.SearchText.ToLower().Trim();
                switch (filter.Column)
                {
                    case "StatusID":
                        if (int.TryParse(searchText, out int id))
                        {
                            filteredStatuses = filteredStatuses.Where(s => s.StatusID == id).ToList();
                        }
                        else
                        {
                            filteredStatuses = filteredStatuses.Where(s => s.StatusID.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "Name":
                        filteredStatuses = filteredStatuses.Where(s => s.Name?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "Description":
                        filteredStatuses = filteredStatuses.Where(s => s.Description?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "Color":
                        filteredStatuses = filteredStatuses.Where(s => s.Color?.ToLower().Contains(searchText) == true).ToList();
                        break;
                }
            }
            return filteredStatuses;
        }

        public void AddStatus(Status status)
        {
            try
            {
                ValidateStatus(status);
                _statusRepository.Add(SelectedTable, status);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddStatus Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при добавлении статуса.", ex);
            }
        }

        public void UpdateStatus(Status status)
        {
            try
            {
                ValidateStatus(status);
                _statusRepository.Update(SelectedTable, status);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateStatus Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при обновлении статуса.", ex);
            }
        }

        public void DeleteStatus(int id)
        {
            try
            {
                _statusRepository.Delete(SelectedTable, id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteStatus Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при удалении статуса.", ex);
            }
        }

        private void ValidateStatus(Status status)
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status), "Статус не может быть null.");
            }

            if (string.IsNullOrWhiteSpace(status.Name))
            {
                throw new ArgumentException("Название статуса не может быть пустым.", nameof(status.Name));
            }

            if (status.Name.Length > 100)
            {
                throw new ArgumentException("Название статуса не должно превышать 100 символов.", nameof(status.Name));
            }

            if (!Regex.IsMatch(status.Name, @"^[a-zA-Z0-9\s]+$"))
            {
                throw new ArgumentException("Название статуса должно содержать только латинские буквы, цифры и пробелы.", nameof(status.Name));
            }

            if (!string.IsNullOrEmpty(status.Description) && status.Description.Length > 500)
            {
                throw new ArgumentException("Описание статуса не должно превышать 500 символов.", nameof(status.Description));
            }

            //if (!string.IsNullOrEmpty(status.Color) && !Regex.IsMatch(status.Color, @"^#[0-9A-Fa-f]{6}$"))
            //{
            //    throw new ArgumentException("Цвет должен быть в формате HEX (#RRGGBB).", nameof(status.Color));
            //}
        }

        public void ExportToExcel(List<Status> statuses, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Statuses");
                    int colIndex = 1;

                    if (columnVisibility["StatusID"])
                    {
                        worksheet.Cell(1, colIndex).Value = "ID";
                        colIndex++;
                    }
                    if (columnVisibility["Name"])
                    {
                        worksheet.Cell(1, colIndex).Value = "Название";
                        colIndex++;
                    }
                    if (columnVisibility["Description"])
                    {
                        worksheet.Cell(1, colIndex).Value = "Описание";
                        colIndex++;
                    }
                    if (columnVisibility["Color"])
                    {
                        worksheet.Cell(1, colIndex).Value = "Цвет";
                        colIndex++;
                    }

                    var headerRange = worksheet.Range(1, 1, 1, colIndex - 1);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(240, 243, 245);
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                    int rowIndex = 2;
                    foreach (var status in statuses)
                    {
                        colIndex = 1;
                        if (columnVisibility["StatusID"])
                        {
                            worksheet.Cell(rowIndex, colIndex).Value = status.StatusID;
                            colIndex++;
                        }
                        if (columnVisibility["Name"])
                        {
                            worksheet.Cell(rowIndex, colIndex).Value = status.Name;
                            colIndex++;
                        }
                        if (columnVisibility["Description"])
                        {
                            worksheet.Cell(rowIndex, colIndex).Value = status.Description;
                            colIndex++;
                        }
                        if (columnVisibility["Color"])
                        {
                            worksheet.Cell(rowIndex, colIndex).Value = status.Color;
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
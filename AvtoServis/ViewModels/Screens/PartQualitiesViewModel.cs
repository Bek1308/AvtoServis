using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AvtoServis.ViewModels.Screens
{
    public class PartQualitiesViewModel
    {
        private readonly IPartQualitiesRepository _repository;
        public List<(string Column, string SearchText)> Filters { get; set; }
        public List<string> VisibleColumns { get; set; }

        public PartQualitiesViewModel(IPartQualitiesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Filters = new List<(string Column, string SearchText)>();
            VisibleColumns = new List<string> { "QualityID", "Name" };
        }

        public List<PartQuality> LoadPartQualities()
        {
            try
            {
                var qualities = _repository.GetAll();
                if (Filters != null && Filters.Any())
                {
                    qualities = ApplyFilters(qualities);
                }
                System.Diagnostics.Debug.WriteLine($"LoadPartQualities: Loaded {qualities.Count} part qualities.");
                return qualities;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadPartQualities Error: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки качеств запчастей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<PartQuality>();
            }
        }

        public PartQuality LoadPartQuality(int id)
        {
            try
            {
                var quality = _repository.GetById(id);
                if (quality == null)
                    throw new Exception($"Качество запчасти с ID {id} не найдено.");
                return quality;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadPartQuality Error: {ex.Message}");
                throw;
            }
        }

        public void AddPartQuality(PartQuality quality)
        {
            try
            {
                ValidatePartQuality(quality);
                _repository.Add(quality);
                System.Diagnostics.Debug.WriteLine($"AddPartQuality: Added part quality '{quality.Name}' with ID {quality.QualityID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddPartQuality Error: {ex.Message}");
                throw new Exception($"Ошибка при добавлении качества запчасти: {ex.Message}", ex);
            }
        }

        public void UpdatePartQuality(PartQuality quality)
        {
            try
            {
                ValidatePartQuality(quality);
                _repository.Update(quality);
                System.Diagnostics.Debug.WriteLine($"UpdatePartQuality: Updated part quality '{quality.Name}' with ID {quality.QualityID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdatePartQuality Error: {ex.Message}");
                throw new Exception($"Ошибка при обновлении качества запчасти: {ex.Message}", ex);
            }
        }

        public void DeletePartQuality(int id)
        {
            try
            {
                _repository.Delete(id);
                System.Diagnostics.Debug.WriteLine($"DeletePartQuality: Deleted part quality with ID {id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeletePartQuality Error: {ex.Message}");
                throw new Exception($"Ошибка при удалении качества запчасти: {ex.Message}", ex);
            }
        }

        public List<PartQuality> SearchPartQualities(string searchText)
        {
            try
            {
                var qualities = LoadPartQualities();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return qualities;
                }

                searchText = searchText.ToLower().Trim();
                var filteredQualities = qualities.Where(quality =>
                    (VisibleColumns.Contains("QualityID") && quality.QualityID.ToString().Contains(searchText)) ||
                    (VisibleColumns.Contains("Name") && quality.Name?.ToLower().Contains(searchText) == true)
                ).ToList();

                System.Diagnostics.Debug.WriteLine($"SearchPartQualities: Found {filteredQualities.Count} part qualities for query '{searchText}'.");
                return filteredQualities;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchPartQualities Error: {ex.Message}");
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<PartQuality>();
            }
        }

        private List<PartQuality> ApplyFilters(IEnumerable<PartQuality> qualities)
        {
            var filteredQualities = qualities.ToList();
            foreach (var filter in Filters)
            {
                var searchText = filter.SearchText.ToLower().Trim();
                switch (filter.Column)
                {
                    case "QualityID":
                        if (int.TryParse(searchText, out int id))
                        {
                            filteredQualities = filteredQualities.Where(q => q.QualityID == id).ToList();
                        }
                        else
                        {
                            filteredQualities = filteredQualities.Where(q => q.QualityID.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "Name":
                        filteredQualities = filteredQualities.Where(q => q.Name?.ToLower().Contains(searchText) == true).ToList();
                        break;
                }
            }
            return filteredQualities;
        }

        private void ValidatePartQuality(PartQuality quality)
        {
            if (quality == null)
                throw new ArgumentNullException(nameof(quality));

            if (string.IsNullOrWhiteSpace(quality.Name))
                throw new ArgumentException("Название качества запчасти не может быть пустым.");

            if (quality.Name.Length > 100)
                throw new ArgumentException("Название качества запчасти не может превышать 100 символов.");

            if (!Regex.IsMatch(quality.Name, @"^[а-яА-Яa-zA-Z0-9\s]+$"))
                throw new ArgumentException("Название качества запчасти может содержать только буквы (русские или латинские), цифры и пробелы.");
        }

        public void ExportToExcel(List<PartQuality> qualities, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PartQualities");
                    var headers = new List<string>();
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "QualityID", "Номер" },
                        { "Name", "Название качества" }
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

                    for (int i = 0; i < qualities.Count; i++)
                    {
                        colIndex = 1;
                        if (columnVisibility["QualityID"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = qualities[i].QualityID;
                            colIndex++;
                        }
                        if (columnVisibility["Name"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = qualities[i].Name;
                            colIndex++;
                        }
                    }

                    var dataRange = worksheet.Range(2, 1, qualities.Count + 1, colIndex - 1);
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
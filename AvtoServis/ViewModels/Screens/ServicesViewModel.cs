using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvtoServis.ViewModels.Screens
{
    public class ServicesViewModel
    {
        private readonly IServicesRepository _repository;
        public List<(string Column, string SearchText)> Filters { get; set; }
        public List<string> VisibleColumns { get; set; }

        public ServicesViewModel(IServicesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Filters = new List<(string Column, string SearchText)>();
            VisibleColumns = new List<string>
            {
                "ServiceID",
                "Name",
                "Price"
            };
        }

        public List<Service> LoadServices()
        {
            try
            {
                var services = _repository.GetAll();
                if (Filters != null && Filters.Any())
                {
                    services = ApplyFilters(services);
                }
                System.Diagnostics.Debug.WriteLine($"LoadServices: Loaded {services.Count} services.");
                return services;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadServices Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public Service LoadService(int id)
        {
            try
            {
                var service = _repository.GetById(id);
                if (service == null)
                    throw new Exception($"Услуга с ID {id} не найдена.");
                return service;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadService Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void AddService(Service service)
        {
            try
            {
                _repository.Add(service);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddService Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void UpdateService(Service service)
        {
            try
            {
                _repository.Update(service);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateService Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void DeleteService(int id)
        {
            try
            {
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteService Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public List<Service> SearchServices(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return LoadServices();
                searchText = searchText.ToLower().Trim();
                var services = LoadServices();
                var filteredServices = services.Where(s =>
                {
                    bool matches = false;
                    if (VisibleColumns.Contains("ServiceID"))
                        matches |= s.ServiceID.ToString().Contains(searchText);
                    if (VisibleColumns.Contains("Name"))
                        matches |= s.Name?.ToLower().Contains(searchText) == true;
                    if (VisibleColumns.Contains("Price"))
                        matches |= s.Price.ToString().Contains(searchText);
                    return matches;
                }).ToList();
                System.Diagnostics.Debug.WriteLine($"SearchServices: Found {filteredServices.Count} services for query '{searchText}'.");
                return filteredServices;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchServices Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        private List<Service> ApplyFilters(IEnumerable<Service> services)
        {
            var filteredServices = services.ToList();
            foreach (var filter in Filters)
            {
                var searchText = filter.SearchText.ToLower().Trim();
                switch (filter.Column)
                {
                    case "ServiceID":
                        if (int.TryParse(searchText, out int id))
                        {
                            filteredServices = filteredServices.Where(s => s.ServiceID == id).ToList();
                        }
                        else
                        {
                            filteredServices = filteredServices.Where(s => s.ServiceID.ToString().Contains(searchText)).ToList();
                        }
                        break;
                    case "Name":
                        filteredServices = filteredServices.Where(s => s.Name?.ToLower().Contains(searchText) == true).ToList();
                        break;
                    case "Price":
                        filteredServices = filteredServices.Where(s => s.Price.ToString().Contains(searchText)).ToList();
                        break;
                }
            }
            return filteredServices;
        }

        public void ExportToExcel(List<Service> services, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Services");
                    var headers = new List<string>();
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "ServiceID", "Номер" },
                        { "Name", "Название услуги" },
                        { "Price", "Цена" }
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

                    for (int i = 0; i < services.Count; i++)
                    {
                        colIndex = 1;
                        if (columnVisibility["ServiceID"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = services[i].ServiceID;
                            colIndex++;
                        }
                        if (columnVisibility["Name"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = services[i].Name;
                            colIndex++;
                        }
                        if (columnVisibility["Price"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = services[i].Price;
                            worksheet.Cell(i + 2, colIndex).Style.NumberFormat.Format = "#,##0.00";
                            colIndex++;
                        }
                    }

                    var dataRange = worksheet.Range(2, 1, services.Count + 1, colIndex - 1);
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
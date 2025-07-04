using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using ClosedXML.Excel;

namespace AvtoServis.ViewModels.Screens
{
    public class SuppliersViewModel
    {
        private readonly ISuppliersRepository _suppliersRepository;

        public SuppliersViewModel(ISuppliersRepository suppliersRepository)
        {
            _suppliersRepository = suppliersRepository ?? throw new ArgumentNullException(nameof(suppliersRepository));
            VisibleColumns = new List<string> { "SupplierID", "Name", "ContactPhone", "Email", "Address" }; // Dastlabki holat
        }

        public List<string> VisibleColumns { get; set; }
        public List<(string Column, string SearchText)> Filters { get; set; }

        public List<Supplier> LoadSuppliers()
        {
            try
            {
                return _suppliersRepository.GetAll();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadSuppliers Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public Supplier GetSupplierById(int id)
        {
            try
            {
                return _suppliersRepository.GetById(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetSupplierById Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public List<Supplier> FilterSuppliers(List<(string Column, string SearchText)> filters)
        {
            try
            {
                var suppliers = _suppliersRepository.GetAll();
                foreach (var filter in filters)
                {
                    suppliers = suppliers.Where(s =>
                    {
                        switch (filter.Column)
                        {
                            case "SupplierID":
                                return s.SupplierID.ToString().Contains(filter.SearchText, StringComparison.OrdinalIgnoreCase);
                            case "Name":
                                return s.Name?.Contains(filter.SearchText, StringComparison.OrdinalIgnoreCase) ?? false;
                            case "ContactPhone":
                                return s.ContactPhone?.Contains(filter.SearchText, StringComparison.OrdinalIgnoreCase) ?? false;
                            case "Email":
                                return s.Email?.Contains(filter.SearchText, StringComparison.OrdinalIgnoreCase) ?? false;
                            case "Address":
                                return s.Address?.Contains(filter.SearchText, StringComparison.OrdinalIgnoreCase) ?? false;
                            default:
                                return false;
                        }
                    }).ToList();
                }
                return suppliers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FilterSuppliers Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public List<Supplier> SearchSuppliers(string searchText, List<string> visibleColumns)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return LoadSuppliers();

                var suppliers = _suppliersRepository.Search(searchText);
                return suppliers.Where(s =>
                {
                    bool matches = false;
                    if (visibleColumns.Contains("SupplierID"))
                        matches |= s.SupplierID.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase);
                    if (visibleColumns.Contains("Name"))
                        matches |= s.Name?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false;
                    if (visibleColumns.Contains("ContactPhone"))
                        matches |= s.ContactPhone?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false;
                    if (visibleColumns.Contains("Email"))
                        matches |= s.Email?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false;
                    if (visibleColumns.Contains("Address"))
                        matches |= s.Address?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false;
                    return matches;
                }).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchSuppliers Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void AddSupplier(Supplier supplier)
        {
            try
            {
                _suppliersRepository.Add(supplier);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddSupplier Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            try
            {
                _suppliersRepository.Update(supplier);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateSupplier Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void DeleteSupplier(int id)
        {
            try
            {
                _suppliersRepository.Delete(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteSupplier Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void ExportToExcel(List<Supplier> suppliers, string filePath, Dictionary<string, bool> columnVisibility)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Suppliers");
                    var headers = new List<string>();
                    var columnMapping = new Dictionary<string, string>
                    {
                        { "SupplierID", "ID" },
                        { "Name", "Название" },
                        { "ContactPhone", "Контактный телефон" },
                        { "Email", "Электронная почта" },
                        { "Address", "Адрес" }
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

                    for (int i = 0; i < suppliers.Count; i++)
                    {
                        colIndex = 1;
                        if (columnVisibility["SupplierID"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = suppliers[i].SupplierID;
                            colIndex++;
                        }
                        if (columnVisibility["Name"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = suppliers[i].Name;
                            colIndex++;
                        }
                        if (columnVisibility["ContactPhone"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = suppliers[i].ContactPhone;
                            colIndex++;
                        }
                        if (columnVisibility["Email"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = suppliers[i].Email;
                            colIndex++;
                        }
                        if (columnVisibility["Address"])
                        {
                            worksheet.Cell(i + 2, colIndex).Value = suppliers[i].Address;
                            colIndex++;
                        }
                    }

                    var dataRange = worksheet.Range(2, 1, suppliers.Count + 1, colIndex - 1);
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
using AvtoServis.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Model.DTOs
{
    public class FullParts
    {
        public int PartID { get; set; }
        public int? IncomeID { get; set; }
        public string PartName { get; set; }
        public string CatalogNumber { get; set; }
        public int RemainingQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public string StockName { get; set; }
        public bool IsPlacedInStock { get; set; }
        public string ShelfNumber { get; set; }
        public string CarBrandName { get; set; }
        public string ManufacturerName { get; set; }
        public string QualityName { get; set; }
        public string Characteristics { get; set; }
        public string PhotoPath { get; set; }
        public int? IncomeQuantity { get; set; }
        public decimal? IncomeUnitPrice { get; set; }
        public decimal? Markup { get; set; }
        public decimal? ProcentForMinPrice { get; set; } = 0.10m; // Masalan, 5%

        public DateTime? IncomeDate { get; set; }
        public string IncomeInvoiceNumber { get; set; }
        public decimal? IncomePaidAmount { get; set; }
        public string SupplierName { get; set; }                                    
        public string BatchName { get; set; }
        public string FinanceStatusName { get; set; }
        public string IncomeStatusName { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public List<PartAttribute> Attributes { get; set; }
    }
}

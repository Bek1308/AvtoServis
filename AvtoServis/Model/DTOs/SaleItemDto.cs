using System;

namespace AvtoServis.Model.DTOs
{
    public class SaleItemDto
    {
        public int ProductId { get; set; }
        public int? IncomeId { get; set; } // ImcomeId
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }=1;
        public decimal Total { get; set; } // Changed to include setter
        public string ManufacturerName { get; set; }
        public string CatalogNumber { get; set; }
        public string StockName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public decimal PaidAmount { get; set; }
    }
}
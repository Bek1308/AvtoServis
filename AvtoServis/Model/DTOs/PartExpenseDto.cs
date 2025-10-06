using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Model.DTOs
{
    public class PartExpenseDto
    {
        public int SaleId { get; set; }
        public string PartName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public int? PaymentStatusId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal PaidAmount { get; set; }
        public string Manufacturer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CatalogNumber { get; set; }
        public string CarBrand { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public string Seller { get; set; }
        public string InvoiceNumber { get; set; }
    }
}

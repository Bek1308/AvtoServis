using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Model.DTOs
{
    public class SaleDto
    {
        public int SaleId { get; set; }
        public List<SaleItemDto> Items { get; set; }
        public decimal PaidAmount { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Model.DTOs
{
    public class IncompleteOrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerId { get; set; } // Mijoz ID sini qo'shish
        public string CustomerPhone { get; set; } // Qo'shildi: Mijoz telefoni
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime Date { get; set; }
        public List<ServiceItemDto> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace AvtoServis.Model.DTOs
{
    public class IncompleteSaleDto
    {
        public int SaleId { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerId { get; set; } // Mijoz ID sini qo'shish
        public string CustomerPhone { get; set; } // Qo'shildi: Mijoz telefoni
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime Date { get; set; }
        public List<SaleItemDto> Items { get; set; }
    }
}
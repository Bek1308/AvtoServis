using System;
using System.Collections.Generic;

namespace AvtoServis.Model.DTOs
{
    public class ServiceOrderDto
    {
        public int OrderId { get; set; }
        public List<ServiceItemDto> Items { get; set; }
        public decimal PaidAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public int? CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public string VehicleModel { get; set; }
        public string UserName { get; set; }
        public string StatusName { get; set; }
        public int FinanceStatusID { get; set; }
        public string ServiceName { get; set; } // Qo‘shildi
        public int Quantity { get; set; } // Qo‘shildi
        public int OrderID { get { return OrderId; } set { OrderId = value; } } // Qo‘shildi
        public DateTime OrderDate { get { return Date; } set { Date = value; } } // Qo‘shildi
    }
}
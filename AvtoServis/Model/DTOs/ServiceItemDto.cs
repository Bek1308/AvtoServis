using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Model.DTOs
{
    public class ServiceItemDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
        public decimal Total { get; set; } // Changed to include setter
        public  int VehicleId { get; set; }
        public int StatusId { get; set; }
        public decimal PaidAmount { get; set; }

    }
}

using System;

namespace AvtoServis.Model.DTOs
{
    public class FullService
    {
        public int ServiceID { get; set; }
        public string Name { get; set; } 
        public decimal Price { get; set; } 
        public int SoldCount { get; set; } 
        public decimal TotalRevenue { get; set; }
    }
}

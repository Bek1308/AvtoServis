using System;

namespace AvtoServis.Model.DTOs
{
    public class TopPart
    {
        public string PartName { get; set; }
        public int Quantity { get; set; }
    }

    public class TopService
    {
        public string ServiceName { get; set; }
        public int Quantity { get; set; }
    }

    public class WeeklySale
    {
        public string WeekStart { get; set; }
        public int PartsQuantity { get; set; }
        public int ServicesQuantity { get; set; }
    }
}
namespace AvtoServis.Model.Entities
{
    public class ServiceOrder
    {
        public int OrderID { get; set; }
        public int? CustomerID { get; set; }
        public int? VehicleID { get; set; }
        public int ServiceID { get; set; }
        public int? OperationID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public int StatusID { get; set; }
        public int Quantity { get; set; }
        public decimal PaidAmount { get; set; } 
        public int FinanceStatusID { get; set; } 
        public decimal? TotalAmount { get; set; }
    }
}

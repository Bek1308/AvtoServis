using System;

namespace AvtoServis.Model.Entities
{
    public class PartExpense
    {
        public int ExpenseID { get; set; }
        public int PartID { get; set; }
        public int? IncomeID { get; set; }
        public int? ExpenseTypeID { get; set; }
        public int? CustomerID { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int? StatusID { get; set; }
        public int? SuplierID { get; set; }
        public int? OperationID { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? UserID { get; set; }
        public decimal PaidAmount { get; set; }
        public int Finance_statusId { get; set; }
    }
}
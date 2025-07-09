using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Model.Entities
{
    public class PartsIncome
    {
        public int IncomeID { get; set; }
        public int PartID { get; set; }
        public string ?PartName { get; set; }
        public int SupplierID { get; set; }
        public string ?SupplierName { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Markup { get; set; }
        public int StatusID { get; set; }
        public string? StatusName { get; set; }
        public int Finance_Status_Id { get; set; }
        public string? Finance_Status_Name { get; set; }
        public int OperationID { get; set; }
        public int StockID { get; set; }
        public string ?StockName { get; set; } 
        public string ?InvoiceNumber { get; set; }
        public int UserID { get; set; }
        public string ?UserFullName { get; set; }
        public decimal PaidAmount { get; set; }
        public int BatchID { get; set; }
        public string? BatchName { get; set; }
    }
}

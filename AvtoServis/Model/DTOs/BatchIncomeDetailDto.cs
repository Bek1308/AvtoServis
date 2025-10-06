using System;
using System.Collections.Generic;
using AvtoServis.Model.Entities; // PartsIncome va PartExpense uchun

namespace AvtoServis.Data.Models
{
    public class BatchIncomeDetailDto
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; } = string.Empty;
        public List<IncomeDetail> Incomes { get; set; } = new List<IncomeDetail>(); // Har income uchun details
    }

    public class IncomeDetail
    {
        public PartsIncome Income { get; set; } = new PartsIncome(); // Asosiy income info
        public List<PartExpense> SoldExpenses { get; set; } = new List<PartExpense>(); // Sotilgan expenses (PartExpense entity)
        public int TotalSoldQuantity { get; set; } // SUM(Expenses.Quantity)
        public decimal TotalSaleSum { get; set; } // SUM(Expenses.Quantity * UnitPrice)
        public int RemainingQuantity { get; set; } // Income.Quantity - TotalSoldQuantity
        public DateTime LastSaleDate { get; set; } // Eng oxirgi sotuv sanasi (MAX(Expense.Date))
    }
}
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Data.Interfaces
{
    public interface IPartsExpensesRepository
    {
        List<PartExpenseDto> GetAllPartExpenses();
        void Add(PartExpense entity);
        void Update(PartExpense expense, string batchName);
        public void ReturnPartExpense(PartExpense expense);
        void Delete(int expenseId, string reason);
        PartExpense GetById(int expenseId);
        List<PartExpense> GetAll();
        void SavePartsExpenses(List<PartExpense> partsExpenses);

    }
}

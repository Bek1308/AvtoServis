using AvtoServis.Data.Models;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Data.Interfaces
{
    public interface IPartsIncomeRepository
    {
        List<PartsIncome> GetAll();
        PartsIncome GetById(int id);
        PartsIncome GetByPartId(int id);
        public PartsIncome GetByIncomeId(int? id);
        void Add(PartsIncome entity);
        void UpdatePartsIncomes(PartsIncome income, string batchName);
        void DeletePartsIncome(int incomeId, string reason);
        //List<PartsIncome> Search(string searchText);
        void SavePartsIncomes(List<PartsIncome> partsIncomes, string batchName);

        public List<IncomeDto> GetAllIncomes();
        public BatchIncomeDetailDto GetBatchIncomesWithExpenses(int batchId);
    }
}

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
        void Add(PartsIncome entity);
        void Update(PartsIncome entity);
        void Delete(int id);
        //List<PartsIncome> Search(string searchText);
    }
}

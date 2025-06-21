using AvtoServis.Model.Entities;
using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface IStockRepository
    {
        List<Stock> GetAll();
        Stock GetById(int id);
        void Add(Stock entity);
        void Update(Stock entity);
        void Delete(int id);
        List<Stock> Search(string searchText);
    }
}
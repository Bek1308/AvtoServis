using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        List<T> Search(string searchText);
        List<T> FilterByPrice(decimal minPrice, decimal maxPrice);
    }
}
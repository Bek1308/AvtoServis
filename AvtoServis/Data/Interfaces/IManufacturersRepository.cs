using AvtoServis.Model.Entities;

namespace AvtoServis.Data.Interfaces
{
    public interface IManufacturersRepository
    {
        List<Manufacturer> GetAll();
        Manufacturer GetById(int id);
        void Add(Manufacturer entity);
        void Update(Manufacturer entity);
        void Delete(int id);
        List<Manufacturer> Search(string searchText);
    }
}
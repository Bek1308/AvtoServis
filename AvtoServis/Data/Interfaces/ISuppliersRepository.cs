using AvtoServis.Model.Entities;

namespace AvtoServis.Data.Interfaces
{
    public interface ISuppliersRepository
    {
        List<Supplier> GetAll();
        Supplier GetById(int id);
        void Add(Supplier entity);
        void Update(Supplier entity);
        void Delete(int id);
        List<Supplier> Search(string searchText);
    }
}
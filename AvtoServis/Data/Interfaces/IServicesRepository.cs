using AvtoServis.Model.Entities;

namespace AvtoServis.Data.Interfaces
{
    public interface IServicesRepository
    {
        List<Service> GetAll();
        Service GetById(int id);
        void Add(Service entity);
        void Update(Service entity);
        void Delete(int id);
        //List<Service> Search(string searchText);
        //List<Service> FilterByPrice(decimal minPrice, decimal maxPrice);
    }
}
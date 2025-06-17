using AvtoServis.Model.Entities;
using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface IManufacturersRepository : IRepository<Manufacturer>
    {
        List<Manufacturer> Search(string searchText);
        List<Manufacturer> FilterByPrice(decimal minPrice, decimal maxPrice);
    }
}
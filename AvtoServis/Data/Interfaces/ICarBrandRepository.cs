using AvtoServis.Model.Entities;
using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface ICarBrandRepository
    {
        List<CarBrand> GetAll();
        CarBrand GetById(int id);
        void Add(CarBrand entity);
        void Update(CarBrand entity);
        void Delete(int id);
        List<CarBrand> Search(string searchText);
    }
}
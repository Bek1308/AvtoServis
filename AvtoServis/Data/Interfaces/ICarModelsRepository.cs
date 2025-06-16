using AvtoServis.Model.Entities;
using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface ICarModelsRepository
    {
        List<CarModel> GetAll();
        CarModel GetById(int id);
        void Add(CarModel entity);
        void Update(CarModel entity);
        void Delete(int id);
        List<CarModel> SearchByModel(string searchText);
        List<CarModel> SearchByYear(int year);
        List<CarModel> FilterByBrand(int brandId);
    }
}
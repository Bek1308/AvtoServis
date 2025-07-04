using AvtoServis.Model.Entities;

namespace AvtoServis.Data.Interfaces
{
    public interface ICarModelsRepository
    {
        List<CarModel> GetAll();
        void Add(CarModel model);
        void Update(CarModel model);
        void Delete(int id);
        //List<CarModel> SearchByModel(string model);
        //List<CarModel> SearchByYear(int year);
        //List<CarModel> FilterByBrand(int brandId);
    }
}
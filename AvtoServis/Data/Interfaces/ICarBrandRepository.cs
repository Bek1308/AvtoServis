using AvtoServis.Model.Entities;

namespace AvtoServis.Data.Interfaces
{
    public interface ICarBrandRepository
    {
        List<CarBrand> GetAll();
        void Add(CarBrand brand);
        void Update(CarBrand brand);
        void Delete(int id);
        List<CarBrand> SearchByName(string name);
    }
}
using AvtoServis.Model.Entities;

namespace AvtoServis.Data.Interfaces
{
    public interface IBatchRepository
    {
        List<Batch> GetAll();
        List<User> GetAllUsers();
        Batch? GetById(int id);

    }
}

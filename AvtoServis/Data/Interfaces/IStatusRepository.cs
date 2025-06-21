using AvtoServis.Model.Entities;
using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface IStatusRepository
    {
        List<Status> GetAll(string tableName);
        Status GetById(string tableName, int id);
        void Add(string tableName, Status entity);
        void Update(string tableName, Status entity);
        void Delete(string tableName, int id);
        List<Status> Search(string tableName, string searchText);
    }
}
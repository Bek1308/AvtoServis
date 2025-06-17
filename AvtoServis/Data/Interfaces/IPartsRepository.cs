using AvtoServis.Model.Entities;
using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface IPartsRepository
    {
        List<Part> GetAll();
        Part GetById(int id);
        void Add(Part entity);
        void Update(Part entity);
        void Delete(int id);
        List<Part> Search(string searchText);
    }
}
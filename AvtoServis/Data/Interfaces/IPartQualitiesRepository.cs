using AvtoServis.Model.Entities;
using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface IPartQualitiesRepository
    {
        List<PartQuality> GetAll();
        PartQuality GetById(int id);
        void Add(PartQuality entity);
        void Update(PartQuality entity);
        void Delete(int id);
        List<PartQuality> Search(string searchText);
    }
}
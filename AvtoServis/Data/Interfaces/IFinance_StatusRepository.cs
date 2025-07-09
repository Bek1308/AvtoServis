using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Data.Interfaces
{
    public interface IFinance_StatusRepository
    {
        List<Finance_Status> GetAll();
        Finance_Status? GetById(int id);
    }
}

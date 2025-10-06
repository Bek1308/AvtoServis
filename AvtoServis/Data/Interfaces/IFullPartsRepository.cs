using AvtoServis.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Data.Interfaces
{
    public interface IFullPartsRepository
    {
        List<FullParts> GetFullParts();
        List<FullParts> GetTopSellingParts();
        FullParts GetPartById(int partId); // Added GetPartById method
    }
}
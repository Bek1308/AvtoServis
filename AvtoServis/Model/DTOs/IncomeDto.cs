using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Model.DTOs
{
    public class IncomeDto
    {
        public int BatchId { get; set; } // Partiya ID (oldin OperationID)
        public string BatchName { get; set; } = string.Empty; // Partiya nomi (yangi)
        public int TotalQuantity { get; set; } // Umumiy miqdor
        public int PartTypesCount { get; set; } // Turlari soni
        public List<string> Suppliers { get; set; } = new List<string>(); // Pоставщикlar (ro'yxat)
        public int RemainingQuantity { get; set; } // Qoldiq miqdor
        public int RemainingPercentage { get; set; } // Qoldiq foiz
        public string StatusName { get; set; } = string.Empty; // Status nomi
        public string UserFullName { get; set; } = string.Empty; // Qo'shgan foydalanuvchi
    }
}

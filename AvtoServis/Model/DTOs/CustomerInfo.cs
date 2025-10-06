using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Model.DTOs
{
    public class CustomerInfo
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Phone))
                return FullName;
            return $"{FullName} ({Phone})";
        }
    }
}

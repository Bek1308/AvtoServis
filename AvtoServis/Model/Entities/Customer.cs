using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Model.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; } 
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string ?Email { get; set; }
        public string ?Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
    }
}

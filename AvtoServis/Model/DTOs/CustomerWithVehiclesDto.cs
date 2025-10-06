using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Model.DTOs
{
    public class CustomerWithVehiclesDto
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Mijozga tegishli avtomobil ID'lari ro'yxati
        /// </summary>
        public List<int> VehicleIds { get; set; } = new List<int>();
    }
}

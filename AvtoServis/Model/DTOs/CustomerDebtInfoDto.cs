using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AvtoServis.Model.DTOs
{
    public class CustomerDebtInfoDto
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public List<string> CarModels { get; set; } = new List<string>();
        public decimal UmumiyQarz { get; set; }
        public List<DebtDetail> DebtDetails { get; set; } = new List<DebtDetail>();

        // STRING_AGG dan kelgan ma'lumotlarni split qilish uchun metod
        public void ParseCarModels(string carModelsString)
        {
            if (!string.IsNullOrEmpty(carModelsString))
            {
                CarModels = carModelsString.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }

        public void ParseDebtDetails(string debtDetailsString)
        {
            if (!string.IsNullOrEmpty(debtDetailsString))
            {
                DebtDetails = debtDetailsString.Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(detail =>
                    {
                        var parts = detail.Split(':');
                        return new DebtDetail
                        {
                            ItemName = parts[0],
                            Amount = decimal.Parse(parts[1], CultureInfo.InvariantCulture)
                        };
                    }).ToList();
            }
        }
    }

    public class DebtDetail
    {
        public string ItemName { get; set; }
        public decimal Amount { get; set; }
    }
}
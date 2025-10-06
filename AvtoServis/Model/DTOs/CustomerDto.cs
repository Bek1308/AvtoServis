using System;

namespace AvtoServis.Model.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        // ComboBox'da ko'rsatish uchun ToString override qilamiz
        public override string ToString()
        {
            return $"{FullName} ({Phone})";
        }
    }
}
namespace AvtoServis.Model.Entities
{
    public class CarModel
    {
        public int Id { get; set; }
        public int CarBrandId { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CarBrandName { get; set; }

        // Qo'shimcha property faqat Display uchun
        public string DisplayName
        {
            get { return $"{CarBrandName} {Model} {Year}"; }
        }
    }
}
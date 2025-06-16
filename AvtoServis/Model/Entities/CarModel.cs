namespace AvtoServis.Model.Entities
{
    public class CarModel
    {
        public int Id { get; set; }
        public int CarBrandId { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CarBrandName { get; set; }
    }
}
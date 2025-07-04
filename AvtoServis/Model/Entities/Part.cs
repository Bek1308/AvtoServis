namespace AvtoServis.Model.Entities
{
    public class Part
    {
        public int PartID { get; set; }
        public int CarBrandId { get; set; }
        public string CarBrandName { get; set; }
        public string CatalogNumber { get; set; }
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
        public int QualityID { get; set; }
        public string QualityName { get; set; }
        public string PartName { get; set; }
        public string Characteristics { get; set; }
        public string PhotoPath { get; set; }
    }
}
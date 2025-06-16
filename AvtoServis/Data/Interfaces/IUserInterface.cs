namespace AvtoServis.Forms.Controls
{
    public interface IUserInterface
    {
        void LoadData();
        void PerformSearch(string searchText);
        void ApplyFilters(int? minYear, int? maxYear, int? brandId);
        void ExportData(string filePath);
        void ShowDialog(int? id);
    }
}
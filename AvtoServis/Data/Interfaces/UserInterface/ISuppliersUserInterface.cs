namespace AvtoServis.Data.Interfaces.SuppliersInterface
{
    public interface ISuppliersUserInterface
    {
        void LoadData();
        void PerformSearch(string searchText);
        void ApplyFilters(string name);
        void ExportData(string filePath);
        void ShowDialog(int? id);
    }
}
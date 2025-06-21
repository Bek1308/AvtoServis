namespace AvtoServis.Data.Interfaces
{
    public interface IStatusesUserInterface
    {
        void LoadData();
        void PerformSearch(string searchText);
        void ExportData(string filePath);
        void ShowDialog(int? id);
    }
}
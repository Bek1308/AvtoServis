namespace AvtoServis.Data.Interfaces
{
    public interface IStockUserInterface
    {
        void LoadData();
        void PerformSearch(string searchText);
        void ExportData(string filePath);
        void ShowDialog(int? id);
    }
}
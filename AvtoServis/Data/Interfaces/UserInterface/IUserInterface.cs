namespace AvtoServis.Data.Interfaces.UserInterface
{
    public interface IUserInterface
    {
        void LoadData();
        void PerformSearch(string searchText);
        void ShowDialog(int? id, bool isViewOnly = false);
    }
}
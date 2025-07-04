using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AvtoServis.ViewModels.Common
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Xususiyat o'zgarganda UI ni xabardor qilish uchun metod
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Xususiyat qiymatini o'rnatish va agar o'zgarsa xabardor qilish uchun yordamchi metod
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
            {
                return false; // Agar qiymat o'zgarmasa, hech narsa qilmaymiz
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

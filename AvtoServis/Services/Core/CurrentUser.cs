using AvtoServis.Model.Entities;

namespace AvtoServis.Services.Core
{
    public class CurrentUser
    {
        private static CurrentUser _instance;
        private static readonly object _lock = new object();

        // Singleton instance
        public static CurrentUser Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new CurrentUser();
                }
            }
        }

        public User User { get; private set; }

        // Foydalanuvchi ma'lumotlarini o'rnatish
        public void SetUser(User user)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        // Foydalanuvchi ma'lumotlarini tozalash
        public void Clear()
        {
            User = null;
        }

        // Foydalanuvchi autentifikatsiya qilinganligini tekshirish
        public bool IsAuthenticated => User != null;
    }
}
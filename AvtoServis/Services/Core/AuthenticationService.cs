using AvtoServis.Data.Repositories;
using AvtoServis.Model.Entities;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Services.Core
{
    public class AuthenticationService
    {
        private readonly UsersRepository _usersRepository;

        public AuthenticationService()
        {
            _usersRepository = new UsersRepository();
        }

        public User Authenticate(string username, string password)
        {
            var user = _usersRepository.GetUserByUsername(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user; // Muvaffaqiyatli autentifikatsiya
            }
            return null; // Xato: foydalanuvchi topilmadi yoki parol noto'g'ri
        }
        public User GetUserByUsername(string username)
        {
            return _usersRepository.GetUserByUsername(username);
        }
    }
}

using AvtoServis.Data.Repositories;
using AvtoServis.Model.Entities;
using System;
using System.Drawing;

namespace AvtoServis.Forms.Controls
{
    public class UserProfileViewModel
    {
        private readonly IProfileRepository _profileRepository;
        public string ConnectionString { get; }

        public UserProfileViewModel(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _profileRepository = new ProfileRepository(connectionString);
        }

        public UserProfileDto GetUserDetails(int userId)
        {
            var user = _profileRepository.GetUserById(userId);
            if (user == null)
                return null;

            return new UserProfileDto
            {
                UserID = user.UserID,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role,
                Email = user.Email,
                Phone = user.Phone,
                CreatedDate = user.CreatedDate,
                IsActive = user.IsActive
            };
        }

        public void UpdateUserProfile(UserProfileDto userProfile)
        {
            if (userProfile == null)
                throw new ArgumentNullException(nameof(userProfile));

            var user = new User
            {
                UserID = userProfile.UserID,
                Username = userProfile.Username,
                FullName = userProfile.FullName,
                Role = userProfile.Role,
                Email = userProfile.Email,
                Phone = userProfile.Phone,
                IsActive = userProfile.IsActive,
                // Preserve existing PasswordHash and PhotoPath
                PasswordHash = _profileRepository.GetUserById(userProfile.UserID)?.PasswordHash,
                PhotoPath = _profileRepository.GetUserById(userProfile.UserID)?.PhotoPath
            };

            _profileRepository.UpdateUserProfile(user);
        }

        public Image GetUserAvatar(int userId)
        {
            return _profileRepository.GetUserAvatar(userId);
        }

        public void UpdateUserAvatar(int userId, Image avatar)
        {
            if (avatar == null)
                throw new ArgumentNullException(nameof(avatar));

            _profileRepository.UpdateUserAvatar(userId, avatar);
        }

        public bool ValidateCurrentPassword(int userId, string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            return _profileRepository.ValidateCurrentPassword(userId, password);
        }

        public void UpdateUserPassword(int userId, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentException("New password cannot be empty.", nameof(newPassword));

            _profileRepository.UpdateUserPassword(userId, newPassword);
        }
    }

    public class UserProfileDto
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
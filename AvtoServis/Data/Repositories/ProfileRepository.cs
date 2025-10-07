using AvtoServis.Data.Configuration;
using AvtoServis.Model.Entities;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace AvtoServis.Data.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly string _connectionString;

        public ProfileRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public User GetUserById(int userId)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE UserID = @UserID AND IsActive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserID = (int)reader["UserID"],
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            FullName = reader["FullName"].ToString(),
                            Role = reader["Role"].ToString(),
                            Email = reader["Email"] as string,
                            Phone = reader["Phone"] as string,
                            CreatedDate = (DateTime)reader["CreatedDate"],
                            IsActive = (bool)reader["IsActive"],
                            PhotoPath = reader["PhotoPath"] as string
                        };
                    }
                }
            }

            return user;
        }

        public void UpdateUserProfile(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE Users 
                    SET Username = @Username, FullName = @FullName, Role = @Role, 
                        Email = @Email, Phone = @Phone, IsActive = @IsActive
                    WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@Username", user.Username ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@FullName", user.FullName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Role", user.Role ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Phone", user.Phone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IsActive", user.IsActive);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Image GetUserAvatar(int userId)
        {
            string photoPath = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT PhotoPath FROM Users WHERE UserID = @UserID AND IsActive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                photoPath = command.ExecuteScalar() as string;
            }

            if (!string.IsNullOrEmpty(photoPath) && File.Exists(photoPath))
            {
                return Image.FromFile(photoPath);
            }

            return null;
        }

        public void UpdateUserAvatar(int userId, Image avatar)
        {
            string photoPath = Path.Combine(Path.GetTempPath(), $"{userId}_{Guid.NewGuid()}.png");
            avatar.Save(photoPath, System.Drawing.Imaging.ImageFormat.Png);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET PhotoPath = @PhotoPath WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@PhotoPath", photoPath);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool ValidateCurrentPassword(int userId, string password)
        {
            string storedHash = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT PasswordHash FROM Users WHERE UserID = @UserID AND IsActive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                storedHash = command.ExecuteScalar() as string;
            }

            if (string.IsNullOrEmpty(storedHash))
                return false;

            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        public void UpdateUserPassword(int userId, string newPassword)
        {
            string newHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@PasswordHash", newHash);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public interface IProfileRepository
    {
        User GetUserById(int userId);
        void UpdateUserProfile(User user);
        Image GetUserAvatar(int userId);
        void UpdateUserAvatar(int userId, Image avatar);
        bool ValidateCurrentPassword(int userId, string password);
        void UpdateUserPassword(int userId, string newPassword);
    }
}
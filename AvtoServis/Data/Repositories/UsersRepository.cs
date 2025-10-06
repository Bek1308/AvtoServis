using AvtoServis.Data.Configuration;
using AvtoServis.Model.Entities;
using System.Data.SqlClient;

namespace AvtoServis.Data.Repositories
{
    public class UsersRepository
    {
        private readonly string _connectionString;

        public UsersRepository()
        {
            _connectionString = DatabaseConfig.ConnectionString; // App.config'dan olish
        }

        public User GetUserByUsername(string username)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE Username = @Username AND IsActive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

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
                            IsActive = (bool)reader["IsActive"]
                        };
                    }
                }
            }

            return user;
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
                            IsActive = (bool)reader["IsActive"]
                        };
                    }
                }
            }

            return user;
        }

    }
}

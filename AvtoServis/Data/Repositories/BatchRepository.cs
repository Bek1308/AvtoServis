using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class BatchRepository: IBatchRepository
    {
        private readonly string _connectionString;

        public BatchRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<Batch> GetAll()
        {
            var batch = new List<Batch>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT BatchID, Name FROM Batches", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            batch.Add(new Batch
                            {
                                BatchID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Loaded {batch.Count} manufacturers.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetAll SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при загрузке производителей.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetAll Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при загрузке производителей.", ex);
            }
            return batch;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT UserID, Username, FullName, Role FROM Users", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                FullName = reader.GetString(2),
                                Role = reader.GetString(3),
                     
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Loaded {users.Count} manufacturers.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetAll SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при загрузке производителей.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetAll Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при загрузке производителей.", ex);
            }
            return users;
        }

       

        public Batch? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT 
                           BatchID, 
                           Name
                        FROM Batches
                        WHERE BatchID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Batch
                            {
                                

                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Ошибка: {ex.Message}");
                throw new Exception($"Партия с ID {id} не найдена.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

    }
}

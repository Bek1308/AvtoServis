using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System.Data.SqlClient;
using System.Diagnostics;


namespace AvtoServis.Data.Repositories
{
    public class Finance_StatusRepository: IFinance_StatusRepository
    {
        private readonly string _connectionString;

        public Finance_StatusRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public Finance_Status? GetById(int id)
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
                           Id, 
                           Name, 
                           Description 
                        FROM Finance_Status
                        WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Finance_Status   
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Ошибка: {ex.Message}");
                throw new Exception($"Деталь с ID {id} не найдена.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }


        public List<Finance_Status> GetAll()
        {
            var status = new List<Finance_Status>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT Id, Name, Description FROM Finance_Status", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            status.Add(new Finance_Status
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                               
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Loaded {status.Count} Finance_Status.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetAll SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при загрузке финансивый статус.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetAll Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при загрузке финансивый статус..", ex);
            }
            return status;
        }

    }
}

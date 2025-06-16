using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class ServicesRepository : IRepository<Service>
    {
        private readonly string _connectionString;

        public ServicesRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<Service> GetAll()
        {
            var services = new List<Service>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT ServiceID, Name, Price FROM Services", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            services.Add(new Service
                            {
                                ServiceID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Loaded {services.Count} services.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetAll SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при загрузке услуг.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetAll Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при загрузке услуг.", ex);
            }
            return services;
        }

        public Service GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID услуги.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT ServiceID, Name, Price FROM Services WHERE ServiceID = @ServiceID", connection);
                    command.Parameters.AddWithValue("@ServiceID", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Service
                            {
                                ServiceID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Error: {ex.Message}");
                throw new Exception($"Ошибка базы данных при загрузке услуги с ID {id}.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Error: {ex.Message}");
                throw new Exception($"Неизвестная ошибка при загрузке услуги с ID {id}.", ex);
            }
        }

        public void Add(Service entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        "INSERT INTO Services (Name, Price) VALUES (@Name, @Price); SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Price", entity.Price);
                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    entity.ServiceID = newId;
                    Debug.WriteLine($"Add: Service added with ID {newId}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Add SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при добавлении услуги.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при добавлении услуги.", ex);
            }
        }

        public void Update(Service entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.ServiceID <= 0)
                throw new ArgumentException("Некорректный ID услуги.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        "UPDATE Services SET Name = @Name, Price = @Price WHERE ServiceID = @ServiceID", connection);
                    command.Parameters.AddWithValue("@ServiceID", entity.ServiceID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Price", entity.Price);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Услуга с ID {entity.ServiceID} не найдена.");
                    Debug.WriteLine($"Update: Service updated with ID {entity.ServiceID}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Update SQL Error: {ex.Message}");
                throw new Exception($"Ошибка базы данных при обновлении услуги с ID {entity.ServiceID}.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Update Error: {ex.Message}");
                throw new Exception($"Неизвестная ошибка при обновлении услуги с ID {entity.ServiceID}.", ex);
            }
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID услуги.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM Services WHERE ServiceID = @ServiceID", connection);
                    command.Parameters.AddWithValue("@ServiceID", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Услуга с ID {id} не найдена.");
                    Debug.WriteLine($"Delete: Service deleted with ID {id}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Delete SQL Error: {ex.Message}");
                throw new Exception($"Ошибка базы данных при удалении услуги с ID {id}.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete Error: {ex.Message}");
                throw new Exception($"Неизвестная ошибка при удалении услуги с ID {id}.", ex);
            }
        }

        public List<Service> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не может быть пустым.");

            var services = new List<Service>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT ServiceID, Name, Price FROM Services WHERE Name LIKE @SearchText", connection);
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            services.Add(new Service
                            {
                                ServiceID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            });
                        }
                    }
                }
                Debug.WriteLine($"Search: Found {services.Count} services for query '{searchText}'.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Search SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при поиске услуг.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Search Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при поиске услуг.", ex);
            }
            return services;
        }

        public List<Service> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < minPrice)
                throw new ArgumentException("Некорректный диапазон цен.");

            var services = new List<Service>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT ServiceID, Name, Price FROM Services WHERE Price BETWEEN @MinPrice AND @MaxPrice", connection);
                    command.Parameters.AddWithValue("@MinPrice", minPrice);
                    command.Parameters.AddWithValue("@MaxPrice", maxPrice);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            services.Add(new Service
                            {
                                ServiceID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            });
                        }
                    }
                }
                Debug.WriteLine($"FilterByPrice: Found {services.Count} services between {minPrice} and {maxPrice}.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"FilterByPrice SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при фильтрации услуг по цене.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FilterByPrice Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при фильтрации услуг по цене.", ex);
            }
            return services;
        }
    }
}
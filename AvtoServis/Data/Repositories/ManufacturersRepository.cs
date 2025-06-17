using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class ManufacturersRepository : IManufacturersRepository
    {
        private readonly string _connectionString;

        public ManufacturersRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<Manufacturer> GetAll()
        {
            var manufacturers = new List<Manufacturer>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT ManufacturerID, Name FROM Manufacturers", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            manufacturers.Add(new Manufacturer
                            {
                                ManufacturerID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Loaded {manufacturers.Count} manufacturers.");
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
            return manufacturers;
        }

        public Manufacturer GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID производителя.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT ManufacturerID, Name FROM Manufacturers WHERE ManufacturerID = @ManufacturerID", connection);
                    command.Parameters.AddWithValue("@ManufacturerID", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Manufacturer
                            {
                                ManufacturerID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Error: {ex.Message}");
                throw new Exception($"Ошибка базы данных при загрузке производителя с ID {id}.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Error: {ex.Message}");
                throw new Exception($"Неизвестная ошибка при загрузке производителя с ID {id}.", ex);
            }
        }

        public void Add(Manufacturer entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        "INSERT INTO Manufacturers (Name) VALUES (@Name); SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    entity.ManufacturerID = newId;
                    Debug.WriteLine($"Add: Manufacturer added with ID {newId}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Add SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при добавлении производителя.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при добавлении производителя.", ex);
            }
        }

        public void Update(Manufacturer entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.ManufacturerID <= 0)
                throw new ArgumentException("Некорректный ID производителя.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        "UPDATE Manufacturers SET Name = @Name WHERE ManufacturerID = @ManufacturerID", connection);
                    command.Parameters.AddWithValue("@ManufacturerID", entity.ManufacturerID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Производитель с ID {entity.ManufacturerID} не найден.");
                    Debug.WriteLine($"Update: Manufacturer updated with ID {entity.ManufacturerID}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Update SQL Error: {ex.Message}");
                throw new Exception($"Ошибка базы данных при обновлении производителя с ID {entity.ManufacturerID}.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Update Error: {ex.Message}");
                throw new Exception($"Неизвестная ошибка при обновлении производителя с ID {entity.ManufacturerID}.", ex);
            }
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID производителя.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM Manufacturers WHERE ManufacturerID = @ManufacturerID", connection);
                    command.Parameters.AddWithValue("@ManufacturerID", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Производитель с ID {id} не найден.");
                    Debug.WriteLine($"Delete: Manufacturer deleted with ID {id}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Delete SQL Error: {ex.Message}");
                throw new Exception($"Ошибка базы данных при удалении производителя с ID {id}.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete Error: {ex.Message}");
                throw new Exception($"Неизвестная ошибка при удалении производителя с ID {id}.", ex);
            }
        }

        public List<Manufacturer> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не может быть пустым.");

            var manufacturers = new List<Manufacturer>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT ManufacturerID, Name FROM Manufacturers WHERE Name LIKE @SearchText", connection);
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            manufacturers.Add(new Manufacturer
                            {
                                ManufacturerID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
                Debug.WriteLine($"Search: Found {manufacturers.Count} manufacturers for query '{searchText}'.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Search SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при поиске производителей.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Search Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при поиске производителей.", ex);
            }
            return manufacturers;
        }

        public List<Manufacturer> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            // Narx yo'qligi sababli bu metod ishlatilmaydi, lekin interfeys talabi uchun bo'sh implementatsiya
            throw new NotSupportedException("Фильтрация по цене не поддерживается для производителей.");
        }
    }
}
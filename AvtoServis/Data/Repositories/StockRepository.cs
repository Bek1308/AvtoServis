using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly string _connectionString;

        public StockRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<Stock> GetAll()
        {
            var stocks = new List<Stock>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT StockID, Name
                          FROM Stock", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stocks.Add(new Stock
                            {
                                StockID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Загружено {stocks.Count} складов.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetAll SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка в базе данных.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetAll Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
            return stocks;
        }

        public Stock GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT StockID, Name
                          FROM Stock
                          WHERE StockID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Stock
                            {
                                StockID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Ошибка: {ex.Message}");
                throw new Exception($"Склад с ID {id} не найден.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Add(Stock entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"INSERT INTO Stock (Name)
                          VALUES (@Name);
                          SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    entity.StockID = newId;
                    Debug.WriteLine($"Add: Склад добавлен с ID {newId}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Add SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при добавлении склада.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Update(Stock entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.StockID <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"UPDATE Stock
                          SET Name = @Name
                          WHERE StockID = @StockID", connection);
                    command.Parameters.AddWithValue("@StockID", entity.StockID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Склад с ID {entity.StockID} не найден.");
                    Debug.WriteLine($"Update: Склад обновлен с ID {entity.StockID}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Update SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при обновлении склада.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Update Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"DELETE FROM Stock
                          WHERE StockID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Склад с ID {id} не найден.");
                    Debug.WriteLine($"Delete: Склад удален с ID {id}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Delete SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при удалении склада.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public List<Stock> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не должен быть пустым.");

            var stocks = new List<Stock>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT StockID, Name
                          FROM Stock
                          WHERE Name LIKE @SearchText", connection);
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stocks.Add(new Stock
                            {
                                StockID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
                Debug.WriteLine($"Search: Найдено {stocks.Count} складов для запроса '{searchText}'.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Search SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при поиске.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Search Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
            return stocks;
        }
    }
}
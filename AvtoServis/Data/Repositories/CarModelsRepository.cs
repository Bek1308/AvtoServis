using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class CarModelsRepository : ICarModelsRepository
    {
        private readonly string _connectionString;

        public CarModelsRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<CarModel> GetAll()
        {
            var models = new List<CarModel>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT cm.Id, cm.CarBrandId, cm.Model, cm.Year, cb.CarBrandName 
                          FROM CarModels cm 
                          INNER JOIN CarBrand cb ON cm.CarBrandId = cb.Id", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            models.Add(new CarModel
                            {
                                Id = reader.GetInt32(0),
                                CarBrandId = reader.GetInt32(1),
                                Model = reader.GetString(2),
                                Year = reader.GetInt32(3),
                                CarBrandName = reader.GetString(4)
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Загружено {models.Count} моделей автомобилей.");
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
            return models;
        }

        public CarModel GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT cm.Id, cm.CarBrandId, cm.Model, cm.Year, cb.CarBrandName 
                          FROM CarModels cm 
                          INNER JOIN CarBrand cb ON cm.CarBrandId = cb.Id 
                          WHERE cm.Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CarModel
                            {
                                Id = reader.GetInt32(0),
                                CarBrandId = reader.GetInt32(1),
                                Model = reader.GetString(2),
                                Year = reader.GetInt32(3),
                                CarBrandName = reader.GetString(4)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Ошибка: {ex.Message}");
                throw new Exception($"Модель с ID {id} не найдена.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Add(CarModel entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"INSERT INTO CarModels (CarBrandId, Model, Year) 
                          VALUES (@CarBrandId, @Model, @Year); 
                          SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@CarBrandId", entity.CarBrandId);
                    command.Parameters.AddWithValue("@Model", entity.Model);
                    command.Parameters.AddWithValue("@Year", entity.Year);
                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    entity.Id = newId;
                    Debug.WriteLine($"Add: Модель автомобиля добавлена с ID {newId}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Add SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при добавлении модели.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Update(CarModel entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.Id <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"UPDATE CarModels 
                          SET CarBrandId = @CarBrandId, Model = @Model, Year = @Year 
                          WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@CarBrandId", entity.CarBrandId);
                    command.Parameters.AddWithValue("@Model", entity.Model);
                    command.Parameters.AddWithValue("@Year", entity.Year);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Модель с ID {entity.Id} не найдена.");
                    Debug.WriteLine($"Update: Модель автомобиля обновлена с ID {entity.Id}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Update SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при обновлении модели.", ex);
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
                        "DELETE FROM CarModels WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Модель с ID {id} не найдена.");
                    Debug.WriteLine($"Delete: Модель автомобиля удалена с ID {id}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Delete SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при удалении модели.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public List<CarModel> SearchByModel(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не должен быть пустым.");

            var models = new List<CarModel>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT cm.Id, cm.CarBrandId, cm.Model, cm.Year, cb.CarBrandName 
                          FROM CarModels cm 
                          INNER JOIN CarBrand cb ON cm.CarBrandId = cb.Id 
                          WHERE cm.Model LIKE @SearchText", connection);
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            models.Add(new CarModel
                            {
                                Id = reader.GetInt32(0),
                                CarBrandId = reader.GetInt32(1),
                                Model = reader.GetString(2),
                                Year = reader.GetInt32(3),
                                CarBrandName = reader.GetString(4)
                            });
                        }
                    }
                }
                Debug.WriteLine($"SearchByModel: Найдено {models.Count} моделей для запроса '{searchText}'.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SearchByModel SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при поиске.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SearchByModel Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
            return models;
        }

        public List<CarModel> SearchByYear(int year)
        {
            var models = new List<CarModel>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT cm.Id, cm.CarBrandId, cm.Model, cm.Year, cb.CarBrandName 
                          FROM CarModels cm 
                          INNER JOIN CarBrand cb ON cm.CarBrandId = cb.Id 
                          WHERE cm.Year = @Year", connection);
                    command.Parameters.AddWithValue("@Year", year);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            models.Add(new CarModel
                            {
                                Id = reader.GetInt32(0),
                                CarBrandId = reader.GetInt32(1),
                                Model = reader.GetString(2),
                                Year = reader.GetInt32(3),
                                CarBrandName = reader.GetString(4)
                            });
                        }
                    }
                }
                Debug.WriteLine($"SearchByYear: Найдено {models.Count} моделей для года {year}.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SearchByYear SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при поиске по году.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SearchByYear Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
            return models;
        }

        public List<CarModel> FilterByBrand(int brandId)
        {
            var models = new List<CarModel>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT cm.Id, cm.CarBrandId, cm.Model, cm.Year, cb.CarBrandName 
                          FROM CarModels cm 
                          INNER JOIN CarBrand cb ON cm.CarBrandId = cb.Id 
                          WHERE cm.CarBrandId = @BrandId", connection);
                    command.Parameters.AddWithValue("@BrandId", brandId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            models.Add(new CarModel
                            {
                                Id = reader.GetInt32(0),
                                CarBrandId = reader.GetInt32(1),
                                Model = reader.GetString(2),
                                Year = reader.GetInt32(3),
                                CarBrandName = reader.GetString(4)
                            });
                        }
                    }
                }
                Debug.WriteLine($"FilterByBrand: Найдено {models.Count} моделей для марки ID {brandId}.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"FilterByBrand SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при фильтрации по марке.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FilterByBrand Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
            return models;
        }
    }
}
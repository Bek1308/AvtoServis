using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class PartQualitiesRepository : IPartQualitiesRepository
    {
        private readonly string _connectionString;

        public PartQualitiesRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<PartQuality> GetAll()
        {
            var qualities = new List<PartQuality>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT QualityID, Name FROM PartQualities", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            qualities.Add(new PartQuality
                            {
                                QualityID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Загружено {qualities.Count} качеств деталей.");
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
            return qualities;
        }

        public PartQuality GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT QualityID, Name FROM PartQualities WHERE QualityID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PartQuality
                            {
                                QualityID = reader.GetInt32(0),
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
                throw new Exception($"Качество с ID {id} не найдено.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Add(PartQuality entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"INSERT INTO PartQualities (Name) VALUES (@Name); SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    entity.QualityID = newId;
                    Debug.WriteLine($"Add: Качество добавлено с ID {newId}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Add SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при добавлении качества.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Update(PartQuality entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.QualityID <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"UPDATE PartQualities SET Name = @Name WHERE QualityID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", entity.QualityID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Качество с ID {entity.QualityID} не найдено.");
                    Debug.WriteLine($"Update: Качество обновлено с ID {entity.QualityID}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Update SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при обновлении качества.", ex);
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
                    var command = new SqlCommand("DELETE FROM PartQualities WHERE QualityID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Качество с ID {id} не найдено.");
                    Debug.WriteLine($"Delete: Качество удалено с ID {id}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Delete SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при удалении качества.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public List<PartQuality> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не должен быть пустым.");

            var qualities = new List<PartQuality>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        "SELECT QualityID, Name FROM PartQualities WHERE Name LIKE @SearchText", connection);
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            qualities.Add(new PartQuality
                            {
                                QualityID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
                Debug.WriteLine($"Search: Найдено {qualities.Count} качеств для запроса '{searchText}'.");
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
            return qualities;
        }
    }
}
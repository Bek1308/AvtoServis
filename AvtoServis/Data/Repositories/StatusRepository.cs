using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly string _connectionString;
        private readonly List<string> _validTables = new List<string>
        {
            "ExpenseStatuses", "IncomeStatuses", "OperationStatuses", "OrderStatuses"
        };

        public StatusRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private void ValidateTableName(string tableName)
        {
            if (!_validTables.Contains(tableName))
                throw new ArgumentException($"Недопустимое имя таблицы: {tableName}.");
        }

        public List<Status> GetAll(string tableName)
        {
            ValidateTableName(tableName);
            var statuses = new List<Status>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        $@"SELECT StatusID, Name, Description, Color
                           FROM {tableName}", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statuses.Add(new Status
                            {
                                StatusID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Color = reader.IsDBNull(3) ? null : reader.GetString(3)
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Загружено {statuses.Count} статусов из таблицы {tableName}.");
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
            return statuses;
        }

        public Status GetById(string tableName, int id)
        {
            ValidateTableName(tableName);
            if (id <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        $@"SELECT StatusID, Name, Description, Color
                           FROM {tableName}
                           WHERE StatusID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Status
                            {
                                StatusID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Color = reader.IsDBNull(3) ? null : reader.GetString(3)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Ошибка: {ex.Message}");
                throw new Exception($"Статус с ID {id} не найден.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Add(string tableName, Status entity)
        {
            ValidateTableName(tableName);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        $@"INSERT INTO {tableName} (Name, Description, Color)
                           VALUES (@Name, @Description, @Color);
                           SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Description", (object)entity.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Color", (object)entity.Color ?? DBNull.Value);
                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    entity.StatusID = newId;
                    Debug.WriteLine($"Add: Статус добавлен с ID {newId} в таблицу {tableName}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Add SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при добавлении статуса.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Update(string tableName, Status entity)
        {
            ValidateTableName(tableName);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.StatusID <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        $@"UPDATE {tableName}
                           SET Name = @Name, Description = @Description, Color = @Color
                           WHERE StatusID = @StatusID", connection);
                    command.Parameters.AddWithValue("@StatusID", entity.StatusID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Description", (object)entity.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Color", (object)entity.Color ?? DBNull.Value);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Статус с ID {entity.StatusID} не найден.");
                    Debug.WriteLine($"Update: Статус обновлен с ID {entity.StatusID} в таблице {tableName}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Update SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при обновлении статуса.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Update Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Delete(string tableName, int id)
        {
            ValidateTableName(tableName);
            if (id <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        $@"DELETE FROM {tableName}
                           WHERE StatusID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Статус с ID {id} не найден.");
                    Debug.WriteLine($"Delete: Статус удален с ID {id} из таблицы {tableName}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Delete SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при удалении статуса.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public List<Status> Search(string tableName, string searchText)
        {
            ValidateTableName(tableName);
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не должен быть пустым.");

            var statuses = new List<Status>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        $@"SELECT StatusID, Name, Description, Color
                           FROM {tableName}
                           WHERE Name LIKE @SearchText OR Description LIKE @SearchText", connection);
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statuses.Add(new Status
                            {
                                StatusID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Color = reader.IsDBNull(3) ? null : reader.GetString(3)
                            });
                        }
                    }
                }
                Debug.WriteLine($"Search: Найдено {statuses.Count} статусов для запроса '{searchText}' в таблице {tableName}.");
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
            return statuses;
        }
    }
}
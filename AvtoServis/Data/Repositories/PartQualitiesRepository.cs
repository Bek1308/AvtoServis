using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
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
                Debug.WriteLine($"GetAll: Loaded {qualities.Count} part qualities.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetAll SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при загрузке качеств запчастей.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetAll Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при загрузке качеств запчастей.", ex);
            }
            return qualities;
        }

        public PartQuality GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID качества запчасти.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT QualityID, Name FROM PartQualities WHERE QualityID = @QualityID", connection);
                    command.Parameters.AddWithValue("@QualityID", id);
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
                Debug.WriteLine($"GetById SQL Error: {ex.Message}");
                throw new Exception($"Ошибка базы данных при загрузке качества запчасти с ID {id}.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Error: {ex.Message}");
                throw new Exception($"Неизвестная ошибка при загрузке качества запчасти с ID {id}.", ex);
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
                        "INSERT INTO PartQualities (Name) VALUES (@Name); SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    entity.QualityID = newId;
                    Debug.WriteLine($"Add: Part quality added with ID {newId}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Add SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при добавлении качества запчасти.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при добавлении качества запчасти.", ex);
            }
        }

        public void Update(PartQuality entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.QualityID <= 0)
                throw new ArgumentException("Некорректный ID качества запчасти.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        "UPDATE PartQualities SET Name = @Name WHERE QualityID = @QualityID", connection);
                    command.Parameters.AddWithValue("@QualityID", entity.QualityID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Качество запчасти с ID {entity.QualityID} не найдено.");
                    Debug.WriteLine($"Update: Part quality updated with ID {entity.QualityID}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Update SQL Error: {ex.Message}");
                throw new Exception($"Ошибка базы данных при обновлении качества запчасти с ID {entity.QualityID}.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Update Error: {ex.Message}");
                throw new Exception($"Неизвестная ошибка при обновлении качества запчасти с ID {entity.QualityID}.", ex);
            }
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID качества запчасти.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM PartQualities WHERE QualityID = @QualityID", connection);
                    command.Parameters.AddWithValue("@QualityID", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Качество запчасти с ID {id} не найдено.");
                    Debug.WriteLine($"Delete: Part quality deleted with ID {id}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Delete SQL Error: {ex.Message}");
                throw new Exception($"Ошибка базы данных при удалении качества запчасти с ID {id}.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete Error: {ex.Message}");
                throw new Exception($"Неизвестная ошибка при удалении качества запчасти с ID {id}.", ex);
            }
        }

        public List<PartQuality> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не может быть пустым.");

            var qualities = new List<PartQuality>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT QualityID, Name FROM PartQualities WHERE Name LIKE @SearchText OR CAST(QualityID AS NVARCHAR) LIKE @SearchText", connection);
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
                Debug.WriteLine($"Search: Found {qualities.Count} part qualities for query '{searchText}'.");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Search SQL Error: {ex.Message}");
                throw new Exception("Ошибка базы данных при поиске качеств запчастей.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Search Error: {ex.Message}");
                throw new Exception("Неизвестная ошибка при поиске качеств запчастей.", ex);
            }
            return qualities;
        }
    }
}
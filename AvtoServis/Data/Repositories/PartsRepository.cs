using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class PartsRepository : IPartsRepository
    {
        private readonly string _connectionString;

        public PartsRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<Part> GetAll()
        {
            var parts = new List<Part>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT p.PartID, p.CarBrand_Id, cb.CarBrandName, p.CatalogNumber, p.ManufacturerID, 
                                 m.Name AS ManufacturerName, p.QualityID, pq.Name AS QualityName, p.PartName, p.Characteristics, p.PhotoPath
                          FROM Parts p
                          INNER JOIN CarBrand cb ON p.CarBrand_Id = cb.Id
                          INNER JOIN Manufacturers m ON p.ManufacturerID = m.ManufacturerID
                          INNER JOIN PartQualities pq ON p.QualityID = pq.QualityID", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            parts.Add(new Part
                            {
                                PartID = reader.GetInt32(0),
                                CarBrandId = reader.GetInt32(1),
                                CarBrandName = reader.GetString(2),
                                CatalogNumber = reader.GetString(3),
                                ManufacturerID = reader.GetInt32(4),
                                ManufacturerName = reader.GetString(5),
                                QualityID = reader.GetInt32(6),
                                QualityName = reader.GetString(7),
                                PartName = reader.GetString(8),
                                Characteristics = reader.IsDBNull(9) ? null : reader.GetString(9),
                                PhotoPath = reader.IsDBNull(10) ? null : reader.GetString(10)
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Загружено {parts.Count} деталей.");
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
            return parts;
        }

        public Part GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT p.PartID, p.CarBrand_Id, cb.CarBrandName, p.CatalogNumber, p.ManufacturerID, 
                                 m.Name AS ManufacturerName, p.QualityID, pq.Name AS QualityName, p.PartName, p.Characteristics, p.PhotoPath
                          FROM Parts p
                          INNER JOIN CarBrand cb ON p.CarBrand_Id = cb.Id
                          INNER JOIN Manufacturers m ON p.ManufacturerID = m.ManufacturerID
                          INNER JOIN PartQualities pq ON p.QualityID = pq.QualityID
                          WHERE p.PartID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Part
                            {
                                PartID = reader.GetInt32(0),
                                CarBrandId = reader.GetInt32(1),
                                CarBrandName = reader.GetString(2),
                                CatalogNumber = reader.GetString(3),
                                ManufacturerID = reader.GetInt32(4),
                                ManufacturerName = reader.GetString(5),
                                QualityID = reader.GetInt32(6),
                                QualityName = reader.GetString(7),
                                PartName = reader.GetString(8),
                                Characteristics = reader.IsDBNull(9) ? null : reader.GetString(9),
                                PhotoPath = reader.IsDBNull(10) ? null : reader.GetString(10)
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

        public void Add(Part entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"INSERT INTO Parts (CarBrand_Id, CatalogNumber, ManufacturerID, QualityID, PartName, Characteristics, PhotoPath)
                          VALUES (@CarBrand_Id, @CatalogNumber, @ManufacturerID, @QualityID, @PartName, @Characteristics, @PhotoPath);
                          SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@CarBrand_Id", entity.CarBrandId);
                    command.Parameters.AddWithValue("@CatalogNumber", entity.CatalogNumber);
                    command.Parameters.AddWithValue("@ManufacturerID", entity.ManufacturerID);
                    command.Parameters.AddWithValue("@QualityID", entity.QualityID);
                    command.Parameters.AddWithValue("@PartName", entity.PartName);
                    command.Parameters.AddWithValue("@Characteristics", (object)entity.Characteristics ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PhotoPath", (object)entity.PhotoPath ?? DBNull.Value);
                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    entity.PartID = newId;
                    Debug.WriteLine($"Add: Деталь добавлена с ID {newId}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Add SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при добавлении детали.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Update(Part entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.PartID <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"UPDATE Parts
                          SET CarBrand_Id = @CarBrand_Id, CatalogNumber = @CatalogNumber, ManufacturerID = @ManufacturerID,
                              QualityID = @QualityID, PartName = @PartName, Characteristics = @Characteristics, PhotoPath = @PhotoPath
                          WHERE PartID = @PartID", connection);
                    command.Parameters.AddWithValue("@PartID", entity.PartID);
                    command.Parameters.AddWithValue("@CarBrand_Id", entity.CarBrandId);
                    command.Parameters.AddWithValue("@CatalogNumber", entity.CatalogNumber);
                    command.Parameters.AddWithValue("@ManufacturerID", entity.ManufacturerID);
                    command.Parameters.AddWithValue("@QualityID", entity.QualityID);
                    command.Parameters.AddWithValue("@PartName", entity.PartName);
                    command.Parameters.AddWithValue("@Characteristics", (object)entity.Characteristics ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PhotoPath", (object)entity.PhotoPath ?? DBNull.Value);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Деталь с ID {entity.PartID} не найдена.");
                    Debug.WriteLine($"Update: Деталь обновлена с ID {entity.PartID}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Update SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при обновлении детали.", ex);
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
                    var command = new SqlCommand("DELETE FROM Parts WHERE PartID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Деталь с ID {id} не найдена.");
                    Debug.WriteLine($"Delete: Деталь удалена с ID {id}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Delete SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при удалении детали.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public List<Part> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не должен быть пустым.");

            var parts = new List<Part>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var conditions = new List<string>();


                    string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" OR ", conditions) : "";
                    var command = new SqlCommand(
                        $@"SELECT p.PartID, p.CarBrand_Id, cb.CarBrandName, p.CatalogNumber, p.ManufacturerID, 
                                  m.Name AS ManufacturerName, p.QualityID, pq.Name AS QualityName, p.PartName, p.Characteristics, p.PhotoPath
                          FROM Parts p
                          INNER JOIN CarBrand cb ON p.CarBrand_Id = cb.Id
                          INNER JOIN Manufacturers m ON p.ManufacturerID = m.ManufacturerID
                          INNER JOIN PartQualities pq ON p.QualityID = pq.QualityID
                          {whereClause}", connection);
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            parts.Add(new Part
                            {
                                PartID = reader.GetInt32(0),
                                CarBrandId = reader.GetInt32(1),
                                CarBrandName = reader.GetString(2),
                                CatalogNumber = reader.GetString(3),
                                ManufacturerID = reader.GetInt32(4),
                                ManufacturerName = reader.GetString(5),
                                QualityID = reader.GetInt32(6),
                                QualityName = reader.GetString(7),
                                PartName = reader.GetString(8),
                                Characteristics = reader.IsDBNull(9) ? null : reader.GetString(9),
                                PhotoPath = reader.IsDBNull(10) ? null : reader.GetString(10)
                            });
                        }
                    }
                }
                Debug.WriteLine($"Search: Найдено {parts.Count} деталей для запроса '{searchText}'.");
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
            return parts;
        }
    }
}
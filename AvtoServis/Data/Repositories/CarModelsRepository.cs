using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System.Data.SqlClient;

namespace AvtoServis.Data
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
                    var query = @"
                        SELECT cm.Id, cm.Model, cm.Year, cm.CarBrandId, cb.CarBrandName
                        FROM CarModels cm
                        JOIN CarBrand cb ON cm.CarBrandId = cb.Id";
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            models.Add(new CarModel
                            {
                                Id = reader.GetInt32(0),
                                Model = reader.GetString(1),
                                Year = reader.GetInt32(2),
                                CarBrandId = reader.GetInt32(3),
                                CarBrandName = reader.GetString(4)
                            });
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine($"GetAll: Retrieved {models.Count} car models.");
                return models;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAll Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при получении списка моделей.", ex);
            }
        }

        public void Add(CarModel model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO CarModels (Model, Year, CarBrandId) VALUES (@Model, @Year, @CarBrandId); SELECT SCOPE_IDENTITY();";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Model", model.Model);
                        command.Parameters.AddWithValue("@Year", model.Year);
                        command.Parameters.AddWithValue("@CarBrandId", model.CarBrandId);
                        model.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Add: Added car model '{model.Model}' with ID {model.Id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Add Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при добавлении модели.", ex);
            }
        }

        public void Update(CarModel model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "UPDATE CarModels SET Model = @Model, Year = @Year, CarBrandId = @CarBrandId WHERE Id = @Id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", model.Id);
                        command.Parameters.AddWithValue("@Model", model.Model);
                        command.Parameters.AddWithValue("@Year", model.Year);
                        command.Parameters.AddWithValue("@CarBrandId", model.CarBrandId);
                        command.ExecuteNonQuery();
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Update: Updated car model '{model.Model}' with ID {model.Id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Update Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при обновлении модели.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "DELETE FROM CarModels WHERE Id = @Id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Delete: Deleted car model with ID {id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Delete Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при удалении модели.", ex);
            }
        }

        public List<CarModel> GetCarModels(int? customerId = null)
        {
            var models = new List<CarModel>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = @"
                    SELECT cm.Id, cm.Model, cm.Year, cm.CarBrandId, cb.CarBrandName
                    FROM CarModels cm
                    JOIN CarBrand cb ON cm.CarBrandId = cb.Id
                    /**customerJoin**/
                    /**customerWhere**/";

                    if (customerId.HasValue)
                    {
                        query = query
                            .Replace("/**customerJoin**/", "JOIN CustomersCars cc ON cm.Id = cc.CarModel_Id")
                            .Replace("/**customerWhere**/", "WHERE cc.CustomerId = @CustomerId");
                    }
                    else
                    {
                        query = query
                            .Replace("/**customerJoin**/", "")
                            .Replace("/**customerWhere**/", "");
                    }

                    using (var command = new SqlCommand(query, connection))
                    {
                        if (customerId.HasValue)
                            command.Parameters.AddWithValue("@CustomerId", customerId.Value);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                models.Add(new CarModel
                                {
                                    Id = reader.GetInt32(0),
                                    Model = reader.GetString(1),
                                    Year = reader.GetInt32(2),
                                    CarBrandId = reader.GetInt32(3),
                                    CarBrandName = reader.GetString(4)
                                });
                            }
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine($"GetCarModels: Retrieved {models.Count} car models.");
                return models;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetCarModels Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при получении списка моделей.", ex);
            }
        }

    }
}
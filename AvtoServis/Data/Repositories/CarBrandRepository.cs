using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System.Data.SqlClient;

namespace AvtoServis.Data
{
    public class CarBrandRepository : ICarBrandRepository
    {
        private readonly string _connectionString;

        public CarBrandRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<CarBrand> GetAll()
        {
            var brands = new List<CarBrand>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT Id, CarBrandName FROM CarBrand";
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            brands.Add(new CarBrand
                            {
                                Id = reader.GetInt32(0),
                                CarBrandName = reader.GetString(1)
                            });
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine($"GetAll: Retrieved {brands.Count} car brands.");
                return brands;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAll Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при получении списка марок.", ex);
            }
        }

        public void Add(CarBrand brand)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO CarBrands (CarBrandName) VALUES (@CarBrandName); SELECT SCOPE_IDENTITY();";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CarBrandName", brand.CarBrandName);
                        brand.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Add: Added car brand '{brand.CarBrandName}' with ID {brand.Id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Add Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при добавлении марки.", ex);
            }
        }

        public void Update(CarBrand brand)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "UPDATE CarBrands SET CarBrandName = @CarBrandName WHERE Id = @Id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", brand.Id);
                        command.Parameters.AddWithValue("@CarBrandName", brand.CarBrandName);
                        command.ExecuteNonQuery();
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Update: Updated car brand '{brand.CarBrandName}' with ID {brand.Id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Update Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при обновлении марки.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "DELETE FROM CarBrands WHERE Id = @Id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Delete: Deleted car brand with ID {id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Delete Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при удалении марки.", ex);
            }
        }

        public List<CarBrand> SearchByName(string name)
        {
            var brands = new List<CarBrand>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT Id, CarBrandName FROM CarBrands WHERE CarBrandName LIKE @Name";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", $"%{name}%");
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                brands.Add(new CarBrand
                                {
                                    Id = reader.GetInt32(0),
                                    CarBrandName = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine($"SearchByName: Found {brands.Count} brands matching '{name}'.");
                return brands;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchByName Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw new Exception("Ошибка при поиске марок.", ex);
            }
        }
    }
}
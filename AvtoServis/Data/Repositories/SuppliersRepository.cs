using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class SuppliersRepository : ISuppliersRepository
    {
        private readonly string _connectionString;

        public SuppliersRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<Supplier> GetAll()
        {
            var suppliers = new List<Supplier>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT SupplierID, Name, ContactPhone, Email, Address
                          FROM Suppliers", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new Supplier
                            {
                                SupplierID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ContactPhone = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Address = reader.IsDBNull(4) ? null : reader.GetString(4)
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: Загружено {suppliers.Count} поставщиков.");
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
            return suppliers;
        }

        public Supplier GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT SupplierID, Name, ContactPhone, Email, Address
                          FROM Suppliers
                          WHERE SupplierID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Supplier
                            {
                                SupplierID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ContactPhone = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Address = reader.IsDBNull(4) ? null : reader.GetString(4)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Ошибка: {ex.Message}");
                throw new Exception($"Поставщик с ID {id} не найден.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Add(Supplier entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"INSERT INTO Suppliers (Name, ContactPhone, Email, Address)
                          VALUES (@Name, @ContactPhone, @Email, @Address);
                          SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@ContactPhone", (object)entity.ContactPhone ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Email", (object)entity.Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Address", (object)entity.Address ?? DBNull.Value);
                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    entity.SupplierID = newId;
                    Debug.WriteLine($"Add: Поставщик добавлен с ID {newId}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Add SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при добавлении поставщика.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Update(Supplier entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.SupplierID <= 0)
                throw new ArgumentException("Некорректный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"UPDATE Suppliers
                          SET Name = @Name, ContactPhone = @ContactPhone, Email = @Email, Address = @Address
                          WHERE SupplierID = @SupplierID", connection);
                    command.Parameters.AddWithValue("@SupplierID", entity.SupplierID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@ContactPhone", (object)entity.ContactPhone ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Email", (object)entity.Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Address", (object)entity.Address ?? DBNull.Value);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Поставщик с ID {entity.SupplierID} не найден.");
                    Debug.WriteLine($"Update: Поставщик обновлен с ID {entity.SupplierID}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Update SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при обновлении поставщика.", ex);
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
                    var command = new SqlCommand("DELETE FROM Suppliers WHERE SupplierID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"Поставщик с ID {id} не найден.");
                    Debug.WriteLine($"Delete: Поставщик удален с ID {id}.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Delete SQL Ошибка: {ex.Message}");
                throw new Exception("Ошибка при удалении поставщика.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete Ошибка: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public List<Supplier> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Поисковый запрос не должен быть пустым.");

            var suppliers = new List<Supplier>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT SupplierID, Name, ContactPhone, Email, Address
                          FROM Suppliers
                          WHERE Name LIKE @SearchText
                          OR ContactPhone LIKE @SearchText
                          OR Email LIKE @SearchText
                          OR Address LIKE @SearchText", connection);
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new Supplier
                            {
                                SupplierID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ContactPhone = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Address = reader.IsDBNull(4) ? null : reader.GetString(4)
                            });
                        }
                    }
                }
                Debug.WriteLine($"Search: Найдено {suppliers.Count} поставщиков для запроса '{searchText}'.");
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
            return suppliers;
        }
    }
}
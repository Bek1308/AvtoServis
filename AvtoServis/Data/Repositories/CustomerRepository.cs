using AvtoServis.Data.Interfaces;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AvtoServis.Data.Repositories
{
    class CustomerRepository : ICustomerRepository
    {
        private readonly string connectionString;

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT CustomerID, FullName, Phone, Email, Address, RegistrationDate, IsActive
                        FROM Customers";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customer
                                {
                                    FullName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    RegistrationDate = reader.GetDateTime(5),
                                    IsActive = reader.GetBoolean(6)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка подключения к базе данных: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении списка клиентов: " + ex.Message, ex);
            }

            return customers;
        }

        public Customer GetById(int id)
        {
            Customer customer = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT CustomerID, FullName, Phone, Email, Address, RegistrationDate, IsActive
                        FROM Customers
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer = new Customer
                                {
                                    FullName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    RegistrationDate = reader.GetDateTime(5),
                                    IsActive = reader.GetBoolean(6)
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка подключения к базе данных: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении клиента по ID: " + ex.Message, ex);
            }

            return customer;
        }

        public void Add(Customer customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Customers (FullName, Phone, Email, Address, RegistrationDate, IsActive)
                        VALUES (@FullName, @Phone, @Email, @Address, @RegistrationDate, @IsActive)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", customer.FullName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Phone", customer.Phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", customer.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Address", customer.Address ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RegistrationDate", customer.RegistrationDate);
                        cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка подключения к базе данных: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при добавлении клиента: " + ex.Message, ex);
            }
        }

        public void Update(Customer customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Customers
                        SET FullName = @FullName, Phone = @Phone, Email = @Email, Address = @Address,
                            RegistrationDate = @RegistrationDate, IsActive = @IsActive
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        cmd.Parameters.AddWithValue("@FullName", customer.FullName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Phone", customer.Phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", customer.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Address", customer.Address ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RegistrationDate", customer.RegistrationDate);
                        cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Клиент с указанным ID не найден.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка подключения к базе данных: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при обновлении клиента: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        DELETE FROM Customers
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Клиент с указанным ID не найден.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка подключения к базе данных: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при удалении клиента: " + ex.Message, ex);
            }
        }

        public async Task<List<CustomerInfo>> SearchCustomersAsync(string searchTerm)
        {
            Console.WriteLine($"[CustomerRepository] SearchCustomers boshlandi, searchTerm: {searchTerm}, Vaqt: {DateTime.Now:HH:mm:ss.fff}");
            var customers = new List<CustomerInfo>();

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    Console.WriteLine("[CustomerRepository] Ma'lumotlar bazasi ulanishi ochildi");

                    // SQL so'rovini optimallashtirish
                    var query = "SELECT TOP 50 CustomerID, FullName, Phone, Email, Address, RegistrationDate, IsActive " +
                                "FROM Customers WHERE FullName LIKE @SearchTerm OR Phone LIKE @SearchTerm";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                        command.CommandTimeout = 5; // 5 sekundlik timeout

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                customers.Add(new CustomerInfo
                                {
                                    CustomerID = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    Phone = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    Email = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                    Address = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    RegistrationDate = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                                    IsActive = reader.IsDBNull(6) ? false : reader.GetBoolean(6)
                                });
                            }
                        }
                    }
                    Console.WriteLine($"[CustomerRepository] Qidiruv yakunlandi, topilgan: {customers.Count} ta mijoz");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"[CustomerRepository] SQL Xato: {ex.Message}\nStackTrace: {ex.StackTrace}\nSearchTerm: {searchTerm}");
                    throw new Exception("Ma'lumotlar bazasidan ma'lumot olishda xato yuz berdi.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CustomerRepository] Umumiy xato: {ex.Message}\nStackTrace: {ex.StackTrace}\nSearchTerm: {searchTerm}");
                    throw;
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                        Console.WriteLine("[CustomerRepository] Ma'lumotlar bazasi ulanishi yopildi");
                    }
                }
            }
            return customers;
        }

        public CustomerWithVehiclesDto GetCustomerWithVehicles(int customerId)
        {
            CustomerWithVehiclesDto customer = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // First, get customer details
                    string customerQuery = @"
                        SELECT CustomerID, FullName, Phone, Email, Address, RegistrationDate, IsActive
                        FROM Customers
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(customerQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer = new CustomerWithVehiclesDto
                                {
                                    CustomerID = reader.GetInt32(0),
                                    FullName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    RegistrationDate = reader.GetDateTime(5),
                                    IsActive = reader.GetBoolean(6),
                                    VehicleIds = new List<int>()
                                };
                            }
                        }
                    }

                    if (customer == null)
                    {
                        return null;
                    }

                    // Then, get associated vehicle IDs from CustomersCars
                    string vehiclesQuery = @"
                        SELECT CarModel_Id
                        FROM CustomersCars
                        WHERE CustomerId = @CustomerId";

                    using (SqlCommand cmd = new SqlCommand(vehiclesQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer.VehicleIds.Add(reader.GetInt32(0));
                            }
                        }
                    }
                    Console.WriteLine($"[CustomerRepository] GetCustomerWithVehicles completed, CustomerID: {customerId}, Vehicles found: {customer.VehicleIds.Count}");
                    return customer;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"[CustomerRepository] SQL Error in GetCustomerWithVehicles: {ex.Message}\nStackTrace: {ex.StackTrace}\nCustomerId: {customerId}");
                throw new Exception($"Ошибка при получении клиента с ID {customerId}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CustomerRepository] General Error in GetCustomerWithVehicles: {ex.Message}\nStackTrace: {ex.StackTrace}\nCustomerId: {customerId}");
                throw new Exception($"Ошибка при получении клиента с ID {customerId}: {ex.Message}", ex);
            }
        }

        public async Task<List<CustomerDebtInfoDto>> GetAllCustomersWithDebtDetailsAsync()
        {
            var customers = new List<CustomerDebtInfoDto>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string query = @"
WITH CustomerCars AS (
    SELECT 
        c.CustomerID,
        STRING_AGG(Cb.CarBrandName + ' ' + Cm.Model + ' ' + CAST(Cm.Year AS nvarchar(10)), ', ') AS CarModels
    FROM Customers c
    LEFT JOIN CustomersCars Cc ON c.CustomerID = Cc.CustomerId
    LEFT JOIN CarModels Cm ON Cc.CarModel_Id = Cm.Id 
    LEFT JOIN CarBrand Cb ON Cm.CarBrandId = Cb.Id
    GROUP BY c.CustomerID
),
PartsAgg AS (
    SELECT 
        pe.OperationID,
        SUM(pe.Quantity * pe.UnitPrice) AS Charge,
        SUM(ISNULL(pe.PaidAmount, 0)) AS Paid
    FROM PartsExpenses pe
    GROUP BY pe.OperationID
),
ServicesAgg AS (
    SELECT 
        so.OperationID,
        SUM(ISNULL(so.TotalAmount, ISNULL(so.Quantity,1) * ISNULL(s.Price,0))) AS Charge,
        SUM(ISNULL(so.PaidAmount, 0)) AS Paid
    FROM ServiceOrders so
    LEFT JOIN Services s ON so.ServiceID = s.ServiceID
    GROUP BY so.OperationID
),
CustomerDebt AS (
    SELECT 
        o.CustomerID,
        SUM(ISNULL(pa.Charge,0) + ISNULL(sa.Charge,0) - ISNULL(pa.Paid,0) - ISNULL(sa.Paid,0)) AS UmumiyQarz
    FROM Operations o
    LEFT JOIN PartsAgg pa ON o.OperationID = pa.OperationID
    LEFT JOIN ServicesAgg sa ON o.OperationID = sa.OperationID
    GROUP BY o.CustomerID
),
CustomerDebtDetails AS (
    SELECT 
        d.CustomerID,
        STRING_AGG(d.NimadanQarz, ', ') AS QarzTafsiloti
    FROM (
        SELECT 
            o.CustomerID,
            p.PartName + ':' + CAST((pe.Quantity * pe.UnitPrice - ISNULL(pe.PaidAmount,0)) AS nvarchar(50)) AS NimadanQarz
        FROM Operations o
        INNER JOIN PartsExpenses pe ON o.OperationID = pe.OperationID
        INNER JOIN Parts p ON pe.PartID = p.PartID
        WHERE (pe.Quantity * pe.UnitPrice) > ISNULL(pe.PaidAmount,0)
        UNION ALL
        SELECT 
            o.CustomerID,
            ISNULL(s.Name,'No Service') + ':' + CAST((ISNULL(so.TotalAmount, ISNULL(so.Quantity,1) * ISNULL(s.Price,0)) - ISNULL(so.PaidAmount,0)) AS nvarchar(50)) AS NimadanQarz
        FROM Operations o
        INNER JOIN ServiceOrders so ON o.OperationID = so.OperationID
        LEFT JOIN Services s ON so.ServiceID = s.ServiceID
        WHERE (ISNULL(so.TotalAmount, ISNULL(so.Quantity,1) * ISNULL(s.Price,0)) > ISNULL(so.PaidAmount,0))
    ) d
    GROUP BY d.CustomerID
)
SELECT 
    c.CustomerID,
    c.FullName,
    c.Phone,
    c.Email,
    c.Address,
    c.RegistrationDate,
    c.IsActive,
    cc.CarModels,
    cd.UmumiyQarz,
    cdDetails.QarzTafsiloti
FROM Customers c
LEFT JOIN CustomerCars cc ON c.CustomerID = cc.CustomerID
LEFT JOIN CustomerDebt cd ON c.CustomerID = cd.CustomerID
LEFT JOIN CustomerDebtDetails cdDetails ON c.CustomerID = cdDetails.CustomerID
ORDER BY c.CustomerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandTimeout = 30;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var customer = new CustomerDebtInfoDto
                                {
                                    CustomerID = reader.GetInt32(0),
                                    FullName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    RegistrationDate = reader.GetDateTime(5),
                                    IsActive = reader.GetBoolean(6),
                                    UmumiyQarz = reader.IsDBNull(8) ? 0 : reader.GetDecimal(8)
                                };

                                if (!reader.IsDBNull(7))
                                    customer.ParseCarModels(reader.GetString(7));

                                if (!reader.IsDBNull(9))
                                    customer.ParseDebtDetails(reader.GetString(9));

                                customers.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Mijozlar va qarzlar haqida ma'lumot olishda xato yuz berdi.", ex);
            }

            return customers;
        }


        public CustomerDebtInfoDto GetCustomerWithDebtDetailsById(int customerId)
        {
            CustomerDebtInfoDto customer = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
WITH CustomerCars AS (
    SELECT 
        c.CustomerID,
        STRING_AGG(Cb.CarBrandName + ' ' + Cm.Model + ' ' + CAST(Cm.Year AS nvarchar(10)), ', ') AS CarModels
    FROM Customers c
    LEFT JOIN CustomersCars Cc ON c.CustomerID = Cc.CustomerId
    LEFT JOIN CarModels Cm ON Cc.CarModel_Id = Cm.Id 
    LEFT JOIN CarBrand Cb ON Cm.CarBrandId = Cb.Id
    WHERE c.CustomerID = @CustomerID
    GROUP BY c.CustomerID
),
PartsAgg AS (
    SELECT 
        pe.OperationID,
        SUM(pe.Quantity * pe.UnitPrice) AS Charge,
        SUM(ISNULL(pe.PaidAmount,0)) AS Paid
    FROM PartsExpenses pe
    GROUP BY pe.OperationID
),
ServicesAgg AS (
    SELECT 
        so.OperationID,
        SUM(ISNULL(so.TotalAmount, ISNULL(so.Quantity,1) * ISNULL(s.Price,0))) AS Charge,
        SUM(ISNULL(so.PaidAmount,0)) AS Paid
    FROM ServiceOrders so
    LEFT JOIN Services s ON so.ServiceID = s.ServiceID
    GROUP BY so.OperationID
),
CustomerDebt AS (
    SELECT 
        o.CustomerID,
        SUM(ISNULL(pa.Charge,0) + ISNULL(sa.Charge,0) - ISNULL(pa.Paid,0) - ISNULL(sa.Paid,0)) AS UmumiyQarz
    FROM Operations o
    LEFT JOIN PartsAgg pa ON o.OperationID = pa.OperationID
    LEFT JOIN ServicesAgg sa ON o.OperationID = sa.OperationID
    WHERE o.CustomerID = @CustomerID
    GROUP BY o.CustomerID
),
CustomerDebtDetails AS (
    SELECT 
        d.CustomerID,
        STRING_AGG(d.NimadanQarz, ', ') AS QarzTafsiloti
    FROM (
        SELECT 
            o.CustomerID,
            p.PartName + ':' + CAST((pe.Quantity * pe.UnitPrice - ISNULL(pe.PaidAmount,0)) AS nvarchar(50)) AS NimadanQarz
        FROM Operations o
        INNER JOIN PartsExpenses pe ON o.OperationID = pe.OperationID
        INNER JOIN Parts p ON pe.PartID = p.PartID
        WHERE o.CustomerID = @CustomerID
          AND (pe.Quantity * pe.UnitPrice) > ISNULL(pe.PaidAmount,0)
        UNION ALL
        SELECT 
            o.CustomerID,
            ISNULL(s.Name,'No Service') + ':' + CAST((ISNULL(so.TotalAmount, ISNULL(so.Quantity,1) * ISNULL(s.Price,0)) - ISNULL(so.PaidAmount,0)) AS nvarchar(50)) AS NimadanQarz
        FROM Operations o
        INNER JOIN ServiceOrders so ON o.OperationID = so.OperationID
        LEFT JOIN Services s ON so.ServiceID = s.ServiceID
        WHERE o.CustomerID = @CustomerID
          AND (ISNULL(so.TotalAmount, ISNULL(so.Quantity,1) * ISNULL(s.Price,0)) > ISNULL(so.PaidAmount,0))
    ) d
    GROUP BY d.CustomerID
)
SELECT 
    c.CustomerID,
    c.FullName,
    c.Phone,
    c.Email,
    c.Address,
    c.RegistrationDate,
    c.IsActive,
    cc.CarModels,
    cd.UmumiyQarz,
    cdDetails.QarzTafsiloti
FROM Customers c
LEFT JOIN CustomerCars cc ON c.CustomerID = cc.CustomerID
LEFT JOIN CustomerDebt cd ON c.CustomerID = cd.CustomerID
LEFT JOIN CustomerDebtDetails cdDetails ON c.CustomerID = cdDetails.CustomerID
WHERE c.CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.CommandTimeout = 30;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer = new CustomerDebtInfoDto
                                {
                                    CustomerID = reader.GetInt32(0),
                                    FullName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    RegistrationDate = reader.GetDateTime(5),
                                    IsActive = reader.GetBoolean(6),
                                    UmumiyQarz = reader.IsDBNull(8) ? 0 : reader.GetDecimal(8)
                                };

                                if (!reader.IsDBNull(7))
                                    customer.ParseCarModels(reader.GetString(7));

                                if (!reader.IsDBNull(9))
                                    customer.ParseDebtDetails(reader.GetString(9));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Mijoz (ID: {customerId}) va qarzlar haqida ma'lumot olishda xato yuz berdi.", ex);
            }

            return customer;
        }

    }
}
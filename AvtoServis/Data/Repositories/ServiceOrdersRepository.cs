// DOCUMENT filename="ServiceOrdersRepository.cs"
using AvtoServis.Data.Interfaces;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using AvtoServis.Services.Core;
using DocumentFormat.OpenXml.Vml.Office;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class ServiceOrdersRepository : IServiceOrdersRepository
    {
        private readonly string _connectionString;

        public ServiceOrdersRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<ServiceOrder> GetAll()
        {
            var serviceOrders = new List<ServiceOrder>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT OrderID, CustomerID, VehicleID, ServiceID, OperationID, UserID, OrderDate, StatusID, Quantity, PaidAmount, FinanceStatusID, TotalAmount
                  FROM ServiceOrders", connection);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            serviceOrders.Add(new ServiceOrder
                            {
                                OrderID = reader.GetInt32(0),
                                CustomerID = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                VehicleID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                ServiceID = reader.GetInt32(3),
                                OperationID = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                UserID = reader.GetInt32(5),
                                OrderDate = reader.GetDateTime(6),
                                StatusID = reader.GetInt32(7),
                                Quantity = reader.GetInt32(8),
                                PaidAmount = reader.GetDecimal(9),
                                FinanceStatusID = reader.GetInt32(10),
                                TotalAmount = reader.IsDBNull(11) ? (int?)null : reader.GetDecimal(11),
                            });
                        }
                    }
                }

                Debug.WriteLine($"Получено {serviceOrders.Count} сервисных заказов.");
                return serviceOrders;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Ошибка SQL в GetAll: {ex.Message}");
                throw new Exception("Ошибка в базе данных.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в GetAll: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public ServiceOrder GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Неверный ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT OrderID, CustomerID, VehicleID, ServiceID, OperationID, UserID, OrderDate, StatusID, Quantity, PaidAmount, FinanceStatusID, TotalAmount
                          FROM ServiceOrders
                          WHERE OrderID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ServiceOrder
                            {
                                OrderID = reader.GetInt32(0),
                                CustomerID = reader.GetInt32(1),
                                VehicleID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                ServiceID = reader.GetInt32(3),
                                OperationID = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                UserID = reader.GetInt32(5),
                                OrderDate = reader.GetDateTime(6),
                                StatusID = reader.GetInt32(7),
                                Quantity = reader.GetInt32(8),
                                PaidAmount = reader.GetDecimal(9), // Новый столбец
                                FinanceStatusID = reader.GetInt32(10), // Новый столбец
                                TotalAmount = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11),
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Ошибка SQL в GetById: {ex.Message}");
                throw new Exception($"Сервисный заказ с ID {id} не найден.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в GetById: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Add(ServiceOrder entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.Quantity <= 0)
                throw new ArgumentException("Количество должно быть положительным.");
            if (entity.PaidAmount < 0)
                throw new ArgumentException("Сумма оплаты не может быть отрицательной.");
            if (entity.FinanceStatusID < 1 || entity.FinanceStatusID > 3)
                throw new ArgumentException("FinanceStatusID должен быть 1 (Оплачен), 2 (Не оплачен) или 3 (Частично оплачен).");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Запись операции в журнал
                            var operationCommand = new SqlCommand(
                                @"INSERT INTO Operations (OperationTypeID, CustomerID, TotalSum, StatusID, OperationDate, Description, UserID)
                                  VALUES (@OperationTypeID, @CustomerID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID);
                                  SELECT SCOPE_IDENTITY();", connection, transaction);

                            var currentUser = CurrentUser.Instance;
                            var user = currentUser.User;
                            string financeStatus = entity.FinanceStatusID switch
                            {
                                1 => "Оплачен",
                                2 => "Не оплачен",
                                3 => "Частично оплачен",
                                _ => "Неизвестный статус"
                            };
                            string description = $"Новый сервисный заказ: OrderID (новый), CustomerID: {entity.CustomerID}, ServiceID: {entity.ServiceID}, Количество: {entity.Quantity}, Оплачено: {entity.PaidAmount}, Статус оплаты: {financeStatus}";

                        
                            
                            operationCommand.Parameters.AddWithValue("@OperationTypeID", 5);
                            operationCommand.Parameters.AddWithValue("@CustomerID", entity.CustomerID);
                            operationCommand.Parameters.AddWithValue("@TotalSum", entity.TotalAmount);
                            operationCommand.Parameters.AddWithValue("@StatusID", 1);
                            operationCommand.Parameters.AddWithValue("@OperationDate", entity.OrderDate);
                            operationCommand.Parameters.AddWithValue("@Description", description);
                            operationCommand.Parameters.AddWithValue("@UserID", user.UserID);

                            int operationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                            // Добавление в таблицу ServiceOrders
                            var orderCommand = new SqlCommand(
                                @"INSERT INTO ServiceOrders (CustomerID, VehicleID, ServiceID, OperationID, UserID, OrderDate, StatusID, Quantity, PaidAmount, FinanceStatusID, TotalAmount)
                                  VALUES (@CustomerID, @VehicleID, @ServiceID, @OperationID, @UserID, @OrderDate, @StatusID, @Quantity, @PaidAmount, @FinanceStatusID, @TotalAmout);
                                  SELECT SCOPE_IDENTITY();", connection, transaction);

                            orderCommand.Parameters.AddWithValue("@CustomerID", entity.CustomerID);
                            orderCommand.Parameters.AddWithValue("@VehicleID", entity.VehicleID == 0 ? (object)DBNull.Value : entity.VehicleID);
                            orderCommand.Parameters.AddWithValue("@ServiceID", entity.ServiceID);
                            orderCommand.Parameters.AddWithValue("@OperationID", operationID);
                            orderCommand.Parameters.AddWithValue("@UserID", user.UserID);
                            orderCommand.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
                            orderCommand.Parameters.AddWithValue("@StatusID", entity.StatusID);
                            orderCommand.Parameters.AddWithValue("@Quantity", entity.Quantity);
                            orderCommand.Parameters.AddWithValue("@PaidAmount", entity.PaidAmount);
                            orderCommand.Parameters.AddWithValue("@FinanceStatusID", entity.FinanceStatusID);
                            orderCommand.Parameters.AddWithValue("@TotalAmout", entity.TotalAmount);

                            int newId = Convert.ToInt32(orderCommand.ExecuteScalar());
                            entity.OrderID = newId;
                            entity.OperationID = operationID;

                            transaction.Commit();
                            Debug.WriteLine($"Добавлен сервисный заказ с ID {newId}, OperationID {operationID}, количеством {entity.Quantity}, оплачено {entity.PaidAmount}, статус оплаты {financeStatus}.");
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Ошибка SQL в Add: {ex.Message}");
                throw new Exception("Ошибка при добавлении сервисного заказа.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в Add: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void Update(ServiceOrder entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.Quantity <= 0)
                throw new ArgumentException("Количество должно быть положительным.");
            if (entity.PaidAmount < 0)
                throw new ArgumentException("Сумма оплаты не может быть отрицательной.");
            if (entity.FinanceStatusID < 1 || entity.FinanceStatusID > 3)
                throw new ArgumentException("FinanceStatusID должен быть 1 (Оплачен), 2 (Не оплачен) или 3 (Частично оплачен).");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Операция в журнал
                            var operationCommand = new SqlCommand(
                                @"INSERT INTO Operations (OperationTypeID, CustomerID, TotalSum, StatusID, OperationDate, Description, UserID, ParentOperationID)
                          VALUES (@OperationTypeID, @CustomerID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID, @ParentOperationID);
                          SELECT SCOPE_IDENTITY();", connection, transaction);

                            var currentUser = CurrentUser.Instance;
                            var user = currentUser.User;

                            string financeStatus = entity.FinanceStatusID switch
                            {
                                1 => "Оплачен",
                                2 => "Не оплачен",
                                3 => "Частично оплачен",
                                _ => "Неизвестный статус"
                            };

                            string description =
                                $"Обновлён сервисный заказ: OrderID: {entity.OrderID}, CustomerID: {entity.CustomerID}, " +
                                $"ServiceID: {entity.ServiceID}, Количество: {entity.Quantity}, Оплачено: {entity.PaidAmount}, " +
                                $"Статус оплаты: {financeStatus}";

                            // TotalSum = Quantity * Price
                            decimal servicePrice = GetServicePrice(entity.ServiceID);
                            decimal totalSum = entity.Quantity * servicePrice;

                            operationCommand.Parameters.Add("@OperationTypeID", SqlDbType.Int).Value = 5;
                            operationCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = entity.CustomerID;
                            operationCommand.Parameters.Add("@TotalSum", SqlDbType.Decimal).Value = totalSum;
                            operationCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = entity.StatusID;
                            operationCommand.Parameters.Add("@OperationDate", SqlDbType.DateTime).Value = entity.OrderDate;
                            operationCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 500).Value = description;
                            operationCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserID;
                            operationCommand.Parameters.Add("@ParentOperationID", SqlDbType.Int).Value = entity.OperationID;

                            int newOperationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                            // Обновление ServiceOrders
                            var orderCommand = new SqlCommand(
                                @"UPDATE ServiceOrders
                          SET CustomerID = @CustomerID, VehicleID = @VehicleID, ServiceID = @ServiceID,
                              OperationID = @OperationID, UserID = @UserID, OrderDate = @OrderDate, 
                              StatusID = @StatusID, Quantity = @Quantity, PaidAmount = @PaidAmount, 
                              FinanceStatusID = @FinanceStatusID, TotalAmount = @TotalAmount
                          WHERE OrderID = @OrderID", connection, transaction);

                            orderCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = entity.OrderID;
                            orderCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = entity.CustomerID;

                            // ✅ VehicleID null bo‘lsa, DBNull.Value yuboriladi
                            orderCommand.Parameters.Add("@VehicleID", SqlDbType.Int).Value =
                                entity.VehicleID.HasValue ? (object)entity.VehicleID.Value : DBNull.Value;

                            orderCommand.Parameters.Add("@ServiceID", SqlDbType.Int).Value = entity.ServiceID;
                            orderCommand.Parameters.Add("@OperationID", SqlDbType.Int).Value = newOperationID;
                            orderCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = entity.UserID;
                            orderCommand.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = entity.OrderDate;
                            orderCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = entity.StatusID;
                            orderCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = entity.Quantity;
                            orderCommand.Parameters.Add("@PaidAmount", SqlDbType.Decimal).Value = entity.PaidAmount;
                            orderCommand.Parameters.Add("@FinanceStatusID", SqlDbType.Int).Value = entity.FinanceStatusID;
                            orderCommand.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = entity.TotalAmount;

                            int rowsAffected = orderCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                                throw new InvalidOperationException("Сервисный заказ не обновлён.");

                            entity.OperationID = newOperationID;

                            transaction.Commit();
                            Debug.WriteLine($"Обновлён сервисный заказ с ID {entity.OrderID}, новой OperationID {newOperationID}, количеством {entity.Quantity}, оплачено {entity.PaidAmount}, статус оплаты {financeStatus}.");
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Ошибка SQL в Update: {ex.Message}");
                throw new Exception("Ошибка при обновлении сервисного заказа.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в Update: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        public void ReturnService(ServiceOrder serviceOrder)
        {
            if (serviceOrder == null)
                throw new ArgumentNullException(nameof(serviceOrder));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Операция в журнал
                            var operationCommand = new SqlCommand(
                                @"INSERT INTO Operations (OperationTypeID, CustomerID, TotalSum, StatusID, OperationDate, Description, UserID, ParentOperationID)
                         VALUES (@OperationTypeID, @CustomerID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID, @ParentOperationID);
                         SELECT SCOPE_IDENTITY();", connection, transaction);

                            var currentUser = CurrentUser.Instance;
                            var user = currentUser.User;

                            string description =
                                $"Возвращён сервисный заказ: OrderID: {serviceOrder.OrderID}, " +
                                $"CustomerID: {serviceOrder.CustomerID}, ServiceID: {serviceOrder.ServiceID}, " +
                                $"Статус оплаты: Оплачен";

                            operationCommand.Parameters.Add("@OperationTypeID", SqlDbType.Int).Value = 3; // Предполагаемый тип для возврата
                            operationCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = (object)serviceOrder.CustomerID ?? DBNull.Value;
                            operationCommand.Parameters.Add("@TotalSum", SqlDbType.Decimal).Value = 0m;
                            operationCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = serviceOrder.StatusID;
                            operationCommand.Parameters.Add("@OperationDate", SqlDbType.DateTime).Value = DateTime.Now;
                            operationCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 500).Value = description;
                            operationCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserID;
                            operationCommand.Parameters.Add("@ParentOperationID", SqlDbType.Int).Value = (object)serviceOrder.OperationID ?? DBNull.Value;

                            int newOperationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                            // Обновление ServiceOrders
                            var orderCommand = new SqlCommand(
                                @"UPDATE ServiceOrders
                         SET CustomerID = @CustomerID, VehicleID = @VehicleID, ServiceID = @ServiceID,
                             OperationID = @OperationID, UserID = @UserID, OrderDate = @OrderDate, 
                             StatusID = @StatusID, Quantity = @Quantity, PaidAmount = @PaidAmount, 
                             FinanceStatusID = @FinanceStatusID, TotalAmount = @TotalAmount
                         WHERE OrderID = @OrderID", connection, transaction);

                            orderCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = serviceOrder.OrderID;
                            orderCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = serviceOrder.CustomerID;
                            // ✅ VehicleID null bo‘lsa, DBNull.Value yuboriladi
                            orderCommand.Parameters.Add("@VehicleID", SqlDbType.Int).Value =
                                serviceOrder.VehicleID.HasValue ? (object)serviceOrder.VehicleID.Value : DBNull.Value;
                            orderCommand.Parameters.Add("@ServiceID", SqlDbType.Int).Value = serviceOrder.ServiceID;
                            orderCommand.Parameters.Add("@OperationID", SqlDbType.Int).Value = newOperationID;
                            orderCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserID;
                            orderCommand.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = DateTime.Now;
                            orderCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = serviceOrder.StatusID;
                            orderCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = 0;
                            orderCommand.Parameters.Add("@PaidAmount", SqlDbType.Decimal).Value = 0m;
                            orderCommand.Parameters.Add("@FinanceStatusID", SqlDbType.Int).Value = 1; // Оплачен
                            orderCommand.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = DBNull.Value;

                            int rowsAffected = orderCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                                throw new InvalidOperationException("Сервисный заказ не возвращён.");

                            serviceOrder.CustomerID = null;
                            serviceOrder.VehicleID = null;
                            serviceOrder.Quantity = 0;
                            serviceOrder.PaidAmount = 0m;
                            serviceOrder.FinanceStatusID = 1; // Оплачен
                            serviceOrder.TotalAmount = null;
                            serviceOrder.OperationID = newOperationID;
                            serviceOrder.OrderDate = DateTime.Now;

                            transaction.Commit();
                            Debug.WriteLine($"Возвращён сервисный заказ с ID {serviceOrder.OrderID}, новой OperationID {newOperationID}, статус оплаты: Оплачен.");
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Ошибка SQL в ReturnService: {ex.Message}");
                throw new Exception("Ошибка при возврате сервисного заказа.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в ReturnService: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }


        public void Delete(int orderId, string reason)
        {
            if (orderId <= 0)
                throw new ArgumentException("OrderID должен быть положительным целым числом.");
            if (string.IsNullOrEmpty(reason))
                throw new ArgumentException("Причина удаления не может быть пустой.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Получение данных ServiceOrders
                            var orderCommand = new SqlCommand(
                                @"SELECT CustomerID, OrderDate, ServiceID, OperationID, Quantity, PaidAmount, FinanceStatusID
                                  FROM ServiceOrders
                                  WHERE OrderID = @OrderID", connection, transaction);
                            orderCommand.Parameters.AddWithValue("@OrderID", orderId);
                            int customerId = 0;
                            DateTime orderDate = DateTime.Now;
                            int serviceId = 0;
                            int operationId = 0;
                            int quantity = 0;
                            decimal paidAmount = 0;
                            int financeStatusId = 0;

                            using (var reader = orderCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    customerId = reader.GetInt32(0);
                                    orderDate = reader.GetDateTime(1);
                                    serviceId = reader.GetInt32(2);
                                    operationId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                                    quantity = reader.GetInt32(4);
                                    paidAmount = reader.GetDecimal(5);
                                    financeStatusId = reader.GetInt32(6);
                                }
                                else
                                {
                                    throw new InvalidOperationException("Сервисный заказ не найден.");
                                }
                            }

                            // Запись операции удаления
                            var operationCommand = new SqlCommand(
                                @"INSERT INTO Operations (OperationTypeID, CustomerID, TotalSum, StatusID, OperationDate, Description, UserID, ParentOperationID)
                                  VALUES (@OperationTypeID, @CustomerID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID, @ParentOperationID);
                                  SELECT SCOPE_IDENTITY();", connection, transaction);

                            var currentUser = CurrentUser.Instance;
                            var user = currentUser.User;
                            string financeStatus = financeStatusId switch
                            {
                                1 => "Оплачен",
                                2 => "Не оплачен",
                                3 => "Частично оплачен",
                                _ => "Неизвестный статус"
                            };
                            string description = $"Удалён сервисный заказ: OrderID: {orderId}, Количество: {quantity}, Оплачено: {paidAmount}, Статус оплаты: {financeStatus}, Причина: {reason}";

                            // TotalSum = Quantity * Price
                            decimal servicePrice = GetServicePrice(serviceId);
                            decimal totalSum = quantity * servicePrice;
                            operationCommand.Parameters.AddWithValue("@OperationTypeID", 5);
                            operationCommand.Parameters.AddWithValue("@CustomerID", customerId);
                            operationCommand.Parameters.AddWithValue("@TotalSum", totalSum);
                            operationCommand.Parameters.AddWithValue("@StatusID", 3);
                            operationCommand.Parameters.AddWithValue("@OperationDate", orderDate);
                            operationCommand.Parameters.AddWithValue("@Description", description);
                            operationCommand.Parameters.AddWithValue("@UserID", user.UserID);
                            operationCommand.Parameters.AddWithValue("@ParentOperationID", operationId);

                            int deleteOperationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                            // Удаление из ServiceOrders
                            var deleteCommand = new SqlCommand(
                                @"DELETE FROM ServiceOrders
                                  WHERE OrderID = @OrderID", connection, transaction);
                            deleteCommand.Parameters.AddWithValue("@OrderID", orderId);

                            int rowsAffected = deleteCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                                throw new InvalidOperationException("Сервисный заказ не удалён.");

                            transaction.Commit();
                            Debug.WriteLine($"Удалён сервисный заказ с ID {orderId}, OperationID {operationId}, количеством {quantity}, оплачено {paidAmount}, статус оплаты {financeStatus}.");
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Ошибка SQL в Delete: {ex.Message}");
                throw new Exception("Ошибка при удалении сервисного заказа.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в Delete: {ex.Message}");
                throw new Exception("Произошла неизвестная ошибка.", ex);
            }
        }

        // Топ 30 самых продаваемых услуг (без изменений)
        public List<FullService> GetTopSellingServices()
        {
            var services = new List<FullService>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                        WITH TopServices AS (
                            SELECT 
                                so.ServiceID,
                                SUM(so.Quantity) AS SoldCount
                            FROM ServiceOrders so
                            GROUP BY so.ServiceID
                            ORDER BY SoldCount DESC
                            OFFSET 0 ROWS FETCH NEXT 30 ROWS ONLY
                        )
                        SELECT 
                            s.ServiceID,
                            s.Name,
                            s.Price,
                            ts.SoldCount
                        FROM 
                            TopServices ts
                            INNER JOIN Services s ON ts.ServiceID = s.ServiceID
                        ORDER BY 
                            ts.SoldCount DESC;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var fullService = new FullService
                                {
                                    ServiceID = reader.GetInt32(0),
                                    Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Price = reader.GetDecimal(2),
                                    SoldCount = reader.GetInt32(3),
                                    TotalRevenue = reader.GetInt32(3) * reader.GetDecimal(2)
                                };
                                services.Add(fullService);
                            }
                        }
                    }
                }
                Debug.WriteLine($"Получено {services.Count} самых продаваемых услуг.");
                return services;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Ошибка SQL в GetTopSellingServices: {ex.Message}");
                throw new Exception("Ошибка при получении самых продаваемых услуг: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в GetTopSellingServices: {ex.Message}");
                throw new Exception("Ошибка при получении данных: " + ex.Message, ex);
            }
        }

        public List<FullService> GetFullServices()
        {
            var services = new List<FullService>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    s.ServiceID,
                    s.Name,
                    s.Price,
                    ISNULL(SUM(so.Quantity), 0) AS SoldCount
                FROM 
                    Services s
                    LEFT JOIN ServiceOrders so ON s.ServiceID = so.ServiceID
                GROUP BY 
                    s.ServiceID, s.Name, s.Price
                ORDER BY 
                    SoldCount DESC;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var fullService = new FullService
                                {
                                    ServiceID = reader.GetInt32(0),
                                    Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Price = reader.GetDecimal(2),
                                    SoldCount = reader.GetInt32(3),
                                    TotalRevenue = reader.GetInt32(3) * reader.GetDecimal(2)
                                };
                                services.Add(fullService);
                            }
                        }
                    }
                }

                Debug.WriteLine($"Получено {services.Count} всех услуг (включая не проданные).");
                return services;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Ошибка SQL в GetFullServices: {ex.Message}");
                throw new Exception("Ошибка при получении всех услуг: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в GetFullServices: {ex.Message}");
                throw new Exception("Ошибка при получении данных: " + ex.Message, ex);
            }
        }


        // Вспомогательный метод: Получение цены по ServiceID
        private decimal GetServicePrice(int serviceId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    @"SELECT Price FROM Services WHERE ServiceID = @ServiceID", connection);
                command.Parameters.AddWithValue("@ServiceID", serviceId);
                var result = command.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0m;
            }
        }
        public FullService? GetServiceById(int serviceId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT 
                    s.ServiceID,
                    s.Name,
                    s.Price,
                    ISNULL(SUM(so.Quantity), 0) AS SoldCount
                FROM Services s
                LEFT JOIN ServiceOrders so ON s.ServiceID = so.ServiceID
                WHERE s.ServiceID = @ServiceID
                GROUP BY s.ServiceID, s.Name, s.Price;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceID", serviceId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var fullService = new FullService
                                {
                                    ServiceID = reader.GetInt32(0),
                                    Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Price = reader.GetDecimal(2),
                                    SoldCount = reader.GetInt32(3),
                                    TotalRevenue = reader.GetInt32(3) * reader.GetDecimal(2)
                                };

                                return fullService;
                            }
                        }
                    }
                }

                // Agar topilmasa
                return null;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Ошибка SQL в GetServiceById: {ex.Message}");
                throw new Exception("Ошибка при получении услуги по ID: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в GetServiceById: {ex.Message}");
                throw new Exception("Ошибка при получении данных: " + ex.Message, ex);
            }
        }

    }
}

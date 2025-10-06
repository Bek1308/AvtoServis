using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Models;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using AvtoServis.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AvtoServis.Data.Repositories
{
    public class PartsIncomeRepository : IPartsIncomeRepository
    {
        private readonly string _connectionString;
       

        public PartsIncomeRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<PartsIncome> GetAll()
        {
            var partsIncomes = new List<PartsIncome>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT IncomeID, PartID, SupplierID, Date, Quantity, UnitPrice, Markup, 
                                 StatusID, Finance_Status_Id, OperationID, StockID, InvoiceNumber, 
                                 UserID, PaidAmount, BatchID
                          FROM PartsIncome", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            partsIncomes.Add(new PartsIncome
                            {
                                IncomeID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                PartID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                SupplierID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                Date = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                                Quantity = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                UnitPrice = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5),
                                Markup = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6),
                                StatusID = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                Finance_Status_Id = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                OperationID = reader.IsDBNull(9) ? 0 : reader.GetInt32(9),
                                StockID = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                                InvoiceNumber = reader.IsDBNull(11) ? null : reader.GetString(11),
                                UserID = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                                PaidAmount = reader.IsDBNull(13) ? 0 : reader.GetDecimal(13),
                                BatchID = reader.IsDBNull(14) ? 0 : reader.GetInt32(14)
                            });
                        }
                    }
                }
                Debug.WriteLine($"GetAll: {partsIncomes.Count} ta qism kiritildi.");
                return partsIncomes;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetAll SQL Xatosi: {ex.Message}");
                throw new Exception("Ma'lumotlar bazasida xato.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetAll Xatosi: {ex.Message}");
                throw new Exception("Noma'lum xato yuz berdi.", ex);
            }
        }

        public PartsIncome GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Noto'g'ri ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT IncomeID, PartID, SupplierID, Date, Quantity, UnitPrice, Markup, 
                                 StatusID, Finance_Status_Id, OperationID, StockID, InvoiceNumber, 
                                 UserID, PaidAmount, BatchID
                          FROM PartsIncome
                          WHERE IncomeID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PartsIncome
                            {
                                IncomeID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                PartID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                SupplierID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                Date = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                                Quantity = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                UnitPrice = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5),
                                Markup = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6),
                                StatusID = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                Finance_Status_Id = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                OperationID = reader.IsDBNull(9) ? 0 : reader.GetInt32(9),
                                StockID = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                                InvoiceNumber = reader.IsDBNull(11) ? null : reader.GetString(11),
                                UserID = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                                PaidAmount = reader.IsDBNull(13) ? 0 : reader.GetDecimal(13),
                                BatchID = reader.IsDBNull(14) ? 0 : reader.GetInt32(14)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Xatosi: {ex.Message}");
                throw new Exception($"ID {id} bilan qism topilmadi.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Xatosi: {ex.Message}");
                throw new Exception("Noma'lum xato yuz berdi.", ex);
            }
        }
        public PartsIncome GetByPartId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Noto'g'ri ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT IncomeID, PartID, SupplierID, Date, Quantity, UnitPrice, Markup, 
                                 StatusID, Finance_Status_Id, OperationID, StockID, InvoiceNumber, 
                                 UserID, PaidAmount, BatchID
                          FROM PartsIncome
                          WHERE PartID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PartsIncome
                            {
                                IncomeID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                PartID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                SupplierID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                Date = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                                Quantity = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                UnitPrice = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5),
                                Markup = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6),
                                StatusID = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                Finance_Status_Id = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                OperationID = reader.IsDBNull(9) ? 0 : reader.GetInt32(9),
                                StockID = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                                InvoiceNumber = reader.IsDBNull(11) ? null : reader.GetString(11),
                                UserID = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                                PaidAmount = reader.IsDBNull(13) ? 0 : reader.GetDecimal(13),
                                BatchID = reader.IsDBNull(14) ? 0 : reader.GetInt32(14)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Xatosi: {ex.Message}");
                throw new Exception($"ID {id} bilan qism topilmadi.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Xatosi: {ex.Message}");
                throw new Exception("Noma'lum xato yuz berdi.", ex);
            }
        }

        public PartsIncome GetByIncomeId(int? id)
        {
            if (id <= 0)
                throw new ArgumentException("Noto'g'ri ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"SELECT IncomeID, PartID, SupplierID, Date, Quantity, UnitPrice, Markup, 
                                 StatusID, Finance_Status_Id, OperationID, StockID, InvoiceNumber, 
                                 UserID, PaidAmount, BatchID
                          FROM PartsIncome
                          WHERE IncomeID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PartsIncome
                            {
                                IncomeID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                PartID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                SupplierID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                Date = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                                Quantity = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                UnitPrice = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5),
                                Markup = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6),
                                StatusID = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                Finance_Status_Id = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                OperationID = reader.IsDBNull(9) ? 0 : reader.GetInt32(9),
                                StockID = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                                InvoiceNumber = reader.IsDBNull(11) ? null : reader.GetString(11),
                                UserID = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                                PaidAmount = reader.IsDBNull(13) ? 0 : reader.GetDecimal(13),
                                BatchID = reader.IsDBNull(14) ? 0 : reader.GetInt32(14)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetById SQL Xatosi: {ex.Message}");
                throw new Exception($"ID {id} bilan qism topilmadi.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetById Xatosi: {ex.Message}");
                throw new Exception("Noma'lum xato yuz berdi.", ex);
            }
        }
        public void Add(PartsIncome entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"INSERT INTO PartsIncome (PartID, SupplierID, Date, Quantity, UnitPrice, Markup, 
                                                  StatusID, Finance_Status_Id, OperationID, StockID, InvoiceNumber, 
                                                  UserID, PaidAmount, BatchID)
                          VALUES (@PartID, @SupplierID, @Date, @Quantity, @UnitPrice, @Markup, 
                                  @StatusID, @Finance_Status_Id, @OperationID, @StockID, @InvoiceNumber, 
                                  @UserID, @PaidAmount, @BatchID);
                          SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@PartID", entity.PartID);
                    command.Parameters.AddWithValue("@SupplierID", entity.SupplierID);
                    command.Parameters.AddWithValue("@Date", entity.Date);
                    command.Parameters.AddWithValue("@Quantity", entity.Quantity);
                    command.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
                    command.Parameters.AddWithValue("@Markup", entity.Markup);
                    command.Parameters.AddWithValue("@StatusID", entity.StatusID);
                    command.Parameters.AddWithValue("@Finance_Status_Id", entity.Finance_Status_Id);
                    command.Parameters.AddWithValue("@OperationID", entity.OperationID);
                    command.Parameters.AddWithValue("@StockID", entity.StockID);
                    command.Parameters.AddWithValue("@InvoiceNumber", (object)entity.InvoiceNumber ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UserID", entity.UserID);
                    command.Parameters.AddWithValue("@PaidAmount", entity.PaidAmount);
                    command.Parameters.AddWithValue("@BatchID", entity.BatchID);
                    var newId = Convert.ToInt32(command.ExecuteScalar());
                    entity.IncomeID = newId;
                    Debug.WriteLine($"Add: Qism {newId} ID bilan qo'shildi.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Add SQL Xatosi: {ex.Message}");
                throw new Exception("Qism qo'shishda xato.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Xatosi: {ex.Message}");
                throw new Exception("Noma'lum xato yuz berdi.", ex);
            }
        }

        public void UpdatePartsIncomes(PartsIncome income, string batchName)
        {
            if (income == null)
                throw new ArgumentException("PartsIncome cannot be null.");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int batchID;
                        var checkBatchCommand = new SqlCommand(
                            @"SELECT BatchID FROM Batches WHERE Name = @Name",
                            connection, transaction);
                        checkBatchCommand.Parameters.AddWithValue("@Name", batchName);

                        var result = checkBatchCommand.ExecuteScalar();
                        if (result != null)
                        {
                            batchID = Convert.ToInt32(result);
                        }
                        else
                        {
                            var batchCommand = new SqlCommand(
                                @"INSERT INTO Batches (Name)
                                  VALUES (@Name);
                                  SELECT SCOPE_IDENTITY();",
                                connection, transaction);
                            batchCommand.Parameters.AddWithValue("@Name", batchName);
                            batchID = Convert.ToInt32(batchCommand.ExecuteScalar());
                        }

                        string oldBatchName;
                        var getOldBatchCommand = new SqlCommand(
                            @"SELECT Name FROM Batches WHERE BatchID = (SELECT BatchID FROM PartsIncome WHERE IncomeID = @IncomeID)",
                            connection, transaction);
                        getOldBatchCommand.Parameters.AddWithValue("@IncomeID", income.IncomeID);
                        var oldBatchResult = getOldBatchCommand.ExecuteScalar();
                        oldBatchName = oldBatchResult != null ? oldBatchResult.ToString() : "Неизвестно";

                        var operationCommand = new SqlCommand(
                            @"INSERT INTO Operations (OperationTypeID, SupplierID, TotalSum, StatusID, OperationDate, Description, UserID, ParentOperationID)
                              VALUES (@OperationTypeID, @SupplierID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID, @ParentOperationID);
                              SELECT SCOPE_IDENTITY();",
                            connection, transaction);

                        decimal totalSum = income.Quantity * income.UnitPrice;
                        string description = $"Изменения прохода: PartsIncome ID: {income.IncomeID}, OperationID: (создано новый), ParentOperationID: {income.OperationID}, Старое имя партии: {oldBatchName}, Новое имя партии: {batchName}";
                        var currentUser = CurrentUser.Instance;
                        var user = currentUser.User;

                        operationCommand.Parameters.AddWithValue("@OperationTypeID", 1);
                        operationCommand.Parameters.AddWithValue("@SupplierID", income.SupplierID);
                        operationCommand.Parameters.AddWithValue("@TotalSum", totalSum);
                        operationCommand.Parameters.AddWithValue("@StatusID", 1);
                        operationCommand.Parameters.AddWithValue("@OperationDate", income.Date);
                        operationCommand.Parameters.AddWithValue("@Description", description);
                        operationCommand.Parameters.AddWithValue("@UserID", user.UserID);
                        operationCommand.Parameters.AddWithValue("@ParentOperationID", income.OperationID);

                        int operationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                        var incomeCommand = new SqlCommand(
                            @"UPDATE PartsIncome 
                              SET PartID = @PartID, SupplierID = @SupplierID, Date = @Date, Quantity = @Quantity, 
                                  UnitPrice = @UnitPrice, Markup = @Markup, StatusID = @StatusID, 
                                  Finance_Status_Id = @FinanceStatusId, OperationID = @OperationID, 
                                  StockID = @StockID, InvoiceNumber = @InvoiceNumber, UserID = @UserID, 
                                  PaidAmount = @PaidAmount, BatchID = @BatchID
                              WHERE IncomeID = @IncomeID;",
                            connection, transaction);

                        decimal totalAmount = income.Quantity * income.UnitPrice;
                        int financeStatusId = 0; // Initialize the variable to avoid CS0165

                        if (income.PaidAmount == 0)
                        {
                            financeStatusId = 2;
                        }
                        else if (income.PaidAmount == totalAmount)
                        {
                            financeStatusId = 1;
                        }
                        else if (income.PaidAmount < totalAmount)
                        {
                            financeStatusId = 3;
                        }

                        income.Finance_Status_Id = financeStatusId;

                        incomeCommand.Parameters.AddWithValue("@IncomeID", income.IncomeID);
                        incomeCommand.Parameters.AddWithValue("@PartID", income.PartID);
                        incomeCommand.Parameters.AddWithValue("@SupplierID", income.SupplierID);
                        incomeCommand.Parameters.AddWithValue("@Date", income.Date);
                        incomeCommand.Parameters.AddWithValue("@Quantity", income.Quantity);
                        incomeCommand.Parameters.AddWithValue("@UnitPrice", income.UnitPrice);
                        incomeCommand.Parameters.AddWithValue("@Markup", income.Markup);
                        incomeCommand.Parameters.AddWithValue("@StatusID", income.StatusID);
                        incomeCommand.Parameters.AddWithValue("@FinanceStatusId", financeStatusId);
                        incomeCommand.Parameters.AddWithValue("@OperationID", operationID);
                        incomeCommand.Parameters.AddWithValue("@StockID", income.StockID);
                        incomeCommand.Parameters.AddWithValue("@InvoiceNumber", income.InvoiceNumber ?? (object)DBNull.Value);
                        incomeCommand.Parameters.AddWithValue("@UserID", user.UserID);
                        incomeCommand.Parameters.AddWithValue("@PaidAmount", income.PaidAmount);
                        incomeCommand.Parameters.AddWithValue("@BatchID", batchID);

                        incomeCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeletePartsIncome(int incomeId, string reason)
        {
            if (incomeId <= 0)
                throw new ArgumentException("IncomeID must be a valid positive integer.");
            if (string.IsNullOrEmpty(reason))
                throw new ArgumentException("Reason for deletion cannot be empty.");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Old batch name ni olish
                        string oldBatchName;
                        var getOldBatchCommand = new SqlCommand(
                            @"SELECT Name FROM Batches WHERE BatchID = (SELECT BatchID FROM PartsIncome WHERE IncomeID = @IncomeID)",
                            connection, transaction);
                        getOldBatchCommand.Parameters.AddWithValue("@IncomeID", incomeId);
                        var oldBatchResult = getOldBatchCommand.ExecuteScalar();
                        oldBatchName = oldBatchResult != null ? oldBatchResult.ToString() : "Неизвестно";

                        // Operatsiyani jurnalga yozish
                        var operationCommand = new SqlCommand(
                            @"INSERT INTO Operations (OperationTypeID, SupplierID, TotalSum, StatusID, OperationDate, Description, UserID, ParentOperationID)
                      VALUES (@OperationTypeID, @SupplierID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID, @ParentOperationID);
                      SELECT SCOPE_IDENTITY();",
                            connection, transaction);

                        var currentUser = CurrentUser.Instance;
                        var user = currentUser.User;
                        string description = $"Удаление прохода: PartsIncome ID: {incomeId}, Причина: {reason}, Имя партии: {oldBatchName}";

                        // PartsIncome ma'lumotlarini olish uchun
                        var incomeCommand = new SqlCommand(
                            @"SELECT SupplierID, Quantity, UnitPrice, OperationID, Date 
                      FROM PartsIncome WHERE IncomeID = @IncomeID",
                            connection, transaction);
                        incomeCommand.Parameters.AddWithValue("@IncomeID", incomeId);
                        var reader = incomeCommand.ExecuteReader();

                        decimal totalSum = 0;
                        int supplierId = 0;
                        int parentOperationId = 0;
                        DateTime operationDate = DateTime.Now;

                        if (reader.Read())
                        {
                            supplierId = reader.GetInt32(0); // SupplierID (int)
                            int quantity = reader.GetInt32(1); // Quantity (int)
                            decimal unitPrice = reader.GetDecimal(2); // UnitPrice (decimal)
                            parentOperationId = reader.GetInt32(3); // OperationID (int)
                            operationDate = reader.GetDateTime(4); // Date (datetime)
                            totalSum = quantity * unitPrice; // TotalSum hisoblash
                        }
                        reader.Close();

                        if (supplierId == 0)
                            throw new InvalidOperationException("PartsIncome record not found.");

                        int operationTypeId = reason == "Возврат товара" ? 4 : 10;
                        operationCommand.Parameters.AddWithValue("@OperationTypeID", operationTypeId); 
                        operationCommand.Parameters.AddWithValue("@SupplierID", supplierId);
                        operationCommand.Parameters.AddWithValue("@TotalSum", totalSum);
                        operationCommand.Parameters.AddWithValue("@StatusID", 3);
                        operationCommand.Parameters.AddWithValue("@OperationDate", operationDate);
                        operationCommand.Parameters.AddWithValue("@Description", description);
                        operationCommand.Parameters.AddWithValue("@UserID", user.UserID);
                        operationCommand.Parameters.AddWithValue("@ParentOperationID", parentOperationId);

                        int operationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                        // PartsIncome dan o'chirish
                        var deleteCommand = new SqlCommand(
                            @"DELETE FROM PartsIncome WHERE IncomeID = @IncomeID",
                            connection, transaction);
                        deleteCommand.Parameters.AddWithValue("@IncomeID", incomeId);

                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            throw new InvalidOperationException("No PartsIncome record was deleted.");

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void SavePartsIncomes(List<PartsIncome> partsIncomes, string batchName)
        {
            if (partsIncomes == null || !partsIncomes.Any())
                throw new ArgumentException("No data to save.");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Yangi Batch yaratish
                        var batchCommand = new SqlCommand(
                            @"INSERT INTO Batches (Name)
                              VALUES (@Name);
                              SELECT SCOPE_IDENTITY();", connection, transaction);

                        batchCommand.Parameters.AddWithValue("@Name", batchName);
                        int batchID = Convert.ToInt32(batchCommand.ExecuteScalar());

                        // 2. Har bir PartsIncome uchun Operation va PartsIncome yozuvini qo'shish
                        foreach (var income in partsIncomes)
                        {
                            // Yangi Operation yaratish (OperationTypeID = 1)
                            var operationCommand = new SqlCommand(
                                @"INSERT INTO Operations (OperationTypeID, SupplierID, TotalSum, StatusID, OperationDate, Description, UserID)
                                  VALUES (@OperationTypeID, @SupplierID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID);
                                  SELECT SCOPE_IDENTITY();", connection, transaction);

                            decimal totalSum = income.Quantity * income.UnitPrice;
                            var currentUser = CurrentUser.Instance;
                            var user = currentUser.User;

                            operationCommand.Parameters.AddWithValue("@OperationTypeID", 1); // Закупка
                            operationCommand.Parameters.AddWithValue("@SupplierID", income.SupplierID);
                            operationCommand.Parameters.AddWithValue("@TotalSum", totalSum);
                            operationCommand.Parameters.AddWithValue("@StatusID", 1); // Новый
                            operationCommand.Parameters.AddWithValue("@OperationDate", DateTime.Now);
                            operationCommand.Parameters.AddWithValue("@Description", $"Прихода для партии: {batchName}");
                            operationCommand.Parameters.AddWithValue("@UserID", user.UserID);

                            int operationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                            // PartsIncome yozuvini qo'shish
                            var incomeCommand = new SqlCommand(
                                @"INSERT INTO PartsIncome (PartID, SupplierID, Date, Quantity, UnitPrice, 
                                                          Markup, StatusID, Finance_Status_Id, OperationID, 
                                                          StockID, InvoiceNumber, UserID, PaidAmount, BatchID)
                                  VALUES (@PartID, @SupplierID, @Date, @Quantity, @UnitPrice, @Markup, 
                                          @StatusID, @FinanceStatusId, @OperationID, @StockID, 
                                          @InvoiceNumber, @UserID, @PaidAmount, @BatchID);
                                  SELECT SCOPE_IDENTITY();", connection, transaction);

                            decimal totalAmount = income.Quantity * income.UnitPrice;
                            int financeStatusId = 0; // Initialize the variable to avoid CS0165

                            if (income.PaidAmount == 0)
                            {
                                financeStatusId = 2;
                            }
                            else if (income.PaidAmount == totalAmount)
                            {
                                financeStatusId = 1;
                            }
                            else if (income.PaidAmount < totalAmount)
                            {
                                financeStatusId = 3;
                            }

                            income.Finance_Status_Id = financeStatusId;

                            incomeCommand.Parameters.AddWithValue("@PartID", income.PartID);
                            incomeCommand.Parameters.AddWithValue("@SupplierID", income.SupplierID);
                            incomeCommand.Parameters.AddWithValue("@Date", income.Date);
                            incomeCommand.Parameters.AddWithValue("@Quantity", income.Quantity);
                            incomeCommand.Parameters.AddWithValue("@UnitPrice", income.UnitPrice);
                            incomeCommand.Parameters.AddWithValue("@Markup", income.Markup);
                            incomeCommand.Parameters.AddWithValue("@StatusID", income.StatusID);
                            incomeCommand.Parameters.AddWithValue("@FinanceStatusId", financeStatusId);
                            incomeCommand.Parameters.AddWithValue("@OperationID", operationID);
                            incomeCommand.Parameters.AddWithValue("@StockID", income.StockID);
                            incomeCommand.Parameters.AddWithValue("@InvoiceNumber", income.InvoiceNumber ?? (object)DBNull.Value);
                            incomeCommand.Parameters.AddWithValue("@UserID", user.UserID);
                            incomeCommand.Parameters.AddWithValue("@PaidAmount", income.PaidAmount);
                            incomeCommand.Parameters.AddWithValue("@BatchID", batchID);

                            income.IncomeID = Convert.ToInt32(incomeCommand.ExecuteScalar());
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Yangi: Barcha income'larni IncomeDto shaklida olish (sinxron, top 20 ta, ordering bilan)
        public List<IncomeDto> GetAllIncomes()
        {
            var incomes = new List<IncomeDto>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(@"
                WITH BatchSummary AS (
                    SELECT 
                        pi.BatchID,
                        b.Name AS BatchName,
                        MAX(pi.Date) AS MaxDate,  -- Eng oxirgi sana
                        SUM(pi.Quantity) AS TotalQuantity,
                        COUNT(DISTINCT pi.PartID) AS PartTypesCount,
                        MAX(st.Name) AS StatusName,  -- Eng oxirgi status (MAX orqali, IncomeStatuses dan)
                        MAX(u.FullName) AS UserFullName,  -- Eng oxirgi user (MAX orqali)
                        -- RemainingQuantity hisoblash (PartsExpenses dan ayirish; BatchID bo'yicha)
                        (SUM(pi.Quantity) - ISNULL((
                            SELECT SUM(pe.Quantity) 
                            FROM PartsExpenses pe  -- To'g'ri table: PartsExpenses
                            INNER JOIN PartsIncome pi2 ON pe.IncomeID = pi2.IncomeID  -- Link orqali
                            WHERE pi2.BatchID = pi.BatchID  -- BatchID bo'yicha
                        ), 0)) AS RemainingQuantity
                    FROM PartsIncome pi
                    LEFT JOIN Batches b ON pi.BatchID = b.BatchID
                    LEFT JOIN IncomeStatuses st ON pi.StatusID = st.StatusID  -- IncomeStatuses
                    LEFT JOIN Users u ON pi.UserID = u.UserID  -- UserFullName uchun
                    GROUP BY pi.BatchID, b.Name  -- Faqat asosiy: BatchID va Name
                ),
                SuppliersAgg AS (  -- Alohida CTE: Unique suppliers ro'yxati
                    SELECT 
                        pi.BatchID,
                        STRING_AGG(s.Name, ', ') AS SuppliersList  -- DISTINCTsiz, lekin GROUP BY orqali unique
                    FROM PartsIncome pi
                    INNER JOIN Suppliers s ON pi.SupplierID = s.SupplierID
                    GROUP BY pi.BatchID
                )
                SELECT TOP 20 
                    bs.BatchID,
                    bs.BatchName,
                    bs.TotalQuantity,
                    bs.PartTypesCount,
                    ISNULL(sa.SuppliersList, '') AS Suppliers,  -- Subquery orqali
                    bs.StatusName,
                    bs.UserFullName,
                    bs.RemainingQuantity,
                    CAST((bs.RemainingQuantity * 100.0 / NULLIF(bs.TotalQuantity, 0)) AS INT) AS RemainingPercentage,  -- Foiz
                    bs.MaxDate
                FROM BatchSummary bs
                LEFT JOIN SuppliersAgg sa ON bs.BatchID = sa.BatchID
                ORDER BY 
                    CASE WHEN bs.RemainingQuantity > 0 THEN 0 ELSE 1 END,  -- 0 bo'lsa, pastga (1)
                    bs.MaxDate DESC  -- Sana bo'yicha (eng yangi birinchi)
                ", connection);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var income = new IncomeDto
                            {
                                BatchId = reader.IsDBNull("BatchID") ? 0 : reader.GetInt32("BatchID"),
                                BatchName = reader.IsDBNull("BatchName") ? string.Empty : reader.GetString("BatchName"),
                                TotalQuantity = reader.IsDBNull("TotalQuantity") ? 0 : reader.GetInt32("TotalQuantity"),
                                PartTypesCount = reader.IsDBNull("PartTypesCount") ? 0 : reader.GetInt32("PartTypesCount"),
                                RemainingQuantity = reader.IsDBNull("RemainingQuantity") ? 0 : reader.GetInt32("RemainingQuantity"),
                                RemainingPercentage = reader.IsDBNull("RemainingPercentage") ? 0 : reader.GetInt32("RemainingPercentage"),
                                StatusName = reader.IsDBNull("StatusName") ? string.Empty : reader.GetString("StatusName"),
                                UserFullName = reader.IsDBNull("UserFullName") ? string.Empty : reader.GetString("UserFullName"),
                                Suppliers = !reader.IsDBNull("Suppliers") && !string.IsNullOrEmpty(reader.GetString("Suppliers"))
                                    ? reader.GetString("Suppliers").Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList<string>()
                                    : new List<string>() // STRING_AGG natijasini List ga aylantirish
                            };
                            incomes.Add(income);
                        }
                    }
                }
                Debug.WriteLine($"GetAllIncomes: {incomes.Count} ta batch topildi (top 20).");
                return incomes;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetAllIncomes SQL Xatosi: {ex.Message}");
                throw new Exception("Batch ma'lumotlarini olishda xato.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetAllIncomes Xatosi: {ex.Message}");
                throw new Exception("Noma'lum xato yuz berdi.", ex);
            }
        }

        // Yangi: Bitta batch uchun to'liq income va expenses ma'lumotlarini olish
        public BatchIncomeDetailDto GetBatchIncomesWithExpenses(int batchId)
        {
            if (batchId <= 0)
                throw new ArgumentException("Noto'g'ri BatchID.");

            var detail = new BatchIncomeDetailDto { BatchId = batchId };
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // 1. BatchName olish
                    var batchCommand = new SqlCommand("SELECT Name FROM Batches WHERE BatchID = @BatchID", connection);
                    batchCommand.Parameters.AddWithValue("@BatchID", batchId);
                    var batchNameResult = batchCommand.ExecuteScalar();
                    detail.BatchName = batchNameResult?.ToString() ?? string.Empty;

                    // 2. Barcha Incomes olish (BatchID bo'yicha)
                    var incomesCommand = new SqlCommand(@"
                SELECT IncomeID, PartID, SupplierID, Date, Quantity, UnitPrice, Markup, 
                       StatusID, Finance_Status_Id, OperationID, StockID, InvoiceNumber, 
                       UserID, PaidAmount, BatchID
                FROM PartsIncome 
                WHERE BatchID = @BatchID 
                ORDER BY Date DESC", connection);  // Sana bo'yicha
                    incomesCommand.Parameters.AddWithValue("@BatchID", batchId);

                    using (var reader = incomesCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var income = new PartsIncome
                            {
                                IncomeID = reader.GetInt32("IncomeID"),
                                PartID = reader.GetInt32("PartID"),
                                SupplierID = reader.GetInt32("SupplierID"),
                                Date = reader.GetDateTime("Date"),
                                Quantity = reader.GetInt32("Quantity"),
                                UnitPrice = reader.GetDecimal("UnitPrice"),
                                Markup = reader.GetDecimal("Markup"),
                                StatusID = reader.GetInt32("StatusID"),
                                Finance_Status_Id = reader.GetInt32("Finance_Status_Id"),
                                OperationID = reader.GetInt32("OperationID"),
                                StockID = reader.GetInt32("StockID"),
                                InvoiceNumber = reader.IsDBNull("InvoiceNumber") ? null : reader.GetString("InvoiceNumber"),
                                UserID = reader.GetInt32("UserID"),
                                PaidAmount = reader.GetDecimal("PaidAmount"),
                                BatchID = reader.GetInt32("BatchID")
                            };

                            // 3. Shu income uchun Expenses olish va hisoblash
                            var incomeDetail = new IncomeDetail { Income = income };
                            decimal totalSaleSum = 0;
                            int totalSoldQuantity = 0;
                            DateTime lastSaleDate = DateTime.MinValue;

                            // Expenses uchun alohida connection (xavfsiz)
                            using (var expenseConn = new SqlConnection(_connectionString))
                            {
                                expenseConn.Open();
                                var expensesCommand = new SqlCommand(@"
                            SELECT ExpenseID, PartID, IncomeID, ExpenseTypeID, CustomerID, Date, Quantity, 
                                   UnitPrice, StatusID, OperationID, InvoiceNumber, 
                                   UserID, PaidAmount, Finance_statusId
                            FROM PartsExpenses 
                            WHERE IncomeID = @IncomeID 
                            ORDER BY Date DESC", expenseConn);
                                expensesCommand.Parameters.AddWithValue("@IncomeID", income.IncomeID);

                                using (var expenseReader = expensesCommand.ExecuteReader())
                                {
                                    while (expenseReader.Read())
                                    {
                                        var expense = new PartExpense  // Sizning entity: PartExpense
                                        {
                                            ExpenseID = expenseReader.GetInt32("ExpenseID"),
                                            PartID = expenseReader.GetInt32("PartID"),
                                            IncomeID = expenseReader.IsDBNull("IncomeID") ? null : expenseReader.GetInt32("IncomeID"),
                                            ExpenseTypeID = expenseReader.IsDBNull("ExpenseTypeID") ? null : expenseReader.GetInt32("ExpenseTypeID"),
                                            CustomerID = expenseReader.IsDBNull("CustomerID") ? null : expenseReader.GetInt32("CustomerID"),
                                            Date = expenseReader.GetDateTime("Date"),
                                            Quantity = expenseReader.GetInt32("Quantity"),
                                            UnitPrice = expenseReader.GetDecimal("UnitPrice"),
                                            StatusID = expenseReader.IsDBNull("StatusID") ? null : expenseReader.GetInt32("StatusID"),
                                            // SuplierID mavjud emas – null qo'yamiz (entity'da nullable)
                                            SuplierID = null,
                                            OperationID = expenseReader.IsDBNull("OperationID") ? null : expenseReader.GetInt32("OperationID"),
                                            InvoiceNumber = expenseReader.IsDBNull("InvoiceNumber") ? null : expenseReader.GetString("InvoiceNumber"),
                                            UserID = expenseReader.IsDBNull("UserID") ? null : expenseReader.GetInt32("UserID"),
                                            PaidAmount = expenseReader.GetDecimal("PaidAmount"),
                                            Finance_statusId = expenseReader.GetInt32("Finance_statusId")  // Mavjud field
                                        };
                                        incomeDetail.SoldExpenses.Add(expense);

                                        totalSoldQuantity += expense.Quantity;
                                        totalSaleSum += expense.Quantity * expense.UnitPrice;
                                        if (expense.Date > lastSaleDate)
                                            lastSaleDate = expense.Date;
                                    }
                                }
                            }

                            // Hisoblash
                            incomeDetail.TotalSoldQuantity = totalSoldQuantity;
                            incomeDetail.TotalSaleSum = totalSaleSum;
                            incomeDetail.RemainingQuantity = income.Quantity - totalSoldQuantity;
                            incomeDetail.LastSaleDate = lastSaleDate;

                            detail.Incomes.Add(incomeDetail);
                        }
                    }
                }
                Debug.WriteLine($"GetBatchIncomesWithExpenses: Batch {batchId} uchun {detail.Incomes.Count} ta income topildi.");
                return detail;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"GetBatchIncomesWithExpenses SQL Xatosi: {ex.Message}");
                throw new Exception($"Batch {batchId} ma'lumotlarini olishda xato.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetBatchIncomesWithExpenses Xatosi: {ex.Message}");
                throw new Exception("Noma'lum xato yuz berdi.", ex);
            }
        }
    }
}
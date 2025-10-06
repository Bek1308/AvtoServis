using AvtoServis.Data.Interfaces;
using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using AvtoServis.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoServis.Data.Repositories
{
    public class PartsExpensesRepository : IPartsExpensesRepository
    {
        private readonly string _connectionString;

        public PartsExpensesRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<PartExpenseDto> GetAllPartExpenses()
        {
            var result = new List<PartExpenseDto>();

            string query = @"
        SELECT 
            pe.ExpenseID AS SaleId,
            p.PartName,
            pe.Quantity,
            pe.UnitPrice,
            (pe.Quantity * pe.UnitPrice) AS TotalAmount,
            pe.Finance_statusid AS PaymentStatusId,
            pe.Date AS SaleDate,
            pe.PaidAmount,
            m.Name AS Manufacturer,
            c.FullName AS CustomerName,
            c.Phone AS CustomerPhone,
            p.CatalogNumber,
            cb.CarBrandName AS CarBrand,
            es.Name AS Status,
            es.Color AS StatusColor,
            u.FullName AS Seller,
            pe.InvoiceNumber
        FROM 
            PartsExpenses pe
        LEFT JOIN Parts p ON p.PartID = pe.PartID
        LEFT JOIN Manufacturers m ON m.ManufacturerID = p.ManufacturerID
        LEFT JOIN Customers c ON c.CustomerID = pe.CustomerID
        LEFT JOIN CarBrand cb ON cb.Id = p.CarBrand_Id
        LEFT JOIN ExpenseStatuses es ON es.StatusID = pe.StatusID
        LEFT JOIN Users u ON u.UserID = pe.UserID
        ORDER BY 
            pe.Date";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var dto = new PartExpenseDto
                                {
                                    SaleId = reader.IsDBNull(reader.GetOrdinal("SaleId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SaleId")),
                                    PartName = reader.IsDBNull(reader.GetOrdinal("PartName")) ? null : reader.GetString(reader.GetOrdinal("PartName")),
                                    Quantity = reader.IsDBNull(reader.GetOrdinal("Quantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                                    TotalAmount = reader.IsDBNull(reader.GetOrdinal("TotalAmount")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                                    PaymentStatusId = reader.IsDBNull(reader.GetOrdinal("PaymentStatusId")) ? null : reader.GetInt32(reader.GetOrdinal("PaymentStatusId")),
                                    SaleDate = reader.IsDBNull(reader.GetOrdinal("SaleDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("SaleDate")),
                                    PaidAmount = reader.IsDBNull(reader.GetOrdinal("PaidAmount")) ? 0 : reader.GetDecimal(reader.GetOrdinal("PaidAmount")),
                                    Manufacturer = reader.IsDBNull(reader.GetOrdinal("Manufacturer")) ? null : reader.GetString(reader.GetOrdinal("Manufacturer")),
                                    CustomerName = reader.IsDBNull(reader.GetOrdinal("CustomerName")) ? null : reader.GetString(reader.GetOrdinal("CustomerName")),
                                    CustomerPhone = reader.IsDBNull(reader.GetOrdinal("CustomerPhone")) ? null : reader.GetString(reader.GetOrdinal("CustomerPhone")),
                                    CatalogNumber = reader.IsDBNull(reader.GetOrdinal("CatalogNumber")) ? null : reader.GetString(reader.GetOrdinal("CatalogNumber")),
                                    CarBrand = reader.IsDBNull(reader.GetOrdinal("CarBrand")) ? null : reader.GetString(reader.GetOrdinal("CarBrand")),
                                    Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString(reader.GetOrdinal("Status")),
                                    StatusColor = reader.IsDBNull(reader.GetOrdinal("StatusColor")) ? null : reader.GetString(reader.GetOrdinal("StatusColor")),
                                    Seller = reader.IsDBNull(reader.GetOrdinal("Seller")) ? null : reader.GetString(reader.GetOrdinal("Seller")),
                                    InvoiceNumber = reader.IsDBNull(reader.GetOrdinal("InvoiceNumber")) ? null : reader.GetString(reader.GetOrdinal("InvoiceNumber"))
                                };
                                result.Add(dto);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error retrieving part expenses: " + ex.Message, ex);
                    }
                }
            }

            return result;
        }

        public void Add(PartExpense entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Operatsiya qo'shish
                            var operationCommand = new SqlCommand(
                                @"INSERT INTO Operations (OperationTypeID, CustomerID, TotalSum, StatusID, OperationDate, Description, UserID)
                                  VALUES (@OperationTypeID, @CustomerID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID);
                                  SELECT SCOPE_IDENTITY();",
                                connection, transaction);

                            decimal totalSum = entity.Quantity * entity.UnitPrice;
                            var currentUser = CurrentUser.Instance;
                            var user = currentUser.User;
                            string description = $"Yangi xarajat qo'shildi: PartExpense ID: (yangi), PartID: {entity.PartID}";

                            operationCommand.Parameters.AddWithValue("@OperationTypeID", 2); // Xarajat operatsiyasi
                            operationCommand.Parameters.AddWithValue("@CustomerID", (object)entity.CustomerID ?? DBNull.Value);
                            operationCommand.Parameters.AddWithValue("@TotalSum", totalSum);
                            operationCommand.Parameters.AddWithValue("@StatusID", 1); // Yangi
                            operationCommand.Parameters.AddWithValue("@OperationDate", entity.Date);
                            operationCommand.Parameters.AddWithValue("@Description", description);
                            operationCommand.Parameters.AddWithValue("@UserID", (object)entity.UserID ?? user.UserID);

                            int operationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                            // PartsExpenses qo'shish
                            var command = new SqlCommand(
                                @"INSERT INTO PartsExpenses (PartID, IncomeID, ExpenseTypeID, CustomerID, Date, Quantity, 
                                                          UnitPrice, StatusID, OperationID, InvoiceNumber, UserID, 
                                                          PaidAmount, Finance_statusId)
                                  VALUES (@PartID, @IncomeID, @ExpenseTypeID, @CustomerID, @Date, @Quantity, 
                                          @UnitPrice, @StatusID, @OperationID, @InvoiceNumber, @UserID, 
                                          @PaidAmount, @FinanceStatusId);
                                  SELECT SCOPE_IDENTITY();",
                                connection, transaction);

                            decimal totalAmount = entity.Quantity * entity.UnitPrice;
                            int financeStatusId = 0;
                            if (entity.PaidAmount == 0)
                                financeStatusId = 2; // To'lanmagan
                            else if (entity.PaidAmount == totalAmount)
                                financeStatusId = 1; // To'liq to'langan
                            else if (entity.PaidAmount < totalAmount)
                                financeStatusId = 3; // Qisman to'langan

                            command.Parameters.AddWithValue("@PartID", entity.PartID);
                            command.Parameters.AddWithValue("@IncomeID", (object)entity.IncomeID ?? DBNull.Value);
                            command.Parameters.AddWithValue("@ExpenseTypeID", (object)entity.ExpenseTypeID ?? DBNull.Value);
                            command.Parameters.AddWithValue("@CustomerID", (object)entity.CustomerID ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Date", entity.Date);
                            command.Parameters.AddWithValue("@Quantity", entity.Quantity);
                            command.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
                            command.Parameters.AddWithValue("@StatusID", (object)entity.StatusID ?? DBNull.Value);
                            command.Parameters.AddWithValue("@OperationID", operationID);
                            command.Parameters.AddWithValue("@InvoiceNumber", (object)entity.InvoiceNumber ?? DBNull.Value);
                            command.Parameters.AddWithValue("@UserID", (object)entity.UserID ?? user.UserID);
                            command.Parameters.AddWithValue("@PaidAmount", entity.PaidAmount);
                            command.Parameters.AddWithValue("@FinanceStatusId", financeStatusId);

                            var newId = Convert.ToInt32(command.ExecuteScalar());
                            entity.ExpenseID = newId;

                            transaction.Commit();
                            Debug.WriteLine($"Add: Xarajat {newId} ID bilan qo'shildi.");
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
                Debug.WriteLine($"Add SQL Xatosi: {ex.Message}");
                throw new Exception("Xarajat qo'shishda xato.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Add Xatosi: {ex.Message}");
                throw new Exception("Noma'lum xato yuz berdi.", ex);
            }
        }

        public void Update(PartExpense expense, string batchName)
        {
            if (expense == null)
                throw new ArgumentException("PartExpense cannot be null.");
            if (string.IsNullOrEmpty(batchName))
                throw new ArgumentException("BatchName cannot be empty.");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // BatchID ni olish yoki yangi batch yaratish


                        // Yangi operatsiya yaratish
                        var operationCommand = new SqlCommand(
                            @"INSERT INTO Operations (OperationTypeID, CustomerID, TotalSum, StatusID, OperationDate, Description, UserID, ParentOperationID)
                              VALUES (@OperationTypeID, @CustomerID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID, @ParentOperationID);
                              SELECT SCOPE_IDENTITY();",
                            connection, transaction);

                        decimal totalSum = expense.Quantity * expense.UnitPrice;
                        string description = $"Редактирование продажи, PartExpenseID: {expense.ExpenseID} PartID: {expense.PartID} ";
                        var currentUser = CurrentUser.Instance;
                        var user = currentUser.User;

                        operationCommand.Parameters.AddWithValue("@OperationTypeID", 2);
                        operationCommand.Parameters.AddWithValue("@CustomerID", (object)expense.CustomerID ?? DBNull.Value);
                        operationCommand.Parameters.AddWithValue("@TotalSum", totalSum);
                        operationCommand.Parameters.AddWithValue("@StatusID", 1);
                        operationCommand.Parameters.AddWithValue("@OperationDate", expense.Date);
                        operationCommand.Parameters.AddWithValue("@Description", description);
                        operationCommand.Parameters.AddWithValue("@UserID", (object)expense.UserID ?? user.UserID);
                        operationCommand.Parameters.AddWithValue("@ParentOperationID", (object)expense.OperationID ?? DBNull.Value);

                        int operationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                        // PartsExpenses ni yangilash
                        var expenseCommand = new SqlCommand(
                            @"UPDATE PartsExpenses 
                              SET PartID = @PartID, IncomeID = @IncomeID, ExpenseTypeID = @ExpenseTypeID, 
                                  CustomerID = @CustomerID, Date = @Date, Quantity = @Quantity, 
                                  UnitPrice = @UnitPrice, StatusID = @StatusID, OperationID = @OperationID, 
                                  InvoiceNumber = @InvoiceNumber, UserID = @UserID, PaidAmount = @PaidAmount, 
                                  Finance_statusId = @FinanceStatusId
                              WHERE ExpenseID = @ExpenseID;",
                            connection, transaction);

                        decimal totalAmount = expense.Quantity * expense.UnitPrice;
                        int financeStatusId = 0;
                        if (expense.PaidAmount == 0)
                            financeStatusId = 2;
                        else if (expense.PaidAmount == totalAmount)
                            financeStatusId = 1;
                        else if (expense.PaidAmount < totalAmount)
                            financeStatusId = 3;

                        expense.Finance_statusId = financeStatusId;

                        expenseCommand.Parameters.AddWithValue("@ExpenseID", expense.ExpenseID);
                        expenseCommand.Parameters.AddWithValue("@PartID", expense.PartID);
                        expenseCommand.Parameters.AddWithValue("@IncomeID", (object)expense.IncomeID ?? DBNull.Value);
                        expenseCommand.Parameters.AddWithValue("@ExpenseTypeID", (object)expense.ExpenseTypeID ?? DBNull.Value);
                        expenseCommand.Parameters.AddWithValue("@CustomerID", (object)expense.CustomerID ?? DBNull.Value);
                        expenseCommand.Parameters.AddWithValue("@Date", expense.Date);
                        expenseCommand.Parameters.AddWithValue("@Quantity", expense.Quantity);
                        expenseCommand.Parameters.AddWithValue("@UnitPrice", expense.UnitPrice);
                        expenseCommand.Parameters.AddWithValue("@StatusID", (object)expense.StatusID ?? DBNull.Value);
                        expenseCommand.Parameters.AddWithValue("@OperationID", operationID);
                        expenseCommand.Parameters.AddWithValue("@InvoiceNumber", (object)expense.InvoiceNumber ?? DBNull.Value);
                        expenseCommand.Parameters.AddWithValue("@UserID", (object)expense.UserID ?? user.UserID);
                        expenseCommand.Parameters.AddWithValue("@PaidAmount", expense.PaidAmount);
                        expenseCommand.Parameters.AddWithValue("@FinanceStatusId", financeStatusId);

                        expenseCommand.ExecuteNonQuery();

                        transaction.Commit();
                        Debug.WriteLine($"Update: Xarajat {expense.ExpenseID} yangilandi.");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void ReturnPartExpense(PartExpense expense)
        {
            if (expense == null)
                throw new ArgumentException("PartExpense cannot be null.");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Yangi operatsiya yaratish
                        var operationCommand = new SqlCommand(
                            @"INSERT INTO Operations (OperationTypeID, CustomerID, TotalSum, StatusID, OperationDate, Description, UserID, ParentOperationID)
                      VALUES (@OperationTypeID, @CustomerID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID, @ParentOperationID);
                      SELECT SCOPE_IDENTITY();",
                            connection, transaction);

                        string description = $"Возврат продажи, PartExpenseID: {expense.ExpenseID}, PartID: {expense.PartID}";
                        var currentUser = CurrentUser.Instance;
                        var user = currentUser.User;

                        operationCommand.Parameters.AddWithValue("@OperationTypeID", 2); // Return operatsiyasi uchun
                        operationCommand.Parameters.AddWithValue("@CustomerID", SqlDbType.Int).Value = (object)expense.CustomerID ?? DBNull.Value;
                        operationCommand.Parameters.AddWithValue("@TotalSum", 0m);
                        operationCommand.Parameters.AddWithValue("@StatusID", expense.StatusID ?? 1);
                        operationCommand.Parameters.AddWithValue("@OperationDate", DateTime.Now);
                        operationCommand.Parameters.AddWithValue("@Description", description);
                        operationCommand.Parameters.AddWithValue("@UserID", user.UserID);
                        operationCommand.Parameters.AddWithValue("@ParentOperationID", (object)expense.OperationID ?? DBNull.Value);

                        int operationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                        // PartsExpenses ni yangilash
                        var expenseCommand = new SqlCommand(
                            @"UPDATE PartsExpenses 
                      SET PartID = @PartID, IncomeID = @IncomeID, ExpenseTypeID = @ExpenseTypeID, 
                          CustomerID = @CustomerID, Date = @Date, Quantity = @Quantity, 
                          UnitPrice = @UnitPrice, StatusID = @StatusID, OperationID = @OperationID, 
                          InvoiceNumber = @InvoiceNumber, UserID = @UserID, PaidAmount = @PaidAmount, 
                          Finance_statusId = @FinanceStatusId
                      WHERE ExpenseID = @ExpenseID;",
                            connection, transaction);

                        expenseCommand.Parameters.AddWithValue("@ExpenseID", expense.ExpenseID);
                        expenseCommand.Parameters.AddWithValue("@PartID", expense.PartID);
                        expenseCommand.Parameters.AddWithValue("@IncomeID", DBNull.Value);
                        expenseCommand.Parameters.AddWithValue("@ExpenseTypeID", 2);
                        expenseCommand.Parameters.AddWithValue("@CustomerID", SqlDbType.Int).Value = (object)expense.CustomerID ?? DBNull.Value;
                        expenseCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                        expenseCommand.Parameters.AddWithValue("@Quantity", 0);
                        expenseCommand.Parameters.AddWithValue("@UnitPrice", 0m);
                        expenseCommand.Parameters.AddWithValue("@StatusID", expense.StatusID);
                        expenseCommand.Parameters.AddWithValue("@OperationID", operationID);
                        expenseCommand.Parameters.AddWithValue("@InvoiceNumber", DBNull.Value);
                        expenseCommand.Parameters.AddWithValue("@UserID", user.UserID);
                        expenseCommand.Parameters.AddWithValue("@PaidAmount", 0m);
                        expenseCommand.Parameters.AddWithValue("@FinanceStatusId", 1); // Оплачен

                        int rowsAffected = expenseCommand.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            throw new InvalidOperationException("PartExpense не возвращён.");

                        // Update entity fields
                      
                        expense.IncomeID = null;
                        expense.ExpenseTypeID = 2;
                        expense.Quantity = 0;
                        expense.UnitPrice = 0m;
                        expense.PaidAmount = 0m;
                        expense.Finance_statusId = 1; // Оплачен
                        expense.InvoiceNumber = null;
                        expense.OperationID = operationID;
                        expense.Date = DateTime.Now;

                        transaction.Commit();
                        Debug.WriteLine($"ReturnPartExpense: Xarajat {expense.ExpenseID} возвращён.");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(int expenseId, string reason)
        {
            if (expenseId <= 0)
                throw new ArgumentException("ExpenseID must be a valid positive integer.");
            if (string.IsNullOrEmpty(reason))
                throw new ArgumentException("Reason for deletion cannot be empty.");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Eski batch nomini olish (IncomeID orqali)
                        string oldBatchName;
                        var getOldBatchCommand = new SqlCommand(
                            @"SELECT Name FROM Batches WHERE BatchID = (SELECT BatchID FROM PartsIncome WHERE IncomeID = (SELECT IncomeID FROM PartsExpenses WHERE ExpenseID = @ExpenseID))",
                            connection, transaction);
                        getOldBatchCommand.Parameters.AddWithValue("@ExpenseID", expenseId);
                        var oldBatchResult = getOldBatchCommand.ExecuteScalar();
                        oldBatchName = oldBatchResult != null ? oldBatchResult.ToString() : "Noma'lum";

                        // Operatsiyani jurnalga yozish
                        var operationCommand = new SqlCommand(
                            @"INSERT INTO Operations (OperationTypeID, CustomerID, TotalSum, StatusID, OperationDate, Description, UserID, ParentOperationID)
                              VALUES (@OperationTypeID, @CustomerID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID, @ParentOperationID);
                              SELECT SCOPE_IDENTITY();",
                            connection, transaction);

                        var currentUser = CurrentUser.Instance;
                        var user = currentUser.User;
                        string description = $"O'chirish: PartExpense ID: {expenseId}, Sabab: {reason}, Partiya: {oldBatchName}";

                        // PartsExpenses ma'lumotlarini olish
                        var expenseCommand = new SqlCommand(
                            @"SELECT CustomerID, Quantity, UnitPrice, OperationID, Date 
                              FROM PartsExpenses WHERE ExpenseID = @ExpenseID",
                            connection, transaction);
                        expenseCommand.Parameters.AddWithValue("@ExpenseID", expenseId);
                        var reader = expenseCommand.ExecuteReader();

                        decimal totalSum = 0;
                        int? customerId = null;
                        int? parentOperationId = null;
                        DateTime operationDate = DateTime.Now;

                        if (reader.Read())
                        {
                            customerId = reader.IsDBNull(0) ? null : reader.GetInt32(0);
                            int quantity = reader.GetInt32(1);
                            decimal unitPrice = reader.GetDecimal(2);
                            parentOperationId = reader.IsDBNull(3) ? null : reader.GetInt32(3);
                            operationDate = reader.GetDateTime(4);
                            totalSum = quantity * unitPrice;
                        }
                        reader.Close();

                        if (customerId == null && parentOperationId == null)
                            throw new InvalidOperationException("PartExpense record not found.");

                        int operationTypeId = reason == "Tovar qaytarildi" ? 5 : 11; // 5 - qaytarish, 11 - boshqa sabab
                        operationCommand.Parameters.AddWithValue("@OperationTypeID", operationTypeId);
                        operationCommand.Parameters.AddWithValue("@CustomerID", (object)customerId ?? DBNull.Value);
                        operationCommand.Parameters.AddWithValue("@TotalSum", totalSum);
                        operationCommand.Parameters.AddWithValue("@StatusID", 3);
                        operationCommand.Parameters.AddWithValue("@OperationDate", operationDate);
                        operationCommand.Parameters.AddWithValue("@Description", description);
                        operationCommand.Parameters.AddWithValue("@UserID", user.UserID);
                        operationCommand.Parameters.AddWithValue("@ParentOperationID", (object)parentOperationId ?? DBNull.Value);

                        int operationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                        // PartsExpenses dan o'chirish
                        var deleteCommand = new SqlCommand(
                            @"DELETE FROM PartsExpenses WHERE ExpenseID = @ExpenseID",
                            connection, transaction);
                        deleteCommand.Parameters.AddWithValue("@ExpenseID", expenseId);

                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            throw new InvalidOperationException("No PartExpense record was deleted.");

                        transaction.Commit();
                        Debug.WriteLine($"Delete: Xarajat {expenseId} o'chirildi.");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public PartExpense GetById(int expenseId)
        {
            if (expenseId <= 0)
                throw new ArgumentException("ExpenseID must be a valid positive integer.");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    @"SELECT ExpenseID, PartID, IncomeID, ExpenseTypeID, CustomerID, Date, Quantity, 
                             UnitPrice, StatusID, OperationID, InvoiceNumber, UserID, PaidAmount, Finance_statusId
                      FROM PartsExpenses WHERE ExpenseID = @ExpenseID",
                    connection);
                command.Parameters.AddWithValue("@ExpenseID", expenseId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PartExpense
                        {
                            ExpenseID = reader.GetInt32(0),
                            PartID = reader.GetInt32(1),
                            IncomeID = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                            ExpenseTypeID = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                            CustomerID = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                            Date = reader.GetDateTime(5),
                            Quantity = reader.GetInt32(6),
                            UnitPrice = reader.GetDecimal(7),
                            StatusID = reader.IsDBNull(8) ? null : reader.GetInt32(8),
                            OperationID = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                            InvoiceNumber = reader.IsDBNull(10) ? null : reader.GetString(10),
                            UserID = reader.IsDBNull(11) ? null : reader.GetInt32(11),
                            PaidAmount = reader.GetDecimal(12),
                            Finance_statusId = reader.GetInt32(13)
                        };
                    }
                    return null;
                }
            }
        }

        public List<PartExpense> GetAll()
        {
            var partsExpenses = new List<PartExpense>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    @"SELECT ExpenseID, PartID, IncomeID, ExpenseTypeID, CustomerID, Date, Quantity, 
                             UnitPrice, StatusID, OperationID, InvoiceNumber, UserID, PaidAmount, Finance_statusId
                      FROM PartsExpenses",
                    connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        partsExpenses.Add(new PartExpense
                        {
                            ExpenseID = reader.GetInt32(0),
                            PartID = reader.GetInt32(1),
                            IncomeID = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                            ExpenseTypeID = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                            CustomerID = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                            Date = reader.GetDateTime(5),
                            Quantity = reader.GetInt32(6),
                            UnitPrice = reader.GetDecimal(7),
                            StatusID = reader.IsDBNull(8) ? null : reader.GetInt32(8),
                            OperationID = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                            InvoiceNumber = reader.IsDBNull(10) ? null : reader.GetString(10),
                            UserID = reader.IsDBNull(11) ? null : reader.GetInt32(11),
                            PaidAmount = reader.GetDecimal(12),
                            Finance_statusId = reader.GetInt32(13)
                        });
                    }
                }
            }
            Debug.WriteLine($"GetAll: {partsExpenses.Count} ta xarajat kiritildi.");
            return partsExpenses;
        }

        public void SavePartsExpenses(List<PartExpense> partsExpenses)
        {
            // Validatsiya
            if (partsExpenses == null || !partsExpenses.Any())
                throw new ArgumentNullException(nameof(partsExpenses), "No data to save.");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Har bir PartExpense uchun operatsiya va xarajat qo'shish
                        foreach (var expense in partsExpenses)
                        {
                            // Operatsiya qo'shish
                            const string operationQuery = @"
                        INSERT INTO Operations (OperationTypeID, CustomerID, SupplierID, TotalSum, StatusID, OperationDate, Description, UserID)
                        VALUES (@OperationTypeID, @CustomerID, @SupplierID, @TotalSum, @StatusID, @OperationDate, @Description, @UserID);
                        SELECT SCOPE_IDENTITY();";

                            var operationCommand = new SqlCommand(operationQuery, connection, transaction);
                            decimal totalSum = expense.Quantity * expense.UnitPrice;
                            var currentUser = CurrentUser.Instance?.User ?? throw new InvalidOperationException("User is not authenticated.");
                            string description = $"Sotuv xarajati, PartID: {expense.PartID}";

                            operationCommand.Parameters.AddRange(new[]
                            {
                        new SqlParameter("@OperationTypeID", SqlDbType.Int) { Value = 2 }, // Xarajat
                        new SqlParameter("@CustomerID", SqlDbType.Int) { Value = expense.CustomerID ?? (object)DBNull.Value },
                        new SqlParameter("@SupplierID", SqlDbType.Int) { Value = expense.SuplierID ?? (object)DBNull.Value },
                        new SqlParameter("@TotalSum", SqlDbType.Decimal) { Value = totalSum },
                        new SqlParameter("@StatusID", SqlDbType.Int) { Value = 1 }, // Yangi
                        new SqlParameter("@OperationDate", SqlDbType.DateTime) { Value = expense.Date },
                        new SqlParameter("@Description", SqlDbType.NVarChar) { Value = description },
                        new SqlParameter("@UserID", SqlDbType.Int) { Value = expense.UserID ?? currentUser.UserID }
                    });

                            int operationID = Convert.ToInt32(operationCommand.ExecuteScalar());

                            // PartExpense qo'shish
                            const string expenseQuery = @"
                        INSERT INTO PartsExpenses (PartID, IncomeID, ExpenseTypeID, CustomerID, Date, Quantity, 
                                                  UnitPrice, StatusID, OperationID, InvoiceNumber, UserID, 
                                                  PaidAmount, Finance_statusId)
                        VALUES (@PartID, @IncomeID, @ExpenseTypeID, @CustomerID, @Date, @Quantity, 
                                @UnitPrice, @StatusID, @OperationID, @InvoiceNumber, @UserID, 
                                @PaidAmount, @FinanceStatusId);
                        SELECT SCOPE_IDENTITY();";

                            var expenseCommand = new SqlCommand(expenseQuery, connection, transaction);
                            decimal totalAmount = expense.Quantity * expense.UnitPrice;
                            int financeStatusId = expense.PaidAmount switch
                            {
                                0 => 2, // To'lanmagan
                                var paid when paid == totalAmount => 1, // To'liq to'langan
                                _ => 3 // Qisman to'langan
                            };

                            expense.Finance_statusId = financeStatusId;

                            expenseCommand.Parameters.AddRange(new[]
                            {
                        new SqlParameter("@PartID", SqlDbType.Int) { Value = expense.PartID },
                        new SqlParameter("@IncomeID", SqlDbType.Int) { Value = expense.IncomeID ?? (object)DBNull.Value },
                        new SqlParameter("@ExpenseTypeID", SqlDbType.Int) { Value = expense.ExpenseTypeID ?? (object)DBNull.Value },
                        new SqlParameter("@CustomerID", SqlDbType.Int) { Value = expense.CustomerID ?? (object)DBNull.Value },
                        new SqlParameter("@Date", SqlDbType.DateTime) { Value = expense.Date },
                        new SqlParameter("@Quantity", SqlDbType.Int) { Value = expense.Quantity },
                        new SqlParameter("@UnitPrice", SqlDbType.Decimal) { Value = expense.UnitPrice },
                        new SqlParameter("@StatusID", SqlDbType.Int) { Value = expense.StatusID ?? (object)DBNull.Value },
                        new SqlParameter("@OperationID", SqlDbType.Int) { Value = operationID },
                        new SqlParameter("@InvoiceNumber", SqlDbType.NVarChar) { Value = expense.InvoiceNumber ?? (object)DBNull.Value },
                        new SqlParameter("@UserID", SqlDbType.Int) { Value = expense.UserID ?? currentUser.UserID },
                        new SqlParameter("@PaidAmount", SqlDbType.Decimal) { Value = expense.PaidAmount },
                        new SqlParameter("@FinanceStatusId", SqlDbType.Int) { Value = financeStatusId }
                    });

                            expense.ExpenseID = Convert.ToInt32(expenseCommand.ExecuteScalar());
                        }

                        transaction.Commit();
                        Debug.WriteLine($"SavePartsExpenses: {partsExpenses.Count} ta xarajat qo'shildi.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Debug.WriteLine($"Xato yuz berdi: {ex.Message}");
                        throw new InvalidOperationException($"Xarajatlar saqlanmadi: {ex.Message}", ex);
                    }
                }
            }
        }
    }
}

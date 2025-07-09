using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
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

        public void Update(PartsIncome entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.IncomeID <= 0)
                throw new ArgumentException("Noto'g'ri ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        @"UPDATE PartsIncome
                          SET PartID = @PartID, SupplierID = @SupplierID, Date = @Date, Quantity = @Quantity,
                              UnitPrice = @UnitPrice, Markup = @Markup, StatusID = @StatusID,
                              Finance_Status_Id = @Finance_Status_Id, OperationID = @OperationID,
                              StockID = @StockID, InvoiceNumber = @InvoiceNumber, UserID = @UserID,
                              PaidAmount = @PaidAmount, BatchID = @BatchID
                          WHERE IncomeID = @IncomeID", connection);
                    command.Parameters.AddWithValue("@IncomeID", entity.IncomeID);
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
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"ID {entity.IncomeID} bilan qism topilmadi.");
                    Debug.WriteLine($"Update: Qism {entity.IncomeID} ID bilan yangilandi.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Update SQL Xatosi: {ex.Message}");
                throw new Exception("Qism yangilashda xato.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Update Xatosi: {ex.Message}");
                throw new Exception("Noma'lum xato yuz berdi.", ex);
            }
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Noto'g'ri ID.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM PartsIncome WHERE IncomeID = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception($"ID {id} bilan qism topilmadi.");
                    Debug.WriteLine($"Delete: Qism {id} ID bilan o'chirildi.");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Delete SQL Xatosi: {ex.Message}");
                throw new Exception("Qism o'chirishda xato.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Delete Xatosi: {ex.Message}");
                throw new Exception("Noma'lum xato yuz berdi.", ex);
            }
        }
    }
}
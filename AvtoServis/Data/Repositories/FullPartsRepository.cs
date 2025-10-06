using AvtoServis.Data.Interfaces;
using AvtoServis.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AvtoServis.Data.Repositories
{
    public class FullPartsRepository : IFullPartsRepository
    {
        private readonly string connectionString;

        public FullPartsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<FullParts> GetFullParts()
        {
            Dictionary<string, FullParts> partsDict = new Dictionary<string, FullParts>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            p.PartID,
                            pi.IncomeID,
                            p.PartName,
                            p.CatalogNumber,
                            COALESCE(pi.Quantity, 0) - COALESCE(SUM(pe.Quantity), 0) AS RemainingQuantity,
                            CASE 
                                WHEN COALESCE(pi.Quantity, 0) - COALESCE(SUM(pe.Quantity), 0) > 0 THEN 1
                                ELSE 0
                            END AS IsAvailable,
                            s.Name AS StockName,
                            CASE 
                                WHEN sl.LocationID IS NOT NULL THEN 1
                                ELSE 0
                            END AS IsPlacedInStock,
                            sl.ShelfNumber,
                            cb.CarBrandName,
                            m.Name AS ManufacturerName,
                            pq.Name AS QualityName,
                            p.Characteristics,
                            p.PhotoPath,
                            pi.Quantity AS IncomeQuantity,
                            pi.UnitPrice AS IncomeUnitPrice,
                            pi.Markup,
                            pi.Date AS IncomeDate,
                            pi.InvoiceNumber AS IncomeInvoiceNumber,
                            pi.PaidAmount AS IncomePaidAmount,
                            sup.Name AS SupplierName,
                            b.Name AS BatchName,
                            fs.Name AS FinanceStatusName,
                            ist.Name AS IncomeStatusName,
                            pa.AttributeName,
                            pa.AttributeValue
                        FROM 
                            Parts p
                            LEFT JOIN PartsIncome pi ON p.PartID = pi.PartID
                            LEFT JOIN PartsExpenses pe ON pe.IncomeID = pi.IncomeID
                            LEFT JOIN CarBrand cb ON p.CarBrand_Id = cb.Id
                            LEFT JOIN Manufacturers m ON p.ManufacturerID = m.ManufacturerID
                            LEFT JOIN PartQualities pq ON p.QualityID = pq.QualityID
                            LEFT JOIN PartAttributes pa ON p.PartID = pa.PartID
                            LEFT JOIN Suppliers sup ON pi.SupplierID = sup.SupplierID
                            LEFT JOIN Batches b ON pi.BatchID = b.BatchID
                            LEFT JOIN Finance_Status fs ON pi.Finance_Status_Id = fs.Id
                            LEFT JOIN IncomeStatuses ist ON pi.StatusID = ist.StatusID
                            LEFT JOIN StockLocations sl ON p.PartID = sl.PartID
                            LEFT JOIN Stock s ON pi.StockID = s.StockID
                        GROUP BY 
                            p.PartID,
                            pi.IncomeID,
                            p.PartName,
                            p.CatalogNumber,
                            pi.Quantity,
                            pi.UnitPrice,
                            pi.Markup,
                            pi.Date,
                            pi.InvoiceNumber,
                            pi.PaidAmount,
                            sup.Name,
                            b.Name,
                            fs.Name,
                            ist.Name,
                            s.Name,
                            sl.LocationID,
                            sl.ShelfNumber,
                            cb.CarBrandName,
                            m.Name,
                            pq.Name,
                            p.Characteristics,
                            p.PhotoPath,
                            pa.AttributeName,
                            pa.AttributeValue
                        ORDER BY 
                            CASE WHEN pi.IncomeID IS NOT NULL THEN 0 ELSE 1 END,
                            p.PartName, 
                            p.PartID, 
                            pi.IncomeID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int partId = reader.GetInt32(0);
                                int? incomeId = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1);
                                string key = $"{partId}_{incomeId ?? 0}"; // Har bir IncomeID uchun noyob kalit

                                if (!partsDict.TryGetValue(key, out FullParts part))
                                {
                                    part = new FullParts
                                    {
                                        PartID = partId,
                                        IncomeID = incomeId,
                                        PartName = reader.IsDBNull(2) ? null : reader.GetString(2),
                                        CatalogNumber = reader.IsDBNull(3) ? null : reader.GetString(3),
                                        RemainingQuantity = reader.GetInt32(4),
                                        IsAvailable = reader.GetInt32(5) == 1,
                                        StockName = reader.IsDBNull(6) ? null : reader.GetString(6),
                                        IsPlacedInStock = reader.GetInt32(7) == 1,
                                        ShelfNumber = reader.IsDBNull(8) ? null : reader.GetString(8),
                                        CarBrandName = reader.IsDBNull(9) ? null : reader.GetString(9),
                                        ManufacturerName = reader.IsDBNull(10) ? null : reader.GetString(10),
                                        QualityName = reader.IsDBNull(11) ? null : reader.GetString(11),
                                        Characteristics = reader.IsDBNull(12) ? null : reader.GetString(12),
                                        PhotoPath = reader.IsDBNull(13) ? null : reader.GetString(13),
                                        IncomeQuantity = reader.IsDBNull(14) ? null : (int?)reader.GetInt32(14),
                                        IncomeUnitPrice = reader.IsDBNull(15) ? null : (decimal?)reader.GetDecimal(15),
                                        Markup = reader.IsDBNull(16) ? null : (decimal?)reader.GetDecimal(16),
                                        IncomeDate = reader.IsDBNull(17) ? null : (DateTime?)reader.GetDateTime(17),
                                        IncomeInvoiceNumber = reader.IsDBNull(18) ? null : reader.GetString(18),
                                        IncomePaidAmount = reader.IsDBNull(19) ? null : (decimal?)reader.GetDecimal(19),
                                        SupplierName = reader.IsDBNull(20) ? null : reader.GetString(20),
                                        BatchName = reader.IsDBNull(21) ? null : reader.GetString(21),
                                        FinanceStatusName = reader.IsDBNull(22) ? null : reader.GetString(22),
                                        IncomeStatusName = reader.IsDBNull(23) ? null : reader.GetString(23),
                                        Attributes = new List<PartAttribute>()
                                    };
                                    partsDict.Add(key, part);
                                }

                                if (!reader.IsDBNull(24) && !reader.IsDBNull(25))
                                {
                                    part.Attributes.Add(new PartAttribute
                                    {
                                        AttributeName = reader.GetString(24),
                                        AttributeValue = reader.GetString(25)
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ma'lumotlar bazasi bilan bog'lanishda xatolik: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ma'lumotlarni olishda xatolik: " + ex.Message, ex);
            }

            return partsDict.Values.ToList();
        }

        public List<FullParts> GetTopSellingParts()
        {
            Dictionary<string, FullParts> partsDict = new Dictionary<string, FullParts>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        WITH TopParts AS (
                            SELECT 
                                p.PartID,
                                SUM(COALESCE(pe.Quantity, 0)) AS TotalSoldQuantity
                            FROM Parts p
                            LEFT JOIN PartsExpenses pe ON p.PartID = pe.PartID
                            GROUP BY p.PartID
                            ORDER BY TotalSoldQuantity DESC
                            OFFSET 0 ROWS FETCH NEXT 30 ROWS ONLY
                        )
                        SELECT 
                            p.PartID,
                            pi.IncomeID,
                            p.PartName,
                            p.CatalogNumber,
                            COALESCE(pi.Quantity, 0) - COALESCE(SUM(pe.Quantity), 0) AS RemainingQuantity,
                            CASE 
                                WHEN COALESCE(pi.Quantity, 0) - COALESCE(SUM(pe.Quantity), 0) > 0 THEN 1
                                ELSE 0
                            END AS IsAvailable,
                            s.Name AS StockName,
                            CASE 
                                WHEN sl.LocationID IS NOT NULL THEN 1
                                ELSE 0
                            END AS IsPlacedInStock,
                            sl.ShelfNumber,
                            cb.CarBrandName,
                            m.Name AS ManufacturerName,
                            pq.Name AS QualityName,
                            p.Characteristics,
                            p.PhotoPath,
                            pi.Quantity AS IncomeQuantity,
                            pi.UnitPrice AS IncomeUnitPrice,
                            pi.Markup,
                            pi.Date AS IncomeDate,
                            pi.InvoiceNumber AS IncomeInvoiceNumber,
                            pi.PaidAmount AS IncomePaidAmount,
                            sup.Name AS SupplierName,
                            b.Name AS BatchName,
                            fs.Name AS FinanceStatusName,
                            ist.Name AS IncomeStatusName,
                            pa.AttributeName,
                            pa.AttributeValue,
                            tp.TotalSoldQuantity
                        FROM 
                            Parts p
                            INNER JOIN TopParts tp ON p.PartID = tp.PartID
                            LEFT JOIN PartsIncome pi ON p.PartID = pi.PartID
                            LEFT JOIN PartsExpenses pe ON pe.IncomeID = pi.IncomeID
                            LEFT JOIN CarBrand cb ON p.CarBrand_Id = cb.Id
                            LEFT JOIN Manufacturers m ON p.ManufacturerID = m.ManufacturerID
                            LEFT JOIN PartQualities pq ON p.QualityID = pq.QualityID
                            LEFT JOIN PartAttributes pa ON p.PartID = pa.PartID
                            LEFT JOIN Suppliers sup ON pi.SupplierID = sup.SupplierID
                            LEFT JOIN Batches b ON pi.BatchID = b.BatchID
                            LEFT JOIN Finance_Status fs ON pi.Finance_Status_Id = fs.Id
                            LEFT JOIN IncomeStatuses ist ON pi.StatusID = ist.StatusID
                            LEFT JOIN StockLocations sl ON p.PartID = sl.PartID
                            LEFT JOIN Stock s ON pi.StockID = s.StockID
                        GROUP BY 
                            p.PartID,
                            pi.IncomeID,
                            p.PartName,
                            p.CatalogNumber,
                            pi.Quantity,
                            pi.UnitPrice,
                            pi.Markup,
                            pi.Date,
                            pi.InvoiceNumber,
                            pi.PaidAmount,
                            sup.Name,
                            b.Name,
                            fs.Name,
                            ist.Name,
                            s.Name,
                            sl.LocationID,
                            sl.ShelfNumber,
                            cb.CarBrandName,
                            m.Name,
                            pq.Name,
                            p.Characteristics,
                            p.PhotoPath,
                            pa.AttributeName,
                            pa.AttributeValue,
                            tp.TotalSoldQuantity
                        ORDER BY 
                            tp.TotalSoldQuantity DESC,
                            CASE WHEN pi.IncomeID IS NOT NULL THEN 0 ELSE 1 END,
                            p.PartName, 
                            p.PartID, 
                            pi.IncomeID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int partId = reader.GetInt32(0);
                                int? incomeId = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1);
                                string key = $"{partId}_{incomeId ?? 0}"; // Har bir IncomeID uchun noyob kalit

                                if (!partsDict.TryGetValue(key, out FullParts part))
                                {
                                    part = new FullParts
                                    {
                                        PartID = partId,
                                        IncomeID = incomeId,
                                        PartName = reader.IsDBNull(2) ? null : reader.GetString(2),
                                        CatalogNumber = reader.IsDBNull(3) ? null : reader.GetString(3),
                                        RemainingQuantity = reader.GetInt32(4),
                                        IsAvailable = reader.GetInt32(5) == 1,
                                        StockName = reader.IsDBNull(6) ? null : reader.GetString(6),
                                        IsPlacedInStock = reader.GetInt32(7) == 1,
                                        ShelfNumber = reader.IsDBNull(8) ? null : reader.GetString(8),
                                        CarBrandName = reader.IsDBNull(9) ? null : reader.GetString(9),
                                        ManufacturerName = reader.IsDBNull(10) ? null : reader.GetString(10),
                                        QualityName = reader.IsDBNull(11) ? null : reader.GetString(11),
                                        Characteristics = reader.IsDBNull(12) ? null : reader.GetString(12),
                                        PhotoPath = reader.IsDBNull(13) ? null : reader.GetString(13),
                                        IncomeQuantity = reader.IsDBNull(14) ? null : (int?)reader.GetInt32(14),
                                        IncomeUnitPrice = reader.IsDBNull(15) ? null : (decimal?)reader.GetDecimal(15),
                                        Markup = reader.IsDBNull(16) ? null : (decimal?)reader.GetDecimal(16),
                                        IncomeDate = reader.IsDBNull(17) ? null : (DateTime?)reader.GetDateTime(17),
                                        IncomeInvoiceNumber = reader.IsDBNull(18) ? null : reader.GetString(18),
                                        IncomePaidAmount = reader.IsDBNull(19) ? null : (decimal?)reader.GetDecimal(19),
                                        SupplierName = reader.IsDBNull(20) ? null : reader.GetString(20),
                                        BatchName = reader.IsDBNull(21) ? null : reader.GetString(21),
                                        FinanceStatusName = reader.IsDBNull(22) ? null : reader.GetString(22),
                                        IncomeStatusName = reader.IsDBNull(23) ? null : reader.GetString(23),
                                        Attributes = new List<PartAttribute>()
                                    };
                                    partsDict.Add(key, part);
                                }

                                if (!reader.IsDBNull(24) && !reader.IsDBNull(25))
                                {
                                    part.Attributes.Add(new PartAttribute
                                    {
                                        AttributeName = reader.GetString(24),
                                        AttributeValue = reader.GetString(25)
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ma'lumotlar bazasi bilan bog'lanishda xatolik: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ma'lumotlarni olishda xatolik: " + ex.Message, ex);
            }

            return partsDict.Values.ToList();
        }

        public FullParts GetPartById(int partId)
        {
            FullParts part = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            p.PartID,
                            pi.IncomeID,
                            p.PartName,
                            p.CatalogNumber,
                            COALESCE(pi.Quantity, 0) - COALESCE(SUM(pe.Quantity), 0) AS RemainingQuantity,
                            CASE 
                                WHEN COALESCE(pi.Quantity, 0) - COALESCE(SUM(pe.Quantity), 0) > 0 THEN 1
                                ELSE 0
                            END AS IsAvailable,
                            s.Name AS StockName,
                            CASE 
                                WHEN sl.LocationID IS NOT NULL THEN 1
                                ELSE 0
                            END AS IsPlacedInStock,
                            sl.ShelfNumber,
                            cb.CarBrandName,
                            m.Name AS ManufacturerName,
                            pq.Name AS QualityName,
                            p.Characteristics,
                            p.PhotoPath,
                            pi.Quantity AS IncomeQuantity,
                            pi.UnitPrice AS IncomeUnitPrice,
                            pi.Markup,
                            pi.Date AS IncomeDate,
                            pi.InvoiceNumber AS IncomeInvoiceNumber,
                            pi.PaidAmount AS IncomePaidAmount,
                            sup.Name AS SupplierName,
                            b.Name AS BatchName,
                            fs.Name AS FinanceStatusName,
                            ist.Name AS IncomeStatusName,
                            pa.AttributeName,
                            pa.AttributeValue
                        FROM 
                            Parts p
                            LEFT JOIN PartsIncome pi ON p.PartID = pi.PartID
                            LEFT JOIN PartsExpenses pe ON pe.IncomeID = pi.IncomeID
                            LEFT JOIN CarBrand cb ON p.CarBrand_Id = cb.Id
                            LEFT JOIN Manufacturers m ON p.ManufacturerID = m.ManufacturerID
                            LEFT JOIN PartQualities pq ON p.QualityID = pq.QualityID
                            LEFT JOIN PartAttributes pa ON p.PartID = pa.PartID
                            LEFT JOIN Suppliers sup ON pi.SupplierID = sup.SupplierID
                            LEFT JOIN Batches b ON pi.BatchID = b.BatchID
                            LEFT JOIN Finance_Status fs ON pi.Finance_Status_Id = fs.Id
                            LEFT JOIN IncomeStatuses ist ON pi.StatusID = ist.StatusID
                            LEFT JOIN StockLocations sl ON p.PartID = sl.PartID
                            LEFT JOIN Stock s ON pi.StockID = s.StockID
                        WHERE 
                            p.PartID = @PartID
                        GROUP BY 
                            p.PartID,
                            pi.IncomeID,
                            p.PartName,
                            p.CatalogNumber,
                            pi.Quantity,
                            pi.UnitPrice,
                            pi.Markup,
                            pi.Date,
                            pi.InvoiceNumber,
                            pi.PaidAmount,
                            sup.Name,
                            b.Name,
                            fs.Name,
                            ist.Name,
                            s.Name,
                            sl.LocationID,
                            sl.ShelfNumber,
                            cb.CarBrandName,
                            m.Name,
                            pq.Name,
                            p.Characteristics,
                            p.PhotoPath,
                            pa.AttributeName,
                            pa.AttributeValue
                        ORDER BY 
                            CASE WHEN pi.IncomeID IS NOT NULL THEN 0 ELSE 1 END,
                            p.PartName, 
                            p.PartID, 
                            pi.IncomeID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PartID", partId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (part == null)
                                {
                                    part = new FullParts
                                    {
                                        PartID = reader.GetInt32(0),
                                        IncomeID = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1),
                                        PartName = reader.IsDBNull(2) ? null : reader.GetString(2),
                                        CatalogNumber = reader.IsDBNull(3) ? null : reader.GetString(3),
                                        RemainingQuantity = reader.GetInt32(4),
                                        IsAvailable = reader.GetInt32(5) == 1,
                                        StockName = reader.IsDBNull(6) ? null : reader.GetString(6),
                                        IsPlacedInStock = reader.GetInt32(7) == 1,
                                        ShelfNumber = reader.IsDBNull(8) ? null : reader.GetString(8),
                                        CarBrandName = reader.IsDBNull(9) ? null : reader.GetString(9),
                                        ManufacturerName = reader.IsDBNull(10) ? null : reader.GetString(10),
                                        QualityName = reader.IsDBNull(11) ? null : reader.GetString(11),
                                        Characteristics = reader.IsDBNull(12) ? null : reader.GetString(12),
                                        PhotoPath = reader.IsDBNull(13) ? null : reader.GetString(13),
                                        IncomeQuantity = reader.IsDBNull(14) ? null : (int?)reader.GetInt32(14),
                                        IncomeUnitPrice = reader.IsDBNull(15) ? null : (decimal?)reader.GetDecimal(15),
                                        Markup = reader.IsDBNull(16) ? null : (decimal?)reader.GetDecimal(16),
                                        IncomeDate = reader.IsDBNull(17) ? null : (DateTime?)reader.GetDateTime(17),
                                        IncomeInvoiceNumber = reader.IsDBNull(18) ? null : reader.GetString(18),
                                        IncomePaidAmount = reader.IsDBNull(19) ? null : (decimal?)reader.GetDecimal(19),
                                        SupplierName = reader.IsDBNull(20) ? null : reader.GetString(20),
                                        BatchName = reader.IsDBNull(21) ? null : reader.GetString(21),
                                        FinanceStatusName = reader.IsDBNull(22) ? null : reader.GetString(22),
                                        IncomeStatusName = reader.IsDBNull(23) ? null : reader.GetString(23),
                                        Attributes = new List<PartAttribute>()
                                    };
                                }

                                if (!reader.IsDBNull(24) && !reader.IsDBNull(25))
                                {
                                    part.Attributes.Add(new PartAttribute
                                    {
                                        AttributeName = reader.GetString(24),
                                        AttributeValue = reader.GetString(25)
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ma'lumotlar bazasi bilan bog'lanishda xatolik: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ma'lumotlarni olishda xatolik: " + ex.Message, ex);
            }

            return part;
        }
    }

    public class PartAttribute
    {
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }
}
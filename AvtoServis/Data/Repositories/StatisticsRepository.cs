using AvtoServis.Data.Interfaces;
using AvtoServis.Model.DTOs;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AvtoServis.Data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly string connectionString;

        public StatisticsRepository(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public int GetTotalDebtors()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT COUNT(DISTINCT CustomerID) AS TotalDebtors
                        FROM (
                            SELECT CustomerID
                            FROM PartsExpenses
                            WHERE PaidAmount < (Quantity * UnitPrice)
                            UNION
                            SELECT CustomerID
                            FROM ServiceOrders
                            WHERE PaidAmount < TotalAmount
                        ) AS Debtors";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ma'lumotlar bazasi bilan bog'lanishda xatolik: " + ex.Message, ex);
            }
        }

        public decimal GetTotalCustomerDebt()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            COALESCE(SUM(PartsDebt), 0) + COALESCE(SUM(ServiceDebt), 0) AS TotalDebt
                        FROM (
                            SELECT SUM((Quantity * UnitPrice) - PaidAmount) AS PartsDebt
                            FROM PartsExpenses
                            WHERE PaidAmount < (Quantity * UnitPrice)
                        ) AS Parts
                        CROSS JOIN (
                            SELECT SUM(TotalAmount - PaidAmount) AS ServiceDebt
                            FROM ServiceOrders
                            WHERE PaidAmount < TotalAmount
                        ) AS Services";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        return Convert.ToDecimal(cmd.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ma'lumotlar bazasi bilan bog'lanishda xatolik: " + ex.Message, ex);
            }
        }

        public int GetSuppliersOwed()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT COUNT(DISTINCT SupplierID) AS SuppliersOwed
                        FROM PartsIncome
                        WHERE PaidAmount < (Quantity * UnitPrice)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ma'lumotlar bazasi bilan bog'lanishda xatolik: " + ex.Message, ex);
            }
        }

        public decimal GetTotalSupplierDebt()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT COALESCE(SUM((Quantity * UnitPrice) - PaidAmount), 0) AS TotalDebt
                        FROM PartsIncome
                        WHERE PaidAmount < (Quantity * UnitPrice)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        return Convert.ToDecimal(cmd.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ma'lumotlar bazasi bilan bog'lanishda xatolik: " + ex.Message, ex);
            }
        }

        public List<TopPart> GetTopSellingParts(int topN)
        {
            List<TopPart> topParts = new List<TopPart>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT TOP (@TopN) 
                            p.PartName,
                            SUM(COALESCE(pe.Quantity, 0)) AS TotalSoldQuantity
                        FROM Parts p
                        LEFT JOIN PartsExpenses pe ON p.PartID = pe.PartID
                        GROUP BY p.PartName
                        ORDER BY TotalSoldQuantity DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TopN", topN);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                topParts.Add(new TopPart
                                {
                                    PartName = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0),
                                    Quantity = reader.GetInt32(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ma'lumotlar bazasi bilan bog'lanishda xatolik: " + ex.Message, ex);
            }
            return topParts;
        }

        public List<TopService> GetTopServices(int topN)
        {
            List<TopService> topServices = new List<TopService>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT TOP (@TopN) 
                            s.Name AS ServiceName,
                            SUM(COALESCE(so.Quantity, 0)) AS TotalSoldQuantity
                        FROM Services s
                        LEFT JOIN ServiceOrders so ON s.ServiceID = so.ServiceID
                        GROUP BY s.Name
                        ORDER BY TotalSoldQuantity DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TopN", topN);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                topServices.Add(new TopService
                                {
                                    ServiceName = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0),
                                    Quantity = reader.GetInt32(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ma'lumotlar bazasi bilan bog'lanishda xatolik: " + ex.Message, ex);
            }
            return topServices;
        }

        public List<DailySale> GetDailySales()
        {
            List<DailySale> dailySales = new List<DailySale>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        WITH Last10Days AS (
    SELECT CAST(GETDATE() AS date) AS SaleDay
    UNION ALL
    SELECT DATEADD(day, -1, SaleDay) 
    FROM Last10Days 
    WHERE SaleDay > DATEADD(day, -9, CAST(GETDATE() AS date))
),
PartsAgg AS (
    SELECT CAST(Date AS date) AS SaleDay, SUM(Quantity) AS PartsQuantity
    FROM PartsExpenses
    WHERE Date >= DATEADD(day, -10, CAST(GETDATE() AS date))
    GROUP BY CAST(Date AS date)
),
ServicesAgg AS (
    SELECT CAST(OrderDate AS date) AS SaleDay, SUM(ISNULL(Quantity,0)) AS ServicesQuantity
    FROM ServiceOrders
    WHERE OrderDate >= DATEADD(day, -10, CAST(GETDATE() AS date))
    GROUP BY CAST(OrderDate AS date)
)
SELECT 
    d.SaleDay,
    ISNULL(p.PartsQuantity,0) AS PartsQuantity,
    ISNULL(s.ServicesQuantity,0) AS ServicesQuantity
FROM Last10Days d
LEFT JOIN PartsAgg p ON d.SaleDay = p.SaleDay
LEFT JOIN ServicesAgg s ON d.SaleDay = s.SaleDay
ORDER BY d.SaleDay DESC
OPTION (MAXRECURSION 0);

";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dailySales.Add(new DailySale
                                {
                                    Day = reader.GetDateTime(0),
                                    PartsQuantity = reader.GetInt32(1),
                                    ServicesQuantity = reader.GetInt32(2)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ma'lumotlar bazasi bilan bog'lanishda xatolik: " + ex.Message, ex);
            }
            return dailySales;
        }
    }
}
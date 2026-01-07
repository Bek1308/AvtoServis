using System;
using System.Collections.Generic;
using AvtoServis.Data.Interfaces;
using AvtoServis.Model.DTOs;

namespace AvtoServis.ViewModels.Screens
{
    public class StatisticsViewModel
    {
        private readonly IStatisticsRepository _statsRepo;

        public StatisticsViewModel(IStatisticsRepository statsRepo)
        {
            _statsRepo = statsRepo ?? throw new ArgumentNullException(nameof(statsRepo));
        }

        public int TotalDebtors
        {
            get
            {
                try
                {
                    return _statsRepo.GetTotalDebtors();
                }
                catch (Exception ex)
                {
                    throw new Exception("Qarzdorlar sonini olishda xatolik: " + ex.Message, ex);
                }
            }
        }

        public decimal TotalCustomerDebt
        {
            get
            {
                try
                {
                    return _statsRepo.GetTotalCustomerDebt();
                }
                catch (Exception ex)
                {
                    throw new Exception("Mijozlar qarzini olishda xatolik: " + ex.Message, ex);
                }
            }
        }

        public int SuppliersOwed
        {
            get
            {
                try
                {
                    return _statsRepo.GetSuppliersOwed();
                }
                catch (Exception ex)
                {
                    throw new Exception("Qarzdor yetkazuvchilarni olishda xatolik: " + ex.Message, ex);
                }
            }
        }

        public decimal TotalSupplierDebt
        {
            get
            {
                try
                {
                    return _statsRepo.GetTotalSupplierDebt();
                }
                catch (Exception ex)
                {
                    throw new Exception("Yetkazuvchi qarzini olishda xatolik: " + ex.Message, ex);
                }
            }
        }

        public List<TopPart> TopSellingParts
        {
            get
            {
                try
                {
                    return _statsRepo.GetTopSellingParts(10);
                }
                catch (Exception ex)
                {
                    throw new Exception("Eng ko'p sotilgan mahsulotlarni olishda xatolik: " + ex.Message, ex);
                }
            }
        }

        public List<TopService> TopServices
        {
            get
            {
                try
                {
                    return _statsRepo.GetTopServices(10);
                }
                catch (Exception ex)
                {
                    throw new Exception("Eng ko'p sotilgan xizmatlarni olishda xatolik: " + ex.Message, ex);
                }
            }
        }

        public List<DailySale> DailySales
        {
            get
            {
                try
                {
                    return _statsRepo.GetDailySales();
                }
                catch (Exception ex)
                {
                    throw new Exception("Kunlik sotuvlarni olishda xatolik: " + ex.Message, ex);
                }
            }
        }
    }

    public class DailySale
    {
        public DateTime Day { get; set; }
        public int PartsQuantity { get; set; }
        public int ServicesQuantity { get; set; }
        public int DaysAgo => (DateTime.Today - Day.Date).Days;
    }
}
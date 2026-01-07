using AvtoServis.Model.DTOs;
using AvtoServis.ViewModels.Screens;
using System;
using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface IStatisticsRepository
    {
        /// <summary>
        /// Qarzdor mijozlar sonini qaytaradi.
        /// </summary>
        int GetTotalDebtors();

        /// <summary>
        /// Jami mijozlar qarzdorligi summasini qaytaradi.
        /// </summary>
        decimal GetTotalCustomerDebt();

        /// <summary>
        /// Qarzimiz bo‘lgan yetkazib beruvchilar sonini qaytaradi.
        /// </summary>
        int GetSuppliersOwed();

        /// <summary>
        /// Yetkazib beruvchilarga jami qarzdorlik summasini qaytaradi.
        /// </summary>
        decimal GetTotalSupplierDebt();

        /// <summary>
        /// Eng ko‘p sotilgan ehtiyot qismlar ro‘yxatini qaytaradi.
        /// </summary>
        List<TopPart> GetTopSellingParts(int topN);

        /// <summary>
        /// Eng ko‘p sotilgan xizmatlar ro‘yxatini qaytaradi.
        /// </summary>
        List<TopService> GetTopServices(int topN);

        /// <summary>
        /// Oxirgi 10 kunlik kunlik savdolarni qaytaradi.
        /// </summary>
        List<DailySale> GetDailySales();
    }
}

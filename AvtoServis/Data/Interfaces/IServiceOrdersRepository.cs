using AvtoServis.Model.Entities;
using AvtoServis.Model.DTOs;
using System;
using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface IServiceOrdersRepository
    {
        List<ServiceOrder> GetAll();
        ServiceOrder GetById(int id);
        void Add(ServiceOrder entity);
        void Update(ServiceOrder entity);
        public void ReturnService(ServiceOrder serviceOrder);
        void Delete(int orderId, string reason);
        List<FullService> GetTopSellingServices(); // Yangi metod: Top 30 ko'p sotilgan xizmatlar
        List<FullService> GetFullServices(); // Yangi metod: Hammasini oladi Hatta umuman sotilmagan bo'lsa ham! 
        FullService? GetServiceById(int serviceId);
    }
}

using AvtoServis.Model.DTOs;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System.Collections.Generic;

namespace AvtoServis.Data.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);

        /// <summary>
        /// Ищет клиентов в базе данных по частичному совпадению имени или телефона.
        /// </summary>
        /// <param name="searchText">Текст для поиска.</param>
        /// <returns>Список клиентов, соответствующих запросу.</returns>
        Task<List<CustomerInfo>> SearchCustomersAsync(string searchText);
        CustomerWithVehiclesDto GetCustomerWithVehicles(int customerId);
        /// <summary>
        /// Barcha mijozlarning qarzlari va mashinalari haqida to'liq ma'lumot olish.
        /// </summary>
        /// <returns>Mijozlar, ularning mashinalari va qarz tafsilotlari ro'yxati.</returns>
        Task<List<CustomerDebtInfoDto>> GetAllCustomersWithDebtDetailsAsync();

        /// <summary>
        /// Belgilangan mijozning qarzlari va mashinalari haqida to'liq ma'lumot olish.
        /// </summary>
        /// <param name="customerId">Mijozning ID raqami.</param>
        /// <returns>Mijozning ma'lumotlari, mashinalari va qarz tafsilotlari.</returns>
        public CustomerDebtInfoDto GetCustomerWithDebtDetailsById(int customerId);

    }
}
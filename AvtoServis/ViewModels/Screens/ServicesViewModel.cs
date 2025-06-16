using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AvtoServis.ViewModels.Screens
{
    public class ServicesViewModel
    {
        private readonly IRepository<Service> _repository;

        public ServicesViewModel(IRepository<Service> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<Service> LoadServices()
        {
            try
            {
                var services = _repository.GetAll();
                System.Diagnostics.Debug.WriteLine($"LoadServices: Loaded {services.Count} services.");
                return services;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadServices Error: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки услуг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Service>();
            }
        }

        public void AddService(Service service)
        {
            try
            {
                ValidateService(service);
                _repository.Add(service);
                System.Diagnostics.Debug.WriteLine($"AddService: Added service '{service.Name}' with ID {service.ServiceID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddService Error: {ex.Message}");
                throw new Exception($"Ошибка при добавлении услуги: {ex.Message}", ex);
            }
        }

        public void UpdateService(Service service)
        {
            try
            {
                ValidateService(service);
                _repository.Update(service);
                System.Diagnostics.Debug.WriteLine($"UpdateService: Updated service '{service.Name}' with ID {service.ServiceID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateService Error: {ex.Message}");
                throw new Exception($"Ошибка при обновлении услуги: {ex.Message}", ex);
            }
        }

        public void DeleteService(int id)
        {
            try
            {
                _repository.Delete(id);
                System.Diagnostics.Debug.WriteLine($"DeleteService: Deleted service with ID {id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteService Error: {ex.Message}");
                throw new Exception($"Ошибка при удалении услуги: {ex.Message}", ex);
            }
        }

        public List<Service> SearchServices(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return LoadServices();

                var services = _repository.Search(searchText);
                System.Diagnostics.Debug.WriteLine($"SearchServices: Found {services.Count} services for query '{searchText}'.");
                return services;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchServices Error: {ex.Message}");
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Service>();
            }
        }

        public List<Service> FilterServicesByPrice(decimal minPrice, decimal maxPrice)
        {
            try
            {
                if (minPrice < 0 || maxPrice < minPrice)
                    throw new ArgumentException("Минимальная цена не может быть отрицательной, а максимальная цена должна быть больше минимальной.");

                var services = _repository.FilterByPrice(minPrice, maxPrice);
                System.Diagnostics.Debug.WriteLine($"FilterServicesByPrice: Found {services.Count} services between {minPrice} and {maxPrice}.");
                return services;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FilterServicesByPrice Error: {ex.Message}");
                MessageBox.Show($"Ошибка фильтрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Service>();
            }
        }

        public List<Service> FilterServices(decimal? minPrice, decimal? maxPrice, bool highPrice)
        {
            var services = LoadServices();
            return services.Where(s =>
                (!minPrice.HasValue || s.Price >= minPrice) &&
                (!maxPrice.HasValue || s.Price <= maxPrice) &&
                (!highPrice || s.Price > 1000) // Yuqori narx chegarasi
            ).ToList();
        }

        private void ValidateService(Service service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (string.IsNullOrWhiteSpace(service.Name))
                throw new ArgumentException("Название услуги не может быть пустым.");

            if (service.Name.Length > 100)
                throw new ArgumentException("Название услуги не может превышать 100 символов.");

            // Rus va lotin harflarni qo'llab-quvvatlash
            if (!Regex.IsMatch(service.Name, @"^[а-яА-Яa-zA-Z0-9\s]+$"))
                throw new ArgumentException("Название услуги может содержать только буквы (русские или латинские), цифры и пробелы.");

            if (service.Price < 0)
                throw new ArgumentException("Цена не может быть отрицательной.");

            if (service.Price > 999999.99m)
                throw new ArgumentException("Цена не может превышать 999999.99.");
        }
    }
}
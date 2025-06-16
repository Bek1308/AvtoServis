using AvtoServis.Data.Interfaces;
using AvtoServis.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AvtoServis.ViewModels.Screens
{
    public class ManufacturersViewModel
    {
        private readonly IRepository<Manufacturer> _repository;

        public ManufacturersViewModel(IRepository<Manufacturer> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<Manufacturer> LoadManufacturers()
        {
            try
            {
                var manufacturers = _repository.GetAll();
                System.Diagnostics.Debug.WriteLine($"LoadManufacturers: Loaded {manufacturers.Count} manufacturers.");
                return manufacturers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadManufacturers Error: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки производителей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Manufacturer>();
            }
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            try
            {
                ValidateManufacturer(manufacturer);
                _repository.Add(manufacturer);
                System.Diagnostics.Debug.WriteLine($"AddManufacturer: Added manufacturer '{manufacturer.Name}' with ID {manufacturer.ManufacturerID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddManufacturer Error: {ex.Message}");
                throw new Exception($"Ошибка при добавлении производителя: {ex.Message}", ex);
            }
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            try
            {
                ValidateManufacturer(manufacturer);
                _repository.Update(manufacturer);
                System.Diagnostics.Debug.WriteLine($"UpdateManufacturer: Updated manufacturer '{manufacturer.Name}' with ID {manufacturer.ManufacturerID}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateManufacturer Error: {ex.Message}");
                throw new Exception($"Ошибка при обновлении производителя: {ex.Message}", ex);
            }
        }

        public void DeleteManufacturer(int id)
        {
            try
            {
                _repository.Delete(id);
                System.Diagnostics.Debug.WriteLine($"DeleteManufacturer: Deleted manufacturer with ID {id}.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteManufacturer Error: {ex.Message}");
                throw new Exception($"Ошибка при удалении производителя: {ex.Message}", ex);
            }
        }

        public List<Manufacturer> SearchManufacturers(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return LoadManufacturers();

                var manufacturers = _repository.Search(searchText);
                System.Diagnostics.Debug.WriteLine($"SearchManufacturers: Found {manufacturers.Count} manufacturers for query '{searchText}'.");
                return manufacturers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SearchManufacturers Error: {ex.Message}");
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Manufacturer>();
            }
        }

        public List<Manufacturer> FilterManufacturers(bool sortAlphabetically)
        {
            try
            {
                var manufacturers = LoadManufacturers();
                if (sortAlphabetically)
                    manufacturers = manufacturers.OrderBy(m => m.Name).ToList();
                System.Diagnostics.Debug.WriteLine($"FilterManufacturers: Sorted {manufacturers.Count} manufacturers.");
                return manufacturers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FilterManufacturers Error: {ex.Message}");
                MessageBox.Show($"Ошибка фильтрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Manufacturer>();
            }
        }

        private void ValidateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            if (string.IsNullOrWhiteSpace(manufacturer.Name))
                throw new ArgumentException("Название производителя не может быть пустым.");

            if (manufacturer.Name.Length > 100)
                throw new ArgumentException("Название производителя не может превышать 100 символов.");

            if (!Regex.IsMatch(manufacturer.Name, @"^[а-яА-Яa-zA-Z0-9\s]+$"))
                throw new ArgumentException("Название производителя может содержать только буквы (русские или латинские), цифры и пробелы.");
        }
    }
}
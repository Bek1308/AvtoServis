using AvtoServis.ViewModels.Screens;
using System;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class CarModelFilterDialog : Form
    {
        private readonly CarModelsViewModel _viewModel;
        public int? MinYear { get; private set; }
        public int? MaxYear { get; private set; }
        public int? BrandId { get; private set; }

        public CarModelFilterDialog(CarModelsViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            InitializeComponent();
            LoadBrands();
        }

        private void LoadBrands()
        {
            try
            {
                var brands = _viewModel.LoadBrands();
                brands.Insert(0, new Model.Entities.CarBrand { Id = 0, CarBrandName = "Все марки" });
                cmbBrand.DataSource = brands;
                cmbBrand.DisplayMember = "CarBrandName";
                cmbBrand.ValueMember = "Id";
                cmbBrand.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке марок: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadBrands Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtMinYear.Text) && !int.TryParse(txtMinYear.Text, out int minYear))
                {
                    MessageBox.Show("Минимальный год должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(txtMaxYear.Text) && !int.TryParse(txtMaxYear.Text, out int maxYear))
                {
                    MessageBox.Show("Максимальный год должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MinYear = string.IsNullOrWhiteSpace(txtMinYear.Text) ? (int?)null : int.Parse(txtMinYear.Text);
                MaxYear = string.IsNullOrWhiteSpace(txtMaxYear.Text) ? (int?)null : int.Parse(txtMaxYear.Text);
                BrandId = (int?)cmbBrand.SelectedValue == 0 ? null : (int?)cmbBrand.SelectedValue;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при применении фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"btnApply_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
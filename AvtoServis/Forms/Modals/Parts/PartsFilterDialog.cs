using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class PartsFilterDialog : Form
    {
        private readonly PartsViewModel _viewModel;
        public int? BrandId { get; private set; }
        public int? QualityId { get; private set; }

        public PartsFilterDialog(PartsViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            InitializeComponent();
            LoadFilters();
        }

        private void LoadFilters()
        {
            try
            {
                var brands = _viewModel.LoadBrands();
                brands.Insert(0, new Model.Entities.CarBrand { Id = 0, CarBrandName = "Все марки" });
                cmbBrand.DataSource = brands;
                cmbBrand.DisplayMember = "CarBrandName";
                cmbBrand.ValueMember = "Id";
                cmbBrand.SelectedIndex = 0;

                var qualities = _viewModel.LoadQualities();
                qualities.Insert(0, new Model.Entities.PartQuality { QualityID = 0, Name = "Все качества" });
                cmbQuality.DataSource = qualities;
                cmbQuality.DisplayMember = "Name";
                cmbQuality.ValueMember = "QualityID";
                cmbQuality.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке фильтров: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadFilters Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                BrandId = (int?)cmbBrand.SelectedValue;
                if (BrandId == 0) BrandId = null;
                QualityId = (int?)cmbQuality.SelectedValue;
                if (QualityId == 0) QualityId = null;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при применении фильтров: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"BtnApply_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }
    }
}
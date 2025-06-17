using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class PartDetailsDialog : Form
    {
        private readonly PartsViewModel _viewModel;
        private readonly int _partId;
        private Part _part;

        public PartDetailsDialog(PartsViewModel viewModel, int partId)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _partId = partId;
            InitializeComponent();
            LoadPartDetails();
        }

        private void LoadPartDetails()
        {
            try
            {
                _part = _viewModel.LoadParts().Find(p => p.PartID == _partId);
                if (_part == null)
                {
                    MessageBox.Show("Деталь не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                lblPartIDValue.Text = _part.PartID.ToString();
                lblBrandValue.Text = _part.CarBrandName;
                lblCatalogNumberValue.Text = _part.CatalogNumber;
                lblManufacturerIDValue.Text = _part.ManufacturerID.ToString();
                lblQualityValue.Text = _part.QualityName;
                lblPartNameValue.Text = _part.PartName;
                lblCharacteristicsValue.Text = _part.Characteristics ?? "Нет данных";
                lblPhotoPathValue.Text = _part.PhotoPath ?? "Нет фотографии";
                UpdatePhotoPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке деталей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadPartDetails Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                Close();
            }
        }

        private void UpdatePhotoPreview()
        {
            try
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = null;
                if (!string.IsNullOrEmpty(_part.PhotoPath) && File.Exists(_part.PhotoPath))
                {
                    using (var stream = new FileStream(_part.PhotoPath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox.Image = Image.FromStream(stream);
                    }
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"UpdatePhotoPreview Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pictureBox.Image?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
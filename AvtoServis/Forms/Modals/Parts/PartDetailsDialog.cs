using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;

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
            LoadPart();
            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Форма для просмотра деталей");
            toolTip.SetToolTip(lblBrand, "Марка автомобиля");
            toolTip.SetToolTip(txtBrand, "Название марки автомобиля");
            toolTip.SetToolTip(lblCatalogNumber, "Каталожный номер детали");
            toolTip.SetToolTip(txtCatalogNumber, "Каталожный номер детали");
            toolTip.SetToolTip(lblManufacturer, "Производитель детали");
            toolTip.SetToolTip(txtManufacturer, "Название производителя");
            toolTip.SetToolTip(lblQuality, "Качество детали");
            toolTip.SetToolTip(txtQuality, "Уровень качества детали");
            toolTip.SetToolTip(lblPartName, "Название детали");
            toolTip.SetToolTip(txtPartName, "Название детали");
            toolTip.SetToolTip(lblCharacteristics, "Характеристики детали");
            toolTip.SetToolTip(txtCharacteristics, "Характеристики детали");
            toolTip.SetToolTip(pictureBox, "Дважды щелкните для увеличения изображения");
            toolTip.SetToolTip(btnClose, "Закрыть окно");
        }

        private void LoadPart()
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

                txtBrand.Text = _part.CarBrandName;
                txtCatalogNumber.Text = _part.CatalogNumber;
                txtManufacturer.Text = _part.ManufacturerName;
                txtQuality.Text = _part.QualityName;
                txtPartName.Text = _part.PartName;
                txtCharacteristics.Text = _part.Characteristics;
                UpdatePhotoPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке детали: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadPart Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                Close();
            }
        }

        private void UpdatePhotoPreview()
        {
            try
            {
                pictureBox.Image = null;
                if (!string.IsNullOrEmpty(_part.PhotoPath) && File.Exists(_part.PhotoPath))
                {
                    using (var img = Image.FromFile(_part.PhotoPath))
                    {
                        pictureBox.Image = new Bitmap(img, pictureBox.Size);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"UpdatePhotoPreview Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void PictureBox_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(_part.PhotoPath) && File.Exists(_part.PhotoPath))
                {
                    using (var dialog = new FullImageDialog(_part.PhotoPath))
                    {
                        dialog.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"PictureBox_DoubleClick Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
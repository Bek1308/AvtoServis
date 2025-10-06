using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class PartFullDetailsDialog : Form
    {
        private readonly FullPartsViewModel _viewModel;
        private readonly int _partId;

        public PartFullDetailsDialog(FullPartsViewModel viewModel, int partId)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _partId = partId;
            InitializeComponent();
            LoadPartDetails();
            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Панель с подробной информацией о детали");
            toolTip.SetToolTip(titleLabel, "Заголовок окна подробной информации");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(btnClose, "Закрыть окно");
            toolTip.SetToolTip(lblPartID, "Идентификатор детали");
            toolTip.SetToolTip(lblPartName, "Название детали");
            toolTip.SetToolTip(lblCatalogNumber, "Каталожный номер детали");
            toolTip.SetToolTip(lblRemainingQuantity, "Остаток на складе");
            toolTip.SetToolTip(lblIsAvailable, "Наличие детали");
            toolTip.SetToolTip(lblStockName, "Название склада");
            toolTip.SetToolTip(lblIsPlacedInStock, "Статус размещения на складе");
            toolTip.SetToolTip(lblShelfNumber, "Номер полки");
            toolTip.SetToolTip(lblCarBrandName, "Марка автомобиля");
            toolTip.SetToolTip(lblManufacturerName, "Производитель детали");
            toolTip.SetToolTip(lblQualityName, "Качество детали");
            toolTip.SetToolTip(lblCharacteristics, "Характеристики детали");
        }

        private void LoadPartDetails()
        {
            try
            {
                var part = _viewModel.LoadParts().Find(p => p.PartID == _partId);
                if (part == null)
                {
                    MessageBox.Show("Деталь не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                lblPartIDValue.Text = part.PartID.ToString();
                lblPartNameValue.Text = part.PartName ?? "Не указано";
                lblCatalogNumberValue.Text = part.CatalogNumber ?? "Не указано";
                lblRemainingQuantityValue.Text = part.RemainingQuantity.ToString();
                lblIsAvailableValue.Text = part.IsAvailable ? "В наличии" : "Нет в наличии";
                lblIsAvailableValue.ForeColor = part.IsAvailable ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                lblStockNameValue.Text = part.StockName ?? "Не указано";
                lblIsPlacedInStockValue.Text = part.IsPlacedInStock ? "Размещено" : "Не размещено";
                lblIsPlacedInStockValue.ForeColor = part.IsPlacedInStock ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                lblShelfNumberValue.Text = part.ShelfNumber ?? "Не указано";
                lblCarBrandNameValue.Text = part.CarBrandName ?? "Не указано";
                lblManufacturerNameValue.Text = part.ManufacturerName ?? "Не указано";
                lblQualityNameValue.Text = part.QualityName ?? "Не указано";
                lblCharacteristicsValue.Text = part.Characteristics ?? "Не указано";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadPartDetails Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                Close();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
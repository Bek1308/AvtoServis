using System;
using System.Drawing;
using System.Windows.Forms;
using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;

namespace AvtoServis.Forms.Controls
{
    public partial class PartExpenseDetailsDialog : Form
    {
        private readonly PartExpensesViewModel _viewModel;
        private readonly int _saleId;

        public PartExpenseDetailsDialog(PartExpensesViewModel viewModel, int saleId)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _saleId = saleId;
            InitializeComponent();
            LoadPartExpenseDetails();
            SetToolTips();
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Панель с подробной информацией о расходе");
            toolTip.SetToolTip(titleLabel, "Заголовок окна подробной информации");
            toolTip.SetToolTip(separator, "Разделительная линия");
            toolTip.SetToolTip(btnClose, "Закрыть окно");
            toolTip.SetToolTip(lblSaleId, "Идентификатор продажи");
            toolTip.SetToolTip(lblPartName, "Название детали");
            toolTip.SetToolTip(lblQuantity, "Количество");
            toolTip.SetToolTip(lblUnitPrice, "Цена за единицу");
            toolTip.SetToolTip(lblTotalAmount, "Общая сумма");
            toolTip.SetToolTip(lblPaymentStatusId, "Толлов холати");
            toolTip.SetToolTip(lblSaleDate, "Дата продажи");
            toolTip.SetToolTip(lblManufacturer, "Производитель");
            toolTip.SetToolTip(lblCustomerName, "Имя клиента");
            toolTip.SetToolTip(lblCustomerPhone, "Телефон клиента");
            toolTip.SetToolTip(lblCatalogNumber, "Каталожный номер");
            toolTip.SetToolTip(lblCarBrand, "Марка автомобиля");
            toolTip.SetToolTip(lblStatus, "Статус");
            toolTip.SetToolTip(lblSeller, "Продавец");
            toolTip.SetToolTip(lblInvoiceNumber, "Номер счета");
        }

        private void LoadPartExpenseDetails()
        {
            try
            {
                var expense = _viewModel.LoadPartExpenses().Find(p => p.SaleId == _saleId);
                if (expense == null)
                {
                    MessageBox.Show("Расход не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                lblSaleIdValue.Text = expense.SaleId.ToString();
                lblPartNameValue.Text = expense.PartName ?? "Не указано";
                lblQuantityValue.Text = expense.Quantity.ToString();
                lblUnitPriceValue.Text = expense.UnitPrice.ToString("C");
                lblTotalAmountValue.Text = expense.TotalAmount.ToString("C");
                lblPaymentStatusIdValue.Text = expense.PaymentStatusId switch
                {
                    1 => "Оплачен",
                    2 => "Не оплачен",
                    3 => "Частично Оплачен",
                    _ => "Неизвестно"
                };
                lblPaymentStatusIdValue.ForeColor = expense.PaymentStatusId switch
                {
                    1 => Color.FromArgb(40, 167, 69), // Yashil
                    2 => Color.FromArgb(220, 53, 69), // Qizil
                    3 => Color.FromArgb(255, 193, 7), // Sariq
                    _ => Color.Black
                };
                lblSaleDateValue.Text = expense.SaleDate.ToString("yyyy-MM-dd");
                lblManufacturerValue.Text = expense.Manufacturer ?? "Не указано";
                lblCustomerNameValue.Text = expense.CustomerName ?? "Не указано";
                lblCustomerPhoneValue.Text = expense.CustomerPhone ?? "Не указано";
                lblCatalogNumberValue.Text = expense.CatalogNumber ?? "Не указано";
                lblCarBrandValue.Text = expense.CarBrand ?? "Не указано";
                lblStatusValue.Text = expense.Status ?? "Не указано";
                if (!string.IsNullOrEmpty(expense.StatusColor))
                {
                    try
                    {
                        lblStatusValue.ForeColor = ColorTranslator.FromHtml(expense.StatusColor);
                    }
                    catch
                    {
                        lblStatusValue.ForeColor = Color.Black;
                    }
                }
                lblSellerValue.Text = expense.Seller ?? "Не указано";
                lblInvoiceNumberValue.Text = expense.InvoiceNumber ?? "Не указано";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadPartExpenseDetails Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                Close();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
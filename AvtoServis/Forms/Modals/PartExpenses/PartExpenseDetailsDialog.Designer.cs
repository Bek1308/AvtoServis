using DocumentFormat.OpenXml.Wordprocessing;
using Font = System.Drawing.Font;
using Color = System.Drawing.Color;

namespace AvtoServis.Forms.Controls
{
    partial class PartExpenseDetailsDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tableLayoutPanel = new TableLayoutPanel();
            titleLabel = new Label();
            separator = new Label();
            btnClose = new Button();
            lblSaleId = new Label();
            lblSaleIdValue = new Label();
            lblPartName = new Label();
            lblPartNameValue = new Label();
            lblQuantity = new Label();
            lblQuantityValue = new Label();
            lblUnitPrice = new Label();
            lblUnitPriceValue = new Label();
            lblTotalAmount = new Label();
            lblTotalAmountValue = new Label();
            lblPaymentStatusId = new Label();
            lblPaymentStatusIdValue = new Label();
            lblSaleDate = new Label();
            lblSaleDateValue = new Label();
            lblManufacturer = new Label();
            lblManufacturerValue = new Label();
            lblCustomerName = new Label();
            lblCustomerNameValue = new Label();
            lblCustomerPhone = new Label();
            lblCustomerPhoneValue = new Label();
            lblCatalogNumber = new Label();
            lblCatalogNumberValue = new Label();
            lblCarBrand = new Label();
            lblCarBrandValue = new Label();
            lblStatus = new Label();
            lblStatusValue = new Label();
            lblSeller = new Label();
            lblSellerValue = new Label();
            lblInvoiceNumber = new Label();
            lblInvoiceNumberValue = new Label();
            toolTip = new ToolTip(components);
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(separator, 0, 1);
            tableLayoutPanel.Controls.Add(btnClose, 1, 17);
            tableLayoutPanel.Controls.Add(lblSaleId, 0, 2);
            tableLayoutPanel.Controls.Add(lblSaleIdValue, 1, 2);
            tableLayoutPanel.Controls.Add(lblPartName, 0, 3);
            tableLayoutPanel.Controls.Add(lblPartNameValue, 1, 3);
            tableLayoutPanel.Controls.Add(lblQuantity, 0, 4);
            tableLayoutPanel.Controls.Add(lblQuantityValue, 1, 4);
            tableLayoutPanel.Controls.Add(lblUnitPrice, 0, 5);
            tableLayoutPanel.Controls.Add(lblUnitPriceValue, 1, 5);
            tableLayoutPanel.Controls.Add(lblTotalAmount, 0, 6);
            tableLayoutPanel.Controls.Add(lblTotalAmountValue, 1, 6);
            tableLayoutPanel.Controls.Add(lblPaymentStatusId, 0, 7);
            tableLayoutPanel.Controls.Add(lblPaymentStatusIdValue, 1, 7);
            tableLayoutPanel.Controls.Add(lblSaleDate, 0, 8);
            tableLayoutPanel.Controls.Add(lblSaleDateValue, 1, 8);
            tableLayoutPanel.Controls.Add(lblManufacturer, 0, 9);
            tableLayoutPanel.Controls.Add(lblManufacturerValue, 1, 9);
            tableLayoutPanel.Controls.Add(lblCustomerName, 0, 10);
            tableLayoutPanel.Controls.Add(lblCustomerNameValue, 1, 10);
            tableLayoutPanel.Controls.Add(lblCustomerPhone, 0, 11);
            tableLayoutPanel.Controls.Add(lblCustomerPhoneValue, 1, 11);
            tableLayoutPanel.Controls.Add(lblCatalogNumber, 0, 12);
            tableLayoutPanel.Controls.Add(lblCatalogNumberValue, 1, 12);
            tableLayoutPanel.Controls.Add(lblCarBrand, 0, 13);
            tableLayoutPanel.Controls.Add(lblCarBrandValue, 1, 13);
            tableLayoutPanel.Controls.Add(lblStatus, 0, 14);
            tableLayoutPanel.Controls.Add(lblStatusValue, 1, 14);
            tableLayoutPanel.Controls.Add(lblSeller, 0, 15);
            tableLayoutPanel.Controls.Add(lblSellerValue, 1, 15);
            tableLayoutPanel.Controls.Add(lblInvoiceNumber, 0, 16);
            tableLayoutPanel.Controls.Add(lblInvoiceNumberValue, 1, 16);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 17;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.Size = new Size(500, 650);
            tableLayoutPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(titleLabel, 2);
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 16);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(437, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Подробная информация о расходе";
            // 
            // separator
            // 
            separator.BackColor = Color.FromArgb(108, 117, 125);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            separator.Location = new Point(19, 51);
            separator.Name = "separator";
            separator.Size = new Size(462, 2);
            separator.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.AutoSize = true;
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(381, 598);
            btnClose.MinimumSize = new Size(100, 33);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 33);
            btnClose.TabIndex = 13;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;
            // 
            // lblSaleId
            // 
            lblSaleId.AutoSize = true;
            lblSaleId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSaleId.ForeColor = Color.FromArgb(33, 37, 41);
            lblSaleId.Location = new Point(19, 53);
            lblSaleId.Name = "lblSaleId";
            lblSaleId.Size = new Size(33, 23);
            lblSaleId.TabIndex = 2;
            lblSaleId.Text = "ID:";
            // 
            // lblSaleIdValue
            // 
            lblSaleIdValue.AutoSize = true;
            lblSaleIdValue.Font = new Font("Segoe UI", 10F);
            lblSaleIdValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblSaleIdValue.Location = new Point(206, 53);
            lblSaleIdValue.Name = "lblSaleIdValue";
            lblSaleIdValue.Size = new Size(0, 23);
            lblSaleIdValue.TabIndex = 3;
            // 
            // lblPartName
            // 
            lblPartName.AutoSize = true;
            lblPartName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPartName.ForeColor = Color.FromArgb(33, 37, 41);
            lblPartName.Location = new Point(19, 83);
            lblPartName.Name = "lblPartName";
            lblPartName.Size = new Size(157, 23);
            lblPartName.TabIndex = 4;
            lblPartName.Text = "Название детали:";
            // 
            // lblPartNameValue
            // 
            lblPartNameValue.AutoSize = true;
            lblPartNameValue.Font = new Font("Segoe UI", 10F);
            lblPartNameValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblPartNameValue.Location = new Point(206, 83);
            lblPartNameValue.Name = "lblPartNameValue";
            lblPartNameValue.Size = new Size(0, 23);
            lblPartNameValue.TabIndex = 5;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQuantity.ForeColor = Color.FromArgb(33, 37, 41);
            lblQuantity.Location = new Point(19, 113);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(112, 23);
            lblQuantity.TabIndex = 6;
            lblQuantity.Text = "Количество:";
            // 
            // lblQuantityValue
            // 
            lblQuantityValue.AutoSize = true;
            lblQuantityValue.Font = new Font("Segoe UI", 10F);
            lblQuantityValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblQuantityValue.Location = new Point(206, 113);
            lblQuantityValue.Name = "lblQuantityValue";
            lblQuantityValue.Size = new Size(0, 23);
            lblQuantityValue.TabIndex = 7;
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUnitPrice.ForeColor = Color.FromArgb(33, 37, 41);
            lblUnitPrice.Location = new Point(19, 143);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(156, 23);
            lblUnitPrice.TabIndex = 8;
            lblUnitPrice.Text = "Цена за единицу:";
            // 
            // lblUnitPriceValue
            // 
            lblUnitPriceValue.AutoSize = true;
            lblUnitPriceValue.Font = new Font("Segoe UI", 10F);
            lblUnitPriceValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblUnitPriceValue.Location = new Point(206, 143);
            lblUnitPriceValue.Name = "lblUnitPriceValue";
            lblUnitPriceValue.Size = new Size(0, 23);
            lblUnitPriceValue.TabIndex = 9;
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(33, 37, 41);
            lblTotalAmount.Location = new Point(19, 173);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(129, 23);
            lblTotalAmount.TabIndex = 10;
            lblTotalAmount.Text = "Общая сумма:";
            // 
            // lblTotalAmountValue
            // 
            lblTotalAmountValue.AutoSize = true;
            lblTotalAmountValue.Font = new Font("Segoe UI", 10F);
            lblTotalAmountValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblTotalAmountValue.Location = new Point(206, 173);
            lblTotalAmountValue.Name = "lblTotalAmountValue";
            lblTotalAmountValue.Size = new Size(0, 23);
            lblTotalAmountValue.TabIndex = 11;
            // 
            // lblPaymentStatusId
            // 
            lblPaymentStatusId.AutoSize = true;
            lblPaymentStatusId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPaymentStatusId.ForeColor = Color.FromArgb(33, 37, 41);
            lblPaymentStatusId.Location = new Point(19, 203);
            lblPaymentStatusId.Name = "lblPaymentStatusId";
            lblPaymentStatusId.Size = new Size(136, 23);
            lblPaymentStatusId.TabIndex = 12;
            lblPaymentStatusId.Text = "Толлов холати:";
            // 
            // lblPaymentStatusIdValue
            // 
            lblPaymentStatusIdValue.AutoSize = true;
            lblPaymentStatusIdValue.Font = new Font("Segoe UI", 10F);
            lblPaymentStatusIdValue.Location = new Point(206, 203);
            lblPaymentStatusIdValue.Name = "lblPaymentStatusIdValue";
            lblPaymentStatusIdValue.Size = new Size(0, 23);
            lblPaymentStatusIdValue.TabIndex = 13;
            // 
            // lblSaleDate
            // 
            lblSaleDate.AutoSize = true;
            lblSaleDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSaleDate.ForeColor = Color.FromArgb(33, 37, 41);
            lblSaleDate.Location = new Point(19, 233);
            lblSaleDate.Name = "lblSaleDate";
            lblSaleDate.Size = new Size(135, 23);
            lblSaleDate.TabIndex = 14;
            lblSaleDate.Text = "Дата продажи:";
            // 
            // lblSaleDateValue
            // 
            lblSaleDateValue.AutoSize = true;
            lblSaleDateValue.Font = new Font("Segoe UI", 10F);
            lblSaleDateValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblSaleDateValue.Location = new Point(206, 233);
            lblSaleDateValue.Name = "lblSaleDateValue";
            lblSaleDateValue.Size = new Size(0, 23);
            lblSaleDateValue.TabIndex = 15;
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblManufacturer.ForeColor = Color.FromArgb(33, 37, 41);
            lblManufacturer.Location = new Point(19, 263);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(147, 23);
            lblManufacturer.TabIndex = 16;
            lblManufacturer.Text = "Производитель:";
            // 
            // lblManufacturerValue
            // 
            lblManufacturerValue.AutoSize = true;
            lblManufacturerValue.Font = new Font("Segoe UI", 10F);
            lblManufacturerValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblManufacturerValue.Location = new Point(206, 263);
            lblManufacturerValue.Name = "lblManufacturerValue";
            lblManufacturerValue.Size = new Size(0, 23);
            lblManufacturerValue.TabIndex = 17;
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCustomerName.ForeColor = Color.FromArgb(33, 37, 41);
            lblCustomerName.Location = new Point(19, 293);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(123, 23);
            lblCustomerName.TabIndex = 18;
            lblCustomerName.Text = "Имя клиента:";
            // 
            // lblCustomerNameValue
            // 
            lblCustomerNameValue.AutoSize = true;
            lblCustomerNameValue.Font = new Font("Segoe UI", 10F);
            lblCustomerNameValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblCustomerNameValue.Location = new Point(206, 293);
            lblCustomerNameValue.Name = "lblCustomerNameValue";
            lblCustomerNameValue.Size = new Size(0, 23);
            lblCustomerNameValue.TabIndex = 19;
            // 
            // lblCustomerPhone
            // 
            lblCustomerPhone.AutoSize = true;
            lblCustomerPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCustomerPhone.ForeColor = Color.FromArgb(33, 37, 41);
            lblCustomerPhone.Location = new Point(19, 323);
            lblCustomerPhone.Name = "lblCustomerPhone";
            lblCustomerPhone.Size = new Size(157, 23);
            lblCustomerPhone.TabIndex = 20;
            lblCustomerPhone.Text = "Телефон клиента:";
            // 
            // lblCustomerPhoneValue
            // 
            lblCustomerPhoneValue.AutoSize = true;
            lblCustomerPhoneValue.Font = new Font("Segoe UI", 10F);
            lblCustomerPhoneValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblCustomerPhoneValue.Location = new Point(206, 323);
            lblCustomerPhoneValue.Name = "lblCustomerPhoneValue";
            lblCustomerPhoneValue.Size = new Size(0, 23);
            lblCustomerPhoneValue.TabIndex = 21;
            // 
            // lblCatalogNumber
            // 
            lblCatalogNumber.AutoSize = true;
            lblCatalogNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCatalogNumber.ForeColor = Color.FromArgb(33, 37, 41);
            lblCatalogNumber.Location = new Point(19, 353);
            lblCatalogNumber.Name = "lblCatalogNumber";
            lblCatalogNumber.Size = new Size(179, 23);
            lblCatalogNumber.TabIndex = 22;
            lblCatalogNumber.Text = "Каталожный номер:";
            // 
            // lblCatalogNumberValue
            // 
            lblCatalogNumberValue.AutoSize = true;
            lblCatalogNumberValue.Font = new Font("Segoe UI", 10F);
            lblCatalogNumberValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblCatalogNumberValue.Location = new Point(206, 353);
            lblCatalogNumberValue.Name = "lblCatalogNumberValue";
            lblCatalogNumberValue.Size = new Size(0, 23);
            lblCatalogNumberValue.TabIndex = 23;
            // 
            // lblCarBrand
            // 
            lblCarBrand.AutoSize = true;
            lblCarBrand.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCarBrand.ForeColor = Color.FromArgb(33, 37, 41);
            lblCarBrand.Location = new Point(19, 383);
            lblCarBrand.Name = "lblCarBrand";
            lblCarBrand.Size = new Size(176, 23);
            lblCarBrand.TabIndex = 24;
            lblCarBrand.Text = "Марка автомобиля:";
            // 
            // lblCarBrandValue
            // 
            lblCarBrandValue.AutoSize = true;
            lblCarBrandValue.Font = new Font("Segoe UI", 10F);
            lblCarBrandValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblCarBrandValue.Location = new Point(206, 383);
            lblCarBrandValue.Name = "lblCarBrandValue";
            lblCarBrandValue.Size = new Size(0, 23);
            lblCarBrandValue.TabIndex = 25;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(33, 37, 41);
            lblStatus.Location = new Point(19, 413);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(68, 23);
            lblStatus.TabIndex = 26;
            lblStatus.Text = "Статус:";
            // 
            // lblStatusValue
            // 
            lblStatusValue.AutoSize = true;
            lblStatusValue.Font = new Font("Segoe UI", 10F);
            lblStatusValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblStatusValue.Location = new Point(206, 413);
            lblStatusValue.Name = "lblStatusValue";
            lblStatusValue.Size = new Size(0, 23);
            lblStatusValue.TabIndex = 27;
            // 
            // lblSeller
            // 
            lblSeller.AutoSize = true;
            lblSeller.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSeller.ForeColor = Color.FromArgb(33, 37, 41);
            lblSeller.Location = new Point(19, 443);
            lblSeller.Name = "lblSeller";
            lblSeller.Size = new Size(99, 23);
            lblSeller.TabIndex = 28;
            lblSeller.Text = "Продавец:";
            // 
            // lblSellerValue
            // 
            lblSellerValue.AutoSize = true;
            lblSellerValue.Font = new Font("Segoe UI", 10F);
            lblSellerValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblSellerValue.Location = new Point(206, 443);
            lblSellerValue.Name = "lblSellerValue";
            lblSellerValue.Size = new Size(0, 23);
            lblSellerValue.TabIndex = 29;
            // 
            // lblInvoiceNumber
            // 
            lblInvoiceNumber.AutoSize = true;
            lblInvoiceNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblInvoiceNumber.ForeColor = Color.FromArgb(33, 37, 41);
            lblInvoiceNumber.Location = new Point(19, 473);
            lblInvoiceNumber.Name = "lblInvoiceNumber";
            lblInvoiceNumber.Size = new Size(120, 23);
            lblInvoiceNumber.TabIndex = 30;
            lblInvoiceNumber.Text = "Номер счета:";
            // 
            // lblInvoiceNumberValue
            // 
            lblInvoiceNumberValue.AutoSize = true;
            lblInvoiceNumberValue.Font = new Font("Segoe UI", 10F);
            lblInvoiceNumberValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblInvoiceNumberValue.Location = new Point(206, 473);
            lblInvoiceNumberValue.Name = "lblInvoiceNumberValue";
            lblInvoiceNumberValue.Size = new Size(0, 23);
            lblInvoiceNumberValue.TabIndex = 31;
            // 
            // PartExpenseDetailsDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(500, 650);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PartExpenseDetailsDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Подробная информация о расходе";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label titleLabel;
        private Label separator;
        private Button btnClose;
        private Label lblSaleId;
        private Label lblSaleIdValue;
        private Label lblPartName;
        private Label lblPartNameValue;
        private Label lblQuantity;
        private Label lblQuantityValue;
        private Label lblUnitPrice;
        private Label lblUnitPriceValue;
        private Label lblTotalAmount;
        private Label lblTotalAmountValue;
        private Label lblPaymentStatusId;
        private Label lblPaymentStatusIdValue;
        private Label lblSaleDate;
        private Label lblSaleDateValue;
        private Label lblManufacturer;
        private Label lblManufacturerValue;
        private Label lblCustomerName;
        private Label lblCustomerNameValue;
        private Label lblCustomerPhone;
        private Label lblCustomerPhoneValue;
        private Label lblCatalogNumber;
        private Label lblCatalogNumberValue;
        private Label lblCarBrand;
        private Label lblCarBrandValue;
        private Label lblStatus;
        private Label lblStatusValue;
        private Label lblSeller;
        private Label lblSellerValue;
        private Label lblInvoiceNumber;
        private Label lblInvoiceNumberValue;
        private ToolTip toolTip;
    }
}
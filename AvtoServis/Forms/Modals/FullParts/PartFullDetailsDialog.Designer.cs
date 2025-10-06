namespace AvtoServis.Forms.Controls
{
    partial class PartFullDetailsDialog
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
            lblPartID = new Label();
            lblPartIDValue = new Label();
            lblPartName = new Label();
            lblPartNameValue = new Label();
            lblCatalogNumber = new Label();
            lblCatalogNumberValue = new Label();
            lblRemainingQuantity = new Label();
            lblRemainingQuantityValue = new Label();
            lblIsAvailable = new Label();
            lblIsAvailableValue = new Label();
            lblStockName = new Label();
            lblStockNameValue = new Label();
            lblIsPlacedInStock = new Label();
            lblIsPlacedInStockValue = new Label();
            lblShelfNumber = new Label();
            lblShelfNumberValue = new Label();
            lblCarBrandName = new Label();
            lblCarBrandNameValue = new Label();
            lblManufacturerName = new Label();
            lblManufacturerNameValue = new Label();
            lblQualityName = new Label();
            lblQualityNameValue = new Label();
            lblCharacteristics = new Label();
            lblCharacteristicsValue = new Label();
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
            tableLayoutPanel.Controls.Add(btnClose, 1, 14);
            tableLayoutPanel.Controls.Add(lblPartID, 0, 2);
            tableLayoutPanel.Controls.Add(lblPartIDValue, 1, 2);
            tableLayoutPanel.Controls.Add(lblPartName, 0, 3);
            tableLayoutPanel.Controls.Add(lblPartNameValue, 1, 3);
            tableLayoutPanel.Controls.Add(lblCatalogNumber, 0, 4);
            tableLayoutPanel.Controls.Add(lblCatalogNumberValue, 1, 4);
            tableLayoutPanel.Controls.Add(lblRemainingQuantity, 0, 5);
            tableLayoutPanel.Controls.Add(lblRemainingQuantityValue, 1, 5);
            tableLayoutPanel.Controls.Add(lblIsAvailable, 0, 6);
            tableLayoutPanel.Controls.Add(lblIsAvailableValue, 1, 6);
            tableLayoutPanel.Controls.Add(lblStockName, 0, 7);
            tableLayoutPanel.Controls.Add(lblStockNameValue, 1, 7);
            tableLayoutPanel.Controls.Add(lblIsPlacedInStock, 0, 8);
            tableLayoutPanel.Controls.Add(lblIsPlacedInStockValue, 1, 8);
            tableLayoutPanel.Controls.Add(lblShelfNumber, 0, 9);
            tableLayoutPanel.Controls.Add(lblShelfNumberValue, 1, 9);
            tableLayoutPanel.Controls.Add(lblCarBrandName, 0, 10);
            tableLayoutPanel.Controls.Add(lblCarBrandNameValue, 1, 10);
            tableLayoutPanel.Controls.Add(lblManufacturerName, 0, 11);
            tableLayoutPanel.Controls.Add(lblManufacturerNameValue, 1, 11);
            tableLayoutPanel.Controls.Add(lblQualityName, 0, 12);
            tableLayoutPanel.Controls.Add(lblQualityNameValue, 1, 12);
            tableLayoutPanel.Controls.Add(lblCharacteristics, 0, 13);
            tableLayoutPanel.Controls.Add(lblCharacteristicsValue, 1, 13);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 14;
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
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.Size = new Size(500, 600);
            tableLayoutPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AccessibleDescription = "Заголовок окна подробной информации";
            titleLabel.AccessibleName = "Подробная информация";
            titleLabel.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(titleLabel, 2);
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 16);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(312, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Подробная информация";
            // 
            // separator
            // 
            separator.AccessibleDescription = "Разделительная линия";
            separator.AccessibleName = "Разделитель";
            separator.BackColor = Color.FromArgb(108, 117, 125);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            separator.Location = new Point(19, 51);
            separator.Name = "separator";
            separator.Size = new Size(462, 2);
            separator.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.AccessibleDescription = "Закрывает окно";
            btnClose.AccessibleName = "Закрыть";
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.AutoSize = true;
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(381, 548);
            btnClose.MinimumSize = new Size(100, 33);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 33);
            btnClose.TabIndex = 13;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;
            // 
            // lblPartID
            // 
            lblPartID.AutoSize = true;
            lblPartID.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPartID.ForeColor = Color.FromArgb(33, 37, 41);
            lblPartID.Location = new Point(19, 53);
            lblPartID.Name = "lblPartID";
            lblPartID.Size = new Size(33, 23);
            lblPartID.TabIndex = 2;
            lblPartID.Text = "ID:";
            // 
            // lblPartIDValue
            // 
            lblPartIDValue.AutoSize = true;
            lblPartIDValue.Font = new Font("Segoe UI", 10F);
            lblPartIDValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblPartIDValue.Location = new Point(206, 53);
            lblPartIDValue.Name = "lblPartIDValue";
            lblPartIDValue.Size = new Size(0, 23);
            lblPartIDValue.TabIndex = 3;
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
            // lblCatalogNumber
            // 
            lblCatalogNumber.AutoSize = true;
            lblCatalogNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCatalogNumber.ForeColor = Color.FromArgb(33, 37, 41);
            lblCatalogNumber.Location = new Point(19, 113);
            lblCatalogNumber.Name = "lblCatalogNumber";
            lblCatalogNumber.Size = new Size(179, 23);
            lblCatalogNumber.TabIndex = 6;
            lblCatalogNumber.Text = "Каталожный номер:";
            // 
            // lblCatalogNumberValue
            // 
            lblCatalogNumberValue.AutoSize = true;
            lblCatalogNumberValue.Font = new Font("Segoe UI", 10F);
            lblCatalogNumberValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblCatalogNumberValue.Location = new Point(206, 113);
            lblCatalogNumberValue.Name = "lblCatalogNumberValue";
            lblCatalogNumberValue.Size = new Size(0, 23);
            lblCatalogNumberValue.TabIndex = 7;
            // 
            // lblRemainingQuantity
            // 
            lblRemainingQuantity.AutoSize = true;
            lblRemainingQuantity.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRemainingQuantity.ForeColor = Color.FromArgb(33, 37, 41);
            lblRemainingQuantity.Location = new Point(19, 143);
            lblRemainingQuantity.Name = "lblRemainingQuantity";
            lblRemainingQuantity.Size = new Size(82, 23);
            lblRemainingQuantity.TabIndex = 8;
            lblRemainingQuantity.Text = "Остаток:";
            // 
            // lblRemainingQuantityValue
            // 
            lblRemainingQuantityValue.AutoSize = true;
            lblRemainingQuantityValue.Font = new Font("Segoe UI", 10F);
            lblRemainingQuantityValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblRemainingQuantityValue.Location = new Point(206, 143);
            lblRemainingQuantityValue.Name = "lblRemainingQuantityValue";
            lblRemainingQuantityValue.Size = new Size(0, 23);
            lblRemainingQuantityValue.TabIndex = 9;
            // 
            // lblIsAvailable
            // 
            lblIsAvailable.AutoSize = true;
            lblIsAvailable.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblIsAvailable.ForeColor = Color.FromArgb(33, 37, 41);
            lblIsAvailable.Location = new Point(19, 173);
            lblIsAvailable.Name = "lblIsAvailable";
            lblIsAvailable.Size = new Size(103, 23);
            lblIsAvailable.TabIndex = 10;
            lblIsAvailable.Text = "В наличии:";
            // 
            // lblIsAvailableValue
            // 
            lblIsAvailableValue.AutoSize = true;
            lblIsAvailableValue.Font = new Font("Segoe UI", 10F);
            lblIsAvailableValue.Location = new Point(206, 173);
            lblIsAvailableValue.Name = "lblIsAvailableValue";
            lblIsAvailableValue.Size = new Size(0, 23);
            lblIsAvailableValue.TabIndex = 11;
            // 
            // lblStockName
            // 
            lblStockName.AutoSize = true;
            lblStockName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStockName.ForeColor = Color.FromArgb(33, 37, 41);
            lblStockName.Location = new Point(19, 203);
            lblStockName.Name = "lblStockName";
            lblStockName.Size = new Size(66, 23);
            lblStockName.TabIndex = 12;
            lblStockName.Text = "Склад:";
            // 
            // lblStockNameValue
            // 
            lblStockNameValue.AutoSize = true;
            lblStockNameValue.Font = new Font("Segoe UI", 10F);
            lblStockNameValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblStockNameValue.Location = new Point(206, 203);
            lblStockNameValue.Name = "lblStockNameValue";
            lblStockNameValue.Size = new Size(0, 23);
            lblStockNameValue.TabIndex = 13;
            // 
            // lblIsPlacedInStock
            // 
            lblIsPlacedInStock.AutoSize = true;
            lblIsPlacedInStock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblIsPlacedInStock.ForeColor = Color.FromArgb(33, 37, 41);
            lblIsPlacedInStock.Location = new Point(19, 233);
            lblIsPlacedInStock.Name = "lblIsPlacedInStock";
            lblIsPlacedInStock.Size = new Size(108, 23);
            lblIsPlacedInStock.TabIndex = 14;
            lblIsPlacedInStock.Text = "Размещено:";
            // 
            // lblIsPlacedInStockValue
            // 
            lblIsPlacedInStockValue.AutoSize = true;
            lblIsPlacedInStockValue.Font = new Font("Segoe UI", 10F);
            lblIsPlacedInStockValue.Location = new Point(206, 233);
            lblIsPlacedInStockValue.Name = "lblIsPlacedInStockValue";
            lblIsPlacedInStockValue.Size = new Size(0, 23);
            lblIsPlacedInStockValue.TabIndex = 15;
            // 
            // lblShelfNumber
            // 
            lblShelfNumber.AutoSize = true;
            lblShelfNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblShelfNumber.ForeColor = Color.FromArgb(33, 37, 41);
            lblShelfNumber.Location = new Point(19, 263);
            lblShelfNumber.Name = "lblShelfNumber";
            lblShelfNumber.Size = new Size(127, 23);
            lblShelfNumber.TabIndex = 16;
            lblShelfNumber.Text = "Номер полки:";
            // 
            // lblShelfNumberValue
            // 
            lblShelfNumberValue.AutoSize = true;
            lblShelfNumberValue.Font = new Font("Segoe UI", 10F);
            lblShelfNumberValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblShelfNumberValue.Location = new Point(206, 263);
            lblShelfNumberValue.Name = "lblShelfNumberValue";
            lblShelfNumberValue.Size = new Size(0, 23);
            lblShelfNumberValue.TabIndex = 17;
            // 
            // lblCarBrandName
            // 
            lblCarBrandName.AutoSize = true;
            lblCarBrandName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCarBrandName.ForeColor = Color.FromArgb(33, 37, 41);
            lblCarBrandName.Location = new Point(19, 293);
            lblCarBrandName.Name = "lblCarBrandName";
            lblCarBrandName.Size = new Size(70, 23);
            lblCarBrandName.TabIndex = 18;
            lblCarBrandName.Text = "Марка:";
            // 
            // lblCarBrandNameValue
            // 
            lblCarBrandNameValue.AutoSize = true;
            lblCarBrandNameValue.Font = new Font("Segoe UI", 10F);
            lblCarBrandNameValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblCarBrandNameValue.Location = new Point(206, 293);
            lblCarBrandNameValue.Name = "lblCarBrandNameValue";
            lblCarBrandNameValue.Size = new Size(0, 23);
            lblCarBrandNameValue.TabIndex = 19;
            // 
            // lblManufacturerName
            // 
            lblManufacturerName.AutoSize = true;
            lblManufacturerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblManufacturerName.ForeColor = Color.FromArgb(33, 37, 41);
            lblManufacturerName.Location = new Point(19, 323);
            lblManufacturerName.Name = "lblManufacturerName";
            lblManufacturerName.Size = new Size(147, 23);
            lblManufacturerName.TabIndex = 20;
            lblManufacturerName.Text = "Производитель:";
            // 
            // lblManufacturerNameValue
            // 
            lblManufacturerNameValue.AutoSize = true;
            lblManufacturerNameValue.Font = new Font("Segoe UI", 10F);
            lblManufacturerNameValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblManufacturerNameValue.Location = new Point(206, 323);
            lblManufacturerNameValue.Name = "lblManufacturerNameValue";
            lblManufacturerNameValue.Size = new Size(0, 23);
            lblManufacturerNameValue.TabIndex = 21;
            // 
            // lblQualityName
            // 
            lblQualityName.AutoSize = true;
            lblQualityName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQualityName.ForeColor = Color.FromArgb(33, 37, 41);
            lblQualityName.Location = new Point(19, 353);
            lblQualityName.Name = "lblQualityName";
            lblQualityName.Size = new Size(90, 23);
            lblQualityName.TabIndex = 22;
            lblQualityName.Text = "Качество:";
            // 
            // lblQualityNameValue
            // 
            lblQualityNameValue.AutoSize = true;
            lblQualityNameValue.Font = new Font("Segoe UI", 10F);
            lblQualityNameValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblQualityNameValue.Location = new Point(206, 353);
            lblQualityNameValue.Name = "lblQualityNameValue";
            lblQualityNameValue.Size = new Size(0, 23);
            lblQualityNameValue.TabIndex = 23;
            // 
            // lblCharacteristics
            // 
            lblCharacteristics.AutoSize = true;
            lblCharacteristics.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCharacteristics.ForeColor = Color.FromArgb(33, 37, 41);
            lblCharacteristics.Location = new Point(19, 383);
            lblCharacteristics.Name = "lblCharacteristics";
            lblCharacteristics.Size = new Size(152, 23);
            lblCharacteristics.TabIndex = 24;
            lblCharacteristics.Text = "Характеристики:";
            // 
            // lblCharacteristicsValue
            // 
            lblCharacteristicsValue.AutoSize = true;
            lblCharacteristicsValue.Font = new Font("Segoe UI", 10F);
            lblCharacteristicsValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblCharacteristicsValue.Location = new Point(206, 383);
            lblCharacteristicsValue.Name = "lblCharacteristicsValue";
            lblCharacteristicsValue.Size = new Size(0, 23);
            lblCharacteristicsValue.TabIndex = 25;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            // 
            // PartFullDetailsDialog
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(500, 600);
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PartFullDetailsDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Подробная информация о детали";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label titleLabel;
        private Label separator;
        private Button btnClose;
        private Label lblPartID;
        private Label lblPartIDValue;
        private Label lblPartName;
        private Label lblPartNameValue;
        private Label lblCatalogNumber;
        private Label lblCatalogNumberValue;
        private Label lblRemainingQuantity;
        private Label lblRemainingQuantityValue;
        private Label lblIsAvailable;
        private Label lblIsAvailableValue;
        private Label lblStockName;
        private Label lblStockNameValue;
        private Label lblIsPlacedInStock;
        private Label lblIsPlacedInStockValue;
        private Label lblShelfNumber;
        private Label lblShelfNumberValue;
        private Label lblCarBrandName;
        private Label lblCarBrandNameValue;
        private Label lblManufacturerName;
        private Label lblManufacturerNameValue;
        private Label lblQualityName;
        private Label lblQualityNameValue;
        private Label lblCharacteristics;
        private Label lblCharacteristicsValue;
        private ToolTip toolTip;
    }
}
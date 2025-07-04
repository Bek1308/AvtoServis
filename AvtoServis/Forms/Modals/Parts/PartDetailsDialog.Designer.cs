namespace AvtoServis.Forms.Controls
{
    partial class PartDetailsDialog
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
            toolTip = new ToolTip(components);
            tableLayoutPanel = new TableLayoutPanel();
            lblBrand = new Label();
            txtBrand = new TextBox();
            lblCatalogNumber = new Label();
            txtCatalogNumber = new TextBox();
            lblManufacturer = new Label();
            txtManufacturer = new TextBox();
            lblQuality = new Label();
            txtQuality = new TextBox();
            lblPartName = new Label();
            txtPartName = new TextBox();
            lblCharacteristics = new Label();
            txtCharacteristics = new TextBox();
            pictureBox = new PictureBox();
            btnClose = new Button();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = Color.FromArgb(245, 245, 245);
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblBrand, 0, 0);
            tableLayoutPanel.Controls.Add(txtBrand, 1, 0);
            tableLayoutPanel.Controls.Add(lblCatalogNumber, 0, 1);
            tableLayoutPanel.Controls.Add(txtCatalogNumber, 1, 1);
            tableLayoutPanel.Controls.Add(lblManufacturer, 0, 2);
            tableLayoutPanel.Controls.Add(txtManufacturer, 1, 2);
            tableLayoutPanel.Controls.Add(lblQuality, 0, 3);
            tableLayoutPanel.Controls.Add(txtQuality, 1, 3);
            tableLayoutPanel.Controls.Add(lblPartName, 0, 4);
            tableLayoutPanel.Controls.Add(txtPartName, 1, 4);
            tableLayoutPanel.Controls.Add(lblCharacteristics, 0, 5);
            tableLayoutPanel.Controls.Add(txtCharacteristics, 1, 5);
            tableLayoutPanel.Controls.Add(pictureBox, 1, 6);
            tableLayoutPanel.Controls.Add(btnClose, 1, 7);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 8;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 241F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel.Size = new Size(507, 573);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 10F);
            lblBrand.ForeColor = Color.FromArgb(33, 37, 41);
            lblBrand.Location = new Point(19, 16);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(61, 23);
            lblBrand.TabIndex = 0;
            lblBrand.Text = "Марка";
            // 
            // txtBrand
            // 
            txtBrand.BorderStyle = BorderStyle.FixedSingle;
            txtBrand.Font = new Font("Segoe UI", 10F);
            txtBrand.Location = new Point(139, 19);
            txtBrand.Name = "txtBrand";
            txtBrand.ReadOnly = true;
            txtBrand.Size = new Size(349, 30);
            txtBrand.TabIndex = 1;
            // 
            // lblCatalogNumber
            // 
            lblCatalogNumber.AutoSize = true;
            lblCatalogNumber.Font = new Font("Segoe UI", 10F);
            lblCatalogNumber.ForeColor = Color.FromArgb(33, 37, 41);
            lblCatalogNumber.Location = new Point(19, 56);
            lblCatalogNumber.Name = "lblCatalogNumber";
            lblCatalogNumber.Size = new Size(114, 40);
            lblCatalogNumber.TabIndex = 2;
            lblCatalogNumber.Text = "Каталожный номер";
            // 
            // txtCatalogNumber
            // 
            txtCatalogNumber.BorderStyle = BorderStyle.FixedSingle;
            txtCatalogNumber.Font = new Font("Segoe UI", 10F);
            txtCatalogNumber.Location = new Point(139, 59);
            txtCatalogNumber.Name = "txtCatalogNumber";
            txtCatalogNumber.ReadOnly = true;
            txtCatalogNumber.Size = new Size(349, 30);
            txtCatalogNumber.TabIndex = 3;
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.Font = new Font("Segoe UI", 10F);
            lblManufacturer.ForeColor = Color.FromArgb(33, 37, 41);
            lblManufacturer.Location = new Point(19, 96);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(114, 40);
            lblManufacturer.TabIndex = 4;
            lblManufacturer.Text = "Производитель";
            // 
            // txtManufacturer
            // 
            txtManufacturer.BorderStyle = BorderStyle.FixedSingle;
            txtManufacturer.Font = new Font("Segoe UI", 10F);
            txtManufacturer.Location = new Point(139, 99);
            txtManufacturer.Name = "txtManufacturer";
            txtManufacturer.ReadOnly = true;
            txtManufacturer.Size = new Size(349, 30);
            txtManufacturer.TabIndex = 5;
            // 
            // lblQuality
            // 
            lblQuality.AutoSize = true;
            lblQuality.Font = new Font("Segoe UI", 10F);
            lblQuality.ForeColor = Color.FromArgb(33, 37, 41);
            lblQuality.Location = new Point(19, 136);
            lblQuality.Name = "lblQuality";
            lblQuality.Size = new Size(82, 23);
            lblQuality.TabIndex = 6;
            lblQuality.Text = "Качество";
            // 
            // txtQuality
            // 
            txtQuality.BorderStyle = BorderStyle.FixedSingle;
            txtQuality.Font = new Font("Segoe UI", 10F);
            txtQuality.Location = new Point(139, 139);
            txtQuality.Name = "txtQuality";
            txtQuality.ReadOnly = true;
            txtQuality.Size = new Size(349, 30);
            txtQuality.TabIndex = 7;
            // 
            // lblPartName
            // 
            lblPartName.AutoSize = true;
            lblPartName.Font = new Font("Segoe UI", 10F);
            lblPartName.ForeColor = Color.FromArgb(33, 37, 41);
            lblPartName.Location = new Point(19, 176);
            lblPartName.Name = "lblPartName";
            lblPartName.Size = new Size(91, 40);
            lblPartName.TabIndex = 8;
            lblPartName.Text = "Название детали";
            // 
            // txtPartName
            // 
            txtPartName.BorderStyle = BorderStyle.FixedSingle;
            txtPartName.Font = new Font("Segoe UI", 10F);
            txtPartName.Location = new Point(139, 179);
            txtPartName.Name = "txtPartName";
            txtPartName.ReadOnly = true;
            txtPartName.Size = new Size(349, 30);
            txtPartName.TabIndex = 9;
            // 
            // lblCharacteristics
            // 
            lblCharacteristics.AutoSize = true;
            lblCharacteristics.Font = new Font("Segoe UI", 10F);
            lblCharacteristics.ForeColor = Color.FromArgb(33, 37, 41);
            lblCharacteristics.Location = new Point(19, 216);
            lblCharacteristics.Name = "lblCharacteristics";
            lblCharacteristics.Size = new Size(107, 40);
            lblCharacteristics.TabIndex = 10;
            lblCharacteristics.Text = "Характеристики";
            // 
            // txtCharacteristics
            // 
            txtCharacteristics.BorderStyle = BorderStyle.FixedSingle;
            txtCharacteristics.Font = new Font("Segoe UI", 10F);
            txtCharacteristics.Location = new Point(139, 219);
            txtCharacteristics.Multiline = true;
            txtCharacteristics.Name = "txtCharacteristics";
            txtCharacteristics.ReadOnly = true;
            txtCharacteristics.Size = new Size(349, 30);
            txtCharacteristics.TabIndex = 11;
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(139, 259);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(349, 235);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 12;
            pictureBox.TabStop = false;
            pictureBox.DoubleClick += PictureBox_DoubleClick;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(368, 512);
            btnClose.Margin = new Padding(3, 15, 3, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 34);
            btnClose.TabIndex = 13;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;
            // 
            // PartDetailsDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(507, 573);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PartDetailsDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Детали";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblBrand;
        private TextBox txtBrand;
        private Label lblCatalogNumber;
        private TextBox txtCatalogNumber;
        private Label lblManufacturer;
        private TextBox txtManufacturer;
        private Label lblQuality;
        private TextBox txtQuality;
        private Label lblPartName;
        private TextBox txtPartName;
        private Label lblCharacteristics;
        private TextBox txtCharacteristics;
        private PictureBox pictureBox;
        private Button btnClose;
        private ToolTip toolTip;
    }
}
namespace AvtoServis.Forms.Controls
{
    partial class PartsDialog
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
            cmbBrand = new ComboBox();
            lblCatalogNumber = new Label();
            txtCatalogNumber = new TextBox();
            lblManufacturer = new Label();
            cmbManufacturer = new ComboBox();
            lblQuality = new Label();
            cmbQuality = new ComboBox();
            lblPartName = new Label();
            txtPartName = new TextBox();
            lblCharacteristics = new Label();
            txtCharacteristics = new TextBox();
            lblPhotoPath = new Label();
            txtPhotoPath = new TextBox();
            btnBrowsePhoto = new Button();
            pictureBox = new PictureBox();
            lblError = new Label();
            btnCancel = new Button();
            btnSave = new Button();
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
            tableLayoutPanel.Controls.Add(cmbBrand, 1, 0);
            tableLayoutPanel.Controls.Add(lblCatalogNumber, 0, 1);
            tableLayoutPanel.Controls.Add(txtCatalogNumber, 1, 1);
            tableLayoutPanel.Controls.Add(lblManufacturer, 0, 2);
            tableLayoutPanel.Controls.Add(cmbManufacturer, 1, 2);
            tableLayoutPanel.Controls.Add(lblQuality, 0, 3);
            tableLayoutPanel.Controls.Add(cmbQuality, 1, 3);
            tableLayoutPanel.Controls.Add(lblPartName, 0, 4);
            tableLayoutPanel.Controls.Add(txtPartName, 1, 4);
            tableLayoutPanel.Controls.Add(lblCharacteristics, 0, 5);
            tableLayoutPanel.Controls.Add(txtCharacteristics, 1, 5);
            tableLayoutPanel.Controls.Add(lblPhotoPath, 0, 6);
            tableLayoutPanel.Controls.Add(txtPhotoPath, 1, 6);
            tableLayoutPanel.Controls.Add(btnBrowsePhoto, 1, 7);
            tableLayoutPanel.Controls.Add(pictureBox, 1, 8);
            tableLayoutPanel.Controls.Add(lblError, 0, 9);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 10);
            tableLayoutPanel.Controls.Add(btnSave, 1, 10);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 11;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 192F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.Size = new Size(434, 629);
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
            // cmbBrand
            // 
            cmbBrand.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBrand.Font = new Font("Segoe UI", 10F);
            cmbBrand.FormattingEnabled = true;
            cmbBrand.Location = new Point(139, 19);
            cmbBrand.Name = "cmbBrand";
            cmbBrand.Size = new Size(276, 31);
            cmbBrand.TabIndex = 1;
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
            txtCatalogNumber.Font = new Font("Segoe UI", 10F);
            txtCatalogNumber.Location = new Point(139, 59);
            txtCatalogNumber.Name = "txtCatalogNumber";
            txtCatalogNumber.Size = new Size(276, 30);
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
            // cmbManufacturer
            // 
            cmbManufacturer.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbManufacturer.Font = new Font("Segoe UI", 10F);
            cmbManufacturer.FormattingEnabled = true;
            cmbManufacturer.Location = new Point(139, 99);
            cmbManufacturer.Name = "cmbManufacturer";
            cmbManufacturer.Size = new Size(276, 31);
            cmbManufacturer.TabIndex = 5;
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
            // cmbQuality
            // 
            cmbQuality.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQuality.Font = new Font("Segoe UI", 10F);
            cmbQuality.FormattingEnabled = true;
            cmbQuality.Location = new Point(139, 139);
            cmbQuality.Name = "cmbQuality";
            cmbQuality.Size = new Size(276, 31);
            cmbQuality.TabIndex = 7;
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
            txtPartName.Font = new Font("Segoe UI", 10F);
            txtPartName.Location = new Point(139, 179);
            txtPartName.Name = "txtPartName";
            txtPartName.Size = new Size(276, 30);
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
            txtCharacteristics.Font = new Font("Segoe UI", 10F);
            txtCharacteristics.Location = new Point(139, 219);
            txtCharacteristics.Multiline = true;
            txtCharacteristics.Name = "txtCharacteristics";
            txtCharacteristics.Size = new Size(276, 30);
            txtCharacteristics.TabIndex = 11;
            // 
            // lblPhotoPath
            // 
            lblPhotoPath.AutoSize = true;
            lblPhotoPath.Font = new Font("Segoe UI", 10F);
            lblPhotoPath.ForeColor = Color.FromArgb(33, 37, 41);
            lblPhotoPath.Location = new Point(19, 256);
            lblPhotoPath.Name = "lblPhotoPath";
            lblPhotoPath.Size = new Size(106, 23);
            lblPhotoPath.TabIndex = 12;
            lblPhotoPath.Text = "Фотография";
            // 
            // txtPhotoPath
            // 
            txtPhotoPath.Font = new Font("Segoe UI", 10F);
            txtPhotoPath.Location = new Point(139, 259);
            txtPhotoPath.Name = "txtPhotoPath";
            txtPhotoPath.ReadOnly = true;
            txtPhotoPath.Size = new Size(276, 30);
            txtPhotoPath.TabIndex = 13;
            // 
            // btnBrowsePhoto
            // 
            btnBrowsePhoto.BackColor = Color.FromArgb(25, 118, 210);
            btnBrowsePhoto.FlatAppearance.BorderSize = 0;
            btnBrowsePhoto.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnBrowsePhoto.FlatStyle = FlatStyle.Flat;
            btnBrowsePhoto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBrowsePhoto.ForeColor = Color.White;
            btnBrowsePhoto.Location = new Point(139, 299);
            btnBrowsePhoto.Name = "btnBrowsePhoto";
            btnBrowsePhoto.Size = new Size(120, 34);
            btnBrowsePhoto.TabIndex = 14;
            btnBrowsePhoto.Text = "Обзор...";
            btnBrowsePhoto.UseVisualStyleBackColor = false;
            btnBrowsePhoto.Click += BtnBrowsePhoto_Click;
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(139, 339);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(276, 186);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 15;
            pictureBox.TabStop = false;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 10F);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(19, 528);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 23);
            lblError.TabIndex = 16;
            lblError.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(19, 579);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(114, 34);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(295, 579);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 34);
            btnSave.TabIndex = 18;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // PartsDialog
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(434, 629);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PartsDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Деталь";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblBrand;
        private ComboBox cmbBrand;
        private Label lblCatalogNumber;
        private TextBox txtCatalogNumber;
        private Label lblManufacturer;
        private ComboBox cmbManufacturer;
        private Label lblQuality;
        private ComboBox cmbQuality;
        private Label lblPartName;
        private TextBox txtPartName;
        private Label lblCharacteristics;
        private TextBox txtCharacteristics;
        private Label lblPhotoPath;
        private TextBox txtPhotoPath;
        private Button btnBrowsePhoto;
        private PictureBox pictureBox;
        private Label lblError;
        private Button btnCancel;
        private Button btnSave;
        private ToolTip toolTip;
    }
}
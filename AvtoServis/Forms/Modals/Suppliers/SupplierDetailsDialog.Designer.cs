namespace AvtoServis.Forms.Controls
{
    partial class SupplierDetailsDialog
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            lblTitle = new Label();
            lblSupplierID = new Label();
            lblSupplierIDValue = new Label();
            lblName = new Label();
            lblNameValue = new Label();
            lblContactPhone = new Label();
            lblContactPhoneValue = new Label();
            lblEmail = new Label();
            lblEmailValue = new Label();
            lblAddress = new Label();
            lblAddressValue = new Label();
            btnClose = new Button();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 131F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanel.Controls.Add(lblSupplierID, 0, 1);
            tableLayoutPanel.Controls.Add(lblSupplierIDValue, 1, 1);
            tableLayoutPanel.Controls.Add(lblName, 0, 2);
            tableLayoutPanel.Controls.Add(lblNameValue, 1, 2);
            tableLayoutPanel.Controls.Add(lblContactPhone, 0, 3);
            tableLayoutPanel.Controls.Add(lblContactPhoneValue, 1, 3);
            tableLayoutPanel.Controls.Add(lblEmail, 0, 4);
            tableLayoutPanel.Controls.Add(lblEmailValue, 1, 4);
            tableLayoutPanel.Controls.Add(lblAddress, 0, 5);
            tableLayoutPanel.Controls.Add(lblAddressValue, 1, 5);
            tableLayoutPanel.Controls.Add(btnClose, 1, 6);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(14, 12, 14, 12);
            tableLayoutPanel.RowCount = 7;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel.Size = new Size(555, 260);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle.Location = new Point(17, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(97, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Детали поставщика";
            // 
            // lblSupplierID
            // 
            lblSupplierID.AutoSize = true;
            lblSupplierID.Font = new Font("Segoe UI", 10F);
            lblSupplierID.ForeColor = Color.FromArgb(33, 37, 41);
            lblSupplierID.Location = new Point(17, 50);
            lblSupplierID.Name = "lblSupplierID";
            lblSupplierID.Size = new Size(23, 19);
            lblSupplierID.TabIndex = 1;
            lblSupplierID.Text = "ID";
            // 
            // lblSupplierIDValue
            // 
            lblSupplierIDValue.AutoSize = true;
            lblSupplierIDValue.Font = new Font("Segoe UI", 10F);
            lblSupplierIDValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblSupplierIDValue.Location = new Point(148, 50);
            lblSupplierIDValue.Name = "lblSupplierIDValue";
            lblSupplierIDValue.Size = new Size(0, 19);
            lblSupplierIDValue.TabIndex = 2;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            lblName.Location = new Point(17, 80);
            lblName.Name = "lblName";
            lblName.Size = new Size(51, 19);
            lblName.TabIndex = 3;
            lblName.Text = "Название";
            // 
            // lblNameValue
            // 
            lblNameValue.AutoSize = true;
            lblNameValue.Font = new Font("Segoe UI", 10F);
            lblNameValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblNameValue.Location = new Point(148, 80);
            lblNameValue.Name = "lblNameValue";
            lblNameValue.Size = new Size(0, 19);
            lblNameValue.TabIndex = 4;
            // 
            // lblContactPhone
            // 
            lblContactPhone.AutoSize = true;
            lblContactPhone.Font = new Font("Segoe UI", 10F);
            lblContactPhone.ForeColor = Color.FromArgb(33, 37, 41);
            lblContactPhone.Location = new Point(17, 110);
            lblContactPhone.Name = "lblContactPhone";
            lblContactPhone.Size = new Size(92, 30);
            lblContactPhone.TabIndex = 5;
            lblContactPhone.Text = "Контактный телефон";
            // 
            // lblContactPhoneValue
            // 
            lblContactPhoneValue.AutoSize = true;
            lblContactPhoneValue.Font = new Font("Segoe UI", 10F);
            lblContactPhoneValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblContactPhoneValue.Location = new Point(148, 110);
            lblContactPhoneValue.Name = "lblContactPhoneValue";
            lblContactPhoneValue.Size = new Size(0, 19);
            lblContactPhoneValue.TabIndex = 6;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.ForeColor = Color.FromArgb(33, 37, 41);
            lblEmail.Location = new Point(17, 140);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(107, 19);
            lblEmail.TabIndex = 7;
            lblEmail.Text = "Электронная почта";
            // 
            // lblEmailValue
            // 
            lblEmailValue.AutoSize = true;
            lblEmailValue.Font = new Font("Segoe UI", 10F);
            lblEmailValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblEmailValue.Location = new Point(148, 140);
            lblEmailValue.Name = "lblEmailValue";
            lblEmailValue.Size = new Size(0, 19);
            lblEmailValue.TabIndex = 8;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F);
            lblAddress.ForeColor = Color.FromArgb(33, 37, 41);
            lblAddress.Location = new Point(17, 170);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(66, 19);
            lblAddress.TabIndex = 9;
            lblAddress.Text = "Адрес";
            // 
            // lblAddressValue
            // 
            lblAddressValue.Font = new Font("Segoe UI", 10F);
            lblAddressValue.ForeColor = Color.FromArgb(33, 37, 41);
            lblAddressValue.Location = new Point(148, 170);
            lblAddressValue.Name = "lblAddressValue";
            lblAddressValue.Size = new Size(262, 58);
            lblAddressValue.TabIndex = 10;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(148, 209);
            btnClose.Margin = new Padding(3, 2, 3, 2);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(105, 27);
            btnClose.TabIndex = 11;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;
            // 
            // SupplierDetailsDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnClose;
            ClientSize = new Size(555, 260);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SupplierDetailsDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Информация о поставщике";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblTitle;
        private Label lblSupplierID;
        private Label lblSupplierIDValue;
        private Label lblName;
        private Label lblNameValue;
        private Label lblContactPhone;
        private Label lblContactPhoneValue;
        private Label lblEmail;
        private Label lblEmailValue;
        private Label lblAddress;
        private Label lblAddressValue;
        private Button btnClose;
    }
}
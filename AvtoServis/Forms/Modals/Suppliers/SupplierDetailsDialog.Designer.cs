namespace AvtoServis.Forms.Controls
{
    partial class SupplierDetailsDialog
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
            titleLabel = new Label();
            separator = new Panel();
            lblName = new Label();
            txtName = new TextBox();
            lblContactPhone = new Label();
            txtContactPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            buttonPanel = new FlowLayoutPanel();
            btnOk = new Button();
            tableLayoutPanel.SuspendLayout();
            buttonPanel.SuspendLayout();
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
            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(separator, 0, 1);
            tableLayoutPanel.Controls.Add(lblName, 0, 2);
            tableLayoutPanel.Controls.Add(txtName, 1, 2);
            tableLayoutPanel.Controls.Add(lblContactPhone, 0, 3);
            tableLayoutPanel.Controls.Add(txtContactPhone, 1, 3);
            tableLayoutPanel.Controls.Add(lblEmail, 0, 4);
            tableLayoutPanel.Controls.Add(txtEmail, 1, 4);
            tableLayoutPanel.Controls.Add(lblAddress, 0, 5);
            tableLayoutPanel.Controls.Add(txtAddress, 1, 5);
            tableLayoutPanel.Controls.Add(buttonPanel, 0, 6);
            tableLayoutPanel.SetColumnSpan(titleLabel, 2);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            tableLayoutPanel.SetColumnSpan(buttonPanel, 2);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 7;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.Size = new Size(434, 268);
            tableLayoutPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 16);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(200, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Подробности о поставщике";
            // 
            // separator
            // 
            separator.BackColor = Color.FromArgb(180, 180, 180);
            separator.Dock = DockStyle.Fill;
            separator.Location = new Point(19, 56);
            separator.Margin = new Padding(3, 0, 3, 0);
            separator.Name = "separator";
            separator.Size = new Size(396, 2);
            separator.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            lblName.Location = new Point(19, 58);
            lblName.Name = "lblName";
            lblName.Size = new Size(70, 23);
            lblName.TabIndex = 2;
            lblName.Text = "Название";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.Location = new Point(139, 61);
            txtName.Name = "txtName";
            txtName.ReadOnly = true;
            txtName.Size = new Size(260, 30);
            txtName.TabIndex = 3;
            // 
            // lblContactPhone
            // 
            lblContactPhone.AutoSize = true;
            lblContactPhone.Font = new Font("Segoe UI", 10F);
            lblContactPhone.ForeColor = Color.FromArgb(33, 37, 41);
            lblContactPhone.Location = new Point(19, 98);
            lblContactPhone.Name = "lblContactPhone";
            lblContactPhone.Size = new Size(114, 23);
            lblContactPhone.TabIndex = 4;
            lblContactPhone.Text = "Контактный телефон";
            // 
            // txtContactPhone
            // 
            txtContactPhone.BorderStyle = BorderStyle.FixedSingle;
            txtContactPhone.Font = new Font("Segoe UI", 10F);
            txtContactPhone.Location = new Point(139, 101);
            txtContactPhone.Name = "txtContactPhone";
            txtContactPhone.ReadOnly = true;
            txtContactPhone.Size = new Size(260, 30);
            txtContactPhone.TabIndex = 5;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.ForeColor = Color.FromArgb(33, 37, 41);
            lblEmail.Location = new Point(19, 138);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(82, 23);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Эл. почта";
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(139, 141);
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = true;
            txtEmail.Size = new Size(260, 30);
            txtEmail.TabIndex = 7;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F);
            lblAddress.ForeColor = Color.FromArgb(33, 37, 41);
            lblAddress.Location = new Point(19, 178);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(54, 23);
            lblAddress.TabIndex = 8;
            lblAddress.Text = "Адрес";
            // 
            // txtAddress
            // 
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(139, 181);
            txtAddress.Name = "txtAddress";
            txtAddress.ReadOnly = true;
            txtAddress.Size = new Size(260, 30);
            txtAddress.TabIndex = 9;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnOk);
            buttonPanel.Dock = DockStyle.Right;
            buttonPanel.Location = new Point(19, 221);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(396, 42);
            buttonPanel.TabIndex = 10;
            // 
            // btnOk
            // 
            btnOk.BackColor = Color.FromArgb(40, 167, 69);
            btnOk.DialogResult = DialogResult.OK;
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnOk.ForeColor = Color.White;
            btnOk.Location = new Point(3, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 34);
            btnOk.TabIndex = 0;
            btnOk.Text = "ОК";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += BtnOk_Click;
            // 
            // SupplierDetailsDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnOk;
            ClientSize = new Size(434, 268);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SupplierDetailsDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Подробности о поставщике";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label titleLabel;
        private Panel separator;
        private Label lblName;
        private TextBox txtName;
        private Label lblContactPhone;
        private TextBox txtContactPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblAddress;
        private TextBox txtAddress;
        private FlowLayoutPanel buttonPanel;
        private Button btnOk;
        private ToolTip toolTip;
    }
}
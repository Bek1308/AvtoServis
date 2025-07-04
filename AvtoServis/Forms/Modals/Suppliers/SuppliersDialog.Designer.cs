namespace AvtoServis.Forms.Controls
{
    partial class SuppliersDialog
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
            lblError = new Label();
            buttonPanel = new FlowLayoutPanel();
            btnCancel = new Button();
            btnSave = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
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
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 235F));
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
            tableLayoutPanel.Controls.Add(lblError, 0, 6);
            tableLayoutPanel.Controls.Add(buttonPanel, 0, 7);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 8;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 57F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
            tableLayoutPanel.Size = new Size(489, 380);
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
            titleLabel.Size = new Size(147, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Поставщик";
            // 
            // separator
            // 
            separator.BackColor = Color.FromArgb(180, 180, 180);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            separator.Dock = DockStyle.Fill;
            separator.Location = new Point(19, 73);
            separator.Margin = new Padding(3, 0, 3, 0);
            separator.Name = "separator";
            separator.Size = new Size(451, 2);
            separator.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            lblName.Location = new Point(19, 101);
            lblName.Margin = new Padding(3, 26, 3, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(86, 23);
            lblName.TabIndex = 2;
            lblName.Text = "Название";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.Location = new Point(254, 93);
            txtName.Margin = new Padding(3, 18, 3, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(216, 30);
            txtName.TabIndex = 3;
            // 
            // lblContactPhone
            // 
            lblContactPhone.AutoSize = true;
            lblContactPhone.Font = new Font("Segoe UI", 10F);
            lblContactPhone.ForeColor = Color.FromArgb(33, 37, 41);
            lblContactPhone.Location = new Point(19, 141);
            lblContactPhone.Name = "lblContactPhone";
            lblContactPhone.Size = new Size(174, 23);
            lblContactPhone.TabIndex = 4;
            lblContactPhone.Text = "Контактный телефон";
            // 
            // txtContactPhone
            // 
            txtContactPhone.BorderStyle = BorderStyle.FixedSingle;
            txtContactPhone.Font = new Font("Segoe UI", 10F);
            txtContactPhone.Location = new Point(254, 144);
            txtContactPhone.Name = "txtContactPhone";
            txtContactPhone.Size = new Size(216, 30);
            txtContactPhone.TabIndex = 5;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.ForeColor = Color.FromArgb(33, 37, 41);
            lblEmail.Location = new Point(19, 192);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(84, 23);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Эл. почта";
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(254, 195);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(216, 30);
            txtEmail.TabIndex = 7;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F);
            lblAddress.ForeColor = Color.FromArgb(33, 37, 41);
            lblAddress.Location = new Point(19, 245);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(57, 23);
            lblAddress.TabIndex = 8;
            lblAddress.Text = "Адрес";
            // 
            // txtAddress
            // 
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(254, 248);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(216, 30);
            txtAddress.TabIndex = 9;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblError, 2);
            lblError.Font = new Font("Segoe UI", 10F);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(19, 283);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 23);
            lblError.TabIndex = 10;
            lblError.Visible = false;
            // 
            // buttonPanel
            // 
            tableLayoutPanel.SetColumnSpan(buttonPanel, 2);
            buttonPanel.Controls.Add(panel2);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(panel1);
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Dock = DockStyle.Right;
            buttonPanel.Location = new Point(19, 319);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(451, 42);
            buttonPanel.TabIndex = 11;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.BackColor = Color.Red;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(71, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 34);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Right;
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(293, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 34);
            btnSave.TabIndex = 1;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // panel1
            // 
            panel1.Location = new Point(177, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(110, 34);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(62, 34);
            panel2.TabIndex = 3;
            // 
            // SuppliersDialog
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(489, 380);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SuppliersDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Поставщик";
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
        private Label lblError;
        private FlowLayoutPanel buttonPanel;
        private Button btnCancel;
        private Button btnSave;
        private ToolTip toolTip;
        private Panel panel1;
        private Panel panel2;
    }
}
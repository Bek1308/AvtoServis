namespace AvtoServis.Forms.Controls
{
    partial class SuppliersDialog
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            lblName = new Label();
            txtName = new TextBox();
            lblContactPhone = new Label();
            txtContactPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            btnCancel = new Button();
            btnSave = new Button();
            panelError = new Panel();
            lblError = new Label();
            tableLayoutPanel.SuspendLayout();
            panelError.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblName, 0, 0);
            tableLayoutPanel.Controls.Add(txtName, 1, 0);
            tableLayoutPanel.Controls.Add(lblContactPhone, 0, 1);
            tableLayoutPanel.Controls.Add(txtContactPhone, 1, 1);
            tableLayoutPanel.Controls.Add(lblEmail, 0, 2);
            tableLayoutPanel.Controls.Add(txtEmail, 1, 2);
            tableLayoutPanel.Controls.Add(lblAddress, 0, 3);
            tableLayoutPanel.Controls.Add(txtAddress, 1, 3);
            tableLayoutPanel.Controls.Add(panelError, 0, 4);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 5);
            tableLayoutPanel.Controls.Add(btnSave, 1, 5);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.Size = new Size(597, 360);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            lblName.Location = new Point(19, 16);
            lblName.Name = "lblName";
            lblName.Size = new Size(61, 23);
            lblName.TabIndex = 0;
            lblName.Text = "Название";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.Location = new Point(219, 19);
            txtName.Name = "txtName";
            txtName.Size = new Size(176, 30);
            txtName.TabIndex = 1;
            txtName.TextChanged += TxtName_TextChanged;
            // 
            // lblContactPhone
            // 
            lblContactPhone.AutoSize = true;
            lblContactPhone.Font = new Font("Segoe UI", 10F);
            lblContactPhone.ForeColor = Color.FromArgb(33, 37, 41);
            lblContactPhone.Location = new Point(19, 56);
            lblContactPhone.Name = "lblContactPhone";
            lblContactPhone.Size = new Size(165, 23);
            lblContactPhone.TabIndex = 2;
            lblContactPhone.Text = "Контактный телефон";
            // 
            // txtContactPhone
            // 
            txtContactPhone.BorderStyle = BorderStyle.FixedSingle;
            txtContactPhone.Font = new Font("Segoe UI", 10F);
            txtContactPhone.Location = new Point(219, 59);
            txtContactPhone.Name = "txtContactPhone";
            txtContactPhone.Size = new Size(176, 30);
            txtContactPhone.TabIndex = 3;
            txtContactPhone.TextChanged += TxtContactPhone_TextChanged;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.ForeColor = Color.FromArgb(33, 37, 41);
            lblEmail.Location = new Point(19, 96);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(132, 23);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Электронная почта";
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(219, 99);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(176, 30);
            txtEmail.TabIndex = 5;
            txtEmail.TextChanged += TxtEmail_TextChanged;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F);
            lblAddress.ForeColor = Color.FromArgb(33, 37, 41);
            lblAddress.Location = new Point(19, 136);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(82, 23);
            lblAddress.TabIndex = 6;
            lblAddress.Text = "Адрес";
            // 
            // txtAddress
            // 
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(219, 139);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(176, 74);
            txtAddress.TabIndex = 7;
            txtAddress.TextChanged += TxtAddress_TextChanged;
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
            btnCancel.Location = new Point(219, 312);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 36);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(401, 312);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 36);
            btnSave.TabIndex = 9;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // panelError
            // 
            panelError.BackColor = Color.FromArgb(255, 245, 245);
            panelError.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(panelError, 2);
            panelError.Controls.Add(lblError);
            panelError.Location = new Point(19, 245);
            panelError.Margin = new Padding(3, 4, 3, 4);
            panelError.Name = "panelError";
            panelError.Size = new Size(559, 60);
            panelError.TabIndex = 10;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(3, 7);
            lblError.MaximumSize = new Size(555, 0);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 23);
            lblError.TabIndex = 0;
            lblError.TextAlign = ContentAlignment.MiddleLeft;
            lblError.Visible = false;
            // 
            // SuppliersDialog
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(597, 360);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SuppliersDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Поставщик";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            panelError.ResumeLayout(false);
            panelError.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblName;
        private TextBox txtName;
        private Label lblContactPhone;
        private TextBox txtContactPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblAddress;
        private TextBox txtAddress;
        private Panel panelError;
        private Label lblError;
        private Button btnCancel;
        private Button btnSave;
    }
}
namespace AvtoServis.Forms.Controls
{
    partial class CustomerDetailsForm
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
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblRegistrationDate = new Label();
            txtRegistrationDate = new TextBox();
            lblIsActive = new Label();
            txtIsActive = new TextBox();
            lblUmumiyQarz = new Label();
            txtUmumiyQarz = new TextBox();
            lblDetails = new Label();
            dataGridView = new DataGridView();
            btnClose = new Button();
            toolTip = new ToolTip(components);
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(separator, 0, 1);
            tableLayoutPanel.Controls.Add(lblFullName, 0, 2);
            tableLayoutPanel.Controls.Add(txtFullName, 1, 2);
            tableLayoutPanel.Controls.Add(lblPhone, 0, 3);
            tableLayoutPanel.Controls.Add(txtPhone, 1, 3);
            tableLayoutPanel.Controls.Add(lblEmail, 0, 4);
            tableLayoutPanel.Controls.Add(txtEmail, 1, 4);
            tableLayoutPanel.Controls.Add(lblAddress, 0, 5);
            tableLayoutPanel.Controls.Add(txtAddress, 1, 5);
            tableLayoutPanel.Controls.Add(lblRegistrationDate, 0, 6);
            tableLayoutPanel.Controls.Add(txtRegistrationDate, 1, 6);
            tableLayoutPanel.Controls.Add(lblIsActive, 0, 7);
            tableLayoutPanel.Controls.Add(txtIsActive, 1, 7);
            tableLayoutPanel.Controls.Add(lblUmumiyQarz, 0, 8);
            tableLayoutPanel.Controls.Add(txtUmumiyQarz, 1, 8);
            tableLayoutPanel.Controls.Add(lblDetails, 0, 9);
            tableLayoutPanel.Controls.Add(dataGridView, 0, 10);
            tableLayoutPanel.Controls.Add(btnClose, 1, 11);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 12;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.Size = new Size(439, 484);
            tableLayoutPanel.TabIndex = 0;
            toolTip.SetToolTip(tableLayoutPanel, "Форма для просмотра подробностей клиента");
            // 
            // titleLabel
            // 
            titleLabel.AccessibleDescription = "Заголовок формы подробностей клиента";
            titleLabel.AccessibleName = "Подробности клиента";
            titleLabel.Anchor = AnchorStyles.Left;
            titleLabel.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(titleLabel, 2);
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 17);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(276, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Подробности клиента";
            toolTip.SetToolTip(titleLabel, "Заголовок формы подробностей клиента");
            // 
            // separator
            // 
            separator.AccessibleDescription = "Разделительная линия";
            separator.AccessibleName = "Разделитель";
            separator.BackColor = Color.FromArgb(108, 117, 125);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            separator.Location = new Point(19, 51);
            separator.Name = "separator";
            separator.Size = new Size(396, 2);
            separator.TabIndex = 1;
            toolTip.SetToolTip(separator, "Разделительная линия");
            // 
            // lblFullName
            // 
            lblFullName.AccessibleDescription = "ФИО клиента";
            lblFullName.AccessibleName = "ФИО";
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 10F);
            lblFullName.ForeColor = Color.FromArgb(33, 37, 41);
            lblFullName.Location = new Point(19, 53);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(52, 23);
            lblFullName.TabIndex = 2;
            lblFullName.Text = "ФИО:";
            // 
            // txtFullName
            // 
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            txtFullName.Font = new Font("Segoe UI", 10F);
            txtFullName.Location = new Point(141, 56);
            txtFullName.Name = "txtFullName";
            txtFullName.ReadOnly = true;
            txtFullName.Size = new Size(279, 30);
            txtFullName.TabIndex = 3;
            toolTip.SetToolTip(txtFullName, "ФИО клиента");
            // 
            // lblPhone
            // 
            lblPhone.AccessibleDescription = "Телефон клиента";
            lblPhone.AccessibleName = "Телефон";
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 10F);
            lblPhone.ForeColor = Color.FromArgb(33, 37, 41);
            lblPhone.Location = new Point(19, 93);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(82, 23);
            lblPhone.TabIndex = 4;
            lblPhone.Text = "Телефон:";
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 10F);
            txtPhone.Location = new Point(141, 96);
            txtPhone.Name = "txtPhone";
            txtPhone.ReadOnly = true;
            txtPhone.Size = new Size(279, 30);
            txtPhone.TabIndex = 5;
            toolTip.SetToolTip(txtPhone, "Номер телефона клиента");
            // 
            // lblEmail
            // 
            lblEmail.AccessibleDescription = "Электронная почта клиента";
            lblEmail.AccessibleName = "Эл. почта";
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.ForeColor = Color.FromArgb(33, 37, 41);
            lblEmail.Location = new Point(19, 133);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(88, 23);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Эл. почта:";
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(141, 136);
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = true;
            txtEmail.Size = new Size(279, 30);
            txtEmail.TabIndex = 7;
            toolTip.SetToolTip(txtEmail, "Электронная почта клиента");
            // 
            // lblAddress
            // 
            lblAddress.AccessibleDescription = "Адрес клиента";
            lblAddress.AccessibleName = "Адрес";
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F);
            lblAddress.ForeColor = Color.FromArgb(33, 37, 41);
            lblAddress.Location = new Point(19, 173);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(61, 23);
            lblAddress.TabIndex = 8;
            lblAddress.Text = "Адрес:";
            // 
            // txtAddress
            // 
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(141, 176);
            txtAddress.Name = "txtAddress";
            txtAddress.ReadOnly = true;
            txtAddress.Size = new Size(279, 30);
            txtAddress.TabIndex = 9;
            toolTip.SetToolTip(txtAddress, "Адрес клиента");
            // 
            // lblRegistrationDate
            // 
            lblRegistrationDate.AccessibleDescription = "Дата регистрации клиента";
            lblRegistrationDate.AccessibleName = "Дата регистрации";
            lblRegistrationDate.AutoSize = true;
            lblRegistrationDate.Font = new Font("Segoe UI", 10F);
            lblRegistrationDate.ForeColor = Color.FromArgb(33, 37, 41);
            lblRegistrationDate.Location = new Point(19, 213);
            lblRegistrationDate.Name = "lblRegistrationDate";
            lblRegistrationDate.Size = new Size(114, 40);
            lblRegistrationDate.TabIndex = 10;
            lblRegistrationDate.Text = "Дата регистрации:";
            // 
            // txtRegistrationDate
            // 
            txtRegistrationDate.BorderStyle = BorderStyle.FixedSingle;
            txtRegistrationDate.Font = new Font("Segoe UI", 10F);
            txtRegistrationDate.Location = new Point(141, 216);
            txtRegistrationDate.Name = "txtRegistrationDate";
            txtRegistrationDate.ReadOnly = true;
            txtRegistrationDate.Size = new Size(279, 30);
            txtRegistrationDate.TabIndex = 11;
            toolTip.SetToolTip(txtRegistrationDate, "Дата регистрации клиента");
            // 
            // lblIsActive
            // 
            lblIsActive.AccessibleDescription = "Статус активности клиента";
            lblIsActive.AccessibleName = "Активен";
            lblIsActive.AutoSize = true;
            lblIsActive.Font = new Font("Segoe UI", 10F);
            lblIsActive.ForeColor = Color.FromArgb(33, 37, 41);
            lblIsActive.Location = new Point(19, 253);
            lblIsActive.Name = "lblIsActive";
            lblIsActive.Size = new Size(78, 23);
            lblIsActive.TabIndex = 12;
            lblIsActive.Text = "Активен:";
            // 
            // txtIsActive
            // 
            txtIsActive.BorderStyle = BorderStyle.FixedSingle;
            txtIsActive.Font = new Font("Segoe UI", 10F);
            txtIsActive.Location = new Point(141, 256);
            txtIsActive.Name = "txtIsActive";
            txtIsActive.ReadOnly = true;
            txtIsActive.Size = new Size(279, 30);
            txtIsActive.TabIndex = 13;
            toolTip.SetToolTip(txtIsActive, "Статус активности клиента");
            // 
            // lblUmumiyQarz
            // 
            lblUmumiyQarz.AccessibleDescription = "Общий долг клиента";
            lblUmumiyQarz.AccessibleName = "Общий долг";
            lblUmumiyQarz.AutoSize = true;
            lblUmumiyQarz.Font = new Font("Segoe UI", 10F);
            lblUmumiyQarz.ForeColor = Color.FromArgb(33, 37, 41);
            lblUmumiyQarz.Location = new Point(19, 293);
            lblUmumiyQarz.Name = "lblUmumiyQarz";
            lblUmumiyQarz.Size = new Size(111, 23);
            lblUmumiyQarz.TabIndex = 14;
            lblUmumiyQarz.Text = "Общий долг:";
            // 
            // txtUmumiyQarz
            // 
            txtUmumiyQarz.BorderStyle = BorderStyle.FixedSingle;
            txtUmumiyQarz.Font = new Font("Segoe UI", 10F);
            txtUmumiyQarz.Location = new Point(141, 296);
            txtUmumiyQarz.Name = "txtUmumiyQarz";
            txtUmumiyQarz.ReadOnly = true;
            txtUmumiyQarz.Size = new Size(279, 30);
            txtUmumiyQarz.TabIndex = 15;
            toolTip.SetToolTip(txtUmumiyQarz, "Общий долг клиента");
            // 
            // lblDetails
            // 
            lblDetails.AccessibleDescription = "Детали долга и машины клиента";
            lblDetails.AccessibleName = "Детали";
            lblDetails.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblDetails, 2);
            lblDetails.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDetails.ForeColor = Color.FromArgb(33, 37, 41);
            lblDetails.Location = new Point(19, 333);
            lblDetails.Name = "lblDetails";
            lblDetails.Size = new Size(220, 23);
            lblDetails.TabIndex = 16;
            lblDetails.Text = "Детали долга и машины:";
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2 });
            tableLayoutPanel.SetColumnSpan(dataGridView, 2);
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(19, 376);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(401, 36);
            dataGridView.TabIndex = 17;
            toolTip.SetToolTip(dataGridView, "Модели машин и детали долга клиента");
            // 
            // btnClose
            // 
            btnClose.AccessibleDescription = "Закрыть форму";
            btnClose.AccessibleName = "Закрыть";
            btnClose.AutoSize = true;
            btnClose.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            tableLayoutPanel.SetColumnSpan(btnClose, 2);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(19, 451);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(92, 14);
            btnClose.TabIndex = 11;
            btnClose.Text = "Закрыть";
            toolTip.SetToolTip(btnClose, "Закрыть форму");
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // CustomerDetailsForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(439, 484);
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomerDetailsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Подробности клиента";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label separator;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblRegistrationDate;
        private System.Windows.Forms.TextBox txtRegistrationDate;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.TextBox txtIsActive;
        private System.Windows.Forms.Label lblUmumiyQarz;
        private System.Windows.Forms.TextBox txtUmumiyQarz;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolTip toolTip;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}
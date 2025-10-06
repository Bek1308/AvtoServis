namespace AvtoServis.Forms.Controls
{
    partial class CustomerEditForm
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
            dtpRegistrationDate = new DateTimePicker();
            lblIsActive = new Label();
            chkIsActive = new CheckBox();
            lblError = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            toolTip = new ToolTip(components);
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.406826F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.593174F));
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
            tableLayoutPanel.Controls.Add(dtpRegistrationDate, 1, 6);
            tableLayoutPanel.Controls.Add(lblIsActive, 0, 7);
            tableLayoutPanel.Controls.Add(chkIsActive, 1, 7);
            tableLayoutPanel.Controls.Add(lblError, 0, 8);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 9);
            tableLayoutPanel.Controls.Add(btnSave, 1, 9);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 10;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel.Size = new Size(413, 378);
            tableLayoutPanel.TabIndex = 0;
            toolTip.SetToolTip(tableLayoutPanel, "Форма для редактирования клиента");
            // 
            // titleLabel
            // 
            titleLabel.AccessibleDescription = "Заголовок формы редактирования клиента";
            titleLabel.AccessibleName = "Редактировать клиента";
            titleLabel.Anchor = AnchorStyles.Left;
            titleLabel.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(titleLabel, 2);
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 17);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(290, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Редактировать клиента";
            toolTip.SetToolTip(titleLabel, "Заголовок формы редактирования клиента");
            // 
            // separator
            // 
            separator.AccessibleDescription = "Разделительная линия";
            separator.AccessibleName = "Разделитель";
            separator.BackColor = Color.FromArgb(108, 117, 125);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            separator.Location = new Point(19, 51);
            separator.Name = "separator";
            separator.Size = new Size(375, 2);
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
            txtFullName.Location = new Point(192, 56);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(200, 30);
            txtFullName.TabIndex = 3;
            toolTip.SetToolTip(txtFullName, "Введите ФИО клиента");
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
            txtPhone.Location = new Point(192, 96);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(200, 30);
            txtPhone.TabIndex = 5;
            toolTip.SetToolTip(txtPhone, "Введите номер телефона клиента");
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
            txtEmail.Location = new Point(192, 136);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 30);
            txtEmail.TabIndex = 7;
            toolTip.SetToolTip(txtEmail, "Введите электронную почту клиента (необязательно)");
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
            txtAddress.Location = new Point(192, 176);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(200, 30);
            txtAddress.TabIndex = 9;
            toolTip.SetToolTip(txtAddress, "Введите адрес клиента (необязательно)");
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
            lblRegistrationDate.Size = new Size(156, 23);
            lblRegistrationDate.TabIndex = 10;
            lblRegistrationDate.Text = "Дата регистрации:";
            // 
            // dtpRegistrationDate
            // 
            dtpRegistrationDate.Font = new Font("Segoe UI", 10F);
            dtpRegistrationDate.Format = DateTimePickerFormat.Short;
            dtpRegistrationDate.Location = new Point(192, 216);
            dtpRegistrationDate.Name = "dtpRegistrationDate";
            dtpRegistrationDate.Size = new Size(200, 30);
            dtpRegistrationDate.TabIndex = 11;
            toolTip.SetToolTip(dtpRegistrationDate, "Выберите дату регистрации клиента");
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
            // chkIsActive
            // 
            chkIsActive.Location = new Point(192, 256);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(30, 30);
            chkIsActive.TabIndex = 13;
            toolTip.SetToolTip(chkIsActive, "Укажите, активен ли клиент");
            // 
            // lblError
            // 
            lblError.AccessibleDescription = "Сообщение об ошибке";
            lblError.AccessibleName = "Ошибка";
            lblError.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblError, 2);
            lblError.Font = new Font("Segoe UI", 9F);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(19, 293);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 20);
            lblError.TabIndex = 8;
            toolTip.SetToolTip(lblError, "Сообщение об ошибке");
            lblError.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.AccessibleDescription = "Отменить редактирование клиента";
            btnCancel.AccessibleName = "Отмена";
            btnCancel.AutoSize = true;
            btnCancel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(19, 326);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(82, 33);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Отмена";
            toolTip.SetToolTip(btnCancel, "Отменить редактирование");
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.AccessibleDescription = "Сохранить изменения клиента";
            btnSave.AccessibleName = "Сохранить";
            btnSave.Anchor = AnchorStyles.Right;
            btnSave.AutoSize = true;
            btnSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(285, 326);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(109, 33);
            btnSave.TabIndex = 10;
            btnSave.Text = "Сохранить";
            toolTip.SetToolTip(btnSave, "Сохранить изменения");
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            // 
            // CustomerEditForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(413, 378);
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomerEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактировать клиента";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
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
        private System.Windows.Forms.DateTimePicker dtpRegistrationDate;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
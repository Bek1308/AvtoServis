namespace AvtoServis.Forms.Controls
{
    partial class PartsIncomeDialog
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
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            lblBatchName = new System.Windows.Forms.Label();
            txtBatchName = new System.Windows.Forms.TextBox();
            lblPartID = new System.Windows.Forms.Label();
            cmbPartID = new System.Windows.Forms.ComboBox();
            lblSupplierID = new System.Windows.Forms.Label();
            cmbSupplierID = new System.Windows.Forms.ComboBox();
            lblDate = new System.Windows.Forms.Label();
            dtpDate = new System.Windows.Forms.DateTimePicker();
            lblQuantity = new System.Windows.Forms.Label();
            txtQuantity = new System.Windows.Forms.TextBox();
            lblUnitPrice = new System.Windows.Forms.Label();
            txtUnitPrice = new System.Windows.Forms.TextBox();
            lblMarkup = new System.Windows.Forms.Label();
            txtMarkup = new System.Windows.Forms.TextBox();
            lblStatusID = new System.Windows.Forms.Label();
            cmbStatusID = new System.Windows.Forms.ComboBox();
            lblStockID = new System.Windows.Forms.Label();
            cmbStockID = new System.Windows.Forms.ComboBox();
            lblInvoiceNumber = new System.Windows.Forms.Label();
            txtInvoiceNumber = new System.Windows.Forms.TextBox();
            lblPaidAmount = new System.Windows.Forms.Label();
            txtPaidAmount = new System.Windows.Forms.TextBox();
            _messagePanel = new System.Windows.Forms.Panel();
            _messageLabel = new System.Windows.Forms.Label();
            btnCancel = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            toolTip = new System.Windows.Forms.ToolTip(components);
            errorTimer = new System.Windows.Forms.Timer(components);
            tableLayoutPanel.SuspendLayout();
            _messagePanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.26984F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.73016F));
            tableLayoutPanel.Controls.Add(lblBatchName, 0, 0);
            tableLayoutPanel.Controls.Add(txtBatchName, 1, 0);
            tableLayoutPanel.Controls.Add(lblPartID, 0, 1);
            tableLayoutPanel.Controls.Add(cmbPartID, 1, 1);
            tableLayoutPanel.Controls.Add(lblSupplierID, 0, 2);
            tableLayoutPanel.Controls.Add(cmbSupplierID, 1, 2);
            tableLayoutPanel.Controls.Add(lblDate, 0, 3);
            tableLayoutPanel.Controls.Add(dtpDate, 1, 3);
            tableLayoutPanel.Controls.Add(lblQuantity, 0, 4);
            tableLayoutPanel.Controls.Add(txtQuantity, 1, 4);
            tableLayoutPanel.Controls.Add(lblUnitPrice, 0, 5);
            tableLayoutPanel.Controls.Add(txtUnitPrice, 1, 5);
            tableLayoutPanel.Controls.Add(lblMarkup, 0, 6);
            tableLayoutPanel.Controls.Add(txtMarkup, 1, 6);
            tableLayoutPanel.Controls.Add(lblStatusID, 0, 7);
            tableLayoutPanel.Controls.Add(cmbStatusID, 1, 7);
            tableLayoutPanel.Controls.Add(lblStockID, 0, 8);
            tableLayoutPanel.Controls.Add(cmbStockID, 1, 8);
            tableLayoutPanel.Controls.Add(lblInvoiceNumber, 0, 9);
            tableLayoutPanel.Controls.Add(txtInvoiceNumber, 1, 9);
            tableLayoutPanel.Controls.Add(lblPaidAmount, 0, 10);
            tableLayoutPanel.Controls.Add(txtPaidAmount, 1, 10);
            tableLayoutPanel.Controls.Add(_messagePanel, 0, 11);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 12);
            tableLayoutPanel.Controls.Add(btnSave, 1, 12);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new System.Windows.Forms.Padding(16);
            tableLayoutPanel.RowCount = 13;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F)); // Xabar paneli uchun
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F)); // Tugmalar uchun
            tableLayoutPanel.Size = new System.Drawing.Size(524, 680);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblBatchName
            // 
            lblBatchName.AutoSize = true;
            lblBatchName.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblBatchName.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblBatchName.Location = new System.Drawing.Point(19, 16);
            lblBatchName.Name = "lblBatchName";
            lblBatchName.Size = new System.Drawing.Size(147, 23);
            lblBatchName.TabIndex = 0;
            lblBatchName.Text = "Название партии";
            lblBatchName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblBatchName, "Введите название партии");
            // 
            // txtBatchName
            // 
            txtBatchName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtBatchName.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtBatchName.Location = new System.Drawing.Point(222, 11);
            txtBatchName.Name = "txtBatchName";
            txtBatchName.Size = new System.Drawing.Size(283, 30);
            txtBatchName.TabIndex = 1;
            toolTip.SetToolTip(txtBatchName, "Введите название партии");
            txtBatchName.TextChanged += txtBatchName_TextChanged;
            // 
            // lblPartID
            // 
            lblPartID.AutoSize = true;
            lblPartID.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblPartID.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblPartID.Location = new System.Drawing.Point(19, 56);
            lblPartID.Name = "lblPartID";
            lblPartID.Size = new System.Drawing.Size(65, 23);
            lblPartID.TabIndex = 2;
            lblPartID.Text = "Деталь";
            lblPartID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblPartID, "Выберите деталь");
            // 
            // cmbPartID
            // 
            cmbPartID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbPartID.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbPartID.Location = new System.Drawing.Point(222, 51);
            cmbPartID.Name = "cmbPartID";
            cmbPartID.Size = new System.Drawing.Size(283, 31);
            cmbPartID.TabIndex = 3;
            toolTip.SetToolTip(cmbPartID, "Выберите деталь");
            cmbPartID.SelectedIndexChanged += cmbPartID_SelectedIndexChanged;
            // 
            // lblSupplierID
            // 
            lblSupplierID.AutoSize = true;
            lblSupplierID.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSupplierID.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblSupplierID.Location = new System.Drawing.Point(19, 96);
            lblSupplierID.Name = "lblSupplierID";
            lblSupplierID.Size = new System.Drawing.Size(97, 23);
            lblSupplierID.TabIndex = 4;
            lblSupplierID.Text = "Поставщик";
            lblSupplierID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblSupplierID, "Выберите поставщика");
            // 
            // cmbSupplierID
            // 
            cmbSupplierID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSupplierID.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbSupplierID.Location = new System.Drawing.Point(222, 91);
            cmbSupplierID.Name = "cmbSupplierID";
            cmbSupplierID.Size = new System.Drawing.Size(283, 31);
            cmbSupplierID.TabIndex = 5;
            toolTip.SetToolTip(cmbSupplierID, "Выберите поставщика");
            cmbSupplierID.SelectedIndexChanged += cmbSupplierID_SelectedIndexChanged;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblDate.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblDate.Location = new System.Drawing.Point(19, 136);
            lblDate.Name = "lblDate";
            lblDate.Size = new System.Drawing.Size(47, 23);
            lblDate.TabIndex = 6;
            lblDate.Text = "Дата";
            lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblDate, "Выберите дату поступления");
            // 
            // dtpDate
            // 
            dtpDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpDate.Location = new System.Drawing.Point(222, 131);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new System.Drawing.Size(283, 30);
            dtpDate.TabIndex = 7;
            toolTip.SetToolTip(dtpDate, "Выберите дату поступления");
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblQuantity.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblQuantity.Location = new System.Drawing.Point(19, 176);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new System.Drawing.Size(102, 23);
            lblQuantity.TabIndex = 8;
            lblQuantity.Text = "Количество";
            lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblQuantity, "Введите количество деталей");
            // 
            // txtQuantity
            // 
            txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtQuantity.Location = new System.Drawing.Point(222, 171);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new System.Drawing.Size(283, 30);
            txtQuantity.TabIndex = 9;
            toolTip.SetToolTip(txtQuantity, "Введите количество деталей");
            txtQuantity.TextChanged += txtQuantity_TextChanged;
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblUnitPrice.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblUnitPrice.Location = new System.Drawing.Point(19, 216);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new System.Drawing.Size(144, 23);
            lblUnitPrice.TabIndex = 10;
            lblUnitPrice.Text = "Цена за единицу";
            lblUnitPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblUnitPrice, "Введите цену за единицу");
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtUnitPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtUnitPrice.Location = new System.Drawing.Point(222, 211);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new System.Drawing.Size(283, 30);
            txtUnitPrice.TabIndex = 11;
            toolTip.SetToolTip(txtUnitPrice, "Введите цену за единицу");
            txtUnitPrice.TextChanged += txtUnitPrice_TextChanged;
            // 
            // lblMarkup
            // 
            lblMarkup.AutoSize = true;
            lblMarkup.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblMarkup.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblMarkup.Location = new System.Drawing.Point(19, 256);
            lblMarkup.Name = "lblMarkup";
            lblMarkup.Size = new System.Drawing.Size(77, 23);
            lblMarkup.TabIndex = 12;
            lblMarkup.Text = "Наценка";
            lblMarkup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblMarkup, "Введите наценку");
            // 
            // txtMarkup
            // 
            txtMarkup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtMarkup.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtMarkup.Location = new System.Drawing.Point(222, 251);
            txtMarkup.Name = "txtMarkup";
            txtMarkup.Size = new System.Drawing.Size(283, 30);
            txtMarkup.TabIndex = 13;
            toolTip.SetToolTip(txtMarkup, "Введите наценку");
            txtMarkup.TextChanged += txtMarkup_TextChanged;
            // 
            // lblStatusID
            // 
            lblStatusID.AutoSize = true;
            lblStatusID.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblStatusID.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblStatusID.Location = new System.Drawing.Point(19, 296);
            lblStatusID.Name = "lblStatusID";
            lblStatusID.Size = new System.Drawing.Size(60, 23);
            lblStatusID.TabIndex = 14;
            lblStatusID.Text = "Статус";
            lblStatusID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblStatusID, "Выберите статус");
            // 
            // cmbStatusID
            // 
            cmbStatusID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbStatusID.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbStatusID.Location = new System.Drawing.Point(222, 291);
            cmbStatusID.Name = "cmbStatusID";
            cmbStatusID.Size = new System.Drawing.Size(283, 31);
            cmbStatusID.TabIndex = 15;
            toolTip.SetToolTip(cmbStatusID, "Выберите статус");
            cmbStatusID.SelectedIndexChanged += cmbStatusID_SelectedIndexChanged;
            // 
            // lblStockID
            // 
            lblStockID.AutoSize = true;
            lblStockID.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblStockID.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblStockID.Location = new System.Drawing.Point(19, 336);
            lblStockID.Name = "lblStockID";
            lblStockID.Size = new System.Drawing.Size(56, 23);
            lblStockID.TabIndex = 16;
            lblStockID.Text = "Склад";
            lblStockID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblStockID, "Выберите склад");
            // 
            // cmbStockID
            // 
            cmbStockID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbStockID.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbStockID.Location = new System.Drawing.Point(222, 331);
            cmbStockID.Name = "cmbStockID";
            cmbStockID.Size = new System.Drawing.Size(283, 31);
            cmbStockID.TabIndex = 17;
            toolTip.SetToolTip(cmbStockID, "Выберите склад");
            cmbStockID.SelectedIndexChanged += cmbStockID_SelectedIndexChanged;
            // 
            // lblInvoiceNumber
            // 
            lblInvoiceNumber.AutoSize = true;
            lblInvoiceNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblInvoiceNumber.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblInvoiceNumber.Location = new System.Drawing.Point(19, 376);
            lblInvoiceNumber.Name = "lblInvoiceNumber";
            lblInvoiceNumber.Size = new System.Drawing.Size(111, 23);
            lblInvoiceNumber.TabIndex = 18;
            lblInvoiceNumber.Text = "Номер счета";
            lblInvoiceNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblInvoiceNumber, "Введите номер счета (необязательно)");
            // 
            // txtInvoiceNumber
            // 
            txtInvoiceNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtInvoiceNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtInvoiceNumber.Location = new System.Drawing.Point(222, 371);
            txtInvoiceNumber.Name = "txtInvoiceNumber";
            txtInvoiceNumber.Size = new System.Drawing.Size(283, 30);
            txtInvoiceNumber.TabIndex = 19;
            toolTip.SetToolTip(txtInvoiceNumber, "Введите номер счета (необязательно)");
            // 
            // lblPaidAmount
            // 
            lblPaidAmount.AutoSize = true;
            lblPaidAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblPaidAmount.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblPaidAmount.Location = new System.Drawing.Point(19, 416);
            lblPaidAmount.Name = "lblPaidAmount";
            lblPaidAmount.Size = new System.Drawing.Size(162, 23);
            lblPaidAmount.TabIndex = 20;
            lblPaidAmount.Text = "Оплаченная сумма";
            lblPaidAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblPaidAmount, "Введите оплаченную сумму");
            // 
            // txtPaidAmount
            // 
            txtPaidAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtPaidAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtPaidAmount.Location = new System.Drawing.Point(222, 411);
            txtPaidAmount.Name = "txtPaidAmount";
            txtPaidAmount.Size = new System.Drawing.Size(283, 30);
            txtPaidAmount.TabIndex = 21;
            toolTip.SetToolTip(txtPaidAmount, "Введите оплаченную сумму");
            txtPaidAmount.TextChanged += txtPaidAmount_TextChanged;
            // 
            // _messagePanel
            // 
            _messagePanel.AutoSize = true;
            _messagePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            _messagePanel.BackColor = System.Drawing.Color.FromArgb(245, 255, 245);
            _messagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(_messagePanel, 2);
            _messagePanel.Controls.Add(_messageLabel);
            _messagePanel.Location = new System.Drawing.Point(19, 451);
            _messagePanel.MinimumSize = new System.Drawing.Size(486, 60);
            _messagePanel.Name = "_messagePanel";
            _messagePanel.Padding = new System.Windows.Forms.Padding(10);
            _messagePanel.Size = new System.Drawing.Size(486, 60);
            _messagePanel.TabIndex = 22;
            // 
            // _messageLabel
            // 
            _messageLabel.AutoSize = true;
            _messageLabel.BackColor = System.Drawing.Color.Transparent;
            _messageLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            _messageLabel.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
            _messageLabel.Location = new System.Drawing.Point(10, 10);
            _messageLabel.MaximumSize = new System.Drawing.Size(460, 0);
            _messageLabel.Name = "_messageLabel";
            _messageLabel.Size = new System.Drawing.Size(0, 23);
            _messageLabel.TabIndex = 0;
            _messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            btnCancel.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(130, 140, 150);
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.Location = new System.Drawing.Point(19, 534);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(121, 36);
            btnCancel.TabIndex = 23;
            btnCancel.Text = "Отмена";
            toolTip.SetToolTip(btnCancel, "Отменить сохранение");
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            btnSave.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 187, 89);
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(372, 534);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(133, 36);
            btnSave.TabIndex = 24;
            btnSave.Text = "Сохранить";
            toolTip.SetToolTip(btnSave, "Сохранить данные в базу");
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
            // errorTimer
            // 
            errorTimer.Interval = 3000;
            // 
            // PartsIncomeDialog
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            ClientSize = new System.Drawing.Size(524, 680);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(450, 620);
            Name = "PartsIncomeDialog";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Редактирование поступления";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            _messagePanel.ResumeLayout(false);
            _messagePanel.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblBatchName;
        private System.Windows.Forms.TextBox txtBatchName;
        private System.Windows.Forms.Label lblPartID;
        private System.Windows.Forms.ComboBox cmbPartID;
        private System.Windows.Forms.Label lblSupplierID;
        private System.Windows.Forms.ComboBox cmbSupplierID;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Label lblMarkup;
        private System.Windows.Forms.TextBox txtMarkup;
        private System.Windows.Forms.Label lblStatusID;
        private System.Windows.Forms.ComboBox cmbStatusID;
        private System.Windows.Forms.Label lblStockID;
        private System.Windows.Forms.ComboBox cmbStockID;
        private System.Windows.Forms.Label lblInvoiceNumber;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.Label lblPaidAmount;
        private System.Windows.Forms.TextBox txtPaidAmount;
        private System.Windows.Forms.Panel _messagePanel;
        private System.Windows.Forms.Label _messageLabel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer errorTimer;
    }
}
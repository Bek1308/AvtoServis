//using DocumentFormat.OpenXml.Vml.Spreadsheet;
//using DocumentFormat.OpenXml.Wordprocessing;
//using Color = System.Drawing.Color;
//using Font = System.Drawing.Font;

//namespace AvtoServis.Forms.Controls
//{
//    partial class PartsIncomeDialog
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            components = new System.ComponentModel.Container();
//            toolTip = new ToolTip(components);
//            tableLayoutPanel = new TableLayoutPanel();
//            lblPart = new Label();
//            cmbPart = new ComboBox();
//            lblSupplier = new Label();
//            cmbSupplier = new ComboBox();
//            lblDate = new Label();
//            dtpDate = new DateTimePicker();
//            lblQuantity = new Label();
//            txtQuantity = new TextBox();
//            lblUnitPrice = new Label();
//            txtUnitPrice = new TextBox();
//            lblMarkup = new Label();
//            txtMarkup = new TextBox();
//            lblStatus = new Label();
//            cmbStatus = new ComboBox();
//            lblOperation = new Label();
//            cmbOperation = new ComboBox();
//            lblStock = new Label();
//            cmbStock = new ComboBox();
//            lblInvoiceNumber = new Label();
//            txtInvoiceNumber = new TextBox();
//            lblUser = new Label();
//            cmbUser = new ComboBox();
//            lblPaidAmount = new Label();
//            txtPaidAmount = new TextBox();
//            lblBatch = new Label();
//            txtBatch = new TextBox();
//            lblError = new Label();
//            btnCancel = new Button();
//            btnSave = new Button();
//            tableLayoutPanel.SuspendLayout();
//            SuspendLayout();
//            // 
//            // toolTip
//            // 
//            toolTip.AutoPopDelay = 5000;
//            toolTip.InitialDelay = 1000;
//            toolTip.ReshowDelay = 500;
//            toolTip.ShowAlways = true;
//            // 
//            // tableLayoutPanel
//            // 
//            tableLayoutPanel.BackColor = Color.FromArgb(245, 245, 245);
//            tableLayoutPanel.ColumnCount = 2;
//            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
//            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
//            tableLayoutPanel.Controls.Add(lblPart, 0, 0);
//            tableLayoutPanel.Controls.Add(cmbPart, 1, 0);
//            tableLayoutPanel.Controls.Add(lblSupplier, 0, 1);
//            tableLayoutPanel.Controls.Add(cmbSupplier, 1, 1);
//            tableLayoutPanel.Controls.Add(lblDate, 0, 2);
//            tableLayoutPanel.Controls.Add(dtpDate, 1, 2);
//            tableLayoutPanel.Controls.Add(lblQuantity, 0, 3);
//            tableLayoutPanel.Controls.Add(txtQuantity, 1, 3);
//            tableLayoutPanel.Controls.Add(lblUnitPrice, 0, 4);
//            tableLayoutPanel.Controls.Add(txtUnitPrice, 1, 4);
//            tableLayoutPanel.Controls.Add(lblMarkup, 0, 5);
//            tableLayoutPanel.Controls.Add(txtMarkup, 1, 5);
//            tableLayoutPanel.Controls.Add(lblStatus, 0, 6);
//            tableLayoutPanel.Controls.Add(cmbStatus, 1, 6);
//            tableLayoutPanel.Controls.Add(lblOperation, 0, 7);
//            tableLayoutPanel.Controls.Add(cmbOperation, 1, 7);
//            tableLayoutPanel.Controls.Add(lblStock, 0, 8);
//            tableLayoutPanel.Controls.Add(cmbStock, 1, 8);
//            tableLayoutPanel.Controls.Add(lblInvoiceNumber, 0, 9);
//            tableLayoutPanel.Controls.Add(txtInvoiceNumber, 1, 9);
//            tableLayoutPanel.Controls.Add(lblUser, 0, 10);
//            tableLayoutPanel.Controls.Add(cmbUser, 1, 10);
//            tableLayoutPanel.Controls.Add(lblPaidAmount, 0, 11);
//            tableLayoutPanel.Controls.Add(txtPaidAmount, 1, 11);
//            tableLayoutPanel.Controls.Add(lblBatch, 0, 12);
//            tableLayoutPanel.Controls.Add(txtBatch, 1, 12);
//            tableLayoutPanel.Controls.Add(lblError, 0, 13);
//            tableLayoutPanel.Controls.Add(btnCancel, 0, 14);
//            tableLayoutPanel.Controls.Add(btnSave, 1, 14);
//            tableLayoutPanel.Dock = DockStyle.Fill;
//            tableLayoutPanel.Location = new Point(0, 0);
//            tableLayoutPanel.Name = "tableLayoutPanel";
//            tableLayoutPanel.Padding = new Padding(16);
//            tableLayoutPanel.RowCount = 15;
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
//            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
//            tableLayoutPanel.Size = new Size(434, 669);
//            tableLayoutPanel.TabIndex = 0;
//            // 
//            // lblPart
//            // 
//            lblPart.AutoSize = true;
//            lblPart.Font = new Font("Segoe UI", 10F);
//            lblPart.ForeColor = Color.FromArgb(33, 37, 41);
//            lblPart.Location = new Point(19, 16);
//            lblPart.Name = "lblPart";
//            lblPart.Size = new Size(61, 23);
//            lblPart.TabIndex = 0;
//            lblPart.Text = "Деталь";
//            // 
//            // cmbPart
//            // 
//            cmbPart.DropDownStyle = ComboBoxStyle.DropDownList;
//            cmbPart.Font = new Font("Segoe UI", 10F);
//            cmbPart.FormattingEnabled = true;
//            cmbPart.Location = new Point(139, 19);
//            cmbPart.Name = "cmbPart";
//            cmbPart.Size = new Size(276, 31);
//            cmbPart.TabIndex = 1;
//            // 
//            // lblSupplier
//            // 
//            lblSupplier.AutoSize = true;
//            lblSupplier.Font = new Font("Segoe UI", 10F);
//            lblSupplier.ForeColor = Color.FromArgb(33, 37, 41);
//            lblSupplier.Location = new Point(19, 56);
//            lblSupplier.Name = "lblSupplier";
//            lblSupplier.Size = new Size(91, 23);
//            lblSupplier.TabIndex = 2;
//            lblSupplier.Text = "Поставщик";
//            // 
//            // cmbSupplier
//            // 
//            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
//            cmbSupplier.Font = new Font("Segoe UI", 10F);
//            cmbSupplier.FormattingEnabled = true;
//            cmbSupplier.Location = new Point(139, 59);
//            cmbSupplier.Name = "cmbSupplier";
//            cmbSupplier.Size = new Size(276, 31);
//            cmbSupplier.TabIndex = 3;
//            // 
//            // lblDate
//            // 
//            lblDate.AutoSize = true;
//            lblDate.Font = new Font("Segoe UI", 10F);
//            lblDate.ForeColor = Color.FromArgb(33, 37, 41);
//            lblDate.Location = new Point(19, 96);
//            lblDate.Name = "lblDate";
//            lblDate.Size = new Size(45, 23);
//            lblDate.TabIndex = 4;
//            lblDate.Text = "Дата";
//            // 
//            // dtpDate
//            // 
//            dtpDate.Font = new Font("Segoe UI", 10F);
//            dtpDate.Location = new Point(139, 99);
//            dtpDate.Name = "dtpDate";
//            dtpDate.Size = new Size(276, 30);
//            dtpDate.TabIndex = 5;
//            // 
//            // lblQuantity
//            // 
//            lblQuantity.AutoSize = true;
//            lblQuantity.Font = new Font("Segoe UI", 10F);
//            lblQuantity.ForeColor = Color.FromArgb(33, 37, 41);
//            lblQuantity.Location = new Point(19, 136);
//            lblQuantity.Name = "lblQuantity";
//            lblQuantity.Size = new Size(82, 23);
//            lblQuantity.TabIndex = 6;
//            lblQuantity.Text = "Количество";
//            // 
//            // txtQuantity
//            // 
//            txtQuantity.Font = new Font("Segoe UI", 10F);
//            txtQuantity.Location = new Point(139, 139);
//            txtQuantity.Name = "txtQuantity";
//            txtQuantity.Size = new Size(276, 30);
//            txtQuantity.TabIndex = 7;
//            // 
//            // lblUnitPrice
//            // 
//            lblUnitPrice.AutoSize = true;
//            lblUnitPrice.Font = new Font("Segoe UI", 10F);
//            lblUnitPrice.ForeColor = Color.FromArgb(33, 37, 41);
//            lblUnitPrice.Location = new Point(19, 176);
//            lblUnitPrice.Name = "lblUnitPrice";
//            lblUnitPrice.Size = new Size(114, 23);
//            lblUnitPrice.TabIndex = 8;
//            lblUnitPrice.Text = "Цена за единицу";
//            // 
//            // txtUnitPrice
//            // 
//            txtUnitPrice.Font = new Font("Segoe UI", 10F);
//            txtUnitPrice.Location = new Point(139, 179);
//            txtUnitPrice.Name = "txtUnitPrice";
//            txtUnitPrice.Size = new Size(276, 30);
//            txtUnitPrice.TabIndex = 9;
//            // 
//            // lblMarkup
//            // 
//            lblMarkup.AutoSize = true;
//            lblMarkup.Font = new Font("Segoe UI", 10F);
//            lblMarkup.ForeColor = Color.FromArgb(33, 37, 41);
//            lblMarkup.Location = new Point(19, 216);
//            lblMarkup.Name = "lblMarkup";
//            lblMarkup.Size = new Size(69, 23);
//            lblMarkup.TabIndex = 10;
//            lblMarkup.Text = "Наценка";
//            // 
//            // txtMarkup
//            // 
//            txtMarkup.Font = new Font("Segoe UI", 10F);
//            txtMarkup.Location = new Point(139, 219);
//            txtMarkup.Name = "txtMarkup";
//            txtMarkup.Size = new Size(276, 30);
//            txtMarkup.TabIndex = 11;
//            // 
//            // lblStatus
//            // 
//            lblStatus.AutoSize = true;
//            lblStatus.Font = new Font("Segoe UI", 10F);
//            lblStatus.ForeColor = Color.FromArgb(33, 37, 41);
//            lblStatus.Location = new Point(19, 256);
//            lblStatus.Name = "lblStatus";
//            lblStatus.Size = new Size(61, 23);
//            lblStatus.TabIndex = 12;
//            lblStatus.Text = "Статус";
//            // 
//            // cmbStatus
//            // 
//            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
//            cmbStatus.Font = new Font("Segoe UI", 10F);
//            cmbStatus.FormattingEnabled = true;
//            cmbStatus.Location = new Point(139, 259);
//            cmbStatus.Name = "cmbStatus";
//            cmbStatus.Size = new Size(276, 31);
//            cmbStatus.TabIndex = 13;
//            // 
//            // lblOperation
//            // 
//            lblOperation.AutoSize = true;
//            lblOperation.Font = new Font("Segoe UI", 10F);
//            lblOperation.ForeColor = Color.FromArgb(33, 37, 41);
//            lblOperation.Location = new Point(19, 296);
//            lblOperation.Name = "lblOperation";
//            lblOperation.Size = new Size(77, 23);
//            lblOperation.TabIndex = 14;
//            lblOperation.Text = "Операция";
//            // 
//            // cmbOperation
//            // 
//            cmbOperation.DropDownStyle = ComboBoxStyle.DropDownList;
//            cmbOperation.Font = new Font("Segoe UI", 10F);
//            cmbOperation.FormattingEnabled = true;
//            cmbOperation.Location = new Point(139, 299);
//            cmbOperation.Name = "cmbOperation";
//            cmbOperation.Size = new Size(276, 31);
//            cmbOperation.TabIndex = 15;
//            // 
//            // lblStock
//            // 
//            lblStock.AutoSize = true;
//            lblStock.Font = new Font("Segoe UI", 10F);
//            lblStock.ForeColor = Color.FromArgb(33, 37, 41);
//            lblStock.Location = new Point(19, 336);
//            lblStock.Name = "lblStock";
//            lblStock.Size = new Size(55, 23);
//            lblStock.TabIndex = 16;
//            lblStock.Text = "Склад";
//            // 
//            // cmbStock
//            // 
//            cmbStock.DropDownStyle = ComboBoxStyle.DropDownList;
//            cmbStock.Font = new Font("Segoe UI", 10F);
//            cmbStock.FormattingEnabled = true;
//            cmbStock.Location = new Point(139, 339);
//            cmbStock.Name = "cmbStock";
//            cmbStock.Size = new Size(276, 31);
//            cmbStock.TabIndex = 17;
//            // 
//            // lblInvoiceNumber
//            // 
//            lblInvoiceNumber.AutoSize = true;
//            lblInvoiceNumber.Font = new Font("Segoe UI", 10F);
//            lblInvoiceNumber.ForeColor = Color.FromArgb(33, 37, 41);
//            lblInvoiceNumber.Location = new Point(19, 376);
//            lblInvoiceNumber.Name = "lblInvoiceNumber";
//            lblInvoiceNumber.Size = new Size(88, 23);
//            lblInvoiceNumber.TabIndex = 18;
//            lblInvoiceNumber.Text = "Номер счета";
//            // 
//            // txtInvoiceNumber
//            // 
//            txtInvoiceNumber.Font = new Font("Segoe UI", 10F);
//            txtInvoiceNumber.Location = new Point(139, 379);
//            txtInvoiceNumber.Name = "txtInvoiceNumber";
//            txtInvoiceNumber.Size = new Size(276, 30);
//            txtInvoiceNumber.TabIndex = 19;
//            // 
//            // lblUser
//            // 
//            lblUser.AutoSize = true;
//            lblUser.Font = new Font("Segoe UI", 10F);
//            lblUser.ForeColor = Color.FromArgb(33, 37, 41);
//            lblUser.Location = new Point(19, 416);
//            lblUser.Name = "lblUser";
//            lblUser.Size = new Size(88, 23);
//            lblUser.TabIndex = 20;
//            lblUser.Text = "Пользователь";
//            // 
//            // cmbUser
//            // 
//            cmbUser.DropDownStyle = ComboBoxStyle.DropDownList;
//            cmbUser.Font = new Font("Segoe UI", 10F);
//            cmbUser.FormattingEnabled = true;
//            cmbUser.Location = new Point(139, 419);
//            cmbUser.Name = "cmbUser";
//            cmbUser.Size = new Size(276, 31);
//            cmbUser.TabIndex = 21;
//            // 
//            // lblPaidAmount
//            // 
//            lblPaidAmount.AutoSize = true;
//            lblPaidAmount.Font = new Font("Segoe UI", 10F);
//            lblPaidAmount.ForeColor = Color.FromArgb(33, 37, 41);
//            lblPaidAmount.Location = new Point(19, 456);
//            lblPaidAmount.Name = "lblPaidAmount";
//            lblPaidAmount.Size = new Size(114, 23);
//            lblPaidAmount.TabIndex = 22;
//            lblPaidAmount.Text = "Оплаченная сумма";
//            // 
//            // txtPaidAmount
//            // 
//            txtPaidAmount.Font = new Font("Segoe UI", 10F);
//            txtPaidAmount.Location = new Point(139, 459);
//            txtPaidAmount.Name = "txtPaidAmount";
//            txtPaidAmount.Size = new Size(276, 30);
//            txtPaidAmount.TabIndex = 23;
//            // 
//            // lblBatch
//            // 
//            lblBatch.AutoSize = true;
//            lblBatch.Font = new Font("Segoe UI", 10F);
//            lblBatch.ForeColor = Color.FromArgb(33, 37, 41);
//            lblBatch.Location = new Point(19, 496);
//            lblBatch.Name = "lblBatch";
//            lblBatch.Size = new Size(88, 23);
//            lblBatch.TabIndex = 24;
//            lblBatch.Text = "ID партии";
//            // 
//            // txtBatch
//            // 
//            txtBatch.Font = new Font("Segoe UI", 10F);
//            txtBatch.Location = new Point(139, 499);
//            txtBatch.Name = "txtBatch";
//            txtBatch.Size = new Size(276, 30);
//            txtBatch.TabIndex = 25;
//            // 
//            // lblError
//            // 
//            lblError.AutoSize = true;
//            lblError.Font = new Font("Segoe UI", 10F);
//            lblError.ForeColor = Color.FromArgb(220, 53, 69);
//            lblError.Location = new Point(19, 545);
//            lblError.Name = "lblError";
//            lblError.Size = new Size(0, 23);
//            lblError.TabIndex = 26;
//            lblError.Visible = false;
//            // 
//            // btnCancel
//            // 
//            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
//            btnCancel.DialogResult = DialogResult.Cancel;
//            btnCancel.FlatAppearance.BorderSize = 0;
//            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
//            btnCancel.FlatStyle = FlatStyle.Flat;
//            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
//            btnCancel.ForeColor = Color.White;
//            btnCancel.Location = new Point(19, 619);
//            btnCancel.Name = "btnCancel";
//            btnCancel.Size = new Size(114, 34);
//            btnCancel.TabIndex = 27;
//            btnCancel.Text = "Отмена";
//            btnCancel.UseVisualStyleBackColor = false;
//            btnCancel.Click += BtnCancel_Click;
//            // 
//            // btnSave
//            // 
//            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
//            btnSave.BackColor = Color.FromArgb(40, 167, 69);
//            btnSave.FlatAppearance.BorderSize = 0;
//            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
//            btnSave.FlatStyle = FlatStyle.Flat;
//            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
//            btnSave.ForeColor = Color.White;
//            btnSave.Location = new Point(295, 619);
//            btnSave.Name = "btnSave";
//            btnSave.Size = new Size(120, 34);
//            btnSave.TabIndex = 28;
//            btnSave.Text = "Сохранить";
//            btnSave.UseVisualStyleBackColor = false;
//            btnSave.Click += BtnSave_Click;
//            // 
//            // PartsIncomeDialog
//            // 
//            AcceptButton = btnSave;
//            AutoScaleDimensions = new SizeF(8F, 20F);
//            AutoScaleMode = AutoScaleMode.Font;
//            BackColor = Color.FromArgb(245, 245, 245);
//            CancelButton = btnCancel;
//            ClientSize = new Size(434, 669);
//            Controls.Add(tableLayoutPanel);
//            FormBorderStyle = FormBorderStyle.FixedDialog;
//            MaximizeBox = false;
//            MinimizeBox = false;
//            Name = "PartsIncomeDialog";
//            StartPosition = FormStartPosition.CenterParent;
//            Text = "Поступление деталей";
//            tableLayoutPanel.ResumeLayout(false);
//            tableLayoutPanel.PerformLayout();
//            ResumeLayout(false);
//        }

//        private TableLayoutPanel tableLayoutPanel;
//        private Label lblPart;
//        private ComboBox cmbPart;
//        private Label lblSupplier;
//        private ComboBox cmbSupplier;
//        private Label lblDate;
//        private DateTimePicker dtpDate;
//        private Label lblQuantity;
//        private TextBox txtQuantity;
//        private Label lblUnitPrice;
//        private TextBox txtUnitPrice;
//        private Label lblMarkup;
//        private TextBox txtMarkup;
//        private Label lblStatus;
//        private ComboBox cmbStatus;
//        private Label lblOperation;
//        private ComboBox cmbOperation;
//        private Label lblStock;
//        private ComboBox cmbStock;
//        private Label lblInvoiceNumber;
//        private TextBox txtInvoiceNumber;
//        private Label lblUser;
//        private ComboBox cmbUser;
//        private Label lblPaidAmount;
//        private TextBox txtPaidAmount;
//        private Label lblBatch;
//        private TextBox txtBatch;
//        private Label lblError;
//        private Button btnCancel;
//        private Button btnSave;
//        private ToolTip toolTip;
//    }
//}
namespace AvtoServis.Forms
{
    partial class PartExpenseEditForm
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
            separator = new Label();
            lblPartID = new Label();
            cmbPartID = new ComboBox();
            lblCustomerID = new Label();
            cmbCustomerID = new ComboBox();
            lblCustomerInfo = new Label();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            lblUnitPrice = new Label();
            txtUnitPrice = new TextBox();
            lblPaidAmount = new Label();
            txtPaidAmount = new TextBox();
            lblStatusID = new Label();
            cmbFinanceStatusID = new ComboBox();
            _messagePanel = new Panel();
            _messageLabel = new Label();
            titleLabel = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            toolTip = new ToolTip(components);
            errorTimer = new System.Windows.Forms.Timer(components);
            tableLayoutPanel.SuspendLayout();
            _messagePanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.26984F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58.73016F));
            tableLayoutPanel.Controls.Add(separator, 0, 1);
            tableLayoutPanel.Controls.Add(lblPartID, 0, 2);
            tableLayoutPanel.Controls.Add(cmbPartID, 1, 2);
            tableLayoutPanel.Controls.Add(lblCustomerID, 0, 3);
            tableLayoutPanel.Controls.Add(cmbCustomerID, 1, 3);
            tableLayoutPanel.Controls.Add(lblCustomerInfo, 0, 4);
            tableLayoutPanel.Controls.Add(lblDate, 0, 5);
            tableLayoutPanel.Controls.Add(dtpDate, 1, 5);
            tableLayoutPanel.Controls.Add(lblQuantity, 0, 6);
            tableLayoutPanel.Controls.Add(txtQuantity, 1, 6);
            tableLayoutPanel.Controls.Add(lblUnitPrice, 0, 7);
            tableLayoutPanel.Controls.Add(txtUnitPrice, 1, 7);
            tableLayoutPanel.Controls.Add(lblPaidAmount, 0, 8);
            tableLayoutPanel.Controls.Add(txtPaidAmount, 1, 8);
            tableLayoutPanel.Controls.Add(lblStatusID, 0, 9);
            tableLayoutPanel.Controls.Add(cmbFinanceStatusID, 1, 9);
            tableLayoutPanel.Controls.Add(_messagePanel, 0, 11);
            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(btnSave, 1, 12);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 12);
            tableLayoutPanel.Location = new Point(3, -1);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 13;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 9F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel.Size = new Size(524, 555);
            tableLayoutPanel.TabIndex = 0;
            // 
            // separator
            // 
            separator.AccessibleDescription = "Разделительная линия";
            separator.BackColor = Color.FromArgb(108, 117, 125);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            separator.Location = new Point(19, 51);
            separator.Name = "separator";
            separator.Size = new Size(486, 2);
            separator.TabIndex = 1;
            // 
            // lblPartID
            // 
            lblPartID.AutoSize = true;
            lblPartID.Font = new Font("Segoe UI", 10F);
            lblPartID.Location = new Point(19, 77);
            lblPartID.Margin = new Padding(3, 24, 3, 0);
            lblPartID.Name = "lblPartID";
            lblPartID.Size = new Size(65, 23);
            lblPartID.TabIndex = 2;
            lblPartID.Text = "Деталь";
            // 
            // cmbPartID
            // 
            cmbPartID.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPartID.Enabled = false;
            cmbPartID.Font = new Font("Segoe UI", 10F);
            cmbPartID.Location = new Point(222, 77);
            cmbPartID.Margin = new Padding(3, 24, 3, 3);
            cmbPartID.Name = "cmbPartID";
            cmbPartID.Size = new Size(283, 31);
            cmbPartID.TabIndex = 3;
            toolTip.SetToolTip(cmbPartID, "Выберите деталь");
            // 
            // lblCustomerID
            // 
            lblCustomerID.AutoSize = true;
            lblCustomerID.Font = new Font("Segoe UI", 10F);
            lblCustomerID.Location = new Point(19, 119);
            lblCustomerID.Name = "lblCustomerID";
            lblCustomerID.Size = new Size(65, 23);
            lblCustomerID.TabIndex = 4;
            lblCustomerID.Text = "Клиент";
            // 
            // cmbCustomerID
            // 
            cmbCustomerID.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCustomerID.Font = new Font("Segoe UI", 10F);
            cmbCustomerID.Location = new Point(222, 122);
            cmbCustomerID.Name = "cmbCustomerID";
            cmbCustomerID.Size = new Size(283, 31);
            cmbCustomerID.TabIndex = 5;
            toolTip.SetToolTip(cmbCustomerID, "Выберите клиента");
            // 
            // lblCustomerInfo
            // 
            lblCustomerInfo.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblCustomerInfo, 2);
            lblCustomerInfo.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblCustomerInfo.Location = new Point(19, 158);
            lblCustomerInfo.Name = "lblCustomerInfo";
            lblCustomerInfo.Size = new Size(163, 23);
            lblCustomerInfo.TabIndex = 6;
            lblCustomerInfo.Text = "Клиент не выбран";
            toolTip.SetToolTip(lblCustomerInfo, "Информация о текущем клиенте");
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 10F);
            lblDate.Location = new Point(19, 198);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(47, 23);
            lblDate.TabIndex = 7;
            lblDate.Text = "Дата";
            // 
            // dtpDate
            // 
            dtpDate.Font = new Font("Segoe UI", 10F);
            dtpDate.Location = new Point(222, 201);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(283, 30);
            dtpDate.TabIndex = 8;
            dtpDate.ValueChanged += dtpDate_ValueChanged;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 10F);
            lblQuantity.Location = new Point(19, 238);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(102, 23);
            lblQuantity.TabIndex = 9;
            lblQuantity.Text = "Количество";
            // 
            // txtQuantity
            // 
            txtQuantity.Font = new Font("Segoe UI", 10F);
            txtQuantity.Location = new Point(222, 241);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(283, 30);
            txtQuantity.TabIndex = 10;
            toolTip.SetToolTip(txtQuantity, "Введите количество деталей");
            txtQuantity.TextChanged += txtQuantity_TextChanged;
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Font = new Font("Segoe UI", 10F);
            lblUnitPrice.Location = new Point(19, 278);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(144, 23);
            lblUnitPrice.TabIndex = 11;
            lblUnitPrice.Text = "Цена за единицу";
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.Font = new Font("Segoe UI", 10F);
            txtUnitPrice.Location = new Point(222, 281);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new Size(283, 30);
            txtUnitPrice.TabIndex = 12;
            toolTip.SetToolTip(txtUnitPrice, "Введите цену за единицу");
            txtUnitPrice.TextChanged += txtUnitPrice_TextChanged;
            // 
            // lblPaidAmount
            // 
            lblPaidAmount.AutoSize = true;
            lblPaidAmount.Font = new Font("Segoe UI", 10F);
            lblPaidAmount.Location = new Point(19, 315);
            lblPaidAmount.Name = "lblPaidAmount";
            lblPaidAmount.Size = new Size(162, 23);
            lblPaidAmount.TabIndex = 13;
            lblPaidAmount.Text = "Оплаченная сумма";
            // 
            // txtPaidAmount
            // 
            txtPaidAmount.Font = new Font("Segoe UI", 10F);
            txtPaidAmount.Location = new Point(222, 318);
            txtPaidAmount.Name = "txtPaidAmount";
            txtPaidAmount.Size = new Size(283, 30);
            txtPaidAmount.TabIndex = 14;
            toolTip.SetToolTip(txtPaidAmount, "Введите оплаченную сумму");
            txtPaidAmount.TextChanged += txtPaidAmount_TextChanged;
            // 
            // lblStatusID
            // 
            lblStatusID.AutoSize = true;
            lblStatusID.Font = new Font("Segoe UI", 10F);
            lblStatusID.Location = new Point(19, 356);
            lblStatusID.Name = "lblStatusID";
            lblStatusID.Size = new Size(60, 23);
            lblStatusID.TabIndex = 15;
            lblStatusID.Text = "Статус";
            // 
            // cmbFinanceStatusID
            // 
            cmbFinanceStatusID.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFinanceStatusID.Font = new Font("Segoe UI", 10F);
            cmbFinanceStatusID.Location = new Point(222, 359);
            cmbFinanceStatusID.Name = "cmbFinanceStatusID";
            cmbFinanceStatusID.Size = new Size(283, 31);
            cmbFinanceStatusID.TabIndex = 16;
            toolTip.SetToolTip(cmbFinanceStatusID, "Выберите статус");
            cmbFinanceStatusID.SelectedIndexChanged += cmbFinanceStatusID_SelectedIndexChanged;
            // 
            // _messagePanel
            // 
            _messagePanel.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(_messagePanel, 2);
            _messagePanel.Controls.Add(_messageLabel);
            _messagePanel.Location = new Point(19, 412);
            _messagePanel.Name = "_messagePanel";
            _messagePanel.Size = new Size(486, 70);
            _messagePanel.TabIndex = 19;
            // 
            // _messageLabel
            // 
            _messageLabel.Font = new Font("Segoe UI", 11F);
            _messageLabel.Location = new Point(10, 10);
            _messageLabel.Name = "_messageLabel";
            _messageLabel.Size = new Size(466, 80);
            _messageLabel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AccessibleDescription = "Заголовок формы редактирования";
            titleLabel.AccessibleName = "Редактировать расход";
            titleLabel.Anchor = AnchorStyles.Left;
            titleLabel.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(titleLabel, 2);
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 17);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(279, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Редактировать расход";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(372, 495);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(133, 41);
            btnSave.TabIndex = 21;
            btnSave.Text = "Сохранить";
            toolTip.SetToolTip(btnSave, "Сохранить изменения");
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(19, 495);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(133, 41);
            btnCancel.TabIndex = 20;
            btnCancel.Text = "Отмена";
            toolTip.SetToolTip(btnCancel, "Отменить изменения");
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
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
            // PartExpenseEditForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(524, 549);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(450, 460);
            Name = "PartExpenseEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактирование продажи";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            _messagePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label separator;
        private System.Windows.Forms.Label lblPartID;
        private System.Windows.Forms.ComboBox cmbPartID;
        private System.Windows.Forms.Label lblCustomerID;
        private System.Windows.Forms.ComboBox cmbCustomerID;
        private System.Windows.Forms.Label lblCustomerInfo;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Label lblPaidAmount;
        private System.Windows.Forms.TextBox txtPaidAmount;
        private System.Windows.Forms.Label lblStatusID;
        private System.Windows.Forms.ComboBox cmbFinanceStatusID;
        private System.Windows.Forms.Panel _messagePanel;
        private System.Windows.Forms.Label _messageLabel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer errorTimer;
    }
}
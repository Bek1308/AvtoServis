namespace AvtoServis.Forms.Modals.PartsIncome
{
    partial class BatchConfirmationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtBatchName = new TextBox();
            txtTotalPaidAmount = new TextBox();
            lblBatchName = new Label();
            lblTotalPaidAmount = new Label();
            lblTotalPaidSum = new Label();
            lblTotalCost = new Label();
            btnConfirm = new Button();
            btnCancel = new Button();
            toolTip = new ToolTip(components);
            tableLayoutPanel = new TableLayoutPanel();
            panelError = new Panel();
            lblError = new Label();
            errorTimer = new System.Windows.Forms.Timer(components);
            tableLayoutPanel.SuspendLayout();
            panelError.SuspendLayout();
            SuspendLayout();
            // 
            // txtBatchName
            // 
            txtBatchName.BorderStyle = BorderStyle.FixedSingle;
            txtBatchName.Font = new Font("Segoe UI", 10F);
            txtBatchName.Location = new Point(279, 19);
            txtBatchName.Name = "txtBatchName";
            txtBatchName.Size = new Size(227, 30);
            txtBatchName.TabIndex = 0;
            toolTip.SetToolTip(txtBatchName, "Введите название партии");
            txtBatchName.TextChanged += txtBatchName_TextChanged;
            // 
            // txtTotalPaidAmount
            // 
            txtTotalPaidAmount.BorderStyle = BorderStyle.FixedSingle;
            txtTotalPaidAmount.Font = new Font("Segoe UI", 10F);
            txtTotalPaidAmount.Location = new Point(279, 59);
            txtTotalPaidAmount.Name = "txtTotalPaidAmount";
            txtTotalPaidAmount.Size = new Size(227, 30);
            txtTotalPaidAmount.TabIndex = 1;
            toolTip.SetToolTip(txtTotalPaidAmount, "Введите общую оплаченную сумму");
            txtTotalPaidAmount.TextChanged += txtTotalPaidAmount_TextChanged;
            // 
            // lblBatchName
            // 
            lblBatchName.AutoSize = true;
            lblBatchName.Font = new Font("Segoe UI", 10F);
            lblBatchName.ForeColor = Color.FromArgb(33, 37, 41);
            lblBatchName.Location = new Point(19, 16);
            lblBatchName.Name = "lblBatchName";
            lblBatchName.Size = new Size(147, 23);
            lblBatchName.TabIndex = 2;
            lblBatchName.Text = "Название партии";
            lblBatchName.TextAlign = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblBatchName, "Введите название партии");
            // 
            // lblTotalPaidAmount
            // 
            lblTotalPaidAmount.AutoSize = true;
            lblTotalPaidAmount.Font = new Font("Segoe UI", 10F);
            lblTotalPaidAmount.ForeColor = Color.FromArgb(33, 37, 41);
            lblTotalPaidAmount.Location = new Point(19, 56);
            lblTotalPaidAmount.Name = "lblTotalPaidAmount";
            lblTotalPaidAmount.Size = new Size(219, 23);
            lblTotalPaidAmount.TabIndex = 3;
            lblTotalPaidAmount.Text = "Общая оплаченная сумма";
            lblTotalPaidAmount.TextAlign = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblTotalPaidAmount, "Введите общую оплаченную сумму");
            // 
            // lblTotalPaidSum
            // 
            lblTotalPaidSum.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblTotalPaidSum, 2);
            lblTotalPaidSum.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalPaidSum.ForeColor = Color.FromArgb(33, 37, 41);
            lblTotalPaidSum.Location = new Point(19, 136);
            lblTotalPaidSum.Name = "lblTotalPaidSum";
            lblTotalPaidSum.Size = new Size(318, 23);
            lblTotalPaidSum.TabIndex = 4;
            lblTotalPaidSum.Text = "Общая оплаченная сумма деталей: 0";
            lblTotalPaidSum.TextAlign = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblTotalPaidSum, "Общая сумма оплаченных деталей");
            // 
            // lblTotalCost
            // 
            lblTotalCost.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblTotalCost, 2);
            lblTotalCost.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalCost.ForeColor = Color.FromArgb(33, 37, 41);
            lblTotalCost.Location = new Point(19, 96);
            lblTotalCost.Name = "lblTotalCost";
            lblTotalCost.Size = new Size(178, 23);
            lblTotalCost.TabIndex = 5;
            lblTotalCost.Text = "Общая стоимость: 0";
            lblTotalCost.TextAlign = ContentAlignment.MiddleLeft;
            toolTip.SetToolTip(lblTotalCost, "Общая стоимость всех деталей");
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Right;
            btnConfirm.BackColor = Color.FromArgb(40, 167, 69);
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(373, 239);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(133, 36);
            btnConfirm.TabIndex = 6;
            btnConfirm.Text = "Продолжить";
            toolTip.SetToolTip(btnConfirm, "Сохранить данные в базу");
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += BtnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Left;
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(19, 239);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(121, 36);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Отмена";
            toolTip.SetToolTip(btnCancel, "Отменить сохранение");
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
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52.73834F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47.26166F));
            tableLayoutPanel.Controls.Add(lblBatchName, 0, 0);
            tableLayoutPanel.Controls.Add(txtBatchName, 1, 0);
            tableLayoutPanel.Controls.Add(lblTotalPaidAmount, 0, 1);
            tableLayoutPanel.Controls.Add(txtTotalPaidAmount, 1, 1);
            tableLayoutPanel.Controls.Add(lblTotalCost, 0, 2);
            tableLayoutPanel.Controls.Add(lblTotalPaidSum, 0, 3);
            tableLayoutPanel.Controls.Add(panelError, 0, 4);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 5);
            tableLayoutPanel.Controls.Add(btnConfirm, 1, 5);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle());
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.Size = new Size(525, 281);
            tableLayoutPanel.TabIndex = 8;
            // 
            // panelError
            // 
            panelError.AutoSize = true;
            panelError.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelError.BackColor = Color.FromArgb(245, 255, 245);
            panelError.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(panelError, 2);
            panelError.Controls.Add(lblError);
            panelError.Location = new Point(19, 179);
            panelError.MinimumSize = new Size(486, 0);
            panelError.Name = "panelError";
            panelError.Padding = new Padding(10);
            panelError.Size = new Size(486, 45);
            panelError.TabIndex = 9;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.BackColor = Color.Transparent;
            lblError.Font = new Font("Segoe UI", 10F);
            lblError.ForeColor = Color.FromArgb(40, 167, 69);
            lblError.Location = new Point(10, 10);
            lblError.MaximumSize = new Size(376, 0);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 23);
            lblError.TabIndex = 0;
            lblError.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // errorTimer
            // 
            errorTimer.Interval = 3000;
            // 
            // BatchConfirmationForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(525, 281);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(450, 320);
            Name = "BatchConfirmationForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Подтверждение партии";
            Load += BatchConfirmationForm_Load;
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            panelError.ResumeLayout(false);
            panelError.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox txtBatchName;
        private System.Windows.Forms.TextBox txtTotalPaidAmount;
        private System.Windows.Forms.Label lblBatchName;
        private System.Windows.Forms.Label lblTotalPaidAmount;
        private System.Windows.Forms.Label lblTotalPaidSum;
        private System.Windows.Forms.Label lblTotalCost;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panelError;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Timer errorTimer;

        private void txtBatchName_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void txtTotalPaidAmount_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode) ValidateInputs();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BatchConfirmationForm_Load(object sender, EventArgs e)
        {
            AdjustFormHeight();
        }
    }
}
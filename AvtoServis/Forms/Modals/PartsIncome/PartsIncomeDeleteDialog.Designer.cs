namespace AvtoServis.Forms.Modals.PartsIncome
{
    partial class PartsIncomeDeleteDialog
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
            lblTitle = new Label();
            separator = new Label();
            lblPrompt = new Label();
            cmbReason = new ComboBox();
            btnCancel = new Button();
            btnConfirm = new Button();
            panelError = new Panel();
            lblError = new Label();
            toolTip = new ToolTip(components);
            tableLayoutPanel.SuspendLayout();
            panelError.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 173F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanel.Controls.Add(separator, 0, 1);
            tableLayoutPanel.Controls.Add(lblPrompt, 0, 3);
            tableLayoutPanel.Controls.Add(cmbReason, 1, 3);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 4);
            tableLayoutPanel.Controls.Add(btnConfirm, 1, 4);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 61F));
            tableLayoutPanel.Size = new Size(447, 184);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblTitle, 2);
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle.Location = new Point(19, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(384, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Вы хотите удалить запись?";
            // 
            // separator
            // 
            separator.BackColor = Color.FromArgb(108, 117, 125);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            separator.Location = new Point(19, 51);
            separator.Name = "separator";
            separator.Size = new Size(402, 2);
            separator.TabIndex = 1;
            // 
            // lblPrompt
            // 
            lblPrompt.AutoSize = true;
            lblPrompt.Font = new Font("Segoe UI", 10F);
            lblPrompt.ForeColor = Color.FromArgb(33, 37, 41);
            lblPrompt.Location = new Point(19, 73);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(159, 23);
            lblPrompt.TabIndex = 2;
            lblPrompt.Text = "Причина удаления";
            // 
            // cmbReason
            // 
            cmbReason.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReason.Font = new Font("Segoe UI", 10F);
            cmbReason.Items.AddRange(new object[] { 
                "Ошибка в данных",
                "Дубликат записи",
                "Возврат товара",
                "Другая причина"
            });
            cmbReason.Location = new Point(192, 76);
            cmbReason.Name = "cmbReason";
            cmbReason.Size = new Size(236, 31);
            cmbReason.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(25, 118, 210);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(19, 126);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 36);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Нет";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConfirm.BackColor = Color.FromArgb(220, 53, 69);
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 100, 100);
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(296, 126);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(132, 36);
            btnConfirm.TabIndex = 6;
            btnConfirm.Text = "Да";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += BtnConfirm_Click;
            // 
            // panelError
            // 
            panelError.BackColor = Color.FromArgb(255, 245, 245);
            panelError.BorderStyle = BorderStyle.FixedSingle;
            panelError.Controls.Add(lblError);
            panelError.Location = new Point(0, 0);
            panelError.Name = "panelError";
            panelError.Size = new Size(405, 64);
            panelError.TabIndex = 4;
            panelError.Visible = false;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(3, 7);
            lblError.MaximumSize = new Size(402, 0);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 23);
            lblError.TabIndex = 0;
            lblError.TextAlign = ContentAlignment.MiddleLeft;
            lblError.Visible = false;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            // 
            // PartsIncomeDeleteDialog
            // 
            AcceptButton = btnConfirm;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(447, 184);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PartsIncomeDeleteDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Подтверждение удаления";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            panelError.ResumeLayout(false);
            panelError.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblTitle;
        private Label separator;
        private Label lblPrompt;
        private ComboBox cmbReason;
        private Panel panelError;
        private Label lblError;
        private Button btnCancel;
        private Button btnConfirm;
        private ToolTip toolTip;
    }
}
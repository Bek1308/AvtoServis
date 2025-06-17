namespace AvtoServis.Forms.Controls
{
    partial class PartsFilterDialog
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
            tableLayoutPanel = new TableLayoutPanel();
            lblBrand = new Label();
            cmbBrand = new ComboBox();
            lblQuality = new Label();
            cmbQuality = new ComboBox();
            lblError = new Label();
            btnCancel = new Button();
            btnApply = new Button();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblBrand, 0, 0);
            tableLayoutPanel.Controls.Add(cmbBrand, 1, 0);
            tableLayoutPanel.Controls.Add(lblQuality, 0, 1);
            tableLayoutPanel.Controls.Add(cmbQuality, 1, 1);
            tableLayoutPanel.Controls.Add(lblError, 0, 2);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 3);
            tableLayoutPanel.Controls.Add(btnApply, 1, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(14, 12, 14, 12);
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.Size = new Size(380, 141);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 10F);
            lblBrand.ForeColor = Color.FromArgb(33, 37, 41);
            lblBrand.Location = new Point(17, 12);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(51, 19);
            lblBrand.TabIndex = 0;
            lblBrand.Text = "Марка";
            // 
            // cmbBrand
            // 
            cmbBrand.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBrand.Font = new Font("Segoe UI", 10F);
            cmbBrand.FormattingEnabled = true;
            cmbBrand.Location = new Point(105, 14);
            cmbBrand.Margin = new Padding(3, 2, 3, 2);
            cmbBrand.Name = "cmbBrand";
            cmbBrand.Size = new Size(176, 25);
            cmbBrand.TabIndex = 1;
            // 
            // lblQuality
            // 
            lblQuality.AutoSize = true;
            lblQuality.Font = new Font("Segoe UI", 10F);
            lblQuality.ForeColor = Color.FromArgb(33, 37, 41);
            lblQuality.Location = new Point(17, 42);
            lblQuality.Name = "lblQuality";
            lblQuality.Size = new Size(66, 19);
            lblQuality.TabIndex = 2;
            lblQuality.Text = "Качество";
            // 
            // cmbQuality
            // 
            cmbQuality.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQuality.Font = new Font("Segoe UI", 10F);
            cmbQuality.FormattingEnabled = true;
            cmbQuality.Location = new Point(105, 44);
            cmbQuality.Margin = new Padding(3, 2, 3, 2);
            cmbQuality.Name = "cmbQuality";
            cmbQuality.Size = new Size(176, 25);
            cmbQuality.TabIndex = 3;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 10F);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(17, 72);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 19);
            lblError.TabIndex = 4;
            lblError.Visible = false;
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
            btnCancel.Location = new Point(17, 104);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(82, 27);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnApply
            // 
            btnApply.BackColor = Color.FromArgb(40, 167, 69);
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(105, 104);
            btnApply.Margin = new Padding(3, 2, 3, 2);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(105, 27);
            btnApply.TabIndex = 6;
            btnApply.Text = "Применить";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += BtnApply_Click;
            // 
            // PartsFilterDialog
            // 
            AcceptButton = btnApply;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(380, 141);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PartsFilterDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Фильтры деталей";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblBrand;
        private ComboBox cmbBrand;
        private Label lblQuality;
        private ComboBox cmbQuality;
        private Label lblError;
        private Button btnCancel;
        private Button btnApply;
    }
}
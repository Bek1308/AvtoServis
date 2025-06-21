namespace AvtoServis.Forms.Controls
{
    partial class SuppliersFilterDialog
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
            lblName = new Label();
            txtName = new TextBox();
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
            tableLayoutPanel.Controls.Add(lblName, 0, 0);
            tableLayoutPanel.Controls.Add(txtName, 1, 0);
            tableLayoutPanel.Controls.Add(lblError, 0, 1);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 2);
            tableLayoutPanel.Controls.Add(btnApply, 1, 2);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(14, 12, 14, 12);
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.Size = new Size(380, 111);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            lblName.Location = new Point(17, 12);
            lblName.Name = "lblName";
            lblName.Size = new Size(51, 19);
            lblName.TabIndex = 0;
            lblName.Text = "Название";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.Location = new Point(105, 14);
            txtName.Margin = new Padding(3, 2, 3, 2);
            txtName.Name = "txtName";
            txtName.Size = new Size(176, 30);
            txtName.TabIndex = 1;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 10F);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(17, 42);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 19);
            lblError.TabIndex = 2;
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
            btnCancel.Location = new Point(17, 74);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(82, 27);
            btnCancel.TabIndex = 3;
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
            btnApply.Location = new Point(105, 74);
            btnApply.Margin = new Padding(3, 2, 3, 2);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(105, 27);
            btnApply.TabIndex = 4;
            btnApply.Text = "Применить";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += BtnApply_Click;
            // 
            // SuppliersFilterDialog
            // 
            AcceptButton = btnApply;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(380, 111);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SuppliersFilterDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Фильтры поставщиков";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblName;
        private TextBox txtName;
        private Label lblError;
        private Button btnCancel;
        private Button btnApply;
    }
}
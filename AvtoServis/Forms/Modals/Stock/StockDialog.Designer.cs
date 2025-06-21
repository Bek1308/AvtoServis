namespace AvtoServis.Forms.Controls
{
    partial class StockDialog
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            lblName = new Label();
            txtName = new TextBox();
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
            tableLayoutPanel.Controls.Add(panelError, 0, 1);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 2);
            tableLayoutPanel.Controls.Add(btnSave, 1, 2);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.Size = new Size(597, 180);
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
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(219, 132);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 36);
            btnCancel.TabIndex = 2;
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
            btnSave.Location = new Point(401, 132);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 36);
            btnSave.TabIndex = 3;
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
            panelError.Location = new Point(19, 65);
            panelError.Margin = new Padding(3, 4, 3, 4);
            panelError.Name = "panelError";
            panelError.Size = new Size(559, 60);
            panelError.TabIndex = 4;
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
            // StockDialog
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(597, 180);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StockDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Склад";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            panelError.ResumeLayout(false);
            panelError.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblName;
        private TextBox txtName;
        private Panel panelError;
        private Label lblError;
        private Button btnCancel;
        private Button btnSave;
    }
}
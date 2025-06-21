namespace AvtoServis.Forms.Controls
{
    partial class StatusesDialog
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            lblName = new Label();
            txtName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblColor = new Label();
            btnColor = new Button();
            panelError = new Panel();
            lblError = new Label();
            btnCancel = new Button();
            btnSave = new Button();
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
            tableLayoutPanel.Controls.Add(lblDescription, 0, 1);
            tableLayoutPanel.Controls.Add(txtDescription, 1, 1);
            tableLayoutPanel.Controls.Add(lblColor, 0, 2);
            tableLayoutPanel.Controls.Add(btnColor, 1, 2);
            tableLayoutPanel.Controls.Add(panelError, 0, 3);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 4);
            tableLayoutPanel.Controls.Add(btnSave, 1, 4);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.Size = new Size(597, 240);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            lblName.Location = new Point(19, 16);
            lblName.Name = "lblName";
            lblName.Size = new Size(86, 23);
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
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 10F);
            lblDescription.ForeColor = Color.FromArgb(33, 37, 41);
            lblDescription.Location = new Point(19, 56);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(86, 23);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Описание";
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.Location = new Point(219, 59);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(176, 30);
            txtDescription.TabIndex = 3;
            txtDescription.TextChanged += TxtDescription_TextChanged;
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Font = new Font("Segoe UI", 10F);
            lblColor.ForeColor = Color.FromArgb(33, 37, 41);
            lblColor.Location = new Point(19, 96);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(86, 23);
            lblColor.TabIndex = 4;
            lblColor.Text = "Цвет";
            // 
            // btnColor
            // 
            btnColor.BackColor = Color.White;
            btnColor.FlatAppearance.BorderSize = 1;
            btnColor.FlatStyle = FlatStyle.Flat;
            btnColor.Location = new Point(219, 99);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(176, 30);
            btnColor.TabIndex = 5;
            btnColor.UseVisualStyleBackColor = false;
            btnColor.Click += BtnColor_Click;
            // 
            // panelError
            // 
            panelError.BackColor = Color.FromArgb(255, 245, 245);
            panelError.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(panelError, 2);
            panelError.Controls.Add(lblError);
            panelError.Location = new Point(19, 136);
            panelError.Margin = new Padding(3, 4, 3, 4);
            panelError.Name = "panelError";
            panelError.Size = new Size(559, 52);
            panelError.TabIndex = 6;
            panelError.Visible = false;
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
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(19, 199);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 36);
            btnCancel.TabIndex = 7;
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
            btnSave.Location = new Point(219, 199);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 36);
            btnSave.TabIndex = 8;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // StatusesDialog
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(597, 240);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StatusesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Статус";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            panelError.ResumeLayout(false);
            panelError.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblName;
        private TextBox txtName;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblColor;
        private Button btnColor;
        private Panel panelError;
        private Label lblError;
        private Button btnCancel;
        private Button btnSave;
    }
}
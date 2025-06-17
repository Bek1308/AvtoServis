namespace AvtoServis.Forms.Controls
{
    partial class QualityEditDialog
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
            lblQualityName = new Label();
            txtQualityName = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            panelError = new Panel();
            lblError = new Label();
            timerError = new System.Windows.Forms.Timer(components);
            tableLayoutPanel.SuspendLayout();
            panelError.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanel.Controls.Add(lblQualityName, 0, 1);
            tableLayoutPanel.Controls.Add(txtQualityName, 1, 1);
            tableLayoutPanel.Controls.Add(panelError, 0, 2);
            tableLayoutPanel.Controls.Add(btnSave, 0, 3);
            tableLayoutPanel.Controls.Add(btnCancel, 1, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(14, 12, 14, 12);
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.Size = new Size(350, 169);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle.Location = new Point(17, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(96, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Качество";
            // 
            // lblQualityName
            // 
            lblQualityName.AutoSize = true;
            lblQualityName.Font = new Font("Segoe UI", 10F);
            lblQualityName.ForeColor = Color.FromArgb(33, 37, 41);
            lblQualityName.Location = new Point(17, 50);
            lblQualityName.Name = "lblQualityName";
            lblQualityName.Size = new Size(69, 19);
            lblQualityName.TabIndex = 1;
            lblQualityName.Text = "Название";
            // 
            // txtQualityName
            // 
            txtQualityName.BorderStyle = BorderStyle.FixedSingle;
            txtQualityName.Font = new Font("Segoe UI", 10F);
            txtQualityName.Location = new Point(122, 52);
            txtQualityName.Margin = new Padding(3, 2, 3, 2);
            txtQualityName.Name = "txtQualityName";
            txtQualityName.Size = new Size(202, 25);
            txtQualityName.TabIndex = 2;
            // 
            // panelError
            // 
            panelError.BackColor = Color.FromArgb(255, 245, 245);
            panelError.BorderStyle = BorderStyle.FixedSingle;
            panelError.Controls.Add(lblError);
            tableLayoutPanel.SetColumnSpan(panelError, 2);
            panelError.Location = new Point(17, 80);
            panelError.Name = "panelError";
            panelError.Size = new Size(316, 30);
            panelError.TabIndex = 3;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(3, 5);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 19);
            lblError.TabIndex = 0;
            lblError.Visible = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(17, 112);
            btnSave.Margin = new Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 30);
            btnSave.TabIndex = 4;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(122, 112);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // timerError
            // 
            timerError.Interval = 3000;
            timerError.Tick += TimerError_Tick;
            // 
            // QualityEditDialog
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(350, 169);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "QualityEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Качество";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            panelError.ResumeLayout(false);
            panelError.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label lblTitle;
        private Label lblQualityName;
        private TextBox txtQualityName;
        private Button btnSave;
        private Button btnCancel;
        private Panel panelError;
        private Label lblError;
        private System.Windows.Forms.Timer timerError;
    }
}
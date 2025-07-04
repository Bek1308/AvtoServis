namespace AvtoServis.Forms.Controls
{
    partial class CarBrandDialog
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
            lblBrand = new Label();
            txtBrand = new TextBox();
            lblError = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            toolTip = new ToolTip(components);
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.Controls.Add(lblBrand, 0, 0);
            tableLayoutPanel.Controls.Add(txtBrand, 0, 1);
            tableLayoutPanel.Controls.Add(lblError, 0, 2);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 3);
            tableLayoutPanel.Controls.Add(btnSave, 1, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.Size = new Size(434, 200);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblBrand
            // 
            lblBrand.AccessibleDescription = "Метка для ввода названия марки";
            lblBrand.AccessibleName = "Марка";
            lblBrand.Anchor = AnchorStyles.Left;
            lblBrand.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblBrand, 2);
            lblBrand.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBrand.ForeColor = Color.FromArgb(33, 37, 41);
            lblBrand.Location = new Point(19, 18);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(176, 23);
            lblBrand.TabIndex = 0;
            lblBrand.Text = "Марка автомобиля:";
            // 
            // txtBrand
            // 
            txtBrand.AccessibleDescription = "Введите название марки (только латинские буквы, цифры, пробел)";
            txtBrand.AccessibleName = "Поле названия марки";
            txtBrand.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(txtBrand, 2);
            txtBrand.Font = new Font("Segoe UI", 10F);
            txtBrand.Location = new Point(19, 48);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(396, 30);
            txtBrand.TabIndex = 1;
            txtBrand.TextChanged += txtBrand_TextChanged;
            // 
            // lblError
            // 
            lblError.AccessibleDescription = "Отображает ошибки операций";
            lblError.AccessibleName = "Сообщение об ошибке";
            lblError.Anchor = AnchorStyles.Left;
            lblError.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblError, 2);
            lblError.Font = new Font("Segoe UI", 9F);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(19, 88);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 20);
            lblError.TabIndex = 2;
            lblError.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.AccessibleDescription = "Закрывает окно без сохранения";
            btnCancel.AccessibleName = "Отмена";
            btnCancel.Anchor = AnchorStyles.Left;
            btnCancel.BackColor = Color.FromArgb(220, 53, 69);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 77, 77);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(19, 120);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.AccessibleDescription = "Сохраняет изменения";
            btnSave.AccessibleName = "Сохранить";
            btnSave.Anchor = AnchorStyles.Right;
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(295, 120);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 4;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // CarBrandDialog
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(434, 200);
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CarBrandDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Новая марка";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblMessage;
        private ToolTip toolTip;
    }
}
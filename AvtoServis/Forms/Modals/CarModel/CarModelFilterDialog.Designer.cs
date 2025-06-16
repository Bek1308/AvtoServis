namespace AvtoServis.Forms.Controls
{
    partial class CarModelFilterDialog
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
            lblMinYear = new Label();
            txtMinYear = new TextBox();
            lblMaxYear = new Label();
            txtMaxYear = new TextBox();
            lblBrand = new Label();
            cmbBrand = new ComboBox();
            btnCancel = new Button();
            btnApply = new Button();
            toolTip = new ToolTip(components);
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.Controls.Add(lblMinYear, 0, 0);
            tableLayoutPanel.Controls.Add(txtMinYear, 0, 1);
            tableLayoutPanel.Controls.Add(lblMaxYear, 0, 2);
            tableLayoutPanel.Controls.Add(txtMaxYear, 0, 3);
            tableLayoutPanel.Controls.Add(lblBrand, 0, 4);
            tableLayoutPanel.Controls.Add(cmbBrand, 0, 5);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 7);
            tableLayoutPanel.Controls.Add(btnApply, 1, 7);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(15);
            tableLayoutPanel.RowCount = 8;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.Size = new Size(434, 325);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblMinYear
            // 
            lblMinYear.AccessibleDescription = "Метка для ввода максимального года выпуска";
            lblMinYear.AccessibleName = "Максимальный год";
            lblMinYear.Anchor = AnchorStyles.Left;
            lblMinYear.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblMinYear, 2);
            lblMinYear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMinYear.ForeColor = Color.FromArgb(33, 37, 41);
            lblMinYear.Location = new Point(18, 18);
            lblMinYear.Name = "lblMinYear";
            lblMinYear.Size = new Size(173, 23);
            lblMinYear.TabIndex = 0;
            lblMinYear.Text = "Минимальный год:";
            // 
            // txtMinYear
            // 
            txtMinYear.AccessibleDescription = "Введите минимальный год выпуска (например, 2010)";
            txtMinYear.AccessibleName = "Поле минимального года";
            txtMinYear.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(txtMinYear, 2);
            txtMinYear.Font = new Font("Segoe UI", 10F);
            txtMinYear.Location = new Point(18, 48);
            txtMinYear.Name = "txtMinYear";
            txtMinYear.Size = new Size(350, 30);
            txtMinYear.TabIndex = 1;
            toolTip.SetToolTip(txtMinYear, "Введите минимальный год выпуска (например, 2010)");
            txtMinYear.KeyPress += txtMinYear_KeyPress;
            // 
            // lblMaxYear
            // 
            lblMaxYear.Anchor = AnchorStyles.Left;
            lblMaxYear.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblMaxYear, 2);
            lblMaxYear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMaxYear.ForeColor = Color.FromArgb(33, 37, 41);
            lblMaxYear.Location = new Point(18, 88);
            lblMaxYear.Name = "lblMaxYear";
            lblMaxYear.Size = new Size(179, 23);
            lblMaxYear.TabIndex = 2;
            lblMaxYear.Text = "Максимальный год:";
            // 
            // txtMaxYear
            // 
            txtMaxYear.AccessibleDescription = "Введите максимальный год выпуска (например, 2023)";
            txtMaxYear.AccessibleName = "Поле максимального года";
            txtMaxYear.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(txtMaxYear, 2);
            txtMaxYear.Font = new Font("Segoe UI", 10F);
            txtMaxYear.Location = new Point(18, 118);
            txtMaxYear.Name = "txtMaxYear";
            txtMaxYear.Size = new Size(350, 30);
            txtMaxYear.TabIndex = 3;
            toolTip.SetToolTip(txtMaxYear, "Введите максимальный год выпуска (например, 2023)");
            txtMaxYear.KeyPress += txtMaxYear_KeyPress;
            // 
            // lblBrand
            // 
            lblBrand.AccessibleDescription = "Метка для выбора марки автомобиля";
            lblBrand.AccessibleName = "Марка";
            lblBrand.Anchor = AnchorStyles.Left;
            lblBrand.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblBrand, 2);
            lblBrand.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBrand.ForeColor = Color.FromArgb(33, 37, 41);
            lblBrand.Location = new Point(18, 158);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(176, 23);
            lblBrand.TabIndex = 4;
            lblBrand.Text = "Марка автомобиля:";
            // 
            // cmbBrand
            // 
            cmbBrand.AccessibleDescription = "Список для выбора марки автомобиля";
            cmbBrand.AccessibleName = "Выбор марки";
            tableLayoutPanel.SetColumnSpan(cmbBrand, 2);
            cmbBrand.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBrand.Font = new Font("Segoe UI", 10F);
            cmbBrand.Location = new Point(18, 188);
            cmbBrand.Name = "cmbBrand";
            cmbBrand.Size = new Size(350, 31);
            cmbBrand.TabIndex = 5;
            toolTip.SetToolTip(cmbBrand, "Выберите марку автомобиля для фильтрации");
            // 
            // btnCancel
            // 
            btnCancel.AccessibleDescription = "Закрывает окно без применения фильтра";
            btnCancel.AccessibleName = "Отмена";
            btnCancel.Anchor = AnchorStyles.Left;
            btnCancel.BackColor = Color.FromArgb(220, 53, 69);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 100, 100);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(18, 270);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Отмена";
            toolTip.SetToolTip(btnCancel, "Закрыть без применения фильтров");
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnApply
            // 
            btnApply.AccessibleDescription = "Применяет выбранные фильтры";
            btnApply.AccessibleName = "Применить";
            btnApply.Anchor = AnchorStyles.Right;
            btnApply.BackColor = Color.FromArgb(25, 118, 210);
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(296, 270);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(120, 40);
            btnApply.TabIndex = 7;
            btnApply.Text = "Применить";
            toolTip.SetToolTip(btnApply, "Применить выбранные фильтры");
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
            // 
            // CarModelFilterDialog
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(434, 325);
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CarModelFilterDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Фильтры для моделей";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        private void txtMinYear_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMaxYear_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblMinYear;
        private System.Windows.Forms.TextBox txtMinYear;
        private System.Windows.Forms.Label lblMaxYear;
        private System.Windows.Forms.TextBox txtMaxYear;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.ComboBox cmbBrand;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private ToolTip toolTip;
    }
}
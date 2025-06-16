namespace AvtoServis.Forms.Controls
{
    partial class CarModelDialog
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
            cmbBrand = new ComboBox();
            lblModel = new Label();
            txtModel = new TextBox();
            lblYear = new Label();
            txtYear = new TextBox();
            lblError = new Label();
            chkAddBrand = new CheckBox();
            btnCancel = new Button();
            btnSave = new Button();
            toolTip = new ToolTip(components);
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.49505F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.50495F));
            tableLayoutPanel.Controls.Add(lblBrand, 0, 0);
            tableLayoutPanel.Controls.Add(cmbBrand, 0, 1);
            tableLayoutPanel.Controls.Add(lblModel, 0, 2);
            tableLayoutPanel.Controls.Add(txtModel, 0, 3);
            tableLayoutPanel.Controls.Add(lblYear, 0, 4);
            tableLayoutPanel.Controls.Add(txtYear, 0, 5);
            tableLayoutPanel.Controls.Add(lblError, 0, 6);
            tableLayoutPanel.Controls.Add(chkAddBrand, 0, 7);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 9);
            tableLayoutPanel.Controls.Add(btnSave, 1, 9);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(15);
            tableLayoutPanel.RowCount = 10;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            tableLayoutPanel.Size = new Size(434, 377);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblBrand
            // 
            lblBrand.AccessibleDescription = "Метка для выбора или ввода марки автомобиля";
            lblBrand.AccessibleName = "Марка";
            lblBrand.Anchor = AnchorStyles.Left;
            lblBrand.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblBrand, 2);
            lblBrand.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBrand.ForeColor = Color.FromArgb(33, 37, 41);
            lblBrand.Location = new Point(18, 18);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(176, 23);
            lblBrand.TabIndex = 0;
            lblBrand.Text = "Марка автомобиля:";
            // 
            // cmbBrand
            // 
            cmbBrand.AccessibleDescription = "Список для выбора или ввода марки автомобиля";
            cmbBrand.AccessibleName = "Выбор или ввод марки";
            tableLayoutPanel.SetColumnSpan(cmbBrand, 2);
            cmbBrand.Font = new Font("Segoe UI", 10F);
            cmbBrand.Location = new Point(18, 48);
            cmbBrand.Name = "cmbBrand";
            cmbBrand.Size = new Size(350, 31);
            cmbBrand.TabIndex = 1;
            toolTip.SetToolTip(cmbBrand, "Выберите или введите марку автомобиля");
            // 
            // lblModel
            // 
            lblModel.AccessibleDescription = "Метка для ввода названия модели";
            lblModel.AccessibleName = "Название модели";
            lblModel.Anchor = AnchorStyles.Left;
            lblModel.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblModel, 2);
            lblModel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblModel.ForeColor = Color.FromArgb(33, 37, 41);
            lblModel.Location = new Point(18, 88);
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(163, 23);
            lblModel.TabIndex = 2;
            lblModel.Text = "Название модели:";
            // 
            // txtModel
            // 
            txtModel.AccessibleDescription = "Введите название модели (только латинские буквы, цифры, пробел)";
            txtModel.AccessibleName = "Поле названия модели";
            txtModel.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(txtModel, 2);
            txtModel.Font = new Font("Segoe UI", 10F);
            txtModel.Location = new Point(18, 118);
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(350, 30);
            txtModel.TabIndex = 3;
            toolTip.SetToolTip(txtModel, "Введите название модели (латинские буквы, цифры, пробел, максимум 100 символов)");
            txtModel.TextChanged += txtModel_TextChanged;
            // 
            // lblYear
            // 
            lblYear.AccessibleDescription = "Метка для ввода года выпуска";
            lblYear.AccessibleName = "Год";
            lblYear.Anchor = AnchorStyles.Left;
            lblYear.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblYear, 2);
            lblYear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblYear.ForeColor = Color.FromArgb(33, 37, 41);
            lblYear.Location = new Point(18, 158);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(119, 23);
            lblYear.TabIndex = 4;
            lblYear.Text = "Год выпуска:";
            // 
            // txtYear
            // 
            txtYear.AccessibleDescription = "Введите год выпуска (например, 2023)";
            txtYear.AccessibleName = "Поле года выпуска";
            txtYear.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(txtYear, 2);
            txtYear.Location = new Point(18, 188);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(350, 30);
            txtYear.TabIndex = 5;
            toolTip.SetToolTip(txtYear, "Введите год выпуска (например, 2023)");
            txtYear.TextChanged += txtYear_TextChanged;
            txtYear.KeyPress += txtYear_KeyPress;
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
            lblError.Location = new Point(18, 225);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 18);
            lblError.TabIndex = 6;
            toolTip.SetToolTip(lblError, "Отображает ошибки или сообщения об операции");
            lblError.Visible = false;
            // 
            // chkAddBrand
            // 
            chkAddBrand.AccessibleDescription = "Включить добавление новой марки автомобиля";
            chkAddBrand.AccessibleName = "Добавить марку";
            chkAddBrand.Anchor = AnchorStyles.Left;
            chkAddBrand.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(chkAddBrand, 2);
            chkAddBrand.Font = new Font("Segoe UI", 10F);
            chkAddBrand.ForeColor = Color.FromArgb(33, 37, 41);
            chkAddBrand.Location = new Point(18, 249);
            chkAddBrand.Name = "chkAddBrand";
            chkAddBrand.Size = new Size(216, 27);
            chkAddBrand.TabIndex = 7;
            chkAddBrand.Text = "Добавить новую марку";
            toolTip.SetToolTip(chkAddBrand, "Включите, чтобы добавить новую марку");
            chkAddBrand.CheckedChanged += chkAddBrand_CheckedChanged;
            // 
            // btnCancel
            // 
            btnCancel.AccessibleDescription = "Закрывает окно без сохранения";
            btnCancel.AccessibleName = "Отмена";
            btnCancel.Anchor = AnchorStyles.Left;
            btnCancel.BackColor = Color.FromArgb(220, 53, 69);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 100, 100);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(18, 327);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Отмена";
            toolTip.SetToolTip(btnCancel, "Закрыть без сохранения");
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.AccessibleDescription = "Сохраняет изменения";
            btnSave.AccessibleName = "Сохранить";
            btnSave.Anchor = AnchorStyles.Right;
            btnSave.BackColor = Color.FromArgb(25, 118, 210);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(296, 327);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 9;
            btnSave.Text = "Сохранить";
            toolTip.SetToolTip(btnSave, "Сохранить изменения");
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // CarModelDialog
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(434, 377);
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CarModelDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Новая модель";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.ComboBox cmbBrand;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.CheckBox chkAddBrand;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private ToolTip toolTip;
    }
}
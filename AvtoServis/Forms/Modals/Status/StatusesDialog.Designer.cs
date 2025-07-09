namespace AvtoServis.Forms.Controls
{
    partial class StatusesDialog
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
            lblName = new Label();
            txtName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            btnColor = new Button();
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
            tableLayoutPanel.Controls.Add(lblName, 0, 0);
            tableLayoutPanel.Controls.Add(txtName, 0, 1);
            tableLayoutPanel.Controls.Add(lblDescription, 0, 2);
            tableLayoutPanel.Controls.Add(txtDescription, 0, 3);
            tableLayoutPanel.Controls.Add(btnColor, 0, 4);
            tableLayoutPanel.Controls.Add(lblError, 0, 5);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 6);
            tableLayoutPanel.Controls.Add(btnSave, 1, 6);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 7;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.Size = new Size(434, 300);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AccessibleDescription = "Метка для ввода названия статуса";
            lblName.AccessibleName = "Название";
            lblName.Anchor = AnchorStyles.Left;
            lblName.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblName, 2);
            lblName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            lblName.Location = new Point(19, 18);
            lblName.Name = "lblName";
            lblName.Size = new Size(176, 23);
            lblName.TabIndex = 0;
            lblName.Text = "Название статуса:";
            // 
            // txtName
            // 
            txtName.AccessibleDescription = "Введите название статуса (только латинские буквы, цифры, пробел)";
            txtName.AccessibleName = "Поле названия статуса";
            txtName.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(txtName, 2);
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.Location = new Point(19, 48);
            txtName.Name = "txtName";
            txtName.Size = new Size(396, 30);
            txtName.TabIndex = 1;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // lblDescription
            // 
            lblDescription.AccessibleDescription = "Метка для ввода описания статуса";
            lblDescription.AccessibleName = "Описание";
            lblDescription.Anchor = AnchorStyles.Left;
            lblDescription.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblDescription, 2);
            lblDescription.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDescription.ForeColor = Color.FromArgb(33, 37, 41);
            lblDescription.Location = new Point(19, 88);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(176, 23);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Описание:";
            // 
            // txtDescription
            // 
            txtDescription.AccessibleDescription = "Введите описание статуса (максимум 500 символов)";
            txtDescription.AccessibleName = "Поле описания";
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.SetColumnSpan(txtDescription, 2);
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.Location = new Point(19, 118);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(396, 50);
            txtDescription.TabIndex = 3;
            txtDescription.TextChanged += txtDescription_TextChanged;
            // 
            // btnColor
            // 
            btnColor.AccessibleDescription = "Выберите цвет статуса";
            btnColor.AccessibleName = "Цвет";
            btnColor.FlatAppearance.BorderSize = 0;
            btnColor.FlatStyle = FlatStyle.Flat;
            btnColor.Location = new Point(19, 178);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(120, 30);
            btnColor.TabIndex = 4;
            btnColor.Text = "Выбрать цвет";
            btnColor.UseVisualStyleBackColor = true;
            btnColor.Click += btnColor_Click;
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
            lblError.Location = new Point(19, 218);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 20);
            lblError.TabIndex = 5;
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
            btnCancel.Location = new Point(19, 250);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 6;
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
            btnSave.Location = new Point(295, 250);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 7;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // StatusesDialog
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(434, 300);
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StatusesDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Новый статус";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblMessage;
        private ToolTip toolTip;
    }
}
namespace AvtoServis.Forms.Controls
{
    partial class CustomerDebtsFilterDialog
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
            titleLabel = new Label();
            separator = new Label();
            btnAddFilter = new Button();
            flowLayoutFilters = new FlowLayoutPanel();
            lblError = new Label();
            btnReset = new Button();
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
            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(separator, 0, 1);
            tableLayoutPanel.Controls.Add(btnAddFilter, 0, 2);
            tableLayoutPanel.Controls.Add(flowLayoutFilters, 0, 3);
            tableLayoutPanel.Controls.Add(lblError, 0, 4);
            tableLayoutPanel.Controls.Add(btnReset, 0, 5);
            tableLayoutPanel.Controls.Add(btnApply, 1, 5);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel.Size = new Size(455, 560);
            tableLayoutPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AccessibleDescription = "Заголовок фильтров должников";
            titleLabel.AccessibleName = "Фильтры должников";
            titleLabel.Anchor = AnchorStyles.Left;
            titleLabel.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(titleLabel, 2);
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 17);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(264, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Фильтры должников";
            // 
            // separator
            // 
            separator.AccessibleDescription = "Разделительная линия";
            separator.AccessibleName = "Разделитель";
            separator.BackColor = Color.FromArgb(108, 117, 125);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            separator.Location = new Point(19, 51);
            separator.Name = "separator";
            separator.Size = new Size(396, 2);
            separator.TabIndex = 1;
            // 
            // btnAddFilter
            // 
            btnAddFilter.AccessibleDescription = "Добавляет новый фильтр";
            btnAddFilter.AccessibleName = "Добавить фильтр";
            btnAddFilter.Anchor = AnchorStyles.Left;
            btnAddFilter.AutoSize = true;
            btnAddFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnAddFilter.BackColor = Color.FromArgb(25, 118, 210);
            btnAddFilter.FlatAppearance.BorderSize = 0;
            btnAddFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnAddFilter.FlatStyle = FlatStyle.Flat;
            btnAddFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddFilter.ForeColor = Color.White;
            btnAddFilter.Location = new Point(19, 77);
            btnAddFilter.Margin = new Padding(3, 20, 3, 3);
            btnAddFilter.MinimumSize = new Size(100, 33);
            btnAddFilter.Name = "btnAddFilter";
            btnAddFilter.Size = new Size(101, 33);
            btnAddFilter.TabIndex = 2;
            btnAddFilter.Text = "Добавить";
            btnAddFilter.UseVisualStyleBackColor = false;
            btnAddFilter.Click += BtnAddFilter_Click;
            // 
            // flowLayoutFilters
            // 
            flowLayoutFilters.AccessibleDescription = "Панель для добавления фильтров";
            flowLayoutFilters.AccessibleName = "Фильтры";
            flowLayoutFilters.AutoSize = true;
            flowLayoutFilters.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.SetColumnSpan(flowLayoutFilters, 2);
            flowLayoutFilters.Location = new Point(19, 121);
            flowLayoutFilters.Name = "flowLayoutFilters";
            flowLayoutFilters.Size = new Size(0, 0);
            flowLayoutFilters.TabIndex = 3;
            // 
            // lblError
            // 
            lblError.AccessibleDescription = "Отображает ошибки фильтрации";
            lblError.AccessibleName = "Сообщение об ошибке";
            lblError.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(lblError, 2);
            lblError.Font = new Font("Segoe UI", 9F);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(19, 481);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 20);
            lblError.TabIndex = 4;
            lblError.Visible = false;
            // 
            // btnReset
            // 
            btnReset.AccessibleDescription = "Сбрасывает все фильтры";
            btnReset.AccessibleName = "Сбросить";
            btnReset.AutoSize = true;
            btnReset.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnReset.BackColor = Color.FromArgb(108, 117, 125);
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(19, 514);
            btnReset.MinimumSize = new Size(100, 33);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 33);
            btnReset.TabIndex = 5;
            btnReset.Text = "Сбросить";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += BtnReset_Click;
            // 
            // btnApply
            // 
            btnApply.AccessibleDescription = "Применяет выбранные фильтры";
            btnApply.AccessibleName = "Применить";
            btnApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnApply.AutoSize = true;
            btnApply.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnApply.BackColor = Color.FromArgb(40, 167, 69);
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(320, 514);
            btnApply.MinimumSize = new Size(100, 33);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(116, 33);
            btnApply.TabIndex = 6;
            btnApply.Text = "Применить";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += BtnApply_Click;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            // 
            // CustomerDebtsFilterDialog
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(455, 560);
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomerDebtsFilterDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Фильтры должников";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label titleLabel;
        private Label separator;
        private FlowLayoutPanel flowLayoutFilters;
        private Button btnReset;
        private Button btnApply;
        private Label lblError;
        private Button btnAddFilter;
        private ToolTip toolTip;
    }
}
namespace AvtoServis.Forms.Controls
{
    partial class PartsControl
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            toolTip = new ToolTip(components);
            tableLayoutPanel = new TableLayoutPanel();
            titleLabel = new Label();
            separator = new Panel();
            searchBox = new TextBox();
            buttonPanel = new FlowLayoutPanel();
            btnExport = new Button();
            btnOpenFilterDialog = new Button();
            btnColumns = new Button();
            addButton = new Button();
            dataGridView = new DataGridView();
            countLabel = new Label();
            tableLayoutPanel.SuspendLayout();
            buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = Color.FromArgb(245, 245, 245);
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.94083F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.05917F));
            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(separator, 0, 1);
            tableLayoutPanel.Controls.Add(searchBox, 0, 2);
            tableLayoutPanel.Controls.Add(buttonPanel, 1, 2);
            tableLayoutPanel.Controls.Add(dataGridView, 0, 3);
            tableLayoutPanel.Controls.Add(countLabel, 0, 4);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.Size = new Size(1067, 609);
            tableLayoutPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(titleLabel, 2);
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 16);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(120, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Запчасти";
            // 
            // separator
            // 
            separator.BackColor = Color.FromArgb(180, 180, 180);
            tableLayoutPanel.SetColumnSpan(separator, 2);
            separator.Dock = DockStyle.Fill;
            separator.Location = new Point(19, 56);
            separator.Margin = new Padding(3, 0, 3, 0);
            separator.Name = "separator";
            separator.Size = new Size(1029, 2);
            separator.TabIndex = 1;
            // 
            // searchBox
            // 
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.Font = new Font("Segoe UI", 10F);
            searchBox.ForeColor = Color.Gray;
            searchBox.Location = new Point(19, 73);
            searchBox.Margin = new Padding(3, 15, 3, 3);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(238, 30);
            searchBox.TabIndex = 2;
            searchBox.Text = "Поиск...";
            searchBox.Enter += SearchBox_Enter;
            searchBox.Leave += SearchBox_Leave;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnExport);
            buttonPanel.Controls.Add(btnOpenFilterDialog);
            buttonPanel.Controls.Add(btnColumns);
            buttonPanel.Controls.Add(addButton);
            buttonPanel.Dock = DockStyle.Right;
            buttonPanel.Location = new Point(608, 73);
            buttonPanel.Margin = new Padding(3, 15, 3, 3);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(440, 42);
            buttonPanel.TabIndex = 3;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.FromArgb(25, 118, 210);
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(3, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(100, 34);
            btnExport.TabIndex = 3;
            btnExport.Text = "Экспорт";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += BtnExport_Click;
            // 
            // btnOpenFilterDialog
            // 
            btnOpenFilterDialog.BackColor = Color.FromArgb(25, 118, 210);
            btnOpenFilterDialog.FlatAppearance.BorderSize = 0;
            btnOpenFilterDialog.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnOpenFilterDialog.FlatStyle = FlatStyle.Flat;
            btnOpenFilterDialog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnOpenFilterDialog.ForeColor = Color.White;
            btnOpenFilterDialog.Location = new Point(109, 3);
            btnOpenFilterDialog.Name = "btnOpenFilterDialog";
            btnOpenFilterDialog.Size = new Size(100, 34);
            btnOpenFilterDialog.TabIndex = 2;
            btnOpenFilterDialog.Text = "Фильтры";
            btnOpenFilterDialog.UseVisualStyleBackColor = false;
            btnOpenFilterDialog.Click += BtnOpenFilterDialog_Click;
            // 
            // btnColumns
            // 
            btnColumns.BackColor = Color.FromArgb(25, 118, 210);
            btnColumns.FlatAppearance.BorderSize = 0;
            btnColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnColumns.FlatStyle = FlatStyle.Flat;
            btnColumns.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnColumns.ForeColor = Color.White;
            btnColumns.Location = new Point(215, 3);
            btnColumns.Name = "btnColumns";
            btnColumns.Size = new Size(100, 34);
            btnColumns.TabIndex = 1;
            btnColumns.Text = "Столбцы";
            btnColumns.UseVisualStyleBackColor = false;
            btnColumns.Click += BtnColumns_Click;
            // 
            // addButton
            // 
            addButton.BackColor = Color.FromArgb(40, 167, 69);
            addButton.FlatAppearance.BorderSize = 0;
            addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(321, 3);
            addButton.Name = "addButton";
            addButton.Size = new Size(100, 34);
            addButton.TabIndex = 0;
            addButton.Text = "Добавить";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += AddButton_Click;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(240, 243, 245);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel.SetColumnSpan(dataGridView, 2);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(19, 121);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView.Size = new Size(1029, 424);
            dataGridView.TabIndex = 5;
            dataGridView.CellClick += DataGridView_CellClick;
            dataGridView.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick;
            // 
            // countLabel
            // 
            countLabel.AutoSize = true;
            countLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            countLabel.ForeColor = Color.FromArgb(33, 37, 41);
            countLabel.Location = new Point(19, 563);
            countLabel.Margin = new Padding(3, 15, 3, 0);
            countLabel.Name = "countLabel";
            countLabel.Size = new Size(100, 25);
            countLabel.TabIndex = 4;
            countLabel.Text = "Детали: 0";
            // 
            // PartsControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel);
            Name = "PartsControl";
            Size = new Size(1067, 609);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label titleLabel;
        private Panel separator;
        private TextBox searchBox;
        private FlowLayoutPanel buttonPanel;
        private Button addButton;
        private Button btnColumns;
        private Button btnOpenFilterDialog;
        private Button btnExport;
        private Label countLabel;
        private DataGridView dataGridView;
        private ToolTip toolTip;
    }
}
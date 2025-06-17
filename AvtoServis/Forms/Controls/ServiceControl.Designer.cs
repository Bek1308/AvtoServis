namespace AvtoServis.Forms.Controls
{
    partial class ServiceControl
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            tableLayoutPanel = new TableLayoutPanel();
            titleLabel = new Label();
            addButton = new Button();
            searchBox = new TextBox();
            countLabel = new Label();
            btnOpenFilterDialog = new Button();
            btnRefresh = new Button();
            dataGridView = new DataGridView();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = Color.FromArgb(248, 248, 248);
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(addButton, 1, 0);
            tableLayoutPanel.Controls.Add(searchBox, 0, 1);
            tableLayoutPanel.Controls.Add(countLabel, 1, 1);
            tableLayoutPanel.Controls.Add(btnOpenFilterDialog, 0, 2);
            tableLayoutPanel.Controls.Add(btnRefresh, 1, 2);
            tableLayoutPanel.Controls.Add(dataGridView, 0, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(14, 12, 14, 12);
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(786, 450);
            tableLayoutPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AccessibleDescription = "Заголовок списка услуг";
            titleLabel.AccessibleName = "Список услуг";
            titleLabel.Anchor = AnchorStyles.Left;
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(17, 16);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(153, 30);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Список услуг";
            // 
            // addButton
            // 
            addButton.AccessibleDescription = "Открывает форму для добавления новой услуги";
            addButton.AccessibleName = "Добавить услугу";
            addButton.Anchor = AnchorStyles.Right;
            addButton.BackColor = Color.FromArgb(25, 118, 210);
            addButton.FlatAppearance.BorderSize = 0;
            addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(664, 17);
            addButton.Margin = new Padding(3, 2, 3, 2);
            addButton.Name = "addButton";
            addButton.Size = new Size(105, 27);
            addButton.TabIndex = 1;
            addButton.Text = "Новая";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += AddButton_Click;
            // 
            // searchBox
            // 
            searchBox.AccessibleDescription = "Введите название услуги для поиска";
            searchBox.AccessibleName = "Поиск услуг";
            searchBox.Anchor = AnchorStyles.Left;
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.Font = new Font("Segoe UI", 10F);
            searchBox.ForeColor = Color.FromArgb(108, 117, 125);
            searchBox.Location = new Point(17, 52);
            searchBox.Margin = new Padding(3, 2, 3, 2);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(219, 25);
            searchBox.TabIndex = 2;
            searchBox.Text = "Поиск...";
            searchBox.TextChanged += SearchBox_TextChanged;
            searchBox.GotFocus += SearchBox_GotFocus;
            searchBox.KeyDown += SearchBox_KeyDown;
            searchBox.LostFocus += SearchBox_LostFocus;
            // 
            // countLabel
            // 
            countLabel.AccessibleDescription = "Отображает общее количество услуг";
            countLabel.AccessibleName = "Количество услуг";
            countLabel.Anchor = AnchorStyles.Left;
            countLabel.AutoSize = true;
            countLabel.Font = new Font("Segoe UI", 10F);
            countLabel.ForeColor = Color.FromArgb(33, 37, 41);
            countLabel.Location = new Point(547, 55);
            countLabel.Name = "countLabel";
            countLabel.Size = new Size(57, 19);
            countLabel.TabIndex = 3;
            countLabel.Text = "Услуг: 0";
            // 
            // btnOpenFilterDialog
            // 
            btnOpenFilterDialog.AccessibleDescription = "Открывает окно для фильтрации услуг";
            btnOpenFilterDialog.AccessibleName = "Открыть фильтры";
            btnOpenFilterDialog.Anchor = AnchorStyles.Left;
            btnOpenFilterDialog.BackColor = Color.FromArgb(25, 118, 210);
            btnOpenFilterDialog.FlatAppearance.BorderSize = 0;
            btnOpenFilterDialog.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnOpenFilterDialog.FlatStyle = FlatStyle.Flat;
            btnOpenFilterDialog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnOpenFilterDialog.ForeColor = Color.White;
            btnOpenFilterDialog.Location = new Point(17, 85);
            btnOpenFilterDialog.Margin = new Padding(3, 2, 3, 2);
            btnOpenFilterDialog.Name = "btnOpenFilterDialog";
            btnOpenFilterDialog.Size = new Size(88, 29);
            btnOpenFilterDialog.TabIndex = 4;
            btnOpenFilterDialog.Text = "Фильтры";
            btnOpenFilterDialog.UseVisualStyleBackColor = false;
            btnOpenFilterDialog.Click += BtnOpenFilterDialog_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.AccessibleDescription = "Перезагружает список услуг";
            btnRefresh.AccessibleName = "Обновить данные";
            btnRefresh.Anchor = AnchorStyles.Left;
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(547, 85);
            btnRefresh.Margin = new Padding(3, 2, 3, 2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(88, 29);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // dataGridView
            // 
            dataGridView.AccessibleDescription = "Отображает список услуг с возможностью редактирования и удаления";
            dataGridView.AccessibleName = "Таблица услуг";
            dataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(250, 250, 250);
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(240, 243, 245);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.ColumnHeadersHeight = 29;
            tableLayoutPanel.SetColumnSpan(dataGridView, 2);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.Location = new Point(17, 122);
            dataGridView.Margin = new Padding(3, 2, 3, 2);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(752, 314);
            dataGridView.TabIndex = 6;
            dataGridView.CellClick += DataGridView_CellClick;
            // 
            // ServiceControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            Controls.Add(tableLayoutPanel);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(350, 225);
            Name = "ServiceControl";
            Size = new Size(786, 450);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Label titleLabel;
        private Button addButton;
        private TextBox searchBox;
        private Label countLabel;
        private DataGridView dataGridView;
        private Button btnOpenFilterDialog;
        private Button btnRefresh;

        private void SearchBox_GotFocus(object sender, EventArgs e)
        {
            if (searchBox.Text == "Поиск...")
            {
                searchBox.Text = "";
                searchBox.ForeColor = Color.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchBox.Text))
            {
                searchBox.Text = "Поиск...";
                searchBox.ForeColor = Color.FromArgb(108, 117, 125);
            }
        }
    }
}
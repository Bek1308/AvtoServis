namespace AvtoServis.Forms.Controls
{
    partial class SuppliersControl
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
            btnExport = new Button();
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
            tableLayoutPanel.Controls.Add(btnExport, 0, 3);
            tableLayoutPanel.Controls.Add(dataGridView, 0, 4);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(898, 600);
            tableLayoutPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AccessibleDescription = "Заголовок списка поставщиков";
            titleLabel.AccessibleName = "Список поставщиков";
            titleLabel.Anchor = AnchorStyles.Left;
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 22);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(227, 37);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Список поставщиков";
            // 
            // addButton
            // 
            addButton.AccessibleDescription = "Открывает форму для добавления нового поставщика";
            addButton.AccessibleName = "Новый поставщик";
            addButton.Anchor = AnchorStyles.Right;
            addButton.BackColor = Color.FromArgb(25, 118, 210);
            addButton.FlatAppearance.BorderSize = 0;
            addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(759, 23);
            addButton.Name = "addButton";
            addButton.Size = new Size(120, 36);
            addButton.TabIndex = 1;
            addButton.Text = "Новый";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += AddButton_Click;
            // 
            // searchBox
            // 
            searchBox.AccessibleDescription = "Введите название или телефон поставщика для поиска";
            searchBox.AccessibleName = "Поиск поставщиков";
            searchBox.Anchor = AnchorStyles.Left;
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.Font = new Font("Segoe UI", 10F);
            searchBox.ForeColor = Color.FromArgb(108, 117, 125);
            searchBox.Location = new Point(19, 71);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(282, 30);
            searchBox.TabIndex = 2;
            searchBox.Text = "Поиск...";
            searchBox.TextChanged += SearchBox_TextChanged;
            searchBox.GotFocus += SearchBox_GotFocus;
            searchBox.KeyDown += SearchBox_KeyDown;
            searchBox.LostFocus += SearchBox_LostFocus;
            // 
            // countLabel
            // 
            countLabel.AccessibleDescription = "Показывает общее количество поставщиков";
            countLabel.AccessibleName = "Количество поставщиков";
            countLabel.Anchor = AnchorStyles.Left;
            countLabel.AutoSize = true;
            countLabel.Font = new Font("Segoe UI", 10F);
            countLabel.ForeColor = Color.FromArgb(33, 37, 41);
            countLabel.Location = new Point(625, 74);
            countLabel.Name = "countLabel";
            countLabel.Size = new Size(84, 23);
            countLabel.TabIndex = 3;
            countLabel.Text = "Поставщики: 0";
            // 
            // btnOpenFilterDialog
            // 
            btnOpenFilterDialog.AccessibleDescription = "Открывает окно для фильтрации поставщиков";
            btnOpenFilterDialog.AccessibleName = "Открыть фильтры";
            btnOpenFilterDialog.BackColor = Color.FromArgb(25, 118, 210);
            btnOpenFilterDialog.FlatAppearance.BorderSize = 0;
            btnOpenFilterDialog.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnOpenFilterDialog.FlatStyle = FlatStyle.Flat;
            btnOpenFilterDialog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnOpenFilterDialog.ForeColor = Color.White;
            btnOpenFilterDialog.Location = new Point(19, 109);
            btnOpenFilterDialog.Name = "btnOpenFilterDialog";
            btnOpenFilterDialog.Size = new Size(120, 34);
            btnOpenFilterDialog.TabIndex = 4;
            btnOpenFilterDialog.Text = "Фильтры";
            btnOpenFilterDialog.UseVisualStyleBackColor = false;
            btnOpenFilterDialog.Click += BtnOpenFilterDialog_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.AccessibleDescription = "Обновляет список поставщиков";
            btnRefresh.AccessibleName = "Обновить данные";
            btnRefresh.Anchor = AnchorStyles.Left;
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(625, 109);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 34);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // btnExport
            // 
            btnExport.AccessibleDescription = "Экспортирует список поставщиков в Excel файл";
            btnExport.AccessibleName = "Экспорт данных";
            btnExport.Anchor = AnchorStyles.Left;
            btnExport.BackColor = Color.FromArgb(40, 167, 69);
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(19, 149);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(120, 34);
            btnExport.TabIndex = 6;
            btnExport.Text = "Экспорт";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += BtnExport_Click;
            // 
            // dataGridView
            // 
            dataGridView.AccessibleDescription = "Отображает список поставщиков с возможностью редактирования, удаления и просмотра деталей";
            dataGridView.AccessibleName = "Таблица поставщиков";
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
            dataGridView.Location = new Point(19, 189);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(860, 392);
            dataGridView.TabIndex = 7;
            dataGridView.CellClick += DataGridView_CellClick;
            dataGridView.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick;
            // 
            // SuppliersControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            Controls.Add(tableLayoutPanel);
            MinimumSize = new Size(400, 300);
            Name = "SuppliersControl";
            Size = new Size(898, 600);
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
        private Button btnOpenFilterDialog;
        private Button btnRefresh;
        private Button btnExport;
        private DataGridView dataGridView;

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
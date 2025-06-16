namespace AvtoServis.Forms.Controls
{
    partial class ManufacturerControl
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
            addButton = new Button();
            searchBox = new TextBox();
            countLabel = new Label();
            dataGridView = new DataGridView();
            btnOpenFilterDialog = new Button();
            btnRefresh = new Button();

            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();

            // tableLayoutPanel
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.BackColor = Color.FromArgb(245, 245, 245);

            // titleLabel
            titleLabel.Text = "Список производителей";
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.AutoSize = true;
            titleLabel.Anchor = AnchorStyles.Left;
            titleLabel.AccessibleName = "Список производителей";
            titleLabel.AccessibleDescription = "Заголовок списка производителей";
            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);

            // addButton
            addButton.Text = "Новая";
            addButton.Size = new Size(120, 36);
            addButton.BackColor = Color.FromArgb(25, 118, 210);
            addButton.ForeColor = Color.White;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.FlatAppearance.BorderSize = 0;
            addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            addButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            addButton.Anchor = AnchorStyles.Right;
            addButton.AccessibleName = "Добавить производителя";
            addButton.AccessibleDescription = "Открывает форму для добавления нового производителя";
            tableLayoutPanel.Controls.Add(addButton, 1, 0);

            // searchBox
            searchBox.Text = "Поиск...";
            searchBox.ForeColor = Color.FromArgb(108, 117, 125);
            searchBox.Size = new Size(250, 32);
            searchBox.Font = new Font("Segoe UI", 10F, GraphicsUnit.Point);
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.GotFocus += (s, e) => { if (searchBox.Text == "Поиск...") { searchBox.Text = ""; searchBox.ForeColor = Color.Black; } };
            searchBox.LostFocus += (s, e) => { if (string.IsNullOrEmpty(searchBox.Text)) { searchBox.Text = "Поиск..."; searchBox.ForeColor = Color.FromArgb(108, 117, 125); } };
            searchBox.Anchor = AnchorStyles.Left;
            searchBox.AccessibleName = "Поиск производителей";
            searchBox.AccessibleDescription = "Введите название производителя для поиска";
            tableLayoutPanel.Controls.Add(searchBox, 0, 1);

            // countLabel
            countLabel.Text = "Производителей: 0";
            countLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            countLabel.ForeColor = Color.FromArgb(33, 37, 41);
            countLabel.AutoSize = true;
            countLabel.Anchor = AnchorStyles.Left;
            countLabel.AccessibleName = "Количество производителей";
            countLabel.AccessibleDescription = "Отображает общее количество производителей";
            tableLayoutPanel.Controls.Add(countLabel, 1, 1);

            // btnOpenFilterDialog
            btnOpenFilterDialog.Text = "Фильтры";
            btnOpenFilterDialog.Size = new Size(100, 28);
            btnOpenFilterDialog.BackColor = Color.FromArgb(25, 118, 210);
            btnOpenFilterDialog.ForeColor = Color.White;
            btnOpenFilterDialog.FlatStyle = FlatStyle.Flat;
            btnOpenFilterDialog.FlatAppearance.BorderSize = 0;
            btnOpenFilterDialog.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnOpenFilterDialog.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnOpenFilterDialog.Anchor = AnchorStyles.Left;
            btnOpenFilterDialog.AccessibleName = "Открыть фильтры";
            btnOpenFilterDialog.AccessibleDescription = "Открывает окно для фильтрации производителей";
            tableLayoutPanel.Controls.Add(btnOpenFilterDialog, 0, 2);

            // btnRefresh
            btnRefresh.Text = "Обновить";
            btnRefresh.Size = new Size(100, 28);
            btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnRefresh.Anchor = AnchorStyles.Left;
            btnRefresh.AccessibleName = "Обновить данные";
            btnRefresh.AccessibleDescription = "Перезагружает список производителей";
            tableLayoutPanel.Controls.Add(btnRefresh, 1, 2);

            // dataGridView
            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 243, 245);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridView.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AccessibleName = "Таблица производителей";
            dataGridView.AccessibleDescription = "Отображает список производителей с возможностью редактирования и удаления";
            tableLayoutPanel.Controls.Add(dataGridView, 0, 3);
            tableLayoutPanel.SetColumnSpan(dataGridView, 2);

            // ManufacturerControl
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            Controls.Add(tableLayoutPanel);
            MinimumSize = new Size(400, 300);
            Size = new Size(800, 600);
            Name = "ManufacturerControl";
            tableLayoutPanel.ResumeLayout(false);
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
    }
}
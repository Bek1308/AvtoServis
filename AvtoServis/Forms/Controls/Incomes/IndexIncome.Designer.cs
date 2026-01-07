namespace AvtoServis.Forms.Controls
{
    partial class IndexIncome
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
            tableLayoutPanelTop = new TableLayoutPanel();
            searchBox = new TextBox();
            addButton = new Button();
            btnOpenFilterDialog = new Button();
            flowLayoutPanelCards = new FlowLayoutPanel();
            tableLayoutPanelTop.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelTop
            // 
            tableLayoutPanelTop.BackColor = Color.FromArgb(248, 248, 248);
            tableLayoutPanelTop.ColumnCount = 3;
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.4872971F));
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.2679F));
            tableLayoutPanelTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.2448044F));
            tableLayoutPanelTop.Controls.Add(searchBox, 0, 0);
            tableLayoutPanelTop.Controls.Add(addButton, 2, 0);
            tableLayoutPanelTop.Controls.Add(btnOpenFilterDialog, 1, 0);
            tableLayoutPanelTop.Dock = DockStyle.Top;
            tableLayoutPanelTop.Location = new Point(0, 0);
            tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            tableLayoutPanelTop.Padding = new Padding(16);
            tableLayoutPanelTop.RowCount = 2;
            tableLayoutPanelTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanelTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanelTop.Size = new Size(898, 95);
            tableLayoutPanelTop.TabIndex = 0;
            // 
            // searchBox
            // 
            searchBox.AccessibleDescription = "Введите номер партии или поставщика для поиска";
            searchBox.AccessibleName = "Поиск партий";
            searchBox.Anchor = AnchorStyles.Left;
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.Font = new Font("Segoe UI", 10F);
            searchBox.ForeColor = Color.FromArgb(108, 117, 125);
            searchBox.Location = new Point(19, 23);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(282, 30);
            searchBox.TabIndex = 0;
            searchBox.Text = "Поиск...";
            // 
            // addButton
            // 
            addButton.AccessibleDescription = "Открывает форму для добавления новой партии";
            addButton.AccessibleName = "Новая партия";
            addButton.Anchor = AnchorStyles.Left;
            addButton.BackColor = Color.FromArgb(25, 118, 210);
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(727, 19);
            addButton.Name = "addButton";
            addButton.Size = new Size(152, 38);
            addButton.TabIndex = 2;
            addButton.Text = "Новый";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;
            // 
            // btnOpenFilterDialog
            // 
            btnOpenFilterDialog.AccessibleDescription = "Открывает окно для фильтрации партий";
            btnOpenFilterDialog.AccessibleName = "Открыть фильтры";
            btnOpenFilterDialog.Anchor = AnchorStyles.Right;
            btnOpenFilterDialog.BackColor = Color.LimeGreen;
            btnOpenFilterDialog.FlatStyle = FlatStyle.Flat;
            btnOpenFilterDialog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnOpenFilterDialog.ForeColor = Color.White;
            btnOpenFilterDialog.Location = new Point(591, 19);
            btnOpenFilterDialog.Name = "btnOpenFilterDialog";
            btnOpenFilterDialog.Size = new Size(130, 38);
            btnOpenFilterDialog.TabIndex = 1;
            btnOpenFilterDialog.Text = "Фильтры";
            btnOpenFilterDialog.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanelCards
            // 
            flowLayoutPanelCards.AutoScroll = true;
            flowLayoutPanelCards.BackColor = Color.FromArgb(248, 248, 248);
            flowLayoutPanelCards.Dock = DockStyle.Fill;
            flowLayoutPanelCards.Location = new Point(0, 95);
            flowLayoutPanelCards.Name = "flowLayoutPanelCards";
            flowLayoutPanelCards.Padding = new Padding(20);
            flowLayoutPanelCards.Size = new Size(898, 505);
            flowLayoutPanelCards.TabIndex = 1;
            // 
            // IndexIncome
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            Controls.Add(flowLayoutPanelCards);
            Controls.Add(tableLayoutPanelTop);
            MinimumSize = new Size(400, 300);
            Name = "IndexIncome";
            Size = new Size(898, 600);
            tableLayoutPanelTop.ResumeLayout(false);
            tableLayoutPanelTop.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanelTop;
        private TextBox searchBox;
        private Button btnOpenFilterDialog;
        private Button addButton;
        private FlowLayoutPanel flowLayoutPanelCards;
    }
}
namespace AvtoServis.Forms.Controls
{
    partial class FilterDialog
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
            flowLayoutPanel = new FlowLayoutPanel();
            panelMinPrice = new Panel();
            lblMinPrice = new Label();
            txtPriceMin = new TextBox();
            panelMaxPrice = new Panel();
            lblMaxPrice = new Label();
            txtPriceMax = new TextBox();
            panelHighPrice = new Panel();
            chkHighPrice = new CheckBox();
            panelButtons = new Panel();
            btnApply = new Button();
            btnCancel = new Button();
            flowLayoutPanel.SuspendLayout();
            panelMinPrice.SuspendLayout();
            panelMaxPrice.SuspendLayout();
            panelHighPrice.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.BackColor = Color.FromArgb(245, 245, 245);
            flowLayoutPanel.Controls.Add(panelMinPrice);
            flowLayoutPanel.Controls.Add(panelMaxPrice);
            flowLayoutPanel.Controls.Add(panelHighPrice);
            flowLayoutPanel.Controls.Add(panelButtons);
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel.Location = new Point(0, 0);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Padding = new Padding(16);
            flowLayoutPanel.Size = new Size(428, 226);
            flowLayoutPanel.TabIndex = 0;
            // 
            // panelMinPrice
            // 
            panelMinPrice.AutoSize = true;
            panelMinPrice.Controls.Add(lblMinPrice);
            panelMinPrice.Controls.Add(txtPriceMin);
            panelMinPrice.Location = new Point(19, 19);
            panelMinPrice.Name = "panelMinPrice";
            panelMinPrice.Size = new Size(193, 40);
            panelMinPrice.TabIndex = 0;
            // 
            // lblMinPrice
            // 
            lblMinPrice.AccessibleDescription = "Метка для поля минимальной цены";
            lblMinPrice.AccessibleName = "Минимальная цена";
            lblMinPrice.AutoSize = true;
            lblMinPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMinPrice.ForeColor = Color.FromArgb(33, 37, 41);
            lblMinPrice.Location = new Point(0, 10);
            lblMinPrice.Name = "lblMinPrice";
            lblMinPrice.Size = new Size(90, 23);
            lblMinPrice.TabIndex = 0;
            lblMinPrice.Text = "Min Price:";
            // 
            // txtPriceMin
            // 
            txtPriceMin.AccessibleDescription = "Введите минимальную цену для фильтрации";
            txtPriceMin.AccessibleName = "Поле минимальной цены";
            txtPriceMin.BorderStyle = BorderStyle.FixedSingle;
            txtPriceMin.Location = new Point(100, 7);
            txtPriceMin.Name = "txtPriceMin";
            txtPriceMin.Size = new Size(90, 30);
            txtPriceMin.TabIndex = 1;
            txtPriceMin.KeyPress += TxtPriceMin_KeyPress;
            // 
            // panelMaxPrice
            // 
            panelMaxPrice.AutoSize = true;
            panelMaxPrice.Controls.Add(lblMaxPrice);
            panelMaxPrice.Controls.Add(txtPriceMax);
            panelMaxPrice.Location = new Point(19, 65);
            panelMaxPrice.Name = "panelMaxPrice";
            panelMaxPrice.Size = new Size(193, 40);
            panelMaxPrice.TabIndex = 1;
            // 
            // lblMaxPrice
            // 
            lblMaxPrice.AccessibleDescription = "Метка для поля максимальной цены";
            lblMaxPrice.AccessibleName = "Максимальная цена";
            lblMaxPrice.AutoSize = true;
            lblMaxPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMaxPrice.ForeColor = Color.FromArgb(33, 37, 41);
            lblMaxPrice.Location = new Point(0, 10);
            lblMaxPrice.Name = "lblMaxPrice";
            lblMaxPrice.Size = new Size(93, 23);
            lblMaxPrice.TabIndex = 0;
            lblMaxPrice.Text = "Max Price:";
            // 
            // txtPriceMax
            // 
            txtPriceMax.AccessibleDescription = "Введите максимальную цену для фильтрации";
            txtPriceMax.AccessibleName = "Поле максимальной цены";
            txtPriceMax.BorderStyle = BorderStyle.FixedSingle;
            txtPriceMax.Location = new Point(100, 7);
            txtPriceMax.Name = "txtPriceMax";
            txtPriceMax.Size = new Size(90, 30);
            txtPriceMax.TabIndex = 1;
            txtPriceMax.KeyPress += TxtPriceMax_KeyPress;
            // 
            // panelHighPrice
            // 
            panelHighPrice.AutoSize = true;
            panelHighPrice.Controls.Add(chkHighPrice);
            panelHighPrice.Location = new Point(19, 111);
            panelHighPrice.Name = "panelHighPrice";
            panelHighPrice.Size = new Size(151, 40);
            panelHighPrice.TabIndex = 2;
            // 
            // chkHighPrice
            // 
            chkHighPrice.AccessibleDescription = "Фильтр для услуг с высокой ценой";
            chkHighPrice.AccessibleName = "Высокая цена";
            chkHighPrice.AutoSize = true;
            chkHighPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            chkHighPrice.Location = new Point(0, 10);
            chkHighPrice.Name = "chkHighPrice";
            chkHighPrice.Size = new Size(148, 27);
            chkHighPrice.TabIndex = 0;
            chkHighPrice.Text = "Высокая цена";
            // 
            // panelButtons
            // 
            panelButtons.AutoSize = true;
            panelButtons.Controls.Add(btnApply);
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Location = new Point(19, 157);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(363, 43);
            panelButtons.TabIndex = 3;
            // 
            // btnApply
            // 
            btnApply.AccessibleDescription = "Применяет фильтр по ценам";
            btnApply.AccessibleName = "Применить фильтр";
            btnApply.BackColor = Color.FromArgb(25, 118, 210);
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(150, 6);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(100, 34);
            btnApply.TabIndex = 0;
            btnApply.Text = "Применить";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += BtnApply_Click;
            // 
            // btnCancel
            // 
            btnCancel.AccessibleDescription = "Закрывает окно без применения фильтров";
            btnCancel.AccessibleName = "Отменить";
            btnCancel.BackColor = Color.FromArgb(220, 53, 69);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 100, 100);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(260, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 34);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // FilterDialog
            // 
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(428, 226);
            Controls.Add(flowLayoutPanel);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FilterDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Фильтры";
            flowLayoutPanel.ResumeLayout(false);
            flowLayoutPanel.PerformLayout();
            panelMinPrice.ResumeLayout(false);
            panelMinPrice.PerformLayout();
            panelMaxPrice.ResumeLayout(false);
            panelMaxPrice.PerformLayout();
            panelHighPrice.ResumeLayout(false);
            panelHighPrice.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private FlowLayoutPanel flowLayoutPanel;
        private TextBox txtPriceMin;
        private TextBox txtPriceMax;
        private CheckBox chkHighPrice;
        private Button btnApply;
        private Button btnCancel;
        private Panel panelMinPrice;
        private Label lblMinPrice;
        private Panel panelMaxPrice;
        private Label lblMaxPrice;
        private Panel panelHighPrice;
        private Panel panelButtons;
    }
}
namespace AvtoServis.Forms.Controls
{
    partial class TestControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestControl));
            mainPanel = new Panel();
            labelPageTitle = new Label();
            btnIncome = new Button();
            imageList1 = new ImageList(components);
            bottomPanel = new Panel();
            panelSearch = new Panel();
            labelSearch = new Label();
            textBoxSearch = new TextBox();
            comboBoxFilter = new ComboBox();
            panelFilter = new Panel();
            labelMinPrice = new Label();
            numericUpDownMinPrice = new NumericUpDown();
            labelMaxPrice = new Label();
            numericUpDownMaxPrice = new NumericUpDown();
            btnApplyFilter = new Button();
            dataGridViewMain = new DataGridView();
            mainPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            panelSearch.SuspendLayout();
            panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMaxPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).BeginInit();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(labelPageTitle);
            mainPanel.Controls.Add(btnIncome);
            mainPanel.Controls.Add(bottomPanel);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1309, 739);
            mainPanel.TabIndex = 0;
            // 
            // labelPageTitle
            // 
            labelPageTitle.AutoSize = true;
            labelPageTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelPageTitle.Location = new Point(10, 10);
            labelPageTitle.Name = "labelPageTitle";
            labelPageTitle.Size = new Size(92, 32);
            labelPageTitle.TabIndex = 0;
            labelPageTitle.Text = "Услуги";
            // 
            // btnIncome
            // 
            btnIncome.BackColor = Color.DodgerBlue;
            btnIncome.FlatAppearance.BorderSize = 0;
            btnIncome.FlatStyle = FlatStyle.Flat;
            btnIncome.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnIncome.ForeColor = Color.White;
            btnIncome.ImageAlign = ContentAlignment.MiddleLeft;
            btnIncome.ImageIndex = 0;
            btnIncome.ImageList = imageList1;
            btnIncome.Location = new Point(1159, 10);
            btnIncome.Name = "btnIncome";
            btnIncome.Size = new Size(140, 40);
            btnIncome.TabIndex = 1;
            btnIncome.Text = "Новый";
            btnIncome.TextAlign = ContentAlignment.MiddleRight;
            btnIncome.UseVisualStyleBackColor = false;
            btnIncome.Click += BtnIncome_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "icons8-plus-24.png");
            // 
            // bottomPanel
            // 
            bottomPanel.Controls.Add(panelSearch);
            bottomPanel.Controls.Add(comboBoxFilter);
            bottomPanel.Controls.Add(panelFilter);
            bottomPanel.Controls.Add(dataGridViewMain);
            bottomPanel.Location = new Point(10, 60);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(1289, 669);
            bottomPanel.TabIndex = 2;
            // 
            // panelSearch
            // 
            panelSearch.Controls.Add(labelSearch);
            panelSearch.Controls.Add(textBoxSearch);
            panelSearch.Location = new Point(10, 10);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(300, 80);
            panelSearch.TabIndex = 0;
            // 
            // labelSearch
            // 
            labelSearch.AutoSize = true;
            labelSearch.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelSearch.Location = new Point(10, 10);
            labelSearch.Name = "labelSearch";
            labelSearch.Size = new Size(58, 23);
            labelSearch.TabIndex = 0;
            labelSearch.Text = "Поиск";
            // 
            // textBoxSearch
            // 
            textBoxSearch.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxSearch.Location = new Point(10, 36);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(280, 34);
            textBoxSearch.TabIndex = 1;
            textBoxSearch.KeyPress += TextBoxSearch_KeyPress;
            // 
            // comboBoxFilter
            // 
            comboBoxFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFilter.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            comboBoxFilter.Items.AddRange(new object[] { "Все", "Активные", "Архив" });
            comboBoxFilter.Location = new Point(320, 36);
            comboBoxFilter.Name = "comboBoxFilter";
            comboBoxFilter.Size = new Size(150, 31);
            comboBoxFilter.TabIndex = 2;
            comboBoxFilter.SelectedIndexChanged += ComboBoxFilter_SelectedIndexChanged;
            // 
            // panelFilter
            // 
            panelFilter.BorderStyle = BorderStyle.FixedSingle;
            panelFilter.Controls.Add(labelMinPrice);
            panelFilter.Controls.Add(numericUpDownMinPrice);
            panelFilter.Controls.Add(labelMaxPrice);
            panelFilter.Controls.Add(numericUpDownMaxPrice);
            panelFilter.Controls.Add(btnApplyFilter);
            panelFilter.Location = new Point(320, 67);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new Size(200, 150);
            panelFilter.TabIndex = 3;
            panelFilter.Visible = false;
            // 
            // labelMinPrice
            // 
            labelMinPrice.AutoSize = true;
            labelMinPrice.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelMinPrice.Location = new Point(10, 10);
            labelMinPrice.Name = "labelMinPrice";
            labelMinPrice.Size = new Size(81, 20);
            labelMinPrice.TabIndex = 0;
            labelMinPrice.Text = "Мин. цена";
            // 
            // numericUpDownMinPrice
            // 
            numericUpDownMinPrice.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numericUpDownMinPrice.Location = new Point(10, 35);
            numericUpDownMinPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownMinPrice.Name = "numericUpDownMinPrice";
            numericUpDownMinPrice.Size = new Size(180, 27);
            numericUpDownMinPrice.TabIndex = 1;
            // 
            // labelMaxPrice
            // 
            labelMaxPrice.AutoSize = true;
            labelMaxPrice.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelMaxPrice.Location = new Point(10, 70);
            labelMaxPrice.Name = "labelMaxPrice";
            labelMaxPrice.Size = new Size(85, 20);
            labelMaxPrice.TabIndex = 2;
            labelMaxPrice.Text = "Макс. цена";
            // 
            // numericUpDownMaxPrice
            // 
            numericUpDownMaxPrice.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numericUpDownMaxPrice.Location = new Point(10, 95);
            numericUpDownMaxPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownMaxPrice.Name = "numericUpDownMaxPrice";
            numericUpDownMaxPrice.Size = new Size(180, 27);
            numericUpDownMaxPrice.TabIndex = 3;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.FlatStyle = FlatStyle.Flat;
            btnApplyFilter.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnApplyFilter.Location = new Point(10, 125);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(80, 25);
            btnApplyFilter.TabIndex = 4;
            btnApplyFilter.Text = "Применить";
            btnApplyFilter.UseVisualStyleBackColor = true;
            btnApplyFilter.Click += BtnApplyFilter_Click;
            // 
            // dataGridViewMain
            // 
            dataGridViewMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMain.Location = new Point(10, 100);
            dataGridViewMain.Name = "dataGridViewMain";
            dataGridViewMain.RowHeadersWidth = 51;
            dataGridViewMain.Size = new Size(1269, 559);
            dataGridViewMain.TabIndex = 5;
            // 
            // TestControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 248, 248);
            Controls.Add(mainPanel);
            Name = "TestControl";
            Size = new Size(1309, 739);
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            bottomPanel.ResumeLayout(false);
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMaxPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Label labelPageTitle;
        private Button btnIncome;
        private Panel bottomPanel;
        private Panel panelSearch;
        private Label labelSearch;
        private TextBox textBoxSearch;
        private ComboBox comboBoxFilter;
        private Panel panelFilter;
        private Label labelMinPrice;
        private NumericUpDown numericUpDownMinPrice;
        private Label labelMaxPrice;
        private NumericUpDown numericUpDownMaxPrice;
        private Button btnApplyFilter;
        private DataGridView dataGridViewMain;
        private ImageList imageList1;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Font = System.Drawing.Font;
using Color = System.Drawing.Color;
using Timer = System.Windows.Forms.Timer;

namespace AvtoServis.Forms
{
    public partial class SaleForm
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            components = new Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            toolTip = new ToolTip(components);
            notificationTimer = new Timer(components);
            tableLayoutPanel = new TableLayoutPanel();
            customerInfoLbl = new Label();
            titleLabel = new Label();
            separatorTop = new Panel();
            popularProductsPanel = new Panel();
            popularProductsHeaderLayout = new TableLayoutPanel();
            popularProductsTitleLabel = new Label();
            btnColumns = new Button();
            popularProductsGrid = new DataGridView();
            separatorLeftMiddle = new Panel();
            middlePanel = new TableLayoutPanel();
            middleHeaderPanel = new TableLayoutPanel();
            selectedProductsTitleLabel = new Label();
            quickButtonsPanel = new FlowLayoutPanel();
            btnAddProduct = new Button();
            button1 = new Button();
            btnSearch = new Button();
            btnColumnsSl = new Button();
            selectedProductsGrid = new DataGridView();
            separatorMiddleRight = new Panel();
            rightPanel = new TableLayoutPanel();
            incompleteSalesPanel = new Panel();
            incompleteSalesTitleLabel = new Label();
            incompleteSalesFlow = new FlowLayoutPanel();
            separatorProductInfo = new Panel();
            selectedProductInfoPanel = new Panel();
            selectedProductInfoLabel = new Label();
            separatorKeypadTop = new Panel();
            inputPanel = new TableLayoutPanel();
            paidAmountTitleLabel = new Label();
            paidAmountTextBox = new TextBox();
            customerSearchTitleLabel = new Label();
            comboBox1 = new ComboBox();
            totalAmountLabel = new Label();
            separatorKeypadBottom = new Panel();
            numericKeypadPanel = new Panel();
            numericKeypadLayout = new TableLayoutPanel();
            btnNum1 = new Button();
            btnNum2 = new Button();
            btnNum3 = new Button();
            btnNum4 = new Button();
            btnNum5 = new Button();
            btnNum6 = new Button();
            btnNum7 = new Button();
            btnNum8 = new Button();
            btnNum9 = new Button();
            btnNum0 = new Button();
            btnNumClear = new Button();
            btnNumEnter = new Button();
            separatorKeypadBottom2 = new Panel();
            buttonsPanel = new TableLayoutPanel();
            btnContinue = new Button();
            btnCancelClose = new Button();
            notificationPanel = new Panel();
            notificationLabel = new Label();
            tableLayoutPanel.SuspendLayout();
            popularProductsPanel.SuspendLayout();
            popularProductsHeaderLayout.SuspendLayout();
            ((ISupportInitialize)popularProductsGrid).BeginInit();
            middlePanel.SuspendLayout();
            middleHeaderPanel.SuspendLayout();
            quickButtonsPanel.SuspendLayout();
            ((ISupportInitialize)selectedProductsGrid).BeginInit();
            rightPanel.SuspendLayout();
            incompleteSalesPanel.SuspendLayout();
            selectedProductInfoPanel.SuspendLayout();
            inputPanel.SuspendLayout();
            numericKeypadPanel.SuspendLayout();
            numericKeypadLayout.SuspendLayout();
            buttonsPanel.SuspendLayout();
            notificationPanel.SuspendLayout();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            // 
            // notificationTimer
            // 
            notificationTimer.Interval = 5000;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = Color.FromArgb(245, 245, 245);
            tableLayoutPanel.ColumnCount = 5;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.6428566F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.3633728F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.014534F));
            tableLayoutPanel.Controls.Add(customerInfoLbl, 2, 0);
            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(separatorTop, 0, 1);
            tableLayoutPanel.Controls.Add(popularProductsPanel, 0, 2);
            tableLayoutPanel.Controls.Add(separatorLeftMiddle, 1, 2);
            tableLayoutPanel.Controls.Add(middlePanel, 2, 2);
            tableLayoutPanel.Controls.Add(separatorMiddleRight, 3, 2);
            tableLayoutPanel.Controls.Add(rightPanel, 4, 2);
            tableLayoutPanel.Controls.Add(notificationPanel, 4, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(10);
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(1400, 800);
            tableLayoutPanel.TabIndex = 0;
            // 
            // customerInfoLbl
            // 
            customerInfoLbl.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(customerInfoLbl, 2);
            customerInfoLbl.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            customerInfoLbl.ForeColor = Color.FromArgb(33, 37, 41);
            customerInfoLbl.Location = new Point(292, 20);
            customerInfoLbl.Margin = new Padding(10);
            customerInfoLbl.Name = "customerInfoLbl";
            customerInfoLbl.Size = new Size(200, 30);
            customerInfoLbl.TabIndex = 7;
            customerInfoLbl.Text = "Панель продаж";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(titleLabel, 2);
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Margin = new Padding(10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(200, 30);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Панель продаж";
            // 
            // separatorTop
            // 
            separatorTop.BackColor = Color.FromArgb(180, 180, 180);
            tableLayoutPanel.SetColumnSpan(separatorTop, 5);
            separatorTop.Dock = DockStyle.Fill;
            separatorTop.Location = new Point(13, 63);
            separatorTop.Name = "separatorTop";
            separatorTop.Size = new Size(1374, 1);
            separatorTop.TabIndex = 1;
            // 
            // popularProductsPanel
            // 
            popularProductsPanel.Controls.Add(popularProductsHeaderLayout);
            popularProductsPanel.Controls.Add(popularProductsGrid);
            popularProductsPanel.Dock = DockStyle.Fill;
            popularProductsPanel.Location = new Point(13, 65);
            popularProductsPanel.Name = "popularProductsPanel";
            popularProductsPanel.Padding = new Padding(5);
            popularProductsPanel.Size = new Size(264, 722);
            popularProductsPanel.TabIndex = 4;
            // 
            // popularProductsHeaderLayout
            // 
            popularProductsHeaderLayout.ColumnCount = 2;
            popularProductsHeaderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82.7450943F));
            popularProductsHeaderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.2549019F));
            popularProductsHeaderLayout.Controls.Add(popularProductsTitleLabel, 0, 0);
            popularProductsHeaderLayout.Controls.Add(btnColumns, 1, 0);
            popularProductsHeaderLayout.Location = new Point(5, 5);
            popularProductsHeaderLayout.Name = "popularProductsHeaderLayout";
            popularProductsHeaderLayout.RowCount = 1;
            popularProductsHeaderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            popularProductsHeaderLayout.Size = new Size(255, 39);
            popularProductsHeaderLayout.TabIndex = 0;
            // 
            // popularProductsTitleLabel
            // 
            popularProductsTitleLabel.AutoSize = true;
            popularProductsTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            popularProductsTitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            popularProductsTitleLabel.Location = new Point(5, 5);
            popularProductsTitleLabel.Margin = new Padding(5);
            popularProductsTitleLabel.Name = "popularProductsTitleLabel";
            popularProductsTitleLabel.Size = new Size(182, 23);
            popularProductsTitleLabel.TabIndex = 0;
            popularProductsTitleLabel.Text = "Популярные товары";
            // 
            // btnColumns
            // 
            btnColumns.BackColor = Color.FromArgb(25, 118, 210);
            btnColumns.FlatAppearance.BorderSize = 0;
            btnColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnColumns.FlatStyle = FlatStyle.Flat;
            btnColumns.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnColumns.ForeColor = Color.White;
            btnColumns.Location = new Point(214, 3);
            btnColumns.Name = "btnColumns";
            btnColumns.Size = new Size(38, 31);
            btnColumns.TabIndex = 1;
            btnColumns.Text = "☰";
            btnColumns.UseVisualStyleBackColor = false;
            btnColumns.Click += BtnColumns_Click;
            // 
            // popularProductsGrid
            // 
            popularProductsGrid.AllowUserToAddRows = false;
            popularProductsGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            popularProductsGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            popularProductsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            popularProductsGrid.BackgroundColor = Color.White;
            popularProductsGrid.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(240, 243, 245);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(33, 37, 41);
            popularProductsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            popularProductsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            popularProductsGrid.DefaultCellStyle = dataGridViewCellStyle3;
            popularProductsGrid.Location = new Point(5, 50);
            popularProductsGrid.Name = "popularProductsGrid";
            popularProductsGrid.ReadOnly = true;
            popularProductsGrid.RowHeadersVisible = false;
            popularProductsGrid.RowHeadersWidth = 51;
            popularProductsGrid.Size = new Size(255, 667);
            popularProductsGrid.TabIndex = 2;
            popularProductsGrid.CellClick += PopularProductsGrid_CellClick;
            popularProductsGrid.CellDoubleClick += PopularProductsGrid_CellDoubleClick;
            // 
            // separatorLeftMiddle
            // 
            separatorLeftMiddle.BackColor = Color.FromArgb(180, 180, 180);
            separatorLeftMiddle.Dock = DockStyle.Fill;
            separatorLeftMiddle.Location = new Point(283, 65);
            separatorLeftMiddle.Name = "separatorLeftMiddle";
            separatorLeftMiddle.Size = new Size(1, 722);
            separatorLeftMiddle.TabIndex = 2;
            // 
            // middlePanel
            // 
            middlePanel.ColumnCount = 1;
            middlePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            middlePanel.Controls.Add(middleHeaderPanel, 0, 0);
            middlePanel.Controls.Add(selectedProductsGrid, 0, 1);
            middlePanel.Dock = DockStyle.Fill;
            middlePanel.Location = new Point(285, 65);
            middlePanel.Name = "middlePanel";
            middlePanel.Padding = new Padding(5);
            middlePanel.RowCount = 2;
            middlePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            middlePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            middlePanel.Size = new Size(686, 722);
            middlePanel.TabIndex = 5;
            // 
            // middleHeaderPanel
            // 
            middleHeaderPanel.ColumnCount = 2;
            middleHeaderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.9298248F));
            middleHeaderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.0701752F));
            middleHeaderPanel.Controls.Add(selectedProductsTitleLabel, 0, 0);
            middleHeaderPanel.Controls.Add(quickButtonsPanel, 1, 0);
            middleHeaderPanel.Dock = DockStyle.Top;
            middleHeaderPanel.Location = new Point(8, 8);
            middleHeaderPanel.Name = "middleHeaderPanel";
            middleHeaderPanel.Padding = new Padding(0, 0, 0, 5);
            middleHeaderPanel.RowCount = 1;
            middleHeaderPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            middleHeaderPanel.Size = new Size(670, 44);
            middleHeaderPanel.TabIndex = 0;
            // 
            // selectedProductsTitleLabel
            // 
            selectedProductsTitleLabel.AutoSize = true;
            selectedProductsTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            selectedProductsTitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            selectedProductsTitleLabel.Location = new Point(5, 5);
            selectedProductsTitleLabel.Margin = new Padding(5, 5, 5, 10);
            selectedProductsTitleLabel.Name = "selectedProductsTitleLabel";
            selectedProductsTitleLabel.Size = new Size(175, 23);
            selectedProductsTitleLabel.TabIndex = 0;
            selectedProductsTitleLabel.Text = "Выбранные товары";
            // 
            // quickButtonsPanel
            // 
            quickButtonsPanel.Controls.Add(btnAddProduct);
            quickButtonsPanel.Controls.Add(button1);
            quickButtonsPanel.Controls.Add(btnSearch);
            quickButtonsPanel.Controls.Add(btnColumnsSl);
            quickButtonsPanel.Dock = DockStyle.Fill;
            quickButtonsPanel.FlowDirection = FlowDirection.RightToLeft;
            quickButtonsPanel.Location = new Point(216, 3);
            quickButtonsPanel.Name = "quickButtonsPanel";
            quickButtonsPanel.Size = new Size(451, 33);
            quickButtonsPanel.TabIndex = 1;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Anchor = AnchorStyles.Right;
            btnAddProduct.BackColor = Color.FromArgb(40, 167, 69);
            btnAddProduct.FlatAppearance.BorderSize = 0;
            btnAddProduct.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnAddProduct.FlatStyle = FlatStyle.Flat;
            btnAddProduct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddProduct.ForeColor = Color.White;
            btnAddProduct.Location = new Point(309, 3);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(139, 30);
            btnAddProduct.TabIndex = 1;
            btnAddProduct.Text = "Новая продажа";
            btnAddProduct.UseVisualStyleBackColor = false;
            btnAddProduct.Click += BtnAddProduct_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(220, 53, 69);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 77, 77);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(215, 3);
            button1.Name = "button1";
            button1.Size = new Size(88, 31);
            button1.TabIndex = 2;
            button1.Text = "Удалить";
            button1.UseVisualStyleBackColor = false;
            button1.Click += Button1_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(25, 118, 210);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(121, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(88, 30);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Добавить";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += BtnSearch_Click;
            // 
            // btnColumnsSl
            // 
            btnColumnsSl.BackColor = Color.FromArgb(25, 118, 210);
            btnColumnsSl.FlatAppearance.BorderSize = 0;
            btnColumnsSl.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnColumnsSl.FlatStyle = FlatStyle.Flat;
            btnColumnsSl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnColumnsSl.ForeColor = Color.White;
            btnColumnsSl.Location = new Point(75, 3);
            btnColumnsSl.Name = "btnColumnsSl";
            btnColumnsSl.Size = new Size(40, 30);
            btnColumnsSl.TabIndex = 3;
            btnColumnsSl.Text = "☰";
            btnColumnsSl.UseVisualStyleBackColor = false;
            btnColumnsSl.Click += BtnColumnsSl_Click;
            // 
            // selectedProductsGrid
            // 
            selectedProductsGrid.AllowUserToAddRows = false;
            selectedProductsGrid.AllowUserToDeleteRows = false;
            selectedProductsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            selectedProductsGrid.BackgroundColor = Color.White;
            selectedProductsGrid.BorderStyle = BorderStyle.None;
            selectedProductsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            selectedProductsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            selectedProductsGrid.DefaultCellStyle = dataGridViewCellStyle4;
            selectedProductsGrid.Location = new Point(8, 58);
            selectedProductsGrid.Name = "selectedProductsGrid";
            selectedProductsGrid.RowHeadersWidth = 51;
            selectedProductsGrid.Size = new Size(670, 656);
            selectedProductsGrid.TabIndex = 1;
            selectedProductsGrid.CellClick += SelectedProductsGrid_CellClick;
            selectedProductsGrid.CellValidating += SelectedProductsGrid_CellValidating;
            selectedProductsGrid.DataError += selectedProductsGrid_DataError;
            // 
            // separatorMiddleRight
            // 
            separatorMiddleRight.BackColor = Color.FromArgb(180, 180, 180);
            separatorMiddleRight.Dock = DockStyle.Fill;
            separatorMiddleRight.Location = new Point(977, 65);
            separatorMiddleRight.Name = "separatorMiddleRight";
            separatorMiddleRight.Size = new Size(1, 722);
            separatorMiddleRight.TabIndex = 3;
            // 
            // rightPanel
            // 
            rightPanel.ColumnCount = 1;
            rightPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightPanel.Controls.Add(incompleteSalesPanel, 0, 0);
            rightPanel.Controls.Add(separatorProductInfo, 0, 1);
            rightPanel.Controls.Add(selectedProductInfoPanel, 0, 2);
            rightPanel.Controls.Add(separatorKeypadTop, 0, 3);
            rightPanel.Controls.Add(inputPanel, 0, 4);
            rightPanel.Controls.Add(totalAmountLabel, 0, 5);
            rightPanel.Controls.Add(separatorKeypadBottom, 0, 6);
            rightPanel.Controls.Add(numericKeypadPanel, 0, 7);
            rightPanel.Controls.Add(separatorKeypadBottom2, 0, 8);
            rightPanel.Controls.Add(buttonsPanel, 0, 9);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(979, 65);
            rightPanel.Name = "rightPanel";
            rightPanel.Padding = new Padding(5);
            rightPanel.RowCount = 10;
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            rightPanel.Size = new Size(408, 722);
            rightPanel.TabIndex = 6;
            // 
            // incompleteSalesPanel
            // 
            incompleteSalesPanel.Controls.Add(incompleteSalesTitleLabel);
            incompleteSalesPanel.Controls.Add(incompleteSalesFlow);
            incompleteSalesPanel.Dock = DockStyle.Top;
            incompleteSalesPanel.Location = new Point(8, 8);
            incompleteSalesPanel.Name = "incompleteSalesPanel";
            incompleteSalesPanel.Padding = new Padding(5);
            incompleteSalesPanel.Size = new Size(392, 90);
            incompleteSalesPanel.TabIndex = 0;
            // 
            // incompleteSalesTitleLabel
            // 
            incompleteSalesTitleLabel.AutoSize = true;
            incompleteSalesTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            incompleteSalesTitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            incompleteSalesTitleLabel.Location = new Point(5, 5);
            incompleteSalesTitleLabel.Margin = new Padding(5, 5, 5, 10);
            incompleteSalesTitleLabel.Name = "incompleteSalesTitleLabel";
            incompleteSalesTitleLabel.Size = new Size(227, 23);
            incompleteSalesTitleLabel.TabIndex = 0;
            incompleteSalesTitleLabel.Text = "Незавершенные продажи";
            // 
            // incompleteSalesFlow
            // 
            incompleteSalesFlow.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            incompleteSalesFlow.AutoScroll = true;
            incompleteSalesFlow.Location = new Point(5, 30);
            incompleteSalesFlow.Name = "incompleteSalesFlow";
            incompleteSalesFlow.Size = new Size(379, 52);
            incompleteSalesFlow.TabIndex = 1;
            incompleteSalesFlow.WrapContents = false;
            // 
            // separatorProductInfo
            // 
            separatorProductInfo.BackColor = Color.FromArgb(180, 180, 180);
            separatorProductInfo.Dock = DockStyle.Top;
            separatorProductInfo.Location = new Point(8, 104);
            separatorProductInfo.Name = "separatorProductInfo";
            separatorProductInfo.Size = new Size(392, 1);
            separatorProductInfo.TabIndex = 1;
            // 
            // selectedProductInfoPanel
            // 
            selectedProductInfoPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            selectedProductInfoPanel.Controls.Add(selectedProductInfoLabel);
            selectedProductInfoPanel.Location = new Point(8, 106);
            selectedProductInfoPanel.Name = "selectedProductInfoPanel";
            selectedProductInfoPanel.Padding = new Padding(5);
            selectedProductInfoPanel.Size = new Size(392, 216);
            selectedProductInfoPanel.TabIndex = 2;
            // 
            // selectedProductInfoLabel
            // 
            selectedProductInfoLabel.AutoSize = true;
            selectedProductInfoLabel.Font = new Font("Segoe UI", 10F);
            selectedProductInfoLabel.ForeColor = Color.FromArgb(33, 37, 41);
            selectedProductInfoLabel.Location = new Point(5, 5);
            selectedProductInfoLabel.Margin = new Padding(5, 5, 5, 10);
            selectedProductInfoLabel.Name = "selectedProductInfoLabel";
            selectedProductInfoLabel.Size = new Size(282, 23);
            selectedProductInfoLabel.TabIndex = 0;
            selectedProductInfoLabel.Text = "Информация о товаре отсутствует";
            // 
            // separatorKeypadTop
            // 
            separatorKeypadTop.BackColor = Color.FromArgb(180, 180, 180);
            separatorKeypadTop.Dock = DockStyle.Top;
            separatorKeypadTop.Location = new Point(8, 328);
            separatorKeypadTop.Name = "separatorKeypadTop";
            separatorKeypadTop.Size = new Size(392, 1);
            separatorKeypadTop.TabIndex = 3;
            // 
            // inputPanel
            // 
            inputPanel.ColumnCount = 2;
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            inputPanel.Controls.Add(paidAmountTitleLabel, 0, 0);
            inputPanel.Controls.Add(paidAmountTextBox, 0, 1);
            inputPanel.Controls.Add(customerSearchTitleLabel, 1, 0);
            inputPanel.Controls.Add(comboBox1, 1, 1);
            inputPanel.Dock = DockStyle.Top;
            inputPanel.Location = new Point(8, 330);
            inputPanel.Name = "inputPanel";
            inputPanel.Padding = new Padding(5);
            inputPanel.RowCount = 2;
            inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            inputPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            inputPanel.Size = new Size(392, 70);
            inputPanel.TabIndex = 4;
            // 
            // paidAmountTitleLabel
            // 
            paidAmountTitleLabel.AutoSize = true;
            paidAmountTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            paidAmountTitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            paidAmountTitleLabel.Location = new Point(10, 10);
            paidAmountTitleLabel.Margin = new Padding(5);
            paidAmountTitleLabel.Name = "paidAmountTitleLabel";
            paidAmountTitleLabel.Size = new Size(167, 20);
            paidAmountTitleLabel.TabIndex = 0;
            paidAmountTitleLabel.Text = "Оплаченная сумма";
            // 
            // paidAmountTextBox
            // 
            paidAmountTextBox.BorderStyle = BorderStyle.FixedSingle;
            paidAmountTextBox.Font = new Font("Segoe UI", 10F);
            paidAmountTextBox.Location = new Point(8, 38);
            paidAmountTextBox.Name = "paidAmountTextBox";
            paidAmountTextBox.Size = new Size(150, 30);
            paidAmountTextBox.TabIndex = 1;
            paidAmountTextBox.Text = "0";
            paidAmountTextBox.Enter += PaidAmountTextBox_Enter;
            // 
            // customerSearchTitleLabel
            // 
            customerSearchTitleLabel.AutoSize = true;
            customerSearchTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            customerSearchTitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            customerSearchTitleLabel.Location = new Point(201, 10);
            customerSearchTitleLabel.Margin = new Padding(5);
            customerSearchTitleLabel.Name = "customerSearchTitleLabel";
            customerSearchTitleLabel.Size = new Size(146, 20);
            customerSearchTitleLabel.TabIndex = 2;
            customerSearchTitleLabel.Text = "Имя покупателя";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(199, 38);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(185, 28);
            comboBox1.TabIndex = 3;
            // 
            // totalAmountLabel
            // 
            totalAmountLabel.AutoSize = true;
            totalAmountLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            totalAmountLabel.ForeColor = Color.ForestGreen;
            totalAmountLabel.Location = new Point(10, 410);
            totalAmountLabel.Margin = new Padding(5, 5, 5, 10);
            totalAmountLabel.Name = "totalAmountLabel";
            totalAmountLabel.Size = new Size(172, 28);
            totalAmountLabel.TabIndex = 5;
            totalAmountLabel.Text = "Общая сумма: 0";
            // 
            // separatorKeypadBottom
            // 
            separatorKeypadBottom.BackColor = Color.FromArgb(180, 180, 180);
            separatorKeypadBottom.Dock = DockStyle.Top;
            separatorKeypadBottom.Location = new Point(8, 456);
            separatorKeypadBottom.Name = "separatorKeypadBottom";
            separatorKeypadBottom.Size = new Size(392, 1);
            separatorKeypadBottom.TabIndex = 6;
            // 
            // numericKeypadPanel
            // 
            numericKeypadPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            numericKeypadPanel.Controls.Add(numericKeypadLayout);
            numericKeypadPanel.Location = new Point(38, 458);
            numericKeypadPanel.Name = "numericKeypadPanel";
            numericKeypadPanel.Padding = new Padding(5);
            numericKeypadPanel.Size = new Size(331, 194);
            numericKeypadPanel.TabIndex = 7;
            // 
            // numericKeypadLayout
            // 
            numericKeypadLayout.ColumnCount = 3;
            numericKeypadLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            numericKeypadLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            numericKeypadLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            numericKeypadLayout.Controls.Add(btnNum1, 0, 0);
            numericKeypadLayout.Controls.Add(btnNum2, 1, 0);
            numericKeypadLayout.Controls.Add(btnNum3, 2, 0);
            numericKeypadLayout.Controls.Add(btnNum4, 0, 1);
            numericKeypadLayout.Controls.Add(btnNum5, 1, 1);
            numericKeypadLayout.Controls.Add(btnNum6, 2, 1);
            numericKeypadLayout.Controls.Add(btnNum7, 0, 2);
            numericKeypadLayout.Controls.Add(btnNum8, 1, 2);
            numericKeypadLayout.Controls.Add(btnNum9, 2, 2);
            numericKeypadLayout.Controls.Add(btnNum0, 0, 3);
            numericKeypadLayout.Controls.Add(btnNumClear, 1, 3);
            numericKeypadLayout.Controls.Add(btnNumEnter, 2, 3);
            numericKeypadLayout.Location = new Point(5, 5);
            numericKeypadLayout.Name = "numericKeypadLayout";
            numericKeypadLayout.Padding = new Padding(5);
            numericKeypadLayout.RowCount = 4;
            numericKeypadLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            numericKeypadLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            numericKeypadLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            numericKeypadLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            numericKeypadLayout.Size = new Size(322, 190);
            numericKeypadLayout.TabIndex = 0;
            // 
            // btnNum1
            // 
            btnNum1.Location = new Point(8, 8);
            btnNum1.Name = "btnNum1";
            btnNum1.Size = new Size(98, 39);
            btnNum1.TabIndex = 0;
            btnNum1.Text = "1";
            btnNum1.Click += NumericButton_Click;
            // 
            // btnNum2
            // 
            btnNum2.Location = new Point(112, 8);
            btnNum2.Name = "btnNum2";
            btnNum2.Size = new Size(98, 39);
            btnNum2.TabIndex = 1;
            btnNum2.Text = "2";
            btnNum2.Click += NumericButton_Click;
            // 
            // btnNum3
            // 
            btnNum3.Location = new Point(216, 8);
            btnNum3.Name = "btnNum3";
            btnNum3.Size = new Size(98, 39);
            btnNum3.TabIndex = 2;
            btnNum3.Text = "3";
            btnNum3.Click += NumericButton_Click;
            // 
            // btnNum4
            // 
            btnNum4.Location = new Point(8, 53);
            btnNum4.Name = "btnNum4";
            btnNum4.Size = new Size(98, 39);
            btnNum4.TabIndex = 3;
            btnNum4.Text = "4";
            btnNum4.Click += NumericButton_Click;
            // 
            // btnNum5
            // 
            btnNum5.Location = new Point(112, 53);
            btnNum5.Name = "btnNum5";
            btnNum5.Size = new Size(98, 39);
            btnNum5.TabIndex = 4;
            btnNum5.Text = "5";
            btnNum5.Click += NumericButton_Click;
            // 
            // btnNum6
            // 
            btnNum6.Location = new Point(216, 53);
            btnNum6.Name = "btnNum6";
            btnNum6.Size = new Size(98, 39);
            btnNum6.TabIndex = 5;
            btnNum6.Text = "6";
            btnNum6.Click += NumericButton_Click;
            // 
            // btnNum7
            // 
            btnNum7.Location = new Point(8, 98);
            btnNum7.Name = "btnNum7";
            btnNum7.Size = new Size(98, 39);
            btnNum7.TabIndex = 6;
            btnNum7.Text = "7";
            btnNum7.Click += NumericButton_Click;
            // 
            // btnNum8
            // 
            btnNum8.Location = new Point(112, 98);
            btnNum8.Name = "btnNum8";
            btnNum8.Size = new Size(98, 39);
            btnNum8.TabIndex = 7;
            btnNum8.Text = "8";
            btnNum8.Click += NumericButton_Click;
            // 
            // btnNum9
            // 
            btnNum9.Location = new Point(216, 98);
            btnNum9.Name = "btnNum9";
            btnNum9.Size = new Size(98, 39);
            btnNum9.TabIndex = 8;
            btnNum9.Text = "9";
            btnNum9.Click += NumericButton_Click;
            // 
            // btnNum0
            // 
            btnNum0.Location = new Point(8, 143);
            btnNum0.Name = "btnNum0";
            btnNum0.Size = new Size(98, 39);
            btnNum0.TabIndex = 9;
            btnNum0.Text = "0";
            btnNum0.Click += NumericButton_Click;
            // 
            // btnNumClear
            // 
            btnNumClear.Location = new Point(112, 143);
            btnNumClear.Name = "btnNumClear";
            btnNumClear.Size = new Size(98, 39);
            btnNumClear.TabIndex = 10;
            btnNumClear.Text = "Очистить";
            btnNumClear.Click += NumericButton_Click;
            // 
            // btnNumEnter
            // 
            btnNumEnter.Location = new Point(216, 143);
            btnNumEnter.Name = "btnNumEnter";
            btnNumEnter.Size = new Size(98, 39);
            btnNumEnter.TabIndex = 11;
            btnNumEnter.Text = "Ввод";
            btnNumEnter.Click += NumericButton_Click;
            // 
            // separatorKeypadBottom2
            // 
            separatorKeypadBottom2.BackColor = Color.FromArgb(180, 180, 180);
            separatorKeypadBottom2.Dock = DockStyle.Top;
            separatorKeypadBottom2.Location = new Point(8, 658);
            separatorKeypadBottom2.Name = "separatorKeypadBottom2";
            separatorKeypadBottom2.Size = new Size(392, 1);
            separatorKeypadBottom2.TabIndex = 8;
            // 
            // buttonsPanel
            // 
            buttonsPanel.ColumnCount = 2;
            buttonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.59813F));
            buttonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.40187F));
            buttonsPanel.Controls.Add(btnContinue, 1, 0);
            buttonsPanel.Controls.Add(btnCancelClose, 0, 0);
            buttonsPanel.Dock = DockStyle.Top;
            buttonsPanel.Location = new Point(8, 660);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Padding = new Padding(5);
            buttonsPanel.RowCount = 1;
            buttonsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonsPanel.Size = new Size(392, 54);
            buttonsPanel.TabIndex = 9;
            // 
            // btnContinue
            // 
            btnContinue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnContinue.BackColor = Color.FromArgb(40, 167, 69);
            btnContinue.FlatAppearance.BorderSize = 0;
            btnContinue.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnContinue.FlatStyle = FlatStyle.Flat;
            btnContinue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnContinue.ForeColor = Color.White;
            btnContinue.Location = new Point(195, 8);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(189, 38);
            btnContinue.TabIndex = 0;
            btnContinue.Text = "Продолжить";
            btnContinue.UseVisualStyleBackColor = false;
            btnContinue.Click += BtnContinue_Click;
            // 
            // btnCancelClose
            // 
            btnCancelClose.BackColor = Color.FromArgb(220, 53, 69);
            btnCancelClose.FlatAppearance.BorderSize = 0;
            btnCancelClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 77, 77);
            btnCancelClose.FlatStyle = FlatStyle.Flat;
            btnCancelClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelClose.ForeColor = Color.White;
            btnCancelClose.Location = new Point(8, 8);
            btnCancelClose.Name = "btnCancelClose";
            btnCancelClose.Size = new Size(179, 38);
            btnCancelClose.TabIndex = 1;
            btnCancelClose.Text = "Отмена и Закрыть";
            btnCancelClose.UseVisualStyleBackColor = false;
            btnCancelClose.Click += BtnCancelClose_Click;
            // 
            // notificationPanel
            // 
            notificationPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            notificationPanel.BorderStyle = BorderStyle.FixedSingle;
            notificationPanel.Controls.Add(notificationLabel);
            notificationPanel.Location = new Point(979, 13);
            notificationPanel.Name = "notificationPanel";
            notificationPanel.Size = new Size(408, 44);
            notificationPanel.TabIndex = 1;
            // 
            // notificationLabel
            // 
            notificationLabel.AutoSize = true;
            notificationLabel.Font = new Font("Segoe UI", 10F);
            notificationLabel.ForeColor = Color.FromArgb(33, 37, 41);
            notificationLabel.Location = new Point(5, 5);
            notificationLabel.Name = "notificationLabel";
            notificationLabel.Size = new Size(0, 23);
            notificationLabel.TabIndex = 0;
            // 
            // SaleForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 800);
            Controls.Add(tableLayoutPanel);
            Name = "SaleForm";
            Text = "Продажи";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            popularProductsPanel.ResumeLayout(false);
            popularProductsHeaderLayout.ResumeLayout(false);
            popularProductsHeaderLayout.PerformLayout();
            ((ISupportInitialize)popularProductsGrid).EndInit();
            middlePanel.ResumeLayout(false);
            middleHeaderPanel.ResumeLayout(false);
            middleHeaderPanel.PerformLayout();
            quickButtonsPanel.ResumeLayout(false);
            ((ISupportInitialize)selectedProductsGrid).EndInit();
            rightPanel.ResumeLayout(false);
            rightPanel.PerformLayout();
            incompleteSalesPanel.ResumeLayout(false);
            incompleteSalesPanel.PerformLayout();
            selectedProductInfoPanel.ResumeLayout(false);
            selectedProductInfoPanel.PerformLayout();
            inputPanel.ResumeLayout(false);
            inputPanel.PerformLayout();
            numericKeypadPanel.ResumeLayout(false);
            numericKeypadLayout.ResumeLayout(false);
            buttonsPanel.ResumeLayout(false);
            notificationPanel.ResumeLayout(false);
            notificationPanel.PerformLayout();
            ResumeLayout(false);
        }

        private ToolTip toolTip;
        private Timer notificationTimer;
        private TableLayoutPanel tableLayoutPanel;
        private Label titleLabel;
        private Panel notificationPanel;
        private Label notificationLabel;
        private Panel separatorTop;
        private Panel popularProductsPanel;
        private TableLayoutPanel popularProductsHeaderLayout;
        private Label popularProductsTitleLabel;
        private Button btnColumns;
        private DataGridView popularProductsGrid;
        private Panel separatorLeftMiddle;
        private TableLayoutPanel middlePanel;
        private TableLayoutPanel middleHeaderPanel;
        private Label selectedProductsTitleLabel;
        private FlowLayoutPanel quickButtonsPanel;
        private Button btnAddProduct;
        private Button button1;
        private Button btnSearch;
        private Button btnColumnsSl;
        private DataGridView selectedProductsGrid;
        private Panel separatorMiddleRight;
        private TableLayoutPanel rightPanel;
        private Panel incompleteSalesPanel;
        private Label incompleteSalesTitleLabel;
        private FlowLayoutPanel incompleteSalesFlow;
        private Panel separatorProductInfo;
        private Panel selectedProductInfoPanel;
        private Label selectedProductInfoLabel;
        private Panel separatorKeypadTop;
        private TableLayoutPanel inputPanel;
        private Label paidAmountTitleLabel;
        private TextBox paidAmountTextBox;
        private Label customerSearchTitleLabel;
        private Label totalAmountLabel;
        private Panel separatorKeypadBottom;
        private Panel numericKeypadPanel;
        private TableLayoutPanel numericKeypadLayout;
        private Button btnNum1;
        private Button btnNum2;
        private Button btnNum3;
        private Button btnNum4;
        private Button btnNum5;
        private Button btnNum6;
        private Button btnNum7;
        private Button btnNum8;
        private Button btnNum9;
        private Button btnNum0;
        private Button btnNumClear;
        private Button btnNumEnter;
        private Panel separatorKeypadBottom2;
        private TableLayoutPanel buttonsPanel;
        private Button btnContinue;
        private Button btnCancelClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private ComboBox comboBox1;
        private Label customerInfoLbl;
    }
}
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
    public partial class ServiceOrderForm
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
            clientInfoLbl = new Label();
            titleLabel = new Label();
            separatorTop = new Panel();
            popularServicesPanel = new Panel();
            popularServicesHeaderLayout = new TableLayoutPanel();
            popularServicesTitleLabel = new Label();
            btnColumns = new Button();
            popularServicesGrid = new DataGridView();
            separatorLeftMiddle = new Panel();
            middlePanel = new TableLayoutPanel();
            middleHeaderPanel = new TableLayoutPanel();
            selectedServicesTitleLabel = new Label();
            quickButtonsPanel = new FlowLayoutPanel();
            btnAddService = new Button();
            btnRemoveService = new Button();
            btnSearchService = new Button();
            btnColumnsSl = new Button();
            selectedServicesGrid = new DataGridView();
            separatorMiddleRight = new Panel();
            rightPanel = new TableLayoutPanel();
            incompleteOrdersPanel = new Panel();
            incompleteOrdersTitleLabel = new Label();
            incompleteOrdersFlow = new FlowLayoutPanel();
            separatorServiceInfo = new Panel();
            selectedServiceInfoPanel = new Panel();
            selectedServiceInfoLabel = new Label();
            separatorKeypadTop = new Panel();
            inputPanel = new TableLayoutPanel();
            paidAmountTitleLabel = new Label();
            paidAmountTextBox = new TextBox();
            clientSearchTitleLabel = new Label();
            clientComboBox = new ComboBox();
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
            btnConfirmOrder = new Button();
            btnCancelOrder = new Button();
            notificationPanel = new Panel();
            notificationLabel = new Label();
            tableLayoutPanel.SuspendLayout();
            popularServicesPanel.SuspendLayout();
            popularServicesHeaderLayout.SuspendLayout();
            ((ISupportInitialize)popularServicesGrid).BeginInit();
            middlePanel.SuspendLayout();
            middleHeaderPanel.SuspendLayout();
            quickButtonsPanel.SuspendLayout();
            ((ISupportInitialize)selectedServicesGrid).BeginInit();
            rightPanel.SuspendLayout();
            incompleteOrdersPanel.SuspendLayout();
            selectedServiceInfoPanel.SuspendLayout();
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
            tableLayoutPanel.Controls.Add(clientInfoLbl, 2, 0);
            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(separatorTop, 0, 1);
            tableLayoutPanel.Controls.Add(popularServicesPanel, 0, 2);
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
            // clientInfoLbl
            // 
            clientInfoLbl.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(clientInfoLbl, 2);
            clientInfoLbl.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            clientInfoLbl.ForeColor = Color.FromArgb(33, 37, 41);
            clientInfoLbl.Location = new Point(292, 20);
            clientInfoLbl.Margin = new Padding(10);
            clientInfoLbl.Name = "clientInfoLbl";
            clientInfoLbl.Size = new Size(268, 30);
            clientInfoLbl.TabIndex = 7;
            clientInfoLbl.Text = "Панель заказов услуг";
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
            titleLabel.Size = new Size(206, 30);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Панель заказов услуг";
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
            // popularServicesPanel
            // 
            popularServicesPanel.Controls.Add(popularServicesHeaderLayout);
            popularServicesPanel.Controls.Add(popularServicesGrid);
            popularServicesPanel.Dock = DockStyle.Fill;
            popularServicesPanel.Location = new Point(13, 65);
            popularServicesPanel.Name = "popularServicesPanel";
            popularServicesPanel.Padding = new Padding(5);
            popularServicesPanel.Size = new Size(264, 722);
            popularServicesPanel.TabIndex = 4;
            // 
            // popularServicesHeaderLayout
            // 
            popularServicesHeaderLayout.ColumnCount = 2;
            popularServicesHeaderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82.7450943F));
            popularServicesHeaderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.2549019F));
            popularServicesHeaderLayout.Controls.Add(popularServicesTitleLabel, 0, 0);
            popularServicesHeaderLayout.Controls.Add(btnColumns, 1, 0);
            popularServicesHeaderLayout.Location = new Point(5, 5);
            popularServicesHeaderLayout.Name = "popularServicesHeaderLayout";
            popularServicesHeaderLayout.RowCount = 1;
            popularServicesHeaderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            popularServicesHeaderLayout.Size = new Size(255, 39);
            popularServicesHeaderLayout.TabIndex = 0;
            // 
            // popularServicesTitleLabel
            // 
            popularServicesTitleLabel.AutoSize = true;
            popularServicesTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            popularServicesTitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            popularServicesTitleLabel.Location = new Point(5, 5);
            popularServicesTitleLabel.Margin = new Padding(5);
            popularServicesTitleLabel.Name = "popularServicesTitleLabel";
            popularServicesTitleLabel.Size = new Size(174, 23);
            popularServicesTitleLabel.TabIndex = 0;
            popularServicesTitleLabel.Text = "Популярные услуги";
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
            // popularServicesGrid
            // 
            popularServicesGrid.AllowUserToAddRows = false;
            popularServicesGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            popularServicesGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            popularServicesGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            popularServicesGrid.BackgroundColor = Color.White;
            popularServicesGrid.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(240, 243, 245);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(33, 37, 41);
            popularServicesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            popularServicesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            popularServicesGrid.DefaultCellStyle = dataGridViewCellStyle3;
            popularServicesGrid.Location = new Point(5, 50);
            popularServicesGrid.Name = "popularServicesGrid";
            popularServicesGrid.ReadOnly = true;
            popularServicesGrid.RowHeadersVisible = false;
            popularServicesGrid.RowHeadersWidth = 51;
            popularServicesGrid.Size = new Size(255, 667);
            popularServicesGrid.TabIndex = 2;
            popularServicesGrid.CellClick += PopularServicesGrid_CellClick;
            popularServicesGrid.CellDoubleClick += PopularServicesGrid_CellDoubleClick;
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
            middlePanel.Controls.Add(selectedServicesGrid, 0, 1);
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
            middleHeaderPanel.Controls.Add(selectedServicesTitleLabel, 0, 0);
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
            // selectedServicesTitleLabel
            // 
            selectedServicesTitleLabel.AutoSize = true;
            selectedServicesTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            selectedServicesTitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            selectedServicesTitleLabel.Location = new Point(5, 5);
            selectedServicesTitleLabel.Margin = new Padding(5, 5, 5, 10);
            selectedServicesTitleLabel.Name = "selectedServicesTitleLabel";
            selectedServicesTitleLabel.Size = new Size(167, 23);
            selectedServicesTitleLabel.TabIndex = 0;
            selectedServicesTitleLabel.Text = "Выбранные услуги";
            // 
            // quickButtonsPanel
            // 
            quickButtonsPanel.Controls.Add(btnAddService);
            quickButtonsPanel.Controls.Add(btnRemoveService);
            quickButtonsPanel.Controls.Add(btnSearchService);
            quickButtonsPanel.Controls.Add(btnColumnsSl);
            quickButtonsPanel.Dock = DockStyle.Fill;
            quickButtonsPanel.FlowDirection = FlowDirection.RightToLeft;
            quickButtonsPanel.Location = new Point(216, 3);
            quickButtonsPanel.Name = "quickButtonsPanel";
            quickButtonsPanel.Size = new Size(451, 33);
            quickButtonsPanel.TabIndex = 1;
            // 
            // btnAddService
            // 
            btnAddService.Anchor = AnchorStyles.Right;
            btnAddService.BackColor = Color.FromArgb(40, 167, 69);
            btnAddService.FlatAppearance.BorderSize = 0;
            btnAddService.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnAddService.FlatStyle = FlatStyle.Flat;
            btnAddService.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddService.ForeColor = Color.White;
            btnAddService.Location = new Point(309, 3);
            btnAddService.Name = "btnAddService";
            btnAddService.Size = new Size(139, 30);
            btnAddService.TabIndex = 1;
            btnAddService.Text = "Новый заказ";
            btnAddService.UseVisualStyleBackColor = false;
            btnAddService.Click += BtnAddService_Click;
            // 
            // btnRemoveService
            // 
            btnRemoveService.BackColor = Color.FromArgb(220, 53, 69);
            btnRemoveService.FlatAppearance.BorderSize = 0;
            btnRemoveService.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 77, 77);
            btnRemoveService.FlatStyle = FlatStyle.Flat;
            btnRemoveService.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRemoveService.ForeColor = Color.White;
            btnRemoveService.Location = new Point(215, 3);
            btnRemoveService.Name = "btnRemoveService";
            btnRemoveService.Size = new Size(88, 31);
            btnRemoveService.TabIndex = 2;
            btnRemoveService.Text = "Удалить";
            btnRemoveService.UseVisualStyleBackColor = false;
            btnRemoveService.Click += BtnRemoveService_Click;
            // 
            // btnSearchService
            // 
            btnSearchService.BackColor = Color.FromArgb(25, 118, 210);
            btnSearchService.FlatAppearance.BorderSize = 0;
            btnSearchService.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnSearchService.FlatStyle = FlatStyle.Flat;
            btnSearchService.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearchService.ForeColor = Color.White;
            btnSearchService.Location = new Point(121, 3);
            btnSearchService.Name = "btnSearchService";
            btnSearchService.Size = new Size(88, 30);
            btnSearchService.TabIndex = 0;
            btnSearchService.Text = "Добавить";
            btnSearchService.UseVisualStyleBackColor = false;
            btnSearchService.Click += BtnSearchService_Click;
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
            // selectedServicesGrid
            // 
            selectedServicesGrid.AllowUserToAddRows = false;
            selectedServicesGrid.AllowUserToDeleteRows = false;
            selectedServicesGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            selectedServicesGrid.BackgroundColor = Color.White;
            selectedServicesGrid.BorderStyle = BorderStyle.None;
            selectedServicesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            selectedServicesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            selectedServicesGrid.DefaultCellStyle = dataGridViewCellStyle4;
            selectedServicesGrid.Location = new Point(8, 58);
            selectedServicesGrid.Name = "selectedServicesGrid";
            selectedServicesGrid.RowHeadersWidth = 51;
            selectedServicesGrid.Size = new Size(670, 656);
            selectedServicesGrid.TabIndex = 1;
            selectedServicesGrid.CellClick += SelectedServicesGrid_CellClick;
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
            rightPanel.Controls.Add(incompleteOrdersPanel, 0, 0);
            rightPanel.Controls.Add(separatorServiceInfo, 0, 1);
            rightPanel.Controls.Add(selectedServiceInfoPanel, 0, 2);
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
            // incompleteOrdersPanel
            // 
            incompleteOrdersPanel.Controls.Add(incompleteOrdersTitleLabel);
            incompleteOrdersPanel.Controls.Add(incompleteOrdersFlow);
            incompleteOrdersPanel.Dock = DockStyle.Top;
            incompleteOrdersPanel.Location = new Point(8, 8);
            incompleteOrdersPanel.Name = "incompleteOrdersPanel";
            incompleteOrdersPanel.Padding = new Padding(5);
            incompleteOrdersPanel.Size = new Size(392, 90);
            incompleteOrdersPanel.TabIndex = 0;
            // 
            // incompleteOrdersTitleLabel
            // 
            incompleteOrdersTitleLabel.AutoSize = true;
            incompleteOrdersTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            incompleteOrdersTitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            incompleteOrdersTitleLabel.Location = new Point(5, 5);
            incompleteOrdersTitleLabel.Margin = new Padding(5, 5, 5, 10);
            incompleteOrdersTitleLabel.Name = "incompleteOrdersTitleLabel";
            incompleteOrdersTitleLabel.Size = new Size(209, 23);
            incompleteOrdersTitleLabel.TabIndex = 0;
            incompleteOrdersTitleLabel.Text = "Незавершенные заказы";
            // 
            // incompleteOrdersFlow
            // 
            incompleteOrdersFlow.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            incompleteOrdersFlow.AutoScroll = true;
            incompleteOrdersFlow.Location = new Point(5, 30);
            incompleteOrdersFlow.Name = "incompleteOrdersFlow";
            incompleteOrdersFlow.Size = new Size(379, 52);
            incompleteOrdersFlow.TabIndex = 1;
            incompleteOrdersFlow.WrapContents = false;
            // 
            // separatorServiceInfo
            // 
            separatorServiceInfo.BackColor = Color.FromArgb(180, 180, 180);
            separatorServiceInfo.Dock = DockStyle.Top;
            separatorServiceInfo.Location = new Point(8, 104);
            separatorServiceInfo.Name = "separatorServiceInfo";
            separatorServiceInfo.Size = new Size(392, 1);
            separatorServiceInfo.TabIndex = 1;
            // 
            // selectedServiceInfoPanel
            // 
            selectedServiceInfoPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            selectedServiceInfoPanel.Controls.Add(selectedServiceInfoLabel);
            selectedServiceInfoPanel.Location = new Point(8, 106);
            selectedServiceInfoPanel.Name = "selectedServiceInfoPanel";
            selectedServiceInfoPanel.Padding = new Padding(5);
            selectedServiceInfoPanel.Size = new Size(392, 216);
            selectedServiceInfoPanel.TabIndex = 2;
            // 
            // selectedServiceInfoLabel
            // 
            selectedServiceInfoLabel.AutoSize = true;
            selectedServiceInfoLabel.Font = new Font("Segoe UI", 10F);
            selectedServiceInfoLabel.ForeColor = Color.FromArgb(33, 37, 41);
            selectedServiceInfoLabel.Location = new Point(5, 5);
            selectedServiceInfoLabel.Margin = new Padding(5, 5, 5, 10);
            selectedServiceInfoLabel.Name = "selectedServiceInfoLabel";
            selectedServiceInfoLabel.Size = new Size(287, 23);
            selectedServiceInfoLabel.TabIndex = 0;
            selectedServiceInfoLabel.Text = "Информация об услуге отсутствует";
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
            inputPanel.Controls.Add(clientSearchTitleLabel, 1, 0);
            inputPanel.Controls.Add(clientComboBox, 1, 1);
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
            // 
            // clientSearchTitleLabel
            // 
            clientSearchTitleLabel.AutoSize = true;
            clientSearchTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            clientSearchTitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            clientSearchTitleLabel.Location = new Point(201, 10);
            clientSearchTitleLabel.Margin = new Padding(5);
            clientSearchTitleLabel.Name = "clientSearchTitleLabel";
            clientSearchTitleLabel.Size = new Size(118, 20);
            clientSearchTitleLabel.TabIndex = 2;
            clientSearchTitleLabel.Text = "Имя клиента";
            // 
            // clientComboBox
            // 
            clientComboBox.FormattingEnabled = true;
            clientComboBox.Location = new Point(199, 38);
            clientComboBox.Name = "clientComboBox";
            clientComboBox.Size = new Size(185, 28);
            clientComboBox.TabIndex = 3;
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
            buttonsPanel.Controls.Add(btnConfirmOrder, 1, 0);
            buttonsPanel.Controls.Add(btnCancelOrder, 0, 0);
            buttonsPanel.Dock = DockStyle.Top;
            buttonsPanel.Location = new Point(8, 660);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Padding = new Padding(5);
            buttonsPanel.RowCount = 1;
            buttonsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonsPanel.Size = new Size(392, 54);
            buttonsPanel.TabIndex = 9;
            // 
            // btnConfirmOrder
            // 
            btnConfirmOrder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConfirmOrder.BackColor = Color.FromArgb(40, 167, 69);
            btnConfirmOrder.FlatAppearance.BorderSize = 0;
            btnConfirmOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnConfirmOrder.FlatStyle = FlatStyle.Flat;
            btnConfirmOrder.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnConfirmOrder.ForeColor = Color.White;
            btnConfirmOrder.Location = new Point(195, 8);
            btnConfirmOrder.Name = "btnConfirmOrder";
            btnConfirmOrder.Size = new Size(189, 38);
            btnConfirmOrder.TabIndex = 0;
            btnConfirmOrder.Text = "Подтвердить";
            btnConfirmOrder.UseVisualStyleBackColor = false;
            btnConfirmOrder.Click += BtnConfirmOrder_Click;
            // 
            // btnCancelOrder
            // 
            btnCancelOrder.BackColor = Color.FromArgb(220, 53, 69);
            btnCancelOrder.FlatAppearance.BorderSize = 0;
            btnCancelOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 77, 77);
            btnCancelOrder.FlatStyle = FlatStyle.Flat;
            btnCancelOrder.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelOrder.ForeColor = Color.White;
            btnCancelOrder.Location = new Point(8, 8);
            btnCancelOrder.Name = "btnCancelOrder";
            btnCancelOrder.Size = new Size(179, 38);
            btnCancelOrder.TabIndex = 1;
            btnCancelOrder.Text = "Отменить";
            btnCancelOrder.UseVisualStyleBackColor = false;
            btnCancelOrder.Click += BtnCancelOrder_Click;
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
            // ServiceOrderForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 800);
            Controls.Add(tableLayoutPanel);
            Name = "ServiceOrderForm";
            Text = "Заказы услуг";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            popularServicesPanel.ResumeLayout(false);
            popularServicesHeaderLayout.ResumeLayout(false);
            popularServicesHeaderLayout.PerformLayout();
            ((ISupportInitialize)popularServicesGrid).EndInit();
            middlePanel.ResumeLayout(false);
            middleHeaderPanel.ResumeLayout(false);
            middleHeaderPanel.PerformLayout();
            quickButtonsPanel.ResumeLayout(false);
            ((ISupportInitialize)selectedServicesGrid).EndInit();
            rightPanel.ResumeLayout(false);
            rightPanel.PerformLayout();
            incompleteOrdersPanel.ResumeLayout(false);
            incompleteOrdersPanel.PerformLayout();
            selectedServiceInfoPanel.ResumeLayout(false);
            selectedServiceInfoPanel.PerformLayout();
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
        private Panel popularServicesPanel;
        private TableLayoutPanel popularServicesHeaderLayout;
        private Label popularServicesTitleLabel;
        private Button btnColumns;
        private DataGridView popularServicesGrid;
        private Panel separatorLeftMiddle;
        private TableLayoutPanel middlePanel;
        private TableLayoutPanel middleHeaderPanel;
        private Label selectedServicesTitleLabel;
        private FlowLayoutPanel quickButtonsPanel;
        private Button btnAddService;
        private Button btnRemoveService;
        private Button btnSearchService;
        private Button btnColumnsSl;
        private DataGridView selectedServicesGrid;
        private Panel separatorMiddleRight;
        private TableLayoutPanel rightPanel;
        private Panel incompleteOrdersPanel;
        private Label incompleteOrdersTitleLabel;
        private FlowLayoutPanel incompleteOrdersFlow;
        private Panel separatorServiceInfo;
        private Panel selectedServiceInfoPanel;
        private Label selectedServiceInfoLabel;
        private Panel separatorKeypadTop;
        private TableLayoutPanel inputPanel;
        private Label paidAmountTitleLabel;
        private TextBox paidAmountTextBox;
        private Label clientSearchTitleLabel;
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
        private Button btnConfirmOrder;
        private Button btnCancelOrder;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private ComboBox clientComboBox;
        private Label clientInfoLbl;
    }
}
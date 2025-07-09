
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AvtoServis.Forms
{
    partial class PartsIncomeForm
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
            titleLabel = new Label();
            separator = new Panel();
            searchBox = new TextBox();
            buttonPanel = new FlowLayoutPanel();
            btnExport = new Button();
            btnOpenFilterDialog = new Button();
            btnImport = new Button();
            btnColumns = new Button();
            dataGridView = new DataGridView();
            countLabel = new Label();
            btnAdd = new Button();
            verticalSeparator = new Panel();
            mainTableLayoutPanel = new TableLayoutPanel();
            leftPanel = new TableLayoutPanel();
            rightPanel = new TableLayoutPanel();
            lblPartID = new Label();
            cmbPartID = new ComboBox();
            lblSupplierID = new Label();
            cmbSupplierID = new ComboBox();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            lblUnitPrice = new Label();
            txtUnitPrice = new TextBox();
            lblMarkup = new Label();
            txtMarkup = new TextBox();
            lblStatusID = new Label();
            cmbStatusID = new ComboBox();
            lblStockID = new Label();
            cmbStockID = new ComboBox();
            lblInvoiceNumber = new Label();
            txtInvoiceNumber = new TextBox();
            lblPaidAmount = new Label();
            txtPaidAmount = new TextBox();
            buttonsPanel = new TableLayoutPanel();
            btnCancel = new Button();
            btnSave = new Button();
            panelError = new Panel();
            lblError = new Label();
            buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            mainTableLayoutPanel.SuspendLayout();
            leftPanel.SuspendLayout();
            rightPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            panelError.SuspendLayout();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            leftPanel.SetColumnSpan(titleLabel, 2);
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 16);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(270, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Поступления деталей";
            toolTip.SetToolTip(titleLabel, "Заголовок раздела поступлений деталей");
            // 
            // separator
            // 
            separator.BackColor = Color.FromArgb(180, 180, 180);
            leftPanel.SetColumnSpan(separator, 2);
            separator.Dock = DockStyle.Fill;
            separator.Location = new Point(19, 86);
            separator.Margin = new Padding(3, 0, 3, 0);
            separator.Name = "separator";
            separator.Size = new Size(980, 2);
            separator.TabIndex = 2;
            toolTip.SetToolTip(separator, "Разделительная линия");
            // 
            // searchBox
            // 
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.Font = new Font("Segoe UI", 10F);
            searchBox.ForeColor = Color.Gray;
            searchBox.Location = new Point(19, 103);
            searchBox.Margin = new Padding(3, 15, 3, 3);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(238, 30);
            searchBox.TabIndex = 3;
            searchBox.Text = "Поиск...";
            toolTip.SetToolTip(searchBox, "Введите текст для поиска по видимым столбцам");
            searchBox.TextChanged += SearchBox_TextChanged;
            searchBox.Enter += SearchBox_Enter;
            searchBox.KeyDown += SearchBox_KeyDown;
            searchBox.Leave += SearchBox_Leave;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnExport);
            buttonPanel.Controls.Add(btnOpenFilterDialog);
            buttonPanel.Controls.Add(btnImport);
            buttonPanel.Controls.Add(btnColumns);
            buttonPanel.Dock = DockStyle.Right;
            buttonPanel.Location = new Point(558, 103);
            buttonPanel.Margin = new Padding(3, 15, 3, 3);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(441, 42);
            buttonPanel.TabIndex = 4;
            toolTip.SetToolTip(buttonPanel, "Панель с кнопками управления");
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
            btnExport.TabIndex = 2;
            btnExport.Text = "Экспорт";
            toolTip.SetToolTip(btnExport, "Экспортировать данные в Excel");
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
            btnOpenFilterDialog.TabIndex = 1;
            btnOpenFilterDialog.Text = "Фильтры";
            toolTip.SetToolTip(btnOpenFilterDialog, "Открыть окно фильтров");
            btnOpenFilterDialog.UseVisualStyleBackColor = false;
            btnOpenFilterDialog.Click += BtnOpenFilterDialog_Click;
            // 
            // btnImport
            // 
            btnImport.BackColor = Color.FromArgb(25, 118, 210);
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnImport.ForeColor = Color.White;
            btnImport.Location = new Point(215, 3);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(100, 34);
            btnImport.TabIndex = 0;
            btnImport.Text = "Импорт";
            toolTip.SetToolTip(btnImport, "Импортировать новые поступления или скачать пример");
            btnImport.UseVisualStyleBackColor = false;
            btnImport.Click += BtnImport_Click;
            // 
            // btnColumns
            // 
            btnColumns.BackColor = Color.FromArgb(25, 118, 210);
            btnColumns.FlatAppearance.BorderSize = 0;
            btnColumns.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnColumns.FlatStyle = FlatStyle.Flat;
            btnColumns.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnColumns.ForeColor = Color.White;
            btnColumns.Location = new Point(321, 3);
            btnColumns.Name = "btnColumns";
            btnColumns.Size = new Size(100, 34);
            btnColumns.TabIndex = 3;
            btnColumns.Text = "Столбцы";
            toolTip.SetToolTip(btnColumns, "Выбрать видимые столбцы таблицы");
            btnColumns.UseVisualStyleBackColor = false;
            btnColumns.Click += BtnColumns_Click;
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
            leftPanel.SetColumnSpan(dataGridView, 2);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(19, 151);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView.Size = new Size(980, 376);
            dataGridView.TabIndex = 5;
            toolTip.SetToolTip(dataGridView, "Таблица с данными о поступлениях деталей");
            dataGridView.CellClick += DataGridView_CellClick;
            dataGridView.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick;
            // 
            // countLabel
            // 
            countLabel.AutoSize = true;
            countLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            countLabel.ForeColor = Color.FromArgb(33, 37, 41);
            countLabel.Location = new Point(19, 545);
            countLabel.Margin = new Padding(3, 15, 3, 0);
            countLabel.Name = "countLabel";
            countLabel.Size = new Size(156, 25);
            countLabel.TabIndex = 6;
            countLabel.Text = "Поступления: 0";
            toolTip.SetToolTip(countLabel, "Количество отображаемых поступлений");
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.BackColor = Color.FromArgb(25, 118, 210);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(270, 538);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 34);
            btnAdd.TabIndex = 19;
            btnAdd.Text = "Добавить";
            toolTip.SetToolTip(btnAdd, "Добавить новое поступление");
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // verticalSeparator
            // 
            verticalSeparator.BackColor = Color.FromArgb(180, 180, 180);
            verticalSeparator.Dock = DockStyle.Fill;
            verticalSeparator.Location = new Point(1043, 16);
            verticalSeparator.Margin = new Padding(3, 0, 3, 0);
            verticalSeparator.Name = "verticalSeparator";
            verticalSeparator.Size = new Size(1, 597);
            verticalSeparator.TabIndex = 10;
            toolTip.SetToolTip(verticalSeparator, "Разделительная линия");
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.ColumnCount = 3;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 71.17117F));
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 4F));
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.82883F));
            mainTableLayoutPanel.Controls.Add(leftPanel, 0, 0);
            mainTableLayoutPanel.Controls.Add(verticalSeparator, 1, 0);
            mainTableLayoutPanel.Controls.Add(rightPanel, 2, 0);
            mainTableLayoutPanel.Controls.Add(buttonsPanel, 2, 1);
            mainTableLayoutPanel.Controls.Add(panelError, 0, 1);
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.Location = new Point(0, 0);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.Padding = new Padding(16);
            mainTableLayoutPanel.RowCount = 2;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainTableLayoutPanel.Size = new Size(1475, 689);
            mainTableLayoutPanel.TabIndex = 0;
            // 
            // leftPanel
            // 
            leftPanel.BackColor = Color.FromArgb(245, 245, 245);
            leftPanel.ColumnCount = 2;
            leftPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.94083F));
            leftPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.05917F));
            leftPanel.Controls.Add(titleLabel, 0, 0);
            leftPanel.Controls.Add(separator, 0, 2);
            leftPanel.Controls.Add(searchBox, 0, 3);
            leftPanel.Controls.Add(buttonPanel, 1, 3);
            leftPanel.Controls.Add(dataGridView, 0, 4);
            leftPanel.Controls.Add(countLabel, 0, 5);
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.Location = new Point(19, 19);
            leftPanel.Name = "leftPanel";
            leftPanel.Padding = new Padding(16);
            leftPanel.RowCount = 6;
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            leftPanel.Size = new Size(1018, 591);
            leftPanel.TabIndex = 0;
            // 
            // rightPanel
            // 
            rightPanel.BackColor = Color.FromArgb(248, 248, 248);
            rightPanel.ColumnCount = 2;
            rightPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            rightPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightPanel.Controls.Add(lblPartID, 0, 0);
            rightPanel.Controls.Add(cmbPartID, 1, 0);
            rightPanel.Controls.Add(lblSupplierID, 0, 1);
            rightPanel.Controls.Add(cmbSupplierID, 1, 1);
            rightPanel.Controls.Add(lblDate, 0, 2);
            rightPanel.Controls.Add(dtpDate, 1, 2);
            rightPanel.Controls.Add(lblQuantity, 0, 3);
            rightPanel.Controls.Add(txtQuantity, 1, 3);
            rightPanel.Controls.Add(lblUnitPrice, 0, 4);
            rightPanel.Controls.Add(txtUnitPrice, 1, 4);
            rightPanel.Controls.Add(lblMarkup, 0, 5);
            rightPanel.Controls.Add(txtMarkup, 1, 5);
            rightPanel.Controls.Add(lblStatusID, 0, 6);
            rightPanel.Controls.Add(cmbStatusID, 1, 6);
            rightPanel.Controls.Add(lblStockID, 0, 7);
            rightPanel.Controls.Add(cmbStockID, 1, 7);
            rightPanel.Controls.Add(lblInvoiceNumber, 0, 8);
            rightPanel.Controls.Add(txtInvoiceNumber, 1, 8);
            rightPanel.Controls.Add(lblPaidAmount, 0, 9);
            rightPanel.Controls.Add(txtPaidAmount, 1, 9);
            rightPanel.Controls.Add(btnAdd, 1, 10);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(1047, 19);
            rightPanel.Name = "rightPanel";
            rightPanel.Padding = new Padding(16);
            rightPanel.RowCount = 11;
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            rightPanel.Size = new Size(409, 591);
            rightPanel.TabIndex = 7;
            // 
            // lblPartID
            // 
            lblPartID.AutoSize = true;
            lblPartID.Font = new Font("Segoe UI", 10F);
            lblPartID.ForeColor = Color.FromArgb(33, 37, 41);
            lblPartID.Location = new Point(19, 16);
            lblPartID.Name = "lblPartID";
            lblPartID.Size = new Size(65, 23);
            lblPartID.TabIndex = 0;
            lblPartID.Text = "Деталь";
            // 
            // cmbPartID
            // 
            cmbPartID.Font = new Font("Segoe UI", 10F);
            cmbPartID.Location = new Point(169, 19);
            cmbPartID.Name = "cmbPartID";
            cmbPartID.Size = new Size(220, 31);
            cmbPartID.TabIndex = 1;
            // 
            // lblSupplierID
            // 
            lblSupplierID.AutoSize = true;
            lblSupplierID.Font = new Font("Segoe UI", 10F);
            lblSupplierID.ForeColor = Color.FromArgb(33, 37, 41);
            lblSupplierID.Location = new Point(19, 56);
            lblSupplierID.Name = "lblSupplierID";
            lblSupplierID.Size = new Size(97, 23);
            lblSupplierID.TabIndex = 2;
            lblSupplierID.Text = "Поставщик";
            // 
            // cmbSupplierID
            // 
            cmbSupplierID.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSupplierID.Font = new Font("Segoe UI", 10F);
            cmbSupplierID.Location = new Point(169, 59);
            cmbSupplierID.Name = "cmbSupplierID";
            cmbSupplierID.Size = new Size(220, 31);
            cmbSupplierID.TabIndex = 3;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 10F);
            lblDate.ForeColor = Color.FromArgb(33, 37, 41);
            lblDate.Location = new Point(19, 96);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(47, 23);
            lblDate.TabIndex = 4;
            lblDate.Text = "Дата";
            // 
            // dtpDate
            // 
            dtpDate.Font = new Font("Segoe UI", 10F);
            dtpDate.Location = new Point(169, 99);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(220, 30);
            dtpDate.TabIndex = 5;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 10F);
            lblQuantity.ForeColor = Color.FromArgb(33, 37, 41);
            lblQuantity.Location = new Point(19, 136);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(102, 23);
            lblQuantity.TabIndex = 6;
            lblQuantity.Text = "Количество";
            // 
            // txtQuantity
            // 
            txtQuantity.BorderStyle = BorderStyle.FixedSingle;
            txtQuantity.Font = new Font("Segoe UI", 10F);
            txtQuantity.Location = new Point(169, 139);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(220, 30);
            txtQuantity.TabIndex = 7;
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Font = new Font("Segoe UI", 10F);
            lblUnitPrice.ForeColor = Color.FromArgb(33, 37, 41);
            lblUnitPrice.Location = new Point(19, 176);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(100, 23);
            lblUnitPrice.TabIndex = 8;
            lblUnitPrice.Text = "Цена за ед.";
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
            txtUnitPrice.Font = new Font("Segoe UI", 10F);
            txtUnitPrice.Location = new Point(169, 179);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new Size(220, 30);
            txtUnitPrice.TabIndex = 9;
            // 
            // lblMarkup
            // 
            lblMarkup.AutoSize = true;
            lblMarkup.Font = new Font("Segoe UI", 10F);
            lblMarkup.ForeColor = Color.FromArgb(33, 37, 41);
            lblMarkup.Location = new Point(19, 216);
            lblMarkup.Name = "lblMarkup";
            lblMarkup.Size = new Size(77, 23);
            lblMarkup.TabIndex = 10;
            lblMarkup.Text = "Наценка";
            // 
            // txtMarkup
            // 
            txtMarkup.BorderStyle = BorderStyle.FixedSingle;
            txtMarkup.Font = new Font("Segoe UI", 10F);
            txtMarkup.Location = new Point(169, 219);
            txtMarkup.Name = "txtMarkup";
            txtMarkup.Size = new Size(220, 30);
            txtMarkup.TabIndex = 11;
            // 
            // lblStatusID
            // 
            lblStatusID.AutoSize = true;
            lblStatusID.Font = new Font("Segoe UI", 10F);
            lblStatusID.ForeColor = Color.FromArgb(33, 37, 41);
            lblStatusID.Location = new Point(19, 256);
            lblStatusID.Name = "lblStatusID";
            lblStatusID.Size = new Size(60, 23);
            lblStatusID.TabIndex = 12;
            lblStatusID.Text = "Статус";
            // 
            // cmbStatusID
            // 
            cmbStatusID.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusID.Font = new Font("Segoe UI", 10F);
            cmbStatusID.Location = new Point(169, 259);
            cmbStatusID.Name = "cmbStatusID";
            cmbStatusID.Size = new Size(220, 31);
            cmbStatusID.TabIndex = 13;
            // 
            // lblStockID
            // 
            lblStockID.AutoSize = true;
            lblStockID.Font = new Font("Segoe UI", 10F);
            lblStockID.ForeColor = Color.FromArgb(33, 37, 41);
            lblStockID.Location = new Point(19, 296);
            lblStockID.Name = "lblStockID";
            lblStockID.Size = new Size(56, 23);
            lblStockID.TabIndex = 14;
            lblStockID.Text = "Склад";
            // 
            // cmbStockID
            // 
            cmbStockID.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStockID.Font = new Font("Segoe UI", 10F);
            cmbStockID.Location = new Point(169, 299);
            cmbStockID.Name = "cmbStockID";
            cmbStockID.Size = new Size(220, 31);
            cmbStockID.TabIndex = 15;
            // 
            // lblInvoiceNumber
            // 
            lblInvoiceNumber.AutoSize = true;
            lblInvoiceNumber.Font = new Font("Segoe UI", 10F);
            lblInvoiceNumber.ForeColor = Color.FromArgb(33, 37, 41);
            lblInvoiceNumber.Location = new Point(19, 336);
            lblInvoiceNumber.Name = "lblInvoiceNumber";
            lblInvoiceNumber.Size = new Size(111, 23);
            lblInvoiceNumber.TabIndex = 16;
            lblInvoiceNumber.Text = "Номер счета";
            // 
            // txtInvoiceNumber
            // 
            txtInvoiceNumber.BorderStyle = BorderStyle.FixedSingle;
            txtInvoiceNumber.Font = new Font("Segoe UI", 10F);
            txtInvoiceNumber.Location = new Point(169, 339);
            txtInvoiceNumber.Name = "txtInvoiceNumber";
            txtInvoiceNumber.Size = new Size(220, 30);
            txtInvoiceNumber.TabIndex = 17;
            // 
            // lblPaidAmount
            // 
            lblPaidAmount.AutoSize = true;
            lblPaidAmount.Font = new Font("Segoe UI", 10F);
            lblPaidAmount.ForeColor = Color.FromArgb(33, 37, 41);
            lblPaidAmount.Location = new Point(19, 376);
            lblPaidAmount.Name = "lblPaidAmount";
            lblPaidAmount.Size = new Size(113, 40);
            lblPaidAmount.TabIndex = 18;
            lblPaidAmount.Text = "Оплаченная сумма";
            // 
            // txtPaidAmount
            // 
            txtPaidAmount.BorderStyle = BorderStyle.FixedSingle;
            txtPaidAmount.Font = new Font("Segoe UI", 10F);
            txtPaidAmount.Location = new Point(169, 379);
            txtPaidAmount.Name = "txtPaidAmount";
            txtPaidAmount.Size = new Size(220, 30);
            txtPaidAmount.TabIndex = 19;
            // 
            // buttonsPanel
            // 
            buttonsPanel.ColumnCount = 2;
            buttonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonsPanel.Controls.Add(btnCancel, 0, 0);
            buttonsPanel.Controls.Add(btnSave, 1, 0);
            buttonsPanel.Dock = DockStyle.Fill;
            buttonsPanel.Location = new Point(1047, 616);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.RowCount = 1;
            buttonsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonsPanel.Size = new Size(409, 54);
            buttonsPanel.TabIndex = 9;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(3, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(198, 48);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Отменить";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(207, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(198, 48);
            btnSave.TabIndex = 0;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // panelError
            // 
            panelError.BackColor = Color.FromArgb(255, 245, 245);
            panelError.BorderStyle = BorderStyle.FixedSingle;
            mainTableLayoutPanel.SetColumnSpan(panelError, 2);
            panelError.Controls.Add(lblError);
            panelError.Location = new Point(19, 616);
            panelError.Name = "panelError";
            panelError.Size = new Size(1021, 54);
            panelError.TabIndex = 8;
            panelError.Visible = false;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location = new Point(3, 7);
            lblError.MaximumSize = new Size(1150, 0);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 23);
            lblError.TabIndex = 0;
            lblError.TextAlign = ContentAlignment.MiddleLeft;
            lblError.Visible = false;
            // 
            // PartsIncomeForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(1475, 689);
            Controls.Add(mainTableLayoutPanel);
            MinimumSize = new Size(1475, 700);
            Name = "PartsIncomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Поступления деталей";
            buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            mainTableLayoutPanel.ResumeLayout(false);
            leftPanel.ResumeLayout(false);
            leftPanel.PerformLayout();
            rightPanel.ResumeLayout(false);
            rightPanel.PerformLayout();
            buttonsPanel.ResumeLayout(false);
            panelError.ResumeLayout(false);
            panelError.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel mainTableLayoutPanel;
        private TableLayoutPanel leftPanel;
        private Label titleLabel;
        private TextBox searchBox;
        private Label countLabel;
        private Button btnOpenFilterDialog;
        private Button btnExport;
        private Button btnImport;
        private Button btnColumns;
        private DataGridView dataGridView;
        private TableLayoutPanel rightPanel;
        private Panel verticalSeparator;
        private Label lblPartID;
        private ComboBox cmbPartID;
        private Label lblSupplierID;
        private ComboBox cmbSupplierID;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private Label lblQuantity;
        private TextBox txtQuantity;
        private Label lblUnitPrice;
        private TextBox txtUnitPrice;
        private Label lblMarkup;
        private TextBox txtMarkup;
        private Label lblStatusID;
        private ComboBox cmbStatusID;
        private Label lblStockID;
        private ComboBox cmbStockID;
        private Label lblInvoiceNumber;
        private TextBox txtInvoiceNumber;
        private Label lblPaidAmount;
        private TextBox txtPaidAmount;
        private Button btnAdd;
        private Panel panelError;
        private Label lblError;
        private TableLayoutPanel buttonsPanel;
        private Button btnSave;
        private Button btnCancel;
        private ToolTip toolTip;
        private FlowLayoutPanel buttonPanel;
        private Panel separator;
    }
}

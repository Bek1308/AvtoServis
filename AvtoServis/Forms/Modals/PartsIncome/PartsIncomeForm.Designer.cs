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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            mainTableLayoutPanel = new TableLayoutPanel();
            leftPanel = new TableLayoutPanel();
            titleLabel = new Label();
            batchNumberLabel = new Label();
            searchBox = new TextBox();
            btnOpenFilterDialog = new Button();
            btnExport = new Button();
            dataGridView = new DataGridView();
            btnImport = new Button();
            countLabel = new Label();
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
            btnAdd = new Button();
            panelError = new Panel();
            lblError = new Label();
            buttonsPanel = new TableLayoutPanel();
            btnCancel = new Button();
            btnSave = new Button();
            mainTableLayoutPanel.SuspendLayout();
            leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            rightPanel.SuspendLayout();
            panelError.SuspendLayout();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.ColumnCount = 2;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 71.17117F));
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.8288288F));
            mainTableLayoutPanel.Controls.Add(leftPanel, 0, 0);
            mainTableLayoutPanel.Controls.Add(rightPanel, 1, 0);
            mainTableLayoutPanel.Controls.Add(panelError, 0, 1);
            mainTableLayoutPanel.Controls.Add(buttonsPanel, 1, 2);
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.Location = new Point(0, 0);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.Padding = new Padding(16);
            mainTableLayoutPanel.RowCount = 3;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainTableLayoutPanel.Size = new Size(1475, 700);
            mainTableLayoutPanel.TabIndex = 0;
            // 
            // leftPanel
            // 
            leftPanel.BackColor = Color.FromArgb(248, 248, 248);
            leftPanel.ColumnCount = 2;
            leftPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.26393F));
            leftPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.73607F));
            leftPanel.Controls.Add(titleLabel, 0, 0);
            leftPanel.Controls.Add(batchNumberLabel, 1, 0);
            leftPanel.Controls.Add(searchBox, 0, 1);
            leftPanel.Controls.Add(btnOpenFilterDialog, 0, 2);
            leftPanel.Controls.Add(btnExport, 0, 3);
            leftPanel.Controls.Add(dataGridView, 0, 4);
            leftPanel.Controls.Add(btnImport, 1, 1);
            leftPanel.Controls.Add(countLabel, 1, 2);
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.Location = new Point(19, 19);
            leftPanel.Name = "leftPanel";
            leftPanel.Padding = new Padding(16);
            leftPanel.RowCount = 5;
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            leftPanel.Size = new Size(1021, 542);
            leftPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AccessibleDescription = "Заголовок списка поступлений деталей";
            titleLabel.AccessibleName = "Список поступлений";
            titleLabel.Anchor = AnchorStyles.Left;
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(19, 22);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(306, 37);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Поступления деталей";
            // 
            // batchNumberLabel
            // 
            batchNumberLabel.AccessibleDescription = "Номер текущей партии поступлений";
            batchNumberLabel.AccessibleName = "Номер партии";
            batchNumberLabel.Anchor = AnchorStyles.Left;
            batchNumberLabel.AutoSize = true;
            batchNumberLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            batchNumberLabel.ForeColor = Color.FromArgb(33, 37, 41);
            batchNumberLabel.Location = new Point(615, 29);
            batchNumberLabel.Name = "batchNumberLabel";
            batchNumberLabel.Size = new Size(141, 23);
            batchNumberLabel.TabIndex = 1;
            batchNumberLabel.Text = "Номер партии: ";
            // 
            // searchBox
            // 
            searchBox.AccessibleDescription = "Введите номер счета или ID для поиска";
            searchBox.AccessibleName = "Поиск поступлений";
            searchBox.Anchor = AnchorStyles.Left;
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.Font = new Font("Segoe UI", 10F);
            searchBox.ForeColor = Color.FromArgb(108, 117, 125);
            searchBox.Location = new Point(19, 71);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(282, 30);
            searchBox.TabIndex = 2;
            searchBox.Text = "Поиск...";
            searchBox.GotFocus += SearchBox_GotFocus;
            searchBox.LostFocus += SearchBox_LostFocus;
            // 
            // btnOpenFilterDialog
            // 
            btnOpenFilterDialog.AccessibleDescription = "Открывает окно для фильтрации поступлений";
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
            // 
            // btnExport
            // 
            btnExport.AccessibleDescription = "Экспортирует список поступлений в Excel файл";
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
            btnExport.TabIndex = 5;
            btnExport.Text = "Экспорт";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // dataGridView
            // 
            dataGridView.AccessibleDescription = "Отображает список поступлений деталей";
            dataGridView.AccessibleName = "Таблица поступлений";
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
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.ColumnHeadersHeight = 29;
            leftPanel.SetColumnSpan(dataGridView, 2);
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
            dataGridView.Size = new Size(983, 334);
            dataGridView.TabIndex = 7;
            // 
            // btnImport
            // 
            btnImport.AccessibleDescription = "Импортирует новые поступления";
            btnImport.AccessibleName = "Импорт поступлений";
            btnImport.Anchor = AnchorStyles.Right;
            btnImport.BackColor = Color.FromArgb(25, 118, 210);
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnImport.ForeColor = Color.White;
            btnImport.Location = new Point(882, 69);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(120, 34);
            btnImport.TabIndex = 6;
            btnImport.Text = "Импорт";
            btnImport.UseVisualStyleBackColor = false;
            // 
            // countLabel
            // 
            countLabel.AccessibleDescription = "Показывает общее количество поступлений";
            countLabel.AccessibleName = "Количество поступлений";
            countLabel.Anchor = AnchorStyles.Left;
            countLabel.AutoSize = true;
            countLabel.Font = new Font("Segoe UI", 10F);
            countLabel.ForeColor = Color.FromArgb(33, 37, 41);
            countLabel.Location = new Point(615, 114);
            countLabel.Name = "countLabel";
            countLabel.Size = new Size(130, 23);
            countLabel.TabIndex = 3;
            countLabel.Text = "Поступления: 0";
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
            rightPanel.Controls.Add(btnAdd, 1, 9);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(1046, 19);
            rightPanel.Name = "rightPanel";
            rightPanel.Padding = new Padding(16);
            rightPanel.RowCount = 10;
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            rightPanel.Size = new Size(410, 542);
            rightPanel.TabIndex = 8;
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
            cmbPartID.Size = new Size(222, 31);
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
            cmbSupplierID.Size = new Size(222, 31);
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
            dtpDate.Size = new Size(222, 30);
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
            txtQuantity.Size = new Size(222, 30);
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
            txtUnitPrice.Size = new Size(222, 30);
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
            txtMarkup.Size = new Size(222, 30);
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
            cmbStatusID.Size = new Size(222, 31);
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
            cmbStockID.Size = new Size(222, 31);
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
            txtInvoiceNumber.Size = new Size(222, 30);
            txtInvoiceNumber.TabIndex = 17;
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
            btnAdd.Location = new Point(271, 489);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 34);
            btnAdd.TabIndex = 18;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // panelError
            // 
            panelError.BackColor = Color.FromArgb(255, 245, 245);
            panelError.BorderStyle = BorderStyle.FixedSingle;
            mainTableLayoutPanel.SetColumnSpan(panelError, 2);
            panelError.Controls.Add(lblError);
            panelError.Location = new Point(19, 567);
            panelError.Name = "panelError";
            panelError.Size = new Size(1437, 54);
            panelError.TabIndex = 9;
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
            // buttonsPanel
            // 
            buttonsPanel.ColumnCount = 2;
            buttonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonsPanel.Controls.Add(btnCancel, 0, 0);
            buttonsPanel.Controls.Add(btnSave, 1, 0);
            buttonsPanel.Dock = DockStyle.Right;
            buttonsPanel.Location = new Point(1065, 627);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.RowCount = 1;
            buttonsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonsPanel.Size = new Size(391, 54);
            buttonsPanel.TabIndex = 10;
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
            btnCancel.Size = new Size(109, 36);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Отменить";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(264, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(124, 36);
            btnSave.TabIndex = 0;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // PartsIncomeForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            CancelButton = btnCancel;
            ClientSize = new Size(1475, 700);
            Controls.Add(mainTableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PartsIncomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Поступления деталей";
            mainTableLayoutPanel.ResumeLayout(false);
            leftPanel.ResumeLayout(false);
            leftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            rightPanel.ResumeLayout(false);
            rightPanel.PerformLayout();
            panelError.ResumeLayout(false);
            panelError.PerformLayout();
            buttonsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel mainTableLayoutPanel;
        private TableLayoutPanel leftPanel;
        private Label titleLabel;
        private Label batchNumberLabel;
        private TextBox searchBox;
        private Label countLabel;
        private Button btnOpenFilterDialog;
        private Button btnExport;
        private Button btnImport;
        private DataGridView dataGridView;
        private TableLayoutPanel rightPanel;
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
        private Button btnAdd;
        private Panel panelError;
        private Label lblError;
        private TableLayoutPanel buttonsPanel;
        private Button btnSave;
        private Button btnCancel;
    }
}
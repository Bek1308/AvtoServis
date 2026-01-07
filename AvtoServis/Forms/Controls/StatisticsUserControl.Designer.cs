using System.Drawing;
using System.Windows.Forms;
using Charting = System.Windows.Forms.DataVisualization.Charting;

namespace AvtoServis.Forms.Controls
{
    partial class StatisticsUserControl
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
            toolTip = new ToolTip(components);
            tableLayoutPanel = new TableLayoutPanel();
            headerPanel = new Panel();
            titleLabel = new Label();
            separator = new Panel();
            statsPanel = new FlowLayoutPanel();
            cardTotalDebtors = new Panel();
            lblTotalDebtorsTitle = new Label();
            lblTotalDebtorsValue = new Label();
            cardCustomerDebt = new Panel();
            lblCustomerDebtTitle = new Label();
            lblCustomerDebtValue = new Label();
            cardSuppliersOwed = new Panel();
            lblSuppliersOwedTitle = new Label();
            lblSuppliersOwedValue = new Label();
            cardSupplierDebt = new Panel();
            lblSupplierDebtTitle = new Label();
            lblSupplierDebtValue = new Label();
            chartsPanel = new TableLayoutPanel();
            topPartsChart = new Charting.Chart();
            topServicesChart = new Charting.Chart();
            weeklySalesChart = new Charting.Chart();
            tableLayoutPanel.SuspendLayout();
            headerPanel.SuspendLayout();
            statsPanel.SuspendLayout();
            cardTotalDebtors.SuspendLayout();
            cardCustomerDebt.SuspendLayout();
            cardSuppliersOwed.SuspendLayout();
            cardSupplierDebt.SuspendLayout();
            chartsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)topPartsChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)topServicesChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)weeklySalesChart).BeginInit();
            SuspendLayout();

            // toolTip
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            // tableLayoutPanel
            tableLayoutPanel.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(headerPanel, 0, 0);
            tableLayoutPanel.Controls.Add(separator, 0, 1);
            tableLayoutPanel.Controls.Add(statsPanel, 0, 2);
            tableLayoutPanel.Controls.Add(chartsPanel, 0, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(16);
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(960, 600);
            tableLayoutPanel.TabIndex = 0;

            // headerPanel
            headerPanel.BackColor = Color.FromArgb(59, 130, 246);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(19, 19);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(922, 54);
            headerPanel.TabIndex = 0;

            // titleLabel
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(20, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(207, 23);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Основная страница статистики";

            // separator
            separator.BackColor = Color.FromArgb(229, 231, 235);
            separator.Dock = DockStyle.Fill;
            separator.Location = new Point(19, 79);
            separator.Name = "separator";
            separator.Size = new Size(922, 1);
            separator.TabIndex = 1;

            // statsPanel
            statsPanel.Controls.Add(cardTotalDebtors);
            statsPanel.Controls.Add(cardCustomerDebt);
            statsPanel.Controls.Add(cardSuppliersOwed);
            statsPanel.Controls.Add(cardSupplierDebt);
            statsPanel.Dock = DockStyle.Fill;
            statsPanel.FlowDirection = FlowDirection.LeftToRight;
            statsPanel.WrapContents = false;
            statsPanel.AutoSize = false;
            statsPanel.Location = new Point(19, 81);
            statsPanel.Name = "statsPanel";
            statsPanel.Size = new Size(922, 94);
            statsPanel.TabIndex = 2;

            // cardTotalDebtors
            cardTotalDebtors.BackColor = Color.White;
            cardTotalDebtors.Controls.Add(lblTotalDebtorsTitle);
            cardTotalDebtors.Controls.Add(lblTotalDebtorsValue);
            cardTotalDebtors.Location = new Point(3, 3);
            cardTotalDebtors.Name = "cardTotalDebtors";
            cardTotalDebtors.Size = new Size(220, 80);
            cardTotalDebtors.TabIndex = 0;

            // lblTotalDebtorsTitle
            lblTotalDebtorsTitle.AutoSize = true;
            lblTotalDebtorsTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblTotalDebtorsTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblTotalDebtorsTitle.Location = new Point(10, 10);
            lblTotalDebtorsTitle.Name = "lblTotalDebtorsTitle";
            lblTotalDebtorsTitle.Size = new Size(100, 15);
            lblTotalDebtorsTitle.TabIndex = 0;
            lblTotalDebtorsTitle.Text = "Общее количество должников";

            // lblTotalDebtorsValue
            lblTotalDebtorsValue.AutoSize = true;
            lblTotalDebtorsValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalDebtorsValue.ForeColor = Color.FromArgb(17, 24, 39);
            lblTotalDebtorsValue.Location = new Point(10, 35);
            lblTotalDebtorsValue.Name = "lblTotalDebtorsValue";
            lblTotalDebtorsValue.Size = new Size(50, 21);
            lblTotalDebtorsValue.TabIndex = 1;
            lblTotalDebtorsValue.Text = "0";

            // cardCustomerDebt
            cardCustomerDebt.BackColor = Color.White;
            cardCustomerDebt.Controls.Add(lblCustomerDebtTitle);
            cardCustomerDebt.Controls.Add(lblCustomerDebtValue);
            cardCustomerDebt.Location = new Point(229, 3);
            cardCustomerDebt.Name = "cardCustomerDebt";
            cardCustomerDebt.Size = new Size(220, 80);
            cardCustomerDebt.TabIndex = 1;

            // lblCustomerDebtTitle
            lblCustomerDebtTitle.AutoSize = true;
            lblCustomerDebtTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblCustomerDebtTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblCustomerDebtTitle.Location = new Point(10, 10);
            lblCustomerDebtTitle.Name = "lblCustomerDebtTitle";
            lblCustomerDebtTitle.Size = new Size(120, 15);
            lblCustomerDebtTitle.TabIndex = 0;
            lblCustomerDebtTitle.Text = "Общая задолженность клиентов";

            // lblCustomerDebtValue
            lblCustomerDebtValue.AutoSize = true;
            lblCustomerDebtValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCustomerDebtValue.ForeColor = Color.FromArgb(17, 24, 39);
            lblCustomerDebtValue.Location = new Point(10, 35);
            lblCustomerDebtValue.Name = "lblCustomerDebtValue";
            lblCustomerDebtValue.Size = new Size(50, 21);
            lblCustomerDebtValue.TabIndex = 1;
            lblCustomerDebtValue.Text = "$0.00";

            // cardSuppliersOwed
            cardSuppliersOwed.BackColor = Color.White;
            cardSuppliersOwed.Controls.Add(lblSuppliersOwedTitle);
            cardSuppliersOwed.Controls.Add(lblSuppliersOwedValue);
            cardSuppliersOwed.Location = new Point(455, 3);
            cardSuppliersOwed.Name = "cardSuppliersOwed";
            cardSuppliersOwed.Size = new Size(220, 80);
            cardSuppliersOwed.TabIndex = 2;

            // lblSuppliersOwedTitle
            lblSuppliersOwedTitle.AutoSize = true;
            lblSuppliersOwedTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblSuppliersOwedTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblSuppliersOwedTitle.Location = new Point(10, 10);
            lblSuppliersOwedTitle.Name = "lblSuppliersOwedTitle";
            lblSuppliersOwedTitle.Size = new Size(130, 15);
            lblSuppliersOwedTitle.TabIndex = 0;
            lblSuppliersOwedTitle.Text = "Количество должных поставщиков";

            // lblSuppliersOwedValue
            lblSuppliersOwedValue.AutoSize = true;
            lblSuppliersOwedValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSuppliersOwedValue.ForeColor = Color.FromArgb(17, 24, 39);
            lblSuppliersOwedValue.Location = new Point(10, 35);
            lblSuppliersOwedValue.Name = "lblSuppliersOwedValue";
            lblSuppliersOwedValue.Size = new Size(50, 21);
            lblSuppliersOwedValue.TabIndex = 1;
            lblSuppliersOwedValue.Text = "0";

            // cardSupplierDebt
            cardSupplierDebt.BackColor = Color.White;
            cardSupplierDebt.Controls.Add(lblSupplierDebtTitle);
            cardSupplierDebt.Controls.Add(lblSupplierDebtValue);
            cardSupplierDebt.Location = new Point(681, 3);
            cardSupplierDebt.Name = "cardSupplierDebt";
            cardSupplierDebt.Size = new Size(220, 80);
            cardSupplierDebt.TabIndex = 3;

            // lblSupplierDebtTitle
            lblSupplierDebtTitle.AutoSize = true;
            lblSupplierDebtTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblSupplierDebtTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblSupplierDebtTitle.Location = new Point(10, 10);
            lblSupplierDebtTitle.Name = "lblSupplierDebtTitle";
            lblSupplierDebtTitle.Size = new Size(140, 15);
            lblSupplierDebtTitle.TabIndex = 0;
            lblSupplierDebtTitle.Text = "Общая задолженность перед поставщиками";

            // lblSupplierDebtValue
            lblSupplierDebtValue.AutoSize = true;
            lblSupplierDebtValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSupplierDebtValue.ForeColor = Color.FromArgb(17, 24, 39);
            lblSupplierDebtValue.Location = new Point(10, 35);
            lblSupplierDebtValue.Name = "lblSupplierDebtValue";
            lblSupplierDebtValue.Size = new Size(50, 21);
            lblSupplierDebtValue.TabIndex = 1;
            lblSupplierDebtValue.Text = "$0.00";

            // chartsPanel
            chartsPanel.ColumnCount = 2;
            chartsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            chartsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            chartsPanel.Controls.Add(topPartsChart, 0, 0);
            chartsPanel.Controls.Add(topServicesChart, 1, 0);
            chartsPanel.Controls.Add(weeklySalesChart, 0, 1);
            chartsPanel.RowCount = 2;
            chartsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            chartsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            chartsPanel.Dock = DockStyle.Fill;
            chartsPanel.Location = new Point(19, 181);
            chartsPanel.Name = "chartsPanel";
            chartsPanel.Size = new Size(922, 400);
            chartsPanel.TabIndex = 3;

            // topPartsChart
            topPartsChart.BackColor = Color.White;
            topPartsChart.ChartAreas.Add(new Charting.ChartArea { Name = "ChartArea1" });
            var topPartsLegend = new Charting.Legend { Name = "Legend1", Docking = Charting.Docking.Right, MaximumAutoSize = 20 };
            topPartsChart.Legends.Add(topPartsLegend);
            topPartsChart.Location = new Point(3, 3);
            topPartsChart.Name = "topPartsChart";
            topPartsChart.Size = new Size(450, 190);
            topPartsChart.TabIndex = 0;
            topPartsChart.Text = "Топ-10 продаваемых запчастей";

            // topServicesChart
            topServicesChart.BackColor = Color.White;
            topServicesChart.ChartAreas.Add(new Charting.ChartArea { Name = "ChartArea1" });
            var topServicesLegend = new Charting.Legend { Name = "Legend1", Docking = Charting.Docking.Right, MaximumAutoSize = 20 };
            topServicesChart.Legends.Add(topServicesLegend);
            topServicesChart.Location = new Point(459, 3);
            topServicesChart.Name = "topServicesChart";
            topServicesChart.Size = new Size(450, 190);
            topServicesChart.TabIndex = 1;
            topServicesChart.Text = "Топ-10 услуг";

            // weeklySalesChart
            weeklySalesChart.BackColor = Color.White;
            weeklySalesChart.ChartAreas.Add(new Charting.ChartArea { Name = "ChartArea1" });
            var weeklySalesLegend = new Charting.Legend { Name = "Legend1", Docking = Charting.Docking.Right, MaximumAutoSize = 20 };
            weeklySalesChart.Legends.Add(weeklySalesLegend);
            chartsPanel.SetColumnSpan(weeklySalesChart, 2);
            weeklySalesChart.Location = new Point(3, 203);
            weeklySalesChart.Name = "weeklySalesChart";
            weeklySalesChart.Size = new Size(906, 190);
            weeklySalesChart.TabIndex = 2;
            weeklySalesChart.Text = "Продажи за последние 10 дней";

            // StatisticsUserControl
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(tableLayoutPanel);
            MinimumSize = new Size(600, 400);
            Name = "StatisticsUserControl";
            Size = new Size(960, 600);
            Resize += StatisticsUserControl_Resize;
            Load += StatisticsUserControl_Load;
            tableLayoutPanel.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            statsPanel.ResumeLayout(false);
            cardTotalDebtors.ResumeLayout(false);
            cardTotalDebtors.PerformLayout();
            cardCustomerDebt.ResumeLayout(false);
            cardCustomerDebt.PerformLayout();
            cardSuppliersOwed.ResumeLayout(false);
            cardSuppliersOwed.PerformLayout();
            cardSupplierDebt.ResumeLayout(false);
            cardSupplierDebt.PerformLayout();
            chartsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)topPartsChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)topServicesChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)weeklySalesChart).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private Panel headerPanel;
        private Label titleLabel;
        private Panel separator;
        private FlowLayoutPanel statsPanel;
        private Panel cardTotalDebtors;
        private Label lblTotalDebtorsTitle;
        private Label lblTotalDebtorsValue;
        private Panel cardCustomerDebt;
        private Label lblCustomerDebtTitle;
        private Label lblCustomerDebtValue;
        private Panel cardSuppliersOwed;
        private Label lblSuppliersOwedTitle;
        private Label lblSuppliersOwedValue;
        private Panel cardSupplierDebt;
        private Label lblSupplierDebtTitle;
        private Label lblSupplierDebtValue;
        private TableLayoutPanel chartsPanel;
        private Charting.Chart topPartsChart;
        private Charting.Chart topServicesChart;
        private Charting.Chart weeklySalesChart;
        private ToolTip toolTip;
    }
}
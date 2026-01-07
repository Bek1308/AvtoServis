using System;
using System.Drawing;
using System.Windows.Forms;
using Charting = System.Windows.Forms.DataVisualization.Charting;
using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories;
using AvtoServis.ViewModels.Screens;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace AvtoServis.Forms.Controls
{
    public partial class StatisticsUserControl : UserControl
    {
        private readonly StatisticsViewModel _viewModel;
        private readonly System.Windows.Forms.Timer _animationTimer;
        private int _animationStepCount;
        private readonly int _animationSteps = 10;
        private readonly Color[] _barColors = new[]
        {
            Color.FromArgb(75, 192, 192), Color.FromArgb(153, 102, 255), Color.FromArgb(255, 99, 132),
            Color.FromArgb(54, 162, 235), Color.FromArgb(255, 206, 86), Color.FromArgb(231, 76, 60),
            Color.FromArgb(149, 165, 166), Color.FromArgb(46, 204, 113), Color.FromArgb(241, 196, 15),
            Color.FromArgb(211, 84, 0)
        };

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public StatisticsUserControl(StatisticsViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _animationTimer = new System.Windows.Forms.Timer { Interval = 20 };
            _animationStepCount = 0;
            InitializeComponent();
            EnhanceVisualStyles();
            SetToolTips();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            AdjustLayoutForSize(Size);
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Панель статистики");
            toolTip.SetToolTip(headerPanel, "Заголовок статистики");
            toolTip.SetToolTip(titleLabel, "Основной заголовок статистики");
            toolTip.SetToolTip(statsPanel, "Карточки общей статистики");
            toolTip.SetToolTip(cardTotalDebtors, "Общее количество должников");
            toolTip.SetToolTip(cardCustomerDebt, "Общая задолженность клиентов");
            toolTip.SetToolTip(cardSuppliersOwed, "Количество должных поставщиков");
            toolTip.SetToolTip(cardSupplierDebt, "Общая задолженность перед поставщиками");
            toolTip.SetToolTip(topPartsChart, "Диаграмма топ-10 продаваемых запчастей");
            toolTip.SetToolTip(topServicesChart, "Диаграмма топ-10 услуг");
            toolTip.SetToolTip(weeklySalesChart, "Диаграмма продаж за последние 10 дней");
        }

        private void EnhanceVisualStyles()
        {
            headerPanel.Paint += (s, e) =>
            {
                using (var brush = new LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(59, 130, 246),
                    Color.FromArgb(99, 179, 255),
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }
            };

            foreach (var card in new[] { cardTotalDebtors, cardCustomerDebt, cardSuppliersOwed, cardSupplierDebt })
            {
                card.BackColor = Color.White;
                card.Paint += (s, e) =>
                {
                    using (var pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                    }
                };
            }

            foreach (var chart in new[] { topPartsChart, topServicesChart, weeklySalesChart })
            {
                chart.BackColor = Color.White;
                chart.ChartAreas[0].BackColor = Color.White;
                chart.Legends[0].BackColor = Color.White;
                chart.Legends[0].Font = new Font("Segoe UI", 7F);
                chart.Legends[0].Docking = Charting.Docking.Right;
                chart.Legends[0].MaximumAutoSize = 20;
            }
        }

        private void AdjustLayoutForSize(Size newSize)
        {
            int baseWidth = 960;
            int baseHeight = 600;
            float scaleFactor = Math.Min((float)newSize.Width / baseWidth, (float)newSize.Height / baseHeight);
            int fontSize = Math.Max(6, (int)(8 * scaleFactor));
            int titleFontSize = Math.Max(8, (int)(10 * scaleFactor));
            int margin = Math.Max(8, (int)(16 * scaleFactor));

            tableLayoutPanel.Padding = new Padding(margin);
            tableLayoutPanel.RowStyles[0].Height = 60 * scaleFactor;
            tableLayoutPanel.RowStyles[1].Height = 2 * scaleFactor;
            tableLayoutPanel.RowStyles[2].Height = 100 * scaleFactor;
            tableLayoutPanel.RowStyles[3].Height = newSize.Height - (60 + 2 + 100) * scaleFactor - 2 * margin;

            titleLabel.Font = new Font("Segoe UI", titleFontSize, FontStyle.Bold);
            foreach (var label in new[] { lblTotalDebtorsTitle, lblCustomerDebtTitle, lblSuppliersOwedTitle, lblSupplierDebtTitle })
            {
                label.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
                label.Location = new Point((int)(10 * scaleFactor), (int)(10 * scaleFactor));
                label.AutoSize = true;
            }
            foreach (var label in new[] { lblTotalDebtorsValue, lblCustomerDebtValue, lblSuppliersOwedValue, lblSupplierDebtValue })
            {
                label.Font = new Font("Segoe UI", fontSize + 4, FontStyle.Bold);
                label.Location = new Point((int)(10 * scaleFactor), (int)(35 * scaleFactor));
                label.AutoSize = true;
            }

            int cardWidth = (int)((newSize.Width - margin * 2 - 12) / 4);
            int cardHeight = (int)(80 * scaleFactor);
            foreach (var card in new[] { cardTotalDebtors, cardCustomerDebt, cardSuppliersOwed, cardSupplierDebt })
            {
                card.Size = new Size(cardWidth, cardHeight);
                card.MinimumSize = new Size(150, 60);
            }
            statsPanel.AutoSize = false;
            statsPanel.Size = new Size(newSize.Width - margin * 2, (int)(100 * scaleFactor));

            int chartWidth = (int)((newSize.Width - margin * 2 - 6) / 2);
            int chartHeight = (int)(tableLayoutPanel.RowStyles[3].Height * 0.45f);
            topPartsChart.Size = new Size(chartWidth, chartHeight);
            topServicesChart.Size = new Size(chartWidth, chartHeight);
            weeklySalesChart.Size = new Size(newSize.Width - margin * 2 - 6, (int)(chartHeight * 1.2f));

            if (scaleFactor > 0.8f)
            {
                int radius = (int)(8 * scaleFactor);
                tableLayoutPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, newSize.Width, newSize.Height, radius, radius));
                headerPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, headerPanel.Width, headerPanel.Height, (int)(6 * scaleFactor), (int)(6 * scaleFactor)));
                foreach (var card in new[] { cardTotalDebtors, cardCustomerDebt, cardSuppliersOwed, cardSupplierDebt })
                {
                    card.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, card.Width, card.Height, radius, radius));
                }
                chartsPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, chartsPanel.Width, chartsPanel.Height, radius, radius));
            }
            else
            {
                tableLayoutPanel.Region = null;
                headerPanel.Region = null;
                foreach (var card in new[] { cardTotalDebtors, cardCustomerDebt, cardSuppliersOwed, cardSupplierDebt })
                {
                    card.Region = null;
                }
                chartsPanel.Region = null;
            }
        }

        private void StatisticsUserControl_Resize(object sender, EventArgs e)
        {
            AdjustLayoutForSize(Size);
        }

        private void StatisticsUserControl_Load(object sender, EventArgs e)
        {
            try
            {
                LoadStatistics();
                StartFadeAnimation(true);
            }
            catch (Exception ex)
            {
                LogAndShowError(ex, "загрузке статистики");
            }
        }

        private void LoadStatistics()
        {
            lblTotalDebtorsValue.Text = _viewModel.TotalDebtors.ToString();
            lblCustomerDebtValue.Text = $"{_viewModel.TotalCustomerDebt:N2} Сомони";
            lblSuppliersOwedValue.Text = _viewModel.SuppliersOwed.ToString();
            lblSupplierDebtValue.Text = $"{_viewModel.TotalSupplierDebt:N2} Сомони";

            // Top Parts Chart (o'zgarishsiz qoladi)
            var topParts = _viewModel.TopSellingParts;
            topPartsChart.Series.Clear();
            topPartsChart.Legends[0].Enabled = true;
            for (int i = 0; i < topParts.Count; i++)
            {
                var series = new Charting.Series(topParts[i].PartName)
                {
                    ChartType = Charting.SeriesChartType.Column,
                    Color = _barColors[i % _barColors.Length],
                    ["PointWidth"] = "0.8"
                };
                series.Points.AddXY(topParts[i].PartName, topParts[i].Quantity);
                topPartsChart.Series.Add(series);
            }
            topPartsChart.Titles.Add(new Charting.Title
            {
                Text = "Топ-10 продаваемых запчастей",
                Docking = Charting.Docking.Top,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39)
            });
            topPartsChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            // Top Services Chart (o'zgarishsiz qoladi)
            var topServices = _viewModel.TopServices;
            topServicesChart.Series.Clear();
            topServicesChart.Legends[0].Enabled = true;
            for (int i = 0; i < topServices.Count; i++)
            {
                var series = new Charting.Series(topServices[i].ServiceName)
                {
                    ChartType = Charting.SeriesChartType.Column,
                    Color = _barColors[i % _barColors.Length],
                    ["PointWidth"] = "0.8"
                };
                series.Points.AddXY(topServices[i].ServiceName, topServices[i].Quantity);
                topServicesChart.Series.Add(series);
            }
            topServicesChart.Titles.Add(new Charting.Title
            {
                Text = "Топ-10 услуг",
                Docking = Charting.Docking.Top,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39)
            });
            topServicesChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            // Weekly Sales Chart (yaxshilangan)
            var dailySales = _viewModel.DailySales.OrderByDescending(s => s.DaysAgo).Take(10).ToList();
            weeklySalesChart.Series.Clear();
            weeklySalesChart.ChartAreas[0].InnerPlotPosition = new Charting.ElementPosition(10, 5, 85, 80); // Diagramma maydonini sozlash
            weeklySalesChart.ChartAreas[0].AxisX.Interval = 1; // Aniqlik bilan 1 kunlik interval
            weeklySalesChart.ChartAreas[0].AxisX.LabelStyle.Angle = 45; // Yozuvlar 45 daraja burchakda
            weeklySalesChart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8F); // Yozuv shriftini sozlash
            weeklySalesChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // Grid chiziqlarini o'chirish
            weeklySalesChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(200, 200, 200); // Y o'qi grid chiziqlari rangi
            var partsLineSeries = new Charting.Series("Запчасти")
            {
                ChartType = Charting.SeriesChartType.Spline, // Silliq chiziqlar
                Color = Color.FromArgb(75, 192, 192),
                BorderWidth = 2, // Chiziq qalinligini oshirish
                
            };
            var servicesLineSeries = new Charting.Series("Услуги")
            {
                ChartType = Charting.SeriesChartType.Spline, // Silliq chiziqlar
                Color = Color.FromArgb(153, 102, 255),
                BorderWidth = 2, // Chiziq qalinligini oshirish
                
            };
            for (int i = 0; i < dailySales.Count; i++)
            {
                var sale = dailySales[i];
                string label = sale.DaysAgo == 0 ? "Сегодня" : $"{sale.DaysAgo}";
                partsLineSeries.Points.AddXY(label, sale.PartsQuantity);
                servicesLineSeries.Points.AddXY(label, sale.ServicesQuantity);
            }
            weeklySalesChart.Series.Add(partsLineSeries);
            weeklySalesChart.Series.Add(servicesLineSeries);
            weeklySalesChart.Titles.Add(new Charting.Title
            {
                Text = "Продажи за последние 10 дней",
                Docking = Charting.Docking.Top,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39)
            });
            weeklySalesChart.Legends[0].Font = new Font("Segoe UI", 8F); // Legend shriftini yaxshilash
        }

        private void StartFadeAnimation(bool start)
        {
            _animationTimer.Stop();
            _animationStepCount = 0;

            Color startColor = start ? Color.FromArgb(243, 244, 246) : Color.White;
            Color endColor = start ? Color.White : Color.FromArgb(243, 244, 246);

            _animationTimer.Tick += AnimationTimer_Tick;

            void AnimationTimer_Tick(object s, EventArgs e)
            {
                _animationStepCount++;
                float t = (float)_animationStepCount / _animationSteps;
                int r = (int)(startColor.R + (endColor.R - startColor.R) * t);
                int g = (int)(startColor.G + (endColor.G - startColor.G) * t);
                int b = (int)(startColor.B + (endColor.B - startColor.B) * t);

                r = Math.Clamp(r, 0, 255);
                g = Math.Clamp(g, 0, 255);
                b = Math.Clamp(b, 0, 255);

                Color newColor = Color.FromArgb(r, g, b);
                foreach (var card in new[] { cardTotalDebtors, cardCustomerDebt, cardSuppliersOwed, cardSupplierDebt })
                {
                    card.BackColor = newColor;
                }
                foreach (var chart in new[] { topPartsChart, topServicesChart, weeklySalesChart })
                {
                    chart.BackColor = newColor;
                }

                if (_animationStepCount >= _animationSteps)
                {
                    _animationTimer.Stop();
                    _animationTimer.Tick -= AnimationTimer_Tick;
                    Color finalColor = start ? Color.White : Color.FromArgb(243, 244, 246);
                    foreach (var card in new[] { cardTotalDebtors, cardCustomerDebt, cardSuppliersOwed, cardSupplierDebt })
                    {
                        card.BackColor = finalColor;
                    }
                    foreach (var chart in new[] { topPartsChart, topServicesChart, weeklySalesChart })
                    {
                        chart.BackColor = finalColor;
                    }
                }
            }

            _animationTimer.Start();
        }

        private void LogAndShowError(Exception ex, string operation)
        {
            System.Diagnostics.Debug.WriteLine($"Ошибка при {operation}: {ex.Message}\nStackTrace: {ex.StackTrace}");
            MessageBox.Show($"Произошла ошибка при {operation.ToLower()}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
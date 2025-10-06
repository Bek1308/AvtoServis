using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Models;
using AvtoServis.Model.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AvtoServis.Forms
{
    public partial class BatchDetailsForm : Form
    {
        private readonly int _batchId;
        private readonly IPartsIncomeRepository _repository;
        private readonly ProgressBar _progressBar;

        public BatchDetailsForm(int batchId, IPartsIncomeRepository repository)
        {
            InitializeComponent();
            _batchId = batchId;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            _progressBar = new ProgressBar
            {
                Dock = DockStyle.Top,
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 20,
                Visible = false,
                Height = 4,
                BackColor = Color.FromArgb(245, 245, 245),
                ForeColor = Color.FromArgb(74, 144, 226)
            };
            Controls.Add(_progressBar);

            LoadDetailsAsync();
        }

        private async void LoadDetailsAsync()
        {
            try
            {
                _progressBar.Visible = true;
                lblBatchInfo.Text = "Загрузка сведений...";

                var details = await Task.Run(() => _repository.GetBatchIncomesWithExpenses(_batchId));

                flowLayoutDetails.Controls.Clear();

                if (details == null || details.Incomes == null || !details.Incomes.Any())
                {
                    lblBatchInfo.Text = "Нет данных о лоте.";
                    return;
                }

                lblBatchInfo.Text = $"Партия №{_batchId} - {details.BatchName ?? "Без названия"}";

                foreach (var i in details.Incomes)
                {
                    var card = CreateDetailCard(i);
                    flowLayoutDetails.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке сведений: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _progressBar.Visible = false;
            }
        }

        private Panel CreateDetailCard(IncomeDetail item)
        {
            var card = new Panel
            {
                Width = flowLayoutDetails.Width - 40,
                Height = 220,
                BackColor = Color.White,
                Margin = new Padding(15),
                Padding = new Padding(15),
                Tag = "card"
            };

            card.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, card.Width, card.Height, 12, 12));

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0.5f, 0.5f, card.Width - 1, card.Height - 1);
                }
            };

            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 247, 250);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            var title = new Label
            {
                Text = $"Поступление №{item.Income?.IncomeID ?? 0}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = true,
                Location = new Point(15, 15)
            };
            card.Controls.Add(title);

            var info = new Label
            {
                Text = $"Дата: {item.Income?.Date:dd.MM.yyyy}\n" +
                       $"Кол-во: {item.Income?.Quantity ?? 0} шт.\n" +
                       $"Цена: {item.Income?.UnitPrice:C}\n" +
                       $"Продано: {item.TotalSoldQuantity} шт.\n" +
                       $"Остаток: {item.RemainingQuantity} шт.\n" +
                       $"Сумма продаж: {item.TotalSaleSum:C}\n" +
                       $"Последняя продажа: {(item.LastSaleDate == DateTime.MinValue ? "Нет продаж" : item.LastSaleDate.ToString("dd.MM.yyyy"))}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(15, 45),
                AutoSize = true
            };
            card.Controls.Add(info);

            var statusLabel = new Label
            {
                Text = item.RemainingQuantity > 0 ? "В наличии" : "Полностью продано",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = item.RemainingQuantity > 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60),
                Location = new Point(15, 150),
                AutoSize = true
            };
            card.Controls.Add(statusLabel);

            var btnShowSales = new Button
            {
                Text = "Показать продажи",
                BackColor = Color.FromArgb(74, 144, 226),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Width = 160,
                Height = 36,
                Location = new Point(15, 175),
                Cursor = Cursors.Hand
            };
            btnShowSales.FlatAppearance.BorderSize = 0;
            btnShowSales.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnShowSales.Width, btnShowSales.Height, 8, 8));

            btnShowSales.MouseEnter += (s, e) => btnShowSales.BackColor = Color.FromArgb(52, 120, 200);
            btnShowSales.MouseLeave += (s, e) => btnShowSales.BackColor = Color.FromArgb(74, 144, 226);

            var salesPanel = new Panel
            {
                Visible = false,
                AutoSize = true,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(10),
                Width = card.Width - 30,
                Location = new Point(15, 220),
                BorderStyle = BorderStyle.None
            };

            if (item.SoldExpenses != null && item.SoldExpenses.Any())
            {
                int yOffset = 0;
                foreach (var e in item.SoldExpenses)
                {
                    var lbl = new Label
                    {
                        Text = $"• №{e.ExpenseID} — {e.Quantity} шт., {e.Quantity * (e.UnitPrice):C}, {e.Date:dd.MM.yyyy}, Клиент: {e.CustomerID ?? 0}",
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        ForeColor = Color.FromArgb(60, 60, 60),
                        AutoSize = true,
                        Location = new Point(0, yOffset)
                    };
                    salesPanel.Controls.Add(lbl);
                    yOffset += 25;
                }
            }
            else
            {
                salesPanel.Controls.Add(new Label
                {
                    Text = "Нет сведений о продажах.",
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.FromArgb(120, 120, 120),
                    AutoSize = true
                });
            }

            btnShowSales.Click += (s, e) =>
            {
                salesPanel.Visible = !salesPanel.Visible;
                btnShowSales.Text = salesPanel.Visible ? "Скрыть продажи" : "Показать продажи";
                card.Height = salesPanel.Visible ? 220 + salesPanel.Height + 10 : 220;
            };

            card.Controls.Add(btnShowSales);
            card.Controls.Add(salesPanel);

            return card;
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnClose_MouseEnter(object sender, EventArgs e) =>
            btnClose.BackColor = Color.FromArgb(52, 120, 200);

        private void btnClose_MouseLeave(object sender, EventArgs e) =>
            btnClose.BackColor = Color.FromArgb(74, 144, 226);
    }
}
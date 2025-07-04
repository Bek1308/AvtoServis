namespace AvtoServis.Forms.Controls
{
    public partial class IndexIncome : UserControl
    {
        private const int MinCardWidth = 350; // Minimal kartochka kengligi
        private const int BaseCardHeight = 380; // Kartochka balandligi
        private const int CardMargin = 20; // Chegaradan uzoqroq masofa

        public IndexIncome()
        {
            InitializeComponent();
            LoadMockData();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            this.SizeChanged -= Control_SizeChanged;
            this.SizeChanged += Control_SizeChanged;
            Control_SizeChanged(this, EventArgs.Empty);
        }

        private void Control_SizeChanged(object sender, EventArgs e)
        {
            int availableWidth = this.ClientSize.Width - 2 * CardMargin - 2 * flowLayoutPanelCards.Padding.Horizontal;
            int cardWidth = Math.Max(MinCardWidth, availableWidth);

            foreach (Control card in flowLayoutPanelCards.Controls)
            {
                card.Width = cardWidth;
                card.Height = BaseCardHeight;
                card.Margin = new Padding(CardMargin);
                AdjustCardContent(card, cardWidth);
            }

            flowLayoutPanelCards.Refresh();
        }

        private void LoadMockData()
        {
            flowLayoutPanelCards.Controls.Clear();
            var mockIncomes = new[]
            {
                new { OperationID = 1001, TotalQuantity = 150, PartTypesCount = 10, Suppliers = new[] { "Поставщик A", "Поставщик B" }, RemainingQuantity = 30, RemainingPercentage = 20, StatusName = "Завершено", UserFullName = "John Doe" },
                new { OperationID = 1002, TotalQuantity = 200, PartTypesCount = 15, Suppliers = new[] { "Поставщик B" }, RemainingQuantity = 120, RemainingPercentage = 60, StatusName = "Ожидается", UserFullName = "Jane Smith" },
                new { OperationID = 1003, TotalQuantity = 300, PartTypesCount = 20, Suppliers = new[] { "Поставщик C", "Поставщик D", "Поставщик E" }, RemainingQuantity = 240, RemainingPercentage = 80, StatusName = "Завершено", UserFullName = "Admin User" }
            };

            foreach (var income in mockIncomes)
            {
                var card = CreateIncomeCard(income);
                flowLayoutPanelCards.Controls.Add(card);
            }
        }

        private Panel CreateIncomeCard(dynamic income)
        {
            int cardWidth = MinCardWidth;
            var panel = new Panel
            {
                Size = new Size(cardWidth, BaseCardHeight),
                Padding = new Padding(20),
                Margin = new Padding(CardMargin),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Tag = new { Shadow = true }
            };
            panel.Paint += (s, e) =>
            {
                using (Pen shadowPen = new Pen(Color.FromArgb(50, 0, 0, 0), 2))
                {
                    e.Graphics.DrawRectangle(shadowPen, 1, 1, panel.Width - 2, panel.Height - 2);
                }
            };

            // Title (ajralib turuvchi qism)
            var titleLabel = new Label
            {
                Text = $"Партия # {income.OperationID}",
                Name = "keyTitleLabel",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Partiya raqami (pastda)


            // Quantity
            var keyQuantityLabel = new Label { Text = "Количество запчастей:", Name = "keyQuantityLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 80), AutoSize = true };
            var valueQuantityLabel = new Label { Text = $"{income.TotalQuantity} шт.", Name = "valueQuantityLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Types
            var keyTypesLabel = new Label { Text = "Виды запчастей:", Name = "keyTypesLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 110), AutoSize = true };
            var valueTypesLabel = new Label { Text = $"{income.PartTypesCount} видов", Name = "valueTypesLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Supplier
            var keySupplierLabel = new Label { Text = "Поставщик:", Name = "keySupplierLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 140), AutoSize = true };
            var valueSupplierLabel = new Label { Text = string.Join(", ", income.Suppliers), Name = "valueSupplierLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Remainder
            var keyRemainderLabel = new Label { Text = "Остаток:", Name = "keyRemainderLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 170), AutoSize = true };
            var valueRemainderLabel = new Label { Text = $"{income.RemainingQuantity} шт.", Name = "valueRemainderLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Progress Bar
            var progressBar = new CustomProgressBar
            {
                Location = new Point(20, 200),
                Width = cardWidth - 80,
                Height = 15,
                Value = (int)income.RemainingPercentage,
                Maximum = 100
            };

            // Status
            var keyStatusLabel = new Label { Text = "Статус:", Name = "keyStatusLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 230), AutoSize = true };
            var valueStatusLabel = new Label { Text = income.StatusName, Name = "valueStatusLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Added By
            var keyAddedByLabel = new Label { Text = "Добавил:", Name = "keyAddedByLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 260), AutoSize = true };
            var valueAddedByLabel = new Label { Text = income.UserFullName, Name = "valueAddedByLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Details Button
            var detailsButton = new Button
            {
                Text = "Подробности",
                BackColor = Color.FromArgb(40, 167, 69),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(140, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(cardWidth - 140, 320)
            };
            detailsButton.Click += (s, e) => MessageBox.Show($"Подробности партии #{income.OperationID}", "Информация");

            panel.Controls.Add(titleLabel);

            panel.Controls.Add(keyQuantityLabel);
            panel.Controls.Add(valueQuantityLabel);
            panel.Controls.Add(keyTypesLabel);
            panel.Controls.Add(valueTypesLabel);
            panel.Controls.Add(keySupplierLabel);
            panel.Controls.Add(valueSupplierLabel);
            panel.Controls.Add(keyRemainderLabel);
            panel.Controls.Add(valueRemainderLabel);
            panel.Controls.Add(progressBar);
            panel.Controls.Add(keyStatusLabel);
            panel.Controls.Add(valueStatusLabel);
            panel.Controls.Add(keyAddedByLabel);
            panel.Controls.Add(valueAddedByLabel);
            panel.Controls.Add(detailsButton);

            AdjustCardContent(panel, cardWidth);

            return panel;
        }

        private void AdjustCardContent(Control card, int cardWidth)
        {
            foreach (Control control in card.Controls)
            {
                if (control is Label label)
                {
                    switch (label.Name)
                    {
                        case "keyTitleLabel":
                            label.Location = new Point(20, 20);
                            break;
                        case "valueTitleLabel":
                            label.Location = new Point(20, 50);
                            break;
                        case "keyQuantityLabel":
                            label.Location = new Point(20, 80);
                            var valueQuantity = card.Controls.Find("valueQuantityLabel", true)[0] as Label;
                            if (valueQuantity != null) valueQuantity.Location = new Point(cardWidth - valueQuantity.Width - 20, 80);
                            break;
                        case "keyTypesLabel":
                            label.Location = new Point(20, 110);
                            var valueTypes = card.Controls.Find("valueTypesLabel", true)[0] as Label;
                            if (valueTypes != null) valueTypes.Location = new Point(cardWidth - valueTypes.Width - 20, 110);
                            break;
                        case "keySupplierLabel":
                            label.Location = new Point(20, 140);
                            var valueSupplier = card.Controls.Find("valueSupplierLabel", true)[0] as Label;
                            if (valueSupplier != null) valueSupplier.Location = new Point(cardWidth - valueSupplier.Width - 20, 140);
                            break;
                        case "keyRemainderLabel":
                            label.Location = new Point(20, 170);
                            var valueRemainder = card.Controls.Find("valueRemainderLabel", true)[0] as Label;
                            if (valueRemainder != null) valueRemainder.Location = new Point(cardWidth - valueRemainder.Width - 20, 170);
                            break;
                        case "keyStatusLabel":
                            label.Location = new Point(20, 230);
                            var valueStatus = card.Controls.Find("valueStatusLabel", true)[0] as Label;
                            if (valueStatus != null) valueStatus.Location = new Point(cardWidth - valueStatus.Width - 20, 230);
                            break;
                        case "keyAddedByLabel":
                            label.Location = new Point(20, 260);
                            var valueAddedBy = card.Controls.Find("valueAddedByLabel", true)[0] as Label;
                            if (valueAddedBy != null) valueAddedBy.Location = new Point(cardWidth - valueAddedBy.Width - 20, 260);
                            break;
                    }
                }
                else if (control is CustomProgressBar progressBar)
                {
                    progressBar.Width = cardWidth - 40;
                    progressBar.Location = new Point(20, 200);
                }
                else if (control is Button button)
                {
                    button.Location = new Point(cardWidth - button.Width - 20, 320);
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            PartsIncomeForm form = new PartsIncomeForm();
            form.ShowDialog();
        }
    }
}
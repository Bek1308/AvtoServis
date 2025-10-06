using AvtoServis.Data.Interfaces;
using AvtoServis.Data.Repositories; // Repository uchun
using AvtoServis.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AvtoServis.Forms; // BatchDetailsForm uchun (yangi using qo'shildi)

namespace AvtoServis.Forms.Controls
{
    public partial class IndexIncome : UserControl
    {
        private const int MinCardWidth = 350; // Minimal kartochka kengligi
        private const int BaseCardHeight = 380; // Kartochka balandligi
        private const int CardMargin = 20; // Chegaradan uzoqroq masofa

        // Ma'lumotlar ro'yxati va repository
        private List<IncomeDto> allIncomes = new List<IncomeDto>(); // Barcha ma'lumotlar
        private List<IncomeDto> filteredIncomes = new List<IncomeDto>(); // Filterlangan ma'lumotlar
        private readonly IPartsIncomeRepository _repository; // Repository (constructor da inject qiling)

        public IndexIncome(IPartsIncomeRepository repository = null) // Repository ni constructor orqali oling (MainForm dan)
        {
            InitializeComponent();
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), "Repository bo'sh bo'lmasligi kerak!");
            LoadData(); // Real ma'lumotlar yuklash
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            AdjustLayout();

            // SearchBox event'lari
            searchBox.TextChanged += searchBox_TextChanged;
            searchBox.Enter += searchBox_Enter;
            searchBox.Leave += searchBox_Leave;
            UpdateSearchPlaceholder(); // Dastlabki holat
        }

        // Placeholder logic
        private void UpdateSearchPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = "Поиск...";
                searchBox.ForeColor = Color.FromArgb(108, 117, 125); // Kulrang
            }
            else
            {
                searchBox.ForeColor = Color.Black; // Qora
            }
        }

        private void searchBox_Enter(object sender, EventArgs e)
        {
            if (searchBox.Text == "Поиск...")
            {
                searchBox.Text = string.Empty;
                searchBox.ForeColor = Color.Black;
            }
        }

        private void searchBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBox.Text))
            {
                UpdateSearchPlaceholder();
            }
        }

        // Search funksiyasi (real-time filter) - BatchName qidirish saqlanib qoldi
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.ToLower().Trim();
            if (searchText == "Поиск..." || string.IsNullOrEmpty(searchText))
            {
                filteredIncomes = new List<IncomeDto>(allIncomes); // Barchasini ko'rsatish
            }
            else
            {
                filteredIncomes = allIncomes.Where(income =>
                    income.BatchId.ToString().Contains(searchText) || // BatchId
                    (!string.IsNullOrEmpty(income.BatchName) && income.BatchName.ToLower().Contains(searchText)) || // BatchName
                    income.Suppliers.Any(s => s.ToLower().Contains(searchText)) ||
                    income.StatusName.ToLower().Contains(searchText) ||
                    income.UserFullName.ToLower().Contains(searchText)
                ).ToList();
            }
            RefreshCards(); // Kartochkalarni qayta chizish
        }

        // Real ma'lumotlar yuklash method (haqiqiy repository chaqiruvi, sinxron)
        private void LoadData()
        {
            try
            {
                // Repository dan haqiqiy ma'lumot olish (sinxron)
                allIncomes = _repository.GetAllIncomes(); // Sinxron method chaqiruvi

                // Agar ma'lumotlar bo'sh bo'lsa, placeholder kartochka qo'shish mumkin (ixtiyoriy)
                if (allIncomes == null || !allIncomes.Any())
                {
                    // Bo'sh holat uchun placeholder (ixtiyoriy)
                    var emptyCard = CreateEmptyCard();
                    flowLayoutPanelCards.Controls.Add(emptyCard);
                }
                else
                {
                    filteredIncomes = new List<IncomeDto>(allIncomes);
                    RefreshCards(); // Kartochkalarni chizish
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ma'lumotlarni yuklashda xato: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadData Error: {ex.Message}");

                // Xato bo'lganda ham placeholder ko'rsatish (ixtiyoriy)
                var errorCard = CreateErrorCard(ex.Message);
                flowLayoutPanelCards.Controls.Add(errorCard);
            }
        }

        // Ixtiyoriy: Bo'sh ma'lumotlar uchun placeholder kartochka
        private Panel CreateEmptyCard()
        {
            var panel = new Panel
            {
                Size = new Size(MinCardWidth, BaseCardHeight),
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };
            var label = new Label
            {
                Text = "Ма'lumотlar topilmadi. Yangi партия qo'shing!",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                ForeColor = Color.Gray
            };
            panel.Controls.Add(label);
            return panel;
        }

        // Ixtiyoriy: Xato holati uchun kartochka
        private Panel CreateErrorCard(string errorMessage)
        {
            var panel = new Panel
            {
                Size = new Size(MinCardWidth, BaseCardHeight),
                BackColor = Color.LightPink,
                BorderStyle = BorderStyle.FixedSingle
            };
            var label = new Label
            {
                Text = $"Xato: {errorMessage}\n\nRetry tugmasini bosing yoki dasturni qayta ishga tushiring.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Red
            };
            var retryButton = new Button
            {
                Text = "Retry",
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White
            };
            retryButton.Click += (s, e) => LoadData(); // Qayta yuklash
            panel.Controls.Add(retryButton);
            panel.Controls.Add(label);
            return panel;
        }

        // Filterlangan ma'lumotlar bo'yicha kartochkalarni qayta chizish
        private void RefreshCards()
        {
            flowLayoutPanelCards.Controls.Clear();
            foreach (var income in filteredIncomes)
            {
                var card = CreateIncomeCard(income);
                flowLayoutPanelCards.Controls.Add(card);
            }
            Control_SizeChanged(this, EventArgs.Empty); // Layout ni yangilash
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

        private Panel CreateIncomeCard(IncomeDto income)
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

            // Title (BatchId + BatchName birlashtirildi)
            var titleLabel = new Label
            {
                Text = !string.IsNullOrEmpty(income.BatchName)
                    ? $"Партия # {income.BatchId} - {income.BatchName}"  // BatchName bor bo'lsa, birlashtirish
                    : $"Партия # {income.BatchId}",  // Bo'sh bo'lsa, faqat BatchId
                Name = "keyTitleLabel",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Quantity
            var keyQuantityLabel = new Label { Text = "Количество запчастей:", Name = "keyQuantityLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 50), AutoSize = true };
            var valueQuantityLabel = new Label { Text = $"{income.TotalQuantity} шт.", Name = "valueQuantityLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Types
            var keyTypesLabel = new Label { Text = "Виды запчастей:", Name = "keyTypesLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 80), AutoSize = true };
            var valueTypesLabel = new Label { Text = $"{income.PartTypesCount} видов", Name = "valueTypesLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Supplier
            var keySupplierLabel = new Label { Text = "Поставщик:", Name = "keySupplierLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 110), AutoSize = true };
            var valueSupplierLabel = new Label { Text = string.Join(", ", income.Suppliers), Name = "valueSupplierLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Remainder
            var keyRemainderLabel = new Label { Text = "Остаток:", Name = "keyRemainderLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 140), AutoSize = true };
            var valueRemainderLabel = new Label { Text = $"{income.RemainingQuantity} шт.", Name = "valueRemainderLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Progress Bar
            var progressBar = new CustomProgressBar
            {
                Location = new Point(20, 170),
                Width = cardWidth - 80,
                Height = 15,
                Value = income.RemainingPercentage,
                Maximum = 100
            };

            // Status
            var keyStatusLabel = new Label { Text = "Статус:", Name = "keyStatusLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 200), AutoSize = true };
            var valueStatusLabel = new Label { Text = income.StatusName, Name = "valueStatusLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Added By
            var keyAddedByLabel = new Label { Text = "Добавил:", Name = "keyAddedByLabel", Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 230), AutoSize = true };
            var valueAddedByLabel = new Label { Text = income.UserFullName, Name = "valueAddedByLabel", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true };

            // Details Button (BatchDetailsForm bilan yangilandi)
            var detailsButton = new Button
            {
                Text = "Подробности",
                BackColor = Color.FromArgb(40, 167, 69),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(140, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(cardWidth - 140, 290) // Joylashuv
            };
            detailsButton.Click += (s, e) =>
            {
                try
                {
                    // Repository ni cast qilish (interface dan implementatsiyaga)
                    
                    //if (concreteRepo == null)
                    //{
                    //    MessageBox.Show("Repository xatosi: Details ochib bo'lmadi.", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    using (var detailsForm = new BatchDetailsForm(income.BatchId, _repository))  // Yangi form ochish
                    {
                        detailsForm.ShowDialog();  // Modal dialog
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Details ochishda xato: {ex.Message}", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

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
                            label.Location = new Point(20, 50);
                            var valueQuantity = card.Controls.Find("valueQuantityLabel", true)[0] as Label;
                            if (valueQuantity != null) valueQuantity.Location = new Point(cardWidth - valueQuantity.Width - 20, 50);
                            break;
                        case "keyTypesLabel":
                            label.Location = new Point(20, 80);
                            var valueTypes = card.Controls.Find("valueTypesLabel", true)[0] as Label;
                            if (valueTypes != null) valueTypes.Location = new Point(cardWidth - valueTypes.Width - 20, 80);
                            break;
                        case "keySupplierLabel":
                            label.Location = new Point(20, 110);
                            var valueSupplier = card.Controls.Find("valueSupplierLabel", true)[0] as Label;
                            if (valueSupplier != null) valueSupplier.Location = new Point(cardWidth - valueSupplier.Width - 20, 110);
                            break;
                        case "keyRemainderLabel":
                            label.Location = new Point(20, 140);
                            var valueRemainder = card.Controls.Find("valueRemainderLabel", true)[0] as Label;
                            if (valueRemainder != null) valueRemainder.Location = new Point(cardWidth - valueRemainder.Width - 20, 140);
                            break;
                        case "keyStatusLabel":
                            label.Location = new Point(20, 200);
                            var valueStatus = card.Controls.Find("valueStatusLabel", true)[0] as Label;
                            if (valueStatus != null) valueStatus.Location = new Point(cardWidth - valueStatus.Width - 20, 200);
                            break;
                        case "keyAddedByLabel":
                            label.Location = new Point(20, 230);
                            var valueAddedBy = card.Controls.Find("valueAddedByLabel", true)[0] as Label;
                            if (valueAddedBy != null) valueAddedBy.Location = new Point(cardWidth - valueAddedBy.Width - 20, 230);
                            break;
                    }
                }
                else if (control is CustomProgressBar progressBar)
                {
                    progressBar.Width = cardWidth - 40;
                    progressBar.Location = new Point(20, 170);
                }
                else if (control is Button button)
                {
                    button.Location = new Point(cardWidth - button.Width - 20, 290); // Siljdi
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            PartsIncomeForm form = new PartsIncomeForm();
            form.ShowDialog();
            LoadData(); // Yangi ma'lumot qo'shilgandan keyin qayta yuklash
        }
    }
}
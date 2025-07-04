using System.Text.RegularExpressions;

namespace AvtoServis.Forms
{
    public partial class PartsIncomeForm : Form
    {
        private List<PartsIncome> _dataSource;
        private System.Windows.Forms.Timer _errorTimer;
        private ImageList _actionImageList; // Kontekst menyusi uchun ImageList

        private class PartsIncome
        {
            public int IncomeID { get; set; }
            public int PartID { get; set; }
            public int SupplierID { get; set; }
            public DateTime Date { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Markup { get; set; }
            public int StatusID { get; set; }
            public int StockID { get; set; }
            public string InvoiceNumber { get; set; }
        }

        public PartsIncomeForm()
        {
            InitializeComponent();
            _dataSource = new List<PartsIncome>();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            _actionImageList = new ImageList { ImageSize = new Size(24, 24) }; // Vaqtinchalik ImageList
            ConfigureDataGridView();
            InitializeComboBoxes();
            EnhanceVisualStyles();
            // Batch raqamini sozlash
            batchNumberLabel.Text = $"Номер партии: {Guid.NewGuid().ToString().Substring(0, 8)}";
        }

        private void ConfigureDataGridView()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IncomeID",
                HeaderText = "ID поступления",
                DataPropertyName = "IncomeID",
                ReadOnly = true,
                Width = 100
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PartID",
                HeaderText = "Деталь",
                DataPropertyName = "PartID",
                ReadOnly = true,
                Width = 80
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SupplierID",
                HeaderText = "Поставщик",
                DataPropertyName = "SupplierID",
                ReadOnly = true,
                Width = 100
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "Дата",
                DataPropertyName = "Date",
                ReadOnly = true,
                Width = 100
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Количество",
                DataPropertyName = "Quantity",
                ReadOnly = true,
                Width = 80
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitPrice",
                HeaderText = "Цена за ед.",
                DataPropertyName = "UnitPrice",
                ReadOnly = true,
                Width = 100
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Markup",
                HeaderText = "Наценка",
                DataPropertyName = "Markup",
                ReadOnly = true,
                Width = 80
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StatusID",
                HeaderText = "Статус",
                DataPropertyName = "StatusID",
                ReadOnly = true,
                Width = 80
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockID",
                HeaderText = "Склад",
                DataPropertyName = "StockID",
                ReadOnly = true,
                Width = 80
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InvoiceNumber",
                HeaderText = "Номер счета",
                DataPropertyName = "InvoiceNumber",
                ReadOnly = true,
                Width = 120
            });
            dataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Actions",
                HeaderText = "Действия",
                Text = "...",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 80
            });

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.CellClick += DataGridView_CellClick;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView.Columns[e.ColumnIndex].Name == "Actions")
            {
                var income = _dataSource[e.RowIndex];
                var menu = new ContextMenuStrip
                {
                    ImageScalingSize = new Size(24, 24),
                    Renderer = new CustomToolStripRenderer()
                };

                var editItem = new ToolStripMenuItem
                {
                    Text = "Редактировать",
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Size = new Size(0, 32),
                    Tag = "Edit"
                };
                editItem.Click += (s, ev) => EditIncome(income);
                menu.Items.Add(editItem);

                var deleteItem = new ToolStripMenuItem
                {
                    Text = "Удалить",
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Size = new Size(0, 32),
                    Tag = "Delete"
                };
                deleteItem.Click += (s, ev) => DeleteIncome(income);
                menu.Items.Add(deleteItem);

                menu.Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
            }
        }

        private void EditIncome(PartsIncome income)
        {
            // Tahrirlash uchun ma'lumotlarni formaga yuklash
            cmbPartID.SelectedItem = income.PartID;
            cmbSupplierID.SelectedItem = income.SupplierID;
            dtpDate.Value = income.Date;
            txtQuantity.Text = income.Quantity.ToString();
            txtUnitPrice.Text = income.UnitPrice.ToString();
            txtMarkup.Text = income.Markup.ToString();
            cmbStatusID.SelectedItem = income.StatusID;
            cmbStockID.SelectedItem = income.StockID;
            txtInvoiceNumber.Text = income.InvoiceNumber;

            // Ma'lumotni yangilash uchun Add tugmasi o'rniga yangi logika
            btnAdd.Text = "Обновить";
            btnAdd.Click -= BtnAdd_Click;
            btnAdd.Click += (s, e) =>
            {
                if (!ValidateInputs()) return;

                income.PartID = (int)cmbPartID.SelectedItem;
                income.SupplierID = (int)cmbSupplierID.SelectedItem;
                income.Date = dtpDate.Value;
                income.Quantity = int.Parse(txtQuantity.Text);
                income.UnitPrice = decimal.Parse(txtUnitPrice.Text);
                income.Markup = decimal.Parse(txtMarkup.Text);
                income.StatusID = (int)cmbStatusID.SelectedItem;
                income.StockID = (int)cmbStockID.SelectedItem;
                income.InvoiceNumber = txtInvoiceNumber.Text;

                RefreshDataGridView();
                ShowSuccess("Запись успешно обновлена!");
                ClearInputs();
                btnAdd.Text = "Добавить";
                //btnAdd.Click -= btnAdd.Click;
                btnAdd.Click += BtnAdd_Click;
            };
        }

        private void DeleteIncome(PartsIncome income)
        {
            var result = MessageBox.Show($"Вы уверены, что хотите удалить поступление #{income.IncomeID}?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _dataSource.Remove(income);
                RefreshDataGridView();
                ShowSuccess("Запись успешно удалена!");
            }
        }

        private void InitializeComboBoxes()
        {
            cmbPartID.Items.AddRange(new object[] { 1, 2, 3 });
            cmbSupplierID.Items.AddRange(new object[] { 1, 2, 3 });
            cmbStatusID.Items.AddRange(new object[] { 1, 2, 3 });
            cmbStockID.Items.AddRange(new object[] { 1, 2, 3 });
        }

        private void EnhanceVisualStyles()
        {
            btnOpenFilterDialog.BackColor = Color.FromArgb(25, 118, 210);
            btnOpenFilterDialog.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnExport.BackColor = Color.FromArgb(40, 167, 69);
            btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnImport.BackColor = Color.FromArgb(25, 118, 210);
            btnImport.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnAdd.BackColor = Color.FromArgb(25, 118, 210);
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 187, 89);
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 140, 150);

            dataGridView.BackgroundColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 243, 245);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 255);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void SearchBox_GotFocus(object sender, EventArgs e)
        {
            if (searchBox.Text == "Поиск...")
            {
                searchBox.Text = "";
                searchBox.ForeColor = Color.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchBox.Text))
            {
                searchBox.Text = "Поиск...";
                searchBox.ForeColor = Color.FromArgb(108, 117, 125);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var newIncome = new PartsIncome
            {
                IncomeID = _dataSource.Count + 1,
                PartID = (int)cmbPartID.SelectedItem,
                SupplierID = (int)cmbSupplierID.SelectedItem,
                Date = dtpDate.Value,
                Quantity = int.Parse(txtQuantity.Text),
                UnitPrice = decimal.Parse(txtUnitPrice.Text),
                Markup = decimal.Parse(txtMarkup.Text),
                StatusID = (int)cmbStatusID.SelectedItem,
                StockID = (int)cmbStockID.SelectedItem,
                InvoiceNumber = txtInvoiceNumber.Text
            };

            _dataSource.Add(newIncome);
            RefreshDataGridView();
            ShowSuccess("Запись успешно добавлена в таблицу!");
            ClearInputs();
        }

        private bool ValidateInputs()
        {
            lblError.Visible = false;
            panelError.Visible = false;

            if (cmbPartID.SelectedItem == null)
            {
                ShowError("Пожалуйста, выберите деталь.");
                return false;
            }

            if (cmbSupplierID.SelectedItem == null)
            {
                ShowError("Пожалуйста, выберите поставщика.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                ShowError("Пожалуйста, введите корректное количество (положительное целое число).");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUnitPrice.Text) || !decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) || unitPrice <= 0)
            {
                ShowError("Пожалуйста, введите корректную цену за единицу (положительное число).");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMarkup.Text) || !decimal.TryParse(txtMarkup.Text, out decimal markup) || markup < 0)
            {
                ShowError("Пожалуйста, введите корректную наценку (неотрицательное число).");
                return false;
            }

            if (cmbStatusID.SelectedItem == null)
            {
                ShowError("Пожалуйста, выберите статус.");
                return false;
            }

            if (cmbStockID.SelectedItem == null)
            {
                ShowError("Пожалуйста, выберите склад.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text) || !Regex.IsMatch(txtInvoiceNumber.Text, @"^[a-zA-Z0-9]+$"))
            {
                ShowError("Пожалуйста, введите корректный номер счета (только латинские буквы и цифры).");
                return false;
            }

            if (txtInvoiceNumber.Text.Length > 50)
            {
                ShowError("Номер счета не должен превышать 50 символов.");
                return false;
            }

            return true;
        }

        private void RefreshDataGridView()
        {
            dataGridView.Rows.Clear();
            foreach (var income in _dataSource)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);
                row.Cells[0].Value = income.IncomeID;
                row.Cells[1].Value = income.PartID;
                row.Cells[2].Value = income.SupplierID;
                row.Cells[3].Value = income.Date.ToShortDateString();
                row.Cells[4].Value = income.Quantity;
                row.Cells[5].Value = income.UnitPrice;
                row.Cells[6].Value = income.Markup;
                row.Cells[7].Value = income.StatusID;
                row.Cells[8].Value = income.StockID;
                row.Cells[9].Value = income.InvoiceNumber;
                row.Cells[10].Value = "...";
                dataGridView.Rows.Add(row);
            }
            countLabel.Text = $"Поступления: {_dataSource.Count}";
            dataGridView.Refresh();
        }

        private void ClearInputs()
        {
            cmbPartID.SelectedIndex = -1;
            cmbSupplierID.SelectedIndex = -1;
            dtpDate.Value = DateTime.Now;
            txtQuantity.Clear();
            txtUnitPrice.Clear();
            txtMarkup.Clear();
            cmbStatusID.SelectedIndex = -1;
            cmbStockID.SelectedIndex = -1;
            txtInvoiceNumber.Clear();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            panelError.BackColor = Color.FromArgb(255, 245, 245);
            lblError.Visible = true;
            panelError.Visible = true;
            _errorTimer.Start();
        }

        private void ShowSuccess(string message)
        {
            lblError.Text = message;
            lblError.ForeColor = Color.FromArgb(40, 167, 69);
            panelError.BackColor = Color.FromArgb(245, 255, 245);
            lblError.Visible = true;
            panelError.Visible = true;
            _errorTimer.Start();
        }

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            panelError.Visible = false;
            _errorTimer.Stop();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_dataSource.Count == 0)
            {
                ShowError("Нет данных для сохранения.");
                return;
            }

            try
            {
                ShowSuccess("Данные успешно подготовлены для сохранения!");
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private class CustomToolStripRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                var item = e.Item as ToolStripMenuItem;
                if (item != null)
                {
                    Color backgroundColor = item.Selected ? Color.FromArgb(200, 230, 255) : Color.White;
                    if (item.Tag?.ToString() == "Edit")
                        backgroundColor = item.Selected ? Color.FromArgb(50, 140, 230) : Color.FromArgb(25, 118, 210);
                    else if (item.Tag?.ToString() == "Delete")
                        backgroundColor = item.Selected ? Color.FromArgb(255, 100, 100) : Color.FromArgb(220, 53, 69);

                    using (var brush = new SolidBrush(backgroundColor))
                    {
                        e.Graphics.FillRectangle(brush, new Rectangle(0, 0, e.Item.Width, e.Item.Height));
                    }
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = e.Item.Selected ? Color.Black : Color.White;
                base.OnRenderItemText(e);
            }
        }
    }
}
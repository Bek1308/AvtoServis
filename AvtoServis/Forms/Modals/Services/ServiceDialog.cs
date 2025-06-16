using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class ServiceDialog : Form
    {
        private readonly ServicesViewModel _viewModel;
        private readonly int? _serviceId;
        private readonly bool _isDeleteMode;
        private Service? _service;
        private Label lblName;
        private TextBox txtName;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblError;
        private Label lblMessage;
        private Button btnSave;
        private Button btnCancel;
        private TableLayoutPanel tableLayoutPanel;

        public ServiceDialog(ServicesViewModel viewModel, int? serviceId, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _serviceId = serviceId;
            _isDeleteMode = isDeleteMode;
            InitializeComponents();
            if (!_isDeleteMode) LoadService();
        }

        private void InitializeComponents()
        {
            Text = _isDeleteMode ? "Подтверждение удаления" : (_serviceId == null ? "Новая услуга" : "Редактировать услугу");
            Size = _isDeleteMode ? new Size(350, 180) : new Size(450, 320);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(245, 245, 245);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

            tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = _isDeleteMode ? 2 : 6,
                Dock = DockStyle.Fill,
                Padding = new Padding(15), // 20 dan 15 ga kamaytirildi
                AutoSize = true
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            if (_isDeleteMode)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // lblMessage
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Buttons
            }
            else
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // lblName
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // txtName
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // lblPrice
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // txtPrice
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // lblError
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Buttons
            }

            if (_isDeleteMode)
            {
                lblMessage = new Label
                {
                    Text = "Вы уверены, что хотите удалить услугу?",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold), // Qalin va kattaroq
                    AccessibleName = "Сообщение подтверждения",
                    AccessibleDescription = "Подтверждение удаления услуги"
                };
                tableLayoutPanel.Controls.Add(lblMessage, 0, 0);
                tableLayoutPanel.SetColumnSpan(lblMessage, 2);
            }
            else
            {
                lblName = new Label
                {
                    Text = "Название услуги:",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    AccessibleName = "Название услуги",
                    AccessibleDescription = "Метка для поля названия услуги"
                };
                tableLayoutPanel.Controls.Add(lblName, 0, 0);
                tableLayoutPanel.SetColumnSpan(lblName, 2);

                txtName = new TextBox
                {
                    Size = new Size(350, 30),
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                    AccessibleName = "Поле названия услуги",
                    AccessibleDescription = "Введите название услуги (русские или латинские буквы)"
                };
                txtName.TextChanged += (s, e) => ValidateInputs();
                tableLayoutPanel.Controls.Add(txtName, 0, 1);
                tableLayoutPanel.SetColumnSpan(txtName, 2);

                lblPrice = new Label
                {
                    Text = "Цена (₽):",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    ForeColor = Color.FromArgb(33, 37, 41),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    AccessibleName = "Цена",
                    AccessibleDescription = "Метка для поля цены"
                };
                tableLayoutPanel.Controls.Add(lblPrice, 0, 2);
                tableLayoutPanel.SetColumnSpan(lblPrice, 2);

                txtPrice = new TextBox
                {
                    Size = new Size(350, 30),
                    BorderStyle = BorderStyle.FixedSingle,
                    AccessibleName = "Поле цены",
                    AccessibleDescription = "Введите цену услуги (например, 1000.50)"
                };
                txtPrice.KeyPress += (s, e) =>
                {
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
                        e.Handled = true;
                    if (e.KeyChar == '.' && txtPrice.Text.Contains("."))
                        e.Handled = true;
                };
                txtPrice.TextChanged += (s, e) => ValidateInputs();
                tableLayoutPanel.Controls.Add(txtPrice, 0, 3);
                tableLayoutPanel.SetColumnSpan(txtPrice, 2);

                lblError = new Label
                {
                    Text = "",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    ForeColor = Color.FromArgb(220, 53, 69),
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    Visible = false,
                    AccessibleName = "Сообщение об ошибке",
                    AccessibleDescription = "Отображает ошибки или статус операции"
                };
                tableLayoutPanel.Controls.Add(lblError, 0, 4);
                tableLayoutPanel.SetColumnSpan(lblError, 2);
            }

            btnCancel = new Button
            {
                Text = _isDeleteMode ? "Нет" : "Отмена",
                Size = new Size(120, 40), // 150 dan 120 ga kamaytirildi
                BackColor = _isDeleteMode ? Color.FromArgb(25, 118, 210) : Color.FromArgb(220, 53, 69), // Ko'k yoki Qizil
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = _isDeleteMode ? Color.FromArgb(50, 140, 230) : Color.FromArgb(255, 100, 100) },
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Anchor = AnchorStyles.Left,
                AccessibleName = _isDeleteMode ? "Нет" : "Отмена",
                AccessibleDescription = _isDeleteMode ? "Отменяет удаление услуги" : "Закрывает окно без сохранения"
            };
            btnCancel.Click += (s, e) => Close();
            tableLayoutPanel.Controls.Add(btnCancel, 0, _isDeleteMode ? 1 : 5);

            btnSave = new Button
            {
                Text = _isDeleteMode ? "Да" : "Сохранить",
                Size = new Size(120, 40), // 150 dan 120 ga kamaytirildi
                BackColor = _isDeleteMode ? Color.FromArgb(220, 53, 69) : Color.FromArgb(25, 118, 210), // Qizil yoki Ko'k
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = _isDeleteMode ? Color.FromArgb(255, 100, 100) : Color.FromArgb(50, 140, 230) },
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Anchor = AnchorStyles.Right,
                AccessibleName = _isDeleteMode ? "Да" : "Сохранить",
                AccessibleDescription = _isDeleteMode ? "Подтверждает удаление услуги" : "Сохраняет изменения в услуге"
            };
            btnSave.Click += BtnSave_Click;
            tableLayoutPanel.Controls.Add(btnSave, 1, _isDeleteMode ? 1 : 5);

            Controls.Add(tableLayoutPanel);

            var toolTip = new ToolTip();
            if (!_isDeleteMode)
            {
                toolTip.SetToolTip(txtName, "Введите название услуги (русские или латинские буквы, максимум 100 символов)");
                toolTip.SetToolTip(txtPrice, "Введите цену услуги (например, 1000.50)");
                toolTip.SetToolTip(lblError, "Отображает ошибки или статус операции");
            }
            else
            {
                toolTip.SetToolTip(lblMessage, "Подтверждение удаления услуги");
            }
            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление" : "Сохранить изменения");
            toolTip.SetToolTip(btnCancel, _isDeleteMode ? "Отменить удаление" : "Закрыть без сохранения");
        }

        private void LoadService()
        {
            try
            {
                _service = _serviceId == null ? new Service() : _viewModel.LoadServices().Find(s => s.ServiceID == _serviceId);
                if (_service == null && _serviceId != null)
                {
                    ShowError("Услуга не найдена.");
                    btnSave.Enabled = false;
                    return;
                }
                txtName.Text = _service.Name;
                txtPrice.Text = _service.Price.ToString();
                ValidateInputs();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка загрузки услуги: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadService Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void ValidateInputs()
        {
            lblError.Visible = false;
            btnSave.Enabled = true;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Пожалуйста, заполните название услуги.");
                btnSave.Enabled = false;
                return;
            }

            if (txtName.Text.Length > 100)
            {
                ShowError("Название услуги не должно превышать 100 символов.");
                btnSave.Enabled = false;
                return;
            }

            if (!Regex.IsMatch(txtName.Text, @"^[\p{L}\s\d]+$"))
            {
                ShowError("Название услуги может содержать только буквы, пробелы и цифры.");
                btnSave.Enabled = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                ShowError("Пожалуйста, заполните цену услуги.");
                btnSave.Enabled = false;
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                ShowError("Введите корректное значение для цены (например, 1000.50).");
                btnSave.Enabled = false;
                return;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isDeleteMode)
                {
                    if (_serviceId == null)
                        throw new Exception("Идентификатор услуги не указан.");
                    _viewModel.DeleteService(_serviceId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                ValidateInputs();
                if (!btnSave.Enabled)
                    return;

                if (_service == null)
                    throw new Exception("Ошибка инициализации услуги.");

                _service.Name = txtName.Text.Trim();
                _service.Price = decimal.Parse(txtPrice.Text);

                if (_serviceId == null)
                {
                    _viewModel.AddService(_service);
                    ShowSuccess("Услуга успешно добавлена.");
                }
                else
                {
                    _viewModel.UpdateService(_service);
                    ShowSuccess("Услуга успешно обновлена.");
                }

                DialogResult = DialogResult.OK;
                var timer = new System.Windows.Forms.Timer { Interval = 1000 };
                timer.Tick += (s, ev) =>
                {
                    timer.Stop();
                    Close();
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                if (!_isDeleteMode)
                    ShowError($"Ошибка: {ex.Message}");
                else
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"BtnSave_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.ForeColor = Color.FromArgb(220, 53, 69); // Qizil
            lblError.Visible = true;
        }

        private void ShowSuccess(string message)
        {
            lblError.Text = message;
            lblError.ForeColor = Color.FromArgb(40, 167, 69); // Yashil
            lblError.Visible = true;
        }
    }
}
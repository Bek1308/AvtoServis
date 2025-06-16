using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace AvtoServis.Forms.Modals.Manufacturers
{
    public class ManufacturerDialog : Form
    {
        private readonly ManufacturersViewModel _viewModel;
        private readonly int? _manufacturerId;
        private readonly bool _isDeleteMode;
        private TableLayoutPanel tableLayoutPanel;
        private Label lblName;
        private TextBox txtName;
        private Button btnSave;
        private Button btnCancel;

        public ManufacturerDialog(ManufacturersViewModel viewModel, int? manufacturerId = null, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _manufacturerId = manufacturerId;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            InitializeData();
        }

        private void InitializeComponent()
        {
            Text = _isDeleteMode ? "Удаление производителя" : _manufacturerId.HasValue ? "Редактирование производителя" : "Добавление производителя";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(400, _isDeleteMode ? 180 : 220);
            BackColor = Color.FromArgb(245, 245, 245);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

            tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = _isDeleteMode ? 2 : 3,
                Dock = DockStyle.Fill,
                Padding = new Padding(16),
                AutoSize = true
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            if (!_isDeleteMode)
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            if (_isDeleteMode)
            {
                lblName = new Label
                {
                    Text = "Вы уверены?",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    ForeColor = Color.FromArgb(33, 37, 41),
                    AccessibleName = "Подтверждение удаления",
                    AccessibleDescription = "Подтверждение удаления производителя"
                };
                tableLayoutPanel.Controls.Add(lblName, 0, 0);
                tableLayoutPanel.SetColumnSpan(lblName, 2);
            }
            else
            {
                lblName = new Label
                {
                    Text = "Название:",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    ForeColor = Color.FromArgb(33, 37, 41),
                    AccessibleName = "Название производителя",
                    AccessibleDescription = "Метка для поля ввода названия производителя"
                };
                tableLayoutPanel.Controls.Add(lblName, 0, 0);

                txtName = new TextBox
                {
                    Size = new Size(200, 27),
                    BorderStyle = BorderStyle.FixedSingle,
                    AccessibleName = "Поле названия производителя",
                    AccessibleDescription = "Введите название производителя"
                };
                tableLayoutPanel.Controls.Add(txtName, 1, 0);
            }

            btnSave = new Button
            {
                Text = _isDeleteMode ? "Удалить" : "Сохранить",
                Size = new Size(100, 28),
                BackColor = _isDeleteMode ? Color.FromArgb(220, 53, 69) : Color.FromArgb(25, 118, 210),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = _isDeleteMode ? Color.FromArgb(255, 100, 100) : Color.FromArgb(50, 140, 230) },
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AccessibleName = _isDeleteMode ? "Удалить производителя" : "Сохранить производителя",
                AccessibleDescription = _isDeleteMode ? "Подтверждает удаление производителя" : "Сохраняет данные производителя"
            };
            btnSave.Click += BtnSave_Click;
            tableLayoutPanel.Controls.Add(btnSave, 1, _isDeleteMode ? 1 : 2);

            btnCancel = new Button
            {
                Text = "Отмена",
                Size = new Size(100, 28),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(130, 140, 150) },
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AccessibleName = "Отменить",
                AccessibleDescription = "Закрывает окно без сохранения изменений"
            };
            btnCancel.Click += (s, e) => Close();
            tableLayoutPanel.Controls.Add(btnCancel, 0, _isDeleteMode ? 1 : 2);

            Controls.Add(tableLayoutPanel);

            AddToolTips();
        }

        private void AddToolTips()
        {
            var toolTip = new ToolTip();
            if (!_isDeleteMode)
            {
                toolTip.SetToolTip(txtName, "Введите название производителя");
            }
            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление производителя" : "Сохранить данные производителя");
            toolTip.SetToolTip(btnCancel, "Закрыть без сохранения изменений");
        }

        private void InitializeData()
        {
            if (_isDeleteMode || !_manufacturerId.HasValue)
                return;

            try
            {
                var manufacturer = _viewModel.LoadManufacturers().Find(m => m.ManufacturerID == _manufacturerId);
                if (manufacturer == null)
                    throw new Exception("Производитель не найден.");
                txtName.Text = manufacturer.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isDeleteMode)
                {
                    _viewModel.DeleteManufacturer(_manufacturerId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                var manufacturer = new Manufacturer
                {
                    ManufacturerID = _manufacturerId ?? 0,
                    Name = txtName.Text.Trim()
                };

                if (_manufacturerId.HasValue)
                    _viewModel.UpdateManufacturer(manufacturer);
                else
                    _viewModel.AddManufacturer(manufacturer);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
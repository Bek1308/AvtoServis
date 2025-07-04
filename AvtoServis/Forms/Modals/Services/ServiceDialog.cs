using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System.Text.RegularExpressions;

namespace AvtoServis.Forms.Controls
{
    public partial class ServiceDialog : Form
    {
        private readonly ServicesViewModel _viewModel;
        private readonly int? _serviceId;
        private readonly bool _isDeleteMode;
        private Service _service;
        private System.Windows.Forms.Timer _errorTimer;

        public ServiceDialog(ServicesViewModel viewModel, int? serviceId, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _serviceId = serviceId;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            panelError.Visible = false;
            if (!_isDeleteMode)
            {
                LoadService();
            }
            else
            {
                SetupDeleteModeUI();
            }
        }

        private void LoadService()
        {
            try
            {
                _service = _serviceId == null ? new Service() : _viewModel.LoadService(_serviceId.Value);
                if (_service == null && _serviceId != null)
                {
                    ShowError("Услуга не найдена.");
                    return;
                }

                if (_serviceId != null)
                {
                    txtName.Text = _service.Name;
                    txtPrice.Text = _service.Price.ToString();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке услуги: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadService Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void SetupDeleteModeUI()
        {
            this.ClientSize = new Size(434, 142);
            this.Text = "Подтверждение удаления";

            lblMessage = new Label
            {
                AutoSize = true,
                Text = "Вы хотите удалить эту услугу?",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Name = "lblMessage",
                Anchor = AnchorStyles.Left,
                AccessibleName = "Сообщение об удалении",
                AccessibleDescription = "Подтверждение удаления услуги"
            };

            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            tableLayoutPanel.Controls.Add(lblMessage, 0, 0);
            tableLayoutPanel.SetColumnSpan(lblMessage, 2);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 1);
            tableLayoutPanel.Controls.Add(btnSave, 1, 1);

            btnCancel.Text = "Нет";
            btnSave.Text = "Да";
            btnCancel.BackColor = Color.FromArgb(25, 118, 210);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 140, 230);
            btnSave.BackColor = Color.FromArgb(220, 53, 69);
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 100, 100);
        }

        private void ValidateInputs()
        {
            if (_isDeleteMode) return;

            lblError.Visible = false;
            panelError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Пожалуйста, введите название услуги.");
                return;
            }

            if (txtName.Text.Length > 100)
            {
                ShowError("Название услуги не должно превышать 100 символов.");
                return;
            }

            if (!Regex.IsMatch(txtName.Text, @"^[a-zA-Z0-9\s]+$"))
            {
                ShowError("Название услуги должно содержать только латинские буквы, цифры и пробелы.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text) || !decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                ShowError("Пожалуйста, введите корректную цену (положительное число).");
                return;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInputs();
                if (lblError.Visible) return;

                if (_isDeleteMode)
                {
                    _viewModel.DeleteService(_serviceId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                _service.Name = txtName.Text.Trim();
                _service.Price = decimal.Parse(txtPrice.Text.Trim());

                if (_serviceId == null)
                {
                    _viewModel.AddService(_service);
                }
                else
                {
                    _service.ServiceID = _serviceId.Value;
                    _viewModel.UpdateService(_service);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при сохранении: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"BtnSave_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
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

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && txtName.Text.Length > 100)
            {
                ShowError("Название услуги не должно превышать 100 символов.");
            }
            else if (!string.IsNullOrWhiteSpace(txtName.Text) && !Regex.IsMatch(txtName.Text, @"^[a-zA-Z0-9\s]+$"))
            {
                ShowError("Название услуги должно содержать только латинские буквы, цифры и пробелы.");
            }
        }

        private void TxtPrice_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPrice.Text) && (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0))
            {
                ShowError("Пожалуйста, введите корректную цену (положительное число).");
            }
        }


    }
}
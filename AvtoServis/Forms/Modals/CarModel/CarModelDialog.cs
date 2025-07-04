using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System.Text.RegularExpressions;

namespace AvtoServis.Forms.Controls
{
    public partial class CarModelDialog : Form
    {
        private readonly CarModelViewModel _viewModel;
        private readonly int? _modelId;
        private readonly bool _isDeleteMode;
        private readonly bool _isViewOnly;
        private CarModel _model;
        private System.Windows.Forms.Timer _errorTimer;

        public CarModelDialog(CarModelViewModel viewModel, int? modelId, bool isViewOnly = false, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _modelId = modelId;
            _isDeleteMode = isDeleteMode;
            _isViewOnly = isViewOnly;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            SetToolTips();
            if (!_isDeleteMode)
            {
                LoadModel();
            }
            else
            {
                SetupDeleteModeUI();
            }
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Форма для добавления или редактирования модели автомобиля");
            toolTip.SetToolTip(lblBrand, "Метка для выбора марки автомобиля");
            toolTip.SetToolTip(cmbBrand, _isDeleteMode ? "Марка автомобиля" : "Выберите марку автомобиля");
            toolTip.SetToolTip(lblModel, "Метка для ввода названия модели");
            toolTip.SetToolTip(txtModel, "Введите название модели (латинские буквы, цифры, пробел, максимум 100 символов)");
            toolTip.SetToolTip(lblYear, "Метка для ввода года выпуска");
            toolTip.SetToolTip(txtYear, "Введите год выпуска (например, 2023)");
            toolTip.SetToolTip(lblError, "Отображает ошибки или сообщения об операции");
            toolTip.SetToolTip(btnCancel, _isDeleteMode ? "Отменить удаление" : "Закрыть без сохранения");
            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление" : "Сохранить изменения");
            if (lblMessage != null)
                toolTip.SetToolTip(lblMessage, "Подтверждение удаления модели");
        }

        private void LoadModel()
        {
            try
            {
                _model = _modelId == null ? new CarModel() : _viewModel.LoadModels().Find(m => m.Id == _modelId);
                if (_model == null && _modelId != null)
                {
                    ShowError("Модель не найдена.");
                    btnSave.Enabled = false;
                    return;
                }

                var brands = _viewModel.LoadBrands();
                cmbBrand.DataSource = brands;
                cmbBrand.DisplayMember = "CarBrandName";
                cmbBrand.ValueMember = "Id";
                if (_modelId != null)
                {
                    cmbBrand.SelectedValue = _model.CarBrandId;
                    txtModel.Text = _model.Model;
                    txtYear.Text = _model.Year.ToString();
                }
                else
                {
                    txtYear.Text = DateTime.Now.Year.ToString();
                }

                if (_isViewOnly)
                {
                    cmbBrand.Enabled = false;
                    txtModel.Enabled = false;
                    txtYear.Enabled = false;
                    btnSave.Visible = false;
                    btnCancel.Text = "Закрыть";
                    Text = "Просмотр модели";
                }

                ValidateInputs();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке модели: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadModel Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void SetupDeleteModeUI()
        {
            this.ClientSize = new Size(434, 142);
            this.Text = "Подтверждение удаления";

            lblMessage = new Label
            {
                AutoSize = true,
                Text = "Вы хотите удалить эту модель?",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Name = "lblMessage",
                Anchor = AnchorStyles.Left,
                AccessibleName = "Сообщение об удалении",
                AccessibleDescription = "Подтверждение удаления модели"
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
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 77, 77);
        }

        private void ValidateInputs()
        {
            if (_isDeleteMode || _isViewOnly) return;

            lblError.Visible = false;
            btnSave.Enabled = true;

            if (cmbBrand.SelectedValue == null)
            {
                ShowError("Пожалуйста, выберите марку автомобиля.");
                btnSave.Enabled = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                ShowError("Пожалуйста, введите название модели.");
                btnSave.Enabled = false;
                return;
            }

            if (txtModel.Text.Length > 100)
            {
                ShowError("Название модели не должно превышать 100 символов.");
                btnSave.Enabled = false;
                return;
            }

            if (!Regex.IsMatch(txtModel.Text, @"^[a-zA-Z0-9\s]+$"))
            {
                ShowError("Название модели должно содержать только латинские буквы, цифры и пробелы.");
                btnSave.Enabled = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtYear.Text))
            {
                ShowError("Пожалуйста, введите год выпуска.");
                btnSave.Enabled = false;
                return;
            }

            if (!int.TryParse(txtYear.Text, out int year) || year < 1900 || year > DateTime.Now.Year + 1)
            {
                ShowError($"Год должен быть числом между 1900 и {DateTime.Now.Year + 1}.");
                btnSave.Enabled = false;
                return;
            }
        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isDeleteMode)
                {
                    _viewModel.DeleteModel(_modelId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                if (_isViewOnly)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                _model.CarBrandId = (int)cmbBrand.SelectedValue;
                _model.Model = txtModel.Text.Trim();
                _model.Year = int.Parse(txtYear.Text);

                if (_modelId == null)
                {
                    _viewModel.AddModel(_model);
                }
                else
                {
                    _viewModel.UpdateModel(_model);
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

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            _errorTimer.Stop();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            _errorTimer.Stop();
            _errorTimer.Start();
        }
    }
}
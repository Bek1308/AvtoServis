using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class CarModelDialog : Form
    {
        private readonly CarModelsViewModel _viewModel;
        private readonly int? _modelId;
        private readonly bool _isDeleteMode;
        private CarModel _model;
        private Label lblMessage;

        public CarModelDialog(CarModelsViewModel viewModel, int? modelId, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _modelId = modelId;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            if (!_isDeleteMode)
            {
                LoadModel();
            }
            else
            {
                SetupDeleteModeUI();
            }
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
            this.ClientSize = new System.Drawing.Size(434, 142);
            this.Text = "Подтверждение удаления";

            // Dinamik UI elementlari
            lblMessage = new Label
            {
                AutoSize = true,
                Text = "Вы хотите удалить эту модель?",
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(33, 37, 41),
                Name = "lblMessage",
                Anchor = AnchorStyles.Left,
                AccessibleName = "Сообщение об удалении",
                AccessibleDescription = "Подтверждение удаления модели"
            };

            // TableLayoutPanel'ni tozalash va yangi elementlarni qo'shish
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            tableLayoutPanel.Controls.Add(lblMessage, 0, 0);
            tableLayoutPanel.SetColumnSpan(lblMessage, 2);
            tableLayoutPanel.Controls.Add(btnCancel, 0, 1);
            tableLayoutPanel.Controls.Add(btnSave, 1, 1);

            // Tugmalarni sozlash
            btnCancel.Text = "Нет";
            btnSave.Text = "Да";
            btnCancel.BackColor = System.Drawing.Color.FromArgb(25, 118, 210);
            btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(50, 140, 230);
            btnSave.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 100, 100);
        }

        private void ValidateInputs()
        {
            if (_isDeleteMode) return;

            lblError.Visible = false;
            btnSave.Enabled = true;

            if (chkAddBrand.Checked)
            {
                if (string.IsNullOrWhiteSpace(cmbBrand.Text))
                {
                    ShowError("Пожалуйста, введите название марки.");
                    btnSave.Enabled = false;
                    return;
                }
                if (cmbBrand.Text.Length > 100)
                {
                    ShowError("Название марки не должно превышать 100 символов.");
                    btnSave.Enabled = false;
                    return;
                }
                if (!Regex.IsMatch(cmbBrand.Text, @"^[a-zA-Z0-9\s]+$"))
                {
                    ShowError("Название марки должно содержать только латинские буквы, цифры и пробелы.");
                    btnSave.Enabled = false;
                    return;
                }
            }
            else if (cmbBrand.SelectedValue == null)
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
                ShowError($"Год не должен быть больше {DateTime.Now.Year + 1} или меньше 1900.");
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
                    _viewModel.DeleteModel(_modelId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                int carBrandId;
                if (chkAddBrand.Checked)
                {
                    var newBrand = new CarBrand { CarBrandName = cmbBrand.Text.Trim() };
                    _viewModel.AddBrand(newBrand);
                    carBrandId = newBrand.Id;
                }
                else
                {
                    carBrandId = (int)cmbBrand.SelectedValue;
                }

                _model.CarBrandId = carBrandId;
                _model.Model = txtModel.Text.Trim();
                _model.Year = int.Parse(txtYear.Text);

                if (_modelId == null)
                {
                    _viewModel.AddModel(_model);
                }
                else
                {
                    _model.Id = _modelId.Value;
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

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void chkAddBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (_isDeleteMode) return;
            cmbBrand.DropDownStyle = chkAddBrand.Checked ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
            if (chkAddBrand.Checked) cmbBrand.Text = "";
            ValidateInputs();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
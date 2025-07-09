using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System.Text.RegularExpressions;

namespace AvtoServis.Forms.Controls
{
    public partial class StatusesDialog : Form
    {
        private readonly StatusesViewModel _viewModel;
        private readonly int? _statusId;
        private readonly bool _isDeleteMode;
        private readonly bool _isViewOnly;
        private Status _status;
        private System.Windows.Forms.Timer _errorTimer;

        public StatusesDialog(StatusesViewModel viewModel, int? statusId, bool isViewOnly = false, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _statusId = statusId;
            _isDeleteMode = isDeleteMode;
            _isViewOnly = isViewOnly;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            SetToolTips();
            if (!_isDeleteMode)
            {
                LoadStatus();
            }
            else
            {
                SetupDeleteModeUI();
            }
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(tableLayoutPanel, "Форма для добавления или редактирования статуса");
            toolTip.SetToolTip(lblName, "Метка для ввода названия статуса");
            toolTip.SetToolTip(txtName, _isDeleteMode ? "Название статуса" : "Введите название статуса (латинские буквы, цифры, пробел, максимум 100 символов)");
            toolTip.SetToolTip(lblDescription, "Метка для ввода описания статуса");
            toolTip.SetToolTip(txtDescription, _isDeleteMode ? "Описание статуса" : "Введите описание статуса (максимум 500 символов, необязательно)");
            toolTip.SetToolTip(btnColor, "Выберите цвет статуса");
            toolTip.SetToolTip(lblError, "Отображает ошибки или сообщения об операции");
            toolTip.SetToolTip(btnCancel, _isDeleteMode ? "Отменить удаление" : "Закрыть без сохранения");
            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление" : "Сохранить изменения");
            if (lblMessage != null)
                toolTip.SetToolTip(lblMessage, "Подтверждение удаления статуса");
        }

        private void LoadStatus()
        {
            try
            {
                _status = _statusId == null ? new Status() : _viewModel.LoadStatuses().Find(s => s.StatusID == _statusId);
                if (_status == null && _statusId != null)
                {
                    ShowError("Статус не найден.");
                    btnSave.Enabled = false;
                    return;
                }

                txtName.Text = _status?.Name;
                txtDescription.Text = _status?.Description;
                btnColor.BackColor = string.IsNullOrEmpty(_status?.Color) ? Color.White : ColorTranslator.FromHtml(_status.Color);

                if (_isViewOnly)
                {
                    txtName.Enabled = false;
                    txtDescription.Enabled = false;
                    btnColor.Enabled = false;
                    btnSave.Visible = false;
                    btnCancel.Text = "Закрыть";
                    Text = "Просмотр статуса";
                }

                ValidateInputs();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке статуса: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadStatus Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void SetupDeleteModeUI()
        {
            this.ClientSize = new Size(434, 142);
            this.Text = "Подтверждение удаления";

            lblMessage = new Label
            {
                AutoSize = true,
                Text = "Вы хотите удалить этот статус?",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Name = "lblMessage",
                Anchor = AnchorStyles.Left,
                AccessibleName = "Сообщение об удалении",
                AccessibleDescription = "Подтверждение удаления статуса"
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

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Пожалуйста, введите название статуса.");
                btnSave.Enabled = false;
                return;
            }

            if (txtName.Text.Length > 100)
            {
                ShowError("Название статуса не должно превышать 100 символов.");
                btnSave.Enabled = false;
                return;
            }

            if (!Regex.IsMatch(txtName.Text, @"^[a-zA-Z0-9\s]+$"))
            {
                ShowError("Название статуса должно содержать только латинские буквы, цифры и пробелы.");
                btnSave.Enabled = false;
                return;
            }

            if (!string.IsNullOrEmpty(txtDescription.Text) && txtDescription.Text.Length > 500)
            {
                ShowError("Описание статуса не должно превышать 500 символов.");
                btnSave.Enabled = false;
                return;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            ValidateInputs();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    btnColor.BackColor = colorDialog.Color;
                    _status.Color = ColorTranslator.ToHtml(colorDialog.Color);
                }
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
                    _viewModel.DeleteStatus(_statusId.Value);
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

                _status.Name = txtName.Text.Trim();
                _status.Description = txtDescription.Text.Trim();
                _status.Color = ColorTranslator.ToHtml(btnColor.BackColor);

                if (_statusId == null)
                {
                    _viewModel.AddStatus(_status);
                }
                else
                {
                    _viewModel.UpdateStatus(_status);
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
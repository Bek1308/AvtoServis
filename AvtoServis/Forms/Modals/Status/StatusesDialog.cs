using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using Timer = System.Windows.Forms.Timer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class StatusesDialog : Form
    {
        private readonly StatusesViewModel _viewModel;
        private readonly string _tableName;
        private readonly int? _statusId;
        private readonly bool _isDeleteMode;
        private Status _status;
        private Label lblMessage;
        private System.Windows.Forms.Timer _errorTimer;

        public StatusesDialog(StatusesViewModel viewModel, string tableName, int? statusId, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            _statusId = statusId;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            _errorTimer = new Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            btnSave.Enabled = true;
            Text = _viewModel.GetTableDisplayNames()[_tableName];
            if (!isDeleteMode)
            {
                LoadStatus();
            }
            else
            {
                SetupDeleteModeUI();
            }
        }

        private void LoadStatus()
        {
            try
            {
                _status = _statusId == null ? new Status() : _viewModel.LoadStatuses(_tableName).Find(s => s.StatusID == _statusId);
                if (_status == null && _statusId != null)
                {
                    ShowError("Статус не найден.");
                    return;
                }

                if (_statusId != null)
                {
                    txtName.Text = _status.Name;
                    txtDescription.Text = _status.Description;
                    if (!string.IsNullOrEmpty(_status.Color))
                    {
                        try
                        {
                            btnColor.BackColor = ColorTranslator.FromHtml(_status.Color);
                        }
                        catch
                        {
                            btnColor.BackColor = Color.White;
                        }
                    }
                }
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
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 100, 100);
        }

        private void ValidateInputs()
        {
            if (_isDeleteMode) return;

            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Пожалуйста, введите название статуса.");
                return;
            }

            if (txtName.Text.Length > 100)
            {
                ShowError("Название статуса не должно превышать 100 символов.");
                return;
            }

            if (txtDescription.Text.Length > 255)
            {
                ShowError("Описание не должно превышать 255 символов.");
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
                    _viewModel.DeleteStatus(_tableName, _statusId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                _status.Name = txtName.Text.Trim();
                _status.Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim();
                _status.Color = ColorTranslator.ToHtml(btnColor.BackColor);

                if (_statusId == null)
                {
                    _viewModel.AddStatus(_tableName, _status);
                }
                else
                {
                    _status.StatusID = _statusId.Value;
                    _viewModel.UpdateStatus(_tableName, _status);
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
                ShowError("Название статуса не должно превышать 100 символов.");
            }
        }

        private void TxtDescription_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDescription.Text) && txtDescription.Text.Length > 255)
            {
                ShowError("Описание не должно превышать 255 символов.");
            }
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    btnColor.BackColor = colorDialog.Color;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _errorTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
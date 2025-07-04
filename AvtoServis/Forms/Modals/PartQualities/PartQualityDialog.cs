using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System.Text.RegularExpressions;

namespace AvtoServis.Forms.Modals.PartQualities
{
    public partial class PartQualityDialog : Form
    {
        private readonly PartQualitiesViewModel _viewModel;
        private readonly int? _qualityId;
        private readonly bool _isDeleteMode;
        private PartQuality _quality;
        private System.Windows.Forms.Timer _errorTimer;

        public PartQualityDialog(PartQualitiesViewModel viewModel, int? qualityId, bool isDeleteMode = false)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _qualityId = qualityId;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            _errorTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _errorTimer.Tick += ErrorTimer_Tick;
            lblError.Visible = false;
            panelError.Visible = false;
            if (!_isDeleteMode)
            {
                LoadPartQuality();
            }
            else
            {
                SetupDeleteModeUI();
            }
            SetToolTips();
        }

        private void SetToolTips()
        {
            if (!_isDeleteMode)
            {
                toolTip.SetToolTip(txtName, "Введите название качества запчасти");
            }
            toolTip.SetToolTip(btnSave, _isDeleteMode ? "Подтвердить удаление качества запчасти" : "Сохранить данные качества запчасти");
            toolTip.SetToolTip(btnCancel, "Закрыть без сохранения изменений");
        }

        private void LoadPartQuality()
        {
            try
            {
                _quality = _qualityId == null ? new PartQuality() : _viewModel.LoadPartQuality(_qualityId.Value);
                if (_quality == null && _qualityId != null)
                {
                    ShowError("Качество запчасти не найдено.");
                    return;
                }

                if (_qualityId != null)
                {
                    txtName.Text = _quality.Name;
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке качества запчасти: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadPartQuality Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void SetupDeleteModeUI()
        {
            this.ClientSize = new Size(400, 142);
            this.Text = "Подтверждение удаления";

            lblName.Text = "Вы хотите удалить это качество запчасти?";
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            tableLayoutPanel.Controls.Add(lblName, 0, 0);
            tableLayoutPanel.SetColumnSpan(lblName, 2);
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
                ShowError("Пожалуйста, введите название качества запчасти.");
                return;
            }

            if (txtName.Text.Length > 100)
            {
                ShowError("Название качества запчасти не должно превышать 100 символов.");
                return;
            }

            if (!Regex.IsMatch(txtName.Text, @"^[а-яА-Яa-zA-Z0-9\s]+$"))
            {
                ShowError("Название качества должно содержать только буквы (русские или латинские), цифры и пробелы.");
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
                    _viewModel.DeletePartQuality(_qualityId.Value);
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                _quality.Name = txtName.Text.Trim();

                if (_qualityId == null)
                {
                    _viewModel.AddPartQuality(_quality);
                }
                else
                {
                    _quality.QualityID = _qualityId.Value;
                    _viewModel.UpdatePartQuality(_quality);
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
            if (string.IsNullOrWhiteSpace(txtName.Text))
                return;
            if (txtName.Text.Length > 100)
            {
                ShowError("Название качества запчасти не должно превышать 100 символов.");
            }
            else if (!Regex.IsMatch(txtName.Text, @"^[а-яА-Яa-zA-Z0-9\s]+$"))
            {
                ShowError("Название качества запчасти должно содержать только буквы (русские или латинские), цифры и пробелы.");
            }
            else
            {
                lblError.Visible = false;
                panelError.Visible = false;
                _errorTimer.Stop();
            }
        }
    }
}
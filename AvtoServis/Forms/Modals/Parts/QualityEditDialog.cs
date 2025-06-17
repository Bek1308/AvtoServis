using AvtoServis.ViewModels.Screens;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class QualityEditDialog : Form
    {
        private readonly PartsViewModel _viewModel;
        private readonly int? _qualityId;
        private AvtoServis.Model.Entities.PartQuality _quality;

        public QualityEditDialog(PartsViewModel viewModel, int? qualityId = null)
        {
            InitializeComponent();
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            _qualityId = qualityId;
            if (_qualityId.HasValue)
            {
                LoadQuality();
                Text = "Редактировать качество";
            }
            else
            {
                _quality = new AvtoServis.Model.Entities.PartQuality();
                Text = "Добавить качество";
            }
        }

        private void LoadQuality()
        {
            try
            {
                _quality = _viewModel.GetQualityById(_qualityId.Value);
                txtQualityName.Text = _quality.Name;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке качества: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadQuality Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtQualityName.Text))
                {
                    ShowError("Введите название качества");
                    return;
                }

                _quality.Name = txtQualityName.Text.Trim();

                if (_qualityId == null)
                {
                    _viewModel.AddQuality(_quality);
                }
                else
                {
                    _quality.QualityID = _qualityId.Value;
                    _viewModel.UpdateQuality(_quality);
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
            Close();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            timerError.Start();
        }

        private void TimerError_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            timerError.Stop();
        }
    }
}
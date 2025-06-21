using AvtoServis.Model.Entities;
using AvtoServis.ViewModels.Screens;
using System;
using System.Windows.Forms;

namespace AvtoServis.Forms.Controls
{
    public partial class SuppliersFilterDialog : Form
    {
        private readonly SuppliersViewModel _viewModel;
        public string Name { get; private set; }

        public SuppliersFilterDialog(SuppliersViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            InitializeComponent();
            LoadFilters();
        }

        private void LoadFilters()
        {
            try
            {
                txtName.Text = "";
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке фильтров: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadFilters Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                Name = string.IsNullOrWhiteSpace(txtName.Text) ? null : txtName.Text.Trim();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при применении фильтров: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"BtnApply_Click Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
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
        }
    }
}
using System;
using System.Drawing; // Color uchun
using System.Windows.Forms;
using AvtoService.ViewModels.Screens;
using AvtoServis.Forms.Screens;
using Timer = System.Windows.Forms.Timer;

namespace AvtoServis.Forms.Screens
{
    public partial class SignInForm : Form
    {
        private readonly SignInViewModel _viewModel;
        private readonly Timer _errorTimer; // Xabar 5 soniyada yo‘qolishi uchun timer

        public SignInForm()
        {
            InitializeComponent();
            _viewModel = new SignInViewModel();
            _errorTimer = new Timer { Interval = 5000 }; // 5 soniya (5000 ms)
            _errorTimer.Tick += (s, e) =>
            {
                lblError.Text = string.Empty; // 5 soniyadan so‘ng xabarni o‘chirish
                _errorTimer.Stop();
            };
            BindControls();
            _viewModel.OnLoginSuccess += OpenMainForm;
        }

        private void BindControls()
        {
            BindingSource bindingSource = new BindingSource { DataSource = _viewModel };
            txtLogin.DataBindings.Add("Text", bindingSource, "Username", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPasword.DataBindings.Add("Text", bindingSource, "Password", true, DataSourceUpdateMode.OnPropertyChanged);
            lblError.DataBindings.Add("Text", bindingSource, "ErrorMessage");

            // Xabar rangini qizil qilish uchun PropertyChanged hodisasini tinglash
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_viewModel.ErrorMessage))
                {
                    lblError.ForeColor = Color.Red; // Qizil rang
                    if (!string.IsNullOrEmpty(_viewModel.ErrorMessage))
                    {
                        _errorTimer.Start(); // Timer ishga tushadi
                    }
                }
                if (e.PropertyName == nameof(_viewModel.Username) || e.PropertyName == nameof(_viewModel.Password))
                {
                    btnSignIn.Enabled = _viewModel.SignInCommand.CanExecute(null);
                }
            };

            btnSignIn.Click += (s, e) => _viewModel.SignInCommand.Execute(null);
            btnSignIn.Enabled = _viewModel.SignInCommand.CanExecute(null);
        }

        private void OpenMainForm()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SignInForm_Load(object sender, EventArgs e)
        {
        }
    }
}
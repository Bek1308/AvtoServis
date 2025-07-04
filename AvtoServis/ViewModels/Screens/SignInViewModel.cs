using AvtoServis.Services.Core;
using EleCho.MvvmToolkit.ComponentModel;
using EleCho.MvvmToolkit.Input;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AvtoService.ViewModels.Screens
{
    public partial class SignInViewModel : ObservableObject
    {
        private readonly AuthenticationService _authService;

        public SignInViewModel()
        {
            _authService = new AuthenticationService();
            SignInCommand = new RelayCommand(ExecuteSignIn, CanExecuteSignIn);

            // Initialize non-nullable fields to default values
            Username = string.Empty;
            Password = string.Empty;
            ErrorMessage = string.Empty;
            OnLoginSuccess = () => { }; // Assign a default no-op delegate
        }

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        public event Action OnLoginSuccess;

        public IRelayCommand SignInCommand { get; }

        private void ExecuteSignIn()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Username))
                {
                    ErrorMessage = "Пожалуйста, введите имя пользователя.";
                    return;
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Пожалуйста, введите пароль.";
                    return;
                }

                var user = _authService.Authenticate(Username, Password);
                if (user != null)
                {
                    CurrentUser.Instance.SetUser(user);
                    ErrorMessage = "Успешный вход!";
                    OnLoginSuccess?.Invoke();
                }
                else
                {
                    var existingUser = _authService.GetUserByUsername(Username);
                    ErrorMessage = existingUser == null
                        ? "Неверное имя пользователя."
                        : "Неверный пароль.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Произошла ошибка: {ex.Message}";
            }
        }

        private bool CanExecuteSignIn()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}

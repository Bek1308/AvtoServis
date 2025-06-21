using AvtoServis.Services.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace AvtoService.ViewModels.Screens
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly AuthenticationService _authService;
        private string _username;
        private string _password;
        private string _errorMessage;

        public event Action OnLoginSuccess;

        public SignInViewModel()
        {
            _authService = new AuthenticationService();
            SignInCommand = new RelayCommand(ExecuteSignIn, CanExecuteSignIn);
        }

        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }

        public RelayCommand SignInCommand { get; }

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
                    // Foydalanuvchi ma'lumotlarini CurrentUser'ga saqlash
                    CurrentUser.Instance.SetUser(user);
                    ErrorMessage = "Успешный вход!";
                    OnLoginSuccess?.Invoke();
                }
                else
                {
                    var existingUser = _authService.GetUserByUsername(Username);
                    if (existingUser == null)
                    {
                        ErrorMessage = "Неверное имя пользователя.";
                    }
                    else
                    {
                        ErrorMessage = "Неверный пароль.";
                    }
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
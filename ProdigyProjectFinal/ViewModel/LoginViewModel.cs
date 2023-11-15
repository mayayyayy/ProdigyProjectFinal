
using ProdigyProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ProdigyProjectFinal.View;

namespace ProdigyProjectFinal.ViewModel
{
    public class LoginViewModel : ViewModel
    {
        private string _username;
        private string _password;
        private bool _isLoginError;
        private string _errorMessage;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChange(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChange(nameof(Password));
            }
        }
        public bool IsLoginError
        {
            get => _isLoginError;
            set
            {
                _isLoginError = value;
                OnPropertyChange(nameof(IsLoginError));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChange(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; protected set; }

        public LoginViewModel()
        {
            Username = "";
            Password = "";
            IsLoginError = true;
            ErrorMessage = "Incorrect username or password";

            LoginCommand = new Command(async () =>
            {
                if (!validateUser(Username, Password))
                    return;

                var service = new Services.ProdigyServices();
                try
                {
                    UserDto user = await service.LogInAsync(Username, Password);
                    if (user is null)
                    {
                        IsLoginError = true;
                    }
                    else
                    {
                        IsLoginError = false;

                        await SecureStorage.SetAsync("CurrentUser", JsonSerializer.Serialize(user));
                        await Shell.Current.DisplayAlert("logged in message", "Logged in!", "OK");
                        await Shell.Current.GoToAsync("MainPage");
                    }
                }
                catch (Exception)
                {
                    ErrorMessage = "A server error occurred";
                    IsLoginError = true;
                }

            });
        }

        private bool validateUser(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && username.Length > 3 && password.Length > 3;
        }
    }
}

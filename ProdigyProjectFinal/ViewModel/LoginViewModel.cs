
using ProdigyProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ProdigyProjectFinal.View;
using ProdigyProjectFinal.Services;

namespace ProdigyProjectFinal.ViewModel
{
    public class LoginViewModel : ViewModel
    {
        private string _username;
        private string _password;
        private bool _isLoginError;
        private string _errorMessage;
        private ProdigyServices _service;
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

        public ICommand BtnCommand { get; protected set; }

        public ICommand BackBtn;  
        public LoginViewModel(ProdigyServices service)
        {
            Username = "";
            Password = "";
            IsLoginError = true;
            ErrorMessage = "incorrect email or password";
            this._service = service;

            //login button command
            BtnCommand = new Command(async () =>
            {

                
                if (!LoginViewModel.validateUser(Username, Password))
                {

                    await Shell.Current.DisplayAlert("error", "invalid email or password", "try again");
                    return;
                }
                    
                try
                {
                    UserDto userDto = await service.LogInAsync(Username, Password);
                    if (!userDto.Success)
                    {
                        IsLoginError = true;
                        await Shell.Current.DisplayAlert("error", "no such user", "try again");

                    }
                    else
                    {
                        IsLoginError = false;

                        await SecureStorage.SetAsync("CurrentUser", JsonSerializer.Serialize(userDto.User));
                        await Shell.Current.DisplayAlert("logged in message", "Logged in!", "OK");
                        await Shell.Current.GoToAsync("//Home");
                    }
                }
                catch (Exception)
                {
                    ErrorMessage = "a server error occurred";
                    IsLoginError = true;
                }
                    
            });
        }

        private static bool validateUser(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && username.Length > 0 && password.Length > 0;
        }
    }
}

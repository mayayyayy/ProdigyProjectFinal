using ProdigyProjectFinal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ProdigyProjectFinal.Models;
//using Android.App;
using System.Net;

namespace ProdigyProjectFinal.ViewModel
{
    public class SignUpViewModel : ViewModel
    {

        private string _username;
        private string _password;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _errorMessage;
        private bool _isErrorMessage;
        private bool _signUpInvalid;
        private readonly ProdigyServices _services;
        private readonly UserService _userService;




        #region get and set for fields

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
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChange(nameof(Email));
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChange(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChange(nameof(LastName));
            }
        }
        public bool IsErrorMessage
        {
            get => _isErrorMessage;
            set
            {
                _isErrorMessage = value;
                OnPropertyChange(nameof(IsErrorMessage));
            }
        }
        public bool IsInvalid
        {
            get => _signUpInvalid;
            set
            {
                _signUpInvalid = value;
                OnPropertyChange(nameof(IsInvalid));
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
        #endregion

        public ICommand SignUpCommand { get; protected set; }
        public ICommand BackBtn { get; protected set; }

        public SignUpViewModel(ProdigyServices services, UserService userService)
        {
            Username = "";
            Password = "";
            Email = "";
            FirstName = "";
            LastName = "";
            IsInvalid = true;
            ErrorMessage = "invalid";
            this._services = services;
            this._userService = userService;

            //sign up button command
            SignUpCommand = new Command(async () =>
            {
             
                if (!validateRegister())
                {
                    await Shell.Current.DisplayAlert("error", "invalid field", "try again");
                    _isErrorMessage = true;
                    return;
                }

                User user = new User() { Username = Username, UserPswd = Password, FirstName = FirstName, LastName = LastName, Email = Email};

                try
                {
                    user = await _services.Register(user);
                    if(user != null)
                    {
                        _isErrorMessage = false;
                        _userService.User = user;
                        await Shell.Current.DisplayAlert("success", "sign up success", "ok");
                        await Shell.Current.GoToAsync("//Home");

                    }
                    else _isErrorMessage = true;
                }
                catch(Exception)
                {
                    _isErrorMessage=true;
                    await Shell.Current.DisplayAlert("error", "server error", "try again");
                }
            });

            BackBtn = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });
        }

        private bool validateRegister()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Email) &&
                !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && Email.Contains('@')
                && Username.Length > 0 && Password.Length > 0;
        }
    }
}

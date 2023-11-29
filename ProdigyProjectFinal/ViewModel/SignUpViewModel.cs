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


        public SignUpViewModel()
        {
            Username = "";
            Password = "";
            Email = "";
            FirstName = "";
            LastName = "";
            IsInvalid = true;
            ErrorMessage = "invalid";


            SignUpCommand = new Command(async () =>
            {
             
                if (!SignUpViewModel.validateRegister(Username, Password, Email,FirstName,LastName))
                {
                    await Shell.Current.DisplayAlert("error", "invalid field", "try again");
                    _isErrorMessage = true;
                    return;
                }

                User user = new User() { Username = Username, FirstName = FirstName, LastName = LastName, UserPswd = Password, Email = Email};
                var service = new Services.ProdigyServices(); 
                try
                {
                    HttpStatusCode statusCode = await service.Register(user);
                    switch(statusCode)
                    {
                        case HttpStatusCode.OK:
                            _isErrorMessage = false;
                            await SecureStorage.Default.SetAsync("CurrentUser", JsonSerializer.Serialize(user));
                            await Shell.Current.DisplayAlert("success", "sign up success", "ok");
                            await Shell.Current.GoToAsync("Home");
                            break;

                        case HttpStatusCode.Conflict:
                            _isErrorMessage = true;
                            break;

                        default:
                            _isErrorMessage = true;
                            await Shell.Current.DisplayAlert("error", "server error", "try again");
                            break;
                    }
                }
                catch(Exception)
                {
                    _isErrorMessage=true;
                }
            }); 
        }

        private static bool validateRegister(string username, string password, string email, string firstName, string lastName)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email) &&
                !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && email.Contains('@')
                && username.Length > 0 && password.Length > 0;
        }
    }
}

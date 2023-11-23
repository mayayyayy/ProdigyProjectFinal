using ProdigyProjectFinal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ProdigyProjectFinal.Models;


namespace ProdigyProjectFinal.ViewModel
{
    internal class SignUpViewModel : ViewModel
    {

        private string _username;
        private string _password;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _errorMessage;
        private bool _signUpInvalid;
        //private string id;
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
                if (!SignUpViewModel.validateUser(Username, Password, Email,FirstName,LastName))
                {
                    await Shell.Current.DisplayAlert("error", "invalid field", "try again");
                    return;
                }

                

            });

           
        }
        private static bool validateUser(string username, string password, string email, string firstName, string lastName)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email) && 
                !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName)
                && username.Length > 0 && password.Length > 0;
        }
    }
}

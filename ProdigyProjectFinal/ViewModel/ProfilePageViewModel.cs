using ProdigyProjectFinal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ProdigyProjectFinal.Models;
using ProdigyProjectFinal.Services;
using Windows.Networking;

namespace ProdigyProjectFinal.ViewModel
{
    public class ProfilePageViewModel : ViewModel
    {
        private Listener listener;
        private string ProfileImageUrl { get; set; }
        private ProdigyServices _services;
        public string newUsername { get; set; }
        public ICommand ChangeUsername { get; protected set; }

        private string _username;
        private string _password;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _errorMessage;
        private bool _isErrorMessage;



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


        public ProfilePageViewModel(ProdigyServices services) //not finished 
        {
            this._services = services;

            ChangeUsername = new Command(async () =>
            {
                if (!validateUsername())
                {
                    await Shell.Current.DisplayAlert("error", "invalid username", "try again");
                    _isErrorMessage = true;
                    return;
                }



            });
        }

        private bool validateUsername()
        {
            return !string.IsNullOrEmpty(Username) && Username.Length > 0;
        }

    }
}

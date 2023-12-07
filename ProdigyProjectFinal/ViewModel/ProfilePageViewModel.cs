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
using System.Net;
//using Windows.Networking;

namespace ProdigyProjectFinal.ViewModel
{
    public class ProfilePageViewModel : ViewModel
    {
        private Listener listener;
        private string ProfileImageUrl { get; set; }
        private ProdigyServices _services;
        public ICommand ChangeUsernameBtn { get; protected set; }

        private string _NEWusername;
        private string _password;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _errorMessage;
        private bool _isErrorMessage;
        private bool _isChangeUserError;



        #region get and set for fields

        public string NewUsername
        {
            get => _NEWusername;
            set
            {
                _NEWusername = value;
                OnPropertyChange(nameof(NewUsername));
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
        public bool IsChangeUsernameError
        {
            get => _isChangeUserError;
            set
            {
                _isChangeUserError = value;
                OnPropertyChange(nameof(IsChangeUsernameError));
            }
        }
        #endregion


        public ProfilePageViewModel(ProdigyServices services) 
        {
            this._services = services;

            ChangeUsernameBtn = new Command(async () =>
            {
                User user = new User() { Username = NewUsername, FirstName = FirstName, LastName = LastName, UserPswd = Password, Email = Email };
                if (!validateUsername())
                {
                    await Shell.Current.DisplayAlert("error", "invalid entry of username", "try again");
                    _isErrorMessage = true;
                    return;
                }
                try
                {
                    UserDto userDto = await _services.ChangeUsername(NewUsername);
                    if (!userDto.Success)
                    {
                        IsChangeUsernameError = true;
                        await Shell.Current.DisplayAlert("error", "unable to change username", "try again");

                    }
                    else
                    {
                        IsChangeUsernameError = false;

                        await SecureStorage.SetAsync("CurrentUser", JsonSerializer.Serialize(userDto.User));
                        await Shell.Current.DisplayAlert("change message", "successfully changed username", "OK");
                        await Shell.Current.GoToAsync("Home");
                    }
                }
                catch (Exception)
                {
                    ErrorMessage = "a server error occurred";
                    IsChangeUsernameError = true;
                }


            });
        }

        private bool validateUsername()
        {
            return !string.IsNullOrEmpty(NewUsername) && NewUsername.Length > 0;
        }

    }
}

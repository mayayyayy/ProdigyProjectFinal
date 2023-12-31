﻿using ProdigyProjectFinal.Services;
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
        public ICommand ChangePasswordBtn { get; protected set; }
        public ICommand AddPfp { get; protected set; }
        public string ImageLocation { get => image; set { if (value != image) { image = value; OnPropertyChange(); } } }

        private string image;
        private string _message;
        private string _username;
        private string _password;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _errorMessage;
        private bool _isErrorMessage;

        private string _NEWusername;
        private string _NEWpassword;
        
        private bool _isChangeUserError;
        private bool _isChangePassError;
        public ImageSource PhotoImageSource { get; set; }


        #region get and set for fields

        public string Message { get => _message; set { _message = value; OnPropertyChange(); } }
        public string NewUsername
        {
            get => _NEWusername;
            set
            {
                _NEWusername = value;
                OnPropertyChange(nameof(NewUsername));
            }
        }
        public string NewPassword
        {
            get => _NEWpassword;
            set
            {
                _NEWpassword = value;
                OnPropertyChange(nameof(NewPassword));
            }
        }
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
        public bool IsChangeUsernameError
        {
            get => _isChangeUserError;
            set
            {
                _isChangeUserError = value;
                OnPropertyChange(nameof(IsChangeUsernameError));
            }
        }
        public bool IsChangePasswordError
        {
            get => _isChangePassError;
            set
            {
                _isChangePassError = value;
                OnPropertyChange(nameof(IsChangePasswordError));
            }
        }
        #endregion


        public ProfilePageViewModel(ProdigyServices services) 
        {
            this._services = services;
            
            #region change X

            #region change username

            ChangeUsernameBtn = new Command(async () =>
            {
                User user = new User() { Username = NewUsername, FirstName = FirstName, LastName = LastName, UserPswd = Password, Email = Email };
                if (!validateUsername())
                {
                    await Shell.Current.DisplayAlert("error", "invalid username, make sure it is longer than 1 character", "try again");
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
                        await Shell.Current.GoToAsync("//Home");
                    }
                }
                catch (Exception)
                {
                    ErrorMessage = "a server error occurred";
                    IsChangeUsernameError = true;
                }


            });
            #endregion

            #region change password

             ChangePasswordBtn = new Command(async () =>
             {
                User user = new User() { Username = Username, FirstName = FirstName, LastName = LastName, UserPswd = NewPassword, Email = Email };
                if (!validatePassword())
                {
                    await Shell.Current.DisplayAlert("error", "invalid password, make sure it is longer than 1 character", "try again");
                    _isErrorMessage = true;
                    return;
                }
                try
                {
                    UserDto userDto = await _services.ChangePassword(NewPassword);
                    
                         
                    if (!userDto.Success)
                    {
                        IsChangePasswordError = true;
                        await Shell.Current.DisplayAlert("error", "unable to change password", "try again");

                    }
                    else
                    {
                        IsChangePasswordError = false;

                        await SecureStorage.SetAsync("CurrentUser", JsonSerializer.Serialize(userDto.User));
                        await Shell.Current.DisplayAlert("change message", "successfully changed password", "OK");
                        await Shell.Current.GoToAsync("//Home");
                    }
                }
                catch (Exception)
                {
                    ErrorMessage = "a server error occurred";
                    IsChangePasswordError = true;
                }


            });
            #endregion

            #endregion

            AddPfp = new Command(async () =>
            {
                try
                {
                    FileResult photo = null;
                    if(MediaPicker.Default.IsCaptureSupported)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            photo = await MediaPicker.Default.CapturePhotoAsync();
                            await LoadPhoto(photo);
                        });
                        
                        
                    }
                }
                catch (Exception) { }   
            
            });

        }

        private async Task LoadPhoto(FileResult photo)
        {
            try
            {

                var stream = await photo.OpenReadAsync();
                PhotoImageSource = ImageSource.FromStream(() => stream);
                OnPropertyChange(nameof(PhotoImageSource));
                await Upload(photo);


            }
            catch (Exception ex) { }
        }
        private async Task Upload(FileResult file)
        {

            try
            {

                // bool success = await _gameService.UploadPhoto(file);
                bool success = await _services.UploadFile(file);
                if (success)
                {
                    var u = JsonSerializer.Deserialize<User>(await SecureStorage.Default.GetAsync("LoggedUser"));
                    ImageLocation = await _services.GetImage() + $"{u.Id}.jpg";
                }
                else
                    Shell.Current.DisplayAlert("no server connection", "fail, try again ", "OK");
            }
            catch (Exception ex) { }

        }
        private bool validateUsername()
        {
            return !string.IsNullOrEmpty(NewUsername) && NewUsername.Length > 0;
        }
        private bool validatePassword()
        {
            return !string.IsNullOrEmpty(NewPassword) && NewPassword.Length > 1;
        }


    }
}

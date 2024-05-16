using ProdigyProjectFinal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ProdigyProjectFinal.Models;
using System.Net;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;


namespace ProdigyProjectFinal.ViewModel
{
    public class ProfilePageViewModel : ViewModel
    {
        readonly PickOptions[] filePickOptions = { null,
            new() { PickerTitle = "Image", FileTypes = FilePickerFileType.Images },
            new() { PickerTitle = "Png", FileTypes = FilePickerFileType.Png }};

        public ObservableCollection<UsersStarredBook> UserBooks { get; set; }    
        private ProdigyServices _services;
        private readonly UserService _userService;
        public ICommand ChangeUsernameBtn { get; protected set; }
        public ICommand ChangePasswordBtn { get; protected set; }
        public ICommand UploadCommand { get; protected set; }
        public ICommand UsePhoneCameraCommand { get; protected set; }
        public ICommand PickFileCommand { get; protected set; }

        private string image;
        private string _message;
        private string _username;
        private string _password;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _errorMessage;
        private bool _isErrorMessage;
        private User _user;
        private bool isRefresh;

        private string _content;
        private int _selectedTab;
        private FileResult _fileResult;
        const string FilePickError = "An error occurred when picking file";


        private string _NEWusername;
        private string _NEWpassword;
        
        private bool _isChangeUserError;
        private bool _isChangePassError;



        #region get and set for fields

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChange(nameof(Content));
            }
        }
        public bool IsRefresh
        {
            get => isRefresh;
            set
            {
                isRefresh = value;  
                OnPropertyChange();    
            }

        }
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
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChange(nameof(User));
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
        public string Image
        {
            get => $"{ProdigyServices.IMAGE_URL}{User.Image}";
            set
            {
                User.Image = value;
                OnPropertyChange(nameof(Image));
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

        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                _selectedTab = value;
                OnPropertyChange(nameof(SelectedTab));
            }
        }
        public string FilePickBtnText
        {
            get => FileResult?.FileName ?? "Pick File";
        }
        public bool IsFileSelected
        {
            get => FileResult != null;
        }
        public FileResult FileResult
        {
            get => _fileResult;
            set
            {
                _fileResult = value;
                OnPropertyChange(nameof(FileResult));
                OnPropertyChange(nameof(FilePickBtnText));
                OnPropertyChange(nameof(IsFileSelected));
            }
        }
        
        #endregion


        
        public ProfilePageViewModel(ProdigyServices services, UserService userService)  
        {
            this._services = services;
            this._userService = userService;

            User = _userService.User;
            UserBooks = new ObservableCollection<UsersStarredBook>();

            LoadBooks = new((s, e) =>
            {
                User = _userService.User;


                UserBooks.Clear();
                foreach (var book in User.UsersStarredBooks)
                {
                    UserBooks.Add(book);
                }
                OnPropertyChange(nameof(UserBooks));
            });


           
            UploadCommand = new Command(async () =>
            {

                try
                {
                    try
                    {
                        FileResult = await FilePicker.Default.PickAsync(filePickOptions[SelectedTab]);
                    }
                    catch (Exception)
                    {
                        FileResult = null;
                        ErrorMessage = FilePickError;
                        IsErrorMessage = true;
                    }
                    if (FileResult == null && SelectedTab != 0 &&
                        !await Shell.Current.DisplayAlert("empty file", "upload profile picture without a selected file?", "yes", "cancel"))
                        return;

                   

                   

                    StatusEnum responseCode = await services.UploadPFP(FileResult);
                    switch (responseCode)
                    {
                        case StatusEnum.OK:
                            await Shell.Current.DisplayAlert("uploaded", "uploaded successfully", "ok");
                            User = userService.User;
                            OnPropertyChange(nameof(Image));
                            //var stream = await FileResult.OpenReadAsync();
                            //PhotoImageSource = ImageSource.FromStream(() => stream);
                            //OnPropertyChange(nameof(PhotoImageSource));
                            Content = ""; FileResult = null;
                            break;

                        case StatusEnum.Unauthorized:
                            IsErrorMessage = true;
                            break;

                        default:
                            throw new Exception();
                    }
                }
                catch (Exception)
                {
                    IsErrorMessage = true;
                }
            });


            UsePhoneCameraCommand = new Command(async () =>
            {
                try
                {
                    try
                    {
                        FileResult = await MediaPicker.Default.CapturePhotoAsync();
                    }
                    catch (Exception)
                    {
                        FileResult = null;
                        ErrorMessage = FilePickError;
                        IsErrorMessage = true;
                    }
                    if (FileResult == null && SelectedTab != 0 &&
                        !await Shell.Current.DisplayAlert("empty file", "upload profile picture without a selected file?", "yes", "cancel"))
                        return;





                    StatusEnum responseCode = await services.UploadPFP(FileResult);
                    switch (responseCode)
                    {
                        case StatusEnum.OK:
                            await Shell.Current.DisplayAlert("uploaded", "uploaded successfully", "ok");
                            User = userService.User;
                            OnPropertyChange(nameof(Image));
                            //var stream = await FileResult.OpenReadAsync();
                            //PhotoImageSource = ImageSource.FromStream(() => stream);
                            //OnPropertyChange(nameof(PhotoImageSource));
                            Content = ""; FileResult = null;
                            break;

                        case StatusEnum.Unauthorized:
                            IsErrorMessage = true;
                            break;

                        default:
                            throw new Exception();
                    }
                }
                catch (Exception)
                {
                    IsErrorMessage = true;
                }
                
            });

            #region change X

            #region change username


            ChangeUsernameBtn = new Command(async () =>
            {
                if (!validateUsername())
                {
                    await Shell.Current.DisplayAlert("error", "invalid username, make sure it is longer than 1 character", "try again");
                    _isErrorMessage = true;
                    return;
                }
                try
                {
                   
                    if (!await services.ChangeUsername(_NEWusername))
                    {
                        IsChangeUsernameError = true;
                        await Shell.Current.DisplayAlert("error", "unable to change username", "try again");
                    }
                    else
                    {
                        IsChangeUsernameError = false;

                        User.Username = _NEWusername;
                        _userService.User = User;
                        OnPropertyChange(nameof(User));
                        await Shell.Current.DisplayAlert("change message", "successfully changed username", "OK");
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
                if (!validatePassword())
                {
                    await Shell.Current.DisplayAlert("error", "invalid password, make sure it is longer than 1 character", "try again");
                    _isErrorMessage = true;
                    return;
                }
                try
                {

                    if (!await services.ChangePassword(_NEWpassword))
                    {
                        IsChangePasswordError= true;
                        await Shell.Current.DisplayAlert("error", "unable to change password", "try again");
                    }
                    else
                    {
                        IsChangePasswordError = false;

                        User.UserPswd = _NEWpassword;
                        _userService.User = User;
                        OnPropertyChange(nameof(User));
                        await Shell.Current.DisplayAlert("change message", "successfully changed password", "OK");
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

        }

        

        public EventHandler LoadBooks { get; private set; }

        private bool validateUsername()
        {
            return !string.IsNullOrEmpty(NewUsername) && NewUsername.Length > 0;
        }
        private bool validatePassword()
        {
            return !string.IsNullOrEmpty(NewPassword) && NewPassword.Length > 1;
        }

        private async void GoToInfo(string isbn)
        {
            await Shell.Current.GoToAsync($"BookInfoPage?isbn={isbn}");
        }


    }
}

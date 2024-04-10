using ProdigyProjectFinal.Models;
using ProdigyProjectFinal.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProdigyProjectFinal.ViewModel
{
    [QueryProperty(nameof(ISBN), "isbn")]
    public class BookInfoViewModel : ViewModel
    {
        private ProdigyServices _services;
        private readonly UserService _userService;
        private List<Book> _books;
        private User _user;

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChange();
            }
        }

        public string ISBN { get; set; }

        public ObservableCollection<Book> Books { get; set; }
        public ICommand GoToProfile { get; protected set; }
        public ICommand GoToSearch { get; protected set; }

        public BookInfoViewModel(ProdigyServices services, UserService userService)
        {
            this._services = services;
            this._userService = userService;
            User = _userService.User;
            Books = new ObservableCollection<Book>();

            GoToProfile = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///ProfilePage");
            });

            GoToSearch = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///Search");
            });


        }
    }
}

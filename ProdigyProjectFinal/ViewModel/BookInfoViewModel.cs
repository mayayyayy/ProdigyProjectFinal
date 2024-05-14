using CommunityToolkit.Maui.Core;
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
        private readonly IPopupService popupService;
        private ProdigyServices _services;
        private readonly UserService _userService;
        private Book _book;
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

        public Book Book
        {
            get => _book;
            set
            {
                _book = value;
                OnPropertyChange();
            }

        }

        public BookInfoViewModel(ProdigyServices services, UserService userService, IPopupService popupService)
        {
            this._services = services;
            this.popupService = popupService;
            this._userService = userService;
            User = _userService.User;

            


        }
        public void LoadBook(Book b)
        {
            Book = b;
        }
    }
}

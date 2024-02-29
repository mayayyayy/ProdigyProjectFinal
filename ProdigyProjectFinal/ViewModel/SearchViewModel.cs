using ProdigyProjectFinal.Models;
using ProdigyProjectFinal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProdigyProjectFinal.ViewModel
{
    public class SearchViewModel : ViewModel
    {
        private ProdigyServices _services;
        private readonly UserService _userService;
        private string _searchRequest;
        private List<Book> _books;
        private Book SelectedBook;
        public string SearchRequest
        {
            get => _searchRequest;
            set
            {
                _searchRequest = value;
                OnPropertyChange(nameof(SearchRequest));
            }
        }
        public Book SelectedB
        {
            get => SelectedBook;
            set
            {
                if (SelectedBook != value)
                {
                    SelectedBook = value;
                    StarBook(SelectedBook.ISBN);


                    OnPropertyChange(nameof(SelectedB));
                    OnPropertyChange(nameof(Books));
                }
            }
        }
        public List<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChange(nameof(Books));
            }
        }


        public bool IsSelectedBookStarred => SelectedB == null? false : _userService.User.UsersStarredBooks.Any(x => x.BookISBN == SelectedB.ISBN);
        

        public ICommand SearchCommand { get; protected set; }

        public SearchViewModel(ProdigyServices services, UserService userService)
        {
            this._services = services;
            this._userService = userService;

            SearchCommand = new Command(async () =>
            {
                if (!validateQuery())
                {
                    await Shell.Current.DisplayAlert("error", "invalid search, make sure it is longer than 1 character", "try again");
                    return;
                }

                try
                {
                  Books =  await services.SearchAsync(SearchRequest); 
                }
                catch { Exception e; }
               

            });


        }
        private async void StarBook(string isbn)
        {
            await _services.StarBook(SelectedBook.ISBN);
        }

        private bool validateQuery()
        {
            return !string.IsNullOrEmpty(SearchRequest) && SearchRequest.Length > 0;
        }
    }
}

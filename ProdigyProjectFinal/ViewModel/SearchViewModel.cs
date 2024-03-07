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
        public ObservableCollection<Book> Books   { get;set;}

        public ICommand SearchCommand { get; protected set; }

        public SearchViewModel(ProdigyServices services, UserService userService)
        {
            this._services = services;
            this._userService = userService;
            Books = new ObservableCollection<Book>();
            SearchCommand = new Command(async () =>
            {
                if (!validateQuery())
                {
                    await Shell.Current.DisplayAlert("error", "invalid search, make sure it is longer than 1 character", "try again");
                    return;
                }

                try
                {
                    Books.Clear();
                    var bookCollection=await services.SearchAsync(SearchRequest); 
                    foreach(var book in bookCollection) 
                    {
                        if(book.IsStarred) { book.IconImage = "starcoloured.png"; }
                        Books.Add(book);    
                    }
                    
                }
                catch { Exception e; }
               

            });


        }
        private async void StarBook(string isbn)
        {
            var success= await _services.StarBook(isbn);
            if (success)
            {
                //find from Books selected book, update IsStarred to true, remove fromm Books, and refresh 
                int i = Books.IndexOf(Books.Where(x => x.ISBN == isbn).FirstOrDefault());
                Book book = Books[i];
                book.IsStarred = !book.IsStarred;
                if (book.IsStarred) { book.IconImage = "starcoloured.png"; }
                else book.IconImage = "starempty.png";
                Books.Remove(book);
                Books.Insert(i, book);  
            }

        }

        private bool validateQuery()
        {
            return !string.IsNullOrEmpty(SearchRequest) && SearchRequest.Length > 0;
        }
    }
}

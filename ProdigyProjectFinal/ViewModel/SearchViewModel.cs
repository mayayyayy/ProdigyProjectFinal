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
    public class SearchViewModel : ViewModel
    {
        private readonly IPopupService popupService;
        private ProdigyServices _services;
        private readonly UserService _userService;
        private string _searchRequest;
        private List<Book> _books;
        private Book SelectedBook;
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
        public string SearchRequest
        {
            get => _searchRequest;
            set
            {
                _searchRequest = value;
                OnPropertyChange(nameof(SearchRequest));
            }
        }
        
        public ObservableCollection<Book> Books { get;set;}

        #region commands
        public ICommand SearchCommand { get; protected set; }
        public ICommand PopUpInfoCommand { get; protected set; }
        public ICommand StarBookCommand { get; protected set; }
        public ICommand TBRCommand { get; protected set; }
        public ICommand CurrentReadCommand { get; protected set; }
        public ICommand DroppedBookCommand { get; protected set; }
        #endregion

        public SearchViewModel(ProdigyServices services, UserService userService, IPopupService popupService)
        {
            this.popupService = popupService;
            this._services = services;
            this._userService = userService;
            User = _userService.User;
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
                        if (book.IsStarred )
                            book.IconImage = "starcoloured.png";
                        if (book.IsTBR)
                            book.TBRImage = "bookyellow.png";
                        if (book.IsCR)
                            book.CurrentReadImage = "clockcoloured.png";
                        if (book.IsDrB)
                            book.DroppedImage = "trashcoloured.png";
                        Books.Add(book);
                    }
                    
                }
                catch { Exception e; }
               

            });

            StarBookCommand = new Command<string>(async (isbn) =>
            { await StarBook(isbn); });

            TBRCommand = new Command<string>(async (isbn) =>
            { await TBRBook(isbn); });

            CurrentReadCommand = new Command<string>(async (isbn) =>
            { await CurrentRead(isbn); });

            DroppedBookCommand = new Command<string>(async (isbn) =>
            { await DroppedBook(isbn); });

            PopUpInfoCommand = new Command<Book>(async (book) =>
            { await BookInfoPopUp(book); });
        }

        private async Task BookInfoPopUp(Book book)
        {
            await this.popupService.ShowPopupAsync<BookInfoViewModel>(onPresenting: viewModel => viewModel.LoadBook(book));
        }

        private async Task StarBook(string isbn)
        {

            var success = await _services.StarBook(isbn);

            if (success)
            {
                User.UsersStarredBooks.Add(new UsersStarredBook() { BookISBN = isbn, User = User });
                //find from Books selected book, update IsStarred to true, remove from Books, and refresh 
                int i = Books.IndexOf(Books.Where(x => x.ISBN == isbn).FirstOrDefault());
                Book book = Books[i];
                book.IsStarred = !book.IsStarred;
                if (book.IsStarred) { book.IconImage = "starcoloured.png"; }
                else book.IconImage = "starempty.png";

                Books.RemoveAt(i);
                Books.Insert(i, book);
            }
        }

        private async Task CurrentRead(string isbn)
        {

            var success = await _services.CRBook(isbn);

            if (success)
            {
                User.UsersCurrentRead.Add(new UsersCurrentRead() { BookISBN = isbn, User = User });
                int i = Books.IndexOf(Books.Where(x => x.ISBN == isbn).FirstOrDefault());
                Book book = Books[i];
                book.IsCR = !book.IsCR;
                if (book.IsCR) { book.CurrentReadImage = "clockcoloured.png"; }
                else book.CurrentReadImage = "clock.png";

                Books.RemoveAt(i);
                Books.Insert(i, book);
            }
        }

        private async Task DroppedBook(string isbn)
        {
            var success = await _services.DropBook(isbn);

            if (success)
            {
                User.UsersDropped.Add(new UsersDroppedBooks() { BookISBN = isbn, User = User });
                int i = Books.IndexOf(Books.Where(x => x.ISBN == isbn).FirstOrDefault());
                Book book = Books[i];
                book.IsDrB = !book.IsDrB;
                if (book.IsDrB) { book.DroppedImage = "trashcoloured.png"; }
                else book.DroppedImage = "trash.png";

                Books.RemoveAt(i);
                Books.Insert(i, book);
            }
        }

        private async Task TBRBook(string isbn)
        {
            var success = await _services.TBRBook(isbn);

            if (success)
            {
                User.UsersTBR.Add(new UsersTBR() { UserId = User.Id, BookIsbn = isbn });
                int i = Books.IndexOf(Books.Where(x => x.ISBN == isbn).FirstOrDefault());
                Book book = Books[i];
                book.IsTBR = !book.IsTBR;
                if (book.IsTBR) { book.TBRImage = "bookyellow.png"; }
                else book.TBRImage = "book.png";

                Books.RemoveAt(i);
                Books.Insert(i, book);
            }
        }


        private bool validateQuery()
        {
            return !string.IsNullOrEmpty(SearchRequest) && SearchRequest.Length > 0;
        }


    }

        
    }


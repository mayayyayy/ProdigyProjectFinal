using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Devices.Bluetooth.Advertisement;

namespace ProdigyProjectFinal.Models
{
    public class Book
    {
        public string Publisher { get; set; }
        public string AuthorKey { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string ISBNstring { get; set; }
        public string IconImage { get; set; } = "starempty.png";
        public bool IsStarred { get; set; } = false;

        

        public Book(string publisher, string authorKey, string authorName, string title, string iSBN)
        {
            Publisher = publisher;
            AuthorKey = authorKey;
            AuthorName = authorName;
            Title = title;
            ISBN = iSBN;
            ISBNstring = $"https://covers.openlibrary.org/b/isbn/{ISBN}-L.jpg";
        }
    }
}

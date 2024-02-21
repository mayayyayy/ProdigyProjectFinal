using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyProjectFinal.Models
{
    public class Book
    {
        public string Publisher { get; set; }
        public string AuthorKey { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }

        public Book(string publisher, string authorKey, string authorName, string title)
        {
            Publisher = publisher;
            AuthorKey = authorKey;
            AuthorName = authorName;
            Title = title;
        }
    }
}

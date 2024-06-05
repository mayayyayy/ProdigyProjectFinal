using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProdigyProjectFinal.Models
{
    public class Book
    {
        public string Publisher { get; set; }
        public string AuthorKey { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        [JsonPropertyName("ISBNstring")]
        public string BookImage { get; set; }
        public string IconImage { get; set; } = "starempty.png";
        public string TBRImage { get; set; } = "book.png";
        public string CurrentReadImage { get; set; } = "clock.png";
        public string DroppedImage { get; set; } = "trash.png";

        public bool IsStarred { get; set; } = false;
        public bool IsTBR { get; set; } = false;
        public bool IsCR { get; set; } = false;
        public bool IsDrB { get; set; } = false;



        public Book(string publisher, string authorKey, string authorName, string title, string iSBN)
        {
            Publisher = publisher;
            AuthorKey = authorKey;
            AuthorName = authorName;
            Title = title;
            ISBN = iSBN;
            BookImage = $"https://covers.openlibrary.org/b/isbn/{ISBN}-L.jpg";
        }
    }
}

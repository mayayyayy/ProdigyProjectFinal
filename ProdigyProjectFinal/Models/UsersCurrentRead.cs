using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyProjectFinal.Models
{
    public class UsersCurrentRead
    {
        public int Id { get; set; }
        public string BookISBN { get; set; } = null!;

        public int UserId { get; set; }

        public string BookImage => string.IsNullOrEmpty(BookISBN) ? "emptyimage.png" : $"https://covers.openlibrary.org/b/isbn/{BookISBN}-L.jpg";

        public virtual User User { get; set; } = null!;
        public UsersCurrentRead() { }
    }
}

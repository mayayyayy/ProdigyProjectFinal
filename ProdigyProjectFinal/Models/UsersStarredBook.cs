using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyProjectFinal.Models
{
    public class UsersStarredBook
    {
        public int Id { get; set; }

        public string BookISBN { get; set; }
        public string BookImage { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public UsersStarredBook()
        {
            BookImage = $"https://covers.openlibrary.org/b/isbn/{BookISBN}-L.jpg";
        }
    }
}

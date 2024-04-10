using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyProjectFinal.Models
{
   public class UsersTBR
    {
        public int Id { get; set; }
        //public string BookImage => string.IsNullOrEmpty(BookIsbn) ? "emptyimage.png" : $"https://covers.openlibrary.org/b/isbn/{BookIsbn}-L.jpg";
        public string BookIsbn { get; set; } = null!;

        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}

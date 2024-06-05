using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyProjectFinal.Models
{
    public class User
    {
        //login is via username and password 
        public int Id { get; set; } 
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPswd { get; set; }
        public string Email { get; set; }
        public string Image {  get; set; }
        public List<UsersStarredBook> UsersStarredBooks { get; set; }
        public List<UsersTBR> UsersTBR { get; set; }

        public List<UsersCurrentRead> UsersCurrentReads { get; set; }
        public List<UsersDroppedBooks> UsersDroppedBooks { get; set; }


        public User()
        {
            Id = 0;
            Username = "";
            FirstName = "";
            LastName = "";
            UserPswd = "";
            Email = "";
            UsersStarredBooks = new();
            UsersTBR = new ();
            UsersCurrentReads = new();
            UsersDroppedBooks = new();
        }
    }
}

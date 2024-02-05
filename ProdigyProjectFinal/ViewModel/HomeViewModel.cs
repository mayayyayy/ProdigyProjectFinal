
using ProdigyProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ProdigyProjectFinal.View;
using ProdigyProjectFinal.Services;

namespace ProdigyProjectFinal.ViewModel
{
    public class HomeViewModel : ViewModel
    {
        private string welcomeM;
        public string WelcomeMessage { get { return welcomeM; } set { welcomeM = value; OnPropertyChange(); }  }
        public ICommand GoToProfile { get; protected set; }
        public ICommand GoToSearch { get; protected set; }

        public EventHandler GetUser { get; protected set; } 
        private User sessionUser;
        public User SessionUser { get { return sessionUser; } set { sessionUser = value; OnPropertyChange(); } }
        public HomeViewModel(ProdigyServices services)
        {
            GetUser = new EventHandler(async (s, e) => { SessionUser = await services.GetCurrentUser(); WelcomeMessage = "welcome " + SessionUser.FirstName; });

            GoToProfile = new Command(async () => 
            { 
                await Shell.Current.GoToAsync("///ProfilePage"); 
            });

            GoToSearch = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///Search");
            });

            
        }

       


    }
}

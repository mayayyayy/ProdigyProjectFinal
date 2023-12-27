using ProdigyProjectFinal.Models;
using ProdigyProjectFinal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProdigyProjectFinal.ViewModel
{
    public class MainPageViewModel :ViewModel
    {
        

        public ICommand GoToLoginBtn { get; protected set; }
        public ICommand GoToSignUpBtn { get; protected set; }

        public MainPageViewModel() 
        {
            GoToLoginBtn = new Command(async () => await Shell.Current.GoToAsync("Login"));
            GoToSignUpBtn = new Command(async () => await Shell.Current.GoToAsync("SignUp"));


        }


    }
}


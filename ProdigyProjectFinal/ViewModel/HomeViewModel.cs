﻿using ProdigyProjectFinal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ProdigyProjectFinal.Models;
using ProdigyProjectFinal.Services;

namespace ProdigyProjectFinal.ViewModel
{
    public class HomeViewModel : ViewModel
    {
        public ICommand GoToProfile { get; protected set; }
        public HomeViewModel(ProdigyServices service)
        {
            GoToProfile = new Command(async () => await Shell.Current.GoToAsync("//ProfilePage"));
        }
    }
}

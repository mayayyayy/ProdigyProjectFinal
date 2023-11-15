using ProdigyProjectFinal.Services;
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
    public class ProdigyViewModel : ViewModel
    {
        #region Fields
        private string message;
        private string foundEmail;
        #endregion

        #region Service
        readonly private ProdigyServices _prodService;
        #endregion

        #region Properties
        public string Message { get => message; set { message = value; OnPropertyChange(); } }
        public string FoundEmail { get => foundEmail; set { foundEmail = value; OnPropertyChange(); } }
        #endregion

        #region Commands
        public ICommand SearchCommand { get; protected set; }
        #endregion


      
        /// <param name="prodService"></param>
        public ProdigyViewModel(ProdigyServices prodService)
        {
            _prodService = prodService;
            var u = SecureStorage.Default.GetAsync("LoggedUser").Result;
            var user = JsonSerializer.Deserialize<User>(u);
            Message = $"Hello {user.FirstName}";
            //find email by name
            SearchCommand = new Command<string>(async (x) => FoundEmail = await _prodService.GetUserEmail(x));

        }
    }
}

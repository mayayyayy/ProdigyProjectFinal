using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ProdigyProjectFinal.Models;

namespace ProdigyProjectFinal.Services
{
    
    public class UserService
    {
        readonly JsonSerializerOptions _serializerOptions;
        private User _user;
        public User User
        {

            get => _user;
            set
            {
                _user = value;
                Task.Run(async () =>
                {
                    var u = JsonSerializer.Serialize(_user, _serializerOptions);
                    await SecureStorage.Default.SetAsync("CurrentUser", u);
                });
            }


        }

        public UserService()
        {
            _serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

            Task.Run(async () => 
             { 
                 var u= await SecureStorage.Default.GetAsync("CurrentUser"); 
                 if (u != null) 
                     User = JsonSerializer.Deserialize<User>(u, _serializerOptions); 
             });

            

        }
    }
}

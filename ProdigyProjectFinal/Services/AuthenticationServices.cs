using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyProjectFinal.Services
{
    public class AuthenticationServices
    {
        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(2000);
            return false;
        }
    }
}

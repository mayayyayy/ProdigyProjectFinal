using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyProjectFinal.ViewModel
{
    public class AppShellViewModel : ViewModel
    {
        private bool isVis;
        public bool Visible { get => isVis; set { if(isVis != value) isVis = value; OnPropertyChange(); } }
        public bool AntiVisible { get { if (isVis) return false;return true; } set { OnPropertyChange(); } }


        public AppShellViewModel()
        {
            Visible = false;
        }
    }
}

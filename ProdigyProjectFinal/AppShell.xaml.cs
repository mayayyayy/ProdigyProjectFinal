using ProdigyProjectFinal.View;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal;

public partial class AppShell : Shell
{
	public AppShell()//AppShellViewModel vm)
	{
        //this.BindingContext = vm;
        InitializeComponent();

        Routing.RegisterRoute("MainPage/Login", typeof(Login));
        Routing.RegisterRoute("MainPage/Signup", typeof(SignUp));


    }
   
}

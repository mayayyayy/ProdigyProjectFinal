using ProdigyProjectFinal.View;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();

        Routing.RegisterRoute("Login", typeof(Login));
        Routing.RegisterRoute("Signup", typeof(SignUp));


    }
   
}

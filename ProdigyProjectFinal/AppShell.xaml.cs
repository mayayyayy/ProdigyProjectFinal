using ProdigyProjectFinal.View;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();

        Routing.RegisterRoute(nameof (Home), typeof (HomeViewModel));
        Routing.RegisterRoute(nameof(Login), typeof(LoginViewModel));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPageViewModel));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePageViewModel));
        Routing.RegisterRoute(nameof(SignUp), typeof(SignUpViewModel));
        

    }
   
}

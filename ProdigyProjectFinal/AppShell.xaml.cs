using ProdigyProjectFinal.View;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();

        Routing.RegisterRoute(nameof(Login), typeof(Login));
        Routing.RegisterRoute(nameof(SignUp), typeof(SignUp));
        Routing.RegisterRoute(nameof(BookInfoPage), typeof(BookInfoPage));


    }
   
}

using ProdigyProjectFinal.View;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal;

public partial class App : Application
{
    public App()
	{
        InitializeComponent();
		MainPage = new AppShell();
	
		
	}
}

using ProdigyProjectFinal.Models;
using ProdigyProjectFinal.View;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal;

public partial class App : Application
{
	public User user { get; set; }
    public App()
	{
        InitializeComponent();
		MainPage = new AppShell();
	
		
	}
}

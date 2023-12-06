using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;

public partial class Home : ContentPage
{
	public Home(HomeViewModel vm)
	{
        
        InitializeComponent();
		BindingContext = vm;
    }
}
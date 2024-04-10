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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        foreach (var x in Shell.Current.Items)
        {
            if (x.CurrentItem.CurrentItem.Route == nameof(MainPage)) { x.FlyoutItemIsVisible = false; }
            //change the visiblity of the flyout Item
            else x.FlyoutItemIsVisible = true;
        }
    }
}
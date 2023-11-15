using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class LoadingPage : ContentPage
{
    public LoadingPage(LoadingPageViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.Opacity = 50;
        this.WidthRequest = 100;
        this.HeightRequest = 100;
        
    }
    
}
namespace ProdigyProjectFinal.View;
using ProdigyProjectFinal.ViewModel;

public partial class ProdPage : ContentPage
{
	public ProdPage(ProdigyViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var user = SecureStorage.GetAsync("LoggedUser");
        if (user == null)
        {
            await AppShell.Current.GoToAsync("MainPage");
        }
    }
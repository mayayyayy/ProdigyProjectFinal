using ProdigyProjectFinal.Services;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class ProfilePage : ContentPage
{
	private readonly ProdigyServices serv;
	public ProfilePage(ProfilePageViewModel vm, ProdigyServices service)
	{
		InitializeComponent();
		BindingContext = vm;
		serv = service;
	}

    private void ButtonClicked(object sender, EventArgs e)
    {
		serv.Logout();
		Shell.Current.GoToAsync($"//{nameof(Login)}");
    }
}
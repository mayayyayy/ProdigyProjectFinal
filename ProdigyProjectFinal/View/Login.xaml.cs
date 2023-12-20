using ProdigyProjectFinal.Services;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class Login : ContentPage
{
	private readonly ProdigyServices prodigyServices;
	public Login(LoginViewModel vm, ProdigyServices service)
	{
		InitializeComponent();
		BindingContext = vm;
		service = prodigyServices;
	}

	private async void ButtonClicked(object sender, EventArgs e)
	{
		prodigyServices.Login()
;		await Shell.Current.GoToAsync($"//{nameof(Home)}");
	}
}
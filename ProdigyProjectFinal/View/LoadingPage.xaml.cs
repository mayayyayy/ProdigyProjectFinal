using ProdigyProjectFinal.Services;

namespace ProdigyProjectFinal.View;

public partial class LoadingPage : ContentPage
{
	private readonly ProdigyServices authServ;
	public LoadingPage(ProdigyServices bm)
	{
		InitializeComponent();
		authServ = bm;

	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

		if (await authServ.IsAuthenticatedAsync())
		{
            // user is logged in, redirect to home page 
            await Shell.Current.GoToAsync($"//{nameof(Home)}");
        }
		else
		{
            //user is not logged in, redirect to login page
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class Home : ContentPage
{
	public Home(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
using ProdigyProjectFinal.Services;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		Loaded += vm.LoadBooks;
	}


}
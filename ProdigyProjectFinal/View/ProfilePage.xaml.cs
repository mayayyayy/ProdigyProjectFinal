using ProdigyProjectFinal.Services;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        Appearing += vm.Reset;

        Loaded += vm.LoadStarBooks;
		Appearing += vm.LoadStarBooks;

        Loaded += vm.LoadTBRBooks;
        Appearing += vm.LoadTBRBooks;

        Loaded += vm.LoadDroppedBooks;
        Appearing += vm.LoadDroppedBooks;

        Loaded += vm.LoadCurrentReadBooks;
        Appearing += vm.LoadCurrentReadBooks;
    }


}
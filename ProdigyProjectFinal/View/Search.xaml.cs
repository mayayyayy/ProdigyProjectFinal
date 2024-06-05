using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class Search : ContentPage
{
	public Search(SearchViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

		Appearing += vm.Reset;
    }
}
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class BookInfoPage : ContentPage
{
	public BookInfoPage(BookInfoViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
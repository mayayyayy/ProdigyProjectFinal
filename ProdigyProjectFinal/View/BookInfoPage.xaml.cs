using CommunityToolkit.Maui.Views;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class BookInfoPage : Popup
{
	public BookInfoPage(BookInfoViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
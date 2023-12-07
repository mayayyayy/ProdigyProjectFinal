using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }


}


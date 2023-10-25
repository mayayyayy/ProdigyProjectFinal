using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal;

public partial class MainPage : ContentPage
{


    public MainPage(MainPageViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }


}


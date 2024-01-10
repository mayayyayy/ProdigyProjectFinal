using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class Login : ContentPage
{
    public Login(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
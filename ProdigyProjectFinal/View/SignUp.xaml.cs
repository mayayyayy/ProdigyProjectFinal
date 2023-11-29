using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal.View;

public partial class SignUp : ContentPage
{
	public SignUp(SignUpViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
using ProdigyProjectFinal.View;
using ProdigyProjectFinal.ViewModel;
using ProdigyProjectFinal.Services;

namespace ProdigyProjectFinal;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("The_Juke_Box-FFP.ttf","MusicFont");
				fonts.AddFont("RobotoMono-VariableFont_wght.ttf", "robotoFont");
			});


        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<Login>();
        builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<ProdigyServices>();
		builder.Services.AddSingleton<SignUp>();
        builder.Services.AddSingleton<SignUpViewModel>();
        builder.Services.AddSingleton<Home>();
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<ProfilePageViewModel>();

        Routing.RegisterRoute("Home", typeof(Home));
        return builder.Build();
    }
}


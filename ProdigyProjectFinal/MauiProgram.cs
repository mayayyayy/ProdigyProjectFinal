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
                fonts.AddFont("Lamarkie.otf", "lamarkieFont");
                fonts.AddFont("Beckan_Regular.otf", "beckanFont");
			});

        builder.Services.AddSingleton<AppShellViewModel>();
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

        builder.Services.AddSingleton<Search>();
        builder.Services.AddSingleton<SearchViewModel>();

        builder.Services.AddSingleton<UserService>();




        return builder.Build();
    }
}


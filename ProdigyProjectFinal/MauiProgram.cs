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
			});


        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<Login>();
        builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddTransient<LoadingPageViewModel>();
		builder.Services.AddSingleton<LoadingPage>();
		builder.Services.AddSingleton<ProdigyServices>();

		return builder.Build();
    }
}


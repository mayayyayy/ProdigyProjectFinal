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
                fonts.AddFont("Lamarkie.otf", "lamarkieFont");
                fonts.AddFont("boho.otf", "bohoFont");
                fonts.AddFont("assassins.ttf", "assassinsFont");
                fonts.AddFont("mango.ttf", "mangoFont");
                fonts.AddFont("figtree.ttf", "figtreeFont");
                fonts.AddFont("oliver.ttf", "oliverFont");
                fonts.AddFont("linden.ttf", "lindenFont");
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

        builder.Services.AddTransient<BookInfoPage>();
        builder.Services.AddTransient<BookInfoViewModel>();


        return builder.Build();
    }
}


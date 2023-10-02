using Microsoft.Extensions.Logging;
using MauiApp1.Data;
using Radzen;

namespace MauiApp1;

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
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddRadzenComponents();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        //builder.Services.AddSingleton<WeatherForecastService>();

        // Set path to the SQLite database (it will be created if it does not exist)
        var dbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                @"WeatherForecasts.db");
        // Register WeatherForecastService and the SQLite database
        builder.Services.AddSingleton<WeatherForecastService>(
            s => ActivatorUtilities.CreateInstance<WeatherForecastService>(s, dbPath));

        var dbPath2 =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                @"UserData.db");
        // Register WeatherForecastService and the SQLite database
        builder.Services.AddSingleton<UserService>(
            s => ActivatorUtilities.CreateInstance<UserService>(s, dbPath2));


        return builder.Build();
	}
}

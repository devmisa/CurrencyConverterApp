using CurrencyConverterApp.Services;
using CurrencyConverterApp.Services.Interface;
using CurrencyConverterApp.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CurrencyConverterApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        ConfigureConfiguration(builder);
        ConfigureFonts(builder);
        ConfigureServices(builder);
        ConfigureLogging(builder);

        return builder.Build();
    }

    private static void ConfigureConfiguration(MauiAppBuilder builder)
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("CurrencyConverterApp.Properties.appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);
            builder.Services.AddSingleton<IConfiguration>(config);
        }
        catch (Exception ex)
        {
            var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder.AddDebug());
            var logger = loggerFactory.CreateLogger(typeof(MauiProgram));
            logger.LogError(ex, $"Error loading configuration.");
            throw;
        }
    }

    private static void ConfigureFonts(MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
    }

    private static void ConfigureServices(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddSingleton<IAppCloser, AppCloser>();
        builder.Services.AddSingleton<ICurrencyService, CurrencyService>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();
    }

    private static void ConfigureLogging(MauiAppBuilder builder)
    {
#if DEBUG
        builder.Logging.AddDebug();
#endif
    }
}

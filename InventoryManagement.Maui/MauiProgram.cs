using InventoryManagement.Device;
using InventoryManagement.Maui.Views;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Maui;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddHttpClient("API", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7234/");
        });

        builder.Services.AddTransient<ProductViewModel>();
        builder.Services.AddTransient<ProductListView>();

        return builder.Build();
	}
}

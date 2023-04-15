using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using POC.EditingPhotoApp.ViewModels;
using POC.EditingPhotoApp.Views;

namespace POC.EditingPhotoApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<MainView>();
		builder.Services.AddSingleton<MainViewModel>();

		builder.Services.AddTransient<EditingView>();
		builder.Services.AddTransient<EditingViewModel>();

		return builder.Build();
	}
}

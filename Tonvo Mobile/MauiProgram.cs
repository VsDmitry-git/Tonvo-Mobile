using Microsoft.Extensions.Logging;

namespace Tonvo_Mobile;

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

        var services = builder.Services;

		services.AddSingleton<RootViewModel>();
		services.AddSingleton<RootView>();
		services.AddSingleton<UserInfoViewModel>();
		services.AddSingleton<UserInfoView>();
		services.AddSingleton<AccountViewModel>();
		services.AddSingleton<AccountView>();
		services.AddSingleton<ApplicantList>();
		services.AddSingleton<ApplicantViewModel>();
		services.AddSingleton<ApplicantInfoView>();
		services.AddSingleton<ApplicantInfoViewModel>();

		return builder.Build();
	}
}

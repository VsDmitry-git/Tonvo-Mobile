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

		services.AddTransient<RootViewModel>();
		services.AddTransient<RootView>();
		services.AddTransient<UserInfoViewModel>();
		services.AddTransient<UserInfoView>();
		services.AddTransient<AccountViewModel>();
		services.AddTransient<AccountView>();
		services.AddTransient<ApplicantList>();
		services.AddTransient<ApplicantViewModel>();
		services.AddTransient<ApplicantInfoView>();
		services.AddTransient<ApplicantInfoViewModel>();

        services.AddTransient<LoginViewModel>();

        services.AddTransient<Login>();

        return builder.Build();
	}
}

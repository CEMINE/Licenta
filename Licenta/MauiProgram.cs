using Licenta.Data;
using Licenta.Services;
using Microsoft.Extensions.Logging;

namespace Licenta
{
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

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

			builder.Services.AddSingleton<WeatherForecastService>();

			builder.Services.AddSingleton<EmployeeServices>();
			builder.Services.AddSingleton<IDialogService, DialogService>();
			builder.Services.AddSingleton<DepartmentServices>();
			builder.Services.AddSingleton<JobServices>();
			builder.Services.AddSingleton<EmployeeProjectServices>();
			builder.Services.AddSingleton<ProjectServices>();
			return builder.Build();
		}
	}
}
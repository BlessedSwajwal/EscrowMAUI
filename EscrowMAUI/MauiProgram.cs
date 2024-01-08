using CommunityToolkit.Maui;
using EscrowMAUI.Services;
using EscrowMAUI.ViewModel;
using EscrowMAUI.Views;
using Microsoft.Extensions.Logging;

namespace EscrowMAUI
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("ShortBaby,ttf", "ShortBaby");
                    fonts.AddFont("Cinnamon.ttf", "Cinnamon");
                })
                .UseMauiCommunityToolkit();

            {
                builder.Services.AddScoped(sp => new HttpClient
                {
                    BaseAddress = new Uri("https://skskkc9d-7240.asse.devtunnels.ms/api/")
                });
            }

            {
                builder.Services.AddSingletonWithShellRoute<LoginPage, LoginViewModel>(nameof(LoginPage));
                builder.Services.AddSingletonWithShellRoute<SignUpPage, LoginViewModel>(nameof(SignUpPage));
            }

            {
                builder.Services.AddScoped<AuthServices>();
            }
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

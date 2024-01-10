using CommunityToolkit.Maui;
using EscrowMAUI.ProviderViews;
using EscrowMAUI.Services;
using EscrowMAUI.ViewModel;
using EscrowMAUI.Views;
using Mapster;
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
                builder.Services.AddSingleton(sp => new HttpClient
                {
                    BaseAddress = new Uri("https://skskkc9d-7240.asse.devtunnels.ms/api/")
                });
            }

            {
                builder.Services.AddSingletonWithShellRoute<LoginPage, LoginViewModel>(nameof(LoginPage));
                builder.Services.AddSingletonWithShellRoute<SignUpPage, LoginViewModel>(nameof(SignUpPage));
                builder.Services.AddScopedWithShellRoute<UserDetailPage, UserDetailViewModel>(nameof(UserDetailPage));
                builder.Services.AddScopedWithShellRoute<OrdersPage, OrdersViewModel>(nameof(OrdersPage));
                builder.Services.AddTransientPopup<CreateOrderPopup, OrdersViewModel>();

                builder.Services.AddTransientWithShellRoute<OrderDetailPage, OrderDetailViewModel>(nameof(OrderDetailPage));

                builder.Services.AddScopedWithShellRoute<CreatedOrdersPage, CreatedOrdersViewModel>(nameof(CreatedOrdersPage));
            }

            {
                builder.Services.AddScoped<AuthServices>();
                builder.Services.AddScoped<OrdersService>();
            }

            {
                //Mapster 
                var config = TypeAdapterConfig.GlobalSettings;
                config.Scan(typeof(MauiProgram).Assembly);
                builder.Services.AddSingleton(config);
            }
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

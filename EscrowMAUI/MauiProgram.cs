﻿using CommunityToolkit.Maui;
using DevExpress.Maui;
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
                .UseMauiCommunityToolkit()
                .UseDevExpress();

            {
                builder.Services.AddSingleton(sp => new HttpClient
                {
                    // BaseAddress = new Uri("https://dragonescrow.somee.com/api/")
                    BaseAddress = new Uri("https://skskkc9d-7240.asse.devtunnels.ms/api/")
                });
            }

            {
                //For transparent entry color in android. Removes the underline that appeats for Entry.
                Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
                {
#if ANDROID
                    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
                });
            }

            {
                builder.Services.AddSingletonWithShellRoute<LoginPage, LoginViewModel>(nameof(LoginPage));
                builder.Services.AddSingletonWithShellRoute<SignUpPage, LoginViewModel>(nameof(SignUpPage));
                builder.Services.AddTransientWithShellRoute<ConsumerDetailPage, ConsumerDetailViewModel>(nameof(ConsumerDetailPage));

                builder.Services.AddTransientWithShellRoute<OrdersPage, OrdersViewModel>(nameof(OrdersPage));

                builder.Services.AddTransientWithShellRoute<OrderDetailPage, OrderDetailViewModel>(nameof(OrderDetailPage));

                builder.Services.AddScopedWithShellRoute<CreatedOrdersPage, CreatedOrdersViewModel>(nameof(CreatedOrdersPage));

                builder.Services.AddTransientWithShellRoute<ProviderHomePage, ProviderHomeViewModel>(nameof(ProviderHomePage));

                builder.Services.AddSingletonWithShellRoute<CreateBidPage, CreateBidViewModel>(nameof(CreateBidPage));
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

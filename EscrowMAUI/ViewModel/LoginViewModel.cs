using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EscrowMAUI.Models;
using EscrowMAUI.ProviderViews;
using EscrowMAUI.Services;
using EscrowMAUI.Views;
using OneOf;

namespace EscrowMAUI.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthServices _authServices;
    public LoginViewModel(AuthServices authServices)
    {
        User = new User();
        _authServices = authServices;
        User.UserType = Constants.Constants.ConsumerType;
    }

    [ObservableProperty]
    string _passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

    [ObservableProperty]
    User _user;

    [ObservableProperty]
    bool _isFormValid;

    [ObservableProperty]
    string _emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    [ObservableProperty]
    bool _isProcessing = false;


    [RelayCommand]
    async Task LoginRequested()
    {
        await Task.CompletedTask;
        IsProcessing = true;
        var result = await _authServices.LoginAsync(User.Email, User.Password, User.UserType);
        await HandleResult(result);
    }

    [RelayCommand]
    async Task RegisterRequested()
    {
        await Task.CompletedTask;
        IsProcessing = true;
        var result = await _authServices.Register(User, User.UserType);
        await HandleResult(result);
    }

    private async Task HandleResult(OneOf<User, Problem> result)
    {
        await result.Match(
                async userResponse =>
                {
                    Preferences.Default.Set(Constants.Constants.TokenKeyConstant, userResponse.Token);
                    Preferences.Default.Set(Constants.Constants.UserType, userResponse.UserType);
                    IsProcessing = false;
                    //TODO: Redirect to home page
                    var appShell = (AppShell)App.Current.MainPage;
                    appShell.UpdateTabs();
                    if (userResponse.UserType.Equals(Constants.Constants.ConsumerType, StringComparison.OrdinalIgnoreCase))
                    {
                        await Shell.Current.GoToAsync($"{nameof(OrdersPage)}");
                    }
                    else
                    {
                        await Shell.Current.GoToAsync(nameof(ProviderHomePage));
                    }

                },
                async errorResponse =>
                {
                    IsProcessing = false;
                    var toast = Toast.Make(errorResponse.Detail, ToastDuration.Short, 14);
                    await toast.Show();
                }
            );
    }

    public async Task LoggedInCheck()
    {

        if (Preferences.Default.ContainsKey(Constants.Constants.TokenKeyConstant))
        {
            if (Preferences.Default.Get<string>(Constants.Constants.UserType, "").Equals(Constants.Constants.ConsumerType))
            {
                await Shell.Current.GoToAsync($"{nameof(ConsumerDetailPage)}");
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(ProviderHomePage));
            }

        }
        return;
    }
}

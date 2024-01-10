using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EscrowMAUI.Constants;
using EscrowMAUI.Models;
using EscrowMAUI.Services;
using EscrowMAUI.Views;

namespace EscrowMAUI.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthServices _authServices;
    public LoginViewModel(AuthServices authServices)
    {
        User = new User();
        _authServices = authServices;
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
        var result = await _authServices.LoginAsync(User.Email, User.Password);
        await result.Match(
                async userResponse =>
                {
                    Preferences.Default.Set(Constants.Constants.TokenKeyConstant, userResponse.Token);
                    Preferences.Default.Set(Constants.Constants.UserType, userResponse.UserType);
                    IsProcessing = false;
                    //TODO: Redirect to home page
                    await Shell.Current.GoToAsync($"{nameof(UserDetailPage)}");
                },
                async errorResponse =>
                {
                    IsProcessing = false;
                    var toast = Toast.Make(errorResponse.Detail, ToastDuration.Short, 14);
                    await toast.Show();
                }
            );
    }

    [RelayCommand]
    async Task RegisterRequested()
    {
        var uu = User;
        await Task.CompletedTask;
    }

    [RelayCommand]
    async Task LoggedInCheck()
    {
        IsProcessing = true;
        if (Preferences.Default.ContainsKey(Constants.Constants.TokenKeyConstant))
        {
            await Shell.Current.GoToAsync($"{nameof(Views.UserDetailPage)}");
        }
        return;
    }
}

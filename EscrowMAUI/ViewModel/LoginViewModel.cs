using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    //TODO: Check if this is needed.
    [ObservableProperty]
    Problem _errors;

    //TODO: Check if this is needed.
    [ObservableProperty]
    bool _errorOccured;

    [ObservableProperty]
    bool _isProcessing = false;

    [RelayCommand]
    async Task LoginRequested()
    {
        await Task.CompletedTask;

        IsProcessing = true;
        ErrorOccured = false;
        var result = await _authServices.LoginAsync(User.Email, User.Password);
        await result.Match(
                async userResponse =>
                {
                    Preferences.Default.Set("JwtToken", userResponse.Token);
                    IsProcessing = false;
                    ErrorOccured = true;
                    //TODO: Redirect to home page
                    await Shell.Current.GoToAsync(nameof(MainPage));
                },
                async errorResponse =>
                {
                    Errors = errorResponse;
                    IsProcessing = false;
                    ErrorOccured = true;
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
}

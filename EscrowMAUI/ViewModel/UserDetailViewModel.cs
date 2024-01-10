using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EscrowMAUI.Models;
using EscrowMAUI.Services;
using EscrowMAUI.Views;

namespace EscrowMAUI.ViewModel;

public partial class UserDetailViewModel : ObservableObject
{
    public UserDetailViewModel()
    {
        var token = Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, string.Empty);
        if (token == string.Empty) Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        User = TokenDecode.ReadJwtTokenContent(token);
        User.UserType = Preferences.Default.Get<string>(Constants.Constants.UserType, string.Empty);
    }

    [ObservableProperty]
    User _user;

    [RelayCommand]
    async Task Logout()
    {
        Preferences.Default.Clear();
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}

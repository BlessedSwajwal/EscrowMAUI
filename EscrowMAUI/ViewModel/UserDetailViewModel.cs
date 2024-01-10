using CommunityToolkit.Mvvm.ComponentModel;
using EscrowMAUI.Constants;
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
}

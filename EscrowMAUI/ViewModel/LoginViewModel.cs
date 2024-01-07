using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EscrowMAUI.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    public LoginViewModel()
    {

    }
    [ObservableProperty]
    string email;

    [ObservableProperty]
    string password;

    [ObservableProperty]
    bool _isFormValid = false;

    [ObservableProperty]
    string _selectedUserType = "consumer";

    [ObservableProperty]
    string emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    [RelayCommand]
    async Task LoginRequested()
    {
        await Task.CompletedTask;
    }
}

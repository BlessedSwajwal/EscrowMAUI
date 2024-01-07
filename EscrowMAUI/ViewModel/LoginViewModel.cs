using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EscrowMAUI.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    public LoginViewModel()
    {

    }

    [ObservableProperty]
    string _passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";


    [ObservableProperty]
    string _firstName;
    [ObservableProperty]
    string _lastName;
    [ObservableProperty]
    string _phone;

    [ObservableProperty]
    string _email;

    [ObservableProperty]
    string _password;

    [ObservableProperty]
    bool _isFormValid = false;

    [ObservableProperty]
    string _selectedUserType = "consumer";

    [ObservableProperty]
    string _emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    [RelayCommand]
    async Task LoginRequested()
    {
        await Task.CompletedTask;
    }

    [RelayCommand]
    async Task RegisterRequested()
    {
        await Task.CompletedTask;
    }
}

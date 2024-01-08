using CommunityToolkit.Mvvm.ComponentModel;

namespace EscrowMAUI.Models;

public partial class User : ObservableObject
{
    public static User Empty => new User() { Id = Guid.Empty };
    [ObservableProperty]
    Guid _id;
    [ObservableProperty]
    string _firstName;
    [ObservableProperty]
    string _lastName;
    [ObservableProperty]
    string _email;
    [ObservableProperty]
    string _phone;
    [ObservableProperty]
    string _password;
    [ObservableProperty]
    string _userType;
    [ObservableProperty]
    string _token;
}

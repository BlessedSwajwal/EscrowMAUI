using CommunityToolkit.Mvvm.ComponentModel;

namespace EscrowMAUI.Models;

public partial class User : ObservableObject
{
    public static User Empty => new User() { Id = Guid.Empty };
    public string FullName => FirstName + " " + LastName;
    public Guid Id { get; set; }
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

    public string Token { get; set; }
}

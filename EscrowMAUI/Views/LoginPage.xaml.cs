using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        EmailEntry.Text = "";
        PasswordEntry.Text = "";
    }
}
using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _loginViewModel;

    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
        _loginViewModel = loginViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        EmailEntry.Text = "";
        PasswordEntry.Text = "";
    }

    private void UserTypeButton_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        btn.BackgroundColor = Colors.Blue;
        string name = btn.StyleId;

        if (name == "ProviderButton")
        {
            ConsumerButton.BackgroundColor = Colors.AntiqueWhite;
            _loginViewModel.SelectedUserType = "provider";
        }
        else
        {
            ProviderButton.BackgroundColor = Colors.AntiqueWhite;
            _loginViewModel.SelectedUserType = "consumer";
        }
    }

    private void Form_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (EmailValidationBehavior.IsValid)
        {
            _loginViewModel.IsFormValid = true;
        }
        else
        {
            _loginViewModel.IsFormValid = false;
        }
    }
}
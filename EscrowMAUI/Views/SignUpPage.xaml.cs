using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class SignUpPage : ContentPage
{
    private readonly LoginViewModel _loginViewModel;
    public SignUpPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        _loginViewModel = loginViewModel;
        BindingContext = _loginViewModel;
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
        if (EmailValidationBehavior.IsValid &&
            FirstNameValidationBehavior.IsValid &&
            LastNameValidationBehavior.IsValid &&
            PhoneValidationBehavior.IsValid &&
            PasswordValidationBehavior.IsValid)
        {
            _loginViewModel.IsFormValid = true;
        }
        else
        {
            _loginViewModel.IsFormValid = false;
        }
    }
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        FirstNameEntry.Text = LastNameEntry.Text = EmailEntry.Text = PasswordEntry.Text = PhoneEntry.Text = "";
    }
}
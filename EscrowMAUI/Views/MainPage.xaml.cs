using EscrowMAUI.ProviderViews;

namespace EscrowMAUI.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        btn.BackgroundColor = Colors.DimGray;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        if (Preferences.Default.ContainsKey(Constants.Constants.TokenKeyConstant))
        {
            if (Preferences.Default.Get<string>(Constants.Constants.UserType, "").Equals(Constants.Constants.ConsumerType))
            {
                await Shell.Current.GoToAsync($"{nameof(Views.UserDetailPage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(ProviderHomePage)}");
            }

        }
    }

}

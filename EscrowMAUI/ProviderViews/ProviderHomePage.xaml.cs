namespace EscrowMAUI.ProviderViews;

public partial class ProviderHomePage : ContentPage
{
    public ProviderHomePage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreatedOrdersPage));
    }
}
using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class ConsumerDetailPage : ContentPage
{
    public ConsumerDetailPage(ConsumerDetailViewModel consumerDetailViewModel)
    {
        InitializeComponent();
        BindingContext = consumerDetailViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await ((ConsumerDetailViewModel)BindingContext).OnAppearing();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Clear();
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    private async void NavigateTemp_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(OrdersPage));
    }
}
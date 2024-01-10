using EscrowMAUI.ViewModel;

namespace EscrowMAUI.ProviderViews;

public partial class ProviderHomePage : ContentPage
{
    private readonly ProviderHomeViewModel _providerHomeViewModel;
    public ProviderHomePage(ProviderHomeViewModel providerHomeViewModel)
    {
        InitializeComponent();
        BindingContext = providerHomeViewModel;
        _providerHomeViewModel = providerHomeViewModel;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreatedOrdersPage));
    }

    //protected async override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    await _providerHomeViewModel.OnAppearing();
    //}
}
using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class UserDetailPage : ContentPage
{
    private readonly UserDetailViewModel viewModel;
    public UserDetailPage(UserDetailViewModel userDetailViewModel)
    {
        InitializeComponent();
        BindingContext = userDetailViewModel;
        viewModel = userDetailViewModel;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(OrdersPage)}");
    }

}
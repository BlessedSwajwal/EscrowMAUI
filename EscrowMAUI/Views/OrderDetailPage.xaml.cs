using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;


public partial class OrderDetailPage : ContentPage
{
    private readonly OrderDetailViewModel viewModel;
    public OrderDetailPage(OrderDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.OnAppearing();
    }
}
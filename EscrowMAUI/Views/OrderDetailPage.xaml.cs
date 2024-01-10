using CommunityToolkit.Maui.Core;
using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;


public partial class OrderDetailPage : ContentPage
{
    private readonly OrderDetailViewModel viewModel;
    private readonly IPopupService _popupService;
    public OrderDetailPage(OrderDetailViewModel viewModel, IPopupService popupService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
        _popupService = popupService;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.OnAppearing();
    }

    private async void Button_Pressed(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Colors.Gray;
        _popupService.ShowPopup<OrderDetailViewModel>(onPresenting: vm =>
        {
            vm.BidDTO.OrderId = Guid.Parse(OrderIdLabel.Text);
        });
    }

    private void Button_Released(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Color.FromArgb("ac99ea");
    }
}
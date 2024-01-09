using CommunityToolkit.Maui.Core;
using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class OrdersPage : ContentPage
{
    private readonly OrdersViewModel _ordersViewModel;
    private readonly IPopupService _popupService;
    public OrdersPage(OrdersViewModel ordersViewModel, IPopupService popupService)
    {
        InitializeComponent();
        _ordersViewModel = ordersViewModel;
        BindingContext = _ordersViewModel;
        _popupService = popupService;
    }

    private async void Button_Pressed(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Colors.Gray;
        _popupService.ShowPopup<OrdersViewModel>();
    }

    private void Button_Released(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Color.FromArgb("ac99ea");
    }


}
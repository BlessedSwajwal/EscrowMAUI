using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class OrdersPage : ContentPage
{
    private readonly OrdersViewModel _ordersViewModel;
    public OrdersPage(OrdersViewModel ordersViewModel)
    {
        InitializeComponent();
        _ordersViewModel = ordersViewModel;
        BindingContext = _ordersViewModel;
    }


}
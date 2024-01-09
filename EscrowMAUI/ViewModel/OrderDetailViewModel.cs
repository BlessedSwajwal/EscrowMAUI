using CommunityToolkit.Mvvm.ComponentModel;
using EscrowMAUI.Models;
using EscrowMAUI.Services;
using System.ComponentModel;

namespace EscrowMAUI.ViewModel;

[QueryProperty(nameof(OrderId), nameof(OrderId))]
public partial class OrderDetailViewModel : ObservableObject, INotifyPropertyChanged
{
    private readonly OrdersService _ordersService;
    public OrderDetailViewModel(OrdersService ordersService)
    {
        _ordersService = ordersService;

    }

    [ObservableProperty]
    bool _errorOccured;
    [ObservableProperty]
    string _errorDetail;

    [ObservableProperty]
    Guid _orderId;

    [ObservableProperty]
    SingleOrderDetail _order;

    public bool CanAcceptBid => (Order is not null && Order.OrderStatus.Equals("created")) ? true : false;

    public async Task OnAppearing()
    {
        var result = await _ordersService.GetOrderDetail(OrderId);
        result.Match(
            orderResult =>
            {
                Order = orderResult;
                return "";
            },
            error =>
            {
                ErrorOccured = true;
                ErrorDetail = error.Detail;
                return "";
            }
            );
    }
}

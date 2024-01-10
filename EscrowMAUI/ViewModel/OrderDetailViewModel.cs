using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    [NotifyPropertyChangedFor(nameof(IsPayable))]
    SingleOrderDetail _order;

    public bool IsPayable => Order is not null && Order.OrderStatus.Equals(Constants.Constants.PendingOrder);

    public bool CanAcceptBid => (Order is not null && Order.OrderStatus.Equals("created")) ? true : false;

    [RelayCommand]
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

    [RelayCommand]
    async Task NavigateBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task PayButtonClicked()
    {
        try
        {
            Uri uri = new(Order.PaymentUri);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            //TODO: Proper error handling
        }
    }
}

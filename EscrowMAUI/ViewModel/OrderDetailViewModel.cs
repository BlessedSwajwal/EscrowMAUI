using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EscrowMAUI.Models;
using EscrowMAUI.Models.DTOs;
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
        BidDTO = new();
    }

    [ObservableProperty]
    bool _errorOccured = false;
    [ObservableProperty]
    string _errorDetail = "";

    [ObservableProperty]
    Guid _orderId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPayable))]
    SingleOrderDetail _order;

    public bool IsPayable => Order is not null && Order.OrderStatus.Equals(Constants.Constants.PendingOrder);

    public bool CanAcceptBid => (Order is not null && Order.OrderStatus.Equals("created")
        && TokenDecode.ReadJwtTokenContent(Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, "")).Id.Equals(Order.CreatorId)) ? true : false;

    [ObservableProperty]
    bool _canBid = false;

    [ObservableProperty]
    CreateBidDTO _bidDTO;

    [RelayCommand]
    public async Task OnAppearing()
    {
        var result = await _ordersService.GetOrderDetail(OrderId);
        result.Match(
            orderResult =>
            {
                Order = orderResult;

                //Checking if the order can be bidded by current user.
                //3 checks needed: If user type is provider, if order status is created and
                //                  if user has already bidded or not.
                if (Preferences.Default.Get<string>(Constants.Constants.UserType, "").Equals(Constants.Constants.ProviderType, StringComparison.OrdinalIgnoreCase)
                    && Order.OrderStatus.Equals("created"))
                {
                    var user = TokenDecode.ReadJwtTokenContent(Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, ""));
                    CanBid = Order.Bids.Any(bid => bid.BidderId == user.Id) ? false : true;
                }
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

    [RelayCommand]
    async Task AcceptBid(Guid BidId)
    {
        var result = await _ordersService.AcceptBid(Order.Id, BidId);
        if (!string.IsNullOrWhiteSpace(result))
        {
            Uri uri = new(result);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
            OnAppearing();
        }

    }

}

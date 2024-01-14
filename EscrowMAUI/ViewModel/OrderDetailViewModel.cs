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
    bool _bidsRefreshing = false;

    [ObservableProperty]
    bool _isKhaltiLoading = false;

    [ObservableProperty]
    bool _isStripeLoading = false;

    [ObservableProperty]
    Guid _bidId;

    [ObservableProperty]
    bool _errorOccured = false;
    [ObservableProperty]
    string _errorDetail = "";

    [ObservableProperty]
    Guid _orderId;

    [ObservableProperty]
    SingleOrderDetail _order;

    public double TopGridHeight = 0.2 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);

    public bool CanAcceptBid => (Order is not null && Order.OrderStatus.Equals("created")
        && TokenDecode.ReadJwtTokenContent(Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, "")).Id.Equals(Order.CreatorId)) ? true : false;

    [ObservableProperty]
    bool _canBid = false;

    [ObservableProperty]
    CreateBidDTO _bidDTO;

    [RelayCommand]
    public async Task OnAppearing()
    {
        BidsRefreshing = true;
        var result = await _ordersService.GetOrderDetail(OrderId);
        result.Match(
            orderResult =>
            {
                Order = orderResult;
                BidsRefreshing = false;

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
                BidsRefreshing = false;
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
    async Task AcceptBidWithKhalti()
    {
        IsKhaltiLoading = true;
        var result = await _ordersService.AcceptBid(Order.Id, BidId);
        if (!string.IsNullOrWhiteSpace(result))
        {
            Uri uri = new(result);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
            OnAppearing();
        }
        IsKhaltiLoading = false;
    }

    [RelayCommand]
    async Task AcceptBidWithStripe()
    {
        IsStripeLoading = true;
        var result = await _ordersService.AcceptBidWithStripe(Order.Id, BidId);
        if (!string.IsNullOrWhiteSpace(result))
        {
            Uri uri = new(result);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
            OnAppearing();
        }
        IsStripeLoading = false;
    }


    [RelayCommand]
    public void SetBidId(Guid id)
    {
        BidId = id;
    }
}

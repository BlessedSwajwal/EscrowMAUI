using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EscrowMAUI.Models;
using EscrowMAUI.Services;
using EscrowMAUI.Views;
using System.Collections.ObjectModel;

namespace EscrowMAUI.ViewModel;

public partial class ProviderHomeViewModel : ObservableObject
{
    private readonly OrdersService _ordersService;
    public ProviderHomeViewModel(OrdersService ordersService)
    {
        _ordersService = ordersService;
        Orders = new();
        OnAppearing();
    }
    public bool ErrorOccured = false;
    public string ErrorDetail = "";

    public ObservableCollection<Order> Orders { get; private set; }

    public async Task OnAppearing()
    {
        ErrorOccured = false;
        ErrorDetail = string.Empty;
        Orders.Clear();
        var result = await _ordersService.GetBidsSelectedOrders();
        result.Match(
            orders =>
            {
                foreach (var order in orders)
                    Orders.Add(order);
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
    async Task GoToUserDetail()
    {
        await Shell.Current.GoToAsync(nameof(ConsumerDetailPage));
    }

    [RelayCommand]
    async Task GoToOrderDetail(Guid id)
    {
        var parameters = new Dictionary<string, object>()
        {
            [nameof(OrderDetailViewModel.OrderId)] = id,
        };

        await Shell.Current.GoToAsync(nameof(OrderDetailPage), true, parameters);
    }
}


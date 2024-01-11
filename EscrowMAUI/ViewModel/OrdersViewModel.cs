using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EscrowMAUI.Models;
using EscrowMAUI.Models.DTOs;
using EscrowMAUI.Services;
using EscrowMAUI.Views;
using Mapster;
using System.Collections.ObjectModel;

namespace EscrowMAUI.ViewModel;

public partial class OrdersViewModel : ObservableObject
{
    private readonly OrdersService _ordersService;
    public OrdersViewModel(OrdersService ordersService)
    {
        _ordersService = ordersService;
        Orders = new();
        CreateOrderDTO = new();
    }

    [ObservableProperty]
    bool _ordersRefreshing = false;

    [ObservableProperty]
    bool errorOccured = false;

    [ObservableProperty]
    CreateOrderDTO _createOrderDTO;

    public ObservableCollection<Order> Orders { get; private set; }

    public string ErrorDetail { get; private set; } = "";

    [RelayCommand]
    public async Task OnAppearing()
    {
        OrdersRefreshing = true;
        ErrorOccured = false;
        ErrorDetail = string.Empty;
        Orders.Clear();
        var result = await _ordersService.GetAllUserOrders();
        result.Match(
            orders =>
            {
                foreach (var order in orders)
                    Orders.Add(order);
                OrdersRefreshing = false;
                return "";
            },
            error =>
            {
                OrdersRefreshing = false;
                ErrorOccured = true;
                ErrorDetail = error.Detail;
                return "";
            }
            );
    }

    [RelayCommand]
    async Task OnFormSubmitted()
    {
        ErrorOccured = false;
        ErrorDetail = string.Empty;
        var result = await _ordersService.SubmitOrder(CreateOrderDTO);

        result.Match(
            createdOrder =>
            {
                var order = createdOrder.Adapt<Order>();
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
    private async void GoToOrderDetail(Guid id)
    {
        var parameters = new Dictionary<string, object>()
        {
            [nameof(OrderDetailViewModel.OrderId)] = id,
        };

        await Shell.Current.GoToAsync(nameof(OrderDetailPage), true, parameters);
    }

}

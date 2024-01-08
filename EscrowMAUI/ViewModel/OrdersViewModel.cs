using CommunityToolkit.Mvvm.ComponentModel;
using EscrowMAUI.Models;
using EscrowMAUI.Services;
using System.Collections.ObjectModel;

namespace EscrowMAUI.ViewModel;

public partial class OrdersViewModel : ObservableObject
{
    private readonly OrdersService _ordersService;
    public OrdersViewModel(OrdersService ordersService)
    {
        _ordersService = ordersService;
        Orders = new();
        OnAppearing();
    }

    [ObservableProperty]
    bool errorOccured = false;

    public ObservableCollection<Order> Orders { get; private set; }

    public string ErrorDetail { get; private set; }

    public async void OnAppearing()
    {
        ErrorOccured = false;
        ErrorDetail = string.Empty;
        var result = await _ordersService.GetAllUserOrders();
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



}

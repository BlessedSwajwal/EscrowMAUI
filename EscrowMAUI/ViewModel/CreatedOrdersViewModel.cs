using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EscrowMAUI.Models;
using EscrowMAUI.Services;
using System.Collections.ObjectModel;

namespace EscrowMAUI.ViewModel;

public partial class CreatedOrdersViewModel : ObservableObject
{
    private readonly OrdersService _ordersService;

    public CreatedOrdersViewModel(OrdersService ordersService)
    {
        _ordersService = ordersService;
        Orders = new();
    }

    public ObservableCollection<Order> Orders { get; private set; }
    public bool ErrorOccured = false;
    public string ErrorDetail = "";

    [RelayCommand]
    public async Task OnAppearing()
    {
        ErrorOccured = false;
        ErrorDetail = string.Empty;
        Orders.Clear();
        var result = await _ordersService.GetAllCreatedOrders();
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

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EscrowMAUI.Models.DTOs;
using EscrowMAUI.Services;

namespace EscrowMAUI.ViewModel;

[QueryProperty(nameof(OrderId), nameof(OrderId))]

public partial class CreateBidViewModel : ObservableObject
{
    private readonly OrdersService _ordersService;
    public CreateBidViewModel(OrdersService ordersService)
    {
        BidDTO = new();
        _ordersService = ordersService;
    }

    public Guid OrderId { get; set; }

    [ObservableProperty]
    CreateBidDTO _bidDTO;

    [RelayCommand]
    async Task BidFormSubmitted()
    {
        var bid = await _ordersService.CreateBid(OrderId, BidDTO);
        await Shell.Current.GoToAsync("..");
    }
}

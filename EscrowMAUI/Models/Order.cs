using CommunityToolkit.Mvvm.ComponentModel;

namespace EscrowMAUI.Models;

public partial class Order : ObservableObject
{
    public Guid Id { get; set; }

    [ObservableProperty]
    string _name;

    [ObservableProperty]
    string _description;

    [ObservableProperty]
    int _cost;

    public string OrderStatus { get; set; }

    public Guid CreatorId { get; set; }

    [ObservableProperty]
    int _allowedDays;

    public Guid ProviderId { get; set; }
    public DateTime AcceptedDate { get; set; }
    public DateTime DeadLine { get; set; }
    public int BidsCount { get; set; }
    public Guid AcceptedBid { get; set; }
}


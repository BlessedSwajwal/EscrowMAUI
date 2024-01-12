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

    private DateTime _deadLine;
    public DateTime DeadLine
    {
        get
        {
            if (AcceptedDate.Equals(DateTime.MinValue)) return DateTime.MinValue;
            return _deadLine;
        }
        set
        {
            _deadLine = value;
        }
    }
    public int BidsCount { get; set; }
    public Guid AcceptedBid { get; set; }
}


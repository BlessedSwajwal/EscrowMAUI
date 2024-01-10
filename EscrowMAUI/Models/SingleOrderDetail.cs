namespace EscrowMAUI.Models;

//TODO: Add bid list and accepted Bid.
public class SingleOrderDetail
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public string OrderStatus { get; set; }
    public Guid CreatorId { get; set; }
    public int AllowedDays { get; set; }
    public Guid ProviderId { get; set; }
    public DateTime AcceptedDate { get; set; }
    public DateTime DeadLine { get; set; }
    string? _paymentUri;
    public string PaymentUri
    {
        get
        {
            return _paymentUri;
        }
        set
        {
            if (value.StartsWith("Order Status")) _paymentUri = "";
            _paymentUri = value;
        }
    }
    public IReadOnlyList<Bid> Bids { get; set; }
    public Guid AcceptedBid { get; set; }

    public int CostInRuppees => Cost / 100;
};


namespace EscrowMAUI.Models;

public record Bid(Guid BidId, Guid BidderId, string Comment, string BidStatus)
{
    public int ProposedAmount { get; set; }
    public int ProposedAmountInRuppees => ProposedAmount / 100;

}

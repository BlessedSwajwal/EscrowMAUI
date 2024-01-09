namespace EscrowMAUI.Models;

public record Bid(Guid BidId, Guid BidderId, int ProposedAmount, string Comment, string BidStatus);

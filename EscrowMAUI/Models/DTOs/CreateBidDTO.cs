using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace EscrowMAUI.Models.DTOs;

public partial class CreateBidDTO : ObservableObject
{
    public Guid OrderId { get; set; }

    [ObservableProperty]
    int _proposedAmountInRuppee;

    [ObservableProperty]
    string _comment;

    [JsonPropertyName("ProposedAmount")]
    public int ProposedAmountInPaisa => ProposedAmountInRuppee * 100;
}

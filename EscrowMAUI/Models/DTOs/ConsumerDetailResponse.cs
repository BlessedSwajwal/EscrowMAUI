namespace EscrowMAUI.Models.DTOs;

public class ConsumerDetailResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public string UserType { get; set; }
    public int TotalOrderCount { get; set; }
}


using CommunityToolkit.Mvvm.ComponentModel;

namespace EscrowMAUI.Models.DTOs;

public partial class CreateOrderDTO : ObservableObject
{
    [ObservableProperty]
    string _name;
    [ObservableProperty]
    string _description;
    [ObservableProperty]
    int _allowedDays;
    [ObservableProperty]
    int _costInRuppees;

    public int Cost => CostInRuppees * 100;
}


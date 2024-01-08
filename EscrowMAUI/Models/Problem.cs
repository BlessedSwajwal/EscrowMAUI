using CommunityToolkit.Mvvm.ComponentModel;

namespace EscrowMAUI.Models;

public partial class Problem : ObservableObject
{
    [ObservableProperty]
    string _title;
    [ObservableProperty]
    string _detail;
    [ObservableProperty]
    string _traceId;
}

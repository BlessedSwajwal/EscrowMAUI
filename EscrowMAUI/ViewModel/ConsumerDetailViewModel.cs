using CommunityToolkit.Mvvm.ComponentModel;
using EscrowMAUI.Models.DTOs;
using EscrowMAUI.Services;

namespace EscrowMAUI.ViewModel;

public partial class ConsumerDetailViewModel : ObservableObject
{
    private readonly AuthServices _authServices;
    public ConsumerDetailViewModel(AuthServices authServices)
    {
        UserDetail = new();
        _authServices = authServices;
    }

    public string FullName => UserDetail.FirstName + " " + UserDetail.LastName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    UserDetailResponse _userDetail;

    [ObservableProperty]
    bool _isConsumer = false;

    [ObservableProperty]
    string _ordersCountLabel = "Total Orders: ";


    public async Task OnAppearing()
    {

        var result = await _authServices.GetUserDetail();

        result.Match(
                userDetailRes =>
                {
                    UserDetail = userDetailRes;
                    IsConsumer = (UserDetail.UserType.Equals("CONSUMER"));
                    OrdersCountLabel = IsConsumer ? "Total Orders: " : "Accepted Orders: ";
                    return "";
                },
                problemRes =>
                {
                    //TODO: Error Handling
                    return "";
                }
            );
    }
}

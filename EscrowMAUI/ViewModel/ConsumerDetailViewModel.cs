using CommunityToolkit.Mvvm.ComponentModel;
using EscrowMAUI.Models.DTOs;
using EscrowMAUI.Services;

namespace EscrowMAUI.ViewModel;

public partial class ConsumerDetailViewModel : ObservableObject
{
    private readonly AuthServices _authServices;
    public ConsumerDetailViewModel(AuthServices authServices)
    {
        ConsumerDetail = new();
        _authServices = authServices;
    }

    public string FullName => ConsumerDetail.FirstName + " " + ConsumerDetail.LastName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    ConsumerDetailResponse _consumerDetail;

    public async Task OnAppearing()
    {
        var result = await _authServices.GetConsumerDetail();

        result.Match(
                consumerDetailRes =>
                {
                    ConsumerDetail = consumerDetailRes;
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

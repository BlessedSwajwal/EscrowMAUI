using CommunityToolkit.Maui.Core;
using DevExpress.Maui.Controls;
using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class OrdersPage : ContentPage
{
    private readonly OrdersViewModel _ordersViewModel;
    private readonly IPopupService _popupService;
    public OrdersPage(OrdersViewModel ordersViewModel, IPopupService popupService)
    {
        InitializeComponent();
        _ordersViewModel = ordersViewModel;
        BindingContext = _ordersViewModel;
        _popupService = popupService;
        bottomSheet.AllowedState = BottomSheetAllowedState.All;
    }

    private async void Button_Pressed(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Colors.Gray;
        _popupService.ShowPopup<OrdersViewModel>();
    }

    private void Button_Released(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Color.FromArgb("ac99ea");
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _ordersViewModel.OnAppearing();
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        bottomSheet.State = BottomSheetState.FullExpanded;
    }

    private void bottomSheetSubmit_Clicked(object sender, EventArgs e)
    {
        bottomSheet.State = BottomSheetState.HalfExpanded;
    }
}
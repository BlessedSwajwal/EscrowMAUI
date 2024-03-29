using DevExpress.Maui.Controls;
using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class OrdersPage : ContentPage
{
    private readonly OrdersViewModel _ordersViewModel;
    public OrdersPage(OrdersViewModel ordersViewModel)
    {
        InitializeComponent();
        _ordersViewModel = ordersViewModel;
        BindingContext = _ordersViewModel;
        bottomSheet.AllowedState = BottomSheetAllowedState.All;
        var s = new ShimmerView();

    }

    private async void Button_Pressed(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Colors.Gray;
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
        bottomSheet.Close();
        NameEntry.Text = DescriptionEntry.Text = AllowedDaysEntry.Text = CostEntry.Text = "";
    }
}
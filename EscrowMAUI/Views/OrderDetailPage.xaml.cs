using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;


public partial class OrderDetailPage : ContentPage
{
    private readonly OrderDetailViewModel viewModel;
    private Guid SelectedBidId;
    public OrderDetailPage(OrderDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
        var rowDefinition = new RowDefinition { Height = new GridLength(viewModel.TopGridHeight, GridUnitType.Star) };
        PageGrid.RowDefinitions.Add(rowDefinition);
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.OnAppearing();
    }

    private async void Button_Pressed(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Colors.Gray;
        var parameters = new Dictionary<string, object>
        {
            [nameof(CreateBidViewModel.OrderId)] = ((OrderDetailViewModel)BindingContext).Order.Id
        };
        await Shell.Current.GoToAsync(nameof(CreateBidPage), true, parameters);
    }

    private void Button_Released(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Color.FromArgb("ac99ea");
    }

    private void OpenPopup_Clicked(object sender, EventArgs e)
    {
        Popup.IsOpen = true;
    }

    private void ClosePopupButton_Clicked(object sender, EventArgs e)
    {
        Popup.IsOpen = false;
    }
}
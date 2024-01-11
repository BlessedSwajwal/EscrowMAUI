using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;


public partial class OrderDetailPage : ContentPage
{
    private readonly OrderDetailViewModel viewModel;
    public OrderDetailPage(OrderDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
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
            [nameof(CreateBidViewModel.OrderId)] = Guid.Parse(OrderIdLabel.Text)
        };
        await Shell.Current.GoToAsync(nameof(CreateBidPage), true, parameters);
    }

    private void Button_Released(object sender, EventArgs e)
    {
        ((Button)sender).BackgroundColor = Color.FromArgb("ac99ea");
    }

}
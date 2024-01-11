using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class CreateBidPage : ContentPage
{
    public CreateBidPage(CreateBidViewModel createBidViewModel)
    {
        InitializeComponent();
        BindingContext = createBidViewModel;
    }
}
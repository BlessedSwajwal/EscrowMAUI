using CommunityToolkit.Maui.Views;
using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class CreateBidPopup : Popup
{
    public CreateBidPopup(OrderDetailViewModel orderDetailViewModel)
    {
        InitializeComponent();
        BindingContext = orderDetailViewModel;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
}
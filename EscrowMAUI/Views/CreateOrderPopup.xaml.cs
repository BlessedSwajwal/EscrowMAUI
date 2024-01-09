using CommunityToolkit.Maui.Views;
using EscrowMAUI.ViewModel;

namespace EscrowMAUI.Views;

public partial class CreateOrderPopup : Popup
{
    public CreateOrderPopup(OrdersViewModel ordersViewModel)
    {
        InitializeComponent();
        BindingContext = ordersViewModel;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
}
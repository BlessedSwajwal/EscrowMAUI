using EscrowMAUI.ViewModel;

namespace EscrowMAUI.ProviderViews;

public partial class CreatedOrdersPage : ContentPage
{
    private readonly CreatedOrdersViewModel _createdOrdersViewModel;
    public CreatedOrdersPage(CreatedOrdersViewModel createdOrdersViewModel)
    {
        InitializeComponent();
        BindingContext = createdOrdersViewModel;
        _createdOrdersViewModel = createdOrdersViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _createdOrdersViewModel.OnAppearing();
    }
}
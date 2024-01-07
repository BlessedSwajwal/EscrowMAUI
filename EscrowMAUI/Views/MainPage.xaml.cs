﻿namespace EscrowMAUI.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        btn.BackgroundColor = Colors.DimGray;
    }

}

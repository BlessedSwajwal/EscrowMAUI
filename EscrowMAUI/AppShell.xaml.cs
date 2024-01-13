using EscrowMAUI.ProviderViews;
using EscrowMAUI.Views;

namespace EscrowMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            UpdateTabs();

        }

        public void UpdateTabs()
        {
            MyTab.Items.Clear();
            MyTab = new();
            this.Items.Add(MyTab);

            if (!Preferences.Default.ContainsKey(Constants.Constants.UserType))
            {
                var mainPageShellContent = new ShellContent
                {
                    Title = "",
                    ContentTemplate = new DataTemplate(typeof(MainPage))
                };
                MyTab.Items.Add(mainPageShellContent);
            }
            else
            {
                string usertype = Preferences.Default.Get<string>(Constants.Constants.UserType, "").ToLower();
                ShellContent homepageShellContent = usertype.Equals("consumer") ?
                new ShellContent
                {
                    Title = "Home",
                    Icon = "home",
                    ContentTemplate = new DataTemplate(typeof(OrdersPage))
                } : new ShellContent
                {
                    Title = "Home",
                    Icon = "home",
                    ContentTemplate = new DataTemplate(typeof(ProviderHomePage))
                };

                MyTab.Items.Add(homepageShellContent);

                if (usertype.Equals("provider"))
                {
                    var createdOrdersShellContent = new ShellContent
                    {
                        Title = "Bid",
                        Icon = "bid",
                        ContentTemplate = new DataTemplate(typeof(CreatedOrdersPage))
                    };
                    MyTab.Items.Add(createdOrdersShellContent);
                }

                var userDetailshellContent = new ShellContent
                {
                    Title = "Profile",
                    Icon = "userfilled",
                    ContentTemplate = new DataTemplate(typeof(ConsumerDetailPage))
                };

                MyTab.Items.Add(userDetailshellContent);
            }
        }
    }
}

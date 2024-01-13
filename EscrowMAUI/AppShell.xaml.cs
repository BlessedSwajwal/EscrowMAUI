using EscrowMAUI.ProviderViews;
using EscrowMAUI.Views;

namespace EscrowMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

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

            var userDetailshellContent = new ShellContent
            {
                Title = "Profile",
                Icon = "userfilled",
                ContentTemplate = new DataTemplate(typeof(ConsumerDetailPage))
            };

            var shellContent1 = new ShellContent
            {
                Title = "Orders",
                Icon = "userfilled",
                ContentTemplate = new DataTemplate(typeof(OrdersPage))
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

            MyTab.Items.Add(userDetailshellContent);
        }
    }
}

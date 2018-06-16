using EksiSozluk.CloneUI.Extension;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Views
{
    public class HomePage : TabbedPage
    {
        public bool FixedMode { get; set; }
        public BarThemeTypes BarTheme { get; set; }

        public HomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            Children.Add(new HeadlinesPage());
            Children.Add(new SearchPage());
            Children.Add(new MessagesPage());
            Children.Add(new NotificationsPage());
            Children.Add(new ProfilePage());

            if (Device.RuntimePlatform == "Android")
            {
                FixedMode = true;

                this.BarTheme = BarThemeTypes.Light;
                this.BarTextColor = (Color)Application.Current.Resources["PrimaryDarkColor"];
            }
        }
    }

    public enum BarThemeTypes
    {
        Light,
        DarkWithAlpha,
        DarkWithoutAlpha
    }
}
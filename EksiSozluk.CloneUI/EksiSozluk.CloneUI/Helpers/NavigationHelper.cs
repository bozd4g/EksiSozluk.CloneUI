using System.Threading.Tasks;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Helpers
{
    public class NavigationHelper
    {
        private static readonly INavigation navigation = Application.Current.MainPage.Navigation;

        public static async Task PushAsync(Page page)
        {
            Device.BeginInvokeOnMainThread(async () => await navigation.PushAsync(page));
        }

        public static async Task PushModalAsync(Page page)
        {
            Device.BeginInvokeOnMainThread(async () => await navigation.PushModalAsync(page));
        }

        public static async Task PopAsync()
        {
            Device.BeginInvokeOnMainThread(async () => await navigation.PopAsync());
        }

        public static async Task PopModalAsync()
        {
            Device.BeginInvokeOnMainThread(async () => await navigation.PopModalAsync());
        }

        public static async Task PopToRootAsync()
        {
            Device.BeginInvokeOnMainThread(async () => await navigation.PopToRootAsync());
        }
    }
}

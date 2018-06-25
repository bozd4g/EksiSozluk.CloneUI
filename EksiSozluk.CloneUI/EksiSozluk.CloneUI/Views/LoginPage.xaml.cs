using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EksiSozluk.CloneUI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();

            if(Device.RuntimePlatform == "Android")
                FacebookButton.ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 30);

            else if(Device.RuntimePlatform == "iOS")
                NavigationPage.SetTitleIcon(this, "NavigationBrand.png");
		}
	}
}
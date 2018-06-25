using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EksiSozluk.CloneUI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupPage : ContentPage
	{
		public SignupPage ()
		{
			InitializeComponent ();

		    if (Device.RuntimePlatform == "Android")
		        FacebookButton.ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 30);

		    else if (Device.RuntimePlatform == "iOS")
		        NavigationPage.SetTitleIcon(this, "NavigationBrand.png");

            PasswordInformationLabel.Text = "şifreniz en az " +
		                                    "<b>sekiz karakterli</b> " +
		                                    "olmalı ve içinde " +
		                                    "<b>büyük harf, küçük harf</b> " +
		                                    "ve " +
		                                    "<b>rakam</b> " +
		                                    "barındırmalıdır";
		    AgreementLabel.Text = "<b><font color= \"#53a245\">ekşi sözlük kullanıcı sözleşmesi</font></b>" +
		                          "'ni okudum<br>ve kabul ediyorum.";
		}

	    private async void BirthDayDatePicker_OnFocused(object sender, FocusEventArgs e)
	    {
	        BirthDayEntry.Text = string.Empty;

	        BirthdayDatePicker.IsVisible = true;
            BirthdayDatePicker.Focus();
	        
	        BirthDayEntry.IsVisible = false;

	        if (Device.RuntimePlatform == "iOS")
	        {
	            await Task.Delay(250);
	            BirthdayDatePicker.Focus();
            }
        }
	}
}
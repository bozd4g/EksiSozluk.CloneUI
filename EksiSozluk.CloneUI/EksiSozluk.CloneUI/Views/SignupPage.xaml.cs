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

	    private void BirthDayDatePicker_OnFocused(object sender, FocusEventArgs e)
	    {
	        BirthDayEntry.Text = string.Empty;
	        BirthdayDatePicker.Focus();
	        BirthdayDatePicker.IsVisible = true;

	        BirthDayEntry.IsVisible = false;
	    }
	}
}
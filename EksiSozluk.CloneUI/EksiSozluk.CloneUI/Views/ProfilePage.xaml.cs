using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EksiSozluk.CloneUI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
		}

	    private async void ProfileScrollView_OnScrolled(object sender, ScrolledEventArgs e)
	    {
	        if (e.ScrollY > SegmentsContentView.Y && !StickySegmentsContentView.IsVisible)
	        {
	            StickySegmentsContentView.IsVisible = true;
	            await StickySegmentsContentView.FadeTo(1, 175, Easing.SinInOut);

	            await ProfileNameContentView.FadeTo(1, 100, Easing.SinInOut);
                return;
	        }

            if (e.ScrollY < SegmentsContentView.Y + SegmentsContentView?.Height && StickySegmentsContentView.IsVisible)
	        {
	            await StickySegmentsContentView.FadeTo(0, 175, Easing.SinIn);
	            StickySegmentsContentView.IsVisible = false;

	            await ProfileNameContentView.FadeTo(0, 100, Easing.SinIn);
            }
        }
	}
}
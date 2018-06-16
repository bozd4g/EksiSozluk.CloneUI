using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EksiSozluk.CloneUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeadlinesPage : ContentPage
	{
		public HeadlinesPage ()
		{
			InitializeComponent ();

		}

	    private void CarouselView_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	    {
	        if(e.PropertyName != "Position")
                return;

	        var headline = ViewModel.Menu[ViewModel.CarouselPosition];
            var cell = HorizontalScrollView.Cells[ViewModel.CarouselPosition];
	        object[] array = {headline, cell.View};

            ViewModel.HeadlineSelectedCommand.Execute(array);
	    }
	}
}
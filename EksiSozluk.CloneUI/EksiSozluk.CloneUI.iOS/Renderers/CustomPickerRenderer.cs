using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.iOS.Helper;
using EksiSozluk.CloneUI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace EksiSozluk.CloneUI.iOS.Renderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null && e.NewElement != null)
            {
                this.Control.BorderStyle = UITextBorderStyle.None;

                if (e.NewElement.ItemsSource?.Count != 0)
                {
                    var customPicker = e.NewElement as CustomPicker;
                    customPicker?.HasItemEvent.Invoke(null, null);
                }

                var font = UIFont.FromName(FontHelper.GetFontName(e.NewElement?.FontFamily), (float)e.NewElement?.FontSize);
                if (font != null)
                    this.Control.Font = font;
            }
        }
    }
}
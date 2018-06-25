using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.iOS.Helper;
using EksiSozluk.CloneUI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace EksiSozluk.CloneUI.iOS.Renderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null && e.NewElement != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;

                var font = UIFont.FromName(FontHelper.GetFontName(e.NewElement?.FontFamily), (float)e.NewElement?.FontSize);
                if (font != null)
                    this.Control.Font = font;
            }
        }
    }
}
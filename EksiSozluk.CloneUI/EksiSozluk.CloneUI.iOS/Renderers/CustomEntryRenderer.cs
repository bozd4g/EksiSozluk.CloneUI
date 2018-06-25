using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.iOS.Helper;
using EksiSozluk.CloneUI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace EksiSozluk.CloneUI.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
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
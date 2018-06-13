using Android.Graphics;
using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace EksiSozluk.CloneUI.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer() : base(MainActivity.ApplicationContext)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var nativeEditText = (global::Android.Widget.EditText) Control;
                nativeEditText.Background = null;

                // For Custom Font
                if (e.NewElement is CustomEntry entry && !string.IsNullOrEmpty(entry.FontFamily))
                {
                    Typeface typeface = Typeface.CreateFromAsset(this.Context.Assets, entry.FontFamily);
                    Control.SetTypeface(typeface, TypefaceStyle.Normal);
                }
            }
        }
    }
}
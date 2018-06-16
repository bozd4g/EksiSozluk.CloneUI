using Android.Graphics;
using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace EksiSozluk.CloneUI.Droid.Renderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer() : base(MainActivity.ApplicationContext)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var nativeEditText = Control as Android.Widget.EditText;
                nativeEditText.Background = null;

                if (e.NewElement.ItemsSource?.Count != 0)
                {
                    var customPicker = e.NewElement as CustomPicker;
                    customPicker?.HasItemEvent.Invoke(null, null);
                }

                // For Custom Font
                if (e.NewElement is CustomPicker picker && !string.IsNullOrEmpty(picker.FontFamily))
                {
                    Typeface typeface = Typeface.CreateFromAsset(this.Context.Assets, picker.FontFamily);
                    Control.SetTypeface(typeface, TypefaceStyle.Normal);
                }
            }
        }

    }
}
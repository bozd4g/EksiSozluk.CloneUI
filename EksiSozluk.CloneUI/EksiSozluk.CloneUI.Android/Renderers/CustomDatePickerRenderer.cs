using Android.Graphics;
using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace EksiSozluk.CloneUI.Droid.Renderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer() : base(MainActivity.ApplicationContext)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var nativeEditText = Control as Android.Widget.EditText;
                nativeEditText.Background = null;

                // For Custom Font
                if (e.NewElement is CustomDatePicker datePicker && !string.IsNullOrEmpty(datePicker.FontFamily))
                {
                    Typeface typeface = Typeface.CreateFromAsset(this.Context.Assets, datePicker.FontFamily);
                    Control.SetTypeface(typeface, TypefaceStyle.Normal);
                }
            }
        }

    }
}
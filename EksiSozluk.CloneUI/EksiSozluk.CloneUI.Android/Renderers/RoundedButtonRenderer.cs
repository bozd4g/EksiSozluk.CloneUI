using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedButton), typeof(RoundedButtonRenderer))]
namespace EksiSozluk.CloneUI.Droid.Renderers
{
    public class RoundedButtonRenderer : ButtonRenderer
    {
        private GradientDrawable _normal;

        public RoundedButtonRenderer() : base(MainActivity.ApplicationContext)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var button = (RoundedButton)e.NewElement;

                if (button != null)
                {
                    button.SizeChanged += (s, args) =>
                    {
                        // for corner round
                        var radius = (float)Math.Min(button.Width, button.Height);
                        // Create a drawable for the button's normal state
                        _normal = new GradientDrawable();
                        _normal.SetCornerRadius(radius);
                    };

                    Control.SetAllCaps(false);
                    if (!string.IsNullOrEmpty(button.Image))
                        Control.SetPadding(Control.PaddingLeft * 4, Control.PaddingTop, Control.PaddingRight * 4, Control.PaddingBottom);

                    // For Custom Font
                    if (!string.IsNullOrEmpty(button.FontFamily))
                    {
                        Typeface typeface = Typeface.CreateFromAsset(this.Context.Assets, button.FontFamily);
                        Control.SetTypeface(typeface, TypefaceStyle.Bold);
                    }

                }
            }
        }
    }
}
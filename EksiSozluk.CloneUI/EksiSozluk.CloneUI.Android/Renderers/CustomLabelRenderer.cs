using System;
using Android.Graphics;
using Android.Widget;
using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace EksiSozluk.CloneUI.Droid.Renderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        public CustomLabelRenderer() : base(MainActivity.ApplicationContext)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var view = (CustomLabel) Element;
            var control = Control;

            UpdateUi(view, control);
        }

        void UpdateUi(CustomLabel view, TextView control)
        {
            if (!string.IsNullOrEmpty(view.FontName))
            {
                string filename = view.FontName;
                if (filename.LastIndexOf(".", System.StringComparison.Ordinal) != filename.Length - 4)
                    filename = $"{filename}.ttf";

                control.Typeface = TrySetFont(filename);
            }
            else if (view?.Font != Font.Default)
                control.Typeface = view?.Font.ToTypeface();

            if (view.FontSize > 0)
                control.TextSize = (float) view.FontSize;

            if (view.IsUnderline)
                control.PaintFlags = control.PaintFlags | PaintFlags.UnderlineText;

            if (view.IsStrikeThrough)
                control.PaintFlags = control.PaintFlags | PaintFlags.StrikeThruText;
        }

        private Typeface TrySetFont(string fontName)
        {
            try
            {
                return Typeface.CreateFromAsset(Context.Assets, "fonts/" + fontName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("not found in assets. Exception: {0}", ex);
                try
                {
                    return Typeface.CreateFromFile("fonts/" + fontName);
                }
                catch (Exception ex1)
                {
                    Console.WriteLine("not found by file. Exception: {0}", ex1);

                    return Typeface.Default;
                }
            }
        }
    }
}
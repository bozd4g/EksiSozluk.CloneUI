using System;
using System.Diagnostics;
using System.IO;
using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.iOS.Helper;
using EksiSozluk.CloneUI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedButton), typeof(RoundedButtonRenderer))]
namespace EksiSozluk.CloneUI.iOS.Renderers
{
    public class RoundedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var button = (RoundedButton) e.NewElement;
                if(button == null)
                    return;

                button.SizeChanged += (s, args) =>
                {
                    var radius = Math.Min(button.Width, button.Height) / 2.0;
                    button.BorderRadius = (int) (radius);
                };

                // For Custom Font
                if (!string.IsNullOrEmpty(button.FontFamily))
                    UpdateUi(button);

            }
        }

        private void UpdateUi(RoundedButton view)
        {
            // Prefer font set through Font property.
            if (view.Font == Font.Default)
            {
                if (view.FontSize > 0)
                {
                    this.Control.Font = UIFont.FromName(this.Control.Font.Name, (float)view.FontSize);
                }

                if (!string.IsNullOrEmpty(view.FontFamily))
                {
                    var fontName = Path.GetFileNameWithoutExtension(view.FontFamily);
                    var font = UIFont.FromName(fontName, this.Control.Font.PointSize);
                    if (font != null)
                    {
                        this.Control.Font = font;
                    }
                }

            }
            else
            {
                try
                {
                    var font = UIFont.FromName(FontHelper.GetFontName(view.FontFamily), (float)view.FontSize);
                    if (font != null)
                        this.Control.Font = font;
                }
                catch (Exception ex)
                {
                    var x = ex;
                }
            }
        }
    }
}
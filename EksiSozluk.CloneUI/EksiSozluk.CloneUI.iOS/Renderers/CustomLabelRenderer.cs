using Foundation;
using System;
using System.IO;
using EksiSozluk.CloneUI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.iOS.Helper;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]

namespace EksiSozluk.CloneUI.iOS.Renderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            //UpdateUi(view, this.Control);
            if (e.NewElement is CustomLabel view)
                SetPlaceholder(view);
        }

        /// <summary>
        /// Raises the element property changed event.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">The event arguments</param>
        protected override void OnElementPropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = Element as CustomLabel;

            if (view != null &&
                e.PropertyName == Label.TextProperty.PropertyName ||
                e.PropertyName == Label.FormattedTextProperty.PropertyName ||
                e.PropertyName == CustomLabel.PlaceholderProperty.PropertyName ||
                e.PropertyName == CustomLabel.FormattedPlaceholderProperty.PropertyName ||
                e.PropertyName == CustomLabel.IsDropShadowProperty.PropertyName ||
                e.PropertyName == CustomLabel.IsStrikeThroughProperty.PropertyName ||
                e.PropertyName == CustomLabel.IsUnderlineProperty.PropertyName)
            {
                SetPlaceholder(view);
            }
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        private void UpdateUi(CustomLabel view)
        {
            // Prefer font set through Font property.
            if (view.Font == Font.Default)
            {
                if (view.FontSize > 0)
                {
                    this.Control.Font = UIFont.FromName(this.Control.Font.Name, (float) view.FontSize);
                }

                if (!string.IsNullOrEmpty(view.FontName))
                {
                    var fontName = Path.GetFileNameWithoutExtension(view.FontName);

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
                    var font = UIFont.FromName(FontHelper.GetFontName(view?.FontName), (float)view?.FontSize);
                    if (font != null)
                        this.Control.Font = font;
                }
                catch (Exception ex)
                {
                    var x = ex;
                }
            }

            //Do not create attributed string if it is not necesarry
            //if (!view.IsUnderline && !view.IsStrikeThrough && !view.IsDropShadow)
            //{
            //    return;
            //}

            var underline = view.IsUnderline ? NSUnderlineStyle.Single : NSUnderlineStyle.None;
            var strikethrough = view.IsStrikeThrough ? NSUnderlineStyle.Single : NSUnderlineStyle.None;

            NSShadow dropShadow = null;

            if (view.IsDropShadow)
            {
                dropShadow = new NSShadow
                {
                    ShadowColor = view.DropShadowColor.ToUIColor(),
                    ShadowBlurRadius = 1.4f,
                    ShadowOffset = new CoreGraphics.CGSize(new CoreGraphics.CGPoint(0.3f, 0.8f))
                };
            }

            // For some reason, if we try and convert Color.Default to a UIColor, the resulting color is
            // either white or transparent. The net result is the ExtendedLabel does not display.
            // Only setting the control's TextColor if is not Color.Default will prevent this issue.
            if (view.TextColor != Color.Default)
            {
                this.Control.TextColor = view.TextColor.ToUIColor();
            }

            this.Control.AttributedText = new NSMutableAttributedString(view.Text,
                this.Control.Font,
                underlineStyle: underline,
                strikethroughStyle: strikethrough,
                shadow: dropShadow);
        }

        private void SetPlaceholder(CustomLabel view)
        {
            if (view == null)
            {
                return;
            }

            if (view.FormattedText != null)
            {
                this.Control.AttributedText = view.FormattedText.ToAttributed(view.Font, view.TextColor);
                LayoutSubviews();
                return;
            }

            if (!string.IsNullOrEmpty(view.Text))
            {
                this.UpdateUi(view);
                LayoutSubviews();
                return;
            }

            if (string.IsNullOrWhiteSpace(view.Placeholder) && view.FormattedPlaceholder == null)
            {
                return;
            }

            var formattedPlaceholder = view.FormattedPlaceholder ?? view.Placeholder;

            Control.AttributedText = formattedPlaceholder.ToAttributed(view.Font, view.TextColor);

            LayoutSubviews();
        }
    }
}
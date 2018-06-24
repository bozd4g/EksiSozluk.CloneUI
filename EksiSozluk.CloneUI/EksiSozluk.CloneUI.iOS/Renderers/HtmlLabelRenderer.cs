using CoreGraphics;
using EksiSozluk.CloneUI.Custom;
using Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using EksiSozluk.CloneUI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(HtmlLabel), typeof(HtmlLabelRenderer))]
namespace EksiSozluk.CloneUI.iOS.Renderers
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class HtmlLabelRenderer : LabelRenderer
    {
        private class LinkData
        {
            public LinkData(NSRange range, string url)
            {
                Range = range;
                Url = url;
            }

            public readonly NSRange Range;
            public readonly string Url;
        }

        private void UpdateUi(HtmlLabel view)
        {
            // Prefer font set through Font property.
            if (view?.Font == Font.Default)
            {
                if (view.FontSize > 0)
                {
                    this.Control.Font = UIFont.FromName(this.Control.Font.Name, (float)view.FontSize);
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
                    var font = UIFont.FromName(view.FontFamily, (float)view.FontSize);
                    if (font != null)
                        this.Control.Font = font;
                }
                catch (Exception ex)
                {
                    var x = ex;
                }
            }
        }

        public static void Initialize()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;

            UpdateText();
            UpdateMaxLines();
            UpdateUi(e.NewElement as HtmlLabel);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == HtmlLabel.MaxLinesProperty.PropertyName)
                UpdateMaxLines();
            else if (e.PropertyName == Label.TextProperty.PropertyName ||
                     e.PropertyName == Label.FontAttributesProperty.PropertyName ||
                     e.PropertyName == Label.FontFamilyProperty.PropertyName ||
                     e.PropertyName == Label.FontSizeProperty.PropertyName ||
                     e.PropertyName == Label.HorizontalTextAlignmentProperty.PropertyName ||
                     e.PropertyName == Label.TextColorProperty.PropertyName)
                UpdateText();
        }

        private void UpdateMaxLines()
        {
            var maxLines = HtmlLabel.GetMaxLines(Element);
            if (maxLines == default(int)) return;
            Control.Lines = maxLines;

            SetNeedsDisplay();
        }

        private void UpdateText()
        {
            if (Control == null || Element == null) return;

            if (string.IsNullOrEmpty(Control.Text)) return;

            var helper = new LabelRendererHelper(Element, Control.Text);

            try
            {
                CreateAttributedString(Control, helper.ToString());
                SetNeedsDisplay();
            }
            catch
            {
                // ignored
            }
        }

        private void CreateAttributedString(UILabel control, string html)
        {
            var attr = new NSAttributedStringDocumentAttributes();
            var nsError = new NSError();
            attr.DocumentType = NSDocumentType.HTML;
            var fontDescriptor = control.Font.FontDescriptor.VisibleName;
            var fontFamily = fontDescriptor.ToLower().Contains("system")
                ? "-apple-system,system-ui,BlinkMacSystemFont,Segoe UI"
                : control.Font.FamilyName;
            html += "<style> body{ font-family: " + fontFamily + ";}</style>";
            var myHtmlData = NSData.FromString(html, NSStringEncoding.Unicode);
            var mutable = new NSMutableAttributedString(new NSAttributedString(myHtmlData, attr, ref nsError));
            var links = new List<LinkData>();
            control.AttributedText = mutable;

            mutable.EnumerateAttributes(new NSRange(0, mutable.Length),
                NSAttributedStringEnumeration.LongestEffectiveRangeNotRequired,
                (NSDictionary attrs, NSRange range, ref bool stop) =>
                {
                    foreach (var a in attrs) // should use attrs.ContainsKey(something) instead
                    {
                        if (a.Key.ToString() != "NSLink") continue;
                        links.Add(new LinkData(range, a.Value.ToString()));
                        return;
                    }
                });

            // Sets up a Gesture recognizer:
            if (links.Count <= 0) return;
            control.UserInteractionEnabled = true;
            var tapGesture = new UITapGestureRecognizer((tap) =>
            {
                var url = DetectTappedUrl(tap, (UILabel) tap.View, links);
                if (url == null) return;

                var label = (HtmlLabel) Element;
                var args = new WebNavigatingEventArgs(WebNavigationEvent.NewPage, new UrlWebViewSource {Url = url},
                    url);
                label.SendNavigating(args);

                if (args.Cancel)
                    return;

                Device.OpenUri(new Uri(url));
                label.SendNavigated(args);
            });
            control.AddGestureRecognizer(tapGesture);
        }

        private string DetectTappedUrl(UIGestureRecognizer tap, UILabel label, IEnumerable<LinkData> linkList)
        {
            var layoutManager = new NSLayoutManager();
            var textContainer = new NSTextContainer();
            var textStorage = new NSTextStorage();
            textStorage.SetString(label.AttributedText);

            layoutManager.AddTextContainer(textContainer);
            textStorage.AddLayoutManager(layoutManager);

            textContainer.LineFragmentPadding = 0;
            textContainer.LineBreakMode = label.LineBreakMode;
            textContainer.MaximumNumberOfLines = (nuint) label.Lines;
            var labelSize = label.Bounds.Size;
            textContainer.Size = labelSize;

            var locationOfTouchInLabel = tap.LocationInView(label);
            var textBoundingBox = layoutManager.GetUsedRectForTextContainer(textContainer);
            var textContainerOffset = new CGPoint(
                (labelSize.Width - textBoundingBox.Size.Width) * 0.0 - textBoundingBox.Location.X,
                (labelSize.Height - textBoundingBox.Size.Height) * 0.0 - textBoundingBox.Location.Y);

            nfloat labelX;
            switch (Element.HorizontalTextAlignment)
            {
                case TextAlignment.End:
                    labelX = locationOfTouchInLabel.X - (labelSize.Width - textBoundingBox.Size.Width);
                    break;
                case TextAlignment.Center:
                    labelX = locationOfTouchInLabel.X - (labelSize.Width - textBoundingBox.Size.Width) / 2;
                    break;
                default:
                    labelX = locationOfTouchInLabel.X;
                    break;
            }

            var locationOfTouchInTextContainer = new CGPoint(labelX - textContainerOffset.X,
                locationOfTouchInLabel.Y - textContainerOffset.Y);

            nfloat partialFraction = 0;
            var indexOfCharacter = (nint) layoutManager.CharacterIndexForPoint(locationOfTouchInTextContainer,
                textContainer, ref partialFraction);

            nint scaledIndexOfCharacter = 0;
            if (label.Font.Name == "NeoSans-Light")
            {
                scaledIndexOfCharacter = (nint) (indexOfCharacter * 1.13);
            }

            if (label.Font.Name.StartsWith("HelveticaNeue", StringComparison.InvariantCulture))
            {
                scaledIndexOfCharacter = (nint) (indexOfCharacter * 1.02);
            }

            foreach (var link in linkList)
            {
                var rangeLength = link.Range.Length;
                var tolerance = 0;
                if (label.Font.Name == "NeoSans-Light")
                {
                    rangeLength = (nint) (rangeLength * 1.13);
                    tolerance = 25;
                    indexOfCharacter = scaledIndexOfCharacter;
                }

                if (label.Font.Name.StartsWith("HelveticaNeue", StringComparison.InvariantCulture))
                {
                    if (link.Range.Location > 2000)
                    {
                        indexOfCharacter = scaledIndexOfCharacter;
                    }
                }

                // Xamarin version of NSLocationInRange?
                if ((indexOfCharacter >= (link.Range.Location - tolerance)) &&
                    (indexOfCharacter < (link.Range.Location + rangeLength + tolerance)))
                {
                    return link.Url;
                }
            }

            return null;
        }
    }
}
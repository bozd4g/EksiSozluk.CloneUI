﻿using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;
using Android.Widget;
using EksiSozluk.CloneUI.Custom;
using Java.Lang;
using Org.Xml.Sax;
using System;
using System.ComponentModel;
using System.Linq;
using Android.Graphics;
using EksiSozluk.CloneUI.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Exception = System.Exception;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(HtmlLabelRenderer))]

namespace EksiSozluk.CloneUI.Droid.Renderers
{
    [Preserve(AllMembers = true)]
    public class HtmlLabelRenderer : LabelRenderer
    {
        public HtmlLabelRenderer(Android.Content.Context context) : base(context)
        {
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

            UpdateUi(Element as HtmlLabel, Control);
        }

        void UpdateUi(HtmlLabel view, TextView control)
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
                control.TextSize = (float)view.FontSize;
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
            Control.SetMaxLines(maxLines);
        }

        private void UpdateText()
        {
            if (Control == null || Element == null) return;
            if (string.IsNullOrEmpty(Control.Text)) return;

            // Gets the complete HTML string
            var customHtml = new LabelRendererHelper(Element, Control.Text).ToString();
            // Android's TextView doesn't handle <ul>s, <ol>s and <li>s 
            // so it replaces them with <ulc>, <olc> and <lic> respectively.
            // Those tags will be handles by a custom TagHandler
            customHtml = customHtml.Replace("ul>", "ulc>").Replace("ol>", "olc>").Replace("li>", "lic>");

            Control.SetIncludeFontPadding(false);

            SetTextViewHtml(Control, customHtml);
        }

        private void SetTextViewHtml(TextView text, string html)
        {
            // Tells the TextView that the content is HTML and adds a custom TagHandler
            var sequence = Build.VERSION.SdkInt >= BuildVersionCodes.N
                ? Html.FromHtml(html, FromHtmlOptions.ModeCompact, null, new ListTagHandler())
                :
#pragma warning disable 618
                Html.FromHtml(html, null, new ListTagHandler());
#pragma warning restore 618

            // Makes clickable links
            text.MovementMethod = LinkMovementMethod.Instance;
            var strBuilder = new SpannableStringBuilder(sequence);
            var urls = strBuilder.GetSpans(0, sequence.Length(), Class.FromType(typeof(URLSpan)));
            foreach (var span in urls)
                MakeLinkClickable(strBuilder, (URLSpan) span);

            // Android adds an unnecessary "\n" that must be removed
            var value = RemoveLastChar(strBuilder);

            // Finally sets the value of the TextView 
            text.SetText(value, TextView.BufferType.Spannable);
        }

        private void MakeLinkClickable(ISpannable strBuilder, URLSpan span)
        {
            var start = strBuilder.GetSpanStart(span);
            var end = strBuilder.GetSpanEnd(span);
            var flags = strBuilder.GetSpanFlags(span);
            var clickable = new MyClickableSpan((HtmlLabel) Element, span);
            strBuilder.SetSpan(clickable, start, end, flags);
            strBuilder.RemoveSpan(span);
        }

        private class MyClickableSpan : ClickableSpan
        {
            private readonly HtmlLabel _label;
            private readonly URLSpan _span;

            public MyClickableSpan(HtmlLabel label, URLSpan span)
            {
                _label = label;
                _span = span;
            }

            public override void OnClick(global::Android.Views.View widget)
            {
                var args = new WebNavigatingEventArgs(WebNavigationEvent.NewPage,
                    new UrlWebViewSource {Url = _span.URL}, _span.URL);
                _label.SendNavigating(args);

                if (args.Cancel)
                    return;

                Device.OpenUri(new Uri(_span.URL));
                _label.SendNavigated(args);
            }
        }

        private static ISpanned RemoveLastChar(ICharSequence text)
        {
            var builder = new SpannableStringBuilder(text);
            builder.Delete(text.Length() - 1, text.Length());
            return builder;
        }
    }

    // TagHandler that handles lists (ul, ol)
    internal class ListTagHandler : Java.Lang.Object, Html.ITagHandler
    {
        private bool _first = true;
        private string _parent;
        private int _index = 1;

        public void HandleTag(bool opening, string tag, IEditable output, IXMLReader xmlReader)
        {
            if (tag.Equals("ulc"))
            {
                _parent = "ulc";
                _index = 1;
            }
            else if (tag.Equals("olc"))
            {
                _parent = "olc";
                _index = 1;
            }

            if (!tag.Equals("lic")) return;

            var lastChar = (char) 0;
            if (output.Length() > 0)
            {
                lastChar = output.CharAt(output.Length() - 1);
            }

            if (_parent.Equals("ulc"))
            {
                if (_first)
                {
                    output.Append(lastChar == '\n' ? "\t•  " : "\n\t•  ");
                    _first = false;
                }
                else
                    _first = true;
            }
            else
            {
                if (_first)
                {
                    if (lastChar == '\n')
                        output.Append("\t" + _index + ". ");
                    else
                        output.Append("\n\t" + _index + ". ");
                    _first = false;
                    _index++;
                }
                else
                    _first = true;
            }
        }
    }
}
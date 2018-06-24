﻿using System;
using System.Text;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Custom
{
    public class HtmlLabel : Label
    {
        public static readonly BindableProperty FontNameProperty = BindableProperty.Create(
            nameof(FontName),
            typeof(string),
            typeof(CustomLabel));
        public string FontName
        {
            get => (string)GetValue(FontNameProperty);
            set => SetValue(FontNameProperty, value);
        }

        public static readonly BindableProperty MaxLinesProperty =
            BindableProperty.CreateAttached("MaxLines", typeof(int), typeof(HtmlLabel), default(int));

        public static int GetMaxLines(BindableObject view)
        {
            if (view == null) return default(int);
            return (int) view.GetValue(MaxLinesProperty);
        }

        public static void SetMaxLines(BindableObject view, int value)
        {
            view?.SetValue(MaxLinesProperty, value);
        }

        public void SendNavigating(WebNavigatingEventArgs args)
        {
            Navigating?.Invoke(this, args);
        }

        public void SendNavigated(WebNavigatingEventArgs args)
        {
            Navigated?.Invoke(this, args);
        }

        public event EventHandler<WebNavigatingEventArgs> Navigating;

        public event EventHandler<WebNavigatingEventArgs> Navigated;
    }

    public class LabelRendererHelper
    {
        private readonly Label _label;
        private readonly string _text;
        private readonly StringBuilder _builder;

        public LabelRendererHelper(Label label, string text)
        {
            _label = label;
            _text = text;
            _builder = new StringBuilder();
        }

        private void SetFontAttributes()
        {
            if (_label.FontAttributes == FontAttributes.None) return;
            switch (_label.FontAttributes)
            {
                case FontAttributes.Bold:
                    _builder.Append("font-weight: bold; ");
                    break;
                case FontAttributes.Italic:
                    _builder.Append("font-style: italic; ");
                    break;
            }
        }

        private void SetFontFamily()
        {
            if (_label.FontFamily == null) return;
            _builder.Append($"font-family: '{_label.FontFamily}'; ");
        }

        private void SetFontSize()
        {
            if (Math.Abs(_label.FontSize - 14d) < 0.000000001) return;
            _builder.Append($"font-size: {_label.FontSize}px; ");
        }

        private void SetTextColor()
        {
            if (_label.TextColor.IsDefault) return;
            var color = _label.TextColor;
            var red = (int) (color.R * 255);
            var green = (int) (color.G * 255);
            var blue = (int) (color.B * 255);
            var alpha = (int) (color.A * 255);
            var hex = $"#{red:X2}{green:X2}{blue:X2}{alpha:X2}";
            _builder.Append($"color: {hex}; ");
        }

        private void SetHorizontalTextAlign()
        {
            if (_label.HorizontalTextAlignment == TextAlignment.Start) return;
            switch (_label.HorizontalTextAlignment)
            {
                case TextAlignment.Center:
                    _builder.Append("text-align: center; ");
                    break;
                case TextAlignment.End:
                    _builder.Append("text-align: right; ");
                    break;
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(_label.Text))
                return string.Empty;

            _builder.Append("<div style=\"");
            SetFontAttributes();
            SetFontFamily();
            SetFontSize();
            SetTextColor();
            SetHorizontalTextAlign();
            _builder.Append($"\">{_text}</div>");
            var text = _builder.ToString();
            return text;
        }
    }
}
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Custom
{
    public class CustomLabel : Label
    {
        public static readonly BindableProperty FontNameProperty = BindableProperty.Create(
            nameof(FontName),
            typeof(string),
            typeof(CustomLabel));
        public string FontName
        {
            get => (string) GetValue(FontNameProperty);
            set => SetValue(FontNameProperty, value);
        }

        public static readonly BindableProperty FriendlyFontNameProperty = BindableProperty.Create(
            nameof(FriendlyFontName),
            typeof(string),
            typeof(CustomLabel));
        public string FriendlyFontName
        {
            get => (string) GetValue(FriendlyFontNameProperty);
            set => SetValue(FriendlyFontNameProperty, value);
        }

        public static readonly BindableProperty IsUnderlineProperty = BindableProperty.Create(
            nameof(IsUnderline),
            typeof(bool),
            typeof(CustomLabel),
            false);
        public bool IsUnderline
        {
            get => (bool) GetValue(IsUnderlineProperty);
            set => SetValue(IsUnderlineProperty, value);
        }

       
        public static readonly BindableProperty IsStrikeThroughProperty = BindableProperty.Create(
            nameof(IsStrikeThrough),
            typeof(bool),
            typeof(CustomLabel),
            false);
        public bool IsStrikeThrough
        {
            get => (bool) GetValue(IsStrikeThroughProperty);
            set => SetValue(IsStrikeThroughProperty, value);
        }

        public static readonly BindableProperty IsDropShadowProperty = BindableProperty.Create(
            nameof(IsDropShadow),
            typeof(bool),
            typeof(CustomLabel),
            false);
        public bool IsDropShadow
        {
            get => (bool) GetValue(IsDropShadowProperty);
            set => SetValue(IsDropShadowProperty, value);
        }

        public static readonly BindableProperty DropShadowColorProperty = BindableProperty.Create(
            nameof(DropShadowColor),
            typeof(Color),
            typeof(CustomLabel),
            Color.Transparent);
        public Color DropShadowColor
        {
            get => (Color) GetValue(DropShadowColorProperty);
            set => SetValue(DropShadowColorProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(CustomLabel));
        public string Placeholder
        {
            get => (string) GetValue(PlaceholderProperty);
            set
            {
                SetValue(FormattedPlaceholderProperty, null);
                SetValue(PlaceholderProperty, value);
            }
        }

        public static readonly BindableProperty FormattedPlaceholderProperty = BindableProperty.Create(
            nameof(FormattedPlaceholder),
            typeof(FormattedString),
            typeof(CustomLabel));
        public FormattedString FormattedPlaceholder
        {
            get => (FormattedString) GetValue(FormattedPlaceholderProperty);
            set
            {
                SetValue(PlaceholderProperty, null);
                SetValue(FormattedPlaceholderProperty, value);
            }
        }
    }
}
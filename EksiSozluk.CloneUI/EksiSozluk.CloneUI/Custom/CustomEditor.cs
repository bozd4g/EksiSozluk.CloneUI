using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Custom
{
    public class CustomEditor : Editor
    {
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(CustomEditor), string.Empty);

        public string Placeholder
        {
            get => (string) GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public void InvalidateLayout()
        {
            InvalidateMeasure();
        }
    }
}
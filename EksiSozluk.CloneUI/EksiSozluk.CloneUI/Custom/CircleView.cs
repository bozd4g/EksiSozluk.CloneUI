using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Custom
{
    public class CircleView : BoxView
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius), 
            typeof(double), 
            typeof(CircleView), 
            0.0);

        public double CornerRadius
        {
            get => (double) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}
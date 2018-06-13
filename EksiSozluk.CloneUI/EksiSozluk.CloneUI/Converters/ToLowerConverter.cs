using System;
using System.Globalization;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Converters
{
    public class ToLowerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
                return str.ToLower();
            return "error!";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
                return str.ToLower();
            return "error!";
        }
    }
}
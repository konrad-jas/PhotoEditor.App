using System;
using System.Globalization;
using Xamarin.Forms;

namespace PhotoEditor.Converters
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int) value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int res;
            int.TryParse((string) value, out res);
            return res;
        }
    }
}

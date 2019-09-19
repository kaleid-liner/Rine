using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;

namespace RineClient.Converters
{
    public class BadgeNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int number = (int)value;
            if (number < 99)
            {
                return XamlBindingHelper.ConvertValue(targetType, value);
            }
            else
            {
                return "99+";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

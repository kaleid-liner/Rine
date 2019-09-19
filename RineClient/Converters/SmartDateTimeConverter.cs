using System;
using Windows.UI.Xaml.Data;

namespace RineClient.Converters
{
    public class SmartDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime time = (DateTime)value;
            DateTime now = DateTime.Now;

            if (time.Date == now.Date)
            {
                return time.ToString("HH:mm");
            }
            else if (time.Date.AddDays(1) == now.Date)
            {
                return "Yesterday";
            }
            else if (time.Year == now.Year)
            {
                return time.ToString("MM-dd");
            }
            else
            {
                return time.ToString("yyyy-MM-dd");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

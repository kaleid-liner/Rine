﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Collections.ObjectModel;
using Rine.ServiceContracts;

namespace RineClient
{
    class MessagesLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string message = (value as ObservableCollection<Message>).LastOrDefault()?.Content;
            message.Replace('\n', ' ');
            if (message != null)
                return message.Length > 10 ? message.Substring(0, 10) + "..." : message;
            else return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

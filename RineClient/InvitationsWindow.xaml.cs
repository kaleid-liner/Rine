using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Rine.ServiceContracts;

namespace RineClient
{
    /// <summary>
    /// InvitationsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InvitationsWindow : Window
    {
        public InvitationsWindow()
        {
            InitializeComponent();
        }
    }

    public class ResponseParameters
    {
        public int Uid { get; set; }

        public bool ConsentOrDecline { get; set; }
    }

    public class ParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new ResponseParameters
            {
                Uid = (values[0] as FriendInfo).Uid,
                ConsentOrDecline = (string)values[1] == "同意"
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RineClient
{
    class MessageStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            string styleName;
            if ((item as Message).IsSelf)
            {
                styleName = "MyMessageStyle";
            }
            else
            {
                styleName = "OtherMessageStyle";
            }
            return Application.Current.FindResource(styleName) as Style;
        }
    }
}

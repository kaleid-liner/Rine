using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RineClient
{
    class MessageTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            string templateName;
            if ((item as Message).IsSelf)
            {
                templateName = "MyMessageTemplate";
            }
            else
            {
                templateName = "OtherMessageTemplate";
            }
            return Application.Current.FindResource(templateName) as DataTemplate;
        }
    }
}

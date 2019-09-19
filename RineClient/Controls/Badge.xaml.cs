using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace RineClient.Controls
{
    public sealed partial class Badge : UserControl
    {
        public Badge()
        {
            this.InitializeComponent();
        }

        public BadgeStyle BadgeStyle
        {
            get { return (BadgeStyle)GetValue(BadgeStyleProperty); }
            set { SetValue(BadgeStyleProperty, value); }
        }

        public static readonly DependencyProperty BadgeStyleProperty =
            DependencyProperty.Register("BadgeStyle", typeof(BadgeStyle), typeof(Badge), new PropertyMetadata(BadgeStyle.Info));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Badge), new PropertyMetadata(null));





    }

    public enum BadgeStyle
    {
        Success,
        Danger,
        Warning,
        Info,
        Accent,
    }

    
}

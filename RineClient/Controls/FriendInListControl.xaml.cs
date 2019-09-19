using RineClient.Models;
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
    public sealed partial class FriendInListControl : UserControl
    {
        public FriendInListControl()
        {
            this.InitializeComponent();
        }

        public Friend Friend
        {
            get { return (Friend)GetValue(FriendProperty); }
            set { SetValue(FriendProperty, value); }
        }

        public static readonly DependencyProperty FriendProperty =
            DependencyProperty.Register("Friend", typeof(Friend), typeof(FriendInListControl), new PropertyMetadata(null));



    }
}

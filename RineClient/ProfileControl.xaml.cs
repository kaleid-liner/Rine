using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Rine.ServiceContracts;
using System.Collections.ObjectModel;

namespace RineClient
{
    /// <summary>
    /// ProfileControl.xaml 的交互逻辑
    /// </summary>
    public partial class ProfileControl : UserControl
    {
        public ProfileControl()
        {
            InitializeComponent();
        }

        public bool Online
        {
            get { return (bool)GetValue(OnlineProperty); }
            set { SetValue(OnlineProperty, value); }
        }

        public static readonly DependencyProperty OnlineProperty =
            DependencyProperty.Register("OnlineProperty", typeof(bool), typeof(ProfileControl), new PropertyMetadata(false, OnOnlinePropertyChanged));

        private static void OnOnlinePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            var control = source as ProfileControl;
            string imagePath = control.Online ? @".\Images\online_portrait.png" : @".\Images\offline_portrait.png";
            control.Portrait.Source = new BitmapImage(new Uri(imagePath));
        }

        public int RineId
        {
            get { return (int)GetValue(RineIdProperty); }
            set { SetValue(RineIdProperty, value); }
        }

        public static readonly DependencyProperty RineIdProperty =
            DependencyProperty.Register("RineId", typeof(int), typeof(ProfileControl), new PropertyMetadata(0));

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(ProfileControl), new PropertyMetadata(""));

        public ObservableCollection<MessageInfo> Messages
        {
            get { return (ObservableCollection<MessageInfo>)GetValue(MessagesProperty); }
            set { SetValue(MessagesProperty, value); }
        }

        public static readonly DependencyProperty MessagesProperty =
            DependencyProperty.Register("Messages", typeof(ObservableCollection<MessageInfo>), typeof(ProfileControl));
    }
}

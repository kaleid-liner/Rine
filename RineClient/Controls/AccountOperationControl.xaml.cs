using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RineClient.Models;
using RineClient.Common;
using System.Windows.Input;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace RineClient.Controls
{
    public sealed partial class AccountOperationControl : UserControl
    {
        public AccountOperationControl()
        {
            this.InitializeComponent();
        }

        public RineUser User
        { 
            get { return (RineUser)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(RineUser), typeof(AccountOperationControl), new PropertyMetadata(null));

        public ICommand ItemClickCommand
        {
            get { return (ICommand)GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ItemClickCommandProperty =
            DependencyProperty.Register("ItemClickCommand", typeof(ICommand), typeof(NamePasswordControl), new PropertyMetadata(null));

        public static string Version => RineSettings.Version;

        public static string AppName => RineSettings.AppName;

        public ObservableCollection<NavLink> NavLinks { get; set; } = new ObservableCollection<NavLink>
        {
            new NavLink { Label = "New Friend", IconGlyph = "\uE8FA" },
            new NavLink { Label = "Settings", IconGlyph = "\uE713" },
            new NavLink { Label = "Switch Account", IconGlyph = "\uE748" }
        };

    }

    public class NavLink
    {
        public string Label { get; set; }
        public string IconGlyph { get; set; }
    }
}

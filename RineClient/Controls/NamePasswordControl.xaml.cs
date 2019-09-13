using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace RineClient.Controls
{
    public sealed partial class NamePasswordControl : UserControl
    {
        public NamePasswordControl()
        {
            this.InitializeComponent();
        }

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(NamePasswordControl), new PropertyMetadata(null));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(NamePasswordControl), new PropertyMetadata(null));

        public bool IsAutoLogin
        {
            get { return (bool)GetValue(IsAutoLoginProperty); }
            set { SetValue(IsAutoLoginProperty, value); }
        }

        public static readonly DependencyProperty IsAutoLoginProperty =
            DependencyProperty.Register("IsAutoLogin", typeof(bool), typeof(NamePasswordControl), new PropertyMetadata(false));

        public bool IsRememberPswd
        {
            get { return (bool)GetValue(IsRememberPswdProperty); }
            set { SetValue(IsRememberPswdProperty, value); }
        }

        public static readonly DependencyProperty IsRememberPswdProperty =
            DependencyProperty.Register("IsRememberPswd", typeof(bool), typeof(NamePasswordControl), new PropertyMetadata(false));

        public IEnumerable<string> SavedUsers
        {
            get { return (IEnumerable<string>)GetValue(SavedUsersProperty); }
            set { SetValue(SavedUsersProperty, value); }
        }

        public static readonly DependencyProperty SavedUsersProperty =
            DependencyProperty.Register("SavedUsers", typeof(IEnumerable<string>), typeof(NamePasswordControl), new PropertyMetadata(null));

        public ICommand LoginCommand
        {
            get { return (ICommand)GetValue(LoginCommandProperty); }
            set { SetValue(LoginCommandProperty, value); }
        }

        public static readonly DependencyProperty LoginCommandProperty =
            DependencyProperty.Register("LoginCommand", typeof(ICommand), typeof(NamePasswordControl), new PropertyMetadata(null));

        public ICommand NameSelectionChanged
        {
            get { return (ICommand)GetValue(NameSelectionChangedProperty); }
            set { SetValue(NameSelectionChangedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NameSelectionChanged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameSelectionChangedProperty =
            DependencyProperty.Register("NameSelectionChanged", typeof(ICommand), typeof(NamePasswordControl), new PropertyMetadata(null));
    }
}

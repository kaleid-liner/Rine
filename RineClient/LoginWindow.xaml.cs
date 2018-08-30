using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ServiceModel;
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
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly EndpointAddress address;
        public LoginWindow()
        {
            InitializeComponent();
            address = new EndpointAddress("net.tcp://localhost:12001/Design_Time_Addresses/RineServiceLibrary/RineService/");
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(RineBox.Text, out int uid))
                {
                    RineViewModel rineViewModel = RineViewModel.Create(uid);
                    IRineService client = new DuplexChannelFactory<IRineService>
                        (new InstanceContext(rineViewModel), new NetTcpBinding(), address).CreateChannel();
                    UserInfo user = new UserInfo
                    {
                        Uid = uid,
                        Password = PasswordBox.Password
                    };
                    string loginResult = client.LogIn(user);
                    if (loginResult.Contains("登陆成功"))
                    {
                        rineViewModel.UserName = loginResult.Substring(4);
                        rineViewModel.SetServiceChannel(client);
                        RineMainWindow rine = new RineMainWindow(rineViewModel);
                        rine.Show();
                        this.Close();
                    }
                    else
                    {
                        RineBox.Text = "";
                        Hint.Content = loginResult;
                        PasswordBox.Password = "";
                    }
                }
                else
                {
                    RineBox.Text = "";
                    Hint.Content = "请输入正确的Rine号";
                    PasswordBox.Password = "";
                }
            }
            catch (TimeoutException timeout)
            {
                MessageBox.Show(timeout.Message + "\n" + "请检查你的网络状态!");
                this.Close();
            }
        }

        private void ReturnToLogin_Click(object sender, RoutedEventArgs e)
        {
            ReturnToLogin();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            IdLabel.Content = "用户名";
            Hint.Content = "请输入任意你偏好的用户名";
            RineBox.Text = "";
            PasswordBox.Password = "";
            RegisterButton.Content = "注册";
            (sender as Button).Click -= RegisterButton_Click;
            (sender as Button).Click += VerifyRegister_Click;
            LoginButton.Content = "前往登陆";
            LoginButton.IsEnabled = true;
            LoginButton.Click -= LoginButton_Click;
            LoginButton.Click += ReturnToLogin_Click;

        }

        private void VerifyRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(RineBox.Text))
            {
                RineBox.Text = "";
                PasswordBox.Password = "";
                Hint.Content = "用户名不能为空";
            }
            if (PasswordBox.Password.Length < 6)
            {
                RineBox.Text = "";
                PasswordBox.Password = "";
                Hint.Content = "密码至少有6个字符";
            }
            try
            {
                IRineService client = new DuplexChannelFactory<IRineService>(new InstanceContext(new RineViewModel()), new NetTcpBinding(), address).CreateChannel();
                UserInfo user = new UserInfo
                {
                    UserName = RineBox.Text,
                    Password = PasswordBox.Password
                };
                int uid = client.Register(user);
                MessageBox.Show($"注册成功！你的Rine号是{uid}");
                ReturnToLogin();
            }
            catch (TimeoutException timeout)
            {
                MessageBox.Show(timeout.Message + "\n" + "请检查你的网络状态!");
                this.Close();
            }
        }

        private void ReturnToLogin()
        {
            IdLabel.Content = "Rine号";
            Hint.Content = "请输入Rine号";
            RineBox.Text = "";
            PasswordBox.Password = "";
            RegisterButton.Content = "前往注册";
            RegisterButton.Click -= VerifyRegister_Click;
            RegisterButton.Click += RegisterButton_Click;
        }

        private void RineBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(RineBox.Text) && !String.IsNullOrEmpty(PasswordBox.Password))
            {
                LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(RineBox.Text) && !String.IsNullOrEmpty(PasswordBox.Password))
            {
                LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }
    }
}

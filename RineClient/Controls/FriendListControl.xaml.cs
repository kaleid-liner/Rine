using RineClient.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace RineClient.Controls
{
    public sealed partial class FriendListControl : UserControl
    {
        public FriendListControl()
        {
            this.InitializeComponent();
        }

        public FriendListViewModel ViewModel
        {
            get { return (FriendListViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(FriendListViewModel), typeof(FriendListControl), new PropertyMetadata(null));


    }
}

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace RineClient.Controls
{
    public sealed partial class MessageInputControl : UserControl
    {
        public MessageInputControl()
        {
            this.InitializeComponent();
        }

        public string MessageContent
        {
            get { return (string)GetValue(MessageContentProperty); }
            set { SetValue(MessageContentProperty, value); }
        }

        public static readonly DependencyProperty MessageContentProperty =
            DependencyProperty.Register("MessageContent", typeof(string), typeof(MessageInputControl), new PropertyMetadata(null));

        public ICommand SendCommand
        {
            get { return (ICommand)GetValue(SendCommandProperty); }
            set { SetValue(SendCommandProperty, value); }
        }

        public static readonly DependencyProperty SendCommandProperty =
            DependencyProperty.Register("SendCommand", typeof(ICommand), typeof(MessageInputControl), new PropertyMetadata(null));

        public ICommand AttachCommand
        {
            get { return (ICommand)GetValue(AttachCommandProperty); }
            set { SetValue(AttachCommandProperty, value); }
        }

        public static readonly DependencyProperty AttachCommandProperty =
            DependencyProperty.Register("AttachCommand", typeof(ICommand), typeof(MessageInputControl), new PropertyMetadata(null));

    }
}

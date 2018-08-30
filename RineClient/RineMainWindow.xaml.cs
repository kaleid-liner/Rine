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
using System.Windows.Shapes;
using Rine.ServiceContracts;
using System.ServiceModel;

namespace RineClient
{
    /// <summary>
    /// RineMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RineMainWindow : Window
    {

        public RineMainWindow(RineViewModel rineViewModel)
        {
            InitializeComponent();
            DataContext = rineViewModel;
            this.Closing += rineViewModel.OnRineMainWindowClosing;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        private void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == RineViewModel.RemoveFriendCommand)
            {
                e.CanExecute = !FriendListBox.Items.IsEmpty;
            }
            if (e.Command == RineViewModel.AddFriendCommand)
            {
                e.CanExecute = true;
            }
            if (e.Command == RineViewModel.CheckInvitationsCommand)
            {
                e.CanExecute = true;
            }
            e.Handled = true;
        }

        private void CommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == RineViewModel.CheckInvitationsCommand)
            {
                var dialog = new InvitationsWindow
                {
                    DataContext = DataContext
                };
                dialog.ShowDialog();
            }
            if (e.Command == RineViewModel.RemoveFriendCommand)
            {
                var dialog = new RemoveFriendWindow
                {
                    DataContext = DataContext
                };
                dialog.ShowDialog();
            }
            if (e.Command == RineViewModel.AddFriendCommand)
            {
                var dialog = new AddFriendWindow
                {
                    DataContext = DataContext
                };
                dialog.ShowDialog();
            }
            e.Handled = true;
        }
    }
}   

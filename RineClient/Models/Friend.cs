using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml.Media;

namespace RineClient.Models
{
    public class Friend : INotifyPropertyChanged
    {
        public string UserName { get; set; }
        public ImageSource Avatar { get; set; }
        public ObservableCollection<Message> Messages { get; set; }

        public Message LatestMessage => Messages.Last();

        public bool AnyUnread => Messages.Any(m => !m.Read);

        public int UnreadCount => Messages.Count(m => !m.Read);

        public DateTime LatestTime => LatestMessage.Sent;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}

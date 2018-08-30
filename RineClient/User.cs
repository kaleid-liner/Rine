using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace RineClient
{
    public class User : INotifyPropertyChanged
    {
        private bool _online;
        public bool Online
        {
            get => _online;
            set
            {
                _online = value;
                OnPropertyChanged(nameof(Online));
            }
        }

        public string UserName { get; set; }

        public int RineID { get; set; }

        private ObservableCollection<Message> _messages;

        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

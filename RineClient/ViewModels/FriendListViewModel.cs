using System.ComponentModel;
using System.Threading.Tasks;
using RineClient.Common;
using RineClient.Models;

namespace RineClient.ViewModels
{
    public class FriendListViewModel : INotifyPropertyChanged
    {
        #region field
        private TrueObservableCollection<Friend> _friends;
        #endregion

        #region property
        public TrueObservableCollection<Friend> Friends
        {
            get => _friends;
            set
            {
                _friends = value;
                OnPropertyChanged(nameof(Friends));
            }
        }
        #endregion

        public Task LoadAsync(MainArgs args)
        {
            Friends = new TrueObservableCollection<Friend>(args.Friends);

            return Task.CompletedTask;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}

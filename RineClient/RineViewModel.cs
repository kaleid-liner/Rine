using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Rine.ServiceContracts;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Windows;
using System.ServiceModel;


namespace RineClient
{
    [XmlRoot("UserData")]
    public class RineViewModel : IRineCallBack, INotifyPropertyChanged
    {
        private IRineService _client;

        public event PropertyChangedEventHandler PropertyChanged;

        [XmlIgnore]
        public ICommand SendCommand
        {
            get => new RelayCommand(SendExecute, SendCanExecute);
        }

        [XmlIgnore]
        public static RoutedCommand LogInCommand = new RoutedCommand("Log in", typeof(RineViewModel));

        [XmlIgnore]
        public static RoutedCommand LogOutCommand = new RoutedCommand("Log out", typeof(RineViewModel));

        [XmlIgnore]
        public static RoutedCommand AddFriendCommand = new RoutedCommand("Add friend", typeof(RineViewModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.N, ModifierKeys.Control)}));

        [XmlIgnore]
        public static RoutedCommand RemoveFriendCommand = new RoutedCommand("Remove friend", typeof(RineViewModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.R, ModifierKeys.Control) }));

        [XmlIgnore]
        public static RoutedCommand CheckInvitationsCommand = new RoutedCommand("Check invitations", typeof(RineViewModel));

        [XmlIgnore]
        public ICommand VerifyAddCommand
        {
            get => new RelayCommand<string>(AddExecute, AddCanExecute);
        }

        [XmlIgnore]
        public ICommand ResponseCommand
        {
            get => new RelayCommand<ResponseParameters>(ResponseExecute, ResponseCanExecute);
        }

        [XmlIgnore]
        public ICommand VerifyRemoveCommand
        {
            get => new RelayCommand<User>(RemoveExecute, RemoveCanExecute);
        }


        private void SendExecute()
        {
            if (string.IsNullOrEmpty(SendBuffer))
                return;
            try
            {
                _client.Send(new MessageInfo
                {
                    Content = SendBuffer,
                    DstUid = CurrentFriend.RineID
                });
                CurrentFriend.Messages.Add(new Message
                {
                    Content = SendBuffer,
                    IsSelf = true,
                    Time = DateTime.Now,
                    UserName = this.UserName
                });
            }
            catch (TimeoutException timeout)
            {
                MessageBox.Show(timeout.Message + "\n" + "请检查你的网络状态!");
            }
            finally
            {
                SendBuffer = "";
            }
        }

        private bool SendCanExecute()
        {
            return CurrentFriend != null;
        }

        private void AddExecute(string uid)
        {
            int realUid = int.Parse(uid);
            _client.AddFriend(new FriendInfo
            {
                Uid = realUid
            });
        }

        private bool AddCanExecute(string uid)
        {
            if (int.TryParse(uid, out int realUid))
                return true;
            return false;
        }

        private void DoLogOut()
        {
            LastLogOut = DateTime.Now;
            Save();
            _client.LogOut();
        }

        private void RemoveExecute(User friend)
        {
            FriendList.Remove(friend);
        }
        private bool RemoveCanExecute(User friend)
        {
            return friend != null;
        }

        private void ResponseExecute(ResponseParameters parameters)
        {
            FriendInfo invitation = Invitations.First(i => i.Uid == parameters.Uid);
            Invitations.Remove(invitation);
            _client.ResponseInvitation(parameters.ConsentOrDecline, parameters.Uid);
            if (parameters.ConsentOrDecline == true)
            {
                if (!FriendList.Any(f => f.RineID == parameters.Uid))
                {
                    FriendList.Add(new User
                    {
                        RineID = invitation.Uid,
                        UserName = invitation.UserName,
                        Messages = new ObservableCollection<Message>(),
                        Online = false
                    });
                }
            }
        }

        private bool ResponseCanExecute(ResponseParameters parameters)
        {
            return parameters.Uid != -1;
        }

        private void OnPropertyChanged(string propertyName) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private User _currentFriend;

        [XmlIgnore]
        public User CurrentFriend
        {
            get => _currentFriend;
            set
            {
                _currentFriend = value;
                OnPropertyChanged(nameof(CurrentFriend));
            }
        }

        public DateTime LastLogOut { get; set; }

        [XmlIgnore]
        public ObservableCollection<FriendInfo> Invitations { get; set; }

        public int Uid { get; set; }

        public string UserName { get; set; }

        private string _sendBuffer;
        public string SendBuffer
        {
            get => _sendBuffer;
            set
            {
                _sendBuffer = value;
                OnPropertyChanged(nameof(SendBuffer));
            }
        }

        public ObservableFriendCollection FriendList { get; set; }

        public void AddFriendNotify(FriendInfo inviter)
        {
            Invitations.Add(inviter);
        }

        public async void AddFriendSuccess(FriendInfo friend)
        {
            if (!FriendList.Any(f => f.RineID == friend.Uid))
            {
                FriendList.Add(new User
                {
                    Messages = new ObservableCollection<Message>(),
                    Online = await Task.Run(() => _client.GetFriendStatus(friend.Uid)),
                    RineID = friend.Uid,
                    UserName = friend.UserName
                });
            }
        }

        public void LogInNotify(int uid)
        {
            User friend = FriendList.FirstOrDefault(f => f.RineID == uid);
            if (friend != null)
                friend.Online = true;
        }

        public void LogOutNotify(int uid)
        {
            User friend = FriendList.FirstOrDefault(f => f.RineID == uid);
            if (friend != null)
                friend.Online = false;
        }

        public void ReceiveChat(MessageInfo message)
        {
            User friend = FriendList.FirstOrDefault(f => f.RineID == message.SrcUid);
            if (friend != null)
            {
                friend.Messages.Add(new Message
                {
                    Content = message.Content,
                    IsSelf = false,
                    Time = message.Time,
                    UserName = friend.UserName
                });
            }
        }

        public static RineViewModel Create(int uid)
        {
            RineViewModel model;
            string filename = @"..\..\Data\" + uid + ".xml";
            if (File.Exists(filename))
            {
                using (var stream = File.OpenRead(filename))
                {
                    var serializer = new XmlSerializer(typeof(RineViewModel));
                    model = serializer.Deserialize(stream) as RineViewModel;
                }
            }
            else
            {
                model = new RineViewModel();
            }
            model.Uid = uid;
            model.Invitations = new ObservableCollection<FriendInfo>();
            return model;
        }

        public void SetServiceChannel(IRineService client)
        {
            _client = client;
            _client.GetInvitations();
            _client.GetFriends();
            _client.Receive(LastLogOut);
            foreach (User friend in FriendList)
            {
                friend.Online = _client.GetFriendStatus(friend.RineID);
            }
        }

        public void Save()
        {
            using (var stream = File.Open(@"..\..\Data\" + Uid + ".xml", FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(RineViewModel));
                serializer.Serialize(stream, this);
            }
        }

        public void OnRineMainWindowClosing(object sender, CancelEventArgs args)
        {
            DoLogOut();
        }
    }
}

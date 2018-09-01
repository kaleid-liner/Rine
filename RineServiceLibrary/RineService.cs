using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Rine.ServiceContracts;
using Rine.DataLibrary;
using System.Security.Cryptography;
using System.Data;
using System.Data.Entity;

namespace Rine.ServiceLibrary
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class RineService : IRineService
    {
        private static Dictionary<int, OperationContext> usersOnline;
        private User _user;

        public RineService()
        {
            usersOnline = new Dictionary<int, OperationContext>();
        }

        [OperationBehavior]
        public string LogIn(UserInfo user)
        {
            using (MessageContext messageDB = new MessageContext())
            {
                messageDB.Database.Connection.Open();
                _user = messageDB.Users.Include(u => u.Invitations).Include(u => u.FriendList)
                    .Include(u => u.ChatLogs).Where(u => u.Uid == user.Uid).FirstOrDefault();
                if (_user == null)
                {
                    OperationContext.Current.OperationCompleted += LogIn_Failed;
                    return "此用户不存在";
                }
                if (CryptoPassword(user.Password) != _user.Password)
                {
                    OperationContext.Current.OperationCompleted += LogIn_Failed;
                    return "密码错误";
                }
                if (usersOnline.ContainsKey(user.Uid))
                {
                    OperationContext.Current.OperationCompleted += LogIn_Failed;
                    return "此用户已登陆";
                }
                usersOnline.Add(_user.Uid, OperationContext.Current);
                OperationContext.Current.Channel.Closed += Channel_Closed;
                OperationContext.Current.Channel.Faulted += Channel_Faulted;
                foreach (var friend in usersOnline.Where(u => _user.FriendList.Any(f => f.Uid == u.Key)))
                {
                    friend.Value.GetCallbackChannel<IRineCallBack>()?.LogInNotify(_user.Uid);
                }
                return "登陆成功" + _user.UserName;
            }
        }

        private void LogIn_Failed(object sender, EventArgs e)
        {
            (sender as OperationContext).Channel.Close();
        }

        private void Channel_Faulted(object sender, EventArgs e)
        {
            DoLogOut();
        }

        private void Channel_Closed(object sender, EventArgs e)
        {
            DoLogOut();
        }

        [OperationBehavior]
        public void LogOut()
        {
            DoLogOut();
        }

        private void DoLogOut()
        {
            if (usersOnline.ContainsKey(_user.Uid))
            {
                usersOnline.Remove(_user.Uid);
                foreach (var friend in usersOnline.Where(u => _user.FriendList.Any(f => f.Uid == u.Key)))
                {
                    friend.Value.GetCallbackChannel<IRineCallBack>()?.LogOutNotify(_user.Uid);
                }
            }
        }

        [OperationBehavior]
        public void Receive(DateTime time)
        {
            IRineCallBack channel = OperationContext.Current.GetCallbackChannel<IRineCallBack>();
            foreach (Message message in _user.ChatLogs.Where(m => m.Time > time))
            {
                channel.ReceiveChat(new MessageInfo
                {
                    Content = message.Content,
                    SrcUid = message.SrcUid
                });
            }
        }

        [OperationBehavior]
        public int Register(UserInfo user)
        {
            using (MessageContext messageDB = new MessageContext())
            {
                _user = new User
                {
                    ChatLogs = new List<Message>(),
                    FriendList = new List<User>(),
                    Password = CryptoPassword(user.Password),
                    UserName = user.UserName,
                    Invitations = new List<User>()
                };
                messageDB.Users.Add(_user);
                messageDB.SaveChanges();
            }
            return _user.Uid;
        }

        [OperationBehavior]
        public void Send(MessageInfo message)
        {
            using (MessageContext messageDB = new MessageContext())
            {
                messageDB.Database.Connection.Open();
                Message _message = new Message
                {
                    Content = message.Content,
                    DstUid = message.DstUid,
                    SrcUid = _user.Uid,
                    Time = DateTime.Now,
                };
                messageDB.Messages.Add(_message);
                messageDB.Users.Find(message.DstUid).ChatLogs.Add(_message);
                messageDB.SaveChanges();
            }
            if (usersOnline.TryGetValue(message.DstUid, out OperationContext operation))
            {
                operation.GetCallbackChannel<IRineCallBack>().ReceiveChat(message);
            }
        }

        [OperationBehavior]
        public void AddFriend(FriendInfo friend)
        {
            using (var messageDB = new MessageContext())
            {
                messageDB.Users.Attach(_user);
                messageDB.Users.Find(friend.Uid).Invitations.Add(_user);
                messageDB.SaveChanges();
                if (usersOnline.TryGetValue(friend.Uid, out OperationContext operation))
                {
                    operation.GetCallbackChannel<IRineCallBack>().AddFriendNotify(new FriendInfo
                    {
                        Uid = _user.Uid,
                        UserName = _user.UserName
                    });
                }
            }
        }

        [OperationBehavior]
        public void RemoveFriend(int uid)
        {
            using (var messageDB = new MessageContext())
            {
                messageDB.Users.Attach(_user);
                _user.FriendList.RemoveAll(f => f.Uid == uid);
                messageDB.SaveChanges();
            }
        }
        
        [OperationBehavior]
        public void ResponseInvitation(bool consentOrDecline, int srcUid)
        {
            using (var messageDB = new MessageContext())
            {
                messageDB.Users.Attach(_user);
                _user.Invitations.RemoveAll(i => i.Uid == srcUid);
                if (consentOrDecline)
                {
                    User friend = messageDB.Users.Find(srcUid);
                    friend.FriendList.Add(_user);
                    _user.FriendList.Add(friend);
                    messageDB.SaveChanges();
                    if (usersOnline.TryGetValue(srcUid, out OperationContext operation))
                    {
                        operation.GetCallbackChannel<IRineCallBack>().AddFriendSuccess(
                            new FriendInfo
                            {
                                UserName = _user.UserName,
                                Uid = _user.Uid
                            });
                    }
                }
            }
        }

        public void GetInvitations()
        {
            IRineCallBack channel = OperationContext.Current.GetCallbackChannel<IRineCallBack>();
            foreach (var invitation in _user.Invitations)
            {
                channel.AddFriendNotify(new FriendInfo
                {
                    Uid = invitation.Uid,
                    UserName = invitation.UserName
                });
            }
        }

        private static string CryptoPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder hash = new StringBuilder();
                foreach (byte b in data)
                {
                    hash.Append(b.ToString("x2"));
                }
                return hash.ToString();
            }
        }
    }
}

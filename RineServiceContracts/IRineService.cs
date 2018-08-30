using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Runtime.Serialization;

namespace Rine.ServiceContracts
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IRineCallBack))]
    public interface IRineService
    {
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Send(MessageInfo message);

        // used when user just logs in to receive messages offline
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Receive(DateTime time);

        [OperationContract(IsOneWay = false, IsInitiating = true, IsTerminating = false)]
        string LogIn(UserInfo user);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void LogOut();

        [OperationContract(IsOneWay = false, IsInitiating = true, IsTerminating = true)]
        int Register(UserInfo user);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void AddFriend(FriendInfo friend);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void RemoveFriend(int uid);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void ResponseInvitation(bool consentOrDecline, int srcUid);
    }

    [DataContract]
    public class MessageInfo
    {
        private int dstUid;

        [DataMember]
        public int DstUid
        {
            get => dstUid;
            set
            {
                if (value <= 0)
                    throw new UidNotValidException(value);
                else dstUid = value;
                    
            }
        }

        private int srcUid;

        [DataMember]
        public int SrcUid
        {
            get => srcUid;
            set
            {
                if (value <= 0)
                    throw new UidNotValidException(value);
                else srcUid = value;
            }
        }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public DateTime Time { get; set; }
    }

    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int Uid { get; set; }
    }

    [DataContract]
    public class FriendInfo
    {
        [DataMember]
        public int Uid { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}

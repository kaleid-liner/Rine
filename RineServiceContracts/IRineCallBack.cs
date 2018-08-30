using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Rine.ServiceContracts
{
    public interface IRineCallBack
    {
        //used for user to receive instant messages
        [OperationContract(IsOneWay = true)]
        void ReceiveChat(MessageInfo message);

        [OperationContract(IsOneWay = true)]
        void LogInNotify(int uid);

        [OperationContract(IsOneWay = true)]
        void LogOutNotify(int uid);

        [OperationContract(IsOneWay = true)]
        void AddFriendNotify(FriendInfo inviter);

        [OperationContract(IsOneWay = true)]
        void AddFriendSuccess(FriendInfo friend);
    }
}

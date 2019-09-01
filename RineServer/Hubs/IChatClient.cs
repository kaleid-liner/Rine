using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RineSignalRContracts;

namespace RineServer.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(MessageRecv mesg);
        Task NotifyFriendRequests(FriendRequestRecv friend);
        Task NotifyNewFriend(FriendInfo friend);
    }
}

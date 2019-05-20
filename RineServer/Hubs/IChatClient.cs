using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RineSignalRContracts;

namespace RineServer.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(MessageInfo mesg);
        Task NotifyFriendRequests(UserInfo friend);
        Task NotifyFriendAccepted(UserInfo friend);
    }
}

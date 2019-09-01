using System;
using System.Collections.Generic;
using System.Text;

namespace RineSignalRContracts
{
    public class FriendRequestSent
    {
        // receiver's username of this friend request
        public string Receiver { get; set; }
        public string Description { get; set; }
    }
}

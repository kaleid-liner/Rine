using System;
using System.Collections.Generic;
using System.Text;

namespace RineSignalRContracts
{
    public class FriendRequestRecv
    {
        // Sender's UserName of this friend request
        public string Sender { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
    }
}

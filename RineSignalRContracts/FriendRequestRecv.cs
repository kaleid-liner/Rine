using System;
using System.Collections.Generic;
using System.Text;

namespace RineSignalRContracts
{
    public class FriendRequestRecv
    {
        public int Id { get; set; }
        // Sender's UserName of this friend request
        public string Sender { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
    }
}

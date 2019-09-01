using System;
using System.Collections.Generic;
using System.Text;

namespace RineSignalRContracts
{
    public class FriendInfo
    {
        public string UserName { get; set; }
        // Created Time of Friend's account
        public DateTime Created { get; set; }
        public DateTime LastOnline { get; set; }
        public FriendStatus Status { get; set; }
        
    }

    // different from UserStatus slightly. as friend has no access to status like 'invisible'
    public enum FriendStatus
    {
        Online,
        Offline,
    }
}

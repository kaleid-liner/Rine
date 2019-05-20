using System;
using System.Collections.Generic;
using System.Text;

namespace RineSignalRContracts
{
    public class MessageInfo
    {
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime Sent { get; set; }
    }
}

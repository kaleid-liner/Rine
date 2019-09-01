using System;
using System.Collections.Generic;
using System.Text;

namespace RineSignalRContracts
{
    public class MessageRecv
    {
        public string Content { get; set; }
        public string Sender { get; set; }
        public DateTime Created { get; set; }
    }
}

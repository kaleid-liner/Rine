﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RineSignalRContracts
{
    public class FriendRequestAction
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public bool Accept { get; set; }
    }
}

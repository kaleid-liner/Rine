﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RineClient
{
    public class Message
    {
        public string Content { get; set; }

        public string UserName { get; set; }

        public DateTime Time { get; set; }

        public bool IsSelf { get; set; }
    }
}

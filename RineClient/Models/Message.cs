using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RineClient.Models
{
    public class Message
    {
        public string Content { get; set; }
        public DateTime Sent { get; set; }
        public bool Read { get; set; }
    }
}

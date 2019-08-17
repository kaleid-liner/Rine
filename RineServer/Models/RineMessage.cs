using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using RineServer.Areas.Identity.Models;

namespace RineServer.Models
{
    public class RineMessage
    {
        public int Id { get; set; }
        public string Content { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Sent { get; set; }

        public RineUser Sender { get; set; }
        public RineUser Receiver { get; set; }

        public bool Received { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RineServer.Models
{
    public class RineUser
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        
        [InverseProperty("Receiver")]
        public List<RineMessage> Inbox { get; set; }
        [InverseProperty("Sender")]
        public List<RineMessage> Outbox { get; set; }

        public string Description { get; set; }

        public List<Friendship> FriendRequest { get; set; }
        public List<Friendship> FriendRecv { get; set; }
    }
}

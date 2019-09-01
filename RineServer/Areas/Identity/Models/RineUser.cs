using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RineServer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RineServer.Areas.Identity.Models
{
    public class RineUser: IdentityUser
    {
        public List<Friendship> FriendRequest { get; set; }
        public List<Friendship> FriendRecv { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
        public DateTime LastOnline { get; set; }

        [InverseProperty("Sender")]
        public List<RineMessage> OutBox { get; set; }
        [InverseProperty("Receiver")]
        public List<RineMessage> InBox { get; set; }

        public UserStatus Status { get; set; }
    }

    public enum UserStatus
    {
        Online,
        Offline,
        Invisible,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RineServer.Models;

namespace RineServer.Areas.Identity.Models
{
    public class RineUser: IdentityUser
    {
        public List<Friendship> FriendRequest { get; set; }
        public List<Friendship> FriendRecv { get; set; }
    }
}

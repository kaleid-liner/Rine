using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RineServer.Areas.Identity.Models;

namespace RineServer.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        public RineUser UserRequest { get; set; }
        public string UserRequestId { get; set; }
        public RineUser UserRecv { get; set; }
        public string UserRecvId { get; set; }

        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
        public DateTime? Actioned { get; set; }
        public FriendshipStatus Status { get; set; }
    }

    public enum FriendshipStatus
    {
        Pending,
        Denied,
        Accepted,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RineServer.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        public RineUser UserRequest { get; set; }
        public int UserRequestId { get; set; }
        public RineUser UserRecv { get; set; }
        public int UserRecvId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
        public DateTime? Actioned { get; set; }
        public bool? Accepted { get; set; }
    }
}

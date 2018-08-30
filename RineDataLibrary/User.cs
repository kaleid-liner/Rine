using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Rine.ServiceContracts;

namespace Rine.DataLibrary
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Uid { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual List<User> FriendList { get; set; }

        public virtual List<Message> ChatLogs { get; set; }

        public virtual List<User> Invitations { get; set; }

        public override int GetHashCode() => Uid;

        public override bool Equals(object obj)
        {
            return Uid == (obj as User).Uid;
        }
    }
}

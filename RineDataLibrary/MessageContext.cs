using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Rine.DataLibrary
{
    public class MessageContext : DbContext
    {
        public MessageContext() : base("RineDB") { }

        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }
    }
}

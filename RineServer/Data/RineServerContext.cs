using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RineServer.Models;

namespace RineServer.Models
{
    public class RineServerContext : DbContext
    {
        public RineServerContext (DbContextOptions<RineServerContext> options)
            : base(options)
        {
        }

        public DbSet<RineServer.Models.RineUser> RineUser { get; set; }

        public DbSet<RineServer.Models.RineMessage> RineMessage { get; set; }

        public DbSet<RineServer.Models.Friendship> Friendship { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RineUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<RineMessage>()
                .Property(m => m.Sent)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Friendship>()
                .Property(fs => fs.Created)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Friendship>()
                .HasOne(fs => fs.UserRequest)
                .WithMany(u => u.FriendRequest)
                .HasForeignKey(fs => fs.UserRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(fs => fs.UserRecv)
                .WithMany(u => u.FriendRecv)
                .HasForeignKey(fs => fs.UserRecvId);
        }

    }
}

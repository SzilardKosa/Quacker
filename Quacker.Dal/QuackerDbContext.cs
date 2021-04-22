using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Quacker.Dal.Entities;

namespace Quacker.Dal
{
    public partial class QuackerDbContext: DbContext
    {
        public QuackerDbContext(DbContextOptions<QuackerDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            // https://stackoverflow.com/questions/54196199/entity-framework-core-multiple-one-to-many-relationships-between-two-entities
            modelBuilder.Entity<User>()
                .HasMany(u => u.Followers)
                .WithOne(f => f.Followed)
                .HasForeignKey(f => f.FollowedId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.FollowedUsers)
                .WithOne(f => f.Follower)
                .HasForeignKey(f => f.FollowerId).OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedMessages)
                .WithOne(m => m.Receiver)
                .HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.NoAction);

            // Disable cascade to prevent https://stackoverflow.com/questions/17127351/introducing-foreign-key-constraint-may-cause-cycles-or-multiple-cascade-paths
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // configures entities without own PK
            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.UserId, l.PostId });
            modelBuilder.Entity<Following>()
                .HasKey(f => new { f.FollowerId, f.FollowedId });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

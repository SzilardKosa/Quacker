using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quacker.Dal.Entities;

namespace Quacker.Dal
{
    public partial class QuackerDbContext: IdentityDbContext<User, IdentityRole<int>, int>
    {
        public QuackerDbContext(DbContextOptions<QuackerDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.Entity<User>().ToTable("Users");
            // configures one-to-many relationship
            // https://stackoverflow.com/questions/54196199/entity-framework-core-multiple-one-to-many-relationships-between-two-entities
            modelBuilder.Entity<User>()
                .HasMany(u => u.Followers)
                .WithOne(f => f.Followed)
                .HasForeignKey(f => f.FollowedId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.FollowedUsers)
                .WithOne(f => f.Follower)
                .HasForeignKey(f => f.FollowerId).OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedMessages)
                .WithOne(m => m.Receiver)
                .HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.Restrict);

            // Disable cascade to prevent https://stackoverflow.com/questions/17127351/introducing-foreign-key-constraint-may-cause-cycles-or-multiple-cascade-paths
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);


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

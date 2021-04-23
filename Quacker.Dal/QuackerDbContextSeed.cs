using Microsoft.EntityFrameworkCore;
using Quacker.Dal.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal
{
    public partial class QuackerDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new UserEntityConfiguration()); Users are created with Identity!!!
            modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LikeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FollowingEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());
        }
    }
}

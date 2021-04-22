using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quacker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.EntityConfiguration
{
    public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(
                new Post
                { 
                    Id = 1,
                    CreationDate = new DateTime(2021, 01, 09, 9, 15, 0),
                    Content = "What a vacation it was!!",
                    UserId = 2
                },
                new Post
                {
                    Id = 2,
                    CreationDate = new DateTime(2021, 01, 09, 9, 15, 0),
                    Content = "What a vacation it was!! Here is a pic!",
                    HasImage = true,
                    UserId = 2
                }
                );
        }
    }
}

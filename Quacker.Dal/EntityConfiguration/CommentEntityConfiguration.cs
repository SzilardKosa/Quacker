using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quacker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.EntityConfiguration
{
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(
                new Comment { 
                    Id = 1,
                    Content = "Well said",
                    CreationDate = new DateTime(2021, 01, 09, 9, 15, 0),
                    UserId = 1,
                    PostId = 1 }
                );
        }
    }
}

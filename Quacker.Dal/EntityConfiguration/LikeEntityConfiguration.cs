using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quacker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.EntityConfiguration
{
    public class LikeEntityConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasData(
                new Like { UserId = 1, PostId = 1}
                );
        }
    }
}

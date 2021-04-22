using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quacker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.EntityConfiguration
{
    public class FollowingEntityConfiguration : IEntityTypeConfiguration<Following>
    {
        public void Configure(EntityTypeBuilder<Following> builder)
        {
            builder.HasData(
                new Following { FollowedId = 2, FollowerId = 1},
                new Following { FollowedId = 3, FollowerId = 1},
                new Following { FollowedId = 4, FollowerId = 1},
                new Following { FollowedId = 5, FollowerId = 1},
                new Following { FollowedId = 6, FollowerId = 1},
                new Following { FollowedId = 7, FollowerId = 1}
                );
        }
    }
}

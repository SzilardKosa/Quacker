using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quacker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.EntityConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // https://blog.reedsy.com/character-name-generator/language/english/
            builder.HasData(
                new User { Id = 1, UserName = "Marsh Holland" , HasImage = true },
                new User { Id = 2, UserName = "Minerva Harrett", HasImage = true },
                new User { Id = 3, UserName = "Daisy Alvarado", HasImage = true },
                new User { Id = 4, UserName = "Frank Kelley", HasImage = true },
                new User { Id = 5, UserName = "Yvette Goodman", HasImage = true },
                new User { Id = 6, UserName = "Shawn Curtis", HasImage = true },
                new User { Id = 7, UserName = "Gaylord Mcgee", HasImage = true },
                new User { Id = 8, UserName = "Alec Reid", HasImage = true },
                new User { Id = 9, UserName = "Neal Mack", HasImage = true },
                new User { Id = 10, UserName = "Britney Abbott", HasImage = true },
                new User { Id = 11, UserName = "Angela Gray", HasImage = true },
                new User { Id = 12, UserName = "Tabitha Dean", HasImage = true },
                new User { Id = 13, UserName = "Marmaduke Schwartz", HasImage = true },
                new User { Id = 14, UserName = "Pearl Moreno", HasImage = true },
                new User { Id = 15, UserName = "Danielle Wintringham", HasImage = true },
                new User { Id = 16, UserName = "Hughie Taylor", HasImage = true },
                new User { Id = 17, UserName = "Gregory Walker", HasImage = true },
                new User { Id = 18, UserName = "Albert French", HasImage = true },
                new User { Id = 19, UserName = "Duncan Barnes", HasImage = true },
                new User { Id = 20, UserName = "Beatrice Johnston", HasImage = true }
                );
        }
    }
}

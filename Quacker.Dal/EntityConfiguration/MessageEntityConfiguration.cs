using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quacker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.EntityConfiguration
{
    public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasData(
                new Message { 
                    Id = 1,
                    Content = "Hi man!",
                    CreationDate = new DateTime(2021, 01, 09, 9, 15, 0),
                    SenderId = 1,
                    ReceiverId = 2},
                new Message { 
                    Id = 2,
                    Content = "Heyyo!",
                    CreationDate = new DateTime(2021, 01, 09, 9, 15, 0),
                    SenderId = 2,
                    ReceiverId = 1},
                new Message { 
                    Id = 3,
                    Content = "How are you?",
                    CreationDate = new DateTime(2021, 01, 09, 9, 15, 0),
                    SenderId = 1,
                    ReceiverId = 2}
                );
        }
    }
}

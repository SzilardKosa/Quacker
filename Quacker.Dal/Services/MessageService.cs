using Quacker.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quacker.Dal.Services
{
    public class MessageService
    {
        public QuackerDbContext DbContext { get; }

        public MessageService(QuackerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<UserHeader> GetChatRooms(int currentUserId)
        {
            var chatRooms = DbContext.Users
                .Where(u => u.ReceivedMessages.Any(m => m.SenderId == currentUserId) ||
                            u.SentMessages.Any(m => m.ReceiverId == currentUserId))
                .OrderBy(u => u.DisplayName)
                .Select(u => new UserHeader
                {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    HasImage = u.HasImage
                })
                .ToList();

            return chatRooms;
        }

        public List<MessageData> GetMessages(int currentUserId, int userId, int count = 10)
        {
            var messages = DbContext.Messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == currentUserId) ||
                            (m.SenderId == currentUserId && m.ReceiverId == userId))
                .OrderBy(m => m.CreationDate)
                .Select(m => new MessageData
                {
                    Id = m.Id,
                    CreationDate = m.CreationDate,
                    Content = m.Content,
                    Author = new UserHeader
                    {
                        Id = m.Sender.Id,
                        DisplayName = m.Sender.DisplayName,
                        HasImage = m.Sender.HasImage
                    }
                })
                //.Take(count)
                .ToList();

            return messages;
        }
    }
}

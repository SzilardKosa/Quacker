using Quacker.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quacker.Dal.Services
{
    public class CommentService
    {
        public QuackerDbContext DbContext { get; }

        public CommentService(QuackerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<CommentData> GetComments(int postId, int count = 5)
        {
            var comments = DbContext.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreationDate)
                .Select(c => new CommentData
                {
                    Id = c.Id,
                    CreationDate = c.CreationDate,
                    Content = c.Content,
                    Author = new UserHeader
                    {
                        Id = c.User.Id,
                        DisplayName = c.User.DisplayName,
                        HasImage = c.User.HasImage
                    }
                })
                //.Take(count)
                .ToList();

            return comments;
        }

    }
}

using Quacker.Dal.Dto;
using Quacker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        private static Expression<Func<Comment, CommentData>> CommentDataSelector = c => new CommentData
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
        };

        public List<CommentData> GetComments(int postId, int count = 5)
        {
            var comments = DbContext.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreationDate)
                .Select(CommentDataSelector)
                //.Take(count)
                .ToList();

            return comments;
        }

        public CommentData GetComment(int id)
        {
            var commment = DbContext.Comments
                .Where(c => c.Id == id)
                .Select(CommentDataSelector)
                .SingleOrDefault();

            return commment;
        }

        public void CreateComment(NewComment newComment)
        {
            DbContext.Comments.Add(new Comment
            {
                CreationDate = DateTime.UtcNow,
                Content = newComment.Content,
                UserId = newComment.UserId,
                PostId = newComment.PostId
            });

            DbContext.SaveChanges();
        }

        public void DeleteComment(int commentId)
        {
            DbContext.Comments.Remove(new Comment { Id = commentId });
            DbContext.SaveChanges();
        }

    }
}

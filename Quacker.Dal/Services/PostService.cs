using Microsoft.EntityFrameworkCore;
using Quacker.Dal.Dto;
using Quacker.Dal.Entities;
using Quacker.Dal.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quacker.Dal.Services
{
    public class PostService
    {
        public QuackerDbContext DbContext { get; }

        public PostService( QuackerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        private static Expression<Func<Post, PostHeader>> PostHeaderSelector(int currentUserId)
        {
            return p => new PostHeader
            {
                Id = p.Id,
                CreationDate = p.CreationDate,
                Content = p.Content,
                HasImage = p.HasImage,
                Author = new UserHeader
                {
                    Id = p.User.Id,
                    DisplayName = p.User.DisplayName,
                    HasImage = p.User.HasImage
                },
                NumberOfComments = p.Comments.Count(),
                NumberOfLikes = p.Likes.Count(),
                HasCurrentUserLikedIt = p.Likes.Any(l => l.UserId == currentUserId)
            };
        }


        public PagedResult<PostHeader> GetPostsOfFollowedUsers(int currentUserId, PagerSpecification specification = null)
        {
            specification ??= new PagerSpecification();

            if (specification.PageSize <= 0)
                specification.PageSize = 9;

            if (specification.PageNumber <= 0)
                specification.PageNumber = 1;

            IQueryable<Post> query = DbContext.Posts;

            var postsQuery = query
                .Where(p => p.User.Followers.Any(f => f.FollowerId == currentUserId))
                .OrderByDescending(p => p.CreationDate)
                .Select(PostHeaderSelector(currentUserId));

            int allResultsCount = postsQuery.Count(); 

            var posts = postsQuery.Skip((specification.PageNumber - 1) * specification.PageSize)
                .Take(specification.PageSize)
                .ToList();

            return new PagedResult<PostHeader> {
                AllResultsCount = allResultsCount, 
                PageNumber = specification.PageNumber, 
                PageSize = specification.PageSize,
                Results = posts
            };
        }

        public PagedResult<PostHeader> GetPostsOfUser(int currentUserId, int userId, PagerSpecification specification = null)
        {
            specification ??= new PagerSpecification();

            if (specification.PageSize <= 0)
                specification.PageSize = 9;

            if (specification.PageNumber <= 0)
                specification.PageNumber = 1;

            IQueryable<Post> query = DbContext.Posts;

            var postsQuery = query
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreationDate)
                .Select(PostHeaderSelector(currentUserId));

            int allResultsCount = postsQuery.Count();

            var posts = postsQuery.Skip((specification.PageNumber - 1) * specification.PageSize)
                .Take(specification.PageSize)
                .ToList();

            return new PagedResult<PostHeader>
            {
                AllResultsCount = allResultsCount,
                PageNumber = specification.PageNumber,
                PageSize = specification.PageSize,
                Results = posts
            };
        }

        public PostHeader GetPost(int currentUserId, int id)
        {
            var post = DbContext.Posts
                .Where(p => p.Id == id)
                .Select(PostHeaderSelector(currentUserId))
                .SingleOrDefault();

            return post;
        }



    }
}

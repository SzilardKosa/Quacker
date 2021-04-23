using Microsoft.EntityFrameworkCore;
using Quacker.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<PostHeader> GetPosts()
        {
            var posts = DbContext.Posts
                .Select(p => new PostHeader
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
                    HasCurrentUserLikedIt = false
                }).ToList();

            return posts;
        }

        //public async Task<IEnumerable<PostHeader>> GetPosts()
        //{
        //    var posts = await DbContext.Posts
        //        .Select(p => new PostHeader
        //        {
        //            Id = p.Id,
        //            CreationDate = p.CreationDate,
        //            Content = p.Content,
        //            HasImage = p.HasImage,
        //            Author = new UserHeader
        //            { 
        //                Id = p.User.Id,
        //                UserName = p.User.UserName,
        //                HasImage = p.User.HasImage
        //            },
        //            NumberOfComments = p.Comments.Count(),
        //            NumberOfLikes = p.Likes.Count(),
        //            HasCurrentUserLikedIt = false
        //        }).ToListAsync();

        //    return posts;
        //}
    }
}

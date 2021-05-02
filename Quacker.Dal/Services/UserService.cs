using Quacker.Dal.Dto;
using Quacker.Dal.Entities;
using Quacker.Dal.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quacker.Dal.Services
{
    public class UserService
    {
        public QuackerDbContext DbContext { get; }

        public UserService(QuackerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<UserHeader> GetFollowedUsers(int currentUserId)
        {
            var users = DbContext.Users
                .Where(u => u.Followers.Any(f => f.FollowerId == currentUserId))
                .OrderBy(u => u.DisplayName)
                .Select(u => new UserHeader
                {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    HasImage = u.HasImage
                })
                .ToList();

            return users;
        }

        public UserHeader GetCurrentUser(int currentUserId)
        {
            var user = DbContext.Users
                .Where(u => u.Id == currentUserId)
                .Select(u => new UserHeader
                {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    HasImage = u.HasImage
                })
                .SingleOrDefault();

            return user;
        }

        public UserDetails GetUser(int currentUserId, int userId)
        {
            var user = DbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserDetails
                {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    HasImage = u.HasImage,
                    isFollowedByCurrentUser = u.Followers.Any(f => f.FollowerId == currentUserId),
                    NumberOfFollowers = u.Followers.Count()
                })
                .SingleOrDefault();

            return user;
        }

        public PagedResult<UserDetails> FindUsers(int currentUserId, string searchTerm, PagerSpecification specification = null)
        {
            specification ??= new PagerSpecification();

            if (specification.PageSize <= 0)
                specification.PageSize = 9;

            if (specification.PageNumber <= 0)
                specification.PageNumber = 1;

            IQueryable<User> query = DbContext.Users;

            var usersQuery = query
                .Where(u => u.DisplayName.Contains(searchTerm))
                .OrderBy(u => u.DisplayName)
                .Select(u => new UserDetails
                {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    HasImage = u.HasImage,
                    isFollowedByCurrentUser = u.Followers.Any(f => f.FollowerId == currentUserId),
                    NumberOfFollowers = u.Followers.Count()
                });

            int allResultsCount = usersQuery.Count();

            var users = usersQuery.Skip((specification.PageNumber - 1) * specification.PageSize)
                .Take(specification.PageSize)
                .ToList();

            return new PagedResult<UserDetails>
            {
                AllResultsCount = allResultsCount,
                PageNumber = specification.PageNumber,
                PageSize = specification.PageSize,
                Results = users
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quacker.Dal.Dto;
using Quacker.Dal.Services;
using Quacker.Dal.Specifications;

namespace Quacker.Web.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly PostService postService;
        private readonly UserService userService;

        public UserProfileModel(PostService postService, UserService userService)
        {
            this.postService = postService;
            this.userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public PagerSpecification Specification { get; set; }

        [BindProperty(SupportsGet = true, Name = "id")]
        public int UserId { get; set; }

        public PagedResult<PostHeader> Posts { get; set; }

        public UserDetails Profile { get; set; }

        public bool IsItOwnProfile { get; set; }

        public void OnGet()
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (UserId == 0)
            {
                UserId = currentUserId;
            }
            Posts = postService.GetPostsOfUser(currentUserId, UserId, Specification);
            Profile = userService.GetUser(currentUserId, UserId);
            if (UserId == currentUserId)
            {
                IsItOwnProfile = true;
            } else
            {
                IsItOwnProfile = false;
            }
        }
    }
}

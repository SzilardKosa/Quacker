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

namespace Quacker.Web.Pages.User
{
    public class HomeModel : PageModel
    {
        private readonly PostService postService;
        private readonly UserService userService;

        public HomeModel( PostService postService, UserService userService)
        {
            this.postService = postService;
            this.userService = userService;
        }


        [BindProperty(SupportsGet = true)]
        public PagerSpecification Specification { get; set; }

        public PagedResult<PostHeader> Posts { get; set; }

        public List<UserHeader> FollowedUsers { get; set; }

        public void OnGet()
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Posts = postService.GetPostsOfFollowedUsers(currentUserId, Specification);
            FollowedUsers = userService.GetFollowedUsers(currentUserId);

        }
    }
}

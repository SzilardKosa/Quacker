using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quacker.Dal.Dto;
using Quacker.Dal.Services;

namespace Quacker.Web.Pages
{
    public class PostModel : PageModel
    {
        private readonly PostService postService;
        private readonly CommentService commentService;
        private readonly UserService userService;

        public PostModel( PostService postService, CommentService commentService, UserService userService)
        {
            this.postService = postService;
            this.commentService = commentService;
            this.userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public PostHeader Post { get; set; }
        public UserHeader CurrentUser { get; set; }

        public List<CommentData> Comments { get; set; }

        public void OnGet()
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CurrentUser = userService.GetCurrentUser(currentUserId);
            Post = postService.GetPost(currentUserId, Id);
            Comments = commentService.GetComments(Id);
        }
    }
}

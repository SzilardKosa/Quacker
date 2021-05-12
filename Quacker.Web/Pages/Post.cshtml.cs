using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ganss.XSS;
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

        [BindProperty]
        public NewComment NewComment { get; set; }

        [BindProperty]
        public int DeleteCommentId { get; set; }

        public List<CommentData> Comments { get; set; }

        public void OnGet()
        {
            LoadModel();
            NewComment = new NewComment { PostId = Id, Content = "" };
        }

        public IActionResult OnPostCreateComment()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    NewComment.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    commentService.CreateComment(NewComment);
                    return RedirectToPage("/Post", new { Id = NewComment.PostId });
                }
                catch (Exception ex)
                {
                    // TODO: Log
                    ModelState.AddModelError("", "An error occurred while creating your comment");
                }
            }
            Id = NewComment.PostId;
            LoadModel();
            return Page();
        }

        public IActionResult OnPostDeleteComment()
        {
            if (CanCurrentUserDelete(DeleteCommentId))
            {
                commentService.DeleteComment(DeleteCommentId);
            }
            return RedirectToPage("/Post", new { Id = Id });
        }

        private bool CanCurrentUserDelete(int deleteCommentId)
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var Comment = commentService.GetComment(deleteCommentId);
            if(currentUserId == Comment.Author.Id || User.IsInRole("Administrator"))
            {
                return true;
            }
            return false;
        }

        private void LoadModel()
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CurrentUser = userService.GetCurrentUser(currentUserId);
            Post = postService.GetPost(currentUserId, Id);
            Comments = commentService.GetComments(Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Quacker.Dal.Dto;
using Quacker.Dal.Services;
using Quacker.Dal.Specifications;

namespace Quacker.Web.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly PostService postService;
        private readonly UserService userService;
        private readonly IWebHostEnvironment environment;
        private readonly long fileSizeLimit;
        private readonly List<string> permittedExtensions;

        public UserProfileModel(PostService postService, UserService userService, IConfiguration config, IWebHostEnvironment env)
        {
            this.postService = postService;
            this.userService = userService;
            this.environment = env;

            fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            permittedExtensions = config.GetSection("PermittedExtensions").Get<List<string>>();
        }

        [BindProperty(SupportsGet = true)]
        public PagerSpecification Specification { get; set; }

        [BindProperty(SupportsGet = true, Name = "id")]
        public int UserId { get; set; }

        public PagedResult<PostHeader> Posts { get; set; }

        public UserDetails Profile { get; set; }

        public bool IsItOwnProfile { get; set; }

        [BindProperty]
        public NewPost NewPost { get; set; }

        [BindProperty]
        public IFormFile CoverImage { get; set; }

        [BindProperty]
        public int DeletePostId { get; set; }

        public int CurrentUserId { get; set; }

        public void OnGet()
        {
            LoadModel();

            if (IsItOwnProfile)
            {
                NewPost = new NewPost() { UserId = CurrentUserId };
            }
        }

        public IActionResult OnPostCreatePost()
        {
            if (!ModelState.IsValid)
            {
                UserId = NewPost.UserId;
                LoadModel();
                return Page();
            }

            try
            {
                if ( CoverImage != null )
                {
                    var fileName = CoverImage.FileName;
                    // Check extension and size
                    var ext = Path.GetExtension(fileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                    {
                        ModelState.AddModelError("CoverImage", "Only .jpg extensions are accepted");

                        UserId = NewPost.UserId;
                        LoadModel();
                        return Page();
                    }
                    if (CoverImage.Length > fileSizeLimit)
                    {
                        ModelState.AddModelError("CoverImage", "Too large file size");

                        UserId = NewPost.UserId;
                        LoadModel();
                        return Page();
                    }

                    NewPost.HasImage = true;

                    int postId = postService.CreatePost(NewPost);

                    if (CoverImage != null && CoverImage.Length > 0)
                    {
                        int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                        var filePath = Path.Combine(this.environment.WebRootPath, $"imgs/posts/{currentUserId}/{postId}{ext}");
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            CoverImage.CopyTo(stream);
                        }
                    }
                }
                else
                {
                    NewPost.HasImage = false;
                    postService.CreatePost(NewPost);
                }
                return RedirectToPage("/Profile", new { Id = NewPost.UserId });
            }
            catch (Exception ex)
            {
                // TODO: Log
                ModelState.AddModelError("", "An error occurred while creating your post");
            }

            UserId = NewPost.UserId;
            LoadModel();
            return Page();
        }

        public IActionResult OnPostDeletePost()
        {
            postService.DeletePost(DeletePostId);
            return RedirectToPage("/Profile", new { Id = UserId });
        }

        public IActionResult OnPostFollow()
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            userService.FollowUser(currentUserId, UserId);
            return RedirectToPage("/Profile", new { Id = UserId });
        }

        public IActionResult OnPostUnfollow()
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            userService.UnfollowUser(currentUserId, UserId);
            return RedirectToPage("/Profile", new { Id = UserId });
        }

        private void LoadModel()
        {
            CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (UserId == 0)
            {
                UserId = CurrentUserId;
            }
            Posts = postService.GetPostsOfUser(CurrentUserId, UserId, Specification);
            Profile = userService.GetUser(CurrentUserId, UserId);
            if (UserId == CurrentUserId)
            {
                IsItOwnProfile = true;
            }
            else
            {
                IsItOwnProfile = false;
            }
        }


    }
}

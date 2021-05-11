using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Quacker.Dal.Dto;
using Quacker.Dal.Entities;
using Quacker.Dal.Services;

namespace Quacker.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment environment;
        private readonly UserService userService;
        private readonly long fileSizeLimit;
        private readonly List<string> permittedExtensions;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration config, 
            IWebHostEnvironment env, 
            UserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.environment = env;
            this.userService = userService;
            fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            permittedExtensions = config.GetSection("PermittedExtensions").Get<List<string>>();
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public IFormFile CoverImage { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }
        public UserHeader CurrentUser { get; set; }

        private async Task LoadAsync(User user)
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CurrentUser = await userService.GetCurrentUserAsync(currentUserId);
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (CoverImage != null)
            {
                var fileName = CoverImage.FileName;
                // Check extension and size
                var ext = Path.GetExtension(fileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("CoverImage", "Only .jpg extensions are accepted");
                    await LoadAsync(user);
                    return Page();
                }
                if (CoverImage.Length > fileSizeLimit)
                {
                    ModelState.AddModelError("CoverImage", "Too large file size");
                    await LoadAsync(user);
                    return Page();
                }

                if (CoverImage != null && CoverImage.Length > 0)
                {
                    int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var filePath = Path.Combine(this.environment.WebRootPath, $"imgs/users/{currentUserId}{ext}");
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await CoverImage.CopyToAsync(stream);
                    }
                    await userService.UpdateCurrentUserAsync(currentUserId, true);
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

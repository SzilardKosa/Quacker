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
    public class SearchModel : PageModel
    {
        private readonly UserService userService;

        public SearchModel( UserService userService)
        {
            this.userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public PagerSpecification Specification { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public PagedResult<UserDetails> Users { get; set; }


        public void OnGet()
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Users = userService.FindUsers(currentUserId, SearchTerm, Specification);

        }
    }
}

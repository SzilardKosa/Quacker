using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Quacker.Dal;
using Quacker.Dal.Dto;
using Quacker.Dal.Entities;
using Quacker.Dal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacker.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IEnumerable<PostHeader> Posts { get; set; }

        public void OnGet( [FromServices] PostService postService )
        {
            Posts = postService.GetPosts();
        }
    }
}

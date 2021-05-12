using Microsoft.AspNetCore.Mvc;
using Quacker.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacker.Web.ViewComponents
{
    public class PostViewComponent : ViewComponent
    {
        public class PostSpecification
        {
            public PostHeader Post { get; set; }
            public bool IsDeletable { get; set; }
            public int DeletePostId { get; set; }
            public int UserId { get; set; }
        }

        // no optional parameter support :( https://stackoverflow.com/questions/44208838/viewcomponent-with-optional-parameters
        public IViewComponentResult Invoke(PostHeader Post, bool IsDeletable, int DeletePostId = 0, int UserId = 0)
        {
            return View(new PostSpecification
            {
                Post = Post,
                IsDeletable = IsDeletable,
                DeletePostId = DeletePostId,
                UserId = UserId
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quacker.Dal.Dto
{
    public class NewComment
    {
        // Comment entity
        [Required]
        public string Content { get; set; }
        // Other entities
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}

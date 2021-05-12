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
        [StringLength(281, ErrorMessage = "Comment cannot be longer than 281 characters.")]
        public string Content { get; set; }
        // Other entities
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}

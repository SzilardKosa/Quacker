using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quacker.Dal.Dto
{
    public class NewPost
    {
        [Required]
        [StringLength(281, ErrorMessage = "Post content cannot be longer than 281 characters.")]
        public string Content { get; set; }
        public bool HasImage { get; set; }
        public int UserId { get; set; }
    }
}

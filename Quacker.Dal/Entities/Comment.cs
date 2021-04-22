using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quacker.Dal.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        [StringLength(281, ErrorMessage = "Comment cannot be longer than 281 characters.")]
        public string Content { get; set; }
        // nav props
        public int UserId { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}

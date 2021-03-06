using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quacker.Dal.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        [StringLength(281, ErrorMessage = "Post content cannot be longer than 281 characters.")]
        public string Content { get; set; }
        public bool HasImage { get; set; }
        // User 1-N
        public int UserId { get; set; }
        public User User { get; set; }
        // Comments N-1
        public ICollection<Comment> Comments { get; set; }
        // Likes N-1
        public ICollection<Like> Likes { get; set; }

    }
}

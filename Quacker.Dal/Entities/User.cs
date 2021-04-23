using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quacker.Dal.Entities
{
    public class User : IdentityUser<int>
    {
        [Required]
        [StringLength(40, ErrorMessage = "User name cannot be longer than 40 characters.")]
        public string DisplayName { get; set; }
        public bool HasImage { get; set; }
        // N - 1 navigation props
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        // Double N - 1 navigaton props
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> FollowedUsers { get; set; }
    }
}

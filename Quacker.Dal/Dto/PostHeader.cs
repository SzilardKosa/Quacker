using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.Dto
{
    public class PostHeader
    {
        // Post entity
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Content { get; set; }
        public bool HasImage { get; set; }
        // Other entities
        public UserHeader Author { get; set; }
        // Calculated entities
        public int NumberOfComments { get; set; }
        public int NumberOfLikes { get; set; }
        public bool HasCurrentUserLikedIt { get; set; }
    }
}

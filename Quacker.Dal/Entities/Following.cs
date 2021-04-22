using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.Entities
{
    public class Following
    {
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }
        public User Follower { get; set; }
        public User Followed { get; set; }
    }
}

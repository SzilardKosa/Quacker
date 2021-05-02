using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.Dto
{
    public class MessageData
    {
        // Message entity
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Content { get; set; }
        // Other entities
        public UserHeader Author { get; set; }
    }
}

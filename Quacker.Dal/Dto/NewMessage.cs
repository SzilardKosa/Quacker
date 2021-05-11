using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quacker.Dal.Dto
{
    public class NewMessage
    {
        // Comment entity
        [Required]
        public string Content { get; set; }
        // Other entities
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}

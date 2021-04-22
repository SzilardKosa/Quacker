using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quacker.Dal.Entities
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        // nav props
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }

    }
}

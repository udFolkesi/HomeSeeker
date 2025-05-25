using Core.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Message: BaseEntity
    {
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public Profile Sender { get; set; }
        [ForeignKey(nameof(ReceiverId))]
        public Profile Receiver { get; set; }
    }
}

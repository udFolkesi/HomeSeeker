using Core.Entities.Abstractions;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Profile: BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public RoleEnum Role { get; set; } = RoleEnum.Guest;
        public string PhoneNumber { get; set; } = string.Empty;
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

        [InverseProperty(nameof(Message.Sender))]
        public ICollection<Message> SenderMessages { get; set; } = new List<Message>();
        [InverseProperty(nameof(Message.Receiver))]
        public ICollection<Message> ReceiverMessages { get; set; } = new List<Message>();
    }
}

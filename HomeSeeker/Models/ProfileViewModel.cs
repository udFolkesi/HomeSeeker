using Core.Entities;

namespace HomeSeeker.Models
{
    public class ProfileViewModel
    {
        public User Data { get; set; }
        public ICollection<Announcement> UserAnnouncements { get; set; }
    }
}

using Core.Entities;

namespace HomeSeeker.Models
{
    public class HomeModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Type { get; set; } // Квартира, Дом, и т.д.
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string? MainImage { get; set; }

        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string Address { get; set; }

        public ICollection<Announcement> PopularAnnouncements { get; set; }
        public ICollection<Announcement> AnnouncementsForUser { get; set; }
        public ICollection<Announcement> AnnouncementsForProfile { get; set; }
        public ICollection<Announcement> RecommendedAnnouncements { get; set; }


        public List<AnnouncementViewModel> PopularAds { get; set; }
        public List<AnnouncementViewModel> RecommendedAds { get; set; }
    }
}

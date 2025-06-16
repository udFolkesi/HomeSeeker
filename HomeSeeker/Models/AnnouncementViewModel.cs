using Core.Entities;

namespace HomeSeeker.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string? MainImage { get; set; }
        public int? TimeToCenter { get; set; }
    }
}

using Core.Entities;

namespace HomeSeeker.Models
{
    public class AnnouncementModel
    {
        public string? Search { get; set; }
        public string? City { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public string? Sort { get; set; }
        public List<int> FavoriteAdIds { get; set; } = new();

        public List<AnnouncementViewModel> Ads { get; set; } = new();
    }
}

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
    }
}

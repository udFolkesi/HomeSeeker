//using Core.Entity;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class HomeSeekerDbContext: DbContext
    {
        public HomeSeekerDbContext(DbContextOptions<HomeSeekerDbContext> options) : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ObjectDescription> ObjectDescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

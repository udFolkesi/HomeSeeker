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
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ObjectDescription> ObjectDescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(p => p.Receiver)
                .WithMany(t => t.ReceiverMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(p => p.Sender)
                .WithMany(t => t.SenderMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.Profile)
                .WithMany(p => p.Announcements)
                .HasForeignKey(a => a.ProfileID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

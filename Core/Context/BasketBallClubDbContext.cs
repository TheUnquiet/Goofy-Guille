using Core.Entities;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public class BasketBallClubDbContext : DbContext
    {
        public BasketBallClubDbContext(DbContextOptions options) : base(options)
        {
        }

        protected BasketBallClubDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventResponse>().HasKey(x => new { x.EventId, x.UserId });
            modelBuilder.Entity<EventResponse>().HasOne<Event>().WithMany().HasForeignKey(x => x.EventId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EventResponse>().HasOne<User>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Event>().HasOne<Club>().WithMany().HasForeignKey(x => x.ClubId).OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Club> Clubs => Set<Club>();

        public DbSet<Event> Events => Set<Event>();

        public DbSet<EventResponse> EventResponses => Set<EventResponse>();
    }
}

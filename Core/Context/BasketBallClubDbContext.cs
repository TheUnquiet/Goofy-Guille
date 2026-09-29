using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public class BasketBallClubDbContext : DbContext
    {
        public BasketBallClubDbContext(DbContextOptions<BasketBallClubDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventResponse>().HasKey(x => new { x.EventId, x.UserId });
            modelBuilder.Entity<EventResponse>().HasOne<Event>().WithMany().HasForeignKey(x => x.EventId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EventResponse>().HasOne<User>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne<Club>().WithMany().HasForeignKey(x => x.ClubId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => new { x.ExternalProvider, x.ExternalId }).IsUnique().HasFilter("[ExternalProvider] IS NOT NULL AND [ExternalId] IS NOT NULL");
            modelBuilder.Entity<Event>().HasOne<Club>().WithMany().HasForeignKey(x => x.ClubId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Event>().HasOne<User>().WithMany().HasForeignKey(x => x.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Event>().HasOne<Club>().WithMany().HasForeignKey(x => x.OpponentClubId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Team>().HasOne<Club>().WithMany().HasForeignKey(x => x.ClubId).OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Club> Clubs => Set<Club>();

        public DbSet<Event> Events => Set<Event>();

        public DbSet<EventResponse> EventResponses => Set<EventResponse>();

        public DbSet<Location> Locations => Set<Location>();

        public DbSet<Team> Teams => Set<Team>();
    }
}

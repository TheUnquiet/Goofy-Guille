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

            // EventResponse -> Event
            modelBuilder.Entity<EventResponse>()
                .HasKey(x => new { x.EventId, x.UserId });

            modelBuilder.Entity<EventResponse>()
                .HasOne<Event>()
                .WithMany()
                .HasForeignKey(x => x.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // EventResponse -> User
            modelBuilder.Entity<EventResponse>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // User -> Club
            modelBuilder.Entity<User>()
                .HasOne<Club>()
                .WithMany()
                .HasForeignKey(x => x.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> Team
            modelBuilder.Entity<User>()
                .HasOne<Team>()
                .WithMany()
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            // User constraints
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x => new { x.ExternalProvider, x.ExternalId })
                .IsUnique()
                .HasFilter(
                    "[ExternalProvider] IS NOT NULL AND [ExternalId] IS NOT NULL");

            // Event -> Club
            modelBuilder.Entity<Event>()
                .HasOne<Club>()
                .WithMany()
                .HasForeignKey(x => x.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            // Event -> Creator
            modelBuilder.Entity<Event>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Event -> Opponent Club
            modelBuilder.Entity<Event>()
                .HasOne<Club>()
                .WithMany()
                .HasForeignKey(x => x.OpponentClubId)
                .OnDelete(DeleteBehavior.NoAction);

            // Event -> Location
            modelBuilder.Entity<Event>()
                .HasOne<Location>()
                .WithMany()
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Team -> Club
            modelBuilder.Entity<Team>()
                .HasOne<Club>()
                .WithMany()
                .HasForeignKey(x => x.ClubId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Club> Clubs => Set<Club>();

        public DbSet<Event> Events => Set<Event>();

        public DbSet<EventResponse> EventResponses => Set<EventResponse>();

        public DbSet<Location> Locations => Set<Location>();

        public DbSet<Team> Teams => Set<Team>();
    }
}

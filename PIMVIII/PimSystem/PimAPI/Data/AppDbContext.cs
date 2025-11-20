// Entity Framework Core DbContext class for database connection and mapping.
using Microsoft.EntityFrameworkCore;
using MyPIMApi.Models;

namespace MyPIMApi.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets map model classes to database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<PlaylistItem> PlaylistItems { get; set; }

        // Optional: Fluent API configuration for relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the many-to-many relationship via PlaylistItem
            modelBuilder.Entity<PlaylistItem>()
                .HasKey(pi => pi.ID); // Assuming a simple ID as PK

            // One Playlist has many PlaylistItems
            modelBuilder.Entity<PlaylistItem>()
                .HasOne(pi => pi.Playlist)
                .WithMany(p => p.Items)
                .HasForeignKey(pi => pi.PlaylistID);

            // One Content has many PlaylistItems
            modelBuilder.Entity<PlaylistItem>()
                .HasOne(pi => pi.Content)
                .WithMany(c => c.PlaylistItems)
                .HasForeignKey(pi => pi.ContentID);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using MyPIMApi.Models;

namespace MyPIMApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<PlaylistItem> PlaylistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlaylistItem>()
                .HasKey(pi => pi.ID); 

            modelBuilder.Entity<PlaylistItem>()
                .HasOne(pi => pi.Playlist)
                .WithMany(p => p.Items)
                .HasForeignKey(pi => pi.PlaylistID);

            modelBuilder.Entity<PlaylistItem>()
                .HasOne(pi => pi.Content)
                .WithMany(c => c.PlaylistItems)
                .HasForeignKey(pi => pi.ContentID);
        }
    }
}
// Repository class to abstract data access logic for the Playlist entity.
using Microsoft.EntityFrameworkCore;
using MyPIMApi.Data;
using MyPIMApi.Models;

namespace MyPIMApi.Repositories
{
    public class PlaylistRepository
    {
        private readonly AppDbContext _context;

        public PlaylistRepository(AppDbContext context)
        {
            _context = context;
        }

        // CRUD - Create
        public async Task<Playlist> AddPlaylistAsync(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();
            return playlist;
        }

        // CRUD - Read (All)
        public async Task<IEnumerable<Playlist>> GetAllPlaylistsAsync()
        {
            // Include navigation properties for a fuller view
            return await _context.Playlists
                .Include(p => p.User)
                .Include(p => p.Items)
                    .ThenInclude(pi => pi.Content)
                        .ThenInclude(c => c.Creator)
                .ToListAsync();
        }

        // CRUD - Read (Single)
        public async Task<Playlist?> GetPlaylistByIdAsync(int id)
        {
            return await _context.Playlists
                .Include(p => p.User)
                .Include(p => p.Items)
                    .ThenInclude(pi => pi.Content)
                        .ThenInclude(c => c.Creator)
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        // CRUD - Update
        public async Task<bool> UpdatePlaylistAsync(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        // CRUD - Delete
        public async Task<bool> DeletePlaylistAsync(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return false;
            }

            _context.Playlists.Remove(playlist);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using MyPIMApi.Repositories;
using MyPIMApi.Data; 
using MyPIMApi.Models;

namespace MyPIMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly PlaylistRepository _repository;
        private readonly AppDbContext _context; 

        public PlaylistsController(PlaylistRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            return await _context.Playlists
                            .Include(p => p.Items)
                            .ThenInclude(i => i.Content)
                            .ToListAsync();
        }

        // POST: api/Playlists
        [HttpPost]
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist playlist)
        {
            await _repository.AddPlaylistAsync(playlist);
            return CreatedAtAction(nameof(GetPlaylist), new { id = playlist.ID }, playlist);
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            var playlist = await _repository.GetPlaylistByIdAsync(id);

            if (playlist == null) 
            {
                return NotFound();
            }

            return Ok(playlist);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using MyPIMApi.Repositories;
using MyPIMApi.Data; // Garanta que está usando o Data
using MyPIMApi.Models;

namespace MyPIMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly PlaylistRepository _repository;
        private readonly AppDbContext _context; // <--- O BEZERRO QUE FALTAVA!

        // Injetamos TANTO o Repositório (pro PIM ficar feliz) QUANTO o Contexto (pro Include funcionar rápido)
        public PlaylistsController(PlaylistRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            // Agora o _context existe!
            // O .Include traz a tabela de ligação (ItemPlaylist)
            // O .ThenInclude traz o Conteúdo real (Musica/Video)
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
            // Aqui usamos o repositório como manda o figurino
            var playlist = await _repository.GetPlaylistByIdAsync(id);

            if (playlist == null) 
            {
                return NotFound();
            }

            return Ok(playlist);
        }
    }
}
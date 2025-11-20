using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPIMApi.Data;
using MyPIMApi.Models;

namespace MyPIMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContentsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Contents/addToPlaylist
        [HttpPost("addToPlaylist")]
        public async Task<IActionResult> AddContentToPlaylist([FromBody] ContentUploadDto dto)
        {
            // 1. Garante que existe um Criador no banco (Regra do PIM)
            var criador = await _context.Creators.FirstOrDefaultAsync();
            if (criador == null)
            {
                criador = new Creator { Name = "Criador Padrão" };
                _context.Creators.Add(criador);
                await _context.SaveChangesAsync();
            }

            // 2. Cria o Conteúdo (Música/Vídeo)
            var novoConteudo = new Content
            {
                Title = dto.Title,
                Type = dto.Type,
                CreatorID = criador.ID,
                Url = dto.Url
            };
            _context.Contents.Add(novoConteudo);
            await _context.SaveChangesAsync();

            // 3. Cria o Link na Tabela Associativa (ItemPlaylist)
            var link = new PlaylistItem
            {
                PlaylistID = dto.PlaylistID,
                ContentID = novoConteudo.ID,
                ItemOrder = 1
            };
            _context.PlaylistItems.Add(link);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Conteúdo adicionado com sucesso!" });
        }
    }

    // --- A CLASSE DTO FICA AQUI FORA ---
    public class ContentUploadDto
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public int PlaylistID { get; set; }
    }
}
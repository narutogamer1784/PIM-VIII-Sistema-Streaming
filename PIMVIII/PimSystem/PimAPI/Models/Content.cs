using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPIMApi.Models
{
    public class Content
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty; // Inicializa vazio pra sumir o aviso

        public string Type { get; set; } = "Musica"; // Valor padrão

        public string Url { get; set; } = ""; // Link da música/vídeo

        // Chave estrangeira
        public int CreatorID { get; set; }

        // Navegação (Pode ser nulo '?' pra evitar erro de validação)
        [ForeignKey(nameof(CreatorID))]
        public Creator? Creator { get; set; }

        // Relação com Playlists
        public List<PlaylistItem> PlaylistItems { get; set; } = new List<PlaylistItem>();
    }
}
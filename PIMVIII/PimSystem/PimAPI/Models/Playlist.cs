using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPIMApi.Models
{
    public class Playlist
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty; 

        public string? Description { get; set; }
        
        public int UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User? User { get; set; } 

        public List<PlaylistItem> Items { get; set; } = new List<PlaylistItem>();
    }
}
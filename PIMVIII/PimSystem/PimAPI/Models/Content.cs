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
        public string Title { get; set; } = string.Empty; 

        public string Type { get; set; } = "Musica"; 

        public string Url { get; set; } = ""; 

        public int CreatorID { get; set; }

        [ForeignKey(nameof(CreatorID))]
        public Creator? Creator { get; set; }

        public List<PlaylistItem> PlaylistItems { get; set; } = new List<PlaylistItem>();
    }
}
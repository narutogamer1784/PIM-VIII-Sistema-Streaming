using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPIMApi.Models
{
    public class PlaylistItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PlaylistID { get; set; }
        
        [Required]
        public int ContentID { get; set; }

        public int ItemOrder { get; set; }

        [ForeignKey(nameof(PlaylistID))]
        public Playlist Playlist { get; set; }
        
        [ForeignKey(nameof(ContentID))]
        public Content Content { get; set; }
    }
}
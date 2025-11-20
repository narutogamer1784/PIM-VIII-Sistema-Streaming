// Represents the 'ItemPlaylist' entity (the many-to-many relationship between Playlist and Content).
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPIMApi.Models
{
    public class PlaylistItem
    {
        // Composite Key (configured in DbContext) or simple ID
        [Key]
        public int ID { get; set; }

        [Required]
        public int PlaylistID { get; set; }
        
        [Required]
        public int ContentID { get; set; }

        // Metadata for the item order in the playlist
        public int ItemOrder { get; set; }

        // Navigation properties
        [ForeignKey(nameof(PlaylistID))]
        public Playlist Playlist { get; set; }
        
        [ForeignKey(nameof(ContentID))]
        public Content Content { get; set; }
    }
}
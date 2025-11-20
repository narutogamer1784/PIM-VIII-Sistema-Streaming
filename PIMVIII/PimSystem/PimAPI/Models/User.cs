using System.ComponentModel.DataAnnotations;

namespace MyPIMApi.Models
{
    public class User
    {
        [Key] 
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        public List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
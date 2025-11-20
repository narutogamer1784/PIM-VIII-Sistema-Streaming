// Represents the 'Usuario' entity from the class diagram.
using System.ComponentModel.DataAnnotations;

namespace MyPIMApi.Models
{
    public class User
    {
        [Key] // Primary Key
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        // Navigation property: One User can have many Playlists
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
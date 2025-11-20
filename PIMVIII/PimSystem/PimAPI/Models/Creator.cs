// Represents the 'Criador' entity.
using System.ComponentModel.DataAnnotations;

namespace MyPIMApi.Models
{
    public class Creator
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Optional: A description for the creator
        public string? Description { get; set; }

        // Navigation property: One Creator can have many Contents
        public List<Content> Contents { get; set; } = new List<Content>();
    }
}
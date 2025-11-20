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

        public string? Description { get; set; }

        public List<Content> Contents { get; set; } = new List<Content>();
    }
}
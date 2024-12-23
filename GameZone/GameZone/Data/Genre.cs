using System.ComponentModel.DataAnnotations;

namespace GameZone.Data
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; } = null!;

        [Required]
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}

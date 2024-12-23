using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Data
{

    [PrimaryKey(nameof(GameId), nameof(GamerId))]
    public class GamerGame
    {
        [Key]
        public int GameId { get; set; }

        [Required]
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; } = null!;

        [Key]
        public string GamerId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(GamerId))]
        public IdentityUser Gamer { get; set; } = null!;

    } 
}

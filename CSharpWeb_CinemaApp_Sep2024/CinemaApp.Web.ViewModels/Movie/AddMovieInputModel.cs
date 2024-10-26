using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class AddMovieInputModel
    {
        public AddMovieInputModel() 
        { 
            this.ReleaseDate = DateTime.UtcNow.ToString("MM/yyyy");
        }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(12)]
        public string Genre { get; set; } = null!;

        [Required]
        public string ReleaseDate { get; set; }

        [Required]
        [MaxLength(25)]
        public string Director { get; set; } = null!;
        
        [Required]
        [Range(0,240)]
        public int Duration { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = null!;



    }
}

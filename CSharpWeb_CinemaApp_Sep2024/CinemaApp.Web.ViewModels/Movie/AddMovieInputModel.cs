namespace CinemaApp.Web.ViewModels.Movie
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationMessages.Movie;

    public class AddMovieInputModel
    {
        public AddMovieInputModel() 
        { 
            this.ReleaseDate = DateTime.UtcNow.ToString("MM/yyyy");
        }

        [Required(ErrorMessage = TitleRequiredMsg)]
        [MaxLength(40)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = GenreRequiredMsg)]
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

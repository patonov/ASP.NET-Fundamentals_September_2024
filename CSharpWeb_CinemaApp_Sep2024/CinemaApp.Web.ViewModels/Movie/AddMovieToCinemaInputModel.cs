using CinemaApp.Web.ViewModels.Cinema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Common.EntityValidationConstants.Movie;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class AddMovieToCinemaInputModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [MaxLength(MovieTitleMaxLength)]
        public string MovieTitle { get; set; } = null!;

        public List<CinemaCheckBoxItemInputModel> Cinemas { get; set; } = new List<CinemaCheckBoxItemInputModel>();
    }
}

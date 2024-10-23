using System;
using System.Collections.Generic;
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

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string ReleaseDate { get; set; }

        public string Director { get; set; } = null!;
        
        public int Duration { get; set; }

        public string Description { get; set; } = null!;



    }
}

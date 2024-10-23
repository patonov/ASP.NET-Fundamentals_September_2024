using CinemaApp.Web.Data;
using CinemaApp.Web.Models;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly CinemaDbContext dbContext;

        public MovieController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Movie> movies = dbContext.Movies.ToList();

            return this.View(movies);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(AddMovieInputModel inputMovie) 
        {
            DateTime releaseDate;

            bool isReleaseDateValid = DateTime.TryParseExact(inputMovie.ReleaseDate, 
                "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);

            if (!isReleaseDateValid) 
            {
                this.ModelState.AddModelError(nameof(inputMovie.ReleaseDate), "You should type Release Date in correct way!");
            }

            if (!this.ModelState.IsValid) 
            {
                return this.View(inputMovie);
            }

            Movie movie = new Movie()
            {
                Title = inputMovie.Title,
                Genre = inputMovie.Genre,
                Director = inputMovie.Director,
                Duration = inputMovie.Duration,
                ReleaseDate = releaseDate,
                Description = inputMovie.Description,
            };

            this.dbContext.Movies.Add(movie);
            this.dbContext.SaveChanges();

            return this.RedirectToAction(nameof(Index));        
        }

        [HttpGet]
        public IActionResult Details(string id) 
        { 
            Movie? movie = this.dbContext.Movies.FirstOrDefault(m => m.Id == int.Parse(id));

            if (movie == null) 
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }

    }
}

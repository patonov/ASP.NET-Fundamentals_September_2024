using CinemaApp.Web.Data;
using CinemaApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create(Movie movie) 
        {
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

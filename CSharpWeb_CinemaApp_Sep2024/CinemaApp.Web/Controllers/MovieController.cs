using CinemaApp.Web.Data;
using CinemaApp.Web.Models;
using CinemaApp.Web.ViewModels.Cinema;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await dbContext.Movies.ToListAsync();

            return this.View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieInputModel inputMovie) 
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

            await this.dbContext.Movies.AddAsync(movie);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));        
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id) 
        { 
            Movie? movie = await this.dbContext.Movies.FirstOrDefaultAsync(m => m.Id == int.Parse(id));

            if (movie == null) 
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> AddToProgram(string? id)
        {
            bool IsIdValidInt = int.TryParse(id, out int idValue);
            if (!IsIdValidInt) 
            { 
                return RedirectToAction(nameof(Index));
            }

            Movie? movie = await dbContext.Movies.FirstOrDefaultAsync(x => x.Id == idValue);

            if (movie == null) 
            {
                return RedirectToAction(nameof(Index));
            }

            AddMovieToCinemaInputModel model = new AddMovieToCinemaInputModel()
            {
                Id = id!,
                MovieTitle = movie.Title,
                Cinemas = await this.dbContext
                    .Cinemas.Include(c => c.CinemaMovies).ThenInclude(cm => cm.Movie)
                    .Select(c => new CinemaCheckBoxItemInputModel()
                    {
                        Id = c.Id.ToString(),
                        Name = c.Name,
                        Location = c.Location,
                        IsSelected = c.CinemaMovies.Any(cm => cm.Movie.Id == idValue)
                    }).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToProgram(AddMovieToCinemaInputModel model)
        {
            if (!this.ModelState.IsValid) 
            { 
                return View(model);
            }

            bool IsIdValidInt = int.TryParse(model.Id, out int idValue);
            if (!IsIdValidInt)
            {
                return RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext.Movies.FirstOrDefaultAsync(x => x.Id == idValue);

            if (movie == null) 
            { 
                return RedirectToAction(nameof(Index));
            }

            ICollection<CinemaMovie> entitiesToAdd = new List<CinemaMovie>();
            foreach (CinemaCheckBoxItemInputModel cinemaInput in model.Cinemas)
            {
                if (cinemaInput.IsSelected)
                {
                    bool IsCinemaInputIdValidInt = int.TryParse(cinemaInput.Id, out int cinemaInputValue);
                    if (!IsCinemaInputIdValidInt)
                    {
                        this.ModelState.AddModelError(string.Empty, "Invalid cinema selected.");
                        return View(model);
                    }

                    Cinema? cinema = await this.dbContext.Cinemas.FirstOrDefaultAsync(c => c.Id == cinemaInputValue);
                    if (cinema == null)
                    {
                        this.ModelState.AddModelError(string.Empty, "Invalid cinema selected.");
                        return View(model);
                    }

                    entitiesToAdd.Add(new CinemaMovie() 
                    { 
                        Cinema = cinema,
                        Movie = movie
                    });
                }
            }
            await this.dbContext.CinemasMovies.AddRangeAsync(entitiesToAdd);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index), "Cinema");
        }


    }
}

using CinemaApp.Web.Data;
using CinemaApp.Web.Models;
using CinemaApp.Web.ViewModels.Cinema;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web.Controllers
{
    public class CinemaController : Controller
    {
        private readonly CinemaDbContext cinemaDbContext;

        public CinemaController(CinemaDbContext cinemaDbContext)
        {
            this.cinemaDbContext = cinemaDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CinemaIndexViewModel> cinemas = await this.cinemaDbContext
                .Cinemas
                .Select(c => new CinemaIndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location,
                })
                .OrderBy(c => c.Location)
                .ToArrayAsync();

            return View(cinemas);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCinemaFormModel formModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(formModel);
            }

            Cinema cinema = new Cinema()
            {
                Name = formModel.Name,
                Location = formModel.Location,
            };

            await cinemaDbContext.Cinemas.AddAsync(cinema);
            await cinemaDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id) 
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction(nameof(Index));
            }

            bool IsIdValidInt = int.TryParse(id, out int idValue);

            if (!IsIdValidInt) 
            {
                return RedirectToAction(nameof(Index));
            }

            Cinema? cinema = await cinemaDbContext
                .Cinemas.Include(c => c.CinemaMovies)
                .ThenInclude(cm => cm.Movie)
                .FirstOrDefaultAsync(c => c.Id == idValue);

            if (cinema == null) 
            {
                return RedirectToAction(nameof(Index));
            }

            CinemaDetailsViewModel detailsViewModel = new CinemaDetailsViewModel()
            {
                Name = cinema.Name,
                Location = cinema.Location,
                Movies = cinema.CinemaMovies.Select(x => new CinemaMovieViewModel()
                {
                    Title = x.Movie.Title,
                    Duration = x.Movie.Duration,
                }).ToArray()
            };

            return View(detailsViewModel);
        }

    } 
}

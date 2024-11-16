using CinemaApp.Web.Data;
using CinemaApp.Web.Models;
using CinemaApp.Web.ViewModels.Cinema;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class CinemaController : Controller
    {
        private readonly CinemaDbContext cinemaDbContext;

        public CinemaController(CinemaDbContext cinemaDbContext)
        { 
            this.cinemaDbContext = cinemaDbContext;
        }

        public IActionResult Index()
        { 
            IEnumerable<CinemaIndexViewModel> cinemas = this.cinemaDbContext
                .Cinemas
                .Select(c => new CinemaIndexViewModel() 
                { 
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location,
                })
                .ToArray();

            return View(cinemas);
        }


    }
}

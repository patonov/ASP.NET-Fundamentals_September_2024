using CinemaApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace CinemaApp.Web.Controllers
{
    public class HomeController : Controller
    {        
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to our Cinema Web App!";
            return View();
        }

        
    }
}

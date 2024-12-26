using GameZone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add() 
        {
            var model = new GameViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GameViewModel model)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            var model = new GameViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GameViewModel model)
        { 
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyZone()
        {
            return View(new List<GameInfoViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> AddToMyZone(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StrikeOut(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) 
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            return View();
        }
    }
}

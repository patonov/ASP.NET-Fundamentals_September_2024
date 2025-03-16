using GameZone.Data;
using GameZone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using System.Security.Policy;

namespace GameZone.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private GameZoneDbContext _dbContext;

        public GameController(GameZoneDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await _dbContext.Games.Where(g => g.IsDeleted == false)
                .Select(g => new GameInfoViewModel()
                {
                    Id = g.Id,
                    Genre = g.Genre.Name,
                    ImageUrl = g.ImageUrl,
                    Publisher = g.Publisher.UserName ?? string.Empty,
                    ReleasedOn = g.ReleasedOn.ToString("yyyy-MM-dd"),
                    Title = g.Title,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var model = new GameViewModel();
            model.Genres = await _dbContext.Genres.ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GameViewModel model)
        {            
            if (ModelState.IsValid == false)
            {
                model.Genres = await _dbContext.Genres.ToListAsync();

                return View(model);
            }

            DateTime releasedOn;

            if (DateTime.TryParseExact(model.ReleasedOn, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out releasedOn) == false)
            {
                ModelState.AddModelError(nameof(model.ReleasedOn), "Invalid date format");
                model.Genres = await _dbContext.Genres.ToListAsync();

                return View(model);
            }

            Game game = new Game()
            {
                Description = model.Description,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
                PublisherId = GetCurrentUserId() ?? string.Empty,
                ReleasedOn = releasedOn,
                Title = model.Title
            };

            await _dbContext.Games.AddAsync(game);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            var model = await _dbContext.Games.Where(g => g.Id == id).Where(g => g.IsDeleted == false)
                .AsNoTracking()
                .Select(g => new GameViewModel() 
                { 
                Description = g.Description,
                GenreId = g.GenreId,
                ImageUrl = g.ImageUrl,
                ReleasedOn = g.ReleasedOn.ToString("yyyy-MM-dd"),
                Title = g.Title
                }).FirstOrDefaultAsync();

            model.Genres = await GetGenres();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GameViewModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                model.Genres = await _dbContext.Genres.ToListAsync();

                return View(model);
            }

            DateTime releasedOn;

            if (DateTime.TryParseExact(model.ReleasedOn, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out releasedOn) == false)
            {
                ModelState.AddModelError(nameof(model.ReleasedOn), "Invalid date format");
                model.Genres = await _dbContext.Genres.ToListAsync();

                return View(model);
            }

            Game? entity = await _dbContext.Games.FindAsync(id);
            if (entity == null || entity.IsDeleted == true) 
            {
                throw new ArgumentException("Invalid Id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (entity.PublisherId != currentUserId) 
            {
                RedirectToAction(nameof(All));
            }

            entity.Description = model.Description;
            entity.GenreId = model.GenreId;
            entity.ImageUrl = model.ImageUrl;
            entity.PublisherId = currentUserId;
            entity.ReleasedOn = releasedOn;
            entity.Title = model.Title;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> MyZone()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            var model = await _dbContext.Games
                .Where(g => g.IsDeleted == false)
                .Where(g => g.GamersGames.Any(gr => gr.GamerId == currentUserId))
                .Select(g => new GameInfoViewModel()
                {
                    Id = g.Id,
                    Genre = g.Genre.Name,
                    ImageUrl = g.ImageUrl,
                    Publisher = g.Publisher.UserName ?? string.Empty,
                    ReleasedOn = g.ReleasedOn.ToString("yyyy-MM-dd"),
                    Title = g.Title,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddToMyZone(int id)
        {
            Game? entity = await _dbContext.Games
                .Where(g => g.Id == id)
                .Include(g => g.GamersGames)
                .FirstOrDefaultAsync();

            if (entity == null || entity.IsDeleted == true)
            {
                throw new ArgumentException("Invalid Id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (entity.GamersGames.Any(gr => gr.GamerId == currentUserId))
            {
                RedirectToAction(nameof(All));
            }

            entity.GamersGames.Add(new GamerGame()
            {
                GamerId = currentUserId,
                GameId = entity.Id
            });

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(MyZone));
        }

        [HttpGet]
        public async Task<IActionResult> StrikeOut(int id)
        {
            Game? entity = await _dbContext.Games
                .Where(g => g.Id == id)
                .Include(g => g.GamersGames)
                .FirstOrDefaultAsync();

            if (entity == null || entity.IsDeleted == true)
            {
                throw new ArgumentException("Invalid Id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            GamerGame? gamerGame = entity.GamersGames.FirstOrDefault(gr => gr.GamerId == currentUserId);

            if (gamerGame != null)
            {
                entity.GamersGames.Remove(gamerGame);

                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MyZone));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) 
        {
            var model = await _dbContext.Games.Where(g => g.Id == id).Where(g => g.IsDeleted == false)
                .AsNoTracking()
                .Select(g => new GameDetailsViewModel()
                {
                    Id = g.Id,
                    Description = g.Description,
                    Genre = g.Genre.Name,
                    ImageUrl = g.ImageUrl,
                    ReleasedOn = g.ReleasedOn.ToString("yyyy-MM-dd"),
                    Title = g.Title,
                    Publisher = g.Publisher.UserName ?? string.Empty,
                }).FirstOrDefaultAsync();
                        
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            var model = await _dbContext.Games.Where(g => g.Id == id).Where(g => g.IsDeleted == false)
                .AsNoTracking()
                .Select(g => new DeleteViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    Publisher = g.Publisher.UserName ?? string.Empty,
                }).FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(DeleteViewModel model)
        {
            Game? game = await _dbContext.Games.Where(g => g.Id == model.Id).Where(g => g.IsDeleted == false).FirstOrDefaultAsync();

            if (game != null)
            { 
                game.IsDeleted = true;

                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(All));
        }

        private string? GetCurrentUserId()
        { 
           return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private async Task<List<Genre>> GetGenres()
        { 
            return await _dbContext.Genres.ToListAsync();
        }
    }
}

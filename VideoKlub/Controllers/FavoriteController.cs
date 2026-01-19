using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideoKlub.Models;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public FavoriteController(IFavoriteRepository favoriteRepository, IVideoRepository videoRepository, UserManager<IdentityUser> userManager)
        {
            _favoriteRepository = favoriteRepository;
            _videoRepository = videoRepository;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var favorites = await _favoriteRepository.GetUserFavoritesAsync(userId);

            return View(favorites);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int videoId)
        {
            var userId = _userManager.GetUserId(User);

            var video = await _videoRepository.GetByIdAsync(videoId);
            if(video == null)
            {
                return NotFound();
            }

            var favorite = await _favoriteRepository.GetAsync(videoId, userId);


            if(favorite != null)
            {
                _favoriteRepository.Remove(favorite);
                TempData["FavoriteMessage"] = "Video uklonjen iz omiljenih!";
                TempData["FavoriteType"] = "error";
            }
            else
            {
                await _favoriteRepository.AddAsync(new Favorite
                {
                    VideoId = videoId,
                    UserId = userId
                });
                TempData["FavoriteMessage"] = "Video dodat u omiljene!";
                TempData["FavoriteType"] = "success";
            }

            await _favoriteRepository.SaveAsync();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}

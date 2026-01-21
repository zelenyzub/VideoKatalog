using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VideoKlub.Repositories.Interfaces;
using VideoKlub.Models;
using System.Security.Claims;

namespace VideoKlub.Controllers
{
    [Authorize]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public VideoController(IVideoRepository videoRepository, IRateRepository rateRepository, IFavoriteRepository favoriteRepository)
        {
            _videoRepository = videoRepository;
            _rateRepository = rateRepository;
            _favoriteRepository = favoriteRepository;
        }
        public async Task<IActionResult> Index()
        {
            var videos = await _videoRepository.GetAllWithCategoryAsync();
            return View(videos);
        }

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index");
            }

            var videos = await _videoRepository.SearchByTitleOrDescriptionAsync(query);
            ViewData["SearchQuery"] = query;
            return View("Index", videos);
        }

        public async Task<IActionResult> FilterByCategory(int[] categoryIds)
        {
            if(categoryIds == null || categoryIds.Length == 0)
            {
                return RedirectToAction("Index");
            }
            var videos = await _videoRepository.GetByCategoryAsync(categoryIds);

            ViewData["FilterActive"] = true;
            ViewData["SelectedCategories"] = categoryIds;

            return View("Index", videos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var video = await _videoRepository.GetByIdWithCategoryAsync(id);

            if(video == null)
            {
                return NotFound();
            }

            string userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                video.IsFavorite = await _favoriteRepository.IsFavoriteAsync(id, userId);
            }

            var avgRating = await _rateRepository.GetAverageRatingAsync(id);
            ViewBag.AverageRating = avgRating;

            var userRate = await _rateRepository.GetUserRatingForVideoAsync(userId, id);
            ViewBag.UserHasRated = userRate != null;
            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateVideo(int videoId, int value)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingRate = await _rateRepository.GetUserRatingForVideoAsync(userId, videoId);
            if(existingRate != null)
            {
                existingRate.Value = value;
                existingRate.Timestamp = DateTime.UtcNow;
            }
            else
            {
                var rate = new Rate
                {
                    VideoId = videoId,
                    UserId = userId,
                    Value = value
                };

                await _rateRepository.AddAsync(rate);
                await _rateRepository.SaveAsync();
                TempData["FavoriteMessage"] = "Uspešno ocenjen video sadržaj!";
                TempData["FavoriteType"] = "success";

            }
            return RedirectToAction("Details", new { id = videoId });

        }
    }
}

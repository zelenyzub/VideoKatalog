using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VideoKlub.Repositories.Interfaces;
using VideoKlub.Models;
using System.Security.Claims;

namespace VideoKlub.Controllers
{

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
            if (User.Identity?.IsAuthenticated == true)
            {
                await MarkFavoritesAsync(videos);
            }
            return View(videos);
        }

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index");
            }

            var videos = await _videoRepository.SearchByTitleOrDescriptionAsync(query);
            if (User.Identity?.IsAuthenticated == true)
            {
                await MarkFavoritesAsync(videos);
            }
            ViewData["SearchQuery"] = query;
            return View("Index", videos);
        }

        [Authorize]
        public async Task<IActionResult> Recommended()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var favorites = await _favoriteRepository.GetUserFavoritesAsync(userId);
            var ratedVideos = await _rateRepository.GetUserRatesWithCategoryAsync(userId);

            var ratedVideoIds = ratedVideos.Select(r => r.VideoId).Distinct().ToHashSet();

            var categoryScores = new Dictionary<int, double>();

            var favoriteCategoryGroups = favorites
                .Where(f => f.Video?.Category != null)
                .GroupBy(f => f.Video.CategoryId);

            foreach (var group in favoriteCategoryGroups)
            {
                categoryScores[group.Key] = categoryScores.GetValueOrDefault(group.Key) + group.Count() * 2;
            }

            var ratedCategoryGroups = ratedVideos
                .Where(r => r.Video?.Category != null)
                .GroupBy(r => r.Video.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    AvgRating = g.Average(r => r.Value)
                });

            foreach (var group in ratedCategoryGroups)
            {
                categoryScores[group.CategoryId] = categoryScores.GetValueOrDefault(group.CategoryId) + group.AvgRating;
            }

            var allVideos = (await _videoRepository.GetAllWithCategoryAsync())
                .Where(v => v.IsActive && !ratedVideoIds.Contains(v.Id))
                .ToList();

            IEnumerable<Video> recommended;
            if (categoryScores.Any())
            {
                recommended = allVideos
                    .Where(v => categoryScores.ContainsKey(v.CategoryId))
                    .OrderByDescending(v => categoryScores[v.CategoryId])
                    .ThenBy(v => v.Title)
                    .ToList();

                if (!recommended.Any())
                {
                    recommended = allVideos
                        .OrderByDescending(v => categoryScores.GetValueOrDefault(v.CategoryId))
                        .ThenBy(v => v.Title)
                        .ToList();
                }
            }
            else
            {
                recommended = allVideos.OrderBy(v => v.Title).ToList();
            }

            await MarkFavoritesAsync(recommended);

            ViewData["PageHeading"] = "Preporučeno za vas";
            ViewData["PageSubheading"] = $"Sadržaji koje vam sistem preporučuje na osnovu vaših omiljenih i ocenjenih kategorija.";

            return View("Index", recommended);
        }

        public async Task<IActionResult> FilterByCategory(int[] categoryIds)
        {
            if(categoryIds == null || categoryIds.Length == 0)
            {
                return RedirectToAction("Index");
            }
            var videos = await _videoRepository.GetByCategoryAsync(categoryIds);
            if (User.Identity?.IsAuthenticated == true)
            {
                await MarkFavoritesAsync(videos);
            }

            ViewData["FilterActive"] = true;
            ViewData["SelectedCategories"] = categoryIds;

            return View("Index", videos);
        }

        private async Task MarkFavoritesAsync(IEnumerable<Video> videos)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            var favorites = await _favoriteRepository.GetUserFavoritesAsync(userId);
            var favoriteIds = favorites.Select(f => f.VideoId).ToHashSet();

            foreach (var video in videos)
            {
                video.IsFavorite = favoriteIds.Contains(video.Id);
            }
        }
        [Authorize]
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
        [Authorize]
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

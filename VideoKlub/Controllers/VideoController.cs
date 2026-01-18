using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Controllers
{
    [Authorize]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;

        public VideoController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
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
    }
}

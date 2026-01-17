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
            var videos = await _videoRepository.GetAllAsync();
            return View(videos);
        }
    }
}

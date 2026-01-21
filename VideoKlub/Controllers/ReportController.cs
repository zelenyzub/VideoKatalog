using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Popular()
        {
            var data = await _reportRepository.GetMostPopularVideosAsync();

            if (!data.Any())
                ViewBag.Message = "Nema podataka za ovaj izveštaj.";

            return View(data);
        }

        public async Task<IActionResult> TopRated()
        {
            var data = await _reportRepository.GetTopRatedVideosAsync();

            if (!data.Any())
                ViewBag.Message = "Nema podataka za ovaj izveštaj.";

            return View(data);
        }

        public async Task<IActionResult> AvgByCategory()
        {
            var data = await _reportRepository.GetAverageRatingByCategoryAsync();

            if (!data.Any())
                ViewBag.Message = "Nema podataka za ovaj izveštaj.";

            return View(data);
        }

        public async Task<IActionResult> UserActivity()
        {
            var data = await _reportRepository.GetUserActivityAsync();

            if (!data.Any())
                ViewBag.Message = "Nema podataka za ovaj izveštaj.";

            return View(data);
        }
    }
}

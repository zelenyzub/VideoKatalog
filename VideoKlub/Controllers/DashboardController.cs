using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VideoKlub.Models;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoriRepository;
        private readonly IWebHostEnvironment _env;


        public DashboardController(
            IVideoRepository videoRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment env)
        {
            _videoRepository = videoRepository;
            _categoriRepository = categoryRepository;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var videos = await _videoRepository.GetAllWithCategoryAdminAsync();
            var categories = await _categoriRepository.GetAllAsync();

            ViewBag.Categories = categories;
            return View(videos);
        }

        public async Task<IActionResult> AddVideo()
        {
            ViewBag.Categories = await _categoriRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVideo(Video v)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoriRepository.GetAllAsync();
                TempData["FavoriteMessage"] = "Greška prilikom čuvanja novog sadržaja";
                TempData["FavoriteType"] = "error";
                return View(v);
            }

            if (v.ImageFile != null && v.ImageFile.Length > 0) 
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(v.ImageFile.FileName).ToLower();

                if(!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ImageFile", "Samo .jpg, .jpeg i .png ekstenzije su dozvoljene.");
                    ViewBag.Categories = await _categoriRepository.GetAllAsync();
                    return View(v);
                }

                var fileName = Guid.NewGuid().ToString() + extension;
                var uploadPath = Path.Combine(_env.WebRootPath, "VideoCoverImages");

                if(!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await v.ImageFile.CopyToAsync(stream);
                }

                v.ImagePath = "VideoCoverImages/" + fileName;
            }
            await _videoRepository.AddAsync(v);
            await _videoRepository.SaveAsync();
            TempData["FavoriteMessage"] = "Uspešno sačuvan novi sadržaj!";
            TempData["FavoriteType"] = "success";

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            if(video == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _categoriRepository.GetAllAsync();
            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Video v)
        {
            ModelState.Remove("ImageFile");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoriRepository.GetAllAsync();
                return View(v);
            }

            var video = await _videoRepository.GetByIdAsync(v.Id);

            if(video == null)
            {
                return NotFound();
            }

            video.Title = v.Title;
            video.Description = v.Description;
            video.Duration = v.Duration;
            video.URL = v.URL;
            video.CategoryId = v.CategoryId;

            _videoRepository.Update(video);
            await _videoRepository.SaveAsync();

            TempData["FavoriteMessage"] = "Uspešno izmenjen sadržaj!";
            TempData["FavoriteType"] = "success";

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            if(video == null)
            {
                return NotFound();
            }

            _videoRepository.Delete(video);
            await _videoRepository.SaveAsync();

            TempData["FavoriteMessage"] = "Uspešno obrisan sadržaj - " + video.Title;
            TempData["FavoriteType"] = "success";

            return RedirectToAction("Index", "Dashboard");
        }
    }
}

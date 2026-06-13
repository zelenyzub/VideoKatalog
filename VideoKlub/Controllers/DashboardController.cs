using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VideoKlub.Models;
using VideoKlub.Repositories.Implementation;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Controllers
{
    
    [Authorize(Roles = "Admin,Moderator")]
    public class DashboardController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoriRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IUserRepository _userRepository;
        private readonly IOmdbRepository _omdbRepository;


        public DashboardController(
            IVideoRepository videoRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment env,
            IUserRepository userRepository,
            IOmdbRepository omdbRepository)
        {
            _videoRepository = videoRepository;
            _categoriRepository = categoryRepository;
            _env = env;
            _userRepository = userRepository;
            _omdbRepository = omdbRepository;
        }
        public async Task<IActionResult> Index(string searchQuery, int? categoryId, string statusFilter = "all", int pageNumber = 1)
        {
            const int pageSize = 10;

            var (videos, totalCount) = await _videoRepository.GetFilteredVideosAsync(searchQuery, categoryId, statusFilter, pageNumber, pageSize);
            var categories = await _categoriRepository.GetAllAsync();
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var users = User.IsInRole("Admin") ? await _userRepository.GetAllUsersAsync(currentUserId) : null;

            ViewBag.Users = users;
            ViewBag.Categories = categories;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.StatusFilter = statusFilter;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(videos);
        }

        public async Task<IActionResult> AddVideo()
        {
            ViewBag.Categories = await _categoriRepository.GetAllAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchOmdb(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new JsonResult(Array.Empty<object>(), new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = null });
            }

            try
            {
                var searchResults = await _omdbRepository.SearchAsync(query);
                return new JsonResult(searchResults.Select(x => new
                {
                    x.Title,
                    x.Year,
                    x.Poster,
                    x.imdbID
                }), new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = null });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message }, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = null }) { StatusCode = 500 };
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOmdbDetails(string imdbId)
        {
            if (string.IsNullOrWhiteSpace(imdbId))
            {
                return new JsonResult(null, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = null });
            }

            try
            {
                var movie = await _omdbRepository.GetByImdbIdAsync(imdbId);
                if (movie == null)
                {
                    return new JsonResult(null, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = null });
                }

                return new JsonResult(new
                {
                    movie.Title,
                    movie.Plot,
                    movie.Runtime,
                    movie.Poster,
                    movie.imdbID,
                    movie.Year
                }, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = null });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message }, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = null }) { StatusCode = 500 };
            }
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
            else if (!string.IsNullOrWhiteSpace(v.ImageUrl))
            {
                var imageBytes = await _omdbRepository.DownloadImageAsync(v.ImageUrl);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    var extension = Path.GetExtension(v.ImageUrl).ToLower();
                    if (string.IsNullOrWhiteSpace(extension) || !new[] { ".jpg", ".jpeg", ".png" }.Contains(extension))
                    {
                        extension = ".jpg";
                    }

                    var fileName = Guid.NewGuid().ToString() + extension;
                    var uploadPath = Path.Combine(_env.WebRootPath, "VideoCoverImages");

                    if(!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    var filePath = Path.Combine(uploadPath, fileName);
                    await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                    v.ImagePath = "VideoCoverImages/" + fileName;
                }
            }

            if (User.IsInRole("Moderator"))
            {
                v.IsActive = false;
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
            if (!User.IsInRole("Moderator"))
            {
                video.IsActive = v.IsActive;
            }

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

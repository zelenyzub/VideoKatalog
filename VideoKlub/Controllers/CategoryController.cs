using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VideoKlub.Models;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Add()
        {
            return PartialView("_AddCategoryModal", new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(string Name)
        {
            if (!ModelState.IsValid)
            {
                TempData["FavoriteMessage"] = "Morate uneti naziv nove kategorija!";
                TempData["FavoriteType"] = "error";
                return RedirectToAction("Index", "Dashboard");
            }

            var category = new Category { Name = Name };
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();

            TempData["FavoriteMessage"] = "Uspešno ste dodali kategoriju - " + Name;
            TempData["FavoriteType"] = "success";

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveAsync();

            TempData["FavoriteMessage"] = "Uspešno ste obrisali kategoriju - " + category.Name;
            TempData["FavoriteType"] = "success";

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return PartialView("_EditCategoryModal", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int Id, string Name)
        {
            if (!ModelState.IsValid)
            {
                TempData["FavoriteMessage"] = "Morate uneti naziv kategorija!";
                TempData["FavoriteType"] = "error";
                return RedirectToAction("Index", "Dashboard");
            }

            var category = await _categoryRepository.GetByIdAsync(Id);
            if (category == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var oldName = category.Name;

            category.Name = Name;
            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();

            TempData["FavoriteMessage"] = "Naziv kategorije je uspešno izmenjen iz " + oldName + " u " + Name;
            TempData["FavoriteType"] = "success";

            return RedirectToAction("Index", "Dashboard");
        }


    }
}

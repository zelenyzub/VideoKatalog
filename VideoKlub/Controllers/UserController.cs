using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VideoKlub.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace VideoKlub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var users = await _userRepository.GetAllUsersAsync(currentUserId);

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enable(string id)
        {
            await _userRepository.EnableUserAsync(id);
            TempData["FavoriteMessage"] = "Korisnik je aktiviran.";
            TempData["FavoriteType"] = "info";
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable(string id)
        {
            await _userRepository.DisableUserAsync(id);
            TempData["FavoriteMessage"] = "Korisnik je deaktiviran.";
            TempData["FavoriteType"] = "info";
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await _userRepository.DeleteUserAsync(id);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string userId, string newRole)
        {
            await _userRepository.ChangeUserRoleAsync(userId, newRole);

            string role = null;
            if(newRole == "Admin")
            {
                role = "Administrator";
            }
            else
            {
                role = "Korisnik";
            }
            TempData["FavoriteMessage"] = "Uloga korisnika je promenjena u " + role;
            TempData["FavoriteType"] = "info";
            return RedirectToAction("Index", "Dashboard");
        }

    }
}

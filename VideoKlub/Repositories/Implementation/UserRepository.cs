using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoKlub.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoKlub.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Retrieves all users from the identity store
        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync(string currentUserId = null)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(currentUserId))
            {
                query = query.Where(u => u.Id != currentUserId);
            }

            return await query.ToListAsync();
        }

        // Retrieves a user by their unique identifier
        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        // Enables a user by setting LockoutEnabled to false
        public async Task EnableUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.LockoutEnabled = false;
                user.LockoutEnd = null; // odmah otključaj korisnika
                await _userManager.UpdateAsync(user);
            }
        }

        // Disables a user by setting LockoutEnabled to true and LockoutEnd to MaxValue
        public async Task DisableUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.MaxValue; // trajno zaključa korisnika
                await _userManager.UpdateAsync(user);
            }
        }

        // Deletes a user from the identity store
        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        // Changes the role of a user to a new specified role
        public async Task ChangeUserRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return;

            var currentRoles = await _userManager.GetRolesAsync(user);

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!_roleManager.Roles.Any(r => r.Name == newRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(newRole));
            }

            await _userManager.AddToRoleAsync(user, newRole);
        }
    }
}

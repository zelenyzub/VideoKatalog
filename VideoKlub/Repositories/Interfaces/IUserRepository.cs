using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoKlub.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync(string currentUserId = null);
        Task<IdentityUser> GetUserByIdAsync(string id);
        Task EnableUserAsync(string id);
        Task DisableUserAsync(string id);
        Task DeleteUserAsync(string id);
        Task ChangeUserRoleAsync(string userId, string newRole);

    }
}

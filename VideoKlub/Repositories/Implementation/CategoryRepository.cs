using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using VideoKlub.Data;
using VideoKlub.Models;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Repositories.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Get all categories
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}

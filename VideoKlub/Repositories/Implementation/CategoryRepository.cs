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
    }
}

using VideoKlub.Models;

namespace VideoKlub.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}

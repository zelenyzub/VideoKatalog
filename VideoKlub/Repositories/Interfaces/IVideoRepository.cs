using VideoKlub.Models;

namespace VideoKlub.Repositories.Interfaces
{
    public interface IVideoRepository: IGenericRepository<Video>
    {
        Task<IEnumerable<Video>> GetByTitleAsync(string title);

        Task<IEnumerable<Video>> GetAllWithCategoryAsync();
        Task<Video> GetByIdWithCategoryAsync(int id);
        Task<IEnumerable<Video>> SearchByTitleOrDescriptionAsync(string query);
        Task<IEnumerable<Video>> GetByCategoryAsync(int[] categoryIds);
        Task<IEnumerable<Video>> GetAllWithCategoryAdminAsync();
    }
}

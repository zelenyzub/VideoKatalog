using VideoKlub.Models;

namespace VideoKlub.Repositories.Interfaces
{
    public interface IVideoRepository: IGenericRepository<Video>
    {
        Task<IEnumerable<Video>> GetByTitleAsync(string title);
    }
}

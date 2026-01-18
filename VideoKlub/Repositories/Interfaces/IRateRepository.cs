using VideoKlub.Models;

namespace VideoKlub.Repositories.Interfaces
{
    public interface IRateRepository : IGenericRepository<Rate>
    {
        Task<double> GetAverageRatingAsync(int videoId);
        Task<Rate> GetUserRatingForVideoAsync(string userId, int videoId);
    }
}

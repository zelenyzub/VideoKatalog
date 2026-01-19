using VideoKlub.Models;

namespace VideoKlub.Repositories.Interfaces
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<bool> IsFavoriteAsync(int videoId, string userId);
        Task<Favorite?> GetAsync(int videoId, string userId);
        Task<List<Favorite>> GetUserFavoritesAsync(string userId);
    }
}

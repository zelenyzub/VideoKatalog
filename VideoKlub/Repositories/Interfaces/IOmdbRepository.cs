using VideoKlub.Models;

namespace VideoKlub.Repositories.Interfaces
{
    public interface IOmdbRepository
    {
        Task<IEnumerable<OmdbSearchItemDto>> SearchAsync(string query);
        Task<OmdbMovieDto?> GetByImdbIdAsync(string imdbId);
        Task<byte[]?> DownloadImageAsync(string imageUrl);
    }
}

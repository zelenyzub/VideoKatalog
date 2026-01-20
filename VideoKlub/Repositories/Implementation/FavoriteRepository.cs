using Microsoft.EntityFrameworkCore;
using VideoKlub.Data;
using VideoKlub.Models;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Repositories.Implementation
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Check if a video is favorited by a user
        public async Task<bool> IsFavoriteAsync(int videoId, string userId)
        {
            return await _context.Favorites.AnyAsync(f => f.VideoId == videoId && f.UserId == userId);
        }

        // Get a specific favorite entry
        public async Task<Favorite?> GetAsync(int videoId, string userId)
        {
            return await _context.Favorites.FirstOrDefaultAsync(f => f.VideoId == videoId && f.UserId == userId);
        }

        // Get all favorites for a user with video and category details
        public async Task<List<Favorite>> GetUserFavoritesAsync(string userId)
        {
            return await _context.Favorites
                .Include(f => f.Video)
                .ThenInclude(v => v.Category)
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.Timestamp)
                .ToListAsync();
        }
    }
}

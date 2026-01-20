using Microsoft.EntityFrameworkCore;
using VideoKlub.Data;
using VideoKlub.Models;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Repositories.Implementation
{
    public class RateRepository : GenericRepository<Rate>, IRateRepository
    {
        public RateRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Get average rating for a specific video
        public async Task<double> GetAverageRatingAsync(int videoId)
        {
            return await _context.Rates
                .Where(r => r.VideoId == videoId)
                .AverageAsync(r => (double?)r.Value) ?? 0;
        }

        // Get rating given by a specific user for a specific video
        public async Task<Rate> GetUserRatingForVideoAsync(string userId, int videoId)
        {
            return await _context.Rates
                .FirstOrDefaultAsync(r => r.UserId == userId && r.VideoId == videoId);
        }
    }
}

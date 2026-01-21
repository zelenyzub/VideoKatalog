using Microsoft.EntityFrameworkCore;
using VideoKlub.Data;
using VideoKlub.Repositories.Interfaces;
using VideoKlub.ViewModels.Reports;

namespace VideoKlub.Repositories.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1️⃣ Najpopularniji (po favoritima)
        public async Task<List<PopularVideosReportVM>> GetMostPopularVideosAsync()
        {
            return await _context.Favorites
                .GroupBy(f => f.Video)
                .Select(g => new PopularVideosReportVM
                {
                    VideoTitle = g.Key.Title,
                    FavoritesCount = g.Count()
                })
                .OrderByDescending(x => x.FavoritesCount)
                .ToListAsync();
        }

        // 2️⃣ Najbolje ocenjeni
        public async Task<List<TopRatedVideosReportVM>> GetTopRatedVideosAsync()
        {
            return await _context.Rates
                .GroupBy(r => r.Video)
                .Select(g => new TopRatedVideosReportVM
                {
                    VideoTitle = g.Key.Title,
                    AverageRating = g.Average(x => x.Value)
                })
                .OrderByDescending(x => x.AverageRating)
                .ToListAsync();
        }

        // 3️⃣ Prosečna ocena po kategoriji
        public async Task<List<AvgRatingByCategoryReportVM>> GetAverageRatingByCategoryAsync()
        {
            return await _context.Rates
                .Include(r => r.Video)
                .ThenInclude(v => v.Category)
                .GroupBy(r => r.Video.Category.Name)
                .Select(g => new AvgRatingByCategoryReportVM
                {
                    CategoryName = g.Key,
                    AverageRating = g.Average(x => x.Value)
                })
                .ToListAsync();
        }

        // 4️⃣ Aktivnost korisnika
        public async Task<List<UserActivityReportVM>> GetUserActivityAsync()
        {
            return await _context.Users
                .Select(u => new UserActivityReportVM
                {
                    UserEmail = u.Email,
                    RatesCount = _context.Rates.Count(r => r.UserId == u.Id),
                    FavoritesCount = _context.Favorites.Count(f => f.UserId == u.Id)
                })
                .ToListAsync();
        }
    }
}

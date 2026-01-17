using Microsoft.EntityFrameworkCore;
using VideoKlub.Data;
using VideoKlub.Models;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Repositories.Implementation
{
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        public VideoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Video>> GetByTitleAsync(string title)
        {
            return await _context.Videos
                .Where(v => v.Title == title)
                .ToListAsync();
        }
    }
}

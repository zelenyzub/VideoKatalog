using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<Video>> GetAllWithCategoryAsync()
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Where(v => v.IsActive)
                .ToListAsync();
        }

        public async Task<Video> GetByIdWithCategoryAsync(int id)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Video>> SearchByTitleOrDescriptionAsync(string query)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Where(v => v.IsActive && (v.Title.Contains(query) || v.Description.Contains(query)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Video>> GetByCategoryAsync(int[] categoryIds)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Where(v => v.IsActive && categoryIds.Contains(v.CategoryId))
                .ToListAsync();
        }
    }
}

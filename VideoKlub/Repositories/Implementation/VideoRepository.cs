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

        // Get videos by exact title match
        public async Task<IEnumerable<Video>> GetByTitleAsync(string title)
        {
            return await _context.Videos
                .Where(v => v.Title == title)
                .ToListAsync();
        }

        // Get all active videos with category
        public async Task<IEnumerable<Video>> GetAllWithCategoryAsync()
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Where(v => v.IsActive)
                .ToListAsync();
        }

        // Get single video by ID with category
        public async Task<Video> GetByIdWithCategoryAsync(int id)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        // Search videos by title or description
        public async Task<IEnumerable<Video>> SearchByTitleOrDescriptionAsync(string query)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Where(v => v.IsActive && (v.Title.Contains(query) || v.Description.Contains(query)))
                .ToListAsync();
        }

        // Get videos by multiple category IDs
        public async Task<IEnumerable<Video>> GetByCategoryAsync(int[] categoryIds)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Where(v => v.IsActive && categoryIds.Contains(v.CategoryId))
                .ToListAsync();
        }

        // Admin method to get all videos including inactive ones
        public async Task<IEnumerable<Video>> GetAllWithCategoryAdminAsync()
        {
            return await _context.Videos
                .Include(v => v.Category)
                .ToListAsync();
        }
    }
}

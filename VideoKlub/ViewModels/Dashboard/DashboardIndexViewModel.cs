using VideoKlub.Models;

namespace VideoKlub.ViewModels.Dashboard
{
    public class DashboardIndexViewModel
    {
        public IEnumerable<Video> Videos { get; set; } = Enumerable.Empty<Video>();
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
        public string SearchQuery { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string StatusFilter { get; set; } = "all";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalItems / PageSize) : 0;
    }
}

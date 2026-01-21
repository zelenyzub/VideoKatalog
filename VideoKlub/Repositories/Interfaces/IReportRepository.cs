using VideoKlub.ViewModels.Reports;

namespace VideoKlub.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<List<PopularVideosReportVM>> GetMostPopularVideosAsync();
        Task<List<TopRatedVideosReportVM>> GetTopRatedVideosAsync();
        Task<List<AvgRatingByCategoryReportVM>> GetAverageRatingByCategoryAsync();
        Task<List<UserActivityReportVM>> GetUserActivityAsync();
    }
}

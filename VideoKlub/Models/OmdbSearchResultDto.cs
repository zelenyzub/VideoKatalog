namespace VideoKlub.Models
{
    public class OmdbSearchResultDto
    {
        public List<OmdbSearchItemDto>? Search { get; set; }
        public string? totalResults { get; set; }
        public string? Response { get; set; }
        public string? Error { get; set; }
    }
}

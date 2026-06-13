using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using VideoKlub.Models;
using VideoKlub.Repositories.Interfaces;

namespace VideoKlub.Repositories.Implementation
{
    public class OmdbRepository : IOmdbRepository
    {
        private readonly HttpClient _httpClient;
        private readonly OmdbSettings _settings;

        public OmdbRepository(HttpClient httpClient, IOptions<OmdbSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<IEnumerable<OmdbSearchItemDto>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query) || string.IsNullOrWhiteSpace(_settings.BaseUrl) || string.IsNullOrWhiteSpace(_settings.ApiKey))
            {
                throw new InvalidOperationException("OMDb API ključ nije konfigurisan.");
            }

            if (_settings.ApiKey.Contains("TVOJ_API_KLJUC") || _settings.ApiKey.Contains("YOUR_API_KEY") || _settings.ApiKey.Contains("API_KEY"))
            {
                throw new InvalidOperationException("OMDb API ključ u konfiguraciji izgleda kao placeholder. Zamenite ga pravim ključem.");
            }

            try
            {
                var url = $"{_settings.BaseUrl}?apikey={_settings.ApiKey}&s={Uri.EscapeDataString(query)}";
                var response = await _httpClient.GetFromJsonAsync<OmdbSearchResultDto>(url);

                if (response == null)
                {
                    return Enumerable.Empty<OmdbSearchItemDto>();
                }

                if (response.Response?.ToLower() != "true")
                {
                    if (!string.IsNullOrWhiteSpace(response.Error) && response.Error != "Movie not found!")
                    {
                        throw new InvalidOperationException(response.Error);
                    }

                    return Enumerable.Empty<OmdbSearchItemDto>();
                }

                return response.Search ?? Enumerable.Empty<OmdbSearchItemDto>();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Odgovor OMDb API-ja nije validan JSON.", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Ne mogu da se povežem na OMDb API.", ex);
            }
        }

        public async Task<OmdbMovieDto?> GetByImdbIdAsync(string imdbId)
        {
            if (string.IsNullOrWhiteSpace(imdbId) || string.IsNullOrWhiteSpace(_settings.BaseUrl) || string.IsNullOrWhiteSpace(_settings.ApiKey))
            {
                return null;
            }

            try
            {
                var url = $"{_settings.BaseUrl}?apikey={_settings.ApiKey}&i={Uri.EscapeDataString(imdbId)}&plot=full";
                var response = await _httpClient.GetFromJsonAsync<OmdbMovieDto>(url);
                return response?.Response?.ToLower() == "true" ? response : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<byte[]?> DownloadImageAsync(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return null;
            }

            try
            {
                return await _httpClient.GetByteArrayAsync(imageUrl);
            }
            catch
            {
                return null;
            }
        }
    }
}

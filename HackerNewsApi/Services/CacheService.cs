using HackerNewsApi.Models;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsApi.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly HttpClient _client;

        public CacheService(IHttpClientFactory clientFactory, IMemoryCache cache)
        {
            _client = clientFactory.CreateClient("HackerNewsClient");
            _cache = cache;
        }

        private async Task<Story?> GetStoryByIdAsync(int id)
        {
            var cacheKey = $"story_{id}";
            if (!_cache.TryGetValue(cacheKey, out Story? story))
            {
                try
                {
                    story = await _client.GetFromJsonAsync<Story>($"item/{id}.json?print=pretty");
                }
                catch (Exception)
                {
                    story = await _client.GetFromJsonAsync<Story>($"item/{id}.json?print=pretty");
                }
                if (story != null)
                    _cache.Set(cacheKey, story, TimeSpan.FromMinutes(30));
            }
            return story;
        }

        public async Task<ResponseData> GetStoriesAsync(string query, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var ids = await _client.GetFromJsonAsync<List<int>>($"newstories.json?print=pretty", cancellationToken);

            if (ids == null || !ids.Any())
            {
                return new ResponseData();
            }

            var tasks = ids.Select(async id => await GetStoryByIdAsync(id));

            var stories = (await Task.WhenAll(tasks)).Where(s => s != null).Cast<Story>().ToList();

            var filteredStories = string.IsNullOrEmpty(query)
                ? stories
                : stories.Where(s => !string.IsNullOrEmpty(s.Title) && s.Title.Contains(query, StringComparison.OrdinalIgnoreCase));

            var paginatedStories = filteredStories.Skip((page - 1) * pageSize).Take(pageSize);
            return new ResponseData
            {
                Total = filteredStories.Count(),
                Page = page,
                PageSize = pageSize,
                Stories = paginatedStories
            };
        }
    }
}

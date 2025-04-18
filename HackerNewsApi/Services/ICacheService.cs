using HackerNewsApi.Models;

namespace HackerNewsApi.Services
{
    public interface ICacheService
    {
        Task<ResponseData> GetStoriesAsync(string query, int page, int pageSize, CancellationToken cancellationToken = default);
    }
}

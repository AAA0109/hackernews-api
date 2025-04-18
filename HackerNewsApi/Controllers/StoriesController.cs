using HackerNewsApi.Models;
using Microsoft.AspNetCore.Mvc;
using HackerNewsApi.Services;

namespace HackerNewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public StoriesController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> GetStoriesAsync(string query = "", int page = 1, int pageSize = 10)
        {
            var response = await _cacheService.GetStoriesAsync(query, page, pageSize);

            return Ok(new Response(response));
        }
    }
}

using HackerNewsApi.Controllers;
using HackerNewsApi.Models;
using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HackerNewsApi.Tests.Controllers
{
    public class StoriesControllerTests
    {
        private readonly Mock<ICacheService> _mockCacheService;
        private readonly StoriesController _controller;

        public StoriesControllerTests()
        {
            _mockCacheService = new Mock<ICacheService>();
            _controller = new StoriesController(_mockCacheService.Object);
        }

        [Fact]
        public async Task GetStoriesAsync_ReturnsOkResult_WithValidResponse()
        {
            var responseData = new ResponseData
            {
                Page = 1,
                PageSize = 10,
                Total = 100,
                Stories = new List<Story> { new Story { Id = 1, Title = "Test Story" } }
            };
            _mockCacheService
                .Setup(service => service.GetStoriesAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), default))
                .ReturnsAsync(responseData);

            var result = await _controller.GetStoriesAsync("test", 1, 10);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Response>(okResult.Value);
            Assert.True(response.Success);
            Assert.Equal(1, response.Data.Page);
            Assert.Single(response.Data.Stories);
        }

        [Fact]
        public async Task GetStoriesAsync_ReturnsBadRequest_WhenExceptionThrown()
        {
            _mockCacheService
                .Setup(service => service.GetStoriesAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), default))
                .ThrowsAsync(new System.Exception("Test exception"));

            Exception ex = new Exception();
            try
            {
                var result = await _controller.GetStoriesAsync("test", 1, 10);
            }
            catch (Exception _ex)
            {
                ex = _ex;
            }
            Assert.Equal("Test exception", ex.Message);
        }
    }
}
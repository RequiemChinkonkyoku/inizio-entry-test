using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services; // or wherever GoogleScrapeService lives
using Repositories;
using Models;
using Models.Entities;
using Repositories.Interface;
using Services.Implement;

namespace Tests.Services
{
    [TestFixture]
    public class GoogleScrapeServiceTests
    {
        private Mock<IGoogleScrapeRepository> _mockRepo;
        private GoogleScrapeService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IGoogleScrapeRepository>();
            _service = new GoogleScrapeService(_mockRepo.Object);
        }

        [Test]
        public void GetSearchResults_EmptyQuery_ThrowsArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(() => _service.GetSearchResults(""));
        }

        [Test]
        public async Task GetSearchResults_ValidQuery_ReturnsExpectedResults()
        {
            var expected = new List<SearchResult>
            {
                new SearchResult { Title = "Test Title", Link = "https://example.com" }
            };

            _mockRepo.Setup(r => r.ScrapeGoogleResults("test"))
                .ReturnsAsync(expected);
            
            var results = await _service.GetSearchResults("test");
            
            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Title, Is.EqualTo("Test Title"));
        }
    }
}
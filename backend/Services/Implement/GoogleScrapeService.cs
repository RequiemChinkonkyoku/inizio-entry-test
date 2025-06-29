using Models.Entities;
using Repositories.Implement;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implement;

public class GoogleScrapeService : IGoogleScrapeService
{
    private readonly IGoogleScrapeRepository _repo;

    public GoogleScrapeService(IGoogleScrapeRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SearchResult>> GetSearchResults(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentException("Query cannot be empty.");

        return await _repo.ScrapeGoogleResults(query);
    }
}
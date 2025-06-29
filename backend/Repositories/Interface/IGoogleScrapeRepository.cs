using Models.Entities;

namespace Repositories.Interface;

public interface IGoogleScrapeRepository
{
    Task<List<SearchResult>> ScrapeGoogleResults(string query);
}
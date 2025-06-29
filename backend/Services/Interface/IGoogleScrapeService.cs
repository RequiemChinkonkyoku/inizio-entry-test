using Models.Entities;

namespace Services.Interface;

public interface IGoogleScrapeService
{
    Task<List<SearchResult>> GetSearchResults(string query);
}
using System.Net.Http.Json;
using System.Text.Json;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Models.Entities;
using Repositories.Interface;

namespace Repositories.Implement;

public class GoogleScrapeRepository : IGoogleScrapeRepository
{
    // public async Task<List<SearchResult>> ScrapeGoogleResults(string query)
    // {
    //     var url = $"https://www.google.com/search?q={Uri.EscapeDataString(query)}&hl=en";
    //     var httpClient = new HttpClient();
    //     httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
    //
    //     var html = await httpClient.GetStringAsync(url);
    //
    //     //debugging
    //     File.WriteAllText("google-output.html", html);
    //     
    //     var doc = new HtmlDocument();
    //     doc.LoadHtml(html);
    //
    //     var results = doc.DocumentNode
    //         .SelectNodes("//div[@class='tF2Cxc']")
    //         ?.Select(node => new SearchResult
    //         {
    //             Title = node.SelectSingleNode(".//h3")?.InnerText?.Trim(),
    //             Link = node.SelectSingleNode(".//a")?.GetAttributeValue("href", "")
    //         })
    //         .Where(r => !string.IsNullOrEmpty(r.Title) && r.Link.StartsWith("http"))
    //         .ToList();
    //
    //     return results ?? new List<SearchResult>();
    // }
    
    private readonly SerpApiConfig _config;
    private readonly HttpClient _http;

    public GoogleScrapeRepository(IOptions<SerpApiConfig> config)
    {
        _config = config.Value;
        _http = new HttpClient();
    }

    public async Task<List<SearchResult>> ScrapeGoogleResults(string query)
    {
        var url = $"https://serpapi.com/search.json?q={Uri.EscapeDataString(query)}&hl=en&gl=us&api_key={_config.SerpApiKey}";

        var json = await _http.GetStringAsync(url);
        var response = JsonSerializer.Deserialize<SerpApiResponse>(json);

        var results = response?.OrganicResults?
            .Select(r => new SearchResult
            {
                Title = r.Title,
                Link = r.Link
            }).ToList();

        return results ?? new List<SearchResult>();
    }
}
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace InizioEntryTestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoogleScrapeController : ControllerBase
{
    private readonly IGoogleScrapeService _service;

    public GoogleScrapeController(IGoogleScrapeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string query)
    {
        try
        {
            var results = await _service.GetSearchResults(query);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
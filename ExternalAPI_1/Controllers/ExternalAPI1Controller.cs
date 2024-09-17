using Microsoft.AspNetCore.Mvc;

namespace ExternalAPI_1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExternalAPI1Controller : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(string searchText)
    {
        await Task.Delay(3000); // Artificial delay of 3 seconds
        return Ok($"Response from ExternalAPI_1 after 3 seconds.");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] object request)
    {
        await Task.Delay(3000); // Artificial delay of 3 seconds
        return Ok($"Response from ExternalAPI_1 after 3 seconds.");
    }
}

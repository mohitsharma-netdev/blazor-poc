using Microsoft.AspNetCore.Mvc;

namespace ExternalAPI_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExternalAPI2Controller : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(string searchText)
    {
        await Task.Delay(5000); // Artificial delay of 5 seconds
        return Ok($"Response from ExternalAPI_2 after 5 seconds.");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] object request)
    {
        await Task.Delay(5000); // Artificial delay of 5 seconds
        return Ok($"Response from ExternalAPI_2 after 5 seconds.");
    }
}

using FrontEndWebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    [HttpGet]
    public IActionResult GetTodos()
    {
        var todos = new List<string> { "Todo1", "Todo2", "Todo3" };
        var response = new ApiResponse(ResponseType.GET, todos, "Todo");

        return Ok(response);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_service.Controllers;

[ApiController]
[Authorize]
[Route("api/items")]
public class ItemController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "ReadAccess")]
    public ActionResult<string> GetItems()
    {
        try
        {
            return Ok("List of items");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
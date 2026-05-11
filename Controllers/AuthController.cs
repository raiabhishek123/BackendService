using backend_service.Interfaces;
using backend_service.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_service.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMongoDbService _mongoDbService;
    public AuthController(IMongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }
    [Route("users")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        try
        {
            var response = await _mongoDbService.GetUser();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [Route("users")]
    [HttpPost]
    public async Task<ActionResult<User>> AddUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var createdUser = await _mongoDbService.AddUser(user);
            return CreatedAtAction(nameof(AddUser), new { id = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [Route("users/{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("User ID cannot be null or empty.");
        }
        try
        {
            var deleteUser = await _mongoDbService.DeleteUser(id);
            return Ok(deleteUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

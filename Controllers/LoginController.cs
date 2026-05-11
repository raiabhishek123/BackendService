using backend_service.Interfaces;
using backend_service.Models;
using backend_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_service.Controllers;

public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [Route("api/login")]
    [HttpPost]
    public async Task<ActionResult<User>> Login([FromBody] Login login)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var loginResponse = await _loginService.LoginAsync(login);
            if (loginResponse == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new { Token = loginResponse });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [Route("api/login/refresh")]
    [HttpGet]
    public async Task<ActionResult<string>> RefreshToken()
    {
        // This method is a placeholder for token refresh logic.
        // Implement your token refresh logic here if needed.
        Thread.CurrentThread.Name = "Main thread"; // Set thread name for demonstration
        Console.WriteLine("Thread name before async call "+ Thread.CurrentThread.Name);

        await Task.Run(()=>
        {
            Thread.Sleep(2000); // Simulate some work
            Console.WriteLine("Thread name inside Task.Run "+Thread.CurrentThread.Name);
        });
        await _loginService.RefreshTokenAsync(); // Call the async method to refresh the token

        Console.WriteLine("Thread name after async call "+Thread.CurrentThread.Name);

        return Ok("Token refresh endpoint hit.");
    }
}
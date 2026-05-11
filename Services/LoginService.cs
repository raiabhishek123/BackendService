using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend_service.Interfaces;
using backend_service.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend_service.Services;

public class LoginService : ILoginService
{
    private readonly IMongoDbService _mongoDbService;
    private readonly IConfiguration _configuration;

    public LoginService(IMongoDbService mongoDbService, IConfiguration configuration)
    {
        _configuration = configuration;
        _mongoDbService = mongoDbService;
    }

    public async Task<string> LoginAsync(Login login)
    {
        var users = await _mongoDbService.GetUser();
        var user = users.FirstOrDefault(u => u.PhoneNumber == login.PhoneNumber && u.Password == login.Password);

        if (user == null)
        {
            return "Invalid phone number or password.";
        }

        var jwtKey = _configuration["JwtSettings:SecretKey"];
        var jwtIssuer = _configuration["JwtSettings:Issuer"];
        var jwtAudience = _configuration["JwtSettings:Audience"];

        var claims = new List<Claim>();

        user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> RefreshTokenAsync()
    {
        Console.WriteLine("Login service thread name "+ Thread.CurrentThread.Name);
         await Task.Delay(1000); // Simulate some delay for token refresh logic
        return "Token refresh logic not implemented yet.";
    }
}
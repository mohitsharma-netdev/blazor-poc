using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FrontEndWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration configuration)
    {
        _config = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        if (userLogin.Username == "test" && userLogin.Password == "password")
        {
            var token = GenerateJwtToken(userLogin.Username);
            return Ok(token);
        }

        return Unauthorized();
    }

    private string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
                new Claim("ShopId","1234"),
                new Claim("username",username),
                new Claim("Role","StoreAdmin")
        };
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}

public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
}

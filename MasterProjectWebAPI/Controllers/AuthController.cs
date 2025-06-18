using MasterProjectDTOModel.login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MasterProjectWebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
         private readonly IConfiguration _configuration;

            public AuthController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [AllowAnonymous]
            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginDTO login)
            {
                // Example hardcoded credentials - replace with DB validation if needed
                if (login.Username == "admin" && login.Password == "password")
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                        new Claim(ClaimTypes.Name, login.Username),
                        new Claim(ClaimTypes.Role, "Admin")
                    }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        Issuer = _configuration["Jwt:Issuer"],
                        Audience = _configuration["Jwt:Audience"],
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwt = tokenHandler.WriteToken(token);

                    return Ok(new { token = $"Bearer {jwt}" });
                }

                return Unauthorized("Invalid username or password");
            }
        
    }
}

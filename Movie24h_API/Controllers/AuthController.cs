using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Movie24h_API.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movie24h_API.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IConfiguration _config;

        public AuthController(IConfiguration config) {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login) {
            if(login.Username == "admin" && login.Password == "password") {
                var tokenStr = GenerateJwtToken(login.Username);
                return Ok(new { token = tokenStr });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(string username) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    //new Claim(ClaimTypes.Name, username),
                    //new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

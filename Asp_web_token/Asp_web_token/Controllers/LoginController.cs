using Asp_web_token.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authorization;

namespace Asp_web_token.Controllers

{
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        public LoginController(IConfiguration con)
        {
            Configuration = con;
        }

        [HttpPost]
        public ActionResult login([FromBody] userid us)
        {
            if(us.username == "Admin" && us.password == "Password")
            {
                var claims = new[] {
                        //new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, Configuration["J:S"]),
                        //new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),                                             
                        new Claim("UserName", us.username),
                        new Claim("Password", us.password)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["J:K"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    Configuration["J:I"],
                    Configuration["J:A"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));

            }
            else
            {
                return BadRequest("No output");
            }
        }
        [Authorize]
        [HttpGet]
        public string Msg()
        {
            return "Helo Boss";
        }
    }
}

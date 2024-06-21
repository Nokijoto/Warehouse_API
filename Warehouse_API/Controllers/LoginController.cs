using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Warehouse_API.Dto;

namespace Warehouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost, Route("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.UserName) ||
                string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and/or Password not specified");
                if (loginDTO.UserName.Equals("admin") &&
                loginDTO.Password.Equals("admin123"))
                {
                    var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("projekt_magazynu_na_inzynierie_oprogramowania_lab"));
                    var signinCredentials = new SigningCredentials
                    (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: "ABCXYZ",
                        audience: "http://localhost:51398",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                   return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }
            }
            catch(Exception ex)
            {
                return BadRequest
                ($"An error occurred in generating the token, {ex}");
            }
            return Unauthorized();
        }
    }
}

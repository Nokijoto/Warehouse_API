using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Warehouse_API.Dto;
using Warehouse_API.Interfaces.IServices;

namespace Warehouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogService _service;

        public LoginController(ILogService service)
        {
            _service = service;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO, ILogService service)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.UserName) ||
                string.IsNullOrEmpty(loginDTO.Password))
                    _service.Add(new LogsDto
                    {
                        CreatedAt = DateTime.Now,
                        LogType = "Error",
                        Message = "Username and/or Password not specified",
                        User = "System"
                    });
                return BadRequest("Username and/or Password not specified");
                if (IsValidUser(loginDTO, out var role))
                {
                   return Ok(GenerateJWT(loginDTO.UserName,role));
                }
            }
            catch(Exception ex)
            {
                _service.Add(new LogsDto
                {
                    CreatedAt = DateTime.Now,
                    LogType = "Error",
                    Message = $"Error Generating Token, {ex}",
                    User = "System"
                });
                return BadRequest
                ($"An error occurred in generating the token, {ex}");
            }
            return Unauthorized();
        }


        private string GenerateJWT(string username, string role)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("projekt_magazynu_na_inzynierie_oprogramowania_lab"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Role, role) 
            };

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "ABCXYZ",
                audience: "http://localhost:51398",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signinCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }


        private bool IsValidUser(LoginDTO creditials,out string role)
        {
            role = null;
            if (creditials.UserName == "admin" && creditials.Password == "admin")
            {
                role = "admin";
                return true;
            }
            if (creditials.UserName == "string" && creditials.Password == "string")
            {
                role = "system";
                return true;
            }
            if (creditials.UserName == "hr" && creditials.Password == "hr")
            {
                role = "hr";
                return true;
            }
            if (creditials.UserName == "user" && creditials.Password == "user")
            {
                role = "user";
                return true;
            }
            return false;
        }
    }
}
